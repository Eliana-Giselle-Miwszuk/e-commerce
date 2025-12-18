<%@ Page Title="Agregar / Editar Dirección"
    Language="C#"
    MasterPageFile="~/MenuWeb.Master"
    AutoEventWireup="true"
    CodeBehind="AgregarEditarDireccion.aspx.cs"
    Inherits="e_commerce.AgregarEditarDireccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-control {
            border-radius: 10px;
            padding: 10px 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <div class="col-md-8 col-lg-6">

                <h2 class="mb-4 text-center">
                    <asp:Literal ID="ltTitulo" runat="server" Text="Nueva Dirección"></asp:Literal>
                </h2>

                <asp:Label ID="lblMensaje" runat="server"></asp:Label>

                <div class="card shadow-lg border-0 rounded-4">
                    <div class="card-body p-4">

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Calle</label>
                            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Número</label>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Localidad</label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Código Postal</label>
                            <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" />
                        </div>

                        <div class="mb-3">
                            <label class="form-label fw-semibold">Observaciones</label>
                            <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                        </div>

                        <div class="d-flex justify-content-end gap-2">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                                CssClass="btn btn-primary px-4"
                                OnClientClick="mostrarModalConfirmar(); return false;" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar"
                                CssClass="btn btn-outline-secondary px-4"
                                OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- Modal Confirmar Guardar -->
    <div class="modal fade" id="modalConfirmar" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content rounded-4 shadow">

                <div class="modal-header">
                    <h5 class="modal-title fw-semibold">Confirmar acción</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">
                    ¿Estás seguro de que deseas guardar la dirección?
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnConfirmarGuardar" runat="server"
                        Text="Sí, guardar"
                        CssClass="btn btn-success px-4"
                        OnClick="btnConfirmarGuardar_Click" />
                    <button type="button" class="btn btn-secondary px-4" data-bs-dismiss="modal">
                        Cancelar
                    </button>
                </div>

            </div>
        </div>
    </div>

    <script>
        function mostrarModalConfirmar() {
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmar'));
            modal.show();
        }
    </script>
</asp:Content>
