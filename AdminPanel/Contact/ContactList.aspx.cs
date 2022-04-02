using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_Contact_ContactList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGridView();
        }
    }
    #endregion Load Event

    #region Fill GridView
    private void FillGridView()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open(); //Open The connecction

            SqlCommand objCmd = new SqlCommand();
            objCmd.Connection = objConn;
            objCmd.CommandType = CommandType.StoredProcedure;
            

            if (Session["UserID"] != null)
                objCmd.Parameters.Add("UserID", SqlDbType.Int).Value = Session["UserID"];
            objCmd.CommandText = "[dbo].[PR_ContactWiseContactCategory_SelectAll]";

            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
            }

            if (objConn.State == ConnectionState.Open)
                objConn.Close(); //Close the connection
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

    #endregion Fill GridView

    #region gvContact : RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region Delete Record
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString() != "")
            {
                DeleteContactCategoryFromContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }
        #endregion Delete Record
    }
    #endregion gvContact : RowCommand

    #region Delete Contact Record
    private void DeleteContact(SqlInt32 ContactID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);

        try
        {
            FillControls(ContactID);
            if (objConn.State != ConnectionState.Open)
                objConn.Open();
            SqlInt32 strUserID = SqlInt32.Null;

            if (Session["UserID"] != null)
            {
                strUserID = Convert.ToInt32(Session["UserID"]);
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;

            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
            objCmd.CommandText = "[dbo].[PR_Contact_DeleteByUserIContactID]";
            objCmd.ExecuteNonQuery();

            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }

            lblSucces.Text = "Deleted Successfully";
            FillGridView();
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

    #endregion Delete Contact Record

    #region Delete ContactCategoryFromContact
    private void DeleteContactCategoryFromContact(SqlInt32 ContactID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        try
        {
            #region Connection
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            #region Command Object
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_ContactWiseContactCategory_DeleteByContactID]";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString());
            objCmd.ExecuteNonQuery();

            FillGridView();
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection

            FillGridView();
        }

        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Connection Establish
    }
    #endregion Delete ContactCategoryFromContactList

    private void FillControls(SqlInt32 ContactID)
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        try
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[dbo].[PR_Contact_SelectByPk]";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if(objSDR.HasRows)
            {
                while(objSDR.Read())
                {
                    if(objSDR["ContactPhotoPath"].Equals(DBNull.Value) != true)
                    {
                        String ContactPhotoPath = objSDR["ContactPhotoPath"].ToString().Trim();
                        FileInfo file = new FileInfo(Server.MapPath(ContactPhotoPath));
                        if(file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    break;
                }
            }
            else
            {
                lblMessage.Text = "No Data available for the ContactID = " + ContactID.ToString();
            }

            if(objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
    }

}