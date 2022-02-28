using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PR.API;

public partial class Program
{
	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}

public partial class Program
{
	public static string Namespace = typeof(Startup).Namespace;
	public static string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}