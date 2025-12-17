using System.Web.UI;
using Dominio;

namespace e_commerce
{
    public class AdminPageOLD : Page
    {
        protected Usuario UsuarioActual
        {
            get { return Session["Usuario"] as Usuario; }
        }

        protected void VerificarAdmin()
        {
            if (UsuarioActual == null || UsuarioActual.Rol != "Admin")
                Response.Redirect("Default.aspx");
        }
    }
}