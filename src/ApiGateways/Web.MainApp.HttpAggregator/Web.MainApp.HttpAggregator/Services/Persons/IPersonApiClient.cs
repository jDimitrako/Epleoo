using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Dto.Persons;

namespace Web.MainApp.HttpAggregator.Services.Persons;

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
