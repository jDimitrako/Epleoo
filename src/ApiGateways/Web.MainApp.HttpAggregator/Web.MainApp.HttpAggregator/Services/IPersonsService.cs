using System.Threading.Tasks;
using Web.MainApp.HttpAggregator.Models;

namespace Web.MainApp.HttpAggregator.Services;

public interface IPersonsService
{
	Task<PersonData> CreatePersonAsync(PersonData personData);
}