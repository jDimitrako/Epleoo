using System;
using System.Threading.Tasks;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Application.Queries;

public interface IFriendRequestsQuery
{
	Task<FriendRequest> GetFriendRequestAsync(Guid userId);
}