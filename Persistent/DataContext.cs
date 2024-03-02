using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistent;

public class DataContext : IdentityDbContext<AppUser>
{
       public DataContext(DbContextOptions options) : base(options)
       {

       }

       public DbSet<Activity> Activities { get; set; }
       public DbSet<ActivityAttendee> ActivityAttendees { get; set; }
       public DbSet<Photo> Photos { get; set; }
       public DbSet<Comment> Comments { get; set; }
       public DbSet<UserFollowing> UserFollowings { get; set; } // followed by the current logged in user

       protected override void OnModelCreating(ModelBuilder builder)
       {
              base.OnModelCreating(builder);

              //This will form the primary key on ActivityAttendee table 
              builder.Entity<ActivityAttendee>(x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId }));

              // Configures the many-many r/ship between Activity and AppUser by using 
              // a third table called ActivityAttendee
              builder.Entity<ActivityAttendee>()
                     .HasOne(u => u.AppUser)
                     .WithMany(a => a.Activities)
                     .HasForeignKey(aa => aa.AppUserId);


              builder.Entity<ActivityAttendee>()
                     .HasOne(a => a.Activity)
                     .WithMany(a => a.Attendees)
                     .HasForeignKey(aa => aa.ActivityId);

              builder.Entity<Comment>()
                     .HasOne(a => a.Activity)
                     .WithMany(c => c.Comments)
                     .OnDelete(DeleteBehavior.Cascade);

              builder.Entity<UserFollowing>(b =>
              {
                     b.HasKey(k => new { k.ObserverId, k.TargetId });

                     b.HasOne(o => o.Observer)
                      .WithMany(f => f.Followings)
                      .HasForeignKey(o => o.ObserverId)
                      .OnDelete(DeleteBehavior.Cascade);

                     b.HasOne(t => t.Target)
                     .WithMany(f => f.Followers)
                     .HasForeignKey(k => k.TargetId)
                     .OnDelete(DeleteBehavior.Cascade);

              });





       }

}
