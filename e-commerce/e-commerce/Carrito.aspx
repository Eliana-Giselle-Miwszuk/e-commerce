<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="e_commerce.CarritoPage" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container mt-5">
        <h2>Tu Carrito</h2>
        <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCarrito_RowCommand" CssClass="table table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Producto" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                <asp:ButtonField Text="Quitar" CommandName="Quitar" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <h4 class="mt-3"><asp:Label ID="lblTotal" runat="server"></asp:Label></h4>

        <div class="mt-3">
            <a href="Productos.aspx" class="btn btn-primary">Seguir Comprando</a>
            <asp:Button ID="btnLimpiar" runat="server" Text="Vaciar Carrito" CssClass="btn btn-danger ms-2" OnClick="btnLimpiar_Click" />
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar Compra" CssClass="btn btn-success ms-2" OnClick="btnFinalizar_Click" />
        </div>


    </div>
</asp:Content>
