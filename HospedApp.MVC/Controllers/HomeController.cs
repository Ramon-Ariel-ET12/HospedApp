using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospedApp.MVC.Models;
using HospedApp.Core;
using HospedApp.MVC.Security.JsonWriteToken;
using HospedApp.MVC.Security.Cookies;
using HospedApp.MVC.Decorators;

namespace HospedApp.MVC.Controllers;

[AuthToken]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdo Ado;
    private readonly Jwt Jwt;

    public HomeController(ILogger<HomeController> logger, IAdo ado, Jwt jwt)
    {
        _logger = logger;
        Ado = ado;
        Jwt = jwt;
    }
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
