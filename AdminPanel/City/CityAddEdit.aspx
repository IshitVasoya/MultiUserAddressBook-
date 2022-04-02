<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AdminPanel_City_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>City Add Edit Page</h2>
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
                    <asp:DropDownList runat="server" ID="ddlCountryID" CssClass="form-control" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    <a style="color:red;">*</a>
                    State
                </div>
                <div class="col-md-8">
                    <asp:DropDownList runat="server" ID="ddlStateID" CssClass="form-control"></asp:DropDownList>
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    <a style="color:red;">*</a>
                    City Name
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control" PlaceHolder="Enter City Name" />
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    STD Code
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtSTDCode" CssClass="form-control" PlaceHolder="Enter STD Code" />
                </div>
            </div><br />
            <div class="row">
                <div class="col-md-4">
                    Pin Code
                </div>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtPinCode" CssClass="form-control" PlaceHolder="Enter Pin Code" />
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

