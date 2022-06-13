using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestCommand : IRequest<bool>
{
	[DataMember] public string SenderPersonIdentityGuid { get; set; }
	[DataMember] public string ReceiverPersonIdentityGuid { get; set; }

	public CreateFriendRequestCommand()
	{
	}

	public CreateFriendRequestCommand(string senderPersonId, string receiverPersonId)
	{
		SenderPersonIdentityGuid = senderPersonId;
		ReceiverPersonIdentityGuid = receiverPersonId;
	}
}