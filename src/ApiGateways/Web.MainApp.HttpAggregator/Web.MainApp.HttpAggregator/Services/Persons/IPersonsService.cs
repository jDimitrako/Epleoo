using System.Threading.Tasks;
using Web.MainApp.HttpAggregator.Dto.Persons;

namespace Web.MainApp.HttpAggregator.Services.Persons;

public interface IPersonsService
{
	Task<PersonData> CreatePersonAsync(PersonData personData);
}