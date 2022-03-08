using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQueries
{
	Task<IEnumerable<FriendRequest>> GetSentFriendRequestAsync(string userId);
}