<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardAdmin/MenuAdmin.Master" 
    AutoEventWireup="true" CodeBehind="Direcciones.aspx.cs" 
    Inherits="e_commerce.DashboardAdmin.Direcciones.Direcciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-grid {
            border: none;
            border-radius: 10px;
            box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.1);
        }
        .table-header-custom {
            background-color: #4e73df;
            color: white;
        }
        .btn-action {
            min-width: 80px;
            margin: 2px;
        }
        .empty-grid {
            padding: 2rem;
            text-align: center;
            color: #6c757d;
            font-size: 1.1rem;
        }
        .badge-tipo {
            font-size: 0.8rem;
            padding: 3px 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid py-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="h4 mb-0 text-gray-800 font-weight-bold">
                <i class="fas fa-address-book fa-fw mr-2"></i>Listado de Direcciones
            </h2>
            <asp:LinkButton ID="LktAgregarDireccion" runat="server" 
                PostBackUrl="~/Vistas/Direcciones/AgregarDireccion.aspx" 
                CssClass="btn btn-primary btn-icon-split">
                <span class="icon text-white-50">
                    <i class="fas fa-plus"></i>
                </span>
                <span class="text">Nueva Dirección</span>
            </asp:LinkButton>
        </div>

        <!-- Mensajes de error/éxito -->
        <asp:Label ID="lblMensaje" runat="server" CssClass="alert alert-danger d-block" 
            Visible="false"></asp:Label>

        <div class="card shadow mb-4 card-grid">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gdDirecciones" runat="server" 
                        AutoGenerateColumns="False" 
                        OnRowCommand="gdDirecciones_RowCommand" 
                        DataKeyNames="IDDireccion" 
                        CssClass="table table-hover"
                        AllowPaging="True" 
                        OnPageIndexChanging="gdDirecciones_PageIndexChanging" 
                        PageSize="5"
                        HeaderStyle-CssClass="table-header-custom"
                        GridLines="None">
                        
                        <Columns>
                            <asp:BoundField DataField="IDDireccion" HeaderText="ID" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="IDUsuario" HeaderText="ID Usuario" />
                            <asp:BoundField DataField="Calle" HeaderText="Calle" />
                            <asp:BoundField DataField="Numero" HeaderText="Número" />
                            <asp:BoundField DataField="Localidad" HeaderText="Localidad" />
                            <asp:BoundField DataField="Provincia" HeaderText="Provincia" />
                            <asp:BoundField DataField="CodigoPostal" HeaderText="CP" />
                            <asp:BoundField DataField="Observaciones" HeaderText="Observaciones" />
                            
                            <asp:TemplateField HeaderText="Tipo">
                                <ItemTemplate>
                                    <span class='badge <%# Eval("TipoDireccion").ToString() == "Casa" ? "badge-success" : "badge-info" %> badge-tipo'>
                                        <%# Eval("TipoDireccion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Principal" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Visible='<%# Convert.ToBoolean(Eval("EsPrincipal")) %>'
                                        CssClass="badge badge-pill badge-warning"
                                        Text="PRINCIPAL">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="FechaRegistro" HeaderText="Registro" 
                                DataFormatString="{0:dd/MM/yyyy}" />
                            
                            <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-nowrap">
                                <ItemTemplate>
                                    <asp:LinkButton ID="BtnEditar" runat="server"
                                        CommandName="Editar"
                                        CommandArgument='<%# Eval("IDDireccion") %>'
                                        CssClass="btn btn-warning btn-sm btn-action"
                                        ToolTip="Editar">
                                        <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                    
                                    <asp:LinkButton ID="BtnEliminar" runat="server"
                                        CommandName="Eliminar"
                                        CommandArgument='<%# Eval("IDDireccion") %>'
                                        CssClass="btn btn-danger btn-sm btn-action"
                                        OnClientClick="return confirm('¿Está seguro que desea eliminar esta dirección?');"
                                        ToolTip="Eliminar">
                                        <i class="fas fa-trash-alt"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <EmptyDataTemplate>
                            <div class="empty-grid">
                                <i class="fas fa-info-circle fa-2x mb-3"></i>
                                <h5 class="text-gray-800">No se encontraron direcciones registradas</h5>
                                <p class="mb-0">Utilice el botón "Nueva Dirección" para agregar una</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

</asp:Content>