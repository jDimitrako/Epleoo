using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persons.API.Application.Commands.Persons;
using Persons.API.Application.Queries;

namespace Persons.API.Controllers;

/// <summary>
/// Persons Controller for internal API  
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private  readonly IMediator _mediator;
	private  readonly ILogger<PersonsController> _logger;
	private readonly IPersonsQueries _queries;

	public PersonsController(IMediator mediator, ILogger<PersonsController> logger, IPersonsQueries queries)
	{
		_mediator = mediator;
		_logger = logger;
		_queries = queries;
	}

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreatePersonAsync(
		[FromBody] CreatePersonCommand createPersonCommand)
	{
		_logger.LogInformation(
			"----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
			createPersonCommand.GetGenericTypeName(),
			nameof(createPersonCommand.IdentityGuid),
			createPersonCommand,
			createPersonCommand);

		var result = await _mediator.Send(createPersonCommand);
		if (result.IsSuccess)
			return Ok(result.Value);

		return BadRequest();
	}
	
	
	[HttpGet]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType(typeof(IEnumerable<PersonViewModel.Person>), (int)HttpStatusCode.OK)]
	public async Task<IActionResult> GetPersons()
	{
		var persons = await _queries.GetPersons();

		return Ok(persons);
	}
}