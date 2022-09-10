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
    
    // public DbSet<UserProfile> UserProfiles { get; set; }

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
        
        // user tests
        // modelBuilder.Entity<User>()
        //     .Property(x => x.Id)
        //     .ValueGeneratedOnAdd();
        
        // modelBuilder.Entity<UserProfile>()
        //     .HasMany(u => u.Friends)
        //     .WithMany(u => u.FriendOf)
        //     .Map(m => m.ToTable("UserFriends")
        //         .MapLeftKey("UserId")
        //         .MapRightKey("FriendId"));
        
        // https://stackoverflow.com/questions/51810776/entity-framework-many-to-many-on-same-table
        // https://www.entityframeworktutorial.net/code-first/configure-entity-mappings-using-fluent-api.aspx
    }
}