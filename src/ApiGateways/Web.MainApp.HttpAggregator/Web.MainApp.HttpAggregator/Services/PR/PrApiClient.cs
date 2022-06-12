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
using Web.MainApp.HttpAggregator.Dto.Persons;
using Web.MainApp.HttpAggregator.Dto.PR;

namespace Web.MainApp.HttpAggregator.Services.PR;

public class PrApiClient : IPrApiClient
{
	private readonly HttpClient _httpClient;
	private readonly IMapper _mapper;

	public PrApiClient(IHttpClientFactory httpClientFactory, IMapper mapper)
	{
		_mapper = mapper;
		_httpClient = httpClientFactory.CreateClient("Pr");
	}

	public async Task<Result> CreateFriendRequest(CreateFriendRequest createFriendRequest)
	{
		try
		{
			var jsonBody = JsonSerializer.Serialize(createFriendRequest);
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

	public async Task<IList<FriendRequestDto>> GetFriendRequests()
	{
		try
		{
			var getFriendRequestsResponse = await _httpClient.GetAsync(UrlsConfig.PrOperations.Base );

			if (!getFriendRequestsResponse.IsSuccessStatusCode)
				return new List<FriendRequestDto>();

			await using var contentStream =
				await getFriendRequestsResponse.Content.ReadAsStreamAsync();

			var friendRequestDtos = await JsonSerializer.DeserializeAsync<IList<FriendRequestDto>>(contentStream);
			return friendRequestDtos;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		
	}

	public async Task<Result> AcceptFriendRequest(int friendRequestId)
	{
		var endpoint = string.Format(UrlsConfig.PrOperations.AcceptFriendRequest, friendRequestId);
		
		var result = await _httpClient.PutAsync(endpoint, null);

		return !result.IsSuccessStatusCode ? Result.Failure("Error creating friendship request") : Result.Success();
	}
}