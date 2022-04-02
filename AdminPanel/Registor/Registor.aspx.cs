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

public partial class Content_AdminPanel_Registor_Default : System.Web.UI.Page
{
    #region Page load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion Page load

    #region Button Login
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AdminPanel/Login/Login");
    }
    #endregion Button Login

    #region Registor
    protected void btnRegistor_Click(object sender, EventArgs e)
    {
        #region Local Variables

        SqlString strUserName = SqlString.Null;
        SqlString strDisplayName = SqlString.Null;
        SqlString strMobileNO = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlString strPassword = SqlString.Null;
        SqlString strReenterPassword = SqlString.Null;

        #endregion Local Variables

        #region Server Side Validation
        String strErrorMessage = "";

            if (txtUserName.Text.ToString().Trim() == "")
            {
            strErrorMessage += "-Enter User Name<br/>";
            }
            if (txtDisplayName.Text.ToString().Trim() == "")
            {
                strErrorMessage += "-Enter Display Name<br/>";
            }
            if (txtMobileNO.Text.ToString().Trim() == "")
            {
                strErrorMessage += "-Enter Mobile NO<br/>";
            }
            if (txtEmail.Text.ToString().Trim() == "")
            {
                strErrorMessage += "-Enter Email<br/>";
            }
            if (txtPassword.Text.ToString().Trim() == "")
            {
                strErrorMessage += "-Enter Password<br/>";
            }
            if (txtReenterPassword.Text.ToString().Trim() == "")
            {
                strErrorMessage += "-Re-enter Password<br/>";
            }
            if (txtPassword.Text.ToString().Trim() != txtReenterPassword.Text.ToString().Trim())
            {
                strErrorMessage += "-Password must be the same<br/>";
            }
            if (strErrorMessage != "")
            {
                lblMessage.Text = strErrorMessage;
                return;
            }
            
        #endregion Server Side Validation

            #region Assign Value

            if (txtUserName.Text.Trim() != "")
                strUserName = txtUserName.Text.Trim();
            if (txtDisplayName.Text.Trim() != "")
                strDisplayName = txtDisplayName.Text.Trim();
            if (txtMobileNO.Text.Trim() != "")
                strMobileNO = txtMobileNO.Text.Trim();
            if (txtEmail.Text.Trim() != "")
                strEmail = txtEmail.Text.Trim();
            if (txtPassword.Text.Trim() != "")
                strPassword = txtPassword.Text.Trim();
            if (txtReenterPassword.Text.Trim() != "")
                strReenterPassword = txtReenterPassword.Text.Trim();

            #endregion Assign Value

            #region Same User Name

            SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);

            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_User_SelectByUserName";
                objCmd.Parameters.AddWithValue("@Username", strUserName);
               

                SqlDataReader objSDR = objCmd.ExecuteReader();
                
                if(objSDR.HasRows)
                {
                    lblMessage.Text = "User Name is already Exist!! Try Diffrent User Name";
                    return;
                }



            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                if (objConn.State != ConnectionState.Closed)
                    objConn.Close();
            }
            #endregion Same User Name

            #region Connection

            try
            {
                if (objConn.State != ConnectionState.Open)
                    objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_User_Insert";
                objCmd.Parameters.AddWithValue("@Username", strUserName);
                objCmd.Parameters.AddWithValue("@Password", strPassword);
                objCmd.Parameters.AddWithValue("@ConformPassword", strReenterPassword);
                objCmd.Parameters.AddWithValue("@DisplayName", strDisplayName);
                objCmd.Parameters.AddWithValue("@MobileNO", strMobileNO);
                objCmd.Parameters.AddWithValue("@Email", strEmail);

                SqlDataReader objSDR = objCmd.ExecuteReader();


                Response.Redirect("~/AdminPanel/Default");
            }
            catch (Exception ex)
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
    #endregion Registor
}