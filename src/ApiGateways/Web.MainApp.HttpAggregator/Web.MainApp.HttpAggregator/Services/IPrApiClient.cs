using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Dto;

namespace Web.MainApp.HttpAggregator.Services;

public interface IPrApiClient
{
    Task<Result> CreateFriendRequest(CreateFriendshipRequest createFriendshipRequest);
}