using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PR.Domain.AggregatesModel.PersonAggregate;

namespace PR.API.Application.Commands;

public class CreateFriendRequestHandler : IRequestHandler<CreateFriendRequestCommand, bool>
{
	private readonly IPersonRepository _personRepository;

	public CreateFriendRequestHandler(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}
	
	/// <summary>
	/// Handler which process then command when person sends a friend request to another person
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public  Task<bool> Handle(CreateFriendRequestCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult(true);
	}
}