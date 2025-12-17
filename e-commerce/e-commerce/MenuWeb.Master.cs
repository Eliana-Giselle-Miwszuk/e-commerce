using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class MenuWeb : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarCantidadCarrito();
                MostrarUsuario();
            }
        }

        public void ActualizarCantidadCarrito()
        {
            Carrito carrito = (Carrito)Session["Carrito"];
            int totalProductos = 0;

            if (carrito != null)
                foreach (var item in carrito.Items)
                    totalProductos += item.Cantidad;

            lblCantidadCarrito.InnerText = totalProductos.ToString();
            upCarrito.Update();
        }
        private void MostrarUsuario()
        {
            Usuario usuario = Session["Usuario"] as Usuario;

            if (usuario != null)
            {
                phUsuario.Visible = false;       // Oculta Login/Registro
                phLogueado.Visible = true;       // Muestra nombre + Logout
                lblUsuarioNav.InnerText = "Hola, " + usuario.Nombre;

                // Mostrar link a AdminPanel solo si es Admin
                phAdmin.Visible = usuario.Rol == "Admin";
            }
            else
            {
                phUsuario.Visible = true;
                phLogueado.Visible = false;
                phAdmin.Visible = false; // Oculta link a AdminPanel si no hay usuario
            }
        }
       

        protected void CerrarSesion_ServerClick(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}