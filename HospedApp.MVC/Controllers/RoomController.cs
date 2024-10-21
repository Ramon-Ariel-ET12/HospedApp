using HospedApp.Core;
using HospedApp.Core.Entities.Relations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class RoomController : Controller
{
    private readonly IAdo Ado;
    public RoomController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var room = await Ado.GetRoomBeds();
        return View(room.OrderByDescending(x => x.IdRoomBed));
    }

    [HttpGet]
    public async Task<IActionResult> Register(RoomBed roomBed)
    {
        var beds = await Ado.GetBeds();
        ViewBag.Beds = beds;
        return View("Upsert", roomBed);
    }
    [HttpGet]
    public async Task<IActionResult> Modify(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var beds = await Ado.GetBeds();
        var rooms = await Ado.GetRoomBeds();
        ViewBag.Beds = beds;
        var encontrado = rooms.FirstOrDefault(x => x.IdRoomBed == id);
        if (encontrado != null)
        {
            return View("Upsert", encontrado);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(RoomBed roomBed)
    {
        if (roomBed == null)
            return RedirectToAction("Room");

        if (roomBed.IdRoomBed == 0)
        {
            var beds = await Ado.GetBeds();
            var existe = beds.FirstOrDefault(x => x.Name == roomBed.Bed!.Name);
            if (existe != null)
            {
                await Ado.CreateRoom(roomBed!.Room!);
                roomBed.Bed!.IdBed = existe.IdBed;
                var room = await Ado.GetRooms();
                var cuarto = room.MaxBy(x => x.IdRoom);
                roomBed.Room!.IdRoom = cuarto!.IdRoom;
                await Ado.CreateRoomBed(roomBed);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        if (roomBed.IdRoomBed != 0)
        {
            var beds = await Ado.GetBeds();
            var existe = beds.FirstOrDefault(x => x.Name == roomBed.Bed!.Name);
            if (existe != null)
            {
                roomBed.Bed!.IdBed = existe.IdBed;
                var room = await Ado.GetRooms();
                var cuarto = room.MaxBy(x => x.IdRoom);
                roomBed.Room!.IdRoom = cuarto!.IdRoom;
                await Ado.ModifyRoom(roomBed.Room);
                await Ado.ModifyRoomBed(roomBed);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }
        return RedirectToAction("Index");
    }
}