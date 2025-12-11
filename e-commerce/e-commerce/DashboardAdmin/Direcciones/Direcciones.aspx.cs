using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace e_commerce.DashboardAdmin.Direcciones
{
    public partial class Direcciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDirecciones();
            }
        }

        private void CargarDirecciones()
        {
            try
            {
                DireccionNegocio negocio = new DireccionNegocio();
                List<Direccion> listaDirecciones = negocio.Listar();

                if (listaDirecciones != null && listaDirecciones.Count > 0)
                {
                    gdDirecciones.DataSource = listaDirecciones;
                    gdDirecciones.DataBind();
                    lblMensaje.Visible = false;
                }
                else
                {
                    // Si no hay datos, mostrar mensaje
                    gdDirecciones.DataSource = new List<Direccion>();
                    gdDirecciones.DataBind();
                    lblMensaje.Text = "No hay direcciones registradas";
                    lblMensaje.CssClass = "alert alert-info d-block";
                    lblMensaje.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Mostrar error detallado
                string errorMsg = "Error al cargar direcciones: ";

                // Verificar si es error de SQL
                if (ex.Message.Contains("Invalid object name"))
                {
                    errorMsg += "La tabla 'Direcciones' no existe en la base de datos.";
                }
                else if (ex.Message.Contains("conexión"))
                {
                    errorMsg += "Error de conexión a la base de datos.";
                }
                else
                {
                    errorMsg += ex.Message;
                }

                lblMensaje.Text = errorMsg;
                lblMensaje.CssClass = "alert alert-danger d-block";
                lblMensaje.Visible = true;

                // También mostrar en consola para depuración
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
                System.Diagnostics.Debug.WriteLine("STACK TRACE: " + ex.StackTrace);
            }
        }

        protected void gdDirecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idDireccion = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Eliminar")
                {
                    DireccionNegocio negocio = new DireccionNegocio();
                    negocio.Eliminar(idDireccion);

                    // Recargar la lista
                    CargarDirecciones();

                    lblMensaje.Text = "Dirección eliminada correctamente";
                    lblMensaje.CssClass = "alert alert-success d-block";
                    lblMensaje.Visible = true;
                }
                else if (e.CommandName == "Editar")
                {
                    Response.Redirect($"EditarDireccion.aspx?id={idDireccion}", false);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger d-block";
                lblMensaje.Visible = true;
            }
        }

        protected void gdDirecciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdDirecciones.PageIndex = e.NewPageIndex;
            CargarDirecciones();
        }
    }
}