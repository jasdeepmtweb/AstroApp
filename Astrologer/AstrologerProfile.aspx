<%@ Page Title="" Language="C#" MasterPageFile="~/Astrologer/Site.Master" AutoEventWireup="true" CodeBehind="AstrologerProfile.aspx.cs" Inherits="AstroApp.Astrologer.AstrologerProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        input[type='checkbox'] {
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
                        <asp:Image ID="ImgUser" ImageUrl="~/user.png" style="height: 76px; width: 76px; object-fit: cover;"
                            runat="server" />
                        <asp:Literal ID="lblImage" Visible="false" runat="server"></asp:Literal>
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
                            <label class="form-label" for="email">
                                Email Id
                             <asp:RequiredFieldValidator ControlToValidate="txtEmailId" ID="FieldValidator4" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtEmailId" placeholder="Enter Email Id" ValidationGroup="a" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox>
                        </div>

                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Bio
                             <asp:RequiredFieldValidator ControlToValidate="txtBio" ID="FieldValidator11" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                            </label>
                            <asp:TextBox ID="txtBio" placeholder="Enter Bio" ValidationGroup="a" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Experience (In Years)
                             <asp:RequiredFieldValidator ControlToValidate="txtExperience" ID="FieldValidator15" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic">
                             </asp:RequiredFieldValidator>
                                <asp:CompareValidator ControlToValidate="txtExperience" Type="Integer" Operator="DataTypeCheck" ID="CompareValidator14" runat="server" ErrorMessage="Enter Only Numbers" Display="Dynamic" ForeColor="Red" ValidationGroup="a"></asp:CompareValidator>
                            </label>
                            <asp:TextBox ID="txtExperience" placeholder="Enter Experience" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <%-- <div class="form-group mb-3">
                            <label class="form-label" for="job">
                                Rate Per Minute
      <asp:RequiredFieldValidator ControlToValidate="txtRatePerMinute" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Required" ValidationGroup="a" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ControlToValidate="txtRatePerMinute" Type="Double" Operator="DataTypeCheck" ID="CompareValidator1" runat="server" ErrorMessage="Enter Only Numbers" Display="Dynamic" ForeColor="Red" ValidationGroup="a"></asp:CompareValidator>
                            </label>
                            <asp:TextBox ID="txtRatePerMinute" placeholder="Enter Rate Per Minute" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>--%>
                        <div class="form-group text-start mb-3">
                            <p class="mb-2">Specialization</p>
                            <asp:CheckBoxList ID="CheckSpecialization" RepeatDirection="Horizontal" RepeatColumns="2" runat="server">
                            </asp:CheckBoxList>
                        </div>

                        <div class="form-group text-start mb-3">
                            <p class="mb-2">Languages Known</p>
                            <asp:CheckBoxList ID="CheckListLanguages" RepeatDirection="Horizontal" CssClass="form-control" runat="server">
                                <asp:ListItem Text="Hindi" Value="Hindi" />
                                <asp:ListItem Text="English" Value="English" />
                                <asp:ListItem Text="Punjabi" Value="Punjabi" />
                            </asp:CheckBoxList>
                        </div>
                        <asp:Button ID="BtnSubmit" CssClass="btn btn-warning w-100" OnClick="BtnSubmit_Click" ValidationGroup="a" runat="server" Text="Submit" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
