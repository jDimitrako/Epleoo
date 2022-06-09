using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Dto;

namespace Web.MainApp.HttpAggregator.Services;

public interface IPersonApiClient
{
    Task<Result> CreatePersonAsync(CreatePersonRequest request);
    
    Task<PersonData> GetPersonAsync(PersonData personData);
}
