using System;
using System.Threading.Tasks;
using EventBus.Events;

namespace Persons.API.Application.IntegrationEvents;
/// <summary>
/// Interface for Persons integrations events service
/// </summary>
public interface IPersonIntegrationEventService
{
	Task PublishEventsThroughEventBusAsync(Guid transactionId);
	Task AddAndSaveEventAsync(IntegrationEvent evt);
}