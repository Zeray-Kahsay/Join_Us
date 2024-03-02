using Application;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class FollowController : BaseApiController
{
  [HttpPost("{username}")]
  public async Task<IActionResult> Follow(string username)
  {
    return HandleResult(await Mediator.Send(new FollowingToggle.Command { TargetUsername = username }));
  }
}
