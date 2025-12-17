<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="AgregarEditarUsuario.aspx.cs" Inherits="e_commerce.AgregarEditarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
    <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
        <asp:ListItem Value="Admin">Admin</asp:ListItem>
        <asp:ListItem Value="Cliente">Cliente</asp:ListItem>
    </asp:DropDownList>
    <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" />

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />

</asp:Content>
