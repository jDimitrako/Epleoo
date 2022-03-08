using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

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

	public async Task<IEnumerable<FriendRequest>> GetSentFriendRequestAsync(string userId)
	{
		using (var connection = new SqlConnection(_connectionString))
		{
			connection.Open();

			var result =
				await connection.QueryAsync<FriendRequest>(
					@"select * from [pr.service].FriendRequests fr
						 where fr.SenderIdentityGuid = @id", new {userId});

			return result;
		}
	}
}