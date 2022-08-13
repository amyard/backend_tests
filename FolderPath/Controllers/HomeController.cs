using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FolderPath.Models;
using FolderPath.Services;

namespace FolderPath.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFolderDirectoryService _folderDirectoryService;
    private readonly IConfiguration _config;

    public HomeController(
        ILogger<HomeController> logger, 
        IFolderDirectoryService folderDirectoryService,
        IConfiguration config)
    {
        _logger = logger;
        _folderDirectoryService = folderDirectoryService;
        _config = config;
    }
    
    public async Task<IActionResult> Index(string? path)
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

    [HttpGet]
    [Route("export-async")]
    public async Task<ActionResult> ExportAsync()
    {
        string message; 
        
        try
        {
            string fileName = _config.GetValue<string>("ExportFile") ?? "export2.csv";
            await _folderDirectoryService.ExportAsync(fileName);
            
            message = $"File was generated in root - {fileName}";
        }
        catch (Exception ex)
        {
            message = $"Some error occured.{Environment.NewLine}{ex.Message}";
        }

        return Json(new {message = message});
    }
    
    [HttpPost]
    [Route("upload-async")]
    public async Task<ActionResult> UploadAsync(IFormFile uploadedFile)
    {
        string message = String.Empty;
        try
        {
            IFormFile file = Request.Form.Files[0];
            
            if(file.Length == 0)
                return Json(new {message = "There were any files in response"});

            await _folderDirectoryService.UploadAsync(file);
            message = "file was uploaded.";

        }
        catch(Exception ex)
        {
            return Json(new {message = $"Some error occured.{Environment.NewLine}{ex.Message}"});
        }
        
        return Json(new {message = message});
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