<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>ContactCategory Add Edit Page</h2>
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
            ContactCategory Name
        </div>
        <div class="col-md-8">
            <asp:TextBox runat="server" ID="txtContactCategoryName" CssClass="form-control" PlaceHolder="Enter ContactCategory Name" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-8">
            <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-md btn-primary" OnClick="btnSave_Click" />
            <asp:Button runat="server" ID="btnCancal" Text="Cancal" CssClass="btn btn-md btn-danger" OnClick="btnCancal_Click" />
        </div>
    </div>
</asp:Content>

