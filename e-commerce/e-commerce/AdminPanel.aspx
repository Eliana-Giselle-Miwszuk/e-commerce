<%@ Page Title="Panel Admin"
    Language="C#"
    MasterPageFile="~/MenuWeb.Master"
    AutoEventWireup="true"
    CodeBehind="AdminPanel.aspx.cs"
    Inherits="e_commerce.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h2 class="mb-3">Panel de Administración</h2>

<!-- ================= TABS ================= -->
<ul class="nav nav-tabs" role="tablist">
    <li class="nav-item">
        <button type="button" class="nav-link active"
                data-bs-toggle="tab"
                data-bs-target="#usuarios">
            Usuarios
        </button>
    </li>
    <li class="nav-item">
        <button type="button" class="nav-link"
                data-bs-toggle="tab"
                data-bs-target="#productos">
            Productos
        </button>
    </li>
    <li class="nav-item">
        <button type="button" class="nav-link"
                data-bs-toggle="tab"
                data-bs-target="#pedidos">
            Pedidos
        </button>
    </li>
</ul>

<div class="tab-content mt-3">

<!-- ================= USUARIOS ================= -->
<div class="tab-pane fade show active" id="usuarios">

    <asp:GridView ID="gvUsuarios" runat="server"
        CssClass="table table-striped"
        AutoGenerateColumns="False"
        DataKeyNames="IdUsuario"
        OnRowCommand="gvUsuarios_RowCommand"
        OnRowDataBound="gvUsuarios_RowDataBound">

        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Email" HeaderText="Email" />

            <asp:TemplateField HeaderText="Rol">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlRol" runat="server"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
                        <asp:ListItem Text="Admin" Value="Admin" />
                        <asp:ListItem Text="Cliente" Value="Cliente" />
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActivo" runat="server"
                        AutoPostBack="true"
                        OnCheckedChanged="chkActivo_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" Text="Editar"
                        CssClass="btn btn-sm btn-primary me-1"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("IdUsuario") %>' />

                    <asp:Button runat="server" Text="Eliminar"
                        CssClass="btn btn-sm btn-danger"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("IdUsuario") %>'
                        OnClientClick="return confirm('¿Desactivar usuario?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAgregarUsuario" runat="server"
        Text="Agregar Usuario"
        CssClass="btn btn-success"
        OnClick="btnAgregarUsuario_Click" />

</div>

<!-- ================= PRODUCTOS ================= -->
<div class="tab-pane fade" id="productos">

    <asp:GridView ID="gvProductos" runat="server"
        CssClass="table table-striped"
        AutoGenerateColumns="False"
        DataKeyNames="IdProducto"
        OnRowCommand="gvProductos_RowCommand">

        <Columns>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Stock" HeaderText="Stock" />

            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActivoProd" runat="server"
                        AutoPostBack="true"
                        OnCheckedChanged="chkActivoProd_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" Text="Editar"
                        CssClass="btn btn-sm btn-primary me-1"
                        CommandName="Editar"
                        CommandArgument='<%# Eval("IdProducto") %>' />

                    <asp:Button runat="server" Text="Eliminar"
                        CssClass="btn btn-sm btn-danger"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("IdProducto") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnAgregarProducto" runat="server"
        Text="Agregar Producto"
        CssClass="btn btn-success"
        OnClick="btnAgregarProducto_Click" />

</div>

<!-- ================= PEDIDOS ================= -->
<div class="tab-pane fade" id="pedidos">

<asp:GridView ID="gvPedidos" runat="server"
    CssClass="table table-striped"
    AutoGenerateColumns="False"
    DataKeyNames="IdPedido"
    OnRowCommand="gvPedidos_RowCommand"
    OnRowDataBound="gvPedidos_RowDataBound">

    <Columns>
        <asp:BoundField DataField="IdPedido" HeaderText="Pedido" />
        <asp:BoundField DataField="NombreUsuario" HeaderText="Cliente" />
        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />

        <asp:TemplateField HeaderText="Estado">
            <ItemTemplate>
                <asp:DropDownList ID="ddlEstadoPedido" runat="server"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEstadoPedido_SelectedIndexChanged">
                    <asp:ListItem Text="Pendiente" />
                    <asp:ListItem Text="Pagado" />
                    <asp:ListItem Text="Enviado" />
                    <asp:ListItem Text="Cancelado" />
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />

        <asp:ButtonField Text="Ver"
            ButtonType="Button"
            CommandName="VerDetalle"
            ControlStyle-CssClass="btn btn-sm btn-info" />
    </Columns>
</asp:GridView>

</div>

</div>

</asp:Content>
