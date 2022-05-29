
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persons.API.Application.Queries;
/// <summary>
/// Interface for Person queries
/// </summary>
public interface IPersonsQueries
{
	Task<IEnumerable<PersonViewModel.Person>> GetPersons();
}