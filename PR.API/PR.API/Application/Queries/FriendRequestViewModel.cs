using System;

namespace PR.API.Application.Queries;

public class FriendRequestViewModel
{
	public record FriendRequest
	{
		public string SenderIdentityGuid { get; init; }
		public string ReceiverIdentityGuid { get; init; }
		public string CreatedDate { get; init; }
		public string Modifier { get; init; }
		public string ModifiedDate { get; init; }
		public int FriendRequestStatusId { get; set; }
		public string FriendRequestStatus { get; set; }
	}

	public record FriendRequestSummary
	{
		public int Id { get; init; }
		public string SenderIdentityGuid { get; init; }
		public string ReceiverIdentityGuid { get; init; }
		public DateTimeOffset CreatedDate { get; init; }
		public string Modifier { get; init; }
		public DateTimeOffset ModifiedDate { get; init; }
		public int FriendRequestStatusId { get; set; }
		public string FriendRequestStatus { get; set; }
	}

	public record FriendRequestStatus
	{
		public int Id { get; init; }
		public string Name { get; init; }
	}
}