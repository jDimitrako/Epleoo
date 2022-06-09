using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Dto;

namespace Web.MainApp.HttpAggregator.Services;

public interface IPersonApiClient
{
    Task<Result> CreatePersonAsync(CreatePersonRequest request);

    Task<IList<PersonData>> GetPersons();

    /// <summary>
    /// Grpc Method to fetch Persons
    /// </summary>
    /// <param name="personData"></param>
    /// <returns></returns>
    Task<PersonData> GetPersonAsync(PersonData personData);
}
