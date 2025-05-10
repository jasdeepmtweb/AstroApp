<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AstroApp.Admin.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid mt-3">
    <div class="row justify-content-center">
        <div class="col-md-6 col-lg-4">
            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h4 class="card-title">Change Password</h4>
                        </div>
                    </div>
                </div>
                <div class="card-body pt-0">
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">
                            Old Password
                            <asp:RequiredFieldValidator ControlToValidate="txtOldPassword" ValidationGroup="a" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                Operator="Equal" Type="String" ErrorMessage="Old password is incorrect" Display="Dynamic" ForeColor="Red" ValidationGroup="a"></asp:CompareValidator>
                        </label>
                       <asp:TextBox ID="txtOldPassword" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">
                            New Password
                            <asp:RequiredFieldValidator ControlToValidate="txtNewPassword" Display="Dynamic" ForeColor="Red" ValidationGroup="a" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                        </label>
                        <asp:TextBox ID="txtNewPassword" CssClass="form-control" ValidationGroup="a" runat="server"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="exampleInputEmail1" class="form-label">
                            Confirm New Password
                             <asp:RequiredFieldValidator ControlToValidate="txtConfirmNewPassword" ValidationGroup="a" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Required" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmNewPassword" ControlToCompare="txtNewPassword"
                            Operator="Equal" Type="String" ErrorMessage="Confirm password is incorrect" Display="Dynamic" ForeColor="Red" ValidationGroup="a"></asp:CompareValidator>
                        </label>
                        <asp:TextBox ID="txtConfirmNewPassword" CssClass="form-control" ValidationGroup="a" runat="server"></asp:TextBox>
                    </div>
                   
                    <asp:Button ID="BtnSubmit" ValidationGroup="a" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" runat="server" Text="Submit" />
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
