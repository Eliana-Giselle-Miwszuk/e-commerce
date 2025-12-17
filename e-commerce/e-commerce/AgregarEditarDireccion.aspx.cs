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
    public partial class AgregarEditarDireccion : System.Web.UI.Page
    {
        private DireccionNegocio direccionNegocio = new DireccionNegocio();
        private int idDireccion = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            Seguridad.VerificarUsuario("Admin");
            if (!IsPostBack)
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                if (Request.QueryString["id"] != null)
                {
                    idDireccion = Convert.ToInt32(Request.QueryString["id"]);
                    ltTitulo.Text = "Editar Dirección";
                    CargarDatos(idDireccion);
                }
            }
        }

        private void CargarDatos(int id)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            var direccion = direccionNegocio.ListarDireccionesPorUsuario(usuario.IdUsuario).Find(d => d.IdDireccion == id);
            if (direccion != null)
            {
                txtCalle.Text = direccion.Calle;
                txtNumero.Text = direccion.Numero;
                txtLocalidad.Text = direccion.Localidad;
                txtCodigoPostal.Text = direccion.CodigoPostal;
                txtObservaciones.Text = direccion.Observaciones;
            }
            else
            {
                Response.Redirect("MisDirecciones.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null) Response.Redirect("Login.aspx");

            Direccion direccion = new Direccion
            {
                IdUsuario = usuario.IdUsuario,
                Calle = txtCalle.Text.Trim(),
                Numero = txtNumero.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                CodigoPostal = txtCodigoPostal.Text.Trim(),
                Observaciones = txtObservaciones.Text.Trim()
            };

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    direccion.IdDireccion = Convert.ToInt32(Request.QueryString["id"]);
                    direccionNegocio.EditarDireccion(direccion);
                }
                else
                {
                    direccionNegocio.AgregarDireccion(direccion);
                }

                Response.Redirect("MisDirecciones.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("MisDirecciones.aspx");
        }
    }
}