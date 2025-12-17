<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="e_commerce.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .registro-container { max-width: 400px; margin: 50px auto; padding: 20px; border: 1px solid #ccc; border-radius: 8px; background: #f9f9f9; }
        .registro-container h2 { text-align: center; margin-bottom: 20px; }
        .registro-container label { display: block; margin-top: 10px; }
        .registro-container input[type=text], .registro-container input[type=password] { width: 100%; padding: 8px; margin-top: 5px; }
        .registro-container button { margin-top: 20px; width: 100%; padding: 10px; background-color: #28a745; color: white; border: none; border-radius: 5px; }
        .registro-container .mensaje { margin-top: 10px; color: red; text-align: center; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card shadow p-4" style="max-width: 400px; width: 100%;">
            <h2 class="text-center mb-4">Registro de Usuario</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-center d-block mb-3"></asp:Label>

            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Nombre completo"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="ejemplo@correo.com"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTelefono" runat="server" Text="Telefono" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" Placeholder="+54 4123 4564"></asp:TextBox>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="••••••"></asp:TextBox>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success w-100" OnClick="btnRegistrar_Click" />

            <div class="mt-3 text-center">
                <a href="Login.aspx" class="link-primary">¿Ya tienes cuenta? Inicia sesión</a>
            </div>
        </div>
    </div>
</asp:Content>
