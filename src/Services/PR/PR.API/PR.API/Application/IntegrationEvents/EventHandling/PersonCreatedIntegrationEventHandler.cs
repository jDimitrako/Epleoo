using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.API.Application.Commands.Person;
using PR.API.Application.IntegrationEvents.Events;
using Serilog.Context;

namespace PR.API.Application.IntegrationEvents.EventHandling;

public class PersonCreatedIntegrationEventHandler : IIntegrationEventHandler<PersonCreatedIntegrationEvent>
{
	private readonly IMediator _mediator;
	private readonly ILogger<PersonCreatedIntegrationEventHandler> _logger;

	public PersonCreatedIntegrationEventHandler(
		IMediator mediator,
		ILogger<PersonCreatedIntegrationEventHandler> logger
	)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public async Task Handle(PersonCreatedIntegrationEvent @event)
	{
		using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}-{Program.AppName}"))
		{
			_logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

			var command = new CreatePersonCommand(@event.PersonId);

			_logger.LogInformation(
				"----- Sending command: {CommandName} - {IdProperty}: ({@Command})",
				command.GetGenericTypeName(),
				nameof(command.PersonId),
				command);

			await _mediator.Send(command);
		}
	}
}