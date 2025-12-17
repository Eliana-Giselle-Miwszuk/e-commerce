<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="AgregarEditarProducto.aspx.cs" Inherits="e_commerce.AgregarEditarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" />
    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
    <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" />
    <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />

</asp:Content>
