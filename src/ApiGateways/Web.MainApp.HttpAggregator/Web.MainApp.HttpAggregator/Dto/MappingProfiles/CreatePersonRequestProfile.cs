using AutoMapper;

namespace Web.MainApp.HttpAggregator.Dto.MappingProfiles;

public class CreatePersonRequestProfile : Profile
{
	public CreatePersonRequestProfile()
	{
		CreateMap<CreatePersonRequest, CreatePersonApiRequest>();
	}
}