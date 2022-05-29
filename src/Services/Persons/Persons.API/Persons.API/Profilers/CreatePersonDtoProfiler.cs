using AutoMapper;
using GrpcPersons;
using Persons.API.Application.Queries;

namespace Persons.API.Profilers;

public class CreatePersonDtoProfiler : Profile
{
	public CreatePersonDtoProfiler()
	{
		CreateMap<PersonViewModel, CreatedPersonDto>();
	}
}