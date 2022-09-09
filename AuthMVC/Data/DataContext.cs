using AuthMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthMVC.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<FriendShip> FriendShips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Profile>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<FriendShip>()
            .HasKey(k => new { k.PrimaryProfileId, k.FriendProfileId });

        modelBuilder.Entity<FriendShip>()
            .HasOne(x => x.PrimaryProfile)
            .WithMany(f => f.Friends)
            .HasForeignKey(x => x.PrimaryProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FriendShip>()
            .HasOne(x => x.FriendProfile)
            .WithMany(f => f.FriendsOf)
            .HasForeignKey(x => x.FriendProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}