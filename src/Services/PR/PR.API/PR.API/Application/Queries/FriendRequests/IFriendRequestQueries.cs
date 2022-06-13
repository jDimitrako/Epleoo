using System.Collections.Generic;
using System.Threading.Tasks;
using PR.API.Application.Queries.FriendRequests;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQueries
{
    Task<IEnumerable<FriendRequestResponse.FriendRequestSummary>> GetFriendRequests(string senderPersonId, string receiverPersonId);
}