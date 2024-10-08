using System.IdentityModel.Tokens.Jwt;
using HospedApp.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace HospedApp.MVC.Decorators
{
    public class AuthTokenAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Acceder al token desde la cookie
            if (context.HttpContext.Request.Cookies.TryGetValue("jwtToken", out var token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Extraer los claims
                var claims = jwtToken.Claims.FirstOrDefault(x => x.Type == "IdUser")?.Value;

                if (claims != null)
                {
                    await next();
                }
            }

            context.Result = new RedirectToActionResult("", "Login", null);
        }

    }
}