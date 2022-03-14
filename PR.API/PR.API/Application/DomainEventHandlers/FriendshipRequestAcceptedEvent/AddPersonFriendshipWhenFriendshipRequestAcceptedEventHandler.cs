using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PR.Domain.AggregatesModel.FriendshipAggregate;
using PR.Domain.Events;

namespace PR.API.Application.DomainEventHandlers.FriendshipRequestAcceptedEvent;

public class AddPersonFriendshipWhenFriendshipRequestAcceptedEventHandler : INotificationHandler<FriendRequestAcceptedDomainEvent>
{
	private readonly IPersonRepository _personRepository;
	private readonly ILoggerFactory _logger;

	public AddPersonFriendshipWhenFriendshipRequestAcceptedEventHandler(IPersonRepository personRepository, ILoggerFactory logger)
	{
		_personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
		_logger = logger;
	}
	
	public async Task Handle(FriendRequestAcceptedDomainEvent notification, CancellationToken cancellationToken)
	{
		var sender = await _personRepository.FindAsync(notification.FriendRequest.SenderPersonId);
		if (sender == null) throw new ArgumentNullException(nameof(notification));
		
		sender.AddFriendship(notification.FriendRequest.SenderPersonId, notification.FriendRequest.ReceiverPersonId);

		 await _personRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
		 
		 _logger.CreateLogger<AddPersonFriendshipWhenFriendshipRequestAcceptedEventHandler>()
			 .LogTrace("Person with Id: {Id} has been successfully create a friendship with person {Id2}",
				 notification.FriendRequest.SenderPersonId, notification.FriendRequest.ReceiverPersonId);
	}
}