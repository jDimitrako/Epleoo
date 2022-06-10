using System.Collections.Generic;
using System.Threading.Tasks;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQueries
{
    Task<IEnumerable<FriendRequestViewModel.FriendRequestSummary>> GetFriendRequests(int senderPersonId,
        int receiverPersonId);
}