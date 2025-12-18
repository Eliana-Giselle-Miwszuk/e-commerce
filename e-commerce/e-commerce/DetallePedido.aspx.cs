using Dominio;
using Negocio;
using System;
using System.Web;
using System.Web.UI;

namespace e_commerce
{
    public partial class DetallePedido : Page
    {
        PedidoNegocio pedidoNegocio = new PedidoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar que el usuario esté logueado y sea Admin
            Seguridad.VerificarUsuario("Admin");

            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int idPedido))
                {
                    CargarInfoPedido(idPedido);
                    CargarDetalle(idPedido);
                }
                else
                {
                    // Si no hay id válido, redirigir al listado
                    Response.Redirect("AdminPanel.aspx");
                }
            }
        }

        private void CargarInfoPedido(int idPedido)
        {
            PedidoDetalleCompletoView p = pedidoNegocio.ObtenerInfoPedido(idPedido);

            lblCliente.Text = p.NombreUsuario;
            lblEmail.Text = p.Email;
            lblTelefono.Text = p.Telefono;
            lblDireccion.Text = $"{p.Calle} {p.Numero}, {p.Localidad} ({p.CodigoPostal})";
            lblFecha.Text = p.Fecha.ToString("dd/MM/yyyy");
            lblEstado.Text = p.Estado;
            lblTotal.Text = p.Total.ToString("C");
        }

        private void CargarDetalle(int idPedido)
        {
            gvDetalle.DataSource = pedidoNegocio.ListarDetallePedido(idPedido);
            gvDetalle.DataBind();
        }
    }
}
