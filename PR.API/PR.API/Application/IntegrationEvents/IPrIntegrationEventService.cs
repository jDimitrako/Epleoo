using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace PR.API.Application.IntegrationEvents;

public interface IPrIntegrationEventService
{
	Task PublishEventsThroughEventBusAsync(Guid transactionId);
	Task AddAndSaveEventAsync(IntegrationEvent evt);
}