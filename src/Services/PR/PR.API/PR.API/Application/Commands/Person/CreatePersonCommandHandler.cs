using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.Domain.AggregatesModel.FriendshipAggregate;

namespace PR.API.Application.Commands.Person;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
{
	private readonly IPersonRepository _personRepository;
	private readonly ILogger<CreatePersonCommandHandler> _logger;

	public CreatePersonCommandHandler(
		IPersonRepository personRepository,
		ILogger<CreatePersonCommandHandler> logger
	)
	{
		_personRepository = personRepository;
		_logger = logger;
	}

	public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var person = new Domain.AggregatesModel.FriendshipAggregate.Person(request.PersonId);
			var result = _personRepository.Add(person);
			var result1 = await _personRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
			return result1;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

	}
}