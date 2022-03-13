using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public class Friendship : Entity
{
	public string SenderId { get; }
	public Person Sender { get; set; }
	public string ReceiverId { get; }
	public Person Receiver { get; set; }

	public Friendship()
	{
	}

	public Friendship(string senderId, string receiverId)
	{
		SenderId = senderId;
		ReceiverId = receiverId;
	}
}