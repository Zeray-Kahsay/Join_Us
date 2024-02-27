using System.Threading;
using System.Threading.Tasks;
using Domain;
using FluentValidation;
using MediatR;
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
    public Handler(DataContext context)
    {
      _context = context;

    }

    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      // At this point of time, we work only in memory, not accessing the Db
      // and therefore don't await and Async methods 
      _context.Activities.Add(request.Activity);

      // Now, we accessing the DB
      var result = await _context.SaveChangesAsync() > 0;
      if (!result) return Result<Unit>.Failure("Failed to create activity");

      return Result<Unit>.Success(Unit.Value);
    }
  }
}
