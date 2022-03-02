using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PR.Domain.AggregatesModel.FriendshipAggregate;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQuery
{
	Task<FriendRequest> GetFriendRequestAsync(Guid userId);
}