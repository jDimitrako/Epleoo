using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PR.Infrastructure;

namespace PR.API.Infrastructure.Factories;

public class PrDbContextFactory : IDesignTimeDbContextFactory<PrDbContext>
{
	public PrDbContext CreateDbContext(string[] args)
	{
		var config = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();

		var optionsBuilder = new DbContextOptionsBuilder<PrDbContext>();

		optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("PR.API"));

		return new PrDbContext(optionsBuilder.Options);	}
}