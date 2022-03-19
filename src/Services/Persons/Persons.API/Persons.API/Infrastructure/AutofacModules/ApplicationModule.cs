using System.Reflection;
using Autofac;
using EventBus.Abstractions;
using Persons.API.Application.Commands.Persons;
using Persons.API.Application.Queries;
using Persons.Domain.AggregatesModel.PersonAggregate;
using Persons.Infrastructure.Idempotency;
using Persons.Infrastructure.Repositories;

namespace Persons.API.Infrastructure.AutofacModules;

public class ApplicationModule
    : Autofac.Module
{

    public string QueriesConnectionString { get; }

    public ApplicationModule(string qconstr)
    {
        QueriesConnectionString = qconstr;

    }

    protected  override void Load(ContainerBuilder builder)
    {

        builder.Register(c => new PersonQueries(QueriesConnectionString))
            .As<IPersonsQueries>()
            .InstancePerLifetimeScope();

        builder.RegisterType<PersonRepository>()
            .As<IPersonRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<RequestManager>()
            .As<IRequestManager>()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(typeof(CreatePersonCommandHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

    }
}
