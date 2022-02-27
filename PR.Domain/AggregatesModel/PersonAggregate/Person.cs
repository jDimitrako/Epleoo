using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot
{
	public string IdentityGuid { get; }
	public string Username { get; }
	public string FirstName { get; }
	public string LastName { get; }

	private readonly List<Friendship> _friends;
	private readonly List<FriendRequest> _friendRequests;

	public IEnumerable<Friendship> Friends => _friends.AsReadOnly();
	public IEnumerable<FriendRequest> FriendRequests => _friendRequests.AsReadOnly();

	private Person()
	{
		_friends = new List<Friendship>();
		_friendRequests = new List<FriendRequest>();
	}

	public Person(string identityGuid, string username, string firstName, string lastName) : this()
	{
		IdentityGuid = !string.IsNullOrEmpty(identityGuid) ? identityGuid : throw new ArgumentNullException(nameof(identityGuid));
		Username = !string.IsNullOrEmpty(username) ? username : throw new ArgumentNullException(nameof(username));
		FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
		LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));

	}

	public FriendRequest SendFriendRequest(string receiverIdentityGuid)
	{
		var existingFriendRequest =
			_friendRequests.SingleOrDefault(f => f.IsEqualTo(IdentityGuid, receiverIdentityGuid));

		if (existingFriendRequest != null)
		{
			//TODO: Add domain event
			return existingFriendRequest;
		}

		var friendRequest = new FriendRequest(IdentityGuid, receiverIdentityGuid);
		
		_friendRequests.Add(friendRequest);
		
		//TODO: Add domain event

		return friendRequest;
	}

	public Friendship? AcceptFriendRequest(int friendRequestId)
	{
		var friendRequest = _friendRequests.SingleOrDefault(f => f.Id == friendRequestId && f.ReceiverIdentityGuid == IdentityGuid);
		
		if (friendRequest == null) throw new PRDomainException(nameof(friendRequestId));

		return new Friendship();
	}
}