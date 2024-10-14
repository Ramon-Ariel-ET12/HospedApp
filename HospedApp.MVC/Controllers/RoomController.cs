using System.Data;
using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.Dapper;
using HospedApp.MVC.Decorators;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class RoomController : Controller
{
    private readonly IAdo Ado;
    public RoomController(IAdo ado) => Ado = ado;

    [AuthToken]
    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var room = await Ado.GetRooms();
        return View(room);
    }

    [AuthToken]
    [HttpGet]
    public IActionResult Register(Room room) => View("Upsert", room);

    [AuthToken]
    [HttpPost]
    public IActionResult Upsert(Room room)
    {
        if (room == null)
            return RedirectToAction("Room");
        
        if (room.IdRoom == 0)
        {
            Ado.CreateRoom(room);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}
