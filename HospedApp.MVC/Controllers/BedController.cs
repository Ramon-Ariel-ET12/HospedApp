using HospedApp.Core;
using HospedApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class BedController : Controller
{
    private readonly IAdo Ado;
    public BedController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var bed = await Ado.GetBeds();
        return View(bed.OrderByDescending(x => x.IdBed));
    }

    [HttpGet]
    public IActionResult Register(Bed bed) => View("Upsert", bed);
    [HttpGet]
    public async Task<IActionResult> Modify(int id)
    {
        var beds = await Ado.GetBeds();
        var bed = beds.FirstOrDefault(x => x.IdBed == id);
        return View("Upsert", bed);
    }

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
        if (bed.IdBed != 0)
        {
            
            Ado.ModifyBed(bed);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
}