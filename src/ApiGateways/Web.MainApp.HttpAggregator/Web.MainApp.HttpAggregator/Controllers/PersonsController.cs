using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Models;
using Web.MainApp.HttpAggregator.Services;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonsService _personsService;

	public PersonsController(IPersonsService personsService)
	{
		_personsService = personsService;
	}
	
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType(typeof(PersonData), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<PersonData>> CreatePersonAsync([FromBody] PersonData person)
	{
		var response = await _personsService.CreatePersonAsync(person);

		return Ok();
	}
}