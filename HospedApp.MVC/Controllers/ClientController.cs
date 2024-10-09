using System.Data;
using HospedApp.Core;
using HospedApp.Core.Entities;
using HospedApp.MVC.Decorators;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class ClientController : Controller
{
    private readonly IAdo Ado;
    public ClientController(IAdo ado) => Ado = ado;

    [AuthToken]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = await Ado.GetClients();
        return View(client);
    }

    [AuthToken]
    [HttpGet]
    public IActionResult Register(Client client) => View("Upsert", client);


    [AuthToken]
    [HttpPost]
    public IActionResult Upsert(Client client)
    {
        if (client == null)
            return RedirectToAction("Client");

        if (client.IdClient == 0)
        {
            Ado.CreateClient(client);
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }

}
