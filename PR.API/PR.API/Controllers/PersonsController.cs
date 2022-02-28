using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PR.API.Controllers;

[Route("api/v1/[controller]")]
//[Authorize]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<PersonsController> _logger;
	public PersonsController(IMediator mediator, ILogger<PersonsController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	[Route("FriendRequests")]
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateFriendRequest()
	{
		//var commandResult = await _mediator.Send()
		return Ok();
	}

}