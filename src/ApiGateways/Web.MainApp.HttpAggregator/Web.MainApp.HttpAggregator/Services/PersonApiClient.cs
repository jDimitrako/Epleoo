using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Web.MainApp.HttpAggregator.Config;
using Web.MainApp.HttpAggregator.Dto;

namespace Web.MainApp.HttpAggregator.Services;

public class PersonApiClient : IPersonApiClient
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly IMapper _mapper;

	public PersonApiClient(IHttpClientFactory httpClientFactory, IMapper mapper)
	{
		_httpClientFactory = httpClientFactory;
		_mapper = mapper;
	}

	public async Task<Result> CreatePersonAsync(CreatePersonRequest request)
	{
		try
		{
			var client = _httpClientFactory.CreateClient("Persons");
			var internalRequest = _mapper.Map<CreatePersonApiRequest>(request);
			internalRequest.IdentityGuid = string.Empty;
			var jsonBody = JsonSerializer.Serialize(internalRequest);
			var internalRequestBody = new StringContent(jsonBody,
				Encoding.UTF8, MediaTypeNames.Application.Json);

			var result = await client.PostAsync(UrlsConfig.PersonsOperations.CreatePerson(), internalRequestBody);

			if (!result.IsSuccessStatusCode)
				return Result.Failure("Error Creating Person");

			return Result.Success();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public Task<PersonData> GetPersonAsync(PersonData personData)
	{
		throw new System.NotImplementedException();
	}
}