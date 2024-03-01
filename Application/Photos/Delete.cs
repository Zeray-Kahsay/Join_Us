﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace Application;

public class Delete
{
  public class Command : IRequest<Result<Unit>>
  {
    public string Id { get; set; }
  }

  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
    private readonly DataContext _context;
    private readonly IUserAccessor _userAccessor;
    private readonly IPhotoAccessor _photoAccessor;
    public Handler(DataContext context, IUserAccessor userAccessor, IPhotoAccessor photoAccessor)
    {
      _photoAccessor = photoAccessor;
      _userAccessor = userAccessor;
      _context = context;

    }
    public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
    {
      var user = await _context.Users.Include(p => p.Photos)
            .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

      if (user is null) return null;

      // This is an in-memory operation and so no need to use 'await' 
      var photo = user.Photos.FirstOrDefault(p => p.Id == request.Id);

      if (photo is null) return null;

      if (photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");

      var result = await _photoAccessor.DeletePhoto(photo.Id);

      if (result is null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");

      user.Photos.Remove(photo);

      var success = await _context.SaveChangesAsync() > 0;

      if (success) return Result<Unit>.Success(Unit.Value);

      return Result<Unit>.Failure("Problem deleting photo from API");

    }
  }

}
