using System.Reflection;
using Autofac;
using EventBus.Abstractions;
using PR.API.Application.Commands.FriendRequest;
using PR.API.Application.Queries;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Infrastructure.Idempotency;
using PR.Infrastructure.Repositories;

namespace PR.API.Infrastructure.AutofacModules;

public class ApplicationModule
	: Autofac.Module
{
	public string QueriesConnectionString { get; }

	public ApplicationModule(string qconstr)
	{
		QueriesConnectionString = qconstr;
	}

	protected override void Load(ContainerBuilder builder)
	{
		builder.Register(c => new FriendRequestQueries(QueriesConnectionString))
			.As<IFriendRequestsQueries>()
			.InstancePerLifetimeScope();

		builder.RegisterType<FriendRequestRepository>()
			.As<IFriendRequestRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<PersonRepository>()
			.As<IPersonRepository>()
			.InstancePerLifetimeScope();

		builder.RegisterType<RequestManager>()
			.As<IRequestManager>()
			.InstancePerLifetimeScope();

		builder.RegisterAssemblyTypes(typeof(CreateFriendRequestCommand).GetTypeInfo().Assembly)
			.AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
	}
}