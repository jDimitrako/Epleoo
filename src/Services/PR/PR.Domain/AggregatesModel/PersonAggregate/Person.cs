using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot
{
	public override int Id
	{
		get => _id;
		protected set => _id = value;
	}

	private int _id;
	public string IdentityGuid { get; }

	private readonly List<Friendship> _friendshipsSent;
	private readonly List<Friendship> _friendshipsReceived;

	public IReadOnlyCollection<Friendship> FriendshipsSent => _friendshipsSent;
	public IReadOnlyCollection<Friendship> FriendshipsReceived => _friendshipsReceived;

	private Person()
	{
		_friendshipsReceived = new List<Friendship>();
		_friendshipsSent = new List<Friendship>();
	}

	public Person(string identityGuid) : this()
	{
		IdentityGuid = !string.IsNullOrEmpty(identityGuid)
			? identityGuid
			: throw new ArgumentNullException(nameof(identityGuid));
	}

	public void AddFriendship(int senderGuid, int receiverGuid)
	{
		var friendshipToAdd = new Friendship(senderGuid, receiverGuid);
		_friendshipsSent.Add(friendshipToAdd);
	}
}