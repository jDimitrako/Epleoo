using System;
using System.Data.Common;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.Extensions.Logging;
using Persons.Infrastructure;

namespace Persons.API.IntegrationEvents;

public class PersonsIntegrationEventService : IPersonsIntegrationEventService
{
	private readonly IEventBus _eventBus;
	private readonly PersonDbContext _context;
	private readonly IntegrationEventLogContext _integrationEventLogContext;
	private readonly IIntegrationEventLogService _eventLogService;
	private readonly ILogger<PersonsIntegrationEventService> _logger;

	public PersonsIntegrationEventService(
		IEventBus eventBus, 
		PersonDbContext context,
		IntegrationEventLogContext integrationEventLogContext,
		Func<DbConnection, IIntegrationEventLogService> eventLogService,
		ILogger<PersonsIntegrationEventService> logger
		)
	{
		_eventBus = eventBus;
		_context = context;
		_integrationEventLogContext = integrationEventLogContext;
		_eventLogService = _;
		_logger = logger;
	}
	
	public Task PublishEventsThroughEventBusAsync(Guid transactionId)
	{
		var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

		foreach (var logEvt in pendingLogEvents)
		{
			_logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

			try
			{
				await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
				_eventBus.Publish(logEvt.IntegrationEvent);
				await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", logEvt.EventId, Program.AppName);

				await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
			}
		}	}

	public Task AddAndSaveEventAsync(IntegrationEvent evt)
	{
		throw new NotImplementedException();
	}
}