<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AstrologerRegistration.aspx.cs" Inherits="AstroApp.AstrologerRegistration" %>

<%@ Register Src="~/CustomLoader.ascx" TagName="CustomLoader" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="mobile-web-app-capable" content="yes" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, viewport-fit=cover" />
    <title>Astrologer Registration</title>
    <link rel="stylesheet" href="style.css" />
    <link rel="stylesheet" href="css/custom-loader.css" />
    <link rel="manifest" href="manifest.json" />
    <style>
        input[type='checkbox'] {
            margin-right: 2px;
        }

        label {
            margin-right: 8px;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.min.css" rel="stylesheet" />
    <%-- <style>
    .swal2-container.swal2-center > .swal2-popup{
        font-size: 16px;
    }
    .swal2-styled.swal2-confirm{
        width:auto!important;
    }
</style>--%>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.18/dist/sweetalert2.all.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="login-back-button">
            <a href="AstrologerLogin.aspx">
                <i class="bi bi-arrow-left-short"></i>
            </a>
        </div>

        <div class="login-wrapper d-flex align-items-center justify-content-center">
            <div class="custom-container">
                <div class="text-center px-4">
                    <img class="login-intro-img" src="img/login-img.png" alt="" />
                </div>
                <div class="register-form mt-4">
                    <h6 class="mb-3 text-center">Astrologer Registeration</h6>

                    <div class="form-group position-relative text-start mb-3">
                        <asp:FileUpload ID="FileProfilePic" accept=".jpg,.jpeg,.png" CssClass="form-control" runat="server" />
                    </div>
                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtName" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Name Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:TextBox CssClass="form-control" ValidationGroup="ab" placeholder="Name" ID="txtName" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtEmailId" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Email Id Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:TextBox CssClass="form-control" placeholder="Email Id" ID="txtEmailId" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Mobile No. Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:TextBox CssClass="form-control" placeholder="Mobile No." ID="txtMobileNo" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group position-relative text-start mb-3">
                        <asp:RequiredFieldValidator ControlToValidate="txtExperienceYears" ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Exp. Required" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="txtExperienceYears" Type="Integer" Operator="DataTypeCheck" ID="RequiredFieldValidator45" runat="server" ErrorMessage="Enter Only Numbers" Display="Dynamic" ForeColor="Red" ValidationGroup="ab"></asp:CompareValidator>
                        <asp:TextBox CssClass="form-control" placeholder="Experience(In Years)" ID="txtExperienceYears" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group text-start mb-3">
                        <p class="mb-2">Specialization</p>
                        <asp:CheckBoxList ID="CheckSpecialization" RepeatDirection="Horizontal" RepeatColumns="2" runat="server">
                        </asp:CheckBoxList>
                    </div>

                    <div class="form-group text-start mb-3">
                        <p class="mb-2">Languages Known</p>
                        <asp:CheckBoxList ID="CheckListLanguages" RepeatDirection="Horizontal" CssClass="form-control" runat="server">
                            <asp:ListItem Text="Hindi" Value="Hindi" />
                            <asp:ListItem Text="English" Value="English" />
                            <asp:ListItem Text="Punjabi" Value="Punjabi" />
                        </asp:CheckBoxList>
                    </div>

                    <div class="mb-3" id="pswmeter"></div>

                    <%--<div class="form-check mb-3">
                        <input class="form-check-input" id="checkedCheckbox" type="checkbox" value="" checked>
                        <label class="form-check-label text-muted fw-normal" for="checkedCheckbox">
                            I agree with the terms &amp;
              policy.</label>
                    </div>--%>
                    <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" ValidationGroup="ab" CssClass="btn btn-primary w-100" runat="server" Text="Sign Up" />
                </div>

                 <div class="login-meta-data text-center">
                    <p class="mt-3 mb-0">Already have an account? <a class="stretched-link" href="AstrologerLogin.aspx">Login</a></p>
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
