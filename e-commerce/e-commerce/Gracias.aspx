<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Gracias.aspx.cs" Inherits="e_commerce.Gracias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .gracias-container { max-width: 600px; margin: 50px auto; padding: 30px; background: #f9f9f9; border-radius: 8px; text-align: center; }
        .gracias-container h2 { margin-bottom: 20px; }
        .gracias-container a { margin-top: 20px; display: inline-block; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <div class="gracias-container">
        <h2>¡Gracias por tu compra, <asp:Label ID="lblNombreUsuario" runat="server"></asp:Label>!</h2>
        <p>Tu pedido ha sido registrado correctamente.</p>
        <a href="Productos.aspx" class="btn btn-primary">Seguir Comprando</a>
    </div>
</asp:Content>
