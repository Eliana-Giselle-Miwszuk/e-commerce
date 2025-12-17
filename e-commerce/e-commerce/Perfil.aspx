<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="e_commerce.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="card shadow-sm mt-3">
    <div class="card-body">
        <h2 class="card-title mb-4">Mi Perfil</h2>

        <asp:Label ID="lblMensaje" runat="server" ForeColor="red"></asp:Label>

        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" Placeholder="Nombre"></asp:TextBox>
        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" Placeholder="Email"></asp:TextBox>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control mb-3" Placeholder="Teléfono"></asp:TextBox>
        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Contraseña"></asp:TextBox>
        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" Placeholder="Confirmar Contraseña"></asp:TextBox>

        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
    </div>
</div>

</asp:Content>
