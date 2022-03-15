using Microsoft.EntityFrameworkCore;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.SeedWork;

namespace PR.Infrastructure.Repositories;

public class FriendRequestRepository : IFriendRequestRepository
{
	private readonly PrDbContext _context;

	public FriendRequestRepository(PrDbContext context)
	{
		_context = context;
	}

	public FriendRequest Add(FriendRequest friendRequest)
	{
		if (friendRequest.IsTransient())
		{
			return _context.FriendRequests.Add(friendRequest).Entity;
		}

		return friendRequest;
	}

	public FriendRequest Update(FriendRequest friendRequest)
	{
		return _context.FriendRequests.Update(friendRequest).Entity;
	}

	public async Task<FriendRequest?> FindByIdAsync(int friendRequestId)
	{
		var friendship = await _context.FriendRequests
			.Where(f => f.Id == friendRequestId)
			.SingleOrDefaultAsync();

		return friendship;
	}

	public Task<bool> Exists(int senderPersonId, int receiverPersonId)
	{
		try
		{
			return Task.FromResult(_context.FriendRequests.Any(f =>
				f.ReceiverPersonId == receiverPersonId && f.SenderPersonId == senderPersonId));
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public IUnitOfWork UnitOfWork => _context;
}