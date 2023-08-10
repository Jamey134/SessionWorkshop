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
        Console.WriteLine(Name);
        HttpContext.Session.SetInt32("Number", 0);

        return RedirectToAction("Display");
    }

    [HttpGet("Display")] //<--- Dashboard
    public IActionResult Display()
    {
        return View("Display");
    }

    //--------INCREMENT OF 1---------
    [HttpGet("/Increase1")]
    public IActionResult Increase1()
    {
        int add1 = HttpContext.Session.GetInt32("Number") ?? 0; //<--- Create a var to store method

        HttpContext.Session.SetInt32("Number", add1 + 1);

        return RedirectToAction("Display");
    }

    //--------DECREMENT OF 1----------
    [HttpGet("Decrease1")]
    public IActionResult Decrease1()
    {
        int sub1 = HttpContext.Session.GetInt32("Number") ?? 0; // For "??" it is checking HttpContext.Session.GetInt32("Number") is an int. If not, it will assign an int.

        HttpContext.Session.SetInt32("Number", sub1 - 1);

        return RedirectToAction("Display");
    }

    //-------MULTIPLY BY 2---------
    [HttpGet("Multi2")]
    public IActionResult Multi2()
    {
        int x2 = HttpContext.Session.GetInt32("Number") ?? 0;

        HttpContext.Session.SetInt32("Number", x2 * 2);

        return RedirectToAction("Display");
    }

    //------ADD RANDOM NUMBER------
    [HttpGet("AddRandom")]
    public IActionResult AddRandom()
    {
        Random randomNum = new Random();
        int rando = randomNum.Next(1, 11);
        HttpContext.Session.SetInt32("Number", rando + 1);

        return RedirectToAction("Display");
    }


    //-----------LOGOUT FROM DISPLAY PAGE-----------
    [HttpGet("Logout")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Remove("Name");
        HttpContext.Session.SetInt32("Number", 0);
        // Session.Clear() deletes everything is session below.
        // HttpContext.Session.Clear() 
        return RedirectToAction("Index");
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
