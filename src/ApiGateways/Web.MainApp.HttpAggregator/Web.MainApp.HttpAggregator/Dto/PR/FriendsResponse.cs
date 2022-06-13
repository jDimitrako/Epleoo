using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.MainApp.HttpAggregator.Dto.PR;

public class FriendsResponse
{
	[JsonPropertyName("friends")]
	public List<FriendResponse> Friends { get; set; } = new List<FriendResponse>();
}