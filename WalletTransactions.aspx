<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WalletTransactions.aspx.cs" Inherits="AstroApp.WalletTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrapper">
        <div class="pt-3"></div>
        <div class="container">
            <p class="mb-2">Available Balance</p>
            <h4>₹ <asp:Literal ID="lblWalletBalance" Text="0" runat="server"></asp:Literal></h4>
            <p>Wallet Transactions</p>
        </div>
        
    </div>
</asp:Content>
