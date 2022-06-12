using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Dto.Persons;
using Web.MainApp.HttpAggregator.Dto.PR;

namespace Web.MainApp.HttpAggregator.Services.PR;

public interface IPrApiClient
{
    Task<Result> CreateFriendRequest(CreateFriendRequest createFriendRequest);
    Task<IList<FriendRequestDto>> GetFriendRequests();
    Task<Result> AcceptFriendRequest(int friendRequestId);
    Task<FriendsResponse> GetFriendshipsAsync(string personIdentityGuid);
}