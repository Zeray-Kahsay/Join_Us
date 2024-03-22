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
       public DbSet<ActivityAttendee> ActivityAttendees { get; set; } // AppUser-Activity (Many-to-Many)
       public DbSet<Photo> Photos { get; set; } // one-to-many / a photo belongs to one user, a user may have many photos
       public DbSet<Comment> Comments { get; set; } // one-to-many / an activity may have many comments, a comment belongs to one activity
       public DbSet<UserFollowing> UserFollowings { get; set; } // followed by the current logged-in user / a user may have many followers, a user may have been followed by many users

       protected override void OnModelCreating(ModelBuilder builder)
       {
              base.OnModelCreating(builder);

              //This will create primary keys on ActivityAttendee table 
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

              // The dependent entity here is Comment.So, if an Activity has been deleted, it cascades to the comment/s also
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
