using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot 
{
	public string IdentityGuid { get; }
	public string Username { get; }
	public string FirstName { get; }
	public string LastName { get; }
	
	private readonly List<Friendship> _friendships;
	
	public IReadOnlyCollection<Friendship> Friendships => _friendships;

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

	public void AddFriendship(string senderGuid, string receiverGuid)
	{
		var friendshipToAdd = new Friendship(senderGuid, receiverGuid);
		_friendships.Add(friendshipToAdd);
	}
}