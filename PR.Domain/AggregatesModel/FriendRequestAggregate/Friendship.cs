using PR.Domain.Events;
using PR.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public class Friendship : Entity
{
	public string SenderIdentityGuid { get; }
	public string ReceiverIdentityGuid { get; }
	
	private readonly List<FriendRequest> _friendRequests;

	public IEnumerable<FriendRequest> FriendRequests => _friendRequests.AsReadOnly();
	
	public Friendship()
	{
		_friendRequests = new List<FriendRequest>();
	}

	public Friendship(string senderIdentityGuid, string receiverIdentityGuid)
	{
		SenderIdentityGuid = senderIdentityGuid;
		ReceiverIdentityGuid = receiverIdentityGuid;
	}
}