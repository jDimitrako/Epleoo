using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PR.API.Application.Commands.FriendRequest;

namespace PR.API.Controllers;

public class FriendRequestsController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<FriendRequestsController> _logger;

	public FriendRequestsController(IMediator mediator, ILogger<FriendRequestsController> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	[Route("FriendRequests")]
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> CreateFriendRequestAsync(
		[FromBody] CreateFriendRequestCommand createFriendRequestCommand)
	{
		_logger.LogInformation(
			"----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
			createFriendRequestCommand.GetGenericTypeName(),
			nameof(createFriendRequestCommand.ReceiverIndentityGuid),
			createFriendRequestCommand.SenderIndentityGuid,
			createFriendRequestCommand);

		var result =  await _mediator.Send(createFriendRequestCommand);
		if (result)
			return Ok();

		return BadRequest();

	}
}