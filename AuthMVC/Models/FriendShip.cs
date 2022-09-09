namespace AuthMVC.Models;

public class FriendShip
{
    public Profile PrimaryProfile { get; set; }
    public int PrimaryProfileId { get; set; }
    
    public Profile FriendProfile { get; set; }
    public int FriendProfileId { get; set; }
}