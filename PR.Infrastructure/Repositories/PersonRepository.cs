using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
	//private readonly 
	
	public IUnitOfWork UnitOfWork { get; }
	public Person Add(Person person)
	{
		throw new NotImplementedException();
	}

	public Person Update(Person person)
	{
		throw new NotImplementedException();
	}

	public Task<Person> FindAsync(string personIdentityGuid)
	{
		throw new NotImplementedException();
	}

	public Task<Person> FindByIdAsync(string id)
	{
		throw new NotImplementedException();
	}
}