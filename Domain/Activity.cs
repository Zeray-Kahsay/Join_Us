namespace Domain;

public class Activity
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public DateTime Date { get; set; }
  public string Description { get; set; }
  public string Category { get; set; }
  public string City { get; set; }
  public string Venue { get; set; }
  public bool IsCancelled { get; set; }
  // Initializing the collection enables us to add attendess/users later on
  public ICollection<ActivityAttendee> Attendees { get; set; } = [];
  public ICollection<Comment> Comments { get; set; } = [];
}
