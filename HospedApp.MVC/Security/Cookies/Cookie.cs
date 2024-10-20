namespace HospedApp.MVC.Security.Cookies
{
    public class Cookie
    {
        public static void CrearCookie(HttpResponse Response, string nombre, string token)
        {
            try
            {
                // Establecer el token en una cookie
                Response.Cookies.Append(nombre, token, new CookieOptions
                {
                    HttpOnly = true, // evita scripts sospechosos
                    Secure = false, // falso ya que por ahora debe aceptar http
                    SameSite = SameSiteMode.Strict // Previene CSRF corte bueno
                });
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo crear la galleta: " + ex.Message);
            }
        }
    }
}