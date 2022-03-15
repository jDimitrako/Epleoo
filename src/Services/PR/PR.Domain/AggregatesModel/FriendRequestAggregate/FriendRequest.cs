using PR.Domain.Events;
using PR.Domain.Exceptions;
using PR.Domain.SeedWork;

namespace PR.Domain.AggregatesModel.FriendRequestAggregate;

public class FriendRequest : Entity, IAggregateRoot
{
	public FriendRequest()
	{
	}

	public int SenderPersonId { get; }
	public int ReceiverPersonId { get; }
	public DateTimeOffset CreatedDate { get; }
	public int Modifier { get; }
	public DateTimeOffset? ModifiedDate { get; }
	public FriendRequestStatus FriendRequestStatus { get; private set; }
	private int _friendRequestStatusId;

	public FriendRequest(int senderIdentityId, int receiverIdentityId, int statusId)
	{
		SenderPersonId = senderIdentityId > 0
			? senderIdentityId
			: throw new PRDomainException(nameof(senderIdentityId));
		ReceiverPersonId = receiverIdentityId > 0
			? receiverIdentityId
			: throw new PRDomainException(nameof(receiverIdentityId));
		CreatedDate = DateTimeOffset.Now;
		Modifier = senderIdentityId;
		ModifiedDate = DateTimeOffset.Now;
		_friendRequestStatusId = statusId;
	}

	public bool IsEqualTo(int senderId, int receiverId)
	{
		return SenderPersonId == senderId
		       && ReceiverPersonId == receiverId;
	}

	public void SetAcceptedFriendRequestStatus()
	{
		if (_friendRequestStatusId != FriendRequestStatus.AwaitingConfirmation.Id)
		{
			StatusChangeException(FriendRequestStatus.Confirmed);
		}

		_friendRequestStatusId = FriendRequestStatus.Confirmed.Id;
		AddDomainEvent(new FriendRequestAcceptedDomainEvent(this));
	}

	private void StatusChangeException(FriendRequestStatus friendRequestStatusToChange)
	{
		throw new PRDomainException(
			$"Is not possible to change the friend request status from {FriendRequestStatus.Name} to {friendRequestStatusToChange.Name}.");
	}
}