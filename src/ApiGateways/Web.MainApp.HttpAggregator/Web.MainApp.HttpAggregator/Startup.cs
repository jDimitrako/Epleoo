using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;
using GrpcPersons;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Web.MainApp.HttpAggregator.Config;
using Web.MainApp.HttpAggregator.Dto.MappingProfiles;
using Web.MainApp.HttpAggregator.Infrastructure;
using Web.MainApp.HttpAggregator.Services;

namespace Web.MainApp.HttpAggregator;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddAutoMapper(typeof(CreatePersonResponseProfile));
		
		services.AddCustomMvc(Configuration)
			.AddHttpServices(Configuration)
			.AddGrpcServices();
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
	{
		var pathBase = Configuration["PATH_BASE"];

		if (!string.IsNullOrEmpty(pathBase))
		{
			loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
			app.UsePathBase(pathBase);
		}

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseSwagger().UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json",
				"Purchase BFF V1");

			c.OAuthClientId("mobileshoppingaggswaggerui");
			c.OAuthClientSecret(string.Empty);
			c.OAuthRealm(string.Empty);
			c.OAuthAppName("Purchase BFF Swagger UI");
		});

		app.UseRouting();
		app.UseCors("CorsPolicy");
		app.UseAuthentication();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapDefaultControllerRoute();
			endpoints.MapControllers();
			/*endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
			endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
			{
				Predicate = r => r.Name.Contains("self")
			});*/
		});
	}
}

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddOptions();
		services.Configure<UrlsConfig>(configuration.GetSection("urls"));

		services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.WriteIndented = true;
				options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			});

		services.AddSwaggerGen(options =>
		{
			options.DescribeAllEnumsAsStrings();
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Epleoo Aggregator for Web Clients",
				Version = "v1",
				Description = "Epleoo Aggregator for Mobile Clients"
			});
	
			//options.OperationFilter<AuthorizeCheckOperationFilter>();
		});

		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy",
				builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed((host) => true)
					.AllowCredentials());
		});

		return services;
	}

	public static IServiceCollection AddHttpServices(this IServiceCollection services, IConfiguration configuration)
	{
		var urls = configuration.GetSection("urls");
		var personsUrl = urls.GetSection("persons").Value;
		//Configure http services
		services.AddHttpClient("Persons", client =>
		{
			client.BaseAddress = new Uri(personsUrl);
			client.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
		});
		//register delegating handlers
		//services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		//register http services
		services.AddScoped<IPersonApiClient, PersonApiClient>();

		return services;
	}

	public static IServiceCollection AddGrpcServices(this IServiceCollection services)
	{
		services.AddTransient<GrpcExceptionInterceptor>();

		services.AddScoped<IPersonsService, PersonsService>();
		AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
		AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
		services.AddGrpcClient<PersonsGrpc.PersonsGrpcClient>((services, options) =>
		{
			var httpHandler = new HttpClientHandler();
// Return `true` to allow certificates that are untrusted/invalid
			httpHandler.ServerCertificateCustomValidationCallback = 
				HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			var grpcPersons = services.GetRequiredService<IOptions<UrlsConfig>>().Value.GrpcPersons;
			options.Address = new Uri(grpcPersons);
		}).AddInterceptor<GrpcExceptionInterceptor>();


		return services;
	}
}