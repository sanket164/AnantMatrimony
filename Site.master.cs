using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;
    dbInteraction objdb = new dbInteraction();

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        //Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetControl();
        }
        if (Session["MemberCode"] != null)
        {
            lnkHomeLink.HRef = "/Welcome";
        }
        else
        {
            lnkHomeLink.HRef = "/Default";
        }
    }

    private void SetControl()
    {
        try
        {
            string strHTML = "";
            if (Session["MemberCode"] != null)
            {
                strHTML = " <li class='dropdown'> ";
                strHTML += " <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>My Account<span class='caret'></span></a> ";
                strHTML += " <ul class='dropdown-menu' role='menu'> ";
                strHTML += " <li><a href='/MemberDetails?M_id=" + Session["MemberCode"] + "&P_Id='''>My Profile</a></li> ";
                strHTML += " <li><a href='ChangePassword'>Change Password</a></li> ";
                //strHTML += " <li><a href='MyFilter'>My Filter</a></li> ";
                strHTML += " <li><a href='logout.aspx'>Logout</a></li> ";
                strHTML += " </ul> ";
                strHTML += " </li>";
            }
            else
            {
                strHTML = "<li class='login'><a href='/Login' >Login</a></li>";
            }
            ltrMyAccCnt.Text = strHTML;
            ////Set Menu based on Login
            strHTML = "";
            if (Session["MemberCode"] != null)
            {
                strHTML = "";
                strHTML += " <ul class='nav navbar-nav'> ";
                //strHTML += " <li class='dropdown'> ";
                //strHTML += " <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Express Interest<span class='caret'></span></a> ";
                //strHTML += " <ul class='dropdown-menu' role='menu'> ";
                //strHTML += " <li><a href='InterestSend'>Interest Send</a></li> ";
                //strHTML += " <li><a href='InterestReceived'>Interest Received</a></li> ";
                //strHTML += " </ul> ";
                //strHTML += " </li> ";
                strHTML += " <li class='dropdown'> ";
                strHTML += " <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>Bookmark<span class='caret'></span></a> ";
                strHTML += " <ul class='dropdown-menu' role='menu'> ";
                strHTML += " <li><a href='Bookmark'>My Bookmark</a></li> ";
                //strHTML += " <li><a href='FollowUp'>Follow Up</a></li> ";
                strHTML += " </ul> ";
                strHTML += " </li> ";
                strHTML += " <li><a href='SimpleSearch'>Direct Search</a></li> ";
                strHTML += " </ul> ";
            }
            else
            {
                strHTML = "";
                strHTML += " <ul class='nav navbar-nav'> ";
                strHTML += " <li><a href='Default'>Services</a></li> ";
                strHTML += " <li><a href='AboutUs'>About Us</a></li> ";
                strHTML += " <li><a href='ContactUs'>Contact Us</a></li> ";
                strHTML += " <li><a href='SimpleSearch'>Direct Search</a></li> ";
                strHTML += " </ul> ";
            }
            ltrMenuCnt.Text = strHTML;
            string sPath = Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            if (sRet == "Default")
            {
                ExtraFooter.Visible = true;
            }
            else
            {
                ExtraFooter.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //if (CheckLogin(txtEmail_login.Text.Trim(), txtPassword_login.Text.Trim()))
            //{

            //}
            //else
            //{

            //}
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    //[WebMethod()]
    //[System.Web.Script.Services.ScriptMethod()]
    //public static bool CheckLogin(string UserName, string Password)
    //{
    //    DataTable dtLoginDetails = new DataTable();
    //    try
    //    {
    //        //DataSet ds = objdb.ExecuteDataset("CheckforLogin_new", UserName, Password); 
    //        //dtLoginDetails = ds.Tables[0];
    //        //if (dtLoginDetails.Rows.Count > 0)
    //        //{
    //        //    Session["MemberCode"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberCode"]);
    //        //    Session["ProfileID"] = Convert.ToString(dtLoginDetails.Rows[0]["ProfileID"]);
    //        //    Session["MemberName"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberName"]);
    //        //    return true;
    //        //}
    //        //else
    //        //{
    //        //    lblErrmsg.Attributes.Add("style", "display:block");
    //        //    return false;
    //        //}
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {

    //        throw;
    //    }
    //}


    protected void btnSubmit_Click1(object sender, EventArgs e)
    {

    }
}