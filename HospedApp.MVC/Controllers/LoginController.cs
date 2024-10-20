using System.Security.Claims;
using HospedApp.Core;
using HospedApp.MVC.Security.JsonWriteToken;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using HospedApp.MVC.Security.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers;

[Authorize]
public class LoginController : Controller
{
    private readonly IAdo Ado;
    private readonly Jwt Jwt;
    public LoginController(IAdo ado, Jwt jwt)
    {
        Ado = ado;
        Jwt = jwt;
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index() => View();

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await Ado.Login(email, password);
        if (user == null)
        {
            TempData["Message"] = @"Creo que la contraseña o el email son incorrectos, ¿podrías intentar con otro? 
                                                    O podrías fijarte si escribiste bien, eso pasa a mucha gente.";
            return Redirect("/Home");
        }
        else
        {
            var token = Jwt.ObtenerToken(user);
            Cookie.CrearCookie(Response, "AuthToken", token);
            var claims = new List<Claim>(){
                new("LastName", user.LastName!),
                new("Name", user.Name!),
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
        return Redirect("/");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/Login");
    }
}