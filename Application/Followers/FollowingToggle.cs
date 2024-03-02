using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace Application;

public class FollowingToggle
{
  public class Command : IRequest<Result<Unit>>
  {
    public string TargetUsername { get; set; }
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
      // Get the TWO USERS who involve in this request 
      var observer = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

      var target = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.TargetUsername);

      // Check the target as it comes along with the request
      if (target is null) return null;

      // Get the pair from DB if any
      var following = await _context.UserFollowings.FindAsync(observer.Id, target.Id);

      //  toggling operation: if/else
      if (following is null)
      {
        following = new Domain.UserFollowing
        {
          Observer = observer,
          Target = target
        };
      }
      else
      {
        _context.UserFollowings.Remove(following);
      }
      var success = await _context.SaveChangesAsync() > 0;

      if (success) return Result<Unit>.Success(Unit.Value);

      return Result<Unit>.Failure("Failed to update following");
    }
  }
}
