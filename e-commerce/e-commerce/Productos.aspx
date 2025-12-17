<%@ Page Title="Productos" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="e_commerce.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .product-card img {
            height: 180px;
            object-fit: cover;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">

        <h2>Todos los productos</h2>

        <!-- Filtros -->
        <div class="row mb-4">
            <div class="col-md-6">
                <asp:TextBox ID="txtBusqueda" runat="server" CssClass="form-control" Placeholder="Buscar productos..." AutoPostBack="true" OnTextChanged="txtBusqueda_TextChanged" />
            </div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlCategorias" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
                    <asp:ListItem Text="Todas las categorías" Value="0" />
                </asp:DropDownList>
            </div>
        </div>

        <asp:Repeater ID="rptProductos" runat="server">
            <HeaderTemplate>
                <div class="row">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-4 mb-3">
                    <div class="card shadow-sm h-100">
                        <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" />
                        <div class="card-body d-flex flex-column">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text">$<%# Eval("Precio") %></p>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar al carrito" CssClass="btn btn-primary mt-auto" CommandArgument='<%# Eval("IdProducto") %>' OnClick="btnAgregar_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <a href="Carrito.aspx" class="btn btn-success mt-3">Ver Carrito</a>

    </div>
</asp:Content>
