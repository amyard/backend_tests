using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FolderPath.Models;
using FolderPath.Services;

namespace FolderPath.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public async Task<IActionResult> Index(string? path, 
        [FromServices] IFolderDirectoryService _folderDirectoryService)
    {
        try
        {
            string validatedPath = ValidatePathUrl(path);
        
            FolderDirectoryVM vm = new()
            {
                PathUrl = validatedPath,
                PathText = ValidatePathText(validatedPath),
                FolderDirectories = await _folderDirectoryService.GetFolderDirectoriesAsync(validatedPath)
            };
        
            return View(model: vm);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private string ValidatePathText(string validatedPath)
    {
        if (string.IsNullOrEmpty(validatedPath))
            return "root";

        string[] splitArray = validatedPath.Split("/");
        
        return splitArray[^1];
    }

    private string ValidatePathUrl(string path)
    {
        if (path is null)
            return string.Empty;

        if (path.EndsWith("/") || path.EndsWith("\\"))
            return path[..^1];
        
        return path;
    }
}