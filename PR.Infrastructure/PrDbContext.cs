using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.AggregatesModel.PersonAggregate;
using PR.Domain.SeedWork;
using PR.Infrastructure.EntityConfigurations;

namespace PR.Infrastructure;

public class PrDbContext : DbContext, IUnitOfWork
{

	public DbSet<FriendRequest> FriendRequests { get; set; }
	public DbSet<Person> Persons { get; set; }
	public DbSet<FriendRequestStatus> FriendRequestStatus { get; set; }

	
	private readonly IMediator _mediator;
	private IDbContextTransaction _currentTransaction;
	public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

	public bool HasActiveTransaction => _currentTransaction != null;

	public PrDbContext(DbContextOptions<PrDbContext> options) : base(options)
	{
	}

	public PrDbContext(DbContextOptions<PrDbContext> options, IMediator mediator) : base(options)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new FriendRequestEntityTypeConfiguration());
		modelBuilder.ApplyConfiguration(new FriendRequestStatusEntityTypeConfiguration());
	}
	
	public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		//TODO: Check this
		// Dispatch Domain Events collection. 
		// Choices:
		// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
		// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
		// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
		// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
		await _mediator.DispatchDomainEventsAsync(this);

		// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
		// performed through the DbContext will be committed
		var result = await base.SaveChangesAsync(cancellationToken);

		return true;
	}

	public async Task<IDbContextTransaction> BeginTransactionAsync()
	{
		if (_currentTransaction != null) return null;

		_currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

		return _currentTransaction;
	}

	public async Task CommitTransactionAsync(IDbContextTransaction transaction)
	{
		if (transaction == null) throw new ArgumentNullException(nameof(transaction));
		if (transaction != _currentTransaction)
			throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

		try
		{
			await SaveChangesAsync();
			transaction.Commit();
		}
		catch
		{
			RollbackTransaction();
			throw;
		}
		finally
		{
			if (_currentTransaction != null)
			{
				_currentTransaction.Dispose();
				_currentTransaction = null;
			}
		}
	}

	public void RollbackTransaction()
	{
		try
		{
			_currentTransaction?.Rollback();
		}
		finally
		{
			if (_currentTransaction != null)
			{
				_currentTransaction.Dispose();
				_currentTransaction = null;
			}
		}
	}
}