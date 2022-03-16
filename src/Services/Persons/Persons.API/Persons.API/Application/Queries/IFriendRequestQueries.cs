
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persons.API.Application.Queries;

public interface IFriendRequestsQueries
{
	Task<IEnumerable<FriendRequestViewModel.FriendRequestSummary>> GetSentFriendRequestAsync(int personId);
}