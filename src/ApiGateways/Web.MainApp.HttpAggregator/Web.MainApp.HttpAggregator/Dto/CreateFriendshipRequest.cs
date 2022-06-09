namespace Web.MainApp.HttpAggregator.Dto;

public class CreateFriendshipRequest
{
    public int SenderPersonId { get; }
    public int ReceiverPersonId { get; }

    public CreateFriendshipRequest(int senderPersonId, int receiverPersonId)
    {
        SenderPersonId = senderPersonId;
        ReceiverPersonId = receiverPersonId;
    }
}