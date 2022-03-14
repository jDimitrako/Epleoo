using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot 
{
	public string IdentityGuid { get; }
	public string Username { get; }
	public string FirstName { get; }
	public string LastName { get; }

	private readonly List<Friendship> _friendshipsSent;
	private readonly List<Friendship> _friendshipsReceived;
	
	public IReadOnlyCollection<Friendship> FriendshipsSent => _friendshipsSent;
	public IReadOnlyCollection<Friendship> FriendshipsReceived => _friendshipsReceived;
	
	private Person()
	{
	}

	public Person(string identityGuid, string username, string firstName, string lastName) : this()
	{
		IdentityGuid = !string.IsNullOrEmpty(identityGuid) ? identityGuid : throw new ArgumentNullException(nameof(identityGuid));
		Username = !string.IsNullOrEmpty(username) ? username : throw new ArgumentNullException(nameof(username));
		FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
		LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));

	}

	public void AddFriendship(int senderGuid, int receiverGuid)
	{
		var friendshipToAdd = new Friendship(senderGuid, receiverGuid);
		_friendshipsSent.Add(friendshipToAdd);
	}
}