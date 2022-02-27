using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot
{
	public string IdentityGuid { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }

	private List<Friend> _friends;
	private List<FriendRequest> _friendRequests;

	public IEnumerable<Friend> Friends => _friends.AsReadOnly();
	public IEnumerable<FriendRequest> FriendRequests => _friendRequests.AsReadOnly();

	protected Person()
	{
		_friends = new List<Friend>();
	}

	public Person(string identity, string firstName, string lastName)
	{
		IdentityGuid = !string.IsNullOrEmpty(identity) ? identity : throw new ArgumentNullException(nameof(identity));
		FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
		LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
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

		var friendRequest = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, senderIdentityGuid);
		
		_friendRequests.Add(friendRequest);
		
		//TODO: Add domain event

		return friendRequest;

	}
}