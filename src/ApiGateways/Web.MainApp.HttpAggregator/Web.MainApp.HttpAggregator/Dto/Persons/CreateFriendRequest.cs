namespace Web.MainApp.HttpAggregator.Dto.Persons;

public class CreateFriendRequest
{
    public string SenderPersonIdentityGuid { get; set; }
	public string ReceiverPersonIdentityGuid { get; set; }
}