using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

	public int Id { get; }
	public string IdentityGuid { get; }

	public IReadOnlyCollection<FriendResponse> FriendshipsSent { get; }
	public IList FriendshipsReceived { get; }
	public IEnumerable<FriendResponse> Friends { get; set; }
}