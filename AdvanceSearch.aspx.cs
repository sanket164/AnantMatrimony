﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdvanceSearch : System.Web.UI.Page
{
    dbInteraction objdb = new dbInteraction();
    UD_Global objGlobal = new UD_Global();
    SqlConnection objConnection = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillCombo();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void FillCombo()
    {
        try
        {
            DataTable dtCaste = objGlobal.GetCasteList("");
            lstCaste.DataSource = dtCaste;
            lstCaste.DataValueField = "CasteCode";
            lstCaste.DataTextField = "Caste";
            lstCaste.DataBind();
            //lstCaste.Items.Add("All");
            lstCaste.Items.Insert(0, new ListItem("Any", ""));

            DataTable dtEducation = objGlobal.GetEducationList("");
            lstEducation.DataSource = dtEducation;
            lstEducation.DataValueField = "EducationCode";
            lstEducation.DataTextField = "Education";
            lstEducation.DataBind();
            lstEducation.Items.Insert(0, new ListItem("Any", ""));

            DataTable dtCountry = objGlobal.GetCountryList("");
            lst_Country.DataSource = dtCountry;
            lst_Country.DataValueField = "CountryCode";
            lst_Country.DataTextField = "Country";
            lst_Country.DataBind();
            lst_Country.Items.Insert(0, new ListItem("Any", ""));

            string MaritalStatus = "";
            if (Convert.ToString(Session["MemberCode"]) != "")
            {
                int MemberCode = Convert.ToInt32(Session["MemberCode"]);
                MaritalStatus = objGlobal.GetMaritalStatus(MemberCode);
            }
            DataTable dtMaritalStatusList = new DataTable();
            if (MaritalStatus != "")
            {
                if (MaritalStatus == "Widow/Widower")
                {
                    MaritalStatus = "Widow";
                }
                dtMaritalStatusList = objGlobal.GetMaritalStatusList(MaritalStatus);
            }
            else
            {
                dtMaritalStatusList = objGlobal.GetMaritalStatusList("");
            }
            lstMarital_Status.DataSource = dtMaritalStatusList;
            lstMarital_Status.DataValueField = "MaritalStatusCode";
            lstMarital_Status.DataTextField = "MaritalStatus";
            lstMarital_Status.DataBind();
            if (MaritalStatus == "")
            {
                lstMarital_Status.Items.Insert(0, new ListItem("Any", ""));
            }

            int FromYear = 1950, ToYear = 1996;
            if (Session["ProfileID"] != null)
            {
                string val = Convert.ToString(Session["ProfileID"]);
                int MemberCode = Convert.ToInt32(Session["MemberCode"]);
                int AgeDiff = objGlobal.GetAgeDiff(MemberCode);
                DataTable dtMemberList = objdb.GetDataTable("select * from tbl_MemberMaster  where MemberCode=" + MemberCode);
                if (dtMemberList.Rows.Count > 0)
                {
                    if (Convert.ToString(dtMemberList.Rows[0]["DateOfBirth"]) != "")
                    {
                        DateTime DOB = Convert.ToDateTime(dtMemberList.Rows[0]["DateOfBirth"]);
                        FromYear = DOB.Year;                        
                        ////0= Male, 1=Female
                        if (Convert.ToString(Session["Gender"]) == "0")
                        {
                            ToYear = FromYear + AgeDiff;
                        }
                        else
                        {
                            ToYear = FromYear;
                            FromYear = FromYear - AgeDiff;
                        }
                    }
                }
            }

            DataTable dtFromBorn = objGlobal.GetYearList(FromYear, ToYear);
            ddlBornFrom.DataSource = dtFromBorn;
            ddlBornFrom.DataValueField = "NValue";
            ddlBornFrom.DataTextField = "Number";
            ddlBornFrom.DataBind();

            DataTable dtToBorn = objGlobal.GetYearList(FromYear, ToYear);
            ddlBornTo.DataSource = dtToBorn;
            ddlBornTo.DataValueField = "NValue";
            ddlBornTo.DataTextField = "Number";
            ddlBornTo.DataBind();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lst_Country.SelectedValue != "")
        {
            DataTable dtStateCity = objGlobal.GetStateCityList("", lst_Country.SelectedValue);
            lstState.DataSource = dtStateCity;
            lstState.DataValueField = "StateCityCode";
            lstState.DataTextField = "StateCity";
            lstState.DataBind();
            lstState.Items.Insert(0, new ListItem("Any", ""));
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["MemberCode"] == null)
            {
                Response.Redirect("/Default");
            }
            else
            {
                //long MemberCode = Convert.ToInt64(Session["MemberCode"]);
                //int s_Gender = objGlobal.GetGender(MemberCode);
                //if (s_Gender == 0)
                //{
                //    s_Gender = 1;
                //}
                //else
                //{
                //    s_Gender = 0;
                //}
                //Session["s_Gender"] = s_Gender;
                Session["S_BornFrom"] = ddlBornFrom.SelectedValue;
                Session["S_BornTo"] = ddlBornTo.SelectedValue;
                Session["MaritalStatusList"] = objGlobal.GetListBox_SelectedItem(lstMarital_Status);
                Session["CasteList"] = objGlobal.GetListBox_SelectedItem(lstCaste);
                Session["Education"] = objGlobal.GetListBox_SelectedItem(lstEducation);
                Session["Country"] = objGlobal.GetListBox_SelectedItem(lst_Country);
                Session["State"] = objGlobal.GetListBox_SelectedItem(lstState);
                Response.Redirect("/SearchResult.aspx");
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }

}