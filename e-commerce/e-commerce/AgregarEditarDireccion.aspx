<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="AgregarEditarDireccion.aspx.cs" Inherits="e_commerce.AgregarEditarDireccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2><asp:Literal ID="ltTitulo" runat="server" Text="Nueva Dirección"></asp:Literal></h2>

    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label><br />

    <table>
        <tr>
            <td>Calle:</td>
            <td><asp:TextBox ID="txtCalle" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Número:</td>
            <td><asp:TextBox ID="txtNumero" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Localidad:</td>
            <td><asp:TextBox ID="txtLocalidad" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Código Postal:</td>
            <td><asp:TextBox ID="txtCodigoPostal" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Observaciones:</td>
            <td><asp:TextBox ID="txtObservaciones" runat="server"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
</asp:Content>
