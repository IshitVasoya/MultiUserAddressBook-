<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Add Edit Page</h2>
        </div>
    </div><br />
    <div class="row">
            <div class="col-md-7">
                <asp:Label runat="server" ID="lblHeading"></asp:Label>
            </div>
    </div><br />
    <div class="row">
        <div class="col-md-12">
          <asp:Label runat="server" Id="lblMessage" EnableViewState="False" ForeColor="Red" /><br />
          <asp:Label runat="server" Id="lblSucces" EnableViewState="False" ForeColor="Green" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            Country
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div><br />
     <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            State 
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlStateID" CssClass="form-control" OnSelectedIndexChanged="ddlStateID_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div><br />
     <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            City
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlCityID" CssClass="form-control"></asp:DropDownList>
        </div>
    </div><br />
     <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            ContactCategory
        </div>
        <div class="col-md-4">
            <asp:CheckBoxList runat="server" ID="cblContactCategoryID" RepeatDirection="Horizontal"></asp:CheckBoxList>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            Contact Name
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" PlaceHolder="Enter Contact Name" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            Contact No
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control" TextMode="Number" ValidationGroup="contactno" PlaceHolder="Enter Contact Number" />
        </div>
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="revContactNO" runat="server" ControlToValidate="txtContactNo" Display="Dynamic" ErrorMessage="Enter Valid Contact Number" ForeColor="#FF3300" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="contactno"></asp:RegularExpressionValidator>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            WhatsApp No
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtWhatsAppNo" CssClass="form-control" TextMode="Number" ValidationGroup="whatsappno" PlaceHolder="Enter WhatsApp Number" />
        </div>
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="revWhatsAppNo" runat="server" ControlToValidate="txtWhatsAppNo" Display="Dynamic" ErrorMessage="Enter Valid WhatsApp Number" ForeColor="#FF3300" ValidationExpression="^[7-9][0-9]{9}$" ValidationGroup="whatsappno"></asp:RegularExpressionValidator>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            BirthDate
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control" ValidationGroup="birthdate" TextMode="Date" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            Email
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ValidationGroup="email" PlaceHolder="Enter Email" TextMode="Email" />
        </div>
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="refEmail" runat="server" ErrorMessage="Enter Validate Email" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="emial"></asp:RegularExpressionValidator>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <a style="color:red;">*</a>
            Age
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtAge" CssClass="form-control" TextMode="Number" PlaceHolder="Enter Age" />
        </div>
        <div class="col-md-4">
            <asp:RangeValidator ID="rvAge" runat="server" ErrorMessage="Enter Valid Age" ControlToValidate="txtAge" Display="Dynamic" ForeColor="#FF3300" MaximumValue="100" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            Address
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" PlaceHolder="Enter Address" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            BloodGroup
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtBloodGroup" CssClass="form-control" PlaceHolder="Enter BloodGroup" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            FacebookID
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtFacebookID" CssClass="form-control" TextMode="Number" PlaceHolder="Enter FaceBookID" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            LinkedINID
        </div>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtLinkedINID" CssClass="form-control" TextMode="Number" PlaceHolder="Enter LinkedINID" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <asp:Label runat="server" Text="Upload Photo" ID="lblUploadPhoto"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Image ID="imgContactPhotoPath" runat="server" Height="250px" Width="250px" /><br /><br />
            <asp:FileUpload ID="fuFileContactPhotoPath" runat="server" />
            <asp:HiddenField ID="hfContactPhotoPath" runat="server" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4">
            <asp:HiddenField runat="server" ID="hfAttribute"></asp:HiddenField>
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-md btn-primary" OnClick="btnSave_Click" />&nbsp&nbsp&nbsp
            <asp:Button runat="server" ID="btnCancal" Text="Cancal" CssClass="btn btn-md btn-danger" OnClick="btnCancal_Click" />
        </div>
    </div>
</asp:Content>

