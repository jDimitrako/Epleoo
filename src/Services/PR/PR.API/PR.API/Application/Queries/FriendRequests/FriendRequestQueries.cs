using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace PR.API.Application.Queries.FriendRequests;

public class FriendRequestQueries : IFriendRequestsQueries
{
	private readonly string _connectionString;

	public FriendRequestQueries(string connectionString)
	{
		_connectionString = !string.IsNullOrEmpty(connectionString)
			? connectionString
			: throw new ArgumentNullException(nameof(connectionString));
	}

	public async Task<IEnumerable<FriendRequestResponse.FriendRequestSummary>> GetFriendRequests(
		string senderPersonId, string receiverPersonId)
	{
		try
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();
				var sqlCommand = @"select fr.id, fr.SenderPersonId, fr.ReceiverPersonId, fr.createddate, 
       						fr.modifier, fr.modifieddate, fr.friendrequeststatusid, frs.Name FriendRequestStatus 
						  	from FriendRequests fr, FriendRequestStatus frs, persons psender, persons preceiver
						 	where fr.FriendRequestStatusId = frs.Id 
						 	  and psender.Id = fr.SenderPersonId
						 	  and preceiver.Id = fr.ReceiverPersonId ";

				if (!string.IsNullOrEmpty(senderPersonId))
					sqlCommand += "and preceiver.IdentityGuid = @senderId ";
				if (!string.IsNullOrEmpty(receiverPersonId))
					sqlCommand += "and psender.IdentityGuid = @receiverId";

				var result =
					await connection.QueryAsync<FriendRequestResponse.FriendRequestSummary>(
						sqlCommand,
						new { senderId = senderPersonId, receiverId = receiverPersonId });

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