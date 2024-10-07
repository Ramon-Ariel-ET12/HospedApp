using HospedApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class ClientController : Controller
{
    private readonly IAdo Ado;
    public ClientController(IAdo ado) => Ado = ado;
    public async Task<IActionResult> Index()
    {
        var client = await Ado.GetClients();
        return View(client);
    }
}
