using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistent;

public class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Activities.Any()) return;

        var activities = new List<Activity>
            {
                new() {
                    Title = "Past Activity 1",
                    Date = DateTime.UtcNow.AddMonths(-2),
                    Description = "Activity 2 months ago",
                    Category = "drinks",
                    City = "Kristiansand",
                    Venue = "Pub",
                },
                new() {
                    Title = "Past Activity 2",
                    Date = DateTime.UtcNow.AddMonths(-1),
                    Description = "Activity 1 month ago",
                    Category = "culture",
                    City = "Oslo",
                    Venue = "Louvre",
                },
                new() {
                    Title = "Future Activity 1",
                    Date = DateTime.UtcNow.AddMonths(1),
                    Description = "Activity 1 month in future",
                    Category = "culture",
                    City = "Bergen",
                    Venue = "Natural History Museum",
                },
                new() {
                    Title = "Future Activity 2",
                    Date = DateTime.UtcNow.AddMonths(2),
                    Description = "Activity 2 months in future",
                    Category = "music",
                    City = "Oslo",
                    Venue = "O2 Arena",
                },
                new() {
                    Title = "Future Activity 3",
                    Date = DateTime.UtcNow.AddMonths(3),
                    Description = "Activity 3 months in future",
                    Category = "drinks",
                    City = "Oslo",
                    Venue = "Another pub",
                },
                new() {
                    Title = "Future Activity 4",
                    Date = DateTime.UtcNow.AddMonths(4),
                    Description = "Activity 4 months in future",
                    Category = "drinks",
                    City = "Oslo",
                    Venue = "Yet another pub",
                },
                new() {
                    Title = "Future Activity 5",
                    Date = DateTime.UtcNow.AddMonths(5),
                    Description = "Activity 5 months in future",
                    Category = "drinks",
                    City = "Oslo",
                    Venue = "Just another pub",
                },
                new() {
                    Title = "Future Activity 6",
                    Date = DateTime.UtcNow.AddMonths(6),
                    Description = "Activity 6 months in future",
                    Category = "music",
                    City = "Oslo",
                    Venue = "Roundhouse Camden",
                },
                new() {
                    Title = "Future Activity 7",
                    Date = DateTime.UtcNow.AddMonths(7),
                    Description = "Activity 2 months ago",
                    Category = "travel",
                    City = "Oslo",
                    Venue = "Somewhere on the Thames",
                },
                new() {
                    Title = "Future Activity 8",
                    Date = DateTime.UtcNow.AddMonths(8),
                    Description = "Activity 8 months in future",
                    Category = "film",
                    City = "Oslo",
                    Venue = "Cinema",
                }
            };

        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}
