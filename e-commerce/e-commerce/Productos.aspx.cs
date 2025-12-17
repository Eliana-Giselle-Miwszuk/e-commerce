using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class Productos : System.Web.UI.Page
    {
        private ProductoNegocio negocio = new ProductoNegocio();
        private CategoriaNegocio negocioCategoria = new CategoriaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarProductos();
            }
        }

        private void CargarCategorias()
        {
            var categorias = negocioCategoria.ListarCategoriasActivas();
            ddlCategorias.Items.Clear();
            ddlCategorias.Items.Add(new ListItem("Todas las categorías", "0"));
            foreach (var cat in categorias)
            {
                ddlCategorias.Items.Add(new ListItem(cat.Nombre, cat.IdCategoria.ToString()));
            }
        }

        private void CargarProductos(string filtro = "", int categoriaId = 0)
        {
            List<Producto> productos = negocio.ListarProductosActivos();

            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(filtro))
            {
                productos = productos
                    .Where(p => p.Nombre.ToLower().Contains(filtro.ToLower())
                             || p.Descripcion.ToLower().Contains(filtro.ToLower()))
                    .ToList();
            }

            // Filtrar por categoría
            if (categoriaId > 0)
            {
                productos = productos.Where(p => p.IdCategoria == categoriaId).ToList();
            }

            Session["ProductosLista"] = productos;

            rptProductos.DataSource = productos;
            rptProductos.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            int productoId = int.Parse(btn.CommandArgument);

            var productos = (List<Producto>)Session["ProductosLista"];
            var producto = productos.Find(p => p.IdProducto == productoId);

            Carrito carrito = (Carrito)Session["Carrito"] ?? new Carrito();
            carrito.AgregarProducto(producto.IdProducto, producto.Nombre, producto.Precio, 1);
            Session["Carrito"] = carrito;

            btn.Text = "Agregado ✅";
            btn.Enabled = false;

            ((MenuWeb)Master).ActualizarCantidadCarrito();
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBusqueda.Text.Trim();
            int categoriaId = int.Parse(ddlCategorias.SelectedValue);
            CargarProductos(filtro, categoriaId);
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = txtBusqueda.Text.Trim();
            int categoriaId = int.Parse(ddlCategorias.SelectedValue);
            CargarProductos(filtro, categoriaId);
        }
    }
}