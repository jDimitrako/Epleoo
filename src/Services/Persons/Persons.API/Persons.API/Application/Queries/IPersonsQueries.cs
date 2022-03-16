
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persons.API.Application.Queries;

public interface IPersonsQueries
{
	Task<IEnumerable<PersonViewModel.Person>> GetPersons();
}