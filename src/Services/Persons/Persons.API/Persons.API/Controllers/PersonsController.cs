using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persons.API.Application.Commands.Persons;

namespace Persons.API.Controllers;

[Route("api/v1/[controller]")]
//[Authorize]
[ApiController]
public class PersonsController : ControllerBase
{
	private  readonly IMediator _mediator;
	private  readonly ILogger<PersonsController> _logger;
	public PersonsController(IMediator mediator, ILogger<PersonsController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateFriendRequestAsync(
		[FromBody] CreatePersonRequestCommand createPersonRequestCommand)
	{
		_logger.LogInformation(
			"----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
			createPersonRequestCommand.GetGenericTypeName(),
			nameof(createPersonRequestCommand.IdentityGuid),
			createPersonRequestCommand,
			createPersonRequestCommand);

		var result = await _mediator.Send(createPersonRequestCommand);
		if (result)
			return Ok();

		return BadRequest();
	}

}