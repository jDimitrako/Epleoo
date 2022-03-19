using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.API.Application.IntegrationEvents;
using Persons.API.Application.IntegrationEvents.Events;
using Persons.Domain.AggregatesModel.PersonAggregate;

namespace Persons.API.Application.Commands.Persons;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
{
	private readonly IPersonRepository _personRepository;
	private readonly ILogger<CreatePersonCommandHandler> _logger;
	private readonly IMediator _mediator;
	private readonly PersonIntegrationEventService _personIntegrationEventService;

	public CreatePersonCommandHandler(
		IPersonRepository personRepository,
		ILogger<CreatePersonCommandHandler> logger,
		IMediator mediator,
		PersonIntegrationEventService personIntegrationEventService
		)
	{
		_personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		_personIntegrationEventService = personIntegrationEventService;
	}
	

	public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var person = new Person(request.IdentityGuid, request.Username, request.FirstName, request.LastName,
				request.KnownAs, request.Bio);
		
			if (string.IsNullOrEmpty(request.IdentityGuid))
				person.SetIdentityGuid(Guid.NewGuid().ToString());

			_logger.LogInformation("----- Creating Person: {@Person}", person);

			_personRepository.Add(person);

			var result = await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

			if (!result) return false;
			
			var personCreatedIntegrationEvent = new PersonCreatedIntegrationEvent(person.IdentityGuid);
			await _personIntegrationEventService.AddAndSaveEventAsync(personCreatedIntegrationEvent);
				
			return true;
		}
		catch (Exception e)
		{
			_logger.LogError(e, nameof(CreatePersonCommandHandler));
			Console.WriteLine(e);
			throw;
		}
		
	}
}