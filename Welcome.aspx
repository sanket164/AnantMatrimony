<%@ Page Title="Welcome | Anant Matrimony" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="content welcome-page">
        <div class="assistance">
            <a href="ContactUs" target="_blank">
                <img src="images/button.png"></a>
        </div>
        <div class="container">
            <div class="row">
                <div class="welcome-intro">
                    <h2>Welcome,
                        <asp:Label ID="lblMemberName" runat="server" Text="sss"></asp:Label></h2>
                    <h4>Namaste, Welcome to Anant Matrimony
                        <br>
                        Elite Matrimonial Service, Since 2003</h4>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <%--<h3 class="common-title">Recommended for you</h3>--%>
                </div>
            </div>
            <asp:Literal ID="ltrLoadRecom" runat="server" Visible="false"></asp:Literal>

            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/First_1.jpeg" CssClass="banner-row" />

        </div>
    </div>
</asp:Content>

