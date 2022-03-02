using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PR.Domain.Events;

namespace PR.API.Application.DomainEventHandlers.FriendshipRequestAcceptedEvent;

public class AddFriendshipAggregateWhenFriendshipRequestAcceptedEventHandler : INotificationHandler<FriendRequestAcceptedDomainEvent>
{
	public Task Handle(FriendRequestAcceptedDomainEvent notification, CancellationToken cancellationToken)
	{
		throw new System.NotImplementedException();
	}
}