using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InventoryApp.Models;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace InventoryApp.Controllers;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    public IActionResult Index()
    {
        _logger.LogInformation("Index page loaded");
        return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> Secured()
    {
        AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
        if (user == null) return RedirectToAction("Login", "Account");
        string message = "Hello " + user.UserName;
        ViewBag.Message = message;
        return View();
    }

    // public async Task<IActionResult> Secured()
    // {
    //     AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    //     if (user == null) return RedirectToAction("Login", "Account");
    //     string message = "Hello " + user.UserName;
    //     ViewBag.Message = message;
    //     return View();
    // }
}
