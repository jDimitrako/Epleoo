using System.Text.Json.Serialization;

namespace Web.MainApp.HttpAggregator.Dto.PR;

public class FriendResponse
{
	[JsonPropertyName("personId")]
	public int PersonId { get; set; }
	[JsonPropertyName("personIdentityGuid")]
	public string PersonIdentityGuid { get; set; }
}