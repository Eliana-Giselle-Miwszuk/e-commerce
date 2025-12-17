using System;
using System.Web;

namespace e_commerce
{
    public static class Seguridad
    {
        public static void VerificarUsuario(string rolRequerido = "")
        {
            var usuario = HttpContext.Current.Session["Usuario"] as Dominio.Usuario;
            if (usuario == null)
            {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
            else if (!string.IsNullOrEmpty(rolRequerido) && usuario.Rol != rolRequerido)
            {
                // Usuario logueado pero no tiene el rol requerido
                HttpContext.Current.Response.Redirect("Default.aspx");
            }
        }
    }
}
