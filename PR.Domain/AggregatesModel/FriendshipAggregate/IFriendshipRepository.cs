using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendshipAggregate;

public interface IPersonRepository : IRepository<Person>
{
	Person Add(Person person);
	Person Update(Person person);
	Task<Person?> FindAsync(int personIdentityGuid);
	Task<Person?> FindByIdAsync(int id);
}