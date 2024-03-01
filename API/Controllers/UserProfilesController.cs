using Application.Photos;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class UserProfilesController : BaseApiController
{
  [HttpGet("{username}")]
  public async Task<IActionResult> GetProfile(string username)
  {
    return HandleResult(await Mediator.Send(new Details.Query { Username = username }));
  }
}
