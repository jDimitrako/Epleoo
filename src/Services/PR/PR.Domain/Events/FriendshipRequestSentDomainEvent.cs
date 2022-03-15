using MediatR;

namespace PR.Domain.Events;

public class FriendshipRequestSentDomainEvent : INotification
{
	public string SenderIdentityGuid { get; }
	public string ReceiverIdentityGuid { get; }

	public FriendshipRequestSentDomainEvent(string senderIdentityGuid, string receiverIdentityGuid)
	{
		SenderIdentityGuid = senderIdentityGuid;
		ReceiverIdentityGuid = receiverIdentityGuid;
	}
}