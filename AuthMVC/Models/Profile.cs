﻿namespace AuthMVC.Models;

public class Profile
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual ICollection<FriendShip> Friends { get; set; }
    public virtual ICollection<FriendShip> FriendsOf { get; set; }
}