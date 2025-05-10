<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AstroApp.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <meta name="theme-color" content="#0134d4" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />

    <title>Error</title>

    <link rel="icon" href="img/core-img/favicon.ico" />
    <link rel="apple-touch-icon" href="img/icons/icon-96x96.png" />
    <link rel="apple-touch-icon" sizes="152x152" href="img/icons/icon-152x152.png" />
    <link rel="apple-touch-icon" sizes="167x167" href="img/icons/icon-167x167.png" />
    <link rel="apple-touch-icon" sizes="180x180" href="img/icons/icon-180x180.png" />

    <link rel="stylesheet" href="style.css" />

    <link rel="manifest" href="manifest.json" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="header-area" id="headerArea">
            <div class="container">
                <div class="header-content header-style-five position-relative d-flex align-items-center justify-content-between">
                    <div class="back-button">
                        <asp:LinkButton ID="LinkPrevious" OnClick="LinkBack_Click" runat="server">
                            <i class="bi bi-arrow-left-short"></i>
                        </asp:LinkButton>
                    </div>
                    <div class="page-heading">
                        <h6 class="mb-0">Page Not found</h6>
                    </div>
                    <div class="navbar--toggler" id="affanNavbarToggler" data-bs-toggle="offcanvas" data-bs-target="#affanOffcanvas"
                        aria-controls="affanOffcanvas">
                    </div>
                </div>
            </div>
        </div>

        <div class="page-content-wrapper py-3">
            <div class="custom-container">
                <div class="card">
                    <div class="card-body px-5 text-center">
                        <img class="mb-4" src="img/bg-img/39.png" alt="" />
                        <h4>OOPS...
                            <br />
                            Page not found!</h4>
                        <p class="mb-4">We couldn't find any results for your search. Try again.</p>
                        <asp:LinkButton ID="LinkBack" OnClick="LinkBack_Click" CssClass="btn btn-creative btn-danger" runat="server">Back</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
