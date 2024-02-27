using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistent;

namespace Application;
public class Edit
{
  public class Command : IRequest<Result<Unit>>
  {
    public Activity Activity { get; set; }
  }

  public class CommandValidator : AbstractValidator<Command>
  {
    public CommandValidator()
    {
      RuleFor(a => a.Activity).SetValidator(new ActivityValidator());
    }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var activityDB = await _context.Activities.FindAsync(request.Activity.Id);

      //map it to request.Activity to activityDB to edit and save into DB 
      _mapper.Map(request.Activity, activityDB);

      // Check if the edition is successful
      var result = await _context.SaveChangesAsync() > 0;

      if (!result) return Result<Unit>.Failure("Unable to edit the activity");

      return Result<Unit>.Success(Unit.Value);

    }
  }




}
