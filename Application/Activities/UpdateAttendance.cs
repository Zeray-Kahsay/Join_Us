using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace Application;

public class UpdateAttendance
{
  public class Command : IRequest<Result<Unit>>
  {
    public Guid Id { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;
    public Handler(DataContext context, IUserAccessor userAccessor)
    {
      _userAccessor = userAccessor;
      _context = context;

    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      // Get the activity
      var activity = await _context.Activities
              .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
              .SingleOrDefaultAsync(x => x.Id == request.Id);

      if (activity is null) return null;

      // Get the current user 
      var user = await _context.Users.FirstOrDefaultAsync(u =>
              u.UserName == _userAccessor.GetUsername());

      if (user is null) return null;

      // Get a host user name of the activity 
      var hostUsername = activity.Attendees.FirstOrDefault(a => a.IsHost)?.AppUser?.UserName;

      // Check if the current user is going to attend in the activity
      var attendance = activity.Attendees.FirstOrDefault(a => a.AppUser.UserName == user.UserName);

      // Give the current user the power to cancell or not (toggling activity cancellation on UI) 
      if (attendance is not null && hostUsername == user.UserName)
        activity.IsCancelled = !activity.IsCancelled;

      //  add the current user to an activity 
      if (attendance is null)
      {
        attendance = new ActivityAttendee
        {
          AppUser = user,
          Activity = activity,
          IsHost = false
        };

        activity.Attendees.Add(attendance);
      }
      var result = await _context.SaveChangesAsync() > 0;

      return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");
    }
  }
}
