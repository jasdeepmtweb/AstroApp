<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="AstroApp.AdminLogin" %>

<%@ Register Src="~/CustomLoader.ascx" TagName="CustomLoader" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="mobile-web-app-capable" content="yes" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <title>Admin Login</title>
    <link rel="stylesheet" href="style.css" />
    <link rel="stylesheet" href="css/custom-loader.css" />
    <link rel="manifest" href="manifest.json" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.min.css" rel="stylesheet" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:CustomLoader runat="server" ID="LoaderReg" Visible="false" />

        <%--<div id="loader" runat="server" visible="false" style="position: fixed; height: 100vh; width: 100%; z-index: 999; display: flex; align-items: center; justify-content: center; background-color: rgba(255,255,255,0.5)">
            <asp:Image ID="ImgLoader" Height="100px" Width="100px" ImageUrl="~/loader.gif" runat="server" />
        </div>--%>

       <%-- <div class="login-back-button">
            <a href="Index.aspx">
                <i class="bi bi-arrow-left-short"></i>
            </a>
        </div>--%>

        <div class="login-wrapper d-flex align-items-center justify-content-center">
            <div class="custom-container">
                <div class="text-center px-4">
                    <img class="login-intro-img" src="img/login-img.png" alt="" />
                </div>
                <div class="register-form mt-4">
                    <h6 class="mb-3 text-center">Admin Login</h6>

                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtUsername" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Username Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:TextBox CssClass="form-control" ValidationGroup="ab" placeholder="Username" ID="txtUsername" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtPassword" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Password Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:TextBox CssClass="form-control" TextMode="Password" placeholder="Password" ID="txtPassword" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3" id="pswmeter"></div>

                    <asp:Button ID="BtnLogin" OnClick="BtnLogin_Click" ValidationGroup="ab" CssClass="btn btn-primary w-100" runat="server" Text="Login" />
                </div>
            </div>
        </div>
    </form>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/internet-status.js"></script>
    <script src="js/dark-rtl.js"></script>
    <script src="js/pswmeter.js"></script>
    <script src="js/active.js"></script>
    <script src="js/pwa.js"></script>
</body>
</html>
