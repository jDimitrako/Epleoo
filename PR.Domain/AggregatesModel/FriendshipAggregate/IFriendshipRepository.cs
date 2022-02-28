using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public interface IFriendshipRepository : IRepository<Friendship>
{
	Friendship Add(Friendship friendship);
	Friendship Update(Friendship friendship);
	Task<Friendship> FindByIdAsync(int id);
}