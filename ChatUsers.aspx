<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChatUsers.aspx.cs" Inherits="AstroApp.ChatUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper py-3">
        <div class="container">
            <%--<div class="card mb-2">
                <div class="card-body p-2">
                    <div class="chat-search-box">

                        <div action="#">
                            <div class="input-group">
                                <span class="input-group-text" id="searchbox">
                                    <i class="bi bi-search"></i>
                                </span>
                                <input class="form-control form-control-clicked" type="search" placeholder="Search users or messages" aria-describedby="searchbox">
                            </div>
                        </div>

                    </div>
                </div>
            </div>--%>
            <div class="element-heading">
                <h6 class="ps-1">Recent Chats</h6>
            </div>
            <ul class="ps-0 chat-user-list">

                <asp:Repeater ID="rptChatUsers" runat="server">
                    <ItemTemplate>
                        <li class="<%# GetOnlineStatus(Container.DataItem) %>">
                            <asp:LinkButton ID="LinkChat" OnClick="LinkChat_Click" CommandArgument='<%# Eval("astrologer_id") %>' runat="server" CssClass="d-flex">
                                                  <div class="chat-user-thumbnail me-3 shadow">
                                                  <img class="img-circle" src="<%# Eval("profile_picture")  %>" style="height: 36px; width: 36px; object-fit: cover;" alt="">
                                                  <span class="active-status"></span>
                                                                </div>
                                                        <div class="chat-user-info">
                                               <h6 class="text-truncate mb-0"><%# Eval("UserName")  %></h6>
                                                 <%-- <div class="last-chat">
                                         <p class="text-truncate mb-0">I want to buy your Affan template.</p>
                                        </div>--%>
                                         </div>
                            </asp:LinkButton>
                            <%--<div class="dropstart chat-options-btn">
                                <button class="btn dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <i class="bi bi-three-dots-vertical"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a href="#"><i class="bi bi-mic-mute"></i>Mute</a></li>
                                    <li><a href="#"><i class="bi bi-slash-circle"></i>Ban</a></li>
                                    <li><a href="#"><i class="bi bi-trash"></i>Remove</a></li>
                                </ul>
                            </div>--%>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
