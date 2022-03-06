using PR.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public class FriendRequest : Entity, IAggregateRoot
{
	public FriendRequest()
	{ }
	public string SenderIdentityGuid { get; }
	public string ReceiverIdentityGuid { get; }
	public string CreatorIdentityGuid { get; }
	public DateTimeOffset CreatedDate { get; }
	public string Modifier { get; }
	public DateTimeOffset? ModifiedDate { get; }
	public FriendRequestStatus FriendRequestStatus { get; private set; }


	public FriendRequest(string senderIdentityGuid, string receiverIdentityGuid)
	{
		SenderIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		ReceiverIdentityGuid = !string.IsNullOrEmpty(receiverIdentityGuid)
			? receiverIdentityGuid
			: throw new PRDomainException(nameof(receiverIdentityGuid));
		CreatorIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		CreatedDate = DateTimeOffset.Now;
		Modifier = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		ModifiedDate = DateTimeOffset.Now;
	}

	public bool IsEqualTo(string senderId, string receiverId)
	{
		return SenderIdentityGuid == senderId
		       && ReceiverIdentityGuid == receiverId;
	}
}