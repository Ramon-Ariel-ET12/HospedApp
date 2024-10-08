using HospedApp.Core;
using HospedApp.MVC.Decorators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

public class ClientController : Controller
{
    private readonly IAdo Ado;
    public ClientController(IAdo ado) => Ado = ado;

    [AuthToken]
    public async Task<IActionResult> Index()
    {
        var client = await Ado.GetClients();
        return View(client);
    }
}
