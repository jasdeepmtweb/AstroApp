<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanditDetails.aspx.cs" Inherits="AstroApp.PanditDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper">
        <div class="pt-3"></div>
        <div class="container">
            <div class="card mb-2 shadow-sm">
                <div class="card-body">
                    <div class="pandit-list-box">
                        
                        <div class="row">
                            <div class="col-4">
                                <a>
                                    <asp:Image ID="ImgAstrologer" CssClass="pandit-img" runat="server" />
                                    </a>
                            </div>
                            <div class="col-8">
                                <h5>
                                    <asp:Literal ID="lblName" runat="server"></asp:Literal>
                                    <asp:Image ID="ImgVerfied" Width="20px" runat="server" />
                                   </h5>
                                <p><asp:Literal ID="lblSpecializations" runat="server"></asp:Literal> </p>
                                <p><asp:Literal ID="lblLanguageKnown" runat="server"></asp:Literal></p>
                                <div class="d-flex justify-content-between">
                                    <div>

                                        <p>Exp: <asp:Literal ID="lblExp" runat="server"></asp:Literal> Years</p>
                                        <%--<p class="fs-6"><b>₹ 33</b>/min</p>--%>
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="LinkChat" OnClick="LinkChat_Click" CssClass="btn m-1 btn-outline-success px-4 rounded-3" runat="server">Chat</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mb-2 shadow-sm">
                <div class="card-body">
                    <p><asp:Literal ID="lblBio" runat="server"></asp:Literal></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
