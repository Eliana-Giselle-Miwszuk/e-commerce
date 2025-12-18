using System;
using Dominio;
using Negocio;
using System.Web;

namespace e_commerce
{
    public partial class AgregarEditarProducto : System.Web.UI.Page
    {
        private ProductoNegocio productoNegocio = new ProductoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar que el usuario esté logueado y sea Admin
            Seguridad.VerificarUsuario("Admin");

            if (!IsPostBack)
            {
                CargarCategorias();

                if (Request.QueryString["id"] != null)
                {
                    if (int.TryParse(Request.QueryString["id"], out int idProducto))
                        CargarProducto(idProducto);
                    else
                        Response.Redirect("AdminPanel.aspx");
                }
            }
        }

        private void CargarCategorias()
        {
            ddlCategoria.DataSource = productoNegocio.ListarCategorias();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "IdCategoria";
            ddlCategoria.DataBind();
        }

        private void CargarProducto(int id)
        {
            Producto p = productoNegocio.ListarProductosActivos()
                                        .Find(x => x.IdProducto == id);

            if (p != null)
            {
                txtNombre.Text = p.Nombre;
                txtDescripcion.Text = p.Descripcion;
                txtPrecio.Text = p.Precio.ToString();
                txtStock.Text = p.Stock.ToString();
                ddlCategoria.SelectedValue = p.IdCategoria.ToString();
                chkActivo.Checked = p.Activo;
                txtImagenUrl.Text = p.ImagenUrl;
            }
        }

        protected void btnConfirmarGuardar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) ||
                !int.TryParse(txtStock.Text, out int stock))
                return;

            Producto p = new Producto
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text,
                Precio = precio,
                Stock = stock,
                IdCategoria = int.Parse(ddlCategoria.SelectedValue),
                Activo = chkActivo.Checked,
                ImagenUrl = txtImagenUrl.Text
            };

            if (Request.QueryString["id"] != null)
            {
                p.IdProducto = Convert.ToInt32(Request.QueryString["id"]);
                productoNegocio.ActualizarProducto(p);
            }
            else
            {
                productoNegocio.AgregarProducto(p);
            }

            Response.Redirect("AdminPanel.aspx");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}
