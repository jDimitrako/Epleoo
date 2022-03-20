using System.Threading.Tasks;
using EventBus.Extensions;
using Grpc.Core;
using GrpcPersons;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Persons.API.Grpc;

public class PersonService : PersonsGrpc.PersonsGrpcBase
{
	private readonly IMediator _mediator;
	private readonly ILogger<PersonService> _logger;

	public PersonService(
		IMediator mediator,
		ILogger<PersonService> logger)
	{
		_mediator = mediator;
		_logger = logger;
	}

	public override async Task<CreatedPersonDto> CreatePerson(CreatePersonCommand createPersonCommand, ServerCallContext context)
	{
		_logger.LogInformation("Begin grpc call form method {Method} for create person {CreatePersonCommand}", context.Method, createPersonCommand);
		_logger.LogTrace("--Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
			createPersonCommand.GetGenericTypeName(),
			nameof(createPersonCommand.Username),
			createPersonCommand.Username,
			createPersonCommand);
		
		
		var command = new Application.Commands.Persons.CreatePersonCommand(createPersonCommand.IdentityGuid,
			createPersonCommand.Username, createPersonCommand.FirstName, createPersonCommand.LastName,
			createPersonCommand.KnownAs, createPersonCommand.Bio);

		var result = await _mediator.Send(command);
		
		if (!result)
			context.Status = new Status(StatusCode.Aborted, $"Bad request for command{command}");

		context.Status = new Status(StatusCode.OK, $"Successful command {command}");

		return new CreatedPersonDto();
	}
}