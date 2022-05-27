using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.API.Application.IntegrationEvents;
using Persons.API.Application.IntegrationEvents.Events;
using Persons.Domain.AggregatesModel.PersonAggregate;

namespace Persons.API.Application.Commands.Persons;
/// <summary>
/// Create person command handler
/// </summary>
public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<string>>
{
	private readonly IPersonRepository _personRepository;
	private readonly ILogger<CreatePersonCommandHandler> _logger;
	private readonly IPersonIntegrationEventService _personIntegrationEventService;

	public CreatePersonCommandHandler(
		IPersonRepository personRepository,
		ILogger<CreatePersonCommandHandler> logger,
		IPersonIntegrationEventService personIntegrationEventService
		)
	{
		_personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_personIntegrationEventService = personIntegrationEventService;
	}
	

	public async Task<Result<string>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
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

			if (!result) return Result.Failure<string>(string.Empty);
			
			var personCreatedIntegrationEvent = new PersonCreatedIntegrationEvent(person.IdentityGuid);
			await _personIntegrationEventService.AddAndSaveEventAsync(personCreatedIntegrationEvent);
				
			return Result.Success(person.IdentityGuid);
		}
		catch (Exception e)
		{
			_logger.LogError(e, nameof(CreatePersonCommandHandler));
			Console.WriteLine(e);
			throw;
		}
		
	}
}