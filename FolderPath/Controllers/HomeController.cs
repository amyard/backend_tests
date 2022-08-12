﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FolderPath.Models;

namespace FolderPath.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index(string? path)
    {
        return View(model: path);
    }
}