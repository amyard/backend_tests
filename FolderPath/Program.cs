using FolderPath.Data;
using FolderPath.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // ADD sqlite connection
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
}

var app = builder.Build();
{
    // RUN MIGRATIONS
    await using var scope = app.Services.CreateAsyncScope();
    await using var context = scope.ServiceProvider.GetService<DataContext>();
    await context.Database.MigrateAsync();

    await InsertStandardFolderStructureAsync(context);
    
    
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "fullpath",
        pattern: "{*path}",
        defaults: new {controller="Home", action="Index"});

    app.Run();
}

async Task InsertStandardFolderStructureAsync(DataContext context)
{
    if (!context.FolderDirectories.Any())
    {
        List<FolderDirectory> data = new List<FolderDirectory>()
        {
            new() {Name = "Creating Digital Images", Level = 1},
            new() {Name = "Resources", Level = 2},
            new() {Name = "Evidence", Level = 2},
            new() {Name = "Graphic Products", Level = 2},
            new() {Name = "Primary Sources", Level = 3},
            new() {Name = "Secondary Sources", Level = 3},
            new() {Name = "Process", Level = 3},
            new() {Name = "Final Product", Level = 3}
        };

        await context.FolderDirectories.AddRangeAsync(data);
        await context.SaveChangesAsync();
    }
}