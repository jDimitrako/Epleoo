using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public class Friendship : Entity
{
	public int SenderId { get; }
	public Person Sender { get; set; }
	public int ReceiverId { get; }
	public Person Receiver { get; set; }

	public Friendship()
	{
	}

	public Friendship(int senderId, int receiverId)
	{
		SenderId = senderId;
		ReceiverId = receiverId;
	}
}