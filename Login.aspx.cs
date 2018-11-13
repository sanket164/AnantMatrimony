using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    dbInteraction objdb = new dbInteraction();
    UD_Global objGlobal = new UD_Global();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        DataTable dtLoginDetails = new DataTable();
        try
        {
            lblErrmsg.Attributes.Add("style", "display:none");
            string UserName = Convert.ToString(txtEmail_login.Text);
            string Password = Convert.ToString(txtPassword_login.Text);
            DataSet ds = objdb.ExecuteDataset("CheckforLogin", UserName, Password);
            dtLoginDetails = ds.Tables[0];
            if (dtLoginDetails.Rows.Count > 0)
            {
                Session["MemberCode"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberCode"]);
                Session["ProfileID"] = Convert.ToString(dtLoginDetails.Rows[0]["ProfileID"]);
                Session["MemberName"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberName"]);
                Session["Gender"] = Convert.ToString(dtLoginDetails.Rows[0]["Gender"]);
                objdb.ExecuteDataset("Insert_tbl_LogTable", Convert.ToString(dtLoginDetails.Rows[0]["MemberCode"]), DateTime.Now, "1.1.1.1.", "");
                Response.Redirect("/Welcome");
            }
            else
            {
                lblErrmsg.Attributes.Add("style", "display:block");
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.ToUpper() == "UNAPPROVED")
            {
                lblAlert.Text = "Your profile is under Admin process. It will take 48 hourse for activation of your profile";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
            }
        }
    }

    [WebMethod]
    public static bool CheckLogin(string UserName, string Password)
    {
        DataTable dtLoginDetails = new DataTable();
        try
        {

            //DataSet ds = objdb.ExecuteDataset("CheckforLogin_new", UserName, Password);
            //dtLoginDetails = ds.Tables[0];
            //if (dtLoginDetails.Rows.Count > 0)
            //{
            //    Session["MemberCode"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberCode"]);
            //    Session["ProfileID"] = Convert.ToString(dtLoginDetails.Rows[0]["ProfileID"]);
            //    Session["MemberName"] = Convert.ToString(dtLoginDetails.Rows[0]["MemberName"]);
            //    Session["Gender"] = Convert.ToString(dtLoginDetails.Rows[0]["Gender"]);
            //    return true;
            //}
            //else
            //{
            //    lblErrmsg.Attributes.Add("style", "display:block");
            //    return false;
            //}
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}