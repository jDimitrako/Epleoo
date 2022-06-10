using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EventBus.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PR.API.Application.Commands.FriendRequest;
using PR.API.Application.Queries;
using PR.API.Application.Queries.FriendRequests;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.API.Controllers;

public class FriendRequestsController : ControllerBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<FriendRequestsController> _logger;
	private readonly IFriendRequestsQueries _queries;

	public FriendRequestsController(IMediator mediator, ILogger<FriendRequestsController> logger,
		IFriendRequestsQueries queries)
	{
		_mediator = mediator;
		_logger = logger;
		_queries = queries;
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
			nameof(createFriendRequestCommand.ReceiverPersonIdentityGuid),
			createFriendRequestCommand.SenderPersonIdentityGuid,
			createFriendRequestCommand);

		var result = await _mediator.Send(createFriendRequestCommand);
		if (result)
			return Ok();

		return BadRequest();
	}

	[Route("FriendRequests/{friendRequestId}/accept")]
	[HttpPut]
	[ProducesResponseType((int)HttpStatusCode.OK)]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	public async Task<IActionResult> AcceptFriendRequestAsync(int friendRequestId)
	{
		var acceptFriendRequestCommand = new AcceptFriendRequestCommand(friendRequestId);
		
		_logger.LogInformation(
			"----- Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
			acceptFriendRequestCommand.GetGenericTypeName(),
			nameof(acceptFriendRequestCommand.FriendRequestId),
			acceptFriendRequestCommand);

		var result = await _mediator.Send(acceptFriendRequestCommand);
		if (result)
			return Ok();

		return BadRequest();
	}

	[HttpGet("FriendRequests")]
	[ProducesResponseType(typeof(IEnumerable<FriendRequestResponse.FriendRequestSummary>), (int)HttpStatusCode.OK)]
	public async Task<ActionResult<IEnumerable<FriendRequest>>> GetFriendRequests(string senderPersonId, string receiverPersonId)
	{
		var friendRequests = await _queries.GetFriendRequests(senderPersonId, receiverPersonId);

		return Ok(friendRequests);
	}
	
}