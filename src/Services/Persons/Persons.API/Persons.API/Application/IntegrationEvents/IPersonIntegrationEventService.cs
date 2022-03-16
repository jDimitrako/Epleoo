using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace Persons.API.Application.IntegrationEvents;

public interface IPersonIntegrationEventService
{
	Task PublishEventsThroughEventBusAsync(Guid transactionId);
	Task AddAndSaveEventAsync(IntegrationEvent evt);
}