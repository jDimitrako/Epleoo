using Microsoft.EntityFrameworkCore;
using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;

namespace PR.Infrastructure.Repositories;

public class FriendshipRepository : IPersonRepository
{
	private readonly PrDbContext _context;

	public FriendshipRepository(PrDbContext context)
	{
		_context = context;
	}
	
	public IUnitOfWork UnitOfWork { get; }
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

	public async Task<Person?> FindAsync(int personIdentityGuid)
	{
		var person = await _context.Persons.Where(p => p.Id == personIdentityGuid)
			.FirstOrDefaultAsync();
		return person;
	}

	public async Task<Person?> FindByIdAsync(int id)
	{
		var person = await _context.Persons.Where(p => p.Id == id)
			.FirstOrDefaultAsync();
		return person;
	}
}