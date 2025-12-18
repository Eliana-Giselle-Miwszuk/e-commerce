using System;
using System.Web;
using Dominio;

namespace e_commerce
{
    public static class Seguridad
    {
        // Ahora acepta un array de roles permitidos
        public static void VerificarUsuario(params string[] rolesPermitidos)
        {
            var usuario = HttpContext.Current.Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                // No está logueado
                HttpContext.Current.Response.Redirect("Login.aspx");
            }
            else if (rolesPermitidos.Length > 0)
            {
                // Si el usuario no tiene ninguno de los roles permitidos
                bool autorizado = false;
                foreach (var rol in rolesPermitidos)
                {
                    if (usuario.Rol == rol)
                    {
                        autorizado = true;
                        break;
                    }
                }

                if (!autorizado)
                {
                    HttpContext.Current.Response.Redirect("Default.aspx");
                }
            }
        }
    }
}
