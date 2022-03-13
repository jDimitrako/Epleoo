using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public interface IFriendshipRepository : IRepository<Friendship>
{
	Person Add(Person person);
	Person Update(Person person);
	Task<Person> FindAsync(string personIdentityGuid);
	Task<Person> FindByIdAsync(string id);
}