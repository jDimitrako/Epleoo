using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.Domain.AggregatesModel.PersonAggregate;

namespace Persons.API.Application.Commands.Persons;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
{
	private readonly IPersonRepository _personRepository;
	private readonly ILogger<CreatePersonCommandHandler> _logger;
	private readonly IMediator _mediator;

	public CreatePersonCommandHandler(
		IPersonRepository personRepository,
		ILogger<CreatePersonCommandHandler> logger,
		IMediator mediator
		)
	{
		_personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_mediator = mediator;
	}
	

	public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		var person = new Person(request.IdentityGuid, request.Username, request.FirstName, request.LastName,
			request.KnownAs, request.Bio);
		
		_logger.LogInformation("----- Creating Person: {@Person}", person);

		return await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
	}
}