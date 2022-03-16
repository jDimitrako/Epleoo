using Microsoft.EntityFrameworkCore;
using Persons.Domain.AggregatesModel.PersonAggregate;
using Persons.Domain.SeedWork;

namespace Persons.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
	private readonly PersonDbContext _context;
	public IUnitOfWork UnitOfWork => _context;
	public PersonRepository(PersonDbContext context)
	{
		_context = context;
	}
	
	public Person Add(Person person)
	{
		if (person.IsTransient())
		{
			return _context.Persons.Add(person).Entity;
		}

		return person;
	}

	public Person Update(Person person)
	{
		return _context.Persons.Update(person).Entity;
	}

	public async Task<Person?> FindAsync(string personIdentityGuid)
	{
		try
		{
			var person = await _context.Persons.Where(p => p.IdentityGuid == personIdentityGuid)
				.FirstOrDefaultAsync();
			return person;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<Person?> FindByIdAsync(int id)
	{
		var person = await _context.Persons.Where(p => p.Id == id)
			.FirstOrDefaultAsync();
		return person;
	}
}