<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtpConfirm.aspx.cs" Inherits="AstroApp.OtpConfirm" %>

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
    <style>
        /* Remove spinner arrows */
        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-back-button ">
            <a href="login.aspx">
                <i class="bi bi-arrow-left-short"></i>
            </a>
        </div>
        <div class="login-wrapper d-flex align-items-center justify-content-center">
            <div class="custom-container">
                <div class="text-center">
                    <h3>Verify Phone Number</h3>
                    <p class="mb-4">
                        Enter the OTP code sent to <strong>
                            <asp:Literal ID="lblMobileNo" runat="server"></asp:Literal>
                        </strong>
                    </p>
                </div>
                <asp:Literal ID="lblOTP" Visible="false" runat="server"></asp:Literal>
                <div class="otp-verify-form mt-4">
                    <div action="home.html">
                        <div class="input-group mb-3 otp-input-group">
                            <asp:RequiredFieldValidator ControlToValidate="txtOTP1" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required" ForeColor="Red" Display="None" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="txtOTP2" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ForeColor="Red" Display="None" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="txtOTP3" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required" ForeColor="Red" Display="None" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ControlToValidate="txtOTP4" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required" ForeColor="Red" Display="None" ValidationGroup="a"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOTP1" ValidationGroup="a" TextMode="Number" CssClass="form-control otp-box" MaxLength="1" placeholder="-" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtOTP2" ValidationGroup="a" TextMode="Number" CssClass="form-control otp-box" MaxLength="1" placeholder="-" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtOTP3" ValidationGroup="a" TextMode="Number" CssClass="form-control otp-box" MaxLength="1" placeholder="-" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtOTP4" ValidationGroup="a" TextMode="Number" CssClass="form-control otp-box" MaxLength="1" placeholder="-" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="BtnVerify" CssClass="btn btn-warning w-100" ValidationGroup="a" runat="server" OnClick="BtnVerify_Click" Text="Verify &amp; Proceed" />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        const inputs = document.querySelectorAll(".otp-input-group .form-control");

        inputs.forEach((input, index) => {
            input.addEventListener("input", function () {
                if (this.value.length === 1 && index < inputs.length - 1) {
                    inputs[index + 1].focus();
                }
            });

            input.addEventListener("keydown", function (e) {
                // Move to the previous input on Backspace if the input is empty
                if (e.key === "Backspace" && this.value === "" && index > 0) {
                    inputs[index - 1].focus();
                }
            });
        });
    });
</script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const otpInputs = document.querySelectorAll(".otp-box");

            otpInputs.forEach(input => {
                input.addEventListener("input", function () {
                    // Allow only a single digit (0-9)
                    if (!/^\d$/.test(this.value)) {
                        this.value = ""; // Clear invalid input
                    }
                });
            });
        });
</script>
</html>
