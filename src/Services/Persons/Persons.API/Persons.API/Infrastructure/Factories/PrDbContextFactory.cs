using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persons.Infrastructure;

namespace Persons.API.Infrastructure.Factories;

public class PrDbContextFactory : IDesignTimeDbContextFactory<PersonDbContext>
{
	public PersonDbContext CreateDbContext(string[] args)
	{
		var config = new ConfigurationBuilder()
			.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
			.AddJsonFile("appsettings.json")
			.AddEnvironmentVariables()
			.Build();

		var optionsBuilder = new DbContextOptionsBuilder<PersonDbContext>();

		optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly("Persons.API"));

		return new PersonDbContext(optionsBuilder.Options);	}
}