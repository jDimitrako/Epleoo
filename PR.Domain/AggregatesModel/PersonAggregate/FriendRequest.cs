using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class FriendRequest : Entity
{
	private readonly string _senderIdentityGuid;
	private readonly string _receiverIdentityGuid;
	private string _creatorIdentityGuid;
	private DateTimeOffset _createDate;
	private string _modifier;
	private DateTimeOffset? _modifyDate;

	public FriendRequest(string senderIdentityGuid, string receiverIdentityGuid, string creatorIdentityGuid)
	{
		_senderIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid)
			? senderIdentityGuid
			: throw new PRDomainException(nameof(senderIdentityGuid));
		_receiverIdentityGuid = !string.IsNullOrEmpty(receiverIdentityGuid)
			? receiverIdentityGuid
			: throw new PRDomainException(nameof(receiverIdentityGuid));
		_creatorIdentityGuid = !string.IsNullOrEmpty(creatorIdentityGuid)
			? creatorIdentityGuid
			: throw new PRDomainException(nameof(creatorIdentityGuid));
		_createDate = DateTimeOffset.Now;
		_modifier = !string.IsNullOrEmpty(creatorIdentityGuid)
			? creatorIdentityGuid
			: throw new PRDomainException(nameof(creatorIdentityGuid));
		_modifyDate = DateTimeOffset.Now;
	}

	public bool IsEqualTo(string senderId, string receiverId)
	{
		return _senderIdentityGuid == senderId
		       && _receiverIdentityGuid == receiverId;
	}
}