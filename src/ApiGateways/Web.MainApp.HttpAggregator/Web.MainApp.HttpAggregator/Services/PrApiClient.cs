using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc.Formatters;
using Web.MainApp.HttpAggregator.Config;
using Web.MainApp.HttpAggregator.Dto;

namespace Web.MainApp.HttpAggregator.Services;

public class PrApiClient : IPrApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public PrApiClient(IHttpClientFactory httpClientFactory, IMapper mapper)
    {
        _mapper = mapper;
        _httpClient = httpClientFactory.CreateClient("Pr");
    }

    public async Task<Result> CreateFriendRequest(CreateFriendshipRequest createFriendshipRequest)
    {
        try
        {
            var internalRequest = new CreateFriendshipRequest(createFriendshipRequest.SenderPersonId,
                createFriendshipRequest.ReceiverPersonId);
            var jsonBody = JsonSerializer.Serialize(internalRequest);
            var internalRequestBody = new StringContent(jsonBody, Encoding.UTF8, MediaTypeNames.Application.Json);

            var result = await _httpClient.PostAsync(UrlsConfig.PrOperations.Base, internalRequestBody);

            return !result.IsSuccessStatusCode ? Result.Failure("Error creating friendship request") : Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}