<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="StateAddEdit.aspx.cs" Inherits="AdminPanel_State_StateAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>State Add Edit Page</h2>
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
        <div class="col-md-6">
            <div class="row">
                <div class="col-md-4">
                    <a style="color:red;">*</a>
                    Country
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control"></asp:DropDownList>
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    <a style="color:red;">*</a>
                    State Name
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtStateName" CssClass="form-control" PlaceHolder="Enter State Name" />
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    State Code
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtStateCode" CssClass="form-control" PlaceHolder="Enter State Code" />
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-8">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="btn btn-md btn-primary" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnCancal" Text="Cancal" CssClass="btn btn-md btn-danger" OnClick="btnCancal_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

