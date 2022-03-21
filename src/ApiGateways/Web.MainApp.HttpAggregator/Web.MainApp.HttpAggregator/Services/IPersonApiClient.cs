using System.Threading.Tasks;
using Web.MainApp.HttpAggregator.Models;

namespace Web.MainApp.HttpAggregator.Services;

public interface IPersonApiClient
{
    Task<PersonData> GetPersonAsync(PersonData personData);
}
