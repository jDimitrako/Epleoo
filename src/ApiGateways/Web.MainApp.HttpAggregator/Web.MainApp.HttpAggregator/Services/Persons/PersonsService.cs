using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcPersons;
using Microsoft.Extensions.Logging;
using Web.MainApp.HttpAggregator.Dto.Persons;

namespace Web.MainApp.HttpAggregator.Services.Persons;

public class PersonsService : IPersonsService
{
	private readonly PersonsGrpc.PersonsGrpcClient _personsGrpcClient;
	private readonly ILogger<PersonsService> _logger;

	//private persons
	public PersonsService(PersonsGrpc.PersonsGrpcClient personsGrpcClient, ILogger<PersonsService> logger)
	{
		_personsGrpcClient = personsGrpcClient;
		_logger = logger;
	}

	public async Task<PersonData> CreatePersonAsync(PersonData personData)
	{
		_logger.LogDebug("Grpc creating person {@PersonData}", personData);
		var createPersonCommand = MapToCreatePersonCommand(personData);
		var createdPersonDto = await _personsGrpcClient.CreatePersonAsync(createPersonCommand);
		_logger.LogDebug("Grpc create person request {@Response}", createdPersonDto);
		var response = new PersonData
		{
			IdentityGuid = createdPersonDto.IdentityGuid
		};
		return response;
	}

	public async Task<List<PersonData>> GetPersonsAsync()
	{
		_logger.LogDebug("Grpc get persons");
		var persons =  await _personsGrpcClient.GetPersonsAsync(new GetPersonsCommand());
		return new List<PersonData>();
	}

	private CreatePersonCommand MapToCreatePersonCommand(PersonData personData)
	{
		var command = new CreatePersonCommand();
		command.Bio = personData.Bio;
		command.Username = personData.Username;
		command.FirstName = personData.FirstName;
		command.LastName = personData.LastName;
		command.IdentityGuid = personData.IdentityGuid;
		command.KnownAs = personData.KnownAs;

		return command;
	}
}