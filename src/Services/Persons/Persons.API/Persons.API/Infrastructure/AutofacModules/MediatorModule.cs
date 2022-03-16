﻿using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using Persons.API.Application.Behaviors;
using Persons.API.Application.Commands.Persons;
using Persons.API.Application.Validations;

namespace Persons.API.Infrastructure.AutofacModules;

public class MediatorModule : Autofac.Module
{
    protected  override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

        // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
        builder.RegisterAssemblyTypes(typeof(CreatePersonRequestCommand).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));

        // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
        /*
        builder.RegisterAssemblyTypes(typeof(AddPersonFriendshipWhenFriendshipRequestAcceptedEventHandler).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(INotificationHandler<>));
            */

        // Register the Command's Validators (Validators based on FluentValidation library)
        builder
            .RegisterAssemblyTypes(typeof(CreatePersonRequestValidator).GetTypeInfo().Assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();


        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
        });

        builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
     //   builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

    }
}
