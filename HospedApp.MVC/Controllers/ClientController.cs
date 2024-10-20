using HospedApp.Core;
using HospedApp.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;
[Authorize]
public class ClientController : Controller
{
    private readonly IAdo Ado;
    public ClientController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var client = await Ado.GetClients();
        return View(client.OrderByDescending(x => x.IdClient));
    }

    [HttpGet]
    public async Task<IActionResult> Modify(int id)
    {
        if (id != 0)
        {
            var client = await Ado.GetClients();
            var encontrado = client.FirstOrDefault(x => x.IdClient == id);
            if (encontrado != null)
            {
                return View("Upsert", encontrado);
            }
        }
        return NotFound();
    }
    [HttpGet]
    public IActionResult Register(Client client) => View("Upsert", client);


    [HttpPost]
    public async Task<IActionResult> Upsert(Client client)
    {
        if (client == null)
            return RedirectToAction("Client");

        if (client.IdClient == 0)
        {
            await Ado.CreateClient(client);
            return RedirectToAction("Index");
        }
        var clientsearch = await Ado.GetClients();
        var encontrado = clientsearch.FirstOrDefault(x => x.IdClient == client.IdClient);
        if (encontrado != null)
        {
            await Ado.ModifyClient(client);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

}
