using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class FriendRequest : Entity
{
	public string SenderIdentityGuid { get; }
	public string ReceiverIdentityGuid { get; }
	private string _creatorIdentityGuid;
	private DateTimeOffset _createDate;
	private string _modifier;
	private DateTimeOffset? _modifyDate;

	public FriendRequest(string senderIdentityGuid, string receiverIdentityGuid)
	{
		SenderIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		ReceiverIdentityGuid = !string.IsNullOrEmpty(receiverIdentityGuid)
			? receiverIdentityGuid
			: throw new PRDomainException(nameof(receiverIdentityGuid));
		_creatorIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		_createDate = DateTimeOffset.Now;
		_modifier = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		_modifyDate = DateTimeOffset.Now;
	}

	public bool IsEqualTo(string senderId, string receiverId)
	{
		return SenderIdentityGuid == senderId
		       && ReceiverIdentityGuid == receiverId;
	}
}