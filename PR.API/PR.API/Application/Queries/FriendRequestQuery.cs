using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Queries;

public class FriendRequestQuery : IFriendRequestsQuery
{
	private readonly string _connectionString;

	public FriendRequestQuery(string connectionString)
	{
		_connectionString = !string.IsNullOrEmpty(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
	}
	
	public async Task<FriendRequest> GetFriendRequestAsync(Guid userId)
	{
		using (var connection = new SqlConnection(_connectionString))
		{
			connection.Open();

			return new FriendRequest();

			//return await connection.QueryAsync<FriendRequest>(@"Select 1 from dual", new { userId });
		}
	}
}