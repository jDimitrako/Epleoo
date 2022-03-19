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
		var person = new Domain.AggregatesModel.FriendshipAggregate.Person(0, request.PersonId);
		var result = _personRepository.Add(person);
		return await _personRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
	}
}