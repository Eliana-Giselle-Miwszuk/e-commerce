using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class FinalizarPedido : System.Web.UI.Page
    {
        private DireccionNegocio direccionNegocio = new DireccionNegocio();
        private PedidoNegocio pedidoNegocio = new PedidoNegocio();
        private FormaEntregaNegocio formaEntregaNegocio = new FormaEntregaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.VerificarUsuario();
            if (!IsPostBack)
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                CargarResumenCarrito();
                CargarDirecciones(usuario.IdUsuario);
                CargarFormasEntrega();
            }
        }

        private void CargarResumenCarrito()
        {
            Carrito carrito = (Carrito)Session["Carrito"];
            if (carrito == null || carrito.Items.Count == 0)
            {
                lblMensaje.Text = "Tu carrito está vacío.";
                btnConfirmarPedido.Enabled = false;
                return;
            }

            gvResumenCarrito.DataSource = carrito.Items;
            gvResumenCarrito.DataBind();

            lblTotalPedido.Text = "Total: " + carrito.Total().ToString("C");
        }

        private void CargarDirecciones(int idUsuario)
        {
            List<Direccion> direcciones = direccionNegocio.ListarDireccionesPorUsuario(idUsuario);
            ddlDirecciones.Items.Clear();
            ddlDirecciones.Items.Add(new ListItem("Seleccione una dirección", "0"));

            foreach (var d in direcciones)
            {
                string texto = $"{d.Calle} {d.Numero}, {d.Localidad} ({d.CodigoPostal})";
                ddlDirecciones.Items.Add(new ListItem(texto, d.IdDireccion.ToString()));
            }
        }

        private void CargarFormasEntrega()
        {
            var formas = formaEntregaNegocio.ListarFormasEntrega();
            ddlFormasEntrega.Items.Clear();
            ddlFormasEntrega.Items.Add(new ListItem("Seleccione forma de entrega", "0"));

            foreach (var f in formas)
            {
                ddlFormasEntrega.Items.Add(new ListItem(f.Nombre, f.IdFormaEntrega.ToString()));
            }
        }

        protected void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                Carrito carrito = (Carrito)Session["Carrito"];

                if (ddlDirecciones.SelectedValue == "0")
                {
                    lblMensaje.Text = "Debe seleccionar una dirección de entrega.";
                    return;
                }

                if (ddlFormasEntrega.SelectedValue == "0")
                {
                    lblMensaje.Text = "Debe seleccionar una forma de entrega.";
                    return;
                }

                int idDireccion = Convert.ToInt32(ddlDirecciones.SelectedValue);
                int idFormaEntrega = Convert.ToInt32(ddlFormasEntrega.SelectedValue);

                pedidoNegocio.CrearPedido(usuario, carrito, idDireccion, idFormaEntrega);

                carrito.Limpiar();
                Session["Carrito"] = carrito;
                ((MenuWeb)this.Master).ActualizarCantidadCarrito();

                Response.Redirect("Gracias.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al finalizar pedido: " + ex.Message;
            }
        }
    }
}