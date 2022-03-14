using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestCommand : IRequest<bool>
{
	[DataMember] public int SenderPersonId { get; set; }
	[DataMember] public int ReceiverPersonId { get; set; }

	public CreateFriendRequestCommand()
	{
	}

	public CreateFriendRequestCommand(int senderPersonId, int receiverPersonId)
	{
		SenderPersonId = senderPersonId;
		ReceiverPersonId = receiverPersonId;
	}
}