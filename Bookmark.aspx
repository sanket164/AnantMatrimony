<%@ Page Title="Bookmark List | Anant Matrimony" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Bookmark.aspx.cs" Inherits="Bookmark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="content welcome-page interest-rec">
        <div class="assistance">
            <a href="ContactUs" target="_blank">
                <img src="images/button.png"></a>
        </div>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="common-title">Bookmark</h3>
                </div>
            </div>
            <%--<a href="AboutUs.aspx" onclick="return confirm('Are you sure?')">Delete</a>--%>
            <div class="row">
                <%--<p><span class="comment">8</span>profile/s have/has shown interest for your profile.</p>--%>
                <asp:Literal ID="ltrBodySection" runat="server"></asp:Literal>
                <!--/myCarousel-->
            </div>
        </div>
    </div>
</asp:Content>

