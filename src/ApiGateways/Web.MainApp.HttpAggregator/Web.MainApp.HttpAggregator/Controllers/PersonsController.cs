using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Dto.Persons;
using Web.MainApp.HttpAggregator.Services.Persons;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonApiClient _client;

	public PersonsController(IPersonApiClient client)
	{
		_client = client;
	}
	
	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetPersons()
	{
		var response = await _client.GetPersons();
		
		return Ok(response);
	}
	
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.Created)]
	public async Task<IActionResult> CreatePersonAsync(CreatePersonRequest person)
	{
		var response = await _client.CreatePersonAsync(person);

		if (response.IsFailure)
			return BadRequest();
		
		return Created(string.Empty, string.Empty);
	}
	
	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType(typeof(PersonData), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<PersonData>> GetPersons()
	{
		var response = await _personsService.GetPersonsAsync();

		return Ok(_mapper.Map<CreatePersonResponse>(response));
	}
}