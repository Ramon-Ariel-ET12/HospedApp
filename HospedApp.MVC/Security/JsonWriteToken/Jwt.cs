using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospedApp.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace HospedApp.MVC.Security.JsonWriteToken
{
    public class Jwt
    {
        public string ObtenerToken(User User)
        {
            // Obtener key y encriptarlo con SHA256
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("lB6y8FMUJ2F2T$gO0!23^vO409Un3CT&"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var IdUser = JsonConvert.SerializeObject(User.IdUser);
            var Name = JsonConvert.SerializeObject(User.Name);
            var LastName = JsonConvert.SerializeObject(User.LastName);
            var Email = JsonConvert.SerializeObject(User.Email);
            var Pass = JsonConvert.SerializeObject(User.Pass);

            // Crear los claims
            var claims = new[]
            {
            new Claim("IdUser", IdUser),
            new Claim("Name", Name),
            new Claim("LastName", LastName),
            new Claim("Email", Email),
            new Claim("Pass", Pass),
        };

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5094",
                audience: "http://localhost:5094",
                claims: claims,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}