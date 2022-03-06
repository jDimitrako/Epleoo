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
	
	public Friendship? AcceptFriendRequest(int friendRequestId)
	{
		var friendRequest = _friendRequests.SingleOrDefault(f => f.Id == friendRequestId);
		
		if (friendRequest == null) throw new PRDomainException(nameof(friendRequestId));
		
		

		return new Friendship();
	}
	
	public FriendRequest SendFriendRequest(string senderIdentityGuid, string receiverIdentityGuid)
	{
		var existingFriendRequest =
			_friendRequests.SingleOrDefault(f => f.IsEqualTo(senderIdentityGuid, receiverIdentityGuid));

		if (existingFriendRequest != null)
		{
			//TODO: Add domain event
			return existingFriendRequest;
		}

		var friendRequest = new FriendRequest(senderIdentityGuid, receiverIdentityGuid);
		
		_friendRequests.Add(friendRequest);
		
		//TODO: Add domain event

		return friendRequest;
	}
	
	private void FriendshipRequestSentDomainEvent(string receiverIdentityGuid)
	{
		var friendshipRequestSentDomainEvent =
			new FriendshipRequestSentDomainEvent(this.SenderIdentityGuid, receiverIdentityGuid);

		this.AddDomainEvent(friendshipRequestSentDomainEvent);
	}
}