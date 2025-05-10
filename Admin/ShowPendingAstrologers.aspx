<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Site.Master" AutoEventWireup="true" CodeBehind="ShowPendingAstrologers.aspx.cs" Inherits="AstroApp.Admin.ShowPendingAstrologers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box d-md-flex justify-content-md-between align-items-center">
                    <h4 class="page-title">Pending Astrologers</h4>
                </div>
            </div>
        </div>
        <asp:Repeater ID="rptAstrolgers" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img class="img-fluid bg-light-alt rounded-circle mx-auto" style="height: 120px; width: 120px;" src="<%# Eval("profile_picture") %>" alt="">
                    <div class="card-header">
                        <h4 class="card-title"><%# Eval("name") %></h4>
                    </div>
                    <div class="card-body pt-0">
                        <p class="card-text">Email Id: <%# Eval("email") %></p>
                        <p class="card-text">Contact No.: <%# Eval("phone") %></p>
                        <p class="card-text">Experience: <%# Eval("experience_years") %></p>
                        <p class="card-text">Specialization: <%# Eval("specializations") %></p>
                        <p class="card-text">Languages Known: <%# Eval("languages_known") %></p>
                        <div class="text-center">
                            <asp:Button ID="BtnApprove" runat="server" CommandArgument='<%# Eval("astrologer_id") %>' OnClick="BtnApprove_Click" CssClass="btn btn-outline-success btn-sm" Text="Approve" />
                            <asp:Button ID="BtnReject" runat="server" CommandArgument='<%# Eval("astrologer_id") %>' OnClick="BtnReject_Click" CssClass="btn btn-outline-danger btn-sm" Text="Reject" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
