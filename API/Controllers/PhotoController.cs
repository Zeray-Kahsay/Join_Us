using Application;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class PhotoController : BaseApiController
{
  [HttpPost]
  public async Task<ActionResult> Add([FromForm] Add.Command command)
  {
    return HandleResult(await Mediator.Send(command));
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> Delete(string id)
  {
    return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
  }

  [HttpPost("{id}/setMain")]
  public async Task<IActionResult> SetMain(string id)
  {
    return HandleResult(await Mediator.Send(new SetMain.Command { Id = id }));
  }

}
