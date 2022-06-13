using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.AggregatesModel.PersonAggregate;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestCommandHandler : IRequestHandler<CreateFriendRequestCommand, bool>
{
	private readonly IFriendRequestRepository _friendRequestRepository;
	private readonly IPersonRepository _personRepository;
	private readonly ILogger<CreateFriendRequestCommandHandler> _logger;

	public CreateFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository,
		IPersonRepository personRepository,
		ILogger<CreateFriendRequestCommandHandler> logger)
	{
		_friendRequestRepository =
			friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
		_personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
		;
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	/// <summary>
	/// Handler which process then command when person sends a friend request to another person
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	/// <exception cref="NotImplementedException"></exception>
	public async Task<bool> Handle(CreateFriendRequestCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var sender = await _personRepository.FindAsync(request.SenderPersonIdentityGuid);
			var receiver = await _personRepository.FindAsync(request.ReceiverPersonIdentityGuid);

			if (sender == null)
				return false;
			if (receiver == null)
				return false;


			var exists = await _friendRequestRepository.Exists(sender.Id, receiver.Id);
			if (exists)
				return false;

			var friendRequest =
				new Domain.AggregatesModel.FriendRequestAggregate.FriendRequest(sender.Id,
					receiver.Id, FriendRequestStatus.AwaitingConfirmation.Id);

			_logger.LogInformation("----- Creating FriendRequest - FriendRequest: {@FriendRequest}", friendRequest);

			_friendRequestRepository.Add(friendRequest);

			return await _friendRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}