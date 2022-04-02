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

public partial class Content_AdminPanel_Login_Default : System.Web.UI.Page
{
    #region Page load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page load

    #region Button Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        #region Local Variables

        SqlString strUserName = SqlString.Null;
        SqlString strPassword = SqlString.Null;

        #endregion Local Variables

        #region Server Side Validation
        String strErrorMessage = "";
            
        if(txtUserName.Text.ToString().Trim()=="")
        {
            strErrorMessage += "-Enter User Name<br/>";
        }
        if (txtPassword.Text.ToString().Trim() == "")
        {
            strErrorMessage += "-Enter Password<br/>";
        }
        if(strErrorMessage!="")
        {
            lblMessage.Text = strErrorMessage;
            return;
        }
        #endregion Server Side Validation

        #region Assign Value

        if(txtUserName.Text.Trim()!="")
            strUserName = txtUserName.Text.Trim();
        if (txtPassword.Text.Trim() != "")
            strPassword = txtPassword.Text.Trim();

        #endregion Assign Value

        #region Connection

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_User_SelectByUserNamePassword";
            objCmd.Parameters.AddWithValue("@Username", strUserName);
            objCmd.Parameters.AddWithValue("@Password", strPassword);

            SqlDataReader objSDR = objCmd.ExecuteReader();

            if(objSDR.HasRows)
            {
                while(objSDR.Read())
                {
                    if (!objSDR["UserID"].Equals(DBNull.Value))
                    {
                        Session["UserID"] = objSDR["UserID"].ToString().Trim();
                    }
                    if(!objSDR["DisplayName"].Equals(DBNull.Value))
                    {
                        Session["DisplayName"] = objSDR["DisplayName"].ToString().Trim();
                    }
                    if (!objSDR["UserName"].Equals(DBNull.Value))
                    {
                        Session["UserName"] = objSDR["UserName"].ToString().Trim();
                    }
                    break;
                }
                Response.Redirect("~/AdminPanel/Default");
            }
            else
            {
                lblMessage.Text = "Either UserName or Password is not Valid";
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        finally
        {
            if (objConn.State != ConnectionState.Closed)
                objConn.Close();
        }

        #endregion Connection

    }
    #endregion Button Login

    #region Registor
    public string configurationManager { get; set; }
    protected void btnRegistor_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Registor/Registor");
    }
    #endregion Registor
}