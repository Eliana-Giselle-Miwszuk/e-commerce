<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="e_commerce.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero {
            background: linear-gradient(120deg, #3483fa, #1d4ed8);
            color: white;
            border-radius: 12px;
        }
        .product-card img {
            height: 180px;
            object-fit: cover;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Hero -->
    <div class="hero p-5 mb-5 shadow">
        <h1 class="fw-bold">Bienvenido a MiTienda</h1>
        <p class="fs-5">Todo lo que necesitás, al mejor precio</p>
        <a class="btn btn-light btn-lg" href="Productos.aspx">Ver ofertas</a>
    </div>

    <!-- Productos destacados -->
    <h3 class="mb-4">Productos destacados</h3>

    <asp:Repeater ID="rptProductosDestacados" runat="server">
        <HeaderTemplate>
            <div class="row g-4">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col-md-4">
                <div class="card product-card shadow-sm">
                    <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" />
                    <div class="card-body">
                        <h5><%# Eval("Nombre") %></h5>
                        <p class="text-success fw-bold fs-5">$<%# Eval("Precio") %></p>
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar al carrito"
                            CssClass="btn btn-primary w-100"
                            CommandArgument='<%# Eval("IdProducto") %>'
                            OnClick="btnAgregar_Click" />
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
