using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AdminPanel/Login/Login", true);
        }
        if (!Page.IsPostBack)
        {
            FillCountryList();
            FillStateList();
            if (Page.RouteData.Values["CityID"] != null)
            {
                lblHeading.Text += "<h2>Edit Mode</h2>";
                FillCityControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CityID"].ToString())));
            }
            else
            {
                lblHeading.Text = "<h2>Add Mode</h2>";
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            }
        }
    }

    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        SqlString strPinCode = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            String strErrorMessge = "";

            if (ddlCountryID.SelectedIndex == 0)
            {
                strErrorMessge += "- Select Country  <br />";
            }
            if (ddlStateID.SelectedIndex == 0)
            {
                strErrorMessge += "- Select City  <br />";
            }
            if (txtCityName.Text.Trim() == "")
            {
                strErrorMessge += "- Enter CityName  <br />";
            }
            if (txtSTDCode.Text.Trim() == "")
            {
                strErrorMessge += "- Enter STDCode  <br />";
            }
            if (txtPinCode.Text.Trim() == "")
            {
                strErrorMessge += "- Enter PinCode  <br />";
            }

            if (strErrorMessge != "")
            {
                lblMessage.Text = strErrorMessge;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information

            if (ddlCountryID.SelectedIndex > 0)
            {
                strCountryID = Convert.ToInt32(ddlCountryID.SelectedValue);
            }
            if (ddlStateID.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlStateID.SelectedValue);
            }
            if (txtCityName.Text.Trim() != "")
            {
                strCityName = txtCityName.Text.Trim();
            }
            if (txtSTDCode.Text.Trim() != "")
            {
                strSTDCode = txtSTDCode.Text.Trim();
            }
            if (txtPinCode.Text.Trim() != "")
            {
                strPinCode = txtPinCode.Text.Trim();
            }
            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            #endregion Gather Information

            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
            objCmd.Parameters.AddWithValue("@StateID", strStateID);
            objCmd.Parameters.AddWithValue("@CityName", strCityName);
            objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);
            objCmd.Parameters.AddWithValue("@PinCode", strPinCode);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["CityID"] != null)
            {
                #region Update Record
                //edit Mode 
                objCmd.Parameters.AddWithValue("@CityID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CityID"].ToString()));
                objCmd.CommandText = "[dbo].[PR_City_UpdateByUserIDCityID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/City/List");
                lblSucces.Text = "Data Updated";

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_City_Insert]";
                objCmd.ExecuteNonQuery();
                clearcontrols();
                ddlCountryID.Focus();
                lblSucces.Text = "Data Inserted";

                #endregion Insert Record
            }

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion Button : Save

    #region Clear Controls
    private void clearcontrols()
    {
        txtCityName.Text = "";
        txtSTDCode.Text = "";
        txtPinCode.Text = "";
    }
    #endregion Clear Controls

    #region Button : Cancel
    protected void btnCancal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/City/List", true);
    }

    #endregion Button : Cancel

    #region Fill CountryList
    private void FillCountryList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Session["UserID"].ToString().Trim());
    }
    #endregion Fill CountryList

    #region Fill StateList
    private void FillStateList()
    {
        CommonDropDownFillMethods.FillDropDownListState(ddlStateID, Session["UserID"].ToString().Trim());
    }
    #endregion Fill StateList

    #region Fill State By Country
    protected void ddlCountryID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryID.SelectedIndex > 0)
        {
            FillDropDownListStateByCountryID(Convert.ToInt32(ddlCountryID.SelectedValue));
        }
        else
        {
            ddlStateID.Items.Clear();
            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
        }
    }
    #endregion Fill State By Country

    #region FillDropDownList State by CountryID
    private void FillDropDownListStateByCountryID(SqlInt32 CountryID)
    {
        CommonDropDownFillMethods.FillDropDownListStateByCountryIDForCity(ddlStateID, Session["UserID"].ToString().Trim(), CountryID);
    }
    #endregion FillDropDownList State by CountryID

    #region FillCityControls
    private void FillCityControls(SqlInt32 CityID)
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variables

        try
        {
            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_City_SelectByUserIDCityID]";
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());


            #endregion Set Connection & Command Object

            #region Read the value and set  the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryID"].Equals(DBNull.Value))
                    {
                        ddlCountryID.SelectedValue = objSDR["CountryID"].ToString().Trim();
                    }
                    if (!objSDR["StateID"].Equals(DBNull.Value))
                    {
                        ddlStateID.SelectedValue = objSDR["StateID"].ToString().Trim();
                    }
                    if (!objSDR["CityName"].Equals(DBNull.Value))
                    {
                        txtCityName.Text = objSDR["CityName"].ToString().Trim();
                    }
                    if (!objSDR["STDCode"].Equals(DBNull.Value))
                    {
                        txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                    }
                    if (!objSDR["PinCode"].Equals(DBNull.Value))
                    {
                        txtPinCode.Text = objSDR["PinCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No Data Available For the CityID = " + CityID.ToString().Trim();
            }

            #endregion Read the value and set  the controls

            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
                objConn.Close();
        }
    }

    #endregion FillCityControls
}