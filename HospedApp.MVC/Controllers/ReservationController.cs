using HospedApp.Core;
using HospedApp.Core.Entities;
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
        return View(reservation.OrderByDescending(x => x.IdReservation));
    }
    public async Task<IActionResult> Cancelled()
    {
        var reservation = await Ado.GetReservationsCancelled();
        return View(reservation.OrderByDescending(x => x.StartDate));
    }

    public async Task<IActionResult> Register(Reservation reservation)
    {
        ViewBag.Domicile = await Ado.GetAddresses();
        ViewBag.Hotel = await Ado.GetHotels();
        ViewBag.Client = await Ado.GetClients();
        ViewBag.HotelRoom = await Ado.GetHotelRooms();
        var ordenado = await Ado.GetRoomBeds();
        ViewBag.RoomBed = ordenado.OrderByDescending(x => x.IdRoomBed);
        return View("Upsert", reservation);
    }


    public async Task<IActionResult> Cancell(int id)
    {
        await Ado.CancelReservation(id);
        return RedirectToAction("Cancelled");
    }
    [HttpPost]
    public async Task<IActionResult> Upsert(Reservation reservation)
    {
        if (reservation.IdReservation == 0)
        {
            await Ado.CreateReservation(reservation);
            return RedirectToAction("Index");
        }
        else
        {
            await Ado.ModifyReservation(reservation);
            return RedirectToAction("Index");
        }

    }

}
