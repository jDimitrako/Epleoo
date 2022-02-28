using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public class FriendRequest : Entity
{
	public FriendRequest()
	{ }
	public string SenderIdentityGuid { get; }
	public string ReceiverIdentityGuid { get; }
	public string CreatorIdentityGuid { get; }
	public DateTimeOffset CreateDate { get; }
	public string Modifier { get; }
	public DateTimeOffset? ModifyDate { get; }

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
		CreateDate = DateTimeOffset.Now;
		Modifier = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		ModifyDate = DateTimeOffset.Now;
	}

	public bool IsEqualTo(string senderId, string receiverId)
	{
		return SenderIdentityGuid == senderId
		       && ReceiverIdentityGuid == receiverId;
	}
}