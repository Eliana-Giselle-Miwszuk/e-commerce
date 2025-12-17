<%@ Page Title="Login" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="e_commerce.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container { max-width: 400px; margin: 50px auto; padding: 20px; border: 1px solid #ccc; border-radius: 8px; background: #f9f9f9; }
        .login-container h2 { text-align: center; margin-bottom: 20px; }
        .login-container label { display: block; margin-top: 10px; }
        .login-container input[type=text], .login-container input[type=password] { width: 100%; padding: 8px; margin-top: 5px; }
        .login-container button { margin-top: 20px; width: 100%; padding: 10px; background-color: #007bff; color: white; border: none; border-radius: 5px; }
        .login-container .mensaje { margin-top: 10px; color: red; text-align: center; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card shadow p-4" style="max-width: 400px; width: 100%;">
            <h2 class="text-center mb-4">Iniciar Sesión</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger text-center d-block mb-3"></asp:Label>

            <div class="mb-3">
                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="ejemplo@correo.com"></asp:TextBox>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" Placeholder="••••••"></asp:TextBox>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />


            <div class="mt-3 text-center">
                <a href="Registro.aspx" class="link-primary">¿No tienes cuenta? Regístrate</a>
            </div>
        </div>
    </div>
</asp:Content>
