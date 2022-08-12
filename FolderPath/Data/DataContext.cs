using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FolderPath.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FolderPath.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<FolderDirectory> FolderDirectories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FolderDirectory>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<FolderDirectory>()
            .Property(c => c.Name)
            .IsRequired();
        
        modelBuilder.Entity<FolderDirectory>()
            .Property(c => c.Level)
            .IsRequired();
    }
}