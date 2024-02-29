using System.Security.Claims;
using Application;
using Microsoft.AspNetCore.Http;

namespace Infrastructure;

public class UserAccessor : IUserAccessor
{
  // Since we are not in the context of  api project and therefore we don't have access 
  // to HttpContext. So, we need to have IHttpContextAccessor interface
  // HttpContext contains a User object and is possible to get user's claim
  // from the User object/ claims 
  private readonly IHttpContextAccessor _httpContextAccessor;
  public UserAccessor(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;

  }
  public string GetUsername()
  {
    return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
  }
}
