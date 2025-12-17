<%@ Page Title="" Language="C#" MasterPageFile="~/MenuWeb.Master" AutoEventWireup="true" CodeBehind="MisDirecciones.aspx.cs" Inherits="e_commerce.MisDirecciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Mis Direcciones</h2>

        <!-- Mensaje de alerta -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

        <div class="table-responsive mt-3">
            <asp:GridView ID="gvDirecciones" runat="server" AutoGenerateColumns="False" DataKeyNames="IdDireccion"
                OnRowCommand="gvDirecciones_RowCommand"
                CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="Calle" HeaderText="Calle" />
                    <asp:BoundField DataField="Numero" HeaderText="Número" />
                    <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                    <asp:BoundField DataField="CodigoPostal" HeaderText="C.P." />
                    <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdDireccion") %>' CssClass="btn btn-sm btn-primary me-1" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("IdDireccion") %>' CssClass="btn btn-sm btn-danger"
                                        OnClientClick="return confirm('¿Desea eliminar esta dirección?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="mt-3">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nueva Dirección" OnClick="btnAgregar_Click" CssClass="btn btn-success" />
        </div>
    </div>
</asp:Content>
