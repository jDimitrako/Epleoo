using System;
using System.Data.Common;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persons.Infrastructure;

namespace Persons.API.IntegrationEvents;

public class PersonsIntegrationEventService : IPersonsIntegrationEventService
{
	private readonly IEventBus _eventBus;
	private readonly PersonDbContext _context;
	private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
	private readonly IIntegrationEventLogService _eventLogService;
	private readonly ILogger<PersonsIntegrationEventService> _logger;

	public PersonsIntegrationEventService(
		IEventBus eventBus,
		PersonDbContext context,
		IIntegrationEventLogService eventLogService,
		Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
		ILogger<PersonsIntegrationEventService> logger
	)
	{
		_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_integrationEventLogServiceFactory = integrationEventLogServiceFactory ??
		                                     throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
		_eventLogService = _integrationEventLogServiceFactory(_context.Database.GetDbConnection());
		;
	}

	public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
	{
		var pendingLogEvents = await _eventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId);

		foreach (var logEvt in pendingLogEvents)
		{
			_logger.LogInformation(
				"----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})",
				logEvt.EventId, Program.AppName, logEvt.IntegrationEvent);

			try
			{
				await _eventLogService.MarkEventAsInProgressAsync(logEvt.EventId);
				_eventBus.Publish(logEvt.IntegrationEvent);
				await _eventLogService.MarkEventAsPublishedAsync(logEvt.EventId);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}",
					logEvt.EventId, Program.AppName);

				await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);
			}
		}
	}

	public async Task AddAndSaveEventAsync(IntegrationEvent evt)
	{
		_logger.LogInformation(
			"----- Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})", evt.Id, evt);

		await _eventLogService.SaveEventAsync(evt, _context.GetCurrentTransaction());
	}
}