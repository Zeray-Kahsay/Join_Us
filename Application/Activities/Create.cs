using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistent;

namespace Application;

public class Create
{
  public class Command : IRequest
  {
    public Activity Activity { get; set; } // paramter to the Handler from API
  }

  public class Handler : IRequestHandler<Command>
  {
    private readonly DataContext _context;
    public Handler(DataContext context)
    {
      _context = context;

    }

    public async Task Handle(Command request, CancellationToken cancellationToken)
    {
      // At this point of time, we work only in memory, not accessing the Db
      // and therefore don't await and Async methods 
      _context.Activities.Add(request.Activity);

      // Now, we accessing the DB
      await _context.SaveChangesAsync();
    }
  }
}
