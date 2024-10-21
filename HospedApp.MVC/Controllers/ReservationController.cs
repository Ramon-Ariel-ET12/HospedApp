using HospedApp.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class ReservationController : Controller
{
    private readonly IAdo Ado;
    public ReservationController(IAdo ado)
    {
        Ado = ado;
    }
    public async Task<IActionResult> Index()
    {
        var reservation = await Ado.GetReservations();
        return View(reservation);
    }

}
