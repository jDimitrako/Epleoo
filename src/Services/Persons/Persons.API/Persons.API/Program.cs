using System;
using System.IO;
using System.Net;
using IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persons.API;
using Persons.API.Infrastructure;
using Persons.Infrastructure;
using Serilog;
using WebHost.Customization;

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", Persons.API.Program.AppName);
    var host = BuildWebHost(configuration, args);

    Log.Information("Applying migrations ({ApplicationContext})...", Persons.API.Program.AppName);
    host.MigrateDbContext<PersonDbContext>((context, services) =>
    {
        var env = services.GetService<IWebHostEnvironment>();
        var settings = services.GetService<IOptions<PrSettings>>();
        var logger = services.GetService<ILogger<PersonContextSeed>>();

        Log.Information($"PrSettings: {settings.Value}");
        
        new PersonContextSeed()
            .SeedAsync(context, env, settings, logger)
            .Wait();
    })
    .MigrateDbContext<IntegrationEventLogContext>((_, __) => { });

    Log.Information("Starting web host ({ApplicationContext})...", Persons.API.Program.AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Persons.API.Program.AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IWebHost BuildWebHost(IConfiguration configuration, string[] args) => 
    Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureKestrel(options =>
        {
            var ports = GetDefinedPorts(configuration);
            options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });

            options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        })
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Persons.API.Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
        .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();

    /*
    if (config.GetValue<bool>("UseVault", false))
    {
        TokenCredential credential = new ClientSecretCredential(
            config["Vault:TenantId"],
            config["Vault:ClientId"],
            config["Vault:ClientSecret"]);
        builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);
    }
    */

    return builder.Build();
}

(int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
{
    var grpcPort = config.GetValue("GRPC_PORT", 55201);
    var port = config.GetValue("PORT", 55103);
    return (port, grpcPort);
}

namespace Persons.API
{
    public partial class Program
    {

        public static string Namespace = typeof(Startup).Namespace;
        public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    }
}