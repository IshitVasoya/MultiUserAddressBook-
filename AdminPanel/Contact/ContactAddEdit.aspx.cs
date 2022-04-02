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
public partial class AdminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region check Valid User
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/AdminPanel/Login.aspx", true);
        }
        #endregion check Valid User

        if (!Page.IsPostBack)
        {
            FillCountryList();
            FillStateList();
            FillCityList();
            FillCBLContactCategoryID();
            if (Page.RouteData.Values["ContactID"] != null)
            {
                lblHeading.Text += "<h2>Edit Mode</h2>";
                FillContactControls(Convert.ToInt32(CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString())));
                
            }
            else
            {
                lblHeading.Text = "<h2>Add Mode</h2>";
                ddlStateID.Items.Insert(0, new ListItem("Select State", "-1"));
                ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
            }
        }
    }

    #endregion Load Event

    #region Button : Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region File Content
        String ContactPhotoPath = "";
        String Attribute = "";

        if (fuFileContactPhotoPath.HasFile)
        {
            ContactPhotoPath = "~/UserContent/" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt") + fuFileContactPhotoPath.FileName.ToString().Trim();
            System.Drawing.Image img = System.Drawing.Image.FromStream(fuFileContactPhotoPath.PostedFile.InputStream);
            decimal size = Math.Round(((decimal)fuFileContactPhotoPath.PostedFile.ContentLength / (decimal)1024), 2);
            string FileExtn = System.IO.Path.GetExtension(fuFileContactPhotoPath.PostedFile.FileName);
            string ext = Path.GetExtension(FileExtn);
            int ActualWidth = img.Width;
            int ActualHeight = img.Height;

            Attribute = "File Size - " + size + " KB "
                   + " Height - " + ActualHeight + " px "
                   + " Width - " + ActualWidth + " px "
                  + " File Type - " + ext + " File ";
        }

        if (!Directory.Exists(Server.MapPath("~/UserContent")))
        {
            Directory.CreateDirectory(Server.MapPath("~/UserContent"));
        }

        #endregion File Content

        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);

        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNo = SqlString.Null;
        SqlString strWhatsAppNo = SqlString.Null;
        SqlString strBirthDate = SqlString.Null;
        SqlString strEmail = SqlString.Null;
        SqlInt32 strAge = SqlInt32.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strFacebookID = SqlString.Null;
        SqlString strLinkedINID = SqlString.Null;
        SqlString strOldAttribute = SqlString.Null;
        SqlInt32 strUserID = SqlInt32.Null;
        string LogicalPath = "~/UserContent/" + DateTime.Now.ToString("ddMMyyyyhhmmssffftt");
        string AbsolutePath = "";
        
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
                strErrorMessge += "- Select State  <br />";
            }
            if (ddlCityID.SelectedIndex == 0)
            {
                strErrorMessge += "- Select City  <br />";
            }
            if (txtContactName.Text.Trim() == "")
            {
                strErrorMessge += "- Enter ContactName  <br />";
            }
            if (txtContactNo.Text.Trim() == "")
            {
                strErrorMessge += "- Enter ContactNo  <br />";
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
            if (ddlCityID.SelectedIndex > 0)
            {
                strCityID = Convert.ToInt32(ddlCityID.SelectedValue);
            }
            if (txtContactName.Text.Trim() != "")
            {
                strContactName = txtContactName.Text.Trim();
            }
            if (txtContactNo.Text.Trim() != "")
            {
                strContactNo = txtContactNo.Text.Trim();
            }
            if (txtWhatsAppNo.Text.Trim() != "")
            {
                strWhatsAppNo = txtWhatsAppNo.Text.Trim();
            }
            if (txtBirthDate.Text.Trim() != "")
            {
                strBirthDate = txtBirthDate.Text.Trim();
            }
            if (txtEmail.Text.Trim() != "")
            {
                strEmail = txtEmail.Text.Trim();
            }
            if (txtAge.Text.Trim() != "")
            {
                strAge = Convert.ToInt32(txtAge.Text.Trim());
            }
            if (txtAddress.Text.Trim() != "")
            {
                strAddress = txtAddress.Text.Trim();
            }
            if (txtBloodGroup.Text.Trim() != "")
            {
                strBloodGroup = txtBloodGroup.Text.Trim();
            }
            if (txtFacebookID.Text.Trim() != "")
            {
                strFacebookID = txtFacebookID.Text.Trim();
            }
            if (txtLinkedINID.Text.Trim() != "")
            {
                strLinkedINID = txtLinkedINID.Text.Trim();
            }
            if (fuFileContactPhotoPath.HasFile)
            {
                AbsolutePath = Server.MapPath(LogicalPath);
            }
            if (hfAttribute.Value.Trim() != "")
            {
                strOldAttribute = hfAttribute.Value.Trim();
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
            objCmd.Parameters.AddWithValue("@CityID", strCityID);
            objCmd.Parameters.AddWithValue("@ContactName", strContactName);
            objCmd.Parameters.AddWithValue("@ContactNo", strContactNo);
            objCmd.Parameters.AddWithValue("@WhatsAppNo", strWhatsAppNo);
            objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
            objCmd.Parameters.AddWithValue("@Email", strEmail);
            objCmd.Parameters.AddWithValue("@Age", strAge);
            objCmd.Parameters.AddWithValue("@Address", strAddress);
            objCmd.Parameters.AddWithValue("@BloodGroup", strBloodGroup);
            objCmd.Parameters.AddWithValue("@FacebookID", strFacebookID);
            objCmd.Parameters.AddWithValue("@LinkedINID", strLinkedINID);
            objCmd.Parameters.AddWithValue("@UserID", strUserID);
            //objCmd.Parameters.AddWithValue("@ContactPhotoPath", ContactPhotoPath);

            #endregion Set Connection & Command Object

            if (Page.RouteData.Values["ContactID"] != null)
            {
                #region Update Image
                if (fuFileContactPhotoPath.HasFile)
                {

                    FileInfo file = new FileInfo(Server.MapPath(hfContactPhotoPath.Value.ToString()));
                    if (file.Exists)
                        file.Delete();

                    fuFileContactPhotoPath.SaveAs(AbsolutePath + fuFileContactPhotoPath.FileName);
                    objCmd.Parameters.AddWithValue("ContactPhotoPath", LogicalPath + fuFileContactPhotoPath.FileName);
                    objCmd.Parameters.AddWithValue("@PhotoAttribute", Attribute);
                }
                else
                {
                    objCmd.Parameters.AddWithValue("ContactPhotoPath", hfContactPhotoPath.Value.ToString());
                    objCmd.Parameters.AddWithValue("@PhotoAttribute", strOldAttribute);
                }
                #endregion Update Image

                #region Update Record
                //edit Mode 
                objCmd.Parameters.AddWithValue("@ContactID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString()));
                objCmd.CommandText = "[dbo].[PR_Contact_UpdateByUserIContactID]";
                objCmd.ExecuteNonQuery();

                SqlCommand objCmdContactCategoryDelete = objConn.CreateCommand();
                objCmdContactCategoryDelete.CommandType = CommandType.StoredProcedure;
                objCmdContactCategoryDelete.CommandText = "[dbo].[PR_ContactWiseContactCategory_DeleteByContactID]";
                objCmdContactCategoryDelete.Parameters.AddWithValue("@ContactID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString()));
                objCmdContactCategoryDelete.ExecuteNonQuery();

                foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
                {
                    if (liContactCategoryID.Selected)
                    {
                        SqlCommand objCmdContactCategory = objConn.CreateCommand();
                        objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                        objCmdContactCategory.CommandText = "[dbo].[PR_ContactWiseContactCategory_Insert]";
                        objCmdContactCategory.Parameters.AddWithValue("ContactID", CommonDropDownFillMethods.Base64Decode(Page.RouteData.Values["ContactID"].ToString()));
                        objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString());

                        objCmdContactCategory.ExecuteNonQuery();
                       
                    }
                }
                Response.Redirect("~/AdminPanel/Contact/List");

                lblSucces.Text = "Data Updated";

                #endregion Update Record
            }
            else
            {
                #region Insert Record
                //add mode
                if (!fuFileContactPhotoPath.HasFile)
                {
                    strErrorMessge += "-Please Upload Photo <br/>";
                }

                else
                {
                    objCmd.CommandText = "[dbo].[PR_Contact_Insert]";
                    objCmd.Parameters.AddWithValue("ContactPhotoPath", LogicalPath + fuFileContactPhotoPath.FileName);
                    objCmd.Parameters.AddWithValue("@PhotoAttribute", Attribute);
                    objCmd.Parameters.Add("ContactID", SqlDbType.Int, 4);
                    objCmd.Parameters["ContactID"].Direction = ParameterDirection.Output;
                    objCmd.ExecuteNonQuery();
                    SqlInt32 ContactID = 0;
                    ContactID = Convert.ToInt32(objCmd.Parameters["ContactID"].Value);

                    foreach (ListItem liContactCategoryID in cblContactCategoryID.Items)
                    {
                        if (liContactCategoryID.Selected)
                        {
                            SqlCommand objCmdContactCategory = objConn.CreateCommand();
                            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
                            objCmdContactCategory.CommandText = "[dbo].[PR_ContactWiseContactCategory_Insert]";
                            objCmdContactCategory.Parameters.AddWithValue("ContactID", ContactID.ToString());
                            objCmdContactCategory.Parameters.AddWithValue("ContactCategoryID", liContactCategoryID.Value.ToString());

                            objCmdContactCategory.ExecuteNonQuery();
                        }
                    }
                    fuFileContactPhotoPath.SaveAs(AbsolutePath + fuFileContactPhotoPath.FileName);
                    clearcontrols();
                    ddlCountryID.Focus();
                    lblSucces.Text = "Data Inserted";
                }

                if (strErrorMessge.Trim() != "")
                {
                    lblMessage.Text = strErrorMessge;
                    return;
                }
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
        txtContactName.Text = "";
        txtContactNo.Text = "";
        txtWhatsAppNo.Text = "";
        txtBirthDate.Text = "";
        txtEmail.Text = "";
        txtAge.Text = "";
        txtAddress.Text = "";
        txtBloodGroup.Text = "";
        txtFacebookID.Text = "";
        txtLinkedINID.Text = "";
        ddlCountryID.SelectedIndex = 0;
        ddlStateID.SelectedIndex = 0;
        ddlCityID.SelectedIndex = 0;

    }
    #endregion Clear Controls

    #region Button : Cancel
    protected void btnCancal_Click(object sender, EventArgs e)
    {
        clearcontrols();
        Response.Redirect("~/AdminPanel/Contact/List", true);
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

    #region Fill CityList
    private void FillCityList()
    {
        CommonDropDownFillMethods.FillDropDownListCity(ddlCityID, Session["UserID"].ToString().Trim());
    }

    #endregion Fill CityList

    #region Fill StateList By Country
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

            ddlCityID.Items.Clear();
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
        }
    }
    #endregion Fill StateList By Country

    #region FillDropDownList State by CountryID
    private void FillDropDownListStateByCountryID(SqlInt32 CountryID)
    {
        CommonDropDownFillMethods.FillDropDownListStateByCountryID(ddlStateID, Session["UserID"].ToString().Trim(), CountryID, ddlCityID);
    }
    #endregion FillDropDownList State by CountryID

    #region Fill CityList By State
    protected void ddlStateID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStateID.SelectedIndex > 0)
        {
            FillDropDownListCityByStateID(Convert.ToInt32(ddlStateID.SelectedValue));
        }
        else
        {
            ddlCityID.Items.Clear();
            ddlCityID.Items.Insert(0, new ListItem("Select City", "-1"));
        }
    }
    #endregion Fill CityList By State

    #region FillDropDownList City by StateID
    private void FillDropDownListCityByStateID(SqlInt32 StateID)
    {
        CommonDropDownFillMethods.FillDropDownListCityByStateID(ddlCityID, Session["UserID"].ToString().Trim(), StateID);
    }

    #endregion FillDropDownList City by StateID

    #region FillContactControls
    private void FillContactControls(SqlInt32 ContactID)
    {
        String Attribute = "";
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
            objCmd.CommandText = "[dbo].[PR_Contact_SelectByUserIDContactID]";
            objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
            objCmd.Parameters.AddWithValue("@UserID", strUserID);

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
                    if (!objSDR["CityID"].Equals(DBNull.Value))
                    {
                        ddlCityID.SelectedValue = objSDR["CityID"].ToString().Trim();
                    }
                    if (!objSDR["ContactName"].Equals(DBNull.Value))
                    {
                        txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                    }
                    if (!objSDR["ContactNo"].Equals(DBNull.Value))
                    {
                        txtContactNo.Text = objSDR["ContactNo"].ToString().Trim();
                    }
                    if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                    {
                        txtWhatsAppNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                    }
                    if (!objSDR["BirthDate"].Equals(DBNull.Value))
                    {
                        txtBirthDate.Text = Convert.ToDateTime(objSDR["BirthDate"].ToString()).ToString("yyyy-MM-dd");
                    }
                    if (!objSDR["Email"].Equals(DBNull.Value))
                    {
                        txtEmail.Text = objSDR["Email"].ToString().Trim();
                    }
                    if (!objSDR["Age"].Equals(DBNull.Value))
                    {
                        txtAge.Text = objSDR["Age"].ToString().Trim();
                    }
                    if (!objSDR["Address"].Equals(DBNull.Value))
                    {
                        txtAddress.Text = objSDR["Address"].ToString().Trim();
                    }
                    if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                    {
                        txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                    }
                    if (!objSDR["FacebookID"].Equals(DBNull.Value))
                    {
                        txtFacebookID.Text = objSDR["FacebookID"].ToString().Trim();
                    }
                    if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                    {
                        txtLinkedINID.Text = objSDR["LinkedINID"].ToString().Trim();
                    }
                    if (objSDR["ContactPhotoPath"].Equals(DBNull.Value) != true)
                    {
                        hfContactPhotoPath.Value = objSDR["ContactPhotoPath"].ToString().Trim();
                    }
                    if (objSDR["ContactPhotoPath"].Equals(DBNull.Value) != true)
                    {
                        imgContactPhotoPath.ImageUrl = objSDR["ContactPhotoPath"].ToString().Trim();
                    }
                    if (objSDR["PhotoAttribute"].Equals(DBNull.Value) != true)
                    {
                        Attribute = objSDR["PhotoAttribute"].ToString().Trim();
                        hfAttribute.Value = objSDR["PhotoAttribute"].ToString().Trim();
                    }

                    break;
                }
            }
            objSDR.Close();


            DataTable dt = new DataTable();
            SqlCommand objCmdContactCategory = objConn.CreateCommand();
            objCmdContactCategory.CommandType = CommandType.StoredProcedure;
            objCmdContactCategory.CommandText = "PR_ContactWiseContactCategory_CheckboxList";
            objCmdContactCategory.Parameters.AddWithValue("@ContactID", ContactID);
            SqlDataReader objSDRContactCategory = objCmdContactCategory.ExecuteReader();
            dt.Load(objSDRContactCategory);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == "SELECTED")
                    {
                        cblContactCategoryID.Items[i].Selected = true;
                    }
                }
            }
            else
            {
                lblMessage.Text = "No Data Available For the ContactID = " + ContactID.ToString().Trim();
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

    #region cbl:FillContactCategoryList
    private void FillCBLContactCategoryID()
    {
        #region Local Variables
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["MultipleUserAddressBookConnectionString"].ConnectionString);
        #endregion Local Variables
        try
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            if (Session["UserID"] != null)
                objCmd.Parameters.Add("UserID", SqlDbType.Int).Value = Session["UserID"];
            objCmd.CommandText = "[dbo].[PR_ContactCategory_SelectDropDownList]";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            if (objSDR.HasRows)
            {
                cblContactCategoryID.DataValueField = "ContactCategoryID";
                cblContactCategoryID.DataTextField = "ContactCategoryName";
                cblContactCategoryID.DataSource = objSDR;
                cblContactCategoryID.DataBind();
            }
            if (objConn.State == ConnectionState.Open)
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
    #endregion cbl:FillContactCategoryList
}