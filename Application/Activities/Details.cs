using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistent;

namespace Application;

public class Details
{
  public class Query : IRequest<Result<Activity>>
  {
    public Guid Id { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<Activity>>
  {
    private readonly DataContext _context;
    public Handler(DataContext context)
    {
      _context = context;

    }
    public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
    {
      return Result<Activity>.Success(await _context.Activities.FindAsync(request.Id));
    }
  }
}



// public class Edit
// {

//   public class Command : IRequest
//   {
//     public Activity Activity { get; set; }
//   }

//   public class CommandValidator : AbstractValidator<Command>
//   {
//     public CommandValidator()
//     {
//       RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
//     }
//   }

//   public class Handler : IRequestHandler<Command>
//   {
//     private readonly DataContext _context;
//     private readonly IMapper _mapper;
//     public Handler(DataContext context, IMapper mapper)
//     {
//       _context = context;
//       _mapper = mapper;

//     }
//     public async Task Handle(Command request, CancellationToken cancellationToken)
//     {
//       var activity = await _context.Activities.FindAsync(request.Activity.Id);
//       _mapper.Map(request.Activity, activity);


//       await _context.SaveChangesAsync();

//     }
//   }

// }
