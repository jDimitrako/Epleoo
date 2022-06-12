namespace Web.MainApp.HttpAggregator.Dto.PR;

public class GetFriendRequestsRequest
{
	public string SenderPersonIdentityGuid { get; }
	public string ReceiverPersonIdentityGuid { get; }

	public GetFriendRequestsRequest(string senderPersonIdentityGuid, string receiverPersonIdentityGuid)
	{
		SenderPersonIdentityGuid = senderPersonIdentityGuid;
		ReceiverPersonIdentityGuid = receiverPersonIdentityGuid;
	}
}