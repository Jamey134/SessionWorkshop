using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshop.Controllers;

// To store a string in session we use ".SetString"
// The first string passed is the key and the second is the value we want to retrieve later
// HttpContext.Session.SetString("Name", "Samantha");
// // To retrieve a string from session we use ".GetString"
// string LocalVariable = HttpContext.Session.GetString("Name");


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

// -------------RENDER INDEX PAGE-------------
    public IActionResult Index()
    {
        HttpContext.Session.SetString("Name", "Mary");
        return View();
    }

// ---------POST FOR FORM------------------
    [HttpPost("Login")]
    public IActionResult Login(string Name)
    {
        HttpContext.Session.SetString("Name", Name);
        // To store an int in session we use ".SetInt32"
        HttpContext.Session.SetInt32("Number", 22);
        
        return RedirectToAction("Display");
    }
    
    [HttpGet("Display")] //<--- Dashboard
    public IActionResult Display();
    {
        return View("Display");
    }

    public IActionResult Privacy()
    {
        return View();
    }



    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
