using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.PersonAggregate;

public interface IPersonRepository : IRepository<Person>
{
	Person Add(Person person);
	Person Update(Person person);
	Task<Person> FindAsync(string personIdentityGuid);
	Task<Person> FindByIdAsync(string id);
}