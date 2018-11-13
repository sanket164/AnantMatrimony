<%@ Page Title="Contact Us | Anant Matrimony" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Contact" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content">
        <div class="container contact-us">
            <div class="row">
                <div class="col-md-12">
                    <h3 class="common-title">Contact Us</h3>
                </div>
            </div>
            <div class="row">
                <div class="col-md-5 col-sm-5">
                    <div class="address">
                        <ul class="add">
                            <li>425, 4th Floor, Saman Complex,</li>
                            <li>Opp. Satyam Mall,</li>
                            <li>Near Jodhpur Cross Roads,</li>
                            <li>Satellite, Ahmedabad,</li>
                            <li>Gujarat 380015</li>
                        </ul>
                        <ul class="cont-info">
                            <li><i class="fas fa-envelope"></i><a href="mailto:anant_matri@yahoo.com">anant_matri@yahoo.com</a></li>
                            <li><i class="fas fa-phone" data-fa-transform="rotate-90"></i><a href="tel:1234567890">9428412065, 9998489093, 9428612910</a></li>
                        </ul>
                        <ul class="cont-info">
                            <li><i class="fas fa-clock"></i>&nbsp;&nbsp;Monday To Saturday 11:00 AM to 07:00 PM</li>
                            <li><i>&nbsp;&nbsp;&nbsp;&nbsp;</i>&nbsp;&nbsp;Sunday 11:00 AM to 04:00 PM</li>
                        </ul>
                        <div class="social">
                            <ul class="fa-ul">
                                <li><a href="#"><i class="fab fa-facebook-f"></i></a></li>
                                <li><a href="#"><i class="fab fa-twitter"></i></a></li>
                                <li><a href="#"><i class="fab fa-instagram"></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-7 col-sm-7">
                    <div class="cont-form">
                        <form>
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="Your Name">
                            </div>
                            <div class="form-group">
                                <input type="email" class="form-control" placeholder="Email">
                            </div>
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="Mobile Number">
                            </div>
                            <div class="form-group">
                                <textarea class="form-control" rows="4" placeholder="Subject"></textarea>
                            </div>
                            <div class="form-group">
                                <button class="common-button">Submit</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3671.8192428854622!2d72.52499382360844!3d23.030408172544483!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x395e84c5249ab5b1%3A0x5ff8d49b78a9616c!2sAnant+Matrimony!5e0!3m2!1sen!2sin!4v1514632389199" width="100%" height="400" frameborder="0" style="border: 0" allowfullscreen></iframe>
            </div>
        </div>
    </div>
</asp:Content>
