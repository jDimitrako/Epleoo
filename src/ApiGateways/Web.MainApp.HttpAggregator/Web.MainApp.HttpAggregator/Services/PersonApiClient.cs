using System.Threading.Tasks;
using Web.MainApp.HttpAggregator.Models;

namespace Web.MainApp.HttpAggregator.Services;

public class PersonApiClient : IPersonApiClient
{
	public Task<PersonData> GetPersonAsync(PersonData personData)
	{
		throw new System.NotImplementedException();
	}
}