<%@ Page Title="Agregar / Editar Producto"
    Language="C#"
    MasterPageFile="~/MenuWeb.Master"
    AutoEventWireup="true"
    CodeBehind="AgregarEditarProducto.aspx.cs"
    Inherits="e_commerce.AgregarEditarProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-control, .form-select {
            border-radius: 10px;
            padding: 10px 12px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">
    <div class="row justify-content-center">
        <div class="col-md-9 col-lg-7">

            <h2 class="mb-4 text-center">Agregar / Editar Producto</h2>

            <div class="card shadow-lg border-0 rounded-4">
                <div class="card-body p-4">

                    <div class="mb-3">
                        <label class="form-label fw-semibold">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold">Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server"
                            CssClass="form-control"
                            TextMode="MultiLine"
                            Rows="3" />
                    </div>

                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-semibold">Precio</label>
                            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                        </div>

                        <div class="col-md-6 mb-3">
                            <label class="form-label fw-semibold">Stock</label>
                            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold">URL Imagen</label>
                        <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" />
                    </div>

                    <div class="form-check form-switch mb-4">
                        <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" />
                        <label class="form-check-label ms-2">Producto activo</label>
                    </div>

                    <div class="d-flex justify-content-end gap-2">
                        <asp:Button ID="btnGuardar"
                            runat="server"
                            Text="Guardar"
                            CssClass="btn btn-primary px-4"
                            OnClientClick="mostrarModalConfirmar(); return false;" />

                        <asp:Button ID="btnCancelar"
                            runat="server"
                            Text="Cancelar"
                            CssClass="btn btn-outline-secondary px-4"
                            OnClick="btnCancelar_Click" />
                    </div>

                </div>
            </div>

        </div>
    </div>
</div>

<!-- ================= MODAL CONFIRMAR ================= -->
<div class="modal fade" id="modalConfirmar" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content rounded-4 shadow">

            <div class="modal-header">
                <h5 class="modal-title fw-semibold">Confirmar acción</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body">
                ¿Estás seguro de que deseas guardar los datos del producto?
            </div>

            <div class="modal-footer">
                <asp:Button ID="btnConfirmarGuardar"
                    runat="server"
                    Text="Sí, guardar"
                    CssClass="btn btn-success px-4"
                    OnClick="btnConfirmarGuardar_Click" />

                <button type="button"
                    class="btn btn-secondary px-4"
                    data-bs-dismiss="modal">
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
