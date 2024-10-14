using System.Data;
using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.Dapper;
using HospedApp.MVC.Decorators;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class HotelController : Controller
{
    private readonly IAdo Ado;
    public HotelController(IAdo ado) => Ado = ado;

    [AuthToken]
    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var hotel = await Ado.GetHotels();
        return View(hotel);
    }

    [AuthToken]
    [HttpGet]
    public IActionResult Register(Hotel hotel) => View("Upsert", hotel);

    [AuthToken]
    [HttpPost]
    public IActionResult Upsert(Hotel hotel)
    {
        if (hotel == null)
            return RedirectToAction("Hotel");
        
        if (hotel.IdHotel == 0)
        {
            Ado.CreateHotel(hotel);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}
