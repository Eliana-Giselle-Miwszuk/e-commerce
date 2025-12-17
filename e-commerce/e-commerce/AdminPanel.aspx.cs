using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace e_commerce
{
    public partial class AdminPanel : AdminPageOLD
    {
        UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        ProductoNegocio productoNegocio = new ProductoNegocio();
        PedidoNegocio pedidoNegocio = new PedidoNegocio();

        private const string VS_ID = "EliminarId";
        private const string VS_TIPO = "EliminarTipo";

        protected void Page_Load(object sender, EventArgs e)
        {
            Seguridad.VerificarUsuario("Admin");

            if (!IsPostBack)
            {
                CargarUsuarios();
                CargarProductos();
                CargarPedidos();
            }
        }

        // ================= USUARIOS =================
        private void CargarUsuarios()
        {
            gvUsuarios.DataSource = usuarioNegocio.ListarUsuarios();
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Usuario u = (Usuario)e.Row.DataItem;

                ((DropDownList)e.Row.FindControl("ddlRol")).SelectedValue = u.Rol;
                ((CheckBox)e.Row.FindControl("chkActivo")).Checked = u.Activo;
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
                Response.Redirect("AgregarEditarUsuario.aspx?id=" + id);

            if (e.CommandName == "Eliminar")
            {
                ViewState[VS_ID] = id;
                ViewState[VS_TIPO] = "Usuario";

                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "modalEliminar",
                    "mostrarModalEliminar();",
                    true
                );
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditarUsuario.aspx");
        }

        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            int id = (int)gvUsuarios.DataKeys[row.RowIndex].Value;

            Usuario u = usuarioNegocio.ObtenerUsuarioPorId(id);

            if (UsuarioActual != null && u.IdUsuario == UsuarioActual.IdUsuario)
                return;

            u.Activo = ((CheckBox)sender).Checked;
            usuarioNegocio.ActualizarUsuarioCompleto(u);
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            int id = (int)gvUsuarios.DataKeys[row.RowIndex].Value;

            Usuario u = usuarioNegocio.ObtenerUsuarioPorId(id);

            if (UsuarioActual != null && u.IdUsuario == UsuarioActual.IdUsuario)
                return;

            u.Rol = ((DropDownList)sender).SelectedValue;
            usuarioNegocio.ActualizarUsuarioCompleto(u);
        }

        // ================= PRODUCTOS =================
        private void CargarProductos()
        {
            gvProductos.DataSource = productoNegocio.ListarProductosActivos();
            gvProductos.DataBind();
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
                Response.Redirect("AgregarEditarProducto.aspx?id=" + id);

            if (e.CommandName == "Eliminar")
            {
                ViewState[VS_ID] = id;
                ViewState[VS_TIPO] = "Producto";

                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "modalEliminar",
                    "mostrarModalEliminar();",
                    true
                );
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarEditarProducto.aspx");
        }

        protected void chkActivoProd_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((CheckBox)sender).NamingContainer;
            int id = (int)gvProductos.DataKeys[row.RowIndex].Value;

            Producto p = productoNegocio.ObtenerProductoPorId(id);
            p.Activo = ((CheckBox)sender).Checked;

            productoNegocio.ActualizarProducto(p);
        }

        // ================= PEDIDOS =================
        private void CargarPedidos()
        {
            gvPedidos.DataSource = pedidoNegocio.ListarPedidosConUsuario();
            gvPedidos.DataBind();
        }

        protected void gvPedidos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dynamic pedido = e.Row.DataItem;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlEstadoPedido");
                ddl.SelectedValue = pedido.Estado;
            }
        }

        protected void ddlEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((DropDownList)sender).NamingContainer;
            int idPedido = (int)gvPedidos.DataKeys[row.RowIndex].Value;

            string nuevoEstado = ((DropDownList)sender).SelectedValue;
            pedidoNegocio.ActualizarEstadoPedido(idPedido, nuevoEstado);
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerDetalle")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                int idPedido = (int)gvPedidos.DataKeys[index].Value;

                Response.Redirect("DetallePedido.aspx?id=" + idPedido);
            }
        }

        // ================= CONFIRMAR ELIMINACIÓN =================
        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            if (ViewState[VS_ID] == null || ViewState[VS_TIPO] == null)
                return;

            int id = (int)ViewState[VS_ID];
            string tipo = ViewState[VS_TIPO].ToString();

            if (tipo == "Usuario")
            {
                usuarioNegocio.EliminarUsuario(id);
                CargarUsuarios();
            }
            else if (tipo == "Producto")
            {
                productoNegocio.EliminarProducto(id);
                CargarProductos();
            }

            ViewState.Remove(VS_ID);
            ViewState.Remove(VS_TIPO);
        }
    }
}
