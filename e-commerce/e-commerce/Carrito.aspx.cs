using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace e_commerce
{
    public partial class CarritoPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            Carrito carrito = (Carrito)Session["Carrito"] ?? new Carrito();
            gvCarrito.DataSource = carrito.Items;
            gvCarrito.DataBind();

            lblTotal.Text = "Total: " + carrito.Total().ToString("C");
        }

        protected void gvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Quitar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                Carrito carrito = (Carrito)Session["Carrito"];
                if (carrito != null && carrito.Items.Count > index)
                {
                    carrito.QuitarProducto(carrito.Items[index].ProductoId);
                    Session["Carrito"] = carrito;
                    CargarCarrito();
                }
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Carrito carrito = (Carrito)Session["Carrito"];
            if (carrito != null)
            {
                carrito.Limpiar();
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            // Verificar que haya un usuario logueado
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx"); // Redirige a login si no hay sesión
                return;
            }

            // Verificar que haya productos en el carrito
            Carrito carrito = (Carrito)Session["Carrito"];
            if (carrito == null || carrito.Items.Count == 0)
            {
                lblTotal.Text = "No hay productos en el carrito para finalizar la compra.";
                return;
            }

            // Redirigir a la página de Finalizar Pedido
            Response.Redirect("FinalizarPedido.aspx");
        }
    }
}