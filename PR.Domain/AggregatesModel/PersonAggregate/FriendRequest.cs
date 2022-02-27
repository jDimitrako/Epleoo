using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class FriendRequest : Entity
{
	private string _senderIdentityGuid;
	private string _receiverIdentityGuid;
	private string _creator;
	private DateTimeOffset _createDate;
	private string _modifier;
	private DateTimeOffset? _modifyDate;

	public FriendRequest(string senderIdentityGuid, string receiverIdentityGuid, string creator)
	{
		_senderIdentityGuid = !string.IsNullOrEmpty(senderIdentityGuid) ? senderIdentityGuid : throw new PRDomainException(nameof(senderIdentityGuid));
		_receiverIdentityGuid = !string.IsNullOrEmpty(receiverIdentityGuid) ? receiverIdentityGuid : throw  new PRDomainException(nameof(receiverIdentityGuid));
		_creator = !string.IsNullOrEmpty(creator) ? creator : throw new PRDomainException(nameof(creator));
		_createDate = DateTimeOffset.Now;
		_modifier = !string.IsNullOrEmpty(creator) ? creator : throw new PRDomainException(nameof(creator));
		_modifyDate = DateTimeOffset.Now;
	}
	
}