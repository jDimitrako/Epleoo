using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.AggregatesModel.PersonAggregate;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQuery
{
	Task<FriendRequest> GetFriendRequestAsync(Guid userId);
}