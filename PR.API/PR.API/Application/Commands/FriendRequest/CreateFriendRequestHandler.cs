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
	private readonly IMediator _mediator;
	public CreateFriendRequestHandler(IFriendRequestRepository friendRequestRepository,
		ILogger<CreateFriendRequestCommand> logger, IMediator mediator)
	{
		_friendRequestRepository =
			friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
			var exists = await _friendRequestRepository.Exists(request.SenderIndentityGuid, request.ReceiverIndentityGuid);
			if (exists)
				return false;

			var friendRequest =
				new Domain.AggregatesModel.FriendRequestAggregate.FriendRequest(request.SenderIndentityGuid,
					request.ReceiverIndentityGuid, FriendRequestStatus.AwaitingConfirmation.Id);

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