using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PR.API.Application.Queries.Persons;

namespace PR.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonsQueries _personsQueries;

	public PersonsController(IPersonsQueries personsQueries)
	{
		_personsQueries = personsQueries;
	}
	
	[HttpGet("{personIdentityGuid}")]
	[ProducesResponseType(typeof(PersonRequestResponse), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetFriendRequests(string personIdentityGuid)
	{
		var friendRequests = await _personsQueries.GetPerson(personIdentityGuid);

		return Ok(friendRequests);
	}
}