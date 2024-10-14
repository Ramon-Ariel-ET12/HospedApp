using System.Data;
using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.Dapper;
using HospedApp.MVC.Decorators;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class BedController : Controller
{
    private readonly IAdo Ado;
    public BedController(IAdo ado) => Ado = ado;

    [AuthToken]
    [HttpGet]

    public async Task<IActionResult> Index()
    {
        var bed = await Ado.GetBeds();
        return View(bed);
    }

    [AuthToken]
    [HttpGet]
    public IActionResult Register(Bed bed) => View("Upsert", bed);

    [AuthToken]
    [HttpPost]
    public IActionResult Upsert(Bed bed)
    {
        if (bed == null)
            return RedirectToAction("Bed");
        
        if (bed.IdBed == 0)
        {
            Ado.CreateBed(bed);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}
 