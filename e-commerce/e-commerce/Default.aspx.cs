using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductosDestacados();
        }

        private void CargarProductosDestacados()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productos = negocio.Top3MasVendidos();

            // Guardamos en Session para el carrito
            Session["ProductosLista"] = productos;

            rptProductosDestacados.DataSource = productos;
            rptProductosDestacados.DataBind();
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

            // Actualizar el carrito del MasterPage
            ((MenuWeb)Master).ActualizarCantidadCarrito();
        }
    }
}

