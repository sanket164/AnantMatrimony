using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    UD_Global objGlobal = new UD_Global();
    cl_tbl_MemberMaster objMemberMaster = new cl_tbl_MemberMaster();
    DataTable dtSibbling = new DataTable();
    dbInteraction objdb = new dbInteraction();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FillCombo();
                Fill_Data_SignUp();
                if (Session["MemberCode"] != null)
                {
                    GetMemberDetails();
                }
            }
            if (Session["dtSibbling"] == null)
            {
                CreateTable();
            }
            else
            {
                dtSibbling = (DataTable)Session["dtSibbling"];
                if (dtSibbling.Columns.Count == 0)
                {
                    CreateTable();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void CreateTable()
    {
        try
        {
            dtSibbling.Columns.Add("MemberCode");
            dtSibbling.Columns.Add("SrNo");
            dtSibbling.Columns.Add("BrotherSister");
            dtSibbling.Columns.Add("SibblingName");
            dtSibbling.Columns.Add("MaritalStatus");
            dtSibbling.Columns.Add("Occupation");
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    public void GetMemberDetails()
    {
        try
        {
            long MemberCode = Convert.ToInt64(Session["MemberCode"]);
            DataTable dtMember = objGlobal.Load_MemberDetails(1, 0, "", MemberCode);
            if (dtMember.Rows.Count > 0)
            {
                txtMemberName.Text = Convert.ToString(dtMember.Rows[0]["MemberName"]);
                lblProfileId.Value = Convert.ToString(dtMember.Rows[0]["ProfileID"]);
                lblPassword.Value = objGlobal.GetPassword(MemberCode);
                //DateTime dtTempVal = DateTime.ParseExact(Convert.ToString(dtMember.Rows[0]["RegisterDate"]), "dd/MM/yyyy", null);
                //lblRegisterDate.Value = dtTempVal.ToString();
                ddlAnnualIncome.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["AnnualIncome"]);
                txtDob.Text = Convert.ToString(dtMember.Rows[0]["DateofBirth"]);
                txtBirthPlace.Text = Convert.ToString(dtMember.Rows[0]["BirthPlace"]);
                txtBirthtime.Text = Convert.ToString(dtMember.Rows[0]["TimeofBirth_1"]);
                lblProfileCreated.Value = Convert.ToString(objGlobal.GetProfileCreatedId(Convert.ToString(dtMember.Rows[0]["ProfileCreatedBy"])));
                if (Convert.ToString(dtMember.Rows[0]["BloodGroup"]) != "")
                {
                    ddlBloodGrp.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["BloodGroup"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["Caste"]) != "")
                {
                    ddlCaste.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Caste"]);
                    GetSubCaste();
                }
                if (Convert.ToString(dtMember.Rows[0]["Country"]) != "")
                {
                    ddlCountry.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Country"]);
                    GetState();
                }
                if (Convert.ToString(dtMember.Rows[0]["Education"]) != "")
                {
                    ddlEducation.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Education"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["Gender"]) != "")
                {
                    ddlGender.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Gender"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["Height"]) != "")
                {
                    ddlHeight.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Height"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["Manglik"]) != "")
                {
                    ddlManglik.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Manglik"]);
                }
                ddlMaritalStatus.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["MaritalStatus"]);
                txtAboutMySelf.Text = Convert.ToString(dtMember.Rows[0]["AboutInfo"]);
                if (Convert.ToString(dtMember.Rows[0]["Occupation"]) != "")
                {
                    ddlOccupation.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["Occupation"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["StateCity"]) != "")
                {
                    ddlStateCity.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["StateCity"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["SubCaste"]) != "")
                {
                    ddlSubCaste.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["SubCaste"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["VisaStatus"]) != "")
                {
                    ddlVisaStatus.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["VisaStatus"]);
                }

                if (Convert.ToString(dtMember.Rows[0]["MobileNo_Rel"]) != "")
                {
                    ddlMob_Rel.SelectedItem.Text = Convert.ToString(dtMember.Rows[0]["MobileNo_Rel"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["MobileNo1_Rel"]) != "")
                {
                    ddlMob1_Rel.Text = Convert.ToString(dtMember.Rows[0]["MobileNo1_Rel"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["MobileNo2_Rel"]) != "")
                {
                    ddlMob2_Rel.Text = Convert.ToString(dtMember.Rows[0]["MobileNo2_Rel"]);
                }
                if (Convert.ToString(dtMember.Rows[0]["LandlineNo1_Rel"]) != "")
                {
                    ddlLanline_Rel.Text = Convert.ToString(dtMember.Rows[0]["LandlineNo1_Rel"]);
                }

                txtWeight.Text = Convert.ToString(dtMember.Rows[0]["Weight"]);
                txtEmail.Text = Convert.ToString(dtMember.Rows[0]["EmailId"]);
                txtSecondEmail.Text = Convert.ToString(dtMember.Rows[0]["SecondaryEmailID"]);
                txtAddress.Text = Convert.ToString(dtMember.Rows[0]["HomeAddress1"]);
                txtAltAddress.Text = Convert.ToString(dtMember.Rows[0]["HomeAddress2"]);
                txtFatherName.Text = Convert.ToString(dtMember.Rows[0]["FatherName"]);
                txtMotherName.Text = Convert.ToString(dtMember.Rows[0]["MotherName"]);
                txtFather_Occupation.Text = Convert.ToString(dtMember.Rows[0]["FatherOccupation"]);
                txtMother_Occupation.Text = Convert.ToString(dtMember.Rows[0]["MotherOccupation"]);
                txtMosalPlace.Text = Convert.ToString(dtMember.Rows[0]["MosalPlace"]);
                txtNativePlace.Text = Convert.ToString(dtMember.Rows[0]["NativePlace"]);
                txtWorkAddress.Text = Convert.ToString(dtMember.Rows[0]["WorkAddress"]);
                txtMobileNo.Text = Convert.ToString(dtMember.Rows[0]["MobileNo"]);
                txtMobileNo_1.Text = Convert.ToString(dtMember.Rows[0]["MobileNo1"]);
                txtLandLineNo.Text = Convert.ToString(dtMember.Rows[0]["LandlineNo"]);
                txtOccupationDetails.Text = Convert.ToString(dtMember.Rows[0]["OccupationDtls"]);
                txtDegree.Text = Convert.ToString(dtMember.Rows[0]["Degree"]);

                GetSibblingDetails(MemberCode);
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    public void FillCombo()
    {
        try
        {
            DataTable dtCaste = objGlobal.GetCasteList("");
            ddlCaste.DataSource = dtCaste;
            ddlCaste.DataValueField = "CasteCode";
            ddlCaste.DataTextField = "Caste";
            ddlCaste.DataBind();

            DataTable dtEducation = objGlobal.GetEducationList("");
            ddlEducation.DataSource = dtEducation;
            ddlEducation.DataValueField = "EducationCode";
            ddlEducation.DataTextField = "Education";
            ddlEducation.DataBind();

            DataTable dtCountry = objGlobal.GetCountryList("");
            ddlCountry.DataSource = dtCountry;
            ddlCountry.DataValueField = "CountryCode";
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataBind();

            ddlVisaCountry.DataSource = dtCountry;
            ddlVisaCountry.DataValueField = "CountryCode";
            ddlVisaCountry.DataTextField = "Country";
            ddlVisaCountry.DataBind();

            DataTable dtMaritalStatusList = objGlobal.GetMaritalStatusList("");
            ddlMaritalStatus.DataSource = dtMaritalStatusList;
            ddlMaritalStatus.DataValueField = "MaritalStatusCode";
            ddlMaritalStatus.DataTextField = "MaritalStatus";
            ddlMaritalStatus.DataBind();

            ddlMaritalStatus_Brother_Sister.DataSource = dtMaritalStatusList;
            ddlMaritalStatus_Brother_Sister.DataValueField = "MaritalStatusCode";
            ddlMaritalStatus_Brother_Sister.DataTextField = "MaritalStatus";
            ddlMaritalStatus_Brother_Sister.DataBind();

            DataTable dtHeightList = objGlobal.GetHeight();
            ddlHeight.DataSource = dtHeightList;
            ddlHeight.DataValueField = "HeightCM";
            ddlHeight.DataTextField = "Height";
            ddlHeight.DataBind();

            DataTable dtOccupationList = objGlobal.GetOccupationList("");
            ddlOccupation.DataSource = dtOccupationList;
            ddlOccupation.DataValueField = "OccupationCode";
            ddlOccupation.DataTextField = "Occupation";
            ddlOccupation.DataBind();

            ddlOccupation_Brother_Sister.DataSource = dtOccupationList;
            ddlOccupation_Brother_Sister.DataValueField = "OccupationCode";
            ddlOccupation_Brother_Sister.DataTextField = "Occupation";
            ddlOccupation_Brother_Sister.DataBind();

            DataTable dtAnnualIncome = objGlobal.GetAnnualIncomeList();
            ddlAnnualIncome.DataSource = dtAnnualIncome;
            ddlAnnualIncome.DataValueField = "AnnualIncomeCode";
            ddlAnnualIncome.DataTextField = "AnnualIncome";
            ddlAnnualIncome.DataBind();

            DataTable dtStateCity = objGlobal.GetStateCityList("", ddlCountry.Text);
            ddlStateCity.DataSource = dtStateCity;
            ddlStateCity.DataValueField = "StateCityCode";
            ddlStateCity.DataTextField = "StateCity";
            ddlStateCity.DataBind();

            DataTable dtBloodGrp = objGlobal.GetBloodGrpList("");
            ddlBloodGrp.DataSource = dtBloodGrp;
            ddlBloodGrp.DataValueField = "BloodGroupCode";
            ddlBloodGrp.DataTextField = "BloodGroup";
            ddlBloodGrp.DataBind();

            DataTable dtVisaStatus = objGlobal.GetVisaStatusList("");
            ddlVisaStatus.DataSource = dtVisaStatus;
            ddlVisaStatus.DataValueField = "VisaStatusCode";
            ddlVisaStatus.DataTextField = "VisaStatus";
            ddlVisaStatus.DataBind();

            string strSql = "SELECT 'Select' as Id ,0 as ORD UNION ";
            strSql += " SELECT 'Father' as Id,1 as ORD UNION  ";
            strSql += " SELECT 'Mother' as Id,2 as ORD UNION";
            strSql += " SELECT 'Self/Brother/Sister' as Id,3 as ORD ORDER BY ORD  ";
            DataTable dtRel_1 = objdb.GetDataTable(strSql);
            ddlMob_Rel.DataSource = dtRel_1;
            ddlMob_Rel.DataValueField = "Id";
            ddlMob_Rel.DataTextField = "Id";
            ddlMob_Rel.DataBind();
            ddlMob_Rel.SelectedIndex = 0;

            ddlMob1_Rel.DataSource = dtRel_1;
            ddlMob1_Rel.DataValueField = "Id";
            ddlMob1_Rel.DataTextField = "Id";
            ddlMob1_Rel.DataBind();
            ddlMob1_Rel.SelectedIndex = 0;

            ddlMob2_Rel.DataSource = dtRel_1;
            ddlMob2_Rel.DataValueField = "Id";
            ddlMob2_Rel.DataTextField = "Id";
            ddlMob2_Rel.DataBind();
            ddlMob2_Rel.SelectedIndex = 0;

            strSql = "SELECT 'Select' as Id,0 as ORD  UNION";
            strSql += " SELECT 'Home' as Id,1 as ORD UNION ";
            strSql += " SELECT 'Work' as Id,2 as ORD  UNION ";
            strSql += " SELECT 'Other' as Id,3 as ORD  ORDER BY ORD  ";
            DataTable dtWLL = objdb.GetDataTable(strSql);
            ddlLanline_Rel.DataSource = dtWLL;
            ddlLanline_Rel.DataValueField = "Id";
            ddlLanline_Rel.DataTextField = "Id";
            ddlLanline_Rel.DataBind();
            ddlLanline_Rel.SelectedIndex = 0;

            GetSubCaste();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            long MemberCode = 0;
            if (txtMemberName.Text.Trim() == "")
            {
                lblErrMsg.Text = "Please Insert Member Name";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }
            if (txtDob.Text.Trim() == "")
            {
                lblErrMsg.Text = "Please Insert Date of Birth";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }
            if (txtDob.Text.Trim() != "")
            {
                DateTime dtDOB = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", null);
                int Age = DateTime.Now.Year - dtDOB.Year;
                if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
                {
                    if (Age < 18)
                    {
                        lblErrMsg.Text = "Age must be 18 or more than 18";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                        return;
                    }
                }
                else
                {
                    if (Age < 21)
                    {
                        lblErrMsg.Text = "Age must be 21 or more than 21";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                        return;
                    }
                }
            }
            if (txtMobileNo.Text.Trim() == "")
            {
                lblErrMsg.Text = "Please Insert Mobile Number";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }
            if (txtEmail.Text.Trim() == "")
            {
                lblErrMsg.Text = "Please Insert Email";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }

            if (Session["MemberCode"] != null)
            {
                MemberCode = Convert.ToInt64(Session["MemberCode"]);
            }
            if (objGlobal.CheckEmailAlreadyExist(txtEmail.Text, MemberCode))
            {
                lblErrMsg.Text = "Email Id Already Exist";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }

            objMemberMaster.MemberCode = MemberCode;
            objMemberMaster.ProfileID = "";
            objMemberMaster.ProfileCreatedBy = 0;
            objMemberMaster.MemberName = txtMemberName.Text;
            if (ddlGender.SelectedValue != "")
            {
                objMemberMaster.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            }

            objMemberMaster.DateofBirth = DateTime.ParseExact(txtDob.Text, "dd/MM/yyyy", null); //Convert.ToDateTime(txtDob.Text);
            if (txtBirthtime.Text != "")
            {
                objMemberMaster.TimeofBirth = Convert.ToDateTime(txtBirthtime.Text);
            }

            objMemberMaster.BirthPlace = txtBirthPlace.Text;
            objMemberMaster.MaritalStatus = Convert.ToInt32(ddlMaritalStatus.SelectedValue);
            objMemberMaster.NoOfChildren = 0;
            objMemberMaster.LiveChildrenTogether = false;
            if (ddlHeight.SelectedValue != "")
            {
                objMemberMaster.Height = Convert.ToInt32(ddlHeight.SelectedValue);
            }
            if (txtWeight.Text != "")
            {
                objMemberMaster.Weight = Convert.ToInt32(txtWeight.Text);
            }
            objMemberMaster.BodyType = 0;
            objMemberMaster.HealthInformation = 0;
            objMemberMaster.Complexion = 0;
            objMemberMaster.AnyDisability = 0;
            if (ddlBloodGrp.SelectedValue != "")
            {
                objMemberMaster.BloodGroup = Convert.ToInt32(ddlBloodGrp.SelectedValue);
            }
            objMemberMaster.HomeAddress1 = txtAddress.Text;
            objMemberMaster.HomeAddress2 = txtAltAddress.Text;
            if (ddlVisaStatus.SelectedValue != "")
            {
                objMemberMaster.VisaStatus = Convert.ToInt32(ddlVisaStatus.SelectedValue);
            }
            if (ddlCountry.SelectedValue != "")
            {
                objMemberMaster.Country = Convert.ToInt32(ddlCountry.SelectedValue);
            }
            if (ddlStateCity.SelectedValue != "")
            {
                objMemberMaster.StateCity = Convert.ToInt32(ddlStateCity.SelectedValue);
            }
            objMemberMaster.MobileNo = txtMobileNo.Text;
            objMemberMaster.LandlineNo = txtLandLineNo.Text;
            objMemberMaster.MobileNo1 = txtMobileNo_1.Text;
            objMemberMaster.LandlineNo1 = "";
            objMemberMaster.SecondaryEmailID = txtSecondEmail.Text;
            objMemberMaster.Religion = 0;
            objMemberMaster.MotherTongue = 0;
            if (ddlCaste.SelectedValue != "")
            {
                objMemberMaster.Caste = Convert.ToInt32(ddlCaste.SelectedValue);
            }
            if (ddlSubCaste.SelectedValue != "")
            {
                objMemberMaster.SubCaste = Convert.ToInt32(ddlSubCaste.SelectedValue);
            }
            objMemberMaster.Gotra = 0;
            objMemberMaster.FatherName = txtFatherName.Text;
            objMemberMaster.FatherOccupation = txtFather_Occupation.Text;
            objMemberMaster.MotherName = txtMotherName.Text;
            objMemberMaster.MotherOccupation = txtMother_Occupation.Text;
            objMemberMaster.MosalPlace = txtMosalPlace.Text;
            objMemberMaster.NativePlace = txtNativePlace.Text;
            objMemberMaster.FamilyType = 0;
            objMemberMaster.FamilyValues = 0;
            if (ddlManglik.SelectedValue != "")
            {
                objMemberMaster.Manglik = Convert.ToInt32(ddlManglik.SelectedValue);
            }
            objMemberMaster.Nakshtra = 0;
            objMemberMaster.Rashi = 0;
            if (ddlEducation.SelectedValue != "")
            {
                objMemberMaster.Education = Convert.ToInt32(ddlEducation.SelectedValue);
            }
            if (ddlOccupation.SelectedValue != "")
            {
                objMemberMaster.Occupation = Convert.ToInt32(ddlOccupation.SelectedValue);
            }
            objMemberMaster.WorkingWith = 0;
            objMemberMaster.WorkingAs = 0;
            objMemberMaster.WorkAddress = txtWorkAddress.Text;
            objMemberMaster.AnnualIncomeCurrency = 0;
            if (ddlAnnualIncome.SelectedValue != "")
            {
                objMemberMaster.AnnualIncome = Convert.ToInt32(ddlAnnualIncome.SelectedValue);
            }
            objMemberMaster.Diet = 0;
            objMemberMaster.Drink = 0;
            objMemberMaster.Smoke = 0;
            objMemberMaster.AboutInfo = txtAboutMySelf.Text;
            objMemberMaster.EmailID = txtEmail.Text;
            objMemberMaster.Password = "";
            objMemberMaster.RegisterDate = DateTime.Now;
            objMemberMaster.IsActive = 0;
            objMemberMaster.Choice = "";
            objMemberMaster.Remarks = "";
            objMemberMaster.Degree = txtDegree.Text;
            objMemberMaster.OccupationDtls = txtOccupationDetails.Text;
            objMemberMaster.VisaCountry = Convert.ToDecimal(ddlVisaCountry.SelectedValue);
            if (ddlMob_Rel.SelectedIndex > 0)
            {
                objMemberMaster.MobileNo_Rel = ddlMob_Rel.SelectedItem.Text;
            }
            if (ddlMob1_Rel.SelectedIndex > 0)
            {
                objMemberMaster.MobileNo1_Rel = ddlMob1_Rel.SelectedItem.Text;
            }
            if (ddlMob2_Rel.SelectedIndex > 0)
            {
                objMemberMaster.MobileNo2_Rel = ddlMob2_Rel.SelectedItem.Text;
            }
            if (ddlLanline_Rel.SelectedIndex > 0)
            {
                objMemberMaster.LandlineNo1_Rel = ddlLanline_Rel.SelectedItem.Text;
            }


            objMemberMaster.MStatus = 0;

            objMemberMaster.FileNote = "";
            if (MemberCode == 0)
            {
                objMemberMaster.isEdit = false;
                if (!objMemberMaster.Insert())
                {

                }
                else
                {
                    int RetVal = Convert.ToInt32(objMemberMaster.RetVal);
                    string RetProfileID = Convert.ToString(objMemberMaster.RetProfileID);
                    string RetPassword = Convert.ToString(objMemberMaster.RetPassword);
                    Session["ProfileID"] = RetProfileID.ToString() + RetVal.ToString();
                    Session["MemberCode"] = RetVal.ToString();
                    InsertSibblingDetails(false);
                    Session["dtSibbling"] = null;
                    Response.Redirect("/PartnerPreferences");
                }
            }
            else
            {
                objMemberMaster.isEdit = false;
                objMemberMaster.ProfileID = lblProfileId.Value;
                objMemberMaster.Password = lblPassword.Value;
                objMemberMaster.ProfileCreatedBy = Convert.ToInt32(lblProfileCreated.Value);
                //objMemberMaster.RegisterDate = Convert.ToDateTime(lblRegisterDate.Value);
                if (!objMemberMaster.Insert_UpdateMemberMaster())
                {

                }
                else
                {
                    InsertSibblingDetails(true);

                    Response.Redirect("/PartnerPreferences");
                }
            }

        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
        }
    }

    public int InsertSibblingDetails(bool isEdit)
    {
        try
        {
            if (!isEdit)
            {
                dtSibbling = (DataTable)Session["dtSibbling"];
                //objMemberMaster.Delete_tbl_SibblingDetails();
                for (int cnt = 0; cnt < dtSibbling.Rows.Count; cnt++)
                {
                    string _BrotherSister = "", _SibblingName = "", _MaritalStatus = "", _Occupation = "";
                    _BrotherSister = Convert.ToString(dtSibbling.Rows[cnt]["BrotherSister"]);
                    _SibblingName = Convert.ToString(dtSibbling.Rows[cnt]["SibblingName"]);
                    _MaritalStatus = Convert.ToString(dtSibbling.Rows[cnt]["MaritalStatus"]);
                    _Occupation = Convert.ToString(dtSibbling.Rows[cnt]["Occupation"]);
                    objMemberMaster.InsertUpdate_tbl_SibblingDetails(_BrotherSister, _SibblingName, _MaritalStatus, _Occupation);
                }
            }
            else
            {
                dtSibbling = (DataTable)Session["dtSibbling"];
                //objMemberMaster.Delete_tbl_SibblingDetails();
                for (int cnt = 0; cnt < dtSibbling.Rows.Count; cnt++)
                {
                    string _BrotherSister = "", _SibblingName = "", _MaritalStatus = "", _Occupation = "";
                    _BrotherSister = Convert.ToString(dtSibbling.Rows[cnt]["BrotherSister"]);
                    _SibblingName = Convert.ToString(dtSibbling.Rows[cnt]["SibblingName"]);
                    _MaritalStatus = Convert.ToString(dtSibbling.Rows[cnt]["MaritalStatus"]);
                    _Occupation = Convert.ToString(dtSibbling.Rows[cnt]["Occupation"]);
                    objMemberMaster.InsertUpdate_tbl_UpdateSibblingDetails(_BrotherSister, _SibblingName, _MaritalStatus, _Occupation);
                }
            }
            return 1;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
            return 0;
        }
    }

    protected void ddlCaste_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetSubCaste();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private void GetSubCaste()
    {
        if (ddlCaste.SelectedValue != "")
        {
            DataTable dtSubCaste = objGlobal.GetSubCasteList(ddlCaste.SelectedValue, "");
            ddlSubCaste.DataSource = dtSubCaste;
            ddlSubCaste.DataValueField = "SubCasteCode";
            ddlSubCaste.DataTextField = "SubCaste";
            ddlSubCaste.DataBind();
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetState();
    }

    private void GetState()
    {
        DataTable dtStateCity = objGlobal.GetStateCityList("", ddlCountry.Text);
        ddlStateCity.DataSource = dtStateCity;
        ddlStateCity.DataValueField = "StateCityCode";
        ddlStateCity.DataTextField = "StateCity";
        ddlStateCity.DataBind();
    }

    public void GetSibblingDetails(long memberCode)
    {
        try
        {
            dtSibbling = objGlobal.GetSibblingDetails(memberCode);
            if (dtSibbling.Rows.Count > 0)
            {
                GridBind();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    private void GridBind()
    {
        if (Session["dtSibbling"] != null)
        {
            dtSibbling = (DataTable)Session["dtSibbling"];
        }
        dgvSibblingDetails.DataSource = dtSibbling;
        dgvSibblingDetails.DataBind();
        Session["dtSibbling"] = dtSibbling;
    }

    protected void btnDeleteBroSis_Click(object sender, CommandEventArgs e)
    {
        try
        {
            string SrNo = Convert.ToString(e.CommandArgument);
            for (int cnt = 0; cnt < dtSibbling.Rows.Count; cnt++)
            {
                if (SrNo == Convert.ToString(dtSibbling.Rows[cnt]["SrNo"]))
                {
                    dtSibbling.Rows.RemoveAt(cnt);
                    break;
                }
            }
            //foreach (GridViewRow gvrow in dgvSibblingDetails.Rows)
            //{
            //    if (gvrow.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            //        if (chk != null & chk.Checked)
            //        {
            //            Label SrNo = (Label)gvrow.FindControl("SrNo");
            //            for (int cnt = 0; cnt < dtSibbling.Rows.Count; cnt++)
            //            {
            //                if (SrNo.Text == Convert.ToString(dtSibbling.Rows[cnt]["SrNo"]))
            //                {
            //                    dtSibbling.Rows.RemoveAt(cnt);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            GridBind();
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
        }
    }

    protected void btn_AddClick(object sender, EventArgs e)
    {
        try
        {
            if (txtName_Brother_Sister.Text.Trim() == "")
            {
                lblErrMsg.Text = "Please Insert Sibbling Name";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }
            if (DDlBroSis.SelectedIndex == 0)
            {
                lblErrMsg.Text = "Please Select Sibbling Type";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal_Intrest();", true);
                return;
            }
            long memberCode = Convert.ToInt32(Session["MemberCode"]);
            DataRow dr;
            if (dtSibbling == null)
            {
                dtSibbling = (DataTable)Session["dtSibbling"];
            }
            int SrNo = dtSibbling.Rows.Count;
            dr = dtSibbling.NewRow();
            dr["MemberCode"] = memberCode;
            dr["BrotherSister"] = DDlBroSis.SelectedItem.Text;
            dr["SibblingName"] = txtName_Brother_Sister.Text;
            dr["MaritalStatus"] = ddlMaritalStatus_Brother_Sister.SelectedItem.Text;
            dr["Occupation"] = ddlOccupation_Brother_Sister.SelectedItem.Text;
            dr["SrNo"] = (SrNo + 1);
            dtSibbling.Rows.Add(dr);
            GridBind();
            DDlBroSis.SelectedIndex = 0;
            txtName_Brother_Sister.Text = "";
            ddlMaritalStatus_Brother_Sister.SelectedIndex = 0;
            ddlOccupation_Brother_Sister.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }

    public void Fill_Data_SignUp()
    {
        try
        {
            if (Session["dtSingUp"] != null)
            {
                DataTable dtSingUp = new DataTable();
                dtSingUp = (DataTable)Session["dtSingUp"];
                txtMemberName.Text = Convert.ToString(dtSingUp.Rows[0]["MemberName"]);
                ddlGender.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["Gender"]);
                //DateTime dt = Convert.ToDateTime(dtSingUp.Rows[0]["DateofBirth"]);
                txtDob.Text = Convert.ToString(dtSingUp.Rows[0]["DateofBirth"]);
                ddlMaritalStatus.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["MaritalStatus"]);
                ddlCountry.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["Country"]);
                ddlStateCity.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["StateCity"]);
                txtMobileNo.Text = Convert.ToString(dtSingUp.Rows[0]["MobileNo"]);
                ddlCaste.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["Caste"]);
                ddlEducation.SelectedValue = Convert.ToString(dtSingUp.Rows[0]["Education"]);
                txtEmail.Text = Convert.ToString(dtSingUp.Rows[0]["EmailID"]);
                GetSubCaste();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
        }
    }


}