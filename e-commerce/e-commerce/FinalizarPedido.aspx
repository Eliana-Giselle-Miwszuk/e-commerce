<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="FinalizarPedido.aspx.cs" Inherits="e_commerce.FinalizarPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pedido-container { max-width: 700px; margin: 50px auto; padding: 20px; background: #f9f9f9; border-radius: 8px; }
        .pedido-container h2, .pedido-container h3 { text-align: center; margin-bottom: 20px; }
        .pedido-container .btn { width: 100%; margin-top: 20px; }
        .pedido-container label { font-weight: bold; }
        .pedido-container select { width: 100%; padding: 5px; margin-bottom: 15px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pedido-container">
        <h2>Finalizar Pedido</h2>

        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>

        <h3>Resumen del carrito</h3>
        <asp:GridView ID="gvResumenCarrito" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio Unitario" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <%# ((decimal)Eval("Subtotal")).ToString("C") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblTotalPedido" runat="server" Text="Total: $0"></asp:Label>

        <h3>Seleccionar Dirección de Entrega</h3>
        <asp:DropDownList ID="ddlDirecciones" runat="server"></asp:DropDownList>
        <asp:HyperLink ID="lnkNuevaDireccion" runat="server" NavigateUrl="AgregarEditarDireccion.aspx">Agregar Nueva Dirección</asp:HyperLink>

        <h3>Seleccionar Forma de Entrega</h3>
        <asp:DropDownList ID="ddlFormasEntrega" runat="server"></asp:DropDownList>

        <asp:Button ID="btnConfirmarPedido" runat="server" Text="Confirmar Pedido" CssClass="btn btn-primary" OnClick="btnConfirmarPedido_Click" />
    </div>
</asp:Content>
