using System.Collections.Generic;
using System.Threading.Tasks;
using PR.API.Application.Queries.FriendRequests;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Queries.Persons;

public class PersonRequestResponse
{
	public PersonRequestResponse(int id, string identityGuid, IReadOnlyCollection<FriendResponse> friendshipsSent,
		List<FriendResponse> friendshipsReceived)
	{
		Id = id;
		IdentityGuid = identityGuid;
		FriendshipsSent = friendshipsSent;
		FriendshipsReceived = friendshipsReceived;
	}

	public int Id { get; }
	public string IdentityGuid { get; }

	public IReadOnlyCollection<FriendResponse> FriendshipsSent { get; }
	public IReadOnlyCollection<FriendResponse> FriendshipsReceived { get; }
}