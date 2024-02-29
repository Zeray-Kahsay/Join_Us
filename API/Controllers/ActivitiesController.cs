using Application;
using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class ActivitiesController : BaseApiController
{

  [HttpGet]
  public async Task<IActionResult> GetActivities()
  {
    // Sends a Query to the Handler(mediator which enables Application layer and API
    // communicate)
    return HandleResult(await Mediator.Send(new List.Query()));
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Activity>> GetActivity(Guid id)
  {
    return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
  }

  [HttpPost]
  public async Task<IActionResult> CreateActivity(Activity activity)
  {
    return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));

  }

  [Authorize(Policy = "IsActivityHost")]
  [HttpPut("{id}")]
  public async Task<IActionResult> EditActivity(Guid id, Activity activity)
  {
    activity.Id = id;

    return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
  }

  [HttpPost("{id}/attend")]
  public async Task<ActionResult> Attend(Guid id)
  {
    return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
  }


}
