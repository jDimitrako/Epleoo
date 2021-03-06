using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Commands.FriendRequest;

public class AcceptFriendRequestCommandHandler : IRequestHandler<AcceptFriendRequestCommand, bool>
{
	private readonly IFriendRequestRepository _friendRequestRepository;
	private readonly ILogger<CreateFriendRequestCommandHandler> _logger;
	private readonly IMediator _mediator;

	public AcceptFriendRequestCommandHandler(IFriendRequestRepository friendRequestRepository,
		ILogger<CreateFriendRequestCommandHandler> logger,
		IMediator mediator)
	{
		_friendRequestRepository =
			friendRequestRepository ?? throw new ArgumentNullException(nameof(friendRequestRepository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	public async Task<bool> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var friendRequestToAccept = await _friendRequestRepository.FindByIdAsync(request.FriendRequestId);
			if (friendRequestToAccept == null) return false;
		
			friendRequestToAccept.SetAcceptedFriendRequestStatus();
			var result =  await _friendRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
			return result;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}