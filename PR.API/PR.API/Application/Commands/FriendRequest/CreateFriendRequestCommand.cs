using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestCommand : IRequest<bool>
{
	[DataMember] public string SenderIdentityGuid { get; set; }
	[DataMember] public string ReceiverIdentityGuid { get; set; }

	public CreateFriendRequestCommand()
	{
	}

	public CreateFriendRequestCommand(string senderIdentityGuid, string receiverIdentityGuid)
	{
		SenderIdentityGuid = senderIdentityGuid;
		ReceiverIdentityGuid = receiverIdentityGuid;
	}
}