using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PR.API.Application.Queries.Persons;

public class PersonRequestResponse
{
	public PersonRequestResponse(int id, string identityGuid, List<FriendResponse> friendshipsSent,
		List<FriendResponse> friendshipsReceived)
	{
		Id = id;
		IdentityGuid = identityGuid;
		FriendshipsSent = friendshipsSent;
		FriendshipsReceived = friendshipsReceived;
		Friends = friendshipsReceived.Concat(friendshipsSent);
	}

	[JsonIgnore]
	public int Id { get; }
	[JsonIgnore]
	public string IdentityGuid { get; }
	[JsonIgnore]
	public IReadOnlyCollection<FriendResponse> FriendshipsSent { get; }
	[JsonIgnore]
	public IList FriendshipsReceived { get; }
	public IEnumerable<FriendResponse> Friends { get; set; }
}