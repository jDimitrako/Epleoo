using Microsoft.EntityFrameworkCore;
using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Domain.SeedWork;

namespace PR.Infrastructure.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
	private readonly PrDbContext _context;

	public FriendshipRepository(PrDbContext context)
	{
		_context = context;
	}

	public Friendship Add(Friendship friendship)
	{
		if (friendship.IsTransient())
		{
			return _context.Friendships.Add(friendship).Entity;
		}

		return friendship;
	}

	public Friendship Update(Friendship friendship)
	{
		return _context.Friendships.Update(friendship).Entity;
	}

	public async Task<Friendship> FindByIdAsync(int friendshipId)
	{
		var friendship = await _context.Friendships
			.Where(f => f.Id == friendshipId)
			.SingleOrDefaultAsync();

		return friendship!;
	}

	public IUnitOfWork UnitOfWork => _context;
}