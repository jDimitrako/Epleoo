using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.FriendRequest;

public class CreateFriendRequestCommand : IRequest<bool>
{
	[DataMember] public string SenderIndentityGuid { get; set; }
	[DataMember] public string ReceiverIndentityGuid { get; set; }

	public CreateFriendRequestCommand()
	{
	}

	public CreateFriendRequestCommand(string senderIndentityGuid, string receiverIndentityGuid)
	{
		SenderIndentityGuid = senderIndentityGuid;
		ReceiverIndentityGuid = receiverIndentityGuid;
	}
}