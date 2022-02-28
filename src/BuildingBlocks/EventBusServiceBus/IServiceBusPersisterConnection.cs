using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace EventBusServiceBus;

public interface IServiceBusPersisterConnection : IDisposable
{
    ServiceBusClient TopicClient { get; }
    ServiceBusAdministrationClient AdministrationClient { get; }
}