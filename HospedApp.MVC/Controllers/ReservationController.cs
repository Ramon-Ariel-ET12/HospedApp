using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class ReservationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}