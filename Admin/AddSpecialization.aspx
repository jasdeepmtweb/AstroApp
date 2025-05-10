<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="AddSpecialization.aspx.cs" Inherits="AstroApp.Admin.AddSpecialization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h4 class="card-title">Add Specialization</h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body pt-0">
                            <div class="mb-3">
                                <label for="exampleInputPassword1" class="form-label">
                                    Specialization
                                    <asp:RequiredFieldValidator ControlToValidate="txtSpecialization" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ForeColor="Red" Display="Dynamic" ValidationGroup="a"></asp:RequiredFieldValidator>
                                </label>
                                <asp:TextBox ID="txtSpecialization" CssClass="form-control" placeholder="Enter Specialization" ValidationGroup="a" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="BtnSubmit" CssClass="btn btn-primary" OnClick="BtnSubmit_Click" ValidationGroup="a" runat="server" Text="Submit" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6 col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h4 class="card-title">List</h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body pt-0">
                            <div class="table-responsive">
                                <asp:GridView ID="GridSpecialization" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button ID="BtnDelete" runat="server" CommandArgument='<%# Eval("category_id") %>' OnClick="BtnDelete_Click" Text="Delete" class="btn btn-danger btn-sm" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="name" HeaderText="Specialization" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
