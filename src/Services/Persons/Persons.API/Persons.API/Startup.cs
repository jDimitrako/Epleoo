using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using EventBusServiceBus;
using HealthChecks.UI.Client;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persons.API.Application.IntegrationEvents;
using Persons.API.Application.IntegrationEvents.Events;
using Persons.API.Controllers;
using Persons.API.Grpc;
using Persons.API.Infrastructure.AutofacModules;
using Persons.API.Infrastructure.Filters;
using Persons.API.Middlewares;
using Persons.Infrastructure;
using RabbitMQ.Client;

namespace Persons.API;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	// This method gets called by the runtime. Use this method to add services to the container.
	public virtual IServiceProvider ConfigureServices(IServiceCollection services)
	{
		services
			.AddGrpc(options => { options.EnableDetailedErrors = true; })
			.Services
			.AddCustomMvc()
			.AddHealthChecks(Configuration)
			.AddCustomDbContext(Configuration)
			.AddCustomSwagger(Configuration)
			.AddCustomIntegrations(Configuration)
			.AddCustomConfiguration(Configuration)
			.AddEventBus(Configuration)
			; //.AddCustomAuthentication(Configuration);

		var container = new ContainerBuilder();
		container.Populate(services);
		//services.AddControllers();
		//services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Persons.API", Version = "v1" }); });
		container.RegisterModule(new MediatorModule());
		container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

		return new AutofacServiceProvider(container.Build());

	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persons.API v1"));
		}

		var pathBase = Configuration["PATH_BASE"];
		
		//app.UseHttpsRedirection();
		
		app.UseSwagger()
			.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint(
					$"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json",
					"Persons.API V1");
				c.OAuthClientId("personsswaggerui");
				c.OAuthAppName("Persons Swagger UI");
			});

		app.UseRouting();
		app.UseMiddleware<ExceptionMiddleware>();

		app.UseCors("CorsPolicy");
		ConfigureAuth(app);

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapGrpcService<PersonService>();
			endpoints.MapDefaultControllerRoute();
			endpoints.MapControllers();
			endpoints.MapGet("/_proto/", async ctx =>
			{
				ctx.Response.ContentType = "text/plain";
				using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "persons.proto"), FileMode.Open, FileAccess.Read);
				using var sr = new StreamReader(fs);
				while (!sr.EndOfStream)
				{
					var line = await sr.ReadLineAsync();
					if (line != "/* >>" || line != "<< */")
					{
						await ctx.Response.WriteAsync(line);
					}
				}
			});
			endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
			{
				Predicate = _ => true,
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
			endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
			{
				Predicate = r => r.Name.Contains("self")
			});
		});

		ConfigureEventBus(app);
	}
	
	private  void ConfigureEventBus(IApplicationBuilder app)
	{
		var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
		eventBus.Subscribe<PersonCreatedIntegrationEvent, IIntegrationEventHandler<PersonCreatedIntegrationEvent>>();
		/*eventBus.Subscribe<UserCheckoutAcceptedIntegrationEvent, IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>>();
		eventBus.Subscribe<GracePeriodConfirmedIntegrationEvent, IIntegrationEventHandler<GracePeriodConfirmedIntegrationEvent>>();
		eventBus.Subscribe<OrderStockConfirmedIntegrationEvent, IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>>();
		eventBus.Subscribe<OrderStockRejectedIntegrationEvent, IIntegrationEventHandler<OrderStockRejectedIntegrationEvent>>();
		eventBus.Subscribe<OrderPaymentFailedIntegrationEvent, IIntegrationEventHandler<OrderPaymentFailedIntegrationEvent>>();
		eventBus.Subscribe<OrderPaymentSucceededIntegrationEvent, IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>>();*/
	}
	
	public  virtual void ConfigureAuth(IApplicationBuilder app)
	{
		app.UseAuthentication();
		app.UseAuthorization();
	}
	
}

static class CustomExtensionsMethods
{
	/*public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration)
	{
	    services.AddApplicationInsightsTelemetry(configuration);
	    services.AddApplicationInsightsKubernetesEnricher();

	    return services;
	}*/

	public static IServiceCollection AddCustomMvc(this IServiceCollection services)
	{
		// Add framework services.
		services.AddControllers(options => { options.Filters.Add(typeof(HttpGlobalExceptionFilter)); })
			// Added for functional tests
			.AddApplicationPart(typeof(PersonsController).Assembly)
			.AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true)
			.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

		services.AddCors(options =>
		{
			options.AddPolicy("CorsPolicy",
				builder => builder
					.SetIsOriginAllowed((host) => true)
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
		});

		return services;
	}

	public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
	{
		var hcBuilder = services.AddHealthChecks();

		hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

		hcBuilder
			.AddSqlServer(
				configuration["ConnectionString"],
				name: "PersonsDB-check",
				tags: new string[] { "personsdb" });

		hcBuilder
			.AddRabbitMQ(
				$"amqp://{configuration["EventBusConnection"]}",
				name: "persons-rabbitmqbus-check",
				tags: new string[] { "rabbitmqbus" });

		return services;
	}

	public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<PersonDbContext>(options =>
			{
				options.UseSqlServer(configuration["ConnectionString"],
					sqlServerOptionsAction: sqlOptions =>
					{
						sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
						sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
					});
			},
			ServiceLifetime
				.Scoped //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
		);

		services.AddDbContext<IntegrationEventLogContext>(options =>
		{
			options.UseSqlServer(configuration["ConnectionString"],
				sqlServerOptionsAction: sqlOptions =>
				{
					sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
					//Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
					sqlOptions.EnableRetryOnFailure(maxRetryCount: 1, maxRetryDelay: TimeSpan.FromSeconds(1),
						errorNumbersToAdd: null);
				});
		});

		return services;
	}

	public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Epleoo - Persons(People Relations) HTTP API",
				Version = "v1",
				Description = "The Persons(People Relation) HTTP API"
			});
			/*options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows()
				{
					Implicit = new OpenApiOAuthFlow()
					{
						AuthorizationUrl =
							new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
						TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
						Scopes = new Dictionary<string, string>()
						{
							{ "persons", "Persons(People Relations)" }
						}
					}
				}
			});

			options.OperationFilter<AuthorizeCheckOperationFilter>();*/
		});

		return services;
	}

	public static IServiceCollection AddCustomIntegrations(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		//services.AddTransient<IIdentityService, IdentityService>();
		services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
			sp => (DbConnection c) => new IntegrationEventLogService(c));

		services.AddTransient<IPersonIntegrationEventService, PersonIntegrationEventService>();

		//services.AddScoped<IFriendshipRepository, FriendshipRepository>();

		services.AddTransient<IPersonIntegrationEventService, PersonIntegrationEventService>();
		
		services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
		{
			var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


			var factory = new ConnectionFactory()
			{
				HostName = configuration["EventBusConnection"],
				DispatchConsumersAsync = true
			};

			if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
			{
				factory.UserName = configuration["EventBusUserName"];
			}

			if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
			{
				factory.Password = configuration["EventBusPassword"];
			}

			var retryCount = 5;
			if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
			{
				retryCount = int.Parse(configuration["EventBusRetryCount"]);
			}

			return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
		});

		return services;
	}

	public static IServiceCollection AddCustomConfiguration(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddOptions();
		services.Configure<PrSettings>(configuration);
		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = context =>
			{
				var personsoblemDetails = new ValidationProblemDetails(context.ModelState)
				{
					Instance = context.HttpContext.Request.Path,
					Status = StatusCodes.Status400BadRequest,
					Detail = "Please refer to the errors personsoperty for additional details."
				};

				return new BadRequestObjectResult(personsoblemDetails)
				{
					ContentTypes = { "application/personsoblem+json", "application/personsoblem+xml" }
				};
			};
		});

		return services;
	}

	public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
	{
		if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
		{
			services.AddSingleton<IEventBus, EventBusServiceBus.EventBusServiceBus>(sp =>
			{
				var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
				var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				var logger = sp.GetRequiredService<ILogger<EventBusServiceBus.EventBusServiceBus>>();
				var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
				string subscriptionName = configuration["SubscriptionClientName"];

				return new EventBusServiceBus.EventBusServiceBus(serviceBusPersisterConnection, logger,
					eventBusSubcriptionsManager, iLifetimeScope, subscriptionName);
			});
		}
		else
		{
			services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
			{
				var subscriptionClientName = configuration["SubscriptionClientName"];
				var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
				var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
				var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

				var retryCount = 5;
				if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
				{
					retryCount = int.Parse(configuration["EventBusRetryCount"]);
				}

				return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
					eventBusSubcriptionsManager, subscriptionClientName, retryCount);
			});
		}

		services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

		return services;
	}

	public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
		IConfiguration configuration)
	{
		// personsevent from mapping "sub" claim to nameidentifier.
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

		var identityUrl = configuration.GetValue<string>("IdentityUrl");

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme =
				Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme =
				Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options =>
		{
			options.Authority = identityUrl;
			options.RequireHttpsMetadata = false;
			options.Audience = "persons";
		});

		return services;
	}
	
}