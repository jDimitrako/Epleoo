using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestHandler : IRequestHandler<CreateFriendRequestCommand, bool>
{
	private readonly IFriendRequestRepository _friendRequestRepository;
	private readonly ILogger<CreateFriendRequestCommand> _logger;

	public CreateFriendRequestHandler(IFriendRequestRepository friendRequestRepository,
		ILogger<CreateFriendRequestCommand> logger)
	{
		_friendRequestRepository =
			friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
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
		var exists = await _friendRequestRepository.Exists(request.SenderIndentityGuid, request.ReceiverIndentityGuid);
		if (exists)
			return false;

		var friendRequest =
			new Domain.AggregatesModel.FriendRequestAggregate.FriendRequest(request.SenderIndentityGuid,
				request.ReceiverIndentityGuid);

		_logger.LogInformation("----- Creating FriendRequest - FriendRequest: {@FriendRequest}", friendRequest);

		_friendRequestRepository.Add(friendRequest);

		return await _friendRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
	}
}