using System.Text.Json.Serialization;

namespace Web.MainApp.HttpAggregator.Dto.PR;

public class FriendRequestDto
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("senderPersonId")]
	public int SenderPersonId { get; set; }
	[JsonPropertyName("receiverPersonId")]
	public int ReceiverPersonId { get; set; }
	[JsonPropertyName("createdDate")]
	public string CreatedDate { get; set; }
	[JsonPropertyName("modifiedDate")]
	public string ModifiedDate { get; set; }
	[JsonPropertyName("friendRequestStatus")]
	public string FriendRequestStatus { get; set; }
}