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

public partial class AdminPanel_State_StateAddEdit : System.Web.UI.Page
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
            if (Page.RouteData.Values["StateID"] != null)
            {
                lblHeading.Text += "<h2>Edit Mode</h2>";
                FillStateControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["StateID"].ToString())));
            }
            else
            {
                lblHeading.Text = "<h2>Add Mode</h2>";
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
        SqlString strStateName = SqlString.Null;
        SqlString strStateCode = SqlString.Null;
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
            if (txtStateName.Text.Trim() == "")
            {
                strErrorMessge += "- Enter StateName  <br />";
            }
            if (txtStateCode.Text.Trim() == "")
            {
                strErrorMessge += "- Enter StateCode  <br />";
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
            if (txtStateName.Text.Trim() != "")
            {
                strStateName = txtStateName.Text.Trim();
            }
            if (txtStateCode.Text.Trim() != "")
            {
                strStateCode = txtStateCode.Text.Trim();
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
            objCmd.Parameters.AddWithValue("@StateName", strStateName);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@StateCode", strStateCode);

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["StateID"] != null)
            {
                #region Update Record
                //edit Mode 
                objCmd.Parameters.AddWithValue("@StateID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["StateID"].ToString()));
                objCmd.CommandText = "[dbo].[PR_State_UpdateByUserIDStateID]";
                objCmd.ExecuteNonQuery();
                lblSucces.Text = "Data Updated";
                Response.Redirect("~/AdminPanel/State/List");

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_State_Insert]";
                objCmd.ExecuteNonQuery();
                txtStateName.Text = "";
                txtStateCode.Text = "";
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

    #region Button : Cancel
    protected void btnCancal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/State/List", true);
    }

    #endregion Button : Cancel

    #region Fill CountryList
    private void FillCountryList()
    {
        CommonDropDownFillMethods.FillDropDownListCountry(ddlCountryID, Session["UserID"].ToString().Trim());
    }
    #endregion Fill CountryList

    #region FillStateControls
    private void FillStateControls(SqlInt32 StateID)
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
            objCmd.CommandText = "[dbo].[PR_State_SelectByUserIDStateID]";
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@StateID", StateID.ToString().Trim());

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
                    if (!objSDR["StateName"].Equals(DBNull.Value))
                    {
                        txtStateName.Text = objSDR["StateName"].ToString().Trim();
                    }
                    if (!objSDR["StateCode"].Equals(DBNull.Value))
                    {
                        txtStateCode.Text = objSDR["StateCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No Data Available For the StateID = " + StateID.ToString().Trim();
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

    #endregion FillStateControls
}