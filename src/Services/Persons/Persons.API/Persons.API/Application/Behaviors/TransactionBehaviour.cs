using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Persons.API.Application.IntegrationEvents;
using Persons.Infrastructure;

namespace Persons.API.Application.Behaviors;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private  readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;
    private  readonly PersonDbContext _dbContext;
    private  readonly IPrIntegrationEventService _personsIntegrationEventService;

    public TransactionBehaviour(PersonDbContext dbContext,
        IPrIntegrationEventService orderingIntegrationEventService,
        ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
    {
        _dbContext = dbContext ?? throw new ArgumentException(nameof(PersonDbContext));
        _personsIntegrationEventService = orderingIntegrationEventService ?? throw new ArgumentException(nameof(_personsIntegrationEventService));
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
    }

    //TODO: Check this -- maybe add serilog
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var response = default(TResponse);
        /*
        var typeName = request.GetGenericTypeName();

        try
        {
            if (_dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = _dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                using (var transaction = await _dbContext.BeginTransactionAsync())
                using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
                {
                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

                    response = await next();

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                    await _dbContext.CommitTransactionAsync(transaction);

                    transactionId = transaction.TransactionId;
                }

                await _personsIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
            });
            */

            return response;
        }
        /*catch (Exception ex)
        {
            _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }*/
    //}
}
