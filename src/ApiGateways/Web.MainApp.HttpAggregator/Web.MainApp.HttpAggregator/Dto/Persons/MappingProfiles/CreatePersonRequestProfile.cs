using AutoMapper;

namespace Web.MainApp.HttpAggregator.Dto.Persons.MappingProfiles;

public class CreatePersonRequestProfile : Profile
{
	public CreatePersonRequestProfile()
	{
		CreateMap<CreatePersonRequest, CreatePersonApiRequest>();
	}
}