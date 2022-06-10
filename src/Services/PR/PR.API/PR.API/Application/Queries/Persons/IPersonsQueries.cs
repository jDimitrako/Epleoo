using System.Threading.Tasks;

namespace PR.API.Application.Queries.Persons;

public interface IPersonsQueries
{
	Task<PersonRequestResponse> GetPerson(string personIdentityGuid);
}