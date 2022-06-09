using AutoMapper;
using Web.MainApp.HttpAggregator.Responses;

namespace Web.MainApp.HttpAggregator.Dto.MappingProfiles;

public class CreatePersonResponseProfile : Profile
{
	public CreatePersonResponseProfile()
	{
		CreateMap<PersonData, CreatePersonResponse>();
	}
}