using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace Application;

public class CommentList
{
  public class Query : IRequest<Result<List<CommentDto>>>
  {
    public Guid ActivityId { get; set; }
  }

  public class Handler : IRequestHandler<Query, Result<List<CommentDto>>>
  {
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public Handler(DataContext context, IMapper mapper)
    {
      _mapper = mapper;
      _context = context;

    }
    public async Task<Result<List<CommentDto>>> Handle(Query request, CancellationToken cancellationToken)
    {
      var comments = await _context.Comments
                  .Where(a => a.Activity.Id == request.ActivityId)
                  .OrderBy(c => c.CreatedAt)
                  .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                  .ToListAsync();

      return Result<List<CommentDto>>.Success(comments);
    }
  }
}
