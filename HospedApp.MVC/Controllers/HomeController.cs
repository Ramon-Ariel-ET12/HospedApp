using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospedApp.MVC.Models;
using HospedApp.Core;
using HospedApp.MVC.Security.JsonWriteToken;
using Microsoft.AspNetCore.Authorization;

namespace HospedApp.MVC.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdo Ado;
    private readonly Jwt Jwt = new();

    public HomeController(ILogger<HomeController> logger, IAdo ado)
    {
        _logger = logger;
        Ado = ado;
    }
    public IActionResult Index() => View();
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
