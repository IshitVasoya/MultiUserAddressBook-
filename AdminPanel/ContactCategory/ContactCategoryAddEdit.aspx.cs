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

public partial class AdminPanel_ContactCategory_ContactCategoryAddEdit : System.Web.UI.Page
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
            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                lblHeading.Text += "<h2>Edit Mode</h2>";
                FillControls((Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString()))));
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

        SqlString strContactCategoryName = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;

        #endregion Local Variables

        try
        {
            #region Server Side Validation
            String strErrorMessge = "";

            if (txtContactCategoryName.Text.Trim() == "")
            {
                strErrorMessge += "- Enter ContactCategoryName  <br />";
            }

            if (strErrorMessge != "")
            {
                lblMessage.Text = strErrorMessge;
                return;
            }
            #endregion Server Side Validation

            #region Gather Information

            if (txtContactCategoryName.Text.Trim() != "")
            {
                strContactCategoryName = txtContactCategoryName.Text.Trim();
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
            objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategoryName);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["ContactCategoryID"] != null)
            {
                #region Update Record
                //edit Mode 
                objCmd.Parameters.AddWithValue("@ContactCategoryID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactCategoryID"].ToString()));
                objCmd.CommandText = "[dbo].[PR_ContactCategory_UpdateByUserIDContactCategoryID]";
                objCmd.ExecuteNonQuery();
                Response.Redirect("~/AdminPanel/ContactCategory/List");
                lblSucces.Text = "Data Updated";

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                objCmd.CommandText = "[dbo].[PR_ContactCategory_Insert]";
                objCmd.ExecuteNonQuery();
                txtContactCategoryName.Text = "";
                txtContactCategoryName.Focus();
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

    #region FillControls
    private void FillControls(SqlInt32 ContactCategoryID)
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
            objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectByUserIDContactCategoryID]";

            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());

            #endregion Set Connection & Command Object


            #region Read the value and set  the controls

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows)
            {
                while (objSDR.Read())
                {
                    if (!objSDR["ContactCategoryName"].Equals(DBNull.Value))
                    {
                        txtContactCategoryName.Text = objSDR["ContactCategoryName"].ToString().Trim();
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No Data Available For the CountryID = " + ContactCategoryID.ToString().Trim();
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

    #endregion FillControls

    #region Button : Cancel
    protected void btnCancal_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/ContactCategory/List", true);
    }
    #endregion Button : Cancel
}