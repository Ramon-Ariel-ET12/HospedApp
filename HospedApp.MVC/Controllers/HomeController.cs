using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospedApp.MVC.Models;
using HospedApp.Core;

namespace HospedApp.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAdo Ado;

    public HomeController(ILogger<HomeController> logger, IAdo ado)
    {
        Ado = ado;
        _logger = logger;
    }
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await Ado.Login(email, password);
            if (user == null)
            {
                return BadRequest();
            }
            return Redirect("/Client");
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
