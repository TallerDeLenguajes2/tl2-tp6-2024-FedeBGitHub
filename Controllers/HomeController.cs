using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using tl2_tp6_2024_FedeBGitHub.Models;

namespace tl2_tp6_2024_FedeBGitHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
        return View();
    }

    public IActionResult Privacy()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("IsAuthenticated"))) return RedirectToAction ("Index", "Logeo");
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }



}
