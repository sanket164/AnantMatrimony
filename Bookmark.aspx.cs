using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bookmark : System.Web.UI.Page
{
    UD_Global objGlobal = new UD_Global();
    dbInteraction objDb = new dbInteraction();

    string StrSql = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["MemberCode"] == null)
            {
                Response.Redirect("/Default");
            }
            if (Session["MemberCode"] != null)
            {
                long MemberCode = Convert.ToInt64(Session["MemberCode"]);
                Delete(MemberCode);
                GetBookMark(MemberCode);
            }
        }
    }

    public void Delete(long MemberCode)
    {
        try
        {

            if (Request["M_id"] != null)
            {

                string strBookmarkList = "";
                long M_Code = Convert.ToInt32(Request["M_id"]);
                cl_tbl_BookmarkList objBM = new cl_tbl_BookmarkList();
                DataTable dtBookmarkList = objBM.GetBookMarkList(MemberCode);
                if (dtBookmarkList.Rows.Count > 0)
                {
                    string BookmarkedProfile = Convert.ToString(dtBookmarkList.Rows[0]["BookmarkedProfile"]);
                    StrSql = "SELECT items FROM dbo.Split('" + BookmarkedProfile + "',',')";
                    DataTable dtB_Profile = objDb.GetDataTable(StrSql);
                    for (int cnt = 0; cnt < dtB_Profile.Rows.Count; cnt++)
                    {
                        if (M_Code.ToString() != Convert.ToString(dtB_Profile.Rows[cnt]["items"]))
                        {
                            strBookmarkList = strBookmarkList + "," + M_Code;
                        }
                    }
                    if (strBookmarkList.Length > 0)
                    {
                        strBookmarkList = strBookmarkList.Substring(1, strBookmarkList.Length - 1);
                    }
                    objBM.InsertUpdate_tbl_BookmarkList(MemberCode, strBookmarkList);
                    Response.Redirect("Bookmark");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    public void GetBookMark(long MemberCode)
    {
        try
        {
            string strHTML = "";
            cl_tbl_BookmarkList objBM = new cl_tbl_BookmarkList();
            DataTable dtBookmarkList = objBM.GetBookMarkList(MemberCode);
            if (dtBookmarkList.Rows.Count > 0)
            {
                
                for (int icnt = 0; icnt < dtBookmarkList.Rows.Count; icnt++)
                {
                    string BookmarkedProfile = Convert.ToString(dtBookmarkList.Rows[icnt]["BookmarkedProfile"]);
                    DataTable dtMemberDetails = objBM.Load_BookmasterList(BookmarkedProfile);
                    strHTML = " <div id='prof-slider' class='carousel slide'> ";
                    strHTML += " <p>Total <span class='comment'>" + dtMemberDetails.Rows.Count + "</span> Profile Bookmarked</p> ";
                    strHTML += " <div class='carousel-inner'> ";
                    for (int cnt = 0; cnt < dtMemberDetails.Rows.Count; cnt++)
                    {
                        if ((cnt + 1) <= 4)
                        {
                            if ((cnt + 1) == 1)
                            {
                                strHTML += " <div class='item active'> ";
                                strHTML += " <div class='row'> ";
                            }
                            strHTML += " <div class='col-md-3 col-sm-6'> ";
                            strHTML += " <div class='prof-box'> ";
                            strHTML += " <div class='prof-id'> ";
                            strHTML += " <h4>" + Convert.ToString(dtMemberDetails.Rows[cnt]["ProfileID"]) + " </h4> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='prof-img'> ";
                            strHTML += " <img class='img-responsive' src='/MemberPhoto/" + Convert.ToString(dtMemberDetails.Rows[cnt]["PhotoFileName"]) + "' /> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='prof-details'> ";
                            strHTML += " <ul> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Year<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["BirthYear"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Height<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Height"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Religion Caste<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Caste"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Marital Status<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["MaritalStatus"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Education<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Education"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>State / City<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["StateCity"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Visa Status<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Country"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " </ul> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='view-more'> ";
                            strHTML += " <a href='/MemberDetails?M_id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["MemberCode"]) + "&P_Id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["ProfileID"]) + "' target='_blank' >View More</a> ";
                            strHTML += " <a href='/Bookmark?M_id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["MemberCode"]) + "' OnClick = \"return confirm('Are you sure?')\" >Delete</a> ";
                            strHTML += " </div> ";
                            strHTML += " </div> ";
                            strHTML += " </div> ";
                            if ((cnt + 1) == 4)
                            {
                                strHTML += " </div> ";
                                strHTML += " </div> ";
                            }
                        }
                        else
                        {
                            if ((cnt + 1) == 5 || (cnt + 1) == 9 || (cnt + 1) == 13)
                            {
                                strHTML += " <div class='item'> ";
                                strHTML += " <div class='row'> ";
                            }
                            strHTML += " <div class='col-md-3 col-sm-6'> ";
                            strHTML += " <div class='prof-box'> ";
                            strHTML += " <div class='prof-id'> ";
                            strHTML += " <h4>" + Convert.ToString(dtMemberDetails.Rows[cnt]["ProfileID"]) + "</h4> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='prof-img'> ";
                            strHTML += " <img class='img-responsive' src='/MemberPhoto/" + Convert.ToString(dtMemberDetails.Rows[cnt]["PhotoFileName"]) + "' /> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='prof-details'> ";
                            strHTML += " <ul> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Year<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["BirthYear"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Height<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Height"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Religion Caste<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Caste"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Marital Status<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["MaritalStatus"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Education<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Education"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>State / City<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["StateCity"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " <li> ";
                            strHTML += " <label>Visa Status<span>:</span></label><div class='prof-val'>" + Convert.ToString(dtMemberDetails.Rows[cnt]["Country"]) + "</div> ";
                            strHTML += " </li> ";
                            strHTML += " </ul> ";
                            strHTML += " </div> ";
                            strHTML += " <div class='view-more'> ";
                            strHTML += " <a href='/MemberDetails?M_id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["MemberCode"]) + "&P_Id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["ProfileID"]) + "' target='_blank'>View More</a> ";
                            strHTML += " <a href='/Bookmark?M_id=" + Convert.ToString(dtMemberDetails.Rows[cnt]["MemberCode"]) + "' OnClick = \"return confirm('Are you sure?')\" >Delete</a> ";
                            strHTML += " </div> ";
                            strHTML += " </div> ";
                            strHTML += " </div> ";
                            if ((cnt + 1) % 4 == 0)
                            {
                                strHTML += " </div> ";
                                strHTML += " </div> ";
                                
                            }
                        }
                    }
                    strHTML += " </div> ";
                    strHTML += " </div>";
                    strHTML += " </div>";
                    strHTML += " <a class='left carousel-control' href='#prof-slider' data-slide='prev'>‹</a> ";
                    strHTML += " <a class='right carousel-control' href='#prof-slider' data-slide='next'>›</a> ";
                    strHTML += " </div>";
                    ltrBodySection.Text = strHTML;
                }                
            }
            else
            {
                strHTML = "";
                strHTML = " <div> ";
                strHTML += " <img class='img-responsive' src='images/Bookmark.png' /> ";
                strHTML += " </div> ";
                strHTML += " <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />";
                ltrBodySection.Text = strHTML;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void Delete_BookmarkList(long MemberCode, long BookmarkId)
    {
        try
        {
            string strSql = "select * form ";
        }
        catch (Exception ex)
        {

        }
    }





}