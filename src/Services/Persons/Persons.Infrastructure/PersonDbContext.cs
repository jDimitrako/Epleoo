using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persons.Domain.AggregatesModel.PersonAggregate;
using Persons.Domain.SeedWork;
using Persons.Infrastructure.EntityConfigurations;

namespace Persons.Infrastructure;

public class PersonDbContext : DbContext, IUnitOfWork
{

	public DbSet<Person> Persons { get; set; }

	
	private readonly IMediator _mediator;
	private IDbContextTransaction _currentTransaction;
	public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

	public bool HasActiveTransaction => _currentTransaction != null;

	public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
	{
	}

	public PersonDbContext(DbContextOptions<PersonDbContext> options, IMediator mediator) : base(options)
	{
		_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
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