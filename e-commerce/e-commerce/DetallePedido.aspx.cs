using System;
using Negocio;
using Dominio;

namespace e_commerce
{
    public partial class DetallePedido : AdminPageOLD
    {
        PedidoNegocio pedidoNegocio = new PedidoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {

            Seguridad.VerificarUsuario("Admin");

            if (!IsPostBack)
            {
                int idPedido = int.Parse(Request.QueryString["id"]);
                CargarInfoPedido(idPedido);
                CargarDetalle(idPedido);
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
