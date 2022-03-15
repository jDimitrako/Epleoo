using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PR.API.Application.DomainEventHandlers.FriendshipRequestSentDomainEvent;

public class FriendshipRequestSentDomainEventHandler : INotificationHandler<Domain.Events.FriendshipRequestSentDomainEvent>
{
	public Task Handle(Domain.Events.FriendshipRequestSentDomainEvent notification, CancellationToken cancellationToken)
	{
		throw new System.NotImplementedException();
	}
}