using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Models;
using Web.MainApp.HttpAggregator.Responses;
using Web.MainApp.HttpAggregator.Services;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonsService _personsService;
	private readonly IMapper _mapper;

	public PersonsController(IPersonsService personsService, IMapper mapper)
	{
		_personsService = personsService;
		_mapper = mapper;
	}
	
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType(typeof(PersonData), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<PersonData>> CreatePersonAsync([FromBody] PersonData person)
	{
		var response = await _personsService.CreatePersonAsync(person);

		if (string.IsNullOrEmpty(response.IdentityGuid))
			return BadRequest();
		
		return Ok(_mapper.Map<CreatePersonResponse>(response));
	}
}