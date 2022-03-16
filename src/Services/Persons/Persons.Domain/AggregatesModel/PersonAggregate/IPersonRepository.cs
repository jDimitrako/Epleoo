using Persons.Domain.SeedWork;

namespace Persons.Domain.AggregatesModel.PersonAggregate;

public interface IPersonRepository : IRepository<Person>
{
	Person Add(Person person);
	Person Update(Person person);
	Task<Person?> FindAsync(string personIdentityGuid);
	Task<Person?> FindByIdAsync(int id);
}