using HospedApp.Core;
using HospedApp.MVC.Security.Cookies;
using HospedApp.MVC.Security.JsonWriteToken;
using Microsoft.AspNetCore.Mvc;

namespace HospedApp.MVC.Controllers
{
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
        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies.ContainsKey("JwtToken"))
                return Redirect("/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await Ado.Login(email, password);
            if (user == null)
                return BadRequest();
            var token = Jwt.ObtenerToken(user);
            Cookie.CrearCookie(Response, "JwtToken", token);
            return Redirect("/");
        }

        [HttpPost]
        public IActionResult Logout()
        {
                HttpContext.Response.Cookies.Delete("JwtToken");
                return Redirect("/Login");
        }
    }
}