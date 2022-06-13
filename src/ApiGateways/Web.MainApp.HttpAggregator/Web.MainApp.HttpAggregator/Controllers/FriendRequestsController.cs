using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Dto.Persons;
using Web.MainApp.HttpAggregator.Services.PR;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FriendRequestsController : ControllerBase
{
    private readonly IPrApiClient _client;

    public FriendRequestsController(IPrApiClient client)
    {
        _client = client;
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateFriendRequest(CreateFriendRequest request)
    {
        var response = await _client.CreateFriendRequest(request);

        if (response.IsFailure)
            return BadRequest();
		
        return Created(string.Empty, string.Empty);
    }
    
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreatePersonAsync()
    {
        var response = await _client.GetFriendRequests();

        return Ok(response);
    }
    
    [Route("{friendRequestId}/accept")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AcceptFriendRequest(int friendRequestId)
    {
        var response = await _client.AcceptFriendRequest(friendRequestId);

        if (response.IsFailure)
            return BadRequest();
        
        return Ok();
    }
}