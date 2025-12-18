using System;
using System.Web;
using System.Web.UI;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class AgregarEditarDireccion : Page
    {
        private DireccionNegocio direccionNegocio = new DireccionNegocio();
        private int idDireccion = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ya no hay seguridad aquí

            if (!IsPostBack)
            {
                var usuario = HttpContext.Current.Session["Usuario"] as Usuario;

                if (Request.QueryString["id"] != null && usuario != null)
                {
                    // Estamos editando
                    idDireccion = Convert.ToInt32(Request.QueryString["id"]);
                    ltTitulo.Text = "Editar Dirección";
                    CargarDireccion(idDireccion, usuario);
                }
                else
                {
                    // Estamos agregando
                    ltTitulo.Text = "Agregar Dirección";
                    // Formulario vacío
                }
            }
        }

        private void CargarDireccion(int id, Usuario usuario)
        {
            var direccion = direccionNegocio
                .ListarDireccionesPorUsuario(usuario.IdUsuario)
                .Find(d => d.IdDireccion == id);

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

        protected void btnConfirmarGuardar_Click(object sender, EventArgs e)
        {
            var usuario = HttpContext.Current.Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                // Si no hay usuario en sesión, redirigir al login
                Response.Redirect("Login.aspx");
                return;
            }

            lblMensaje.Text = "";
            lblMensaje.CssClass = "text-danger";

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtCalle.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                string.IsNullOrWhiteSpace(txtCodigoPostal.Text))
            {
                lblMensaje.Text = "Por favor complete todos los campos obligatorios.";
                return;
            }

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

                lblMensaje.CssClass = "text-success";
                lblMensaje.Text = "Dirección guardada correctamente.";

                ClientScript.RegisterStartupScript(this.GetType(), "redirect",
                    "setTimeout(function(){ window.location='MisDirecciones.aspx'; }, 1000);", true);
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
