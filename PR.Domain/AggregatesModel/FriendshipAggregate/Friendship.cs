using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public class Friendship : Entity, IAggregateRoot 
{
	public int SenderId { get; }
	public Person Sender { get; set; }
	public int ReceiverId { get; }
	public Person Receiver { get; set; }

	//private readonly >

	public Friendship()
	{
	}

	public Friendship(string senderIdentityGuid, string receiverIdentityGuid)
	{

	}
}