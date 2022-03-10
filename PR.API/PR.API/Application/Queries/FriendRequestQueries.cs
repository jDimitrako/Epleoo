using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace PR.API.Application.Queries;

public class FriendRequestQueries : IFriendRequestsQueries
{
	private readonly string _connectionString;

	public FriendRequestQueries(string connectionString)
	{
		_connectionString = !string.IsNullOrEmpty(connectionString)
			? connectionString
			: throw new ArgumentNullException(nameof(connectionString));
	}

	public async Task<IEnumerable<FriendRequestViewModel.FriendRequestSummary>> GetSentFriendRequestAsync(string userId)
	{
		try
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var result =
					await connection.QueryAsync<FriendRequestViewModel.FriendRequestSummary>(
						@"select fr.id, fr.senderidentityguid, fr.receiveridentityguid, fr.createddate, 
       						fr.modifier, fr.modifieddate, fr.friendshipid, fr.friendrequeststatusid, frs.Name FriendRequestStatus 
						  	from FriendRequests fr, FriendRequestStatus frs
						 	where fr.FriendRequestStatusId = frs.Id 
						       and fr.SenderIdentityGuid = @userid", new { userId });

				return result;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}

	}

	/*private FriendRequestViewModel.FriendRequest MapFriendRequest(FriendRequest friendRequest)
	{
		var result = new FriendRequestViewModel.FriendRequest()
		{
			SenderIdentityGuid = friendRequest.SenderIdentityGuid,
			ReceiverIdentityGuid = friendRequest.ReceiverIdentityGuid,
			CreatedDate = friendRequest.CreatedDate.ToString("DD/MM/YYYY"),
			Modifier = friendRequest.Modifier,
			ModifiedDate = friendRequest.ModifiedDate?.ToString("DD/MM/YYYY"),
			FriendRequestStatusId = friendRequest.FriendRequestStatus.Id
		};
		return result;
	}*/
}