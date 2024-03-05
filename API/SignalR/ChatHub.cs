using Application;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class ChatHub(IMediator mediator) : Hub
{
  private readonly IMediator _mediator = mediator;

  // This method will be invoked by users from client side of the app to send a message 
  public async Task SendComment(CreateComment.Command command)
  {
    // The comment is a type of Result object
    // To get the value of the comment -> comment.Value
    var comment = await _mediator.Send(command);

    await Clients.Group(command.ActivityId.ToString())
      .SendAsync("RecieveComment", comment.Value);
  }

  // When a client is connected to the Hub, place them to a Group
  // and send them comments belong to the specific activity
  public override async Task OnConnectedAsync()
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext.Request.Query["activityId"];
    await Groups.AddToGroupAsync(Context.ConnectionId, activityId);
    var result = await _mediator.Send(new CommentList.Query { ActivityId = Guid.Parse(activityId) });
    await Clients.Caller.SendAsync("LoadComments", result.Value);
  }
}
