using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.MainApp.HttpAggregator.Dto;
using Web.MainApp.HttpAggregator.Services;

namespace Web.MainApp.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FriendshipsController : ControllerBase
{
    private readonly IPrApiClient _client;

    public FriendshipsController(IPrApiClient client)
    {
        _client = client;
    }
    
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreatePersonAsync(CreateFriendshipRequest request)
    {
        var response = await _client.CreateFriendRequest(request);

        if (response.IsFailure)
            return BadRequest();
		
        return Created(string.Empty, string.Empty);
    }
}