using MediatR;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.Domain.Events;
/// <summary>
/// Event used when a friend request accepted
/// </summary>
public class FriendRequestAcceptedDomainEvent : INotification
{
	public FriendRequest FriendRequest { get; }

	public FriendRequestAcceptedDomainEvent(FriendRequest friendRequest)
	{
		FriendRequest = friendRequest;
	}
}