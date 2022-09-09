using System.Diagnostics;
using AuthMVC.Data;
using Microsoft.AspNetCore.Mvc;
using AuthMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index([FromServices] DataContext dataContext)
    {
        var profile = dataContext.Profiles
            .Where(x => x.Id == 1)
            .Include(x => x.Friends)
                .ThenInclude(x => x.FriendProfile)
            .FirstOrDefault();

        var friends = profile.Friends
            .Select(x => x.FriendProfile)
            .Where(x => x.Id != profile.Id)
            .ToList();
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}