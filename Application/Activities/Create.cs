using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace Application;

public class Create
{
  public class Command : IRequest<Result<Unit>>
  {
    public Activity Activity { get; set; } // paramter to the Handler from API
  }

  // Fluent validation
  public class CommandValidator : AbstractValidator<Command>
  {
    public CommandValidator()
    {
      RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
    }
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
      var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.UserName == _userAccessor.GetUsername());

      var attendee = new ActivityAttendee
      {
        AppUser = user,
        Activity = request.Activity,
        IsHost = true
      };

      // Add the user to the activity 
      request.Activity.Attendees.Add(attendee);

      // At this point of time, we work only in memory, not accessing the Db
      // and therefore don't use await and Async methods 
      _context.Activities.Add(request.Activity);

      // Now, we are accessing the DB
      var result = await _context.SaveChangesAsync() > 0;
      if (!result) return Result<Unit>.Failure("Failed to create activity");

      return Result<Unit>.Success(Unit.Value);
    }
  }
}
