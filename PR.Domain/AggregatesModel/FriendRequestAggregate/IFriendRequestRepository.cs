using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public interface IFriendRequestRepository : IRepository<FriendRequest>
{
	FriendRequest Add(FriendRequest friendRequest);
	FriendRequest Update(FriendRequest friendRequest);
	Task<FriendRequest> FindByIdAsync(int friendRequestId);
	Task<bool> Exists(string senderIdentityGuid, string receiverIdentityGuid);
}