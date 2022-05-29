using AutoMapper;
using GrpcPersons;
using Web.MainApp.HttpAggregator.Models;
using Web.MainApp.HttpAggregator.Responses;

namespace Web.MainApp.HttpAggregator.MappingProfiles;

public class CreatePersonResponseProfile : Profile
{
	public CreatePersonResponseProfile()
	{
		CreateMap<PersonData, CreatePersonResponse>();
		CreateMap<PersonData, GetPersonsResponse>();
	}
}