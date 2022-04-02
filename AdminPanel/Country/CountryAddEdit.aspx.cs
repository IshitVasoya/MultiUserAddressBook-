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

public partial class AdminPanel_Country_CountryAddEdit : System.Web.UI.Page
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
            if (Page.RouteData.Values["CountryID"] != null)
            {
                lblHeading.Text += "<h2>Edit Mode</h2>";
                FillCountryControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CountryID"].ToString())));
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

        SqlString strCountryName = SqlString.Null;
        SqlString strCountryCode = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        #endregion Local Variables

        try
        {
            #region Server Side Validation
            String strErrorMessge = "";

            if (txtCountryName.Text.Trim() == "")
            {
                strErrorMessge += "- Enter CountryName  <br />";
            }
            if (txtCountryCode.Text.Trim() == "")
            {
                strErrorMessge += "- Enter CountryCode  <br />";
            }

            if (strErrorMessge != "")
            {
                lblMessage.Text = strErrorMessge;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information
            
            if (txtCountryName.Text.Trim() != "")
            {
                strCountryName = txtCountryName.Text.Trim();
            }
            if (txtCountryCode.Text.Trim() != "")
            {
                strCountryCode = txtCountryCode.Text.Trim();
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
            objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@CountryCode", strCountryCode);

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["CountryID"] != null)
            {
                #region Update Record
                //edit Mode 
                objCmd.Parameters.AddWithValue("@CountryID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["CountryID"].ToString()));
                objCmd.CommandText = "[dbo].[PR_Country_UpdateByUserIDCountryID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/Country/List");
                lblSucces.Text = "Data Updated";

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_Country_Insert]";
                objCmd.ExecuteNonQuery();
                txtCountryName.Text = "";
                txtCountryCode.Text = "";
                txtCountryName.Focus();
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
        Response.Redirect("~/AdminPanel/Country/List", true);
    }

    #endregion Button : Cancel

    #region FillCountryControls
    private void FillCountryControls(SqlInt32 CountryID)
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
            objCmd.CommandText = "[dbo].[PR_Country_SelectByUserIDCountryID]";
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());

            #endregion Set Connection & Command Object

            #region Read the value and set  the controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["CountryName"].Equals(DBNull.Value))
                    {
                        txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                    }
                    if (!objSDR["CountryCode"].Equals(DBNull.Value))
                    {
                        txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No Data Available For the CountryID = " + CountryID.ToString().Trim();
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

    #endregion FillCountryControls
}