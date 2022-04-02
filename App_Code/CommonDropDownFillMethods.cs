using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommonDropDownFillMethods
/// </summary>
public static class CommonDropDownFillMethods
{
    #region FillDropDownListCountry
    public static void FillDropDownListCountry(DropDownList ddlCountryID, String UserID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_Country_SelectDropDownList]";
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows == true)
            {
                ddlCountryID.DataSource = objSDR;
                ddlCountryID.DataValueField = "CountryID";
                ddlCountryID.DataTextField = "CountryName";
                ddlCountryID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlCountryID.Items.Insert(0, new ListItem("Select Country", "-1"));
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }

        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion FillDropDownListCountry

    #region FillDropDownListState
    public static void FillDropDownListState(DropDownList ddlStateID, String UserID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_State_SelectDropDownList]";
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block

        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block

        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block

        #endregion Connection Establish
    }
    #endregion FillDropDownListState

    #region FillDropDownListCity
    public static void FillDropDownListCity(DropDownList ddlCityID, String UserID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_City_SelectDropDownList]";
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCityID.DataSource = objSDR;
                ddlCityID.DataValueField = "CityID";
                ddlCityID.DataTextField = "CityName";
                ddlCityID.DataBind();
            }
            #endregion Read the Values and set the Controls
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion FillDropDownListCity

    #region FillDropDownListStateByCountryID
    public static void FillDropDownListStateByCountryID(DropDownList ddlStateID, String UserID, SqlInt32 CountryID, DropDownList ddlCityID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_State_SelectDropDownListByCountryID]";
            objCmd.Parameters.AddWithValue("CountryID", SqlDbType.Int).Value = CountryID;
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            }

            else
            {
                ddlStateID.Items.Clear();
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));

                ddlCityID.Items.Clear();
                ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
            }
            #endregion Read the Values and set the Controls

            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion FillDropDownListStateByCountryID

    #region FillDropDownListStateByCountryIDForCity
    public static void FillDropDownListStateByCountryIDForCity(DropDownList ddlStateID, String UserID, SqlInt32 CountryID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_State_SelectDropDownListByCountryID]";
            objCmd.Parameters.AddWithValue("CountryID", SqlDbType.Int).Value = CountryID;
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlStateID.DataSource = objSDR;
                ddlStateID.DataValueField = "StateID";
                ddlStateID.DataTextField = "StateName";
                ddlStateID.DataBind();
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            }

            else
            {
                ddlStateID.Items.Clear();
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
            }
            #endregion Read the Values and set the Controls

            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion FillDropDownListStateByCountryIDForCity

    #region FillDropDownListCityByStateID
    public static void FillDropDownListCityByStateID(DropDownList ddlCityID, String UserID, SqlInt32 StateID)
    {
        #region Connection Establish
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #region Try Block
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
            objCmd.CommandText = "[dbo].[PR_City_SelectDropDownListByStateID]";
            objCmd.Parameters.AddWithValue("StateID", SqlDbType.Int).Value = StateID;
            if (UserID.ToString().Trim() != "")
            {
                objCmd.Parameters.AddWithValue("@UserID", UserID);
            }
            #region Read the Values and set the Controls
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlCityID.DataSource = objSDR;
                ddlCityID.DataValueField = "CityID";
                ddlCityID.DataTextField = "CityName";
                ddlCityID.DataBind();
                ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
            }

            else
            {
                ddlCityID.Items.Clear();
                ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
            }
            #endregion Read the Values and set the Controls

            #endregion Command Object
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
            #endregion Connection
        }
        #endregion Try Block
        #region Catch Block
        catch (Exception ex)
        {

        }
        #endregion Catch Block
        #region Finally Block
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
        #endregion Finally Block
        #endregion Connection Establish
    }
    #endregion FillDropDownListCityByStateID

    #region Base64Encode
    public static string Base64Encode(String PlainText)
    {

        var PlainTextBytes = System.Text.Encoding.UTF8.GetBytes(PlainText);
        return System.Convert.ToBase64String(PlainTextBytes);

    }
    #endregion Base64Encode

    #region Base64Decode
    public static string Base64Decode(String Base64Encodeddata)
    {

        var Base64EncodeddataBytes = System.Convert.FromBase64String(Base64Encodeddata);
        return System.Text.Encoding.UTF8.GetString(Base64EncodeddataBytes);
    }
    #endregion Base64Decode

}