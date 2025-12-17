using System;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                lblMensaje.Text = "Debe ingresar Email y Password.";
                return;
            }

            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.ValidarLogin(email, password);

                if (usuario != null)
                {
                    Session["Usuario"] = usuario;

                    if (Session["Carrito"] == null)
                        Session["Carrito"] = new Carrito();

                    if (usuario.Rol == "Admin")
                        Response.Redirect("AdminPanel.aspx");
                    else
                        Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMensaje.Text = "Email o contraseña incorrectos.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al iniciar sesión: " + ex.Message;
            }
        }




    }
}