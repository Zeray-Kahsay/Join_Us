using Domain;

namespace Application;

public class UserProfile
{
  public string Username { get; set; }
  public string DisplayName { get; set; }
  public string Bio { get; set; }
  public string Image { get; set; }
  public bool Following { get; set; } // if the current logged-in user following a specific user
  public int FollowersCount { get; set; }
  public int FollowingCount { get; set; }
  public ICollection<Photo> Photos { get; set; }

}
