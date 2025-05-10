<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AstroApp.Index" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrapper">
        <div class="pt-3"></div>
        <div class="container">
            <asp:UpdatePanel runat="server" ID="up1">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="120000" OnTick="Timer1_Tick"></asp:Timer>
                    <asp:Repeater ID="rptAstrolgers" runat="server">
                        <ItemTemplate>
                            <div class="card mb-2 shadow-sm">
                                <div class="card-body">
                                    <div class="pandit-list-box">
                                        <asp:Image ID="ImgVerified" ImageUrl='<%# GetVerificationStatus(Eval("verification_status")) %>' CssClass="right-check-icon" runat="server" />
                                        <div class="row">
                                            <div class="col-4">
                                                <a href="PanditDetails.aspx?id=<%# Eval("astrologer_id") %>">
                                                    <img src="<%# Eval("profile_picture") %>" alt="profile" class="pandit-img" /></a>
                                            </div>
                                            <div class="col-8">
                                                <a href="PanditDetails.aspx?id=<%# Eval("astrologer_id") %>">
                                                    <h5><%# Eval("name") %> </h5>
                                                </a>
                                                <p><%# Eval("specializations") %> </p>
                                                <p><%# Eval("languages_known") %></p>
                                                <div class="d-flex justify-content-between">
                                                    <div>

                                                        <p>Exp: <%# Eval("experience_years") %> Years</p>
                                                        <%--<p class="fs-6"><b>₹ 33</b>/min</p>--%>
                                                    </div>
                                                    <div>
                                                        <asp:LinkButton ID="LinkChat" Enabled='<%# (bool)Eval("is_online") %>' OnClick="LinkChat_Click" CommandArgument='<%# Eval("astrologer_id") %>' runat="server" CssClass="<%# GetOnlineStatus(Container.DataItem) %>">
                                                            Chat
                                                        </asp:LinkButton>
                                                        <%-- 1000=1 sec --%>
                                                        <%-- 300000 --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="pb-3"></div>
    </div>
</asp:Content>
