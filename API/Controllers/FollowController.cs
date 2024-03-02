using Application.Followers;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class FollowController : BaseApiController
{
  [HttpPost("{username}")]
  public async Task<IActionResult> Follow(string username)
  {
    return HandleResult(await Mediator.Send(new Application.FollowingToggle.Command { TargetUsername = username }));
  }

  [HttpGet("{username}")]
  public async Task<IActionResult> GetFollowing(string username, string predicate)
  {
    return HandleResult(await Mediator.Send(new List.Query { Username = username, Predicate = predicate }));
  }
}
