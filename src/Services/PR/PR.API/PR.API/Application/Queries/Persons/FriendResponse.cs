namespace PR.API.Application.Queries.Persons;

public class FriendResponse
{
	public FriendResponse(int personId, string personIdentityGuid)
	{
		PersonId = personId;
		PersonIdentityGuid = personIdentityGuid;
	}

	public int PersonId { get; }
	public string PersonIdentityGuid { get; }
}