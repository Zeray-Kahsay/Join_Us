﻿using Application;
using Application.Activities;
using Domain;
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
    await Mediator.Send(new Create.Command { Activity = activity });

    return Ok();
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> EditActivity(Guid id, Activity activity)
  {
    activity.Id = id;

    await Mediator.Send(new Edit.Command { Activity = activity });
    return Ok();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await Mediator.Send(new Delete.Command { Id = id });

    return Ok();
  }




}
