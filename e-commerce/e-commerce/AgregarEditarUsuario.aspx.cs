using System;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class AgregarEditarUsuario : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.VerificarUsuario("Admin");
            if (!IsPostBack)
            {
                CargarRoles();

                if (Request.QueryString["id"] != null)
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                    CargarUsuario(idUsuario);
                }
            }
        }

        private void CargarRoles()
        {
            ddlRol.Items.Clear();
            ddlRol.Items.Add("Admin");
            ddlRol.Items.Add("Cliente");
        }

        private void CargarUsuario(int id)
        {
            Usuario u = usuarioNegocio.ListarUsuarios().Find(x => x.IdUsuario == id);
            if (u != null)
            {
                txtNombre.Text = u.Nombre;
                txtEmail.Text = u.Email;
                txtPassword.Text = u.Password;
                txtTelefono.Text = u.Telefono;
                ddlRol.SelectedValue = u.Rol;
                chkActivo.Checked = u.Activo;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario
            {
                Nombre = txtNombre.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                Telefono = txtTelefono.Text,
                Rol = ddlRol.SelectedValue,
                Activo = chkActivo.Checked
            };

            if (Request.QueryString["id"] != null)
            {
                u.IdUsuario = Convert.ToInt32(Request.QueryString["id"]);
                usuarioNegocio.ActualizarUsuarioCompleto(u);
            }
            else
            {
                usuarioNegocio.RegistrarUsuario(u);
            }

            Response.Redirect("AdminPanel.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}
