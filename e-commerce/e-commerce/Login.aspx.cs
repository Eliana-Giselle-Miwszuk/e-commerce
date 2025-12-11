using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_commerce
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Opcional: Si ya está logueado, redirigir a página principal
            if (Session["Usuario"] != null)
            {
                Response.Redirect("DashboardAdmin/Default.aspx");
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Validar campos vacíos
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MostrarError("Por favor, completa todos los campos");
                return;
            }

            // Aquí va la lógica de validación
            if (ValidarUsuario(email, password) == 1)
            {
                // Login exitoso
                Session["Usuario"] = email;
                Session["NombreUsuario"] = ObtenerNombreUsuario(email);

                // Mostrar mensaje de éxito
                MostrarExito("¡Login exitoso! Redirigiendo...");

                // Redirigir después de 2 segundos
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                    "setTimeout(function(){ window.location.href = 'DashboardAdmin/Default.aspx'; }, 2000);", true);
            }
            else if (ValidarUsuario(email, password) == 2)
            {
                // Login exitoso
                Session["Usuario"] = email;
                Session["NombreUsuario"] = ObtenerNombreUsuario(email);

                // Mostrar mensaje de éxito
                MostrarExito("¡Login exitoso! Redirigiendo...");

                // Redirigir después de 2 segundos
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect",
                    "setTimeout(function(){ window.location.href = 'DashboardAdmin/Default.aspx'; }, 2000);", true);
            }
            else
            {
                // Mostrar error
                MostrarError("Email o contraseña incorrectos");
            }
        }
        private int ValidarUsuario(string email, string password)
        {
            // USUARIOS DE PRUEBA - Cámbialos según necesites
            if (email == "admin@test.com" && password == "123")
            {
                return 1;
            }

            if (email == "usuario@test.com" && password == "123")
            {
                return 2;
            }

            if (email == "test@test.com" && password == "123")
            {
                return 3;
            }

            return 0;
        }
        private string ObtenerNombreUsuario(string email)
        {
            // Simplemente retorna la parte antes del @ como nombre
            return email.Split('@')[0];
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
            pnlExito.Visible = false;
        }

        private void MostrarExito(string mensaje)
        {
            lblExito.Text = mensaje;
            pnlExito.Visible = true;
            pnlMensaje.Visible = false;
        }
    }
}