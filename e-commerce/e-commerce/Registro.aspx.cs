using System;
using System.Web.UI;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(telefono))
            {
                lblMensaje.Text = "Todos los campos son obligatorios.";
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                // Verificar si el email ya existe
                if (negocio.ExisteEmail(email))
                {
                    lblMensaje.Text = "El email ya está registrado. Intente con otro.";
                    return;
                }

                Usuario usuario = new Usuario
                {
                    Nombre = nombre,
                    Email = email,
                    Telefono = telefono,
                    Password = password,
                    Rol = "Cliente"
                };

                negocio.RegistrarUsuario(usuario);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Registro exitoso. Puede iniciar sesión.";

                txtNombre.Text = "";
                txtEmail.Text = "";
                txtTelefono.Text = "";
                txtPassword.Text = "";
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al registrar: " + ex.Message;
            }
        }
    }
}
