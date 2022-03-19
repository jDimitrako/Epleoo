using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace Persons.API.IntegrationEvents;

public interface IPersonsIntegrationEventService
{
	Task PublishEventsThroughEventBusAsync(Guid transactionId);
	Task AddAndSaveEventAsync(IntegrationEvent evt);
}