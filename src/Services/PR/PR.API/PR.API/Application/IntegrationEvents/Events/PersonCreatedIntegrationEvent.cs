using EventBus.Events;

namespace PR.API.Application.IntegrationEvents.Events;

public record PersonCreatedIntegrationEvent : IntegrationEvent
{
	public string PersonId { get; init; }

	public PersonCreatedIntegrationEvent(string personId) => PersonId = personId;
	
}