<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfileSetting.aspx.cs" Inherits="AstroApp.ProfileSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type='radio'] {
            margin-right: 2px;
        }

        label {
            margin-right: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper py-3">
        <div class="container">
            <div class="card user-info-card mb-3">
                <div class="card-body d-flex align-items-center">
                    <div class="user-profile me-3">
                        <asp:Image ID="ImgUser" ImageUrl="~/user.png" runat="server" style="height: 76px; width: 76px; object-fit: cover;" />
                        <i class="bi bi-pencil" style="pointer-events: none;"></i>
                        <div>
                            <asp:FileUpload ID="FilePic" CssClass="form-control" runat="server" />
                        </div>
                    </div>
                    <div class="user-info">
                        <div class="d-flex align-items-center">
                            <h5 class="mb-1">
                                <asp:Literal ID="lblName" runat="server"></asp:Literal>
                            </h5>

                        </div>
                        <p class="mb-0">
                            <asp:Literal ID="lblMobileNo" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
            </div>

            <div class="card user-data-card">
                <div class="card-body">
                    <div action="#">
                        <div class="form-group mb-3">
                            <label class="form-label" for="Username">
                                Name
                                <asp:RequiredFieldValidator ControlToValidate="txtName" ID="FieldValidator1" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtName" placeholder="Enter Name" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group mb-3">
                            <label class="form-label me-4" for="fullname">Gender </label>
                            <div class="form-check-inline">
                                <asp:RadioButtonList ID="RadioListGender" RepeatDirection="Horizontal" runat="server">
                                    <asp:ListItem Text="Male" Value="Male" Selected="True" />
                                    <asp:ListItem Text="Female" Value="Female" />
                                </asp:RadioButtonList>
                            </div>
                        </div>

                        <div class="form-group mb-3">
                            <label class="form-label" for="email">
                                Email Id
                                <asp:RequiredFieldValidator ControlToValidate="txtEmailId" ID="FieldValidator4" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtEmailId" placeholder="Enter Email Id" ValidationGroup="a" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Date Of Birth
                                <asp:RequiredFieldValidator ControlToValidate="txtDateOfBirth" ID="FieldValidator11" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtDateOfBirth" placeholder="Enter DOB" ValidationGroup="a" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Place of Birth
                                <asp:RequiredFieldValidator ControlToValidate="txtPlaceOfBirth" ID="FieldValidator15" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtPlaceOfBirth" placeholder="Enter Place of Birth" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group mb-3">
                            <label class="form-label" for="address">
                                Birth Time
                                <asp:RequiredFieldValidator ControlToValidate="txtBirthTime" ID="FieldValidator16" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtBirthTime" placeholder="Enter Birth Time" ValidationGroup="a" TextMode="Time" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Address
        <asp:RequiredFieldValidator ControlToValidate="txtAddress" ID="FieldValidator17" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtAddress" placeholder="Enter Address" TextMode="MultiLine" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                State
        <asp:RequiredFieldValidator ControlToValidate="txtState" ID="FieldValidator27" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtState" placeholder="Enter State" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:Button ID="BtnSubmit" CssClass="btn btn-warning w-100" OnClick="BtnSubmit_Click" ValidationGroup="a" runat="server" Text="Submit" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
