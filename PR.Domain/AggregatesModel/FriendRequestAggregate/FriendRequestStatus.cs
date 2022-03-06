using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public class FriendRequestStatus : Enumeration
{
	public static FriendRequestStatus Sent = new FriendRequestStatus(1, nameof(Sent).ToLowerInvariant());

	public static FriendRequestStatus AwaitingConfirmation =
		new FriendRequestStatus(2, nameof(AwaitingConfirmation).ToLowerInvariant());

	public static FriendRequestStatus Confirmed = new FriendRequestStatus(3, nameof(Confirmed).ToLowerInvariant());
	public static FriendRequestStatus Established = new FriendRequestStatus(4, nameof(Established).ToLowerInvariant());
	public static FriendRequestStatus Removed = new FriendRequestStatus(5, nameof(Removed).ToLowerInvariant());
	public static FriendRequestStatus Cancelled = new FriendRequestStatus(6, nameof(Cancelled).ToLowerInvariant());

	public FriendRequestStatus(int id, string name) : base(id, name)
	{
	}
}