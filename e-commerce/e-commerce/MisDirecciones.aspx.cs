using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace e_commerce
{
    public partial class MisDirecciones : System.Web.UI.Page
    {
        private DireccionNegocio direccionNegocio = new DireccionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDirecciones();
            }
        }

        private void CargarDirecciones()
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

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
            else if (e.CommandName == "Eliminar")
            {
                try
                {
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditarDireccion.aspx");
        }
    }
}