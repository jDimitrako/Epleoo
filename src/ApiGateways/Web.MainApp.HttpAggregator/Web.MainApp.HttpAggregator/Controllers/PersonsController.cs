﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Dto;
using Web.MainApp.HttpAggregator.Responses;
using Web.MainApp.HttpAggregator.Services;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PersonsController : ControllerBase
{
	private readonly IPersonApiClient _client;

	public PersonsController(IPersonApiClient client)
	{
		_client = client;
	}
	
	[HttpPost]
	[ProducesResponseType((int)HttpStatusCode.BadRequest)]
	[ProducesResponseType((int)HttpStatusCode.Created)]
	public async Task<IActionResult> CreatePersonAsync(CreatePersonRequest person)
	{
		var response = await _client.CreatePersonAsync(person);

		if (response.IsFailure)
			return BadRequest();
		
		return Created(string.Empty, string.Empty);
	}
}