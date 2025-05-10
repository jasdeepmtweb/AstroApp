<%@ Page Title="" Language="C#" MasterPageFile="~/Astrologer/Site.Master" AutoEventWireup="true" CodeBehind="AstroHome.aspx.cs" Inherits="AstroApp.Astrologer.ChatMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper py-3">
        <div class="container">
            <div class="card mb-2">
                <div class="card-body p-2">
                    <div class="chat-search-box">

                        <div>
                            <div class="input-group">
                                <asp:LinkButton ID="LinkSearch" ValidationGroup="ast" OnClick="LinkSearch_Click" CssClass="input-group-text" runat="server">
                                    <i class="bi bi-search"></i>
                                </asp:LinkButton>
                                <asp:TextBox ID="txtSearch" ValidationGroup="ast" CssClass="form-control form-control-clicked" placeholder="Search users" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtSearch" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username Required" ForeColor="Red" Display="Dynamic" ValidationGroup="ast"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="element-heading">
                <h6 class="ps-1">Recent contacts</h6>
            </div>
            <ul class="ps-0 chat-user-list">
                <asp:UpdatePanel runat="server" ID="up1">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick"></asp:Timer>
                        <asp:Repeater runat="server" ID="rptRecentChats">
                            <ItemTemplate>
                                <li class="<%# GetOnlineStatus(Container.DataItem) %>">
                                    <asp:LinkButton CssClass="d-flex" ID="LinkChat" OnClick="LinkChat_Click" CommandArgument='<%# Eval("user_id") %>' runat="server">
                                            <div class="chat-user-thumbnail me-3 shadow">
                                      <img class="img-circle" style="height: 36px; width: 36px; object-fit: cover;" src="<%# Eval("profile_picture")  %>" alt="">
                                            <span class="active-status"></span>
                                                           </div>
                                                         <div class="chat-user-info">
                                                       <h6 class="text-truncate mb-0"><%# Eval("UserName")  %></h6>
                                                             <%--<div class="last-chat">
                                                                  <p class="mb-0 text-truncate">
                                                                        Hello, Are you there?
                                                                     <span class="badge rounded-pill bg-primary">2</span>
                                                                             </p>
                                                                                </div>--%>
                                                                               </div>
                                    </asp:LinkButton>
                                    <%-- <div class="dropstart chat-options-btn">
                                <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i class="bi bi-three-dots-vertical"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a href="#"><i class="bi bi-trash"></i>Remove</a></li>
                                </ul>
                            </div>--%>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ul>
        </div>
    </div>
</asp:Content>
