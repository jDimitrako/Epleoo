using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.FriendRequest;

public class AcceptFriendRequestCommand : IRequest<bool>
{
	[DataMember] public int FriendRequestId { get; set; }

	public AcceptFriendRequestCommand(int friendRequestId)
	{
		FriendRequestId = friendRequestId;
	}
}