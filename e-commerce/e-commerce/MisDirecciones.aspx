<%@ Page Title="Mis Direcciones"
    Language="C#"
    MasterPageFile="~/MenuWeb.Master"
    AutoEventWireup="true"
    CodeBehind="MisDirecciones.aspx.cs"
    Inherits="e_commerce.MisDirecciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Mis Direcciones</h2>

        <div class="mt-3" style="text-align:right;">
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nueva Dirección"
                OnClick="btnAgregar_Click"
                CssClass="btn btn-success" />
        </div>

        <asp:Label ID="lblMensaje" runat="server" CssClass="alert" Visible="false"></asp:Label>

        <div class="table-responsive mt-3">
            <asp:GridView ID="gvDirecciones" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="IdDireccion"
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
                            <asp:Button ID="btnEditar" runat="server" Text="Editar"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("IdDireccion") %>'
                                CssClass="btn btn-sm btn-primary me-1" />

                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdDireccion") %>'
                                CssClass="btn btn-sm btn-danger"
                                OnClientClick='<%# "mostrarModalEliminar(" + Eval("IdDireccion") + "); return false;" %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>


    </div>

    <!-- Modal Confirmar Eliminación -->
    <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 shadow">
                <div class="modal-header">
                    <h5 class="modal-title">Confirmar eliminación</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    ¿Estás seguro de que deseas eliminar esta dirección?
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarEliminar" runat="server"
                        Text="Sí, eliminar"
                        CssClass="btn btn-danger"
                        OnClick="btnConfirmarEliminar_Click" />
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <script>
        function mostrarModalEliminar(id) {
            // Guardamos el id en un campo hidden
            document.getElementById('<%= hfEliminarId.ClientID %>').value = id;
            var modal = new bootstrap.Modal(document.getElementById('modalEliminar'));
            modal.show();
        }
    </script>

    <asp:HiddenField ID="hfEliminarId" runat="server" />
</asp:Content>
