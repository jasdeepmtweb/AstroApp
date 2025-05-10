<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AstroApp.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="mobile-web-app-capable" content="yes" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <title>App</title>
    <link rel="stylesheet" href="style.css" />
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrapper d-flex align-items-center justify-content-center">
            <div class="custom-container">
                <div class="text-center">
                    <img class="mx-auto mb-4 d-block" src="img/login-img.png" alt="" />
                    <h6>Astro App</h6>
                    <p>First Chat with App is FREE!</p>
                </div>

                <div class="otp-form mt-4">
                    <div>
                        <div class="input-group">
                            <asp:DropDownList ID="ddlDialingCode" CssClass="input-group-text form-select" Style="max-width: 110px;" runat="server"></asp:DropDownList>

                            <asp:TextBox ID="txtMobileNo" TextMode="Phone" ValidationGroup="av" CssClass="form-control" placeholder="Mobile number" runat="server"></asp:TextBox>

                            <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Mobile No. Required" ForeColor="Red" Display="None" ValidationGroup="av"></asp:RequiredFieldValidator>

                        </div>

                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="text-center" runat="server" ValidationGroup="av" ShowSummary="true" ForeColor="Red" DisplayMode="SingleParagraph" />

                        <asp:Button ID="BtnGetOTP" CssClass="btn btn-warning w-100 mt-3" ValidationGroup="av" OnClick="BtnGetOTP_Click" runat="server" Text="GET OTP" />
                    </div>
                </div>

                <div class="login-meta-data px-4 text-center">
                    <p class="mt-3 mb-0 ">
                        By providing my phone number, I hereby agree the
          <a class="stretched-link" href="#">Term of Services</a>
                        and
          <a class="stretched-link" href="#">Privacy Policy.</a>
                    </p>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
