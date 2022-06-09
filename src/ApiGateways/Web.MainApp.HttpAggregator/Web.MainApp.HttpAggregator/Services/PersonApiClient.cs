using System;
using System.Collections.Generic;
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
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public PersonApiClient(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _mapper = mapper;
        _httpClient = httpClientFactory.CreateClient("Persons");
    }

    public async Task<Result> CreatePersonAsync(CreatePersonRequest request)
    {
        try
        {
            var internalRequest = _mapper.Map<CreatePersonApiRequest>(request);
            var jsonBody = JsonSerializer.Serialize(internalRequest);
            var internalRequestBody = new StringContent(jsonBody,
                Encoding.UTF8, MediaTypeNames.Application.Json);

            var result = await _httpClient.PostAsync(UrlsConfig.PersonsOperations.Base, internalRequestBody);

            return !result.IsSuccessStatusCode ? Result.Failure("Error Creating Person") : Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IList<PersonData>> GetPersons()
    {
        var getPersonsResponseMessage = await _httpClient.GetAsync(UrlsConfig.PersonsOperations.Base);

        if (!getPersonsResponseMessage.IsSuccessStatusCode)
            return new List<PersonData>();

        await using var contentStream =
            await getPersonsResponseMessage.Content.ReadAsStreamAsync();

        var persons = await JsonSerializer.DeserializeAsync<IList<PersonData>>(contentStream);
        return persons;
    }

    public Task<PersonData> GetPersonAsync(PersonData personData)
    {
        throw new System.NotImplementedException();
    }
}