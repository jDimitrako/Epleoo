using System.Collections.Generic;
using GrpcPersons;
using Persons.API.Application.Queries;

namespace Persons.API.Profilers;

public static class ManualMappings
{
	public static CreatedPersonDto MapCreatedPersonDto(PersonViewModel.Person person)
	{
		var createdPersonDto = new CreatedPersonDto();
		createdPersonDto.Bio = person.Bio;
		createdPersonDto.Username = person.Username;
		createdPersonDto.FirstName = person.FirstName;
		createdPersonDto.IdentityGuid = person.IdentityGuid;
		createdPersonDto.KnownAs = person.KnownAs;
		createdPersonDto.LastName = person.LastName;
		return createdPersonDto;
	}
	
	public static List<CreatedPersonDto> MapList(this IList<PersonViewModel.Person> persons)
	{
		var createdPersonsDtos = new List<CreatedPersonDto>();

		foreach (var person in persons)
		{
			var createdPersonDto = MapCreatedPersonDto(person);
			createdPersonsDtos.Add(createdPersonDto);
		}

		return createdPersonsDtos;
	}
}