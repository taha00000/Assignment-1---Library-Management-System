using System.Diagnostics;
using LibraryManagementSystem.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
