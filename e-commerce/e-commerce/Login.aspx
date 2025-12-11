<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="e_commerce.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login</title>
    <link href="Css/bootstrap.css" rel="stylesheet" />
    <style>
        .login-container {
            max-width: 400px;
            margin: 100px auto;
            padding: 20px;
        }

        .card {
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            border: none;
        }

        body {
            background-color: #f8f9fa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="login-container">
                <div class="card">
                    <div class="card-header bg-primary text-white text-center">
                        <h4>Iniciar Sesión</h4>
                    </div>
                    <div class="card-body">
                        <!-- Campo Email -->
                        <div class="mb-3">
                            <label for="txtEmail" class="form-label">Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                TextMode="Email" placeholder="admin@test.com" Required="true" />
                        </div>

                        <!-- Campo Contraseña -->
                        <div class="mb-3">
                            <label for="txtPassword" class="form-label">Contraseña</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"
                                TextMode="Password" placeholder="123" Required="true" />
                        </div>

                        <!-- Checkbox Recordarme -->
                        <div class="mb-3 form-check">
                            <asp:CheckBox ID="chkRecordar" runat="server" CssClass="form-check-input" />
                            <label class="form-check-label" for="chkRecordar">Recordar sesión</label>
                        </div>

                        <!-- Botón Login -->
                        <div class="d-grid">
                            <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión"
                                CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" />
                        </div>

                        <!-- Mensaje de error -->
                        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
                            <asp:Label ID="lblMensaje" runat="server" Text="" />
                        </asp:Panel>

                        <!-- Mensaje de éxito -->
                        <asp:Panel ID="pnlExito" runat="server" Visible="false" CssClass="alert alert-success mt-3">
                            <asp:Label ID="lblExito" runat="server" Text="" />
                        </asp:Panel>

                        <!-- Enlaces adicionales -->
                        <div class="text-center mt-3">
                            <a href="Registro.aspx" class="text-decoration-none">¿No tienes cuenta? Regístrate</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="Js/bootstrap.js"></script>
</body>
</html>
