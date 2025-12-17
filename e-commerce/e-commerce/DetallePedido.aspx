<%@ Page Title="Detalle Pedido" Language="C#" MasterPageFile="~/MenuWeb.Master"
    AutoEventWireup="true"
    CodeBehind="DetallePedido.aspx.cs"
    Inherits="e_commerce.DetallePedido" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3>Detalle del Pedido</h3>

<div class="card mb-3">
    <div class="card-body">
        <strong>Cliente:</strong> <asp:Label ID="lblCliente" runat="server" /><br />
        <strong>Email:</strong> <asp:Label ID="lblEmail" runat="server" /><br />
        <strong>Teléfono:</strong> <asp:Label ID="lblTelefono" runat="server" /><br />
        <strong>Dirección:</strong>
        <asp:Label ID="lblDireccion" runat="server" /><br />
        <strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" /><br />
        <strong>Estado:</strong> <asp:Label ID="lblEstado" runat="server" /><br />
        <strong>Total:</strong> <asp:Label ID="lblTotal" runat="server" />
    </div>
</div>

<asp:GridView ID="gvDetalle" runat="server"
    CssClass="table table-bordered"
    AutoGenerateColumns="False">

    <Columns>
        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
    </Columns>
</asp:GridView>

<asp:Button Text="Volver" runat="server"
    CssClass="btn btn-secondary"
    PostBackUrl="~/AdminPanel.aspx" />

</asp:Content>
