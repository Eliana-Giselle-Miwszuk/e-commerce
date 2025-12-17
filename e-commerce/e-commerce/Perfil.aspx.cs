using Dominio;
using Negocio;
using System;


namespace e_commerce
{
    public partial class Perfil : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosUsuario();
            }
        }

        private void CargarDatosUsuario()
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            txtNombre.Text = usuario.Nombre;
            txtEmail.Text = usuario.Email;
            txtTelefono.Text = usuario.Telefono;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = Session["Usuario"] as Usuario;
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx");
                    return;
                }

                // Validar contraseña si se quiere cambiar
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (txtPassword.Text != txtConfirmPassword.Text)
                    {
                        lblMensaje.Text = "Las contraseñas no coinciden.";
                        return;
                    }
                    usuario.Password = txtPassword.Text;
                }

                // Actualizar datos
                usuario.Nombre = txtNombre.Text.Trim();
                usuario.Email = txtEmail.Text.Trim();
                usuario.Telefono = txtTelefono.Text.Trim();

                usuarioNegocio.ActualizarUsuario(usuario);

                // Actualizar sesión
                Session["Usuario"] = usuario;

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Datos actualizados correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}