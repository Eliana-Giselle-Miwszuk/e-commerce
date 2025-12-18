using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class MisDirecciones : Page
    {
        private DireccionNegocio direccionNegocio = new DireccionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.VerificarUsuario("Cliente", "Admin");

            if (!IsPostBack)
            {
                CargarDirecciones();
            }
        }

        private void CargarDirecciones()
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            List<Direccion> lista = direccionNegocio.ListarDireccionesPorUsuario(usuario.IdUsuario);
            gvDirecciones.DataSource = lista;
            gvDirecciones.DataBind();
        }

        protected void gvDirecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idDireccion = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("AgregarEditarDireccion.aspx?id=" + idDireccion);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditarDireccion.aspx");
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            int idDireccion;
            if (int.TryParse(hfEliminarId.Value, out idDireccion))
            {
                try
                {
                    // Verificar si la dirección tiene pedidos asociados
                    bool enUso = direccionNegocio.TienePedidosAsociados(idDireccion);
                    if (enUso)
                    {
                        lblMensaje.Visible = true;
                        lblMensaje.CssClass = "alert alert-warning";
                        lblMensaje.Text = "No se puede eliminar esta dirección porque está asociada a un pedido.";
                        return;
                    }

                    // Si no tiene pedidos, se elimina
                    direccionNegocio.EliminarDireccion(idDireccion);

                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "alert alert-success";
                    lblMensaje.Text = "Dirección eliminada correctamente.";

                    CargarDirecciones();
                }
                catch (Exception ex)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Text = "Error al eliminar la dirección: " + ex.Message;
                }
            }
        }




    }
}
