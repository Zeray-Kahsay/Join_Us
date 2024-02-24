using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistent;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions options) : base(options)
  {
    
  }

  public DbSet<Activity> Activities { get; set; }

}
