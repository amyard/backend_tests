namespace AuthMVC.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public virtual ICollection<UserProfile> Friends { get; set; }
    public virtual ICollection<UserProfile> FriendOf { get; set; }
}