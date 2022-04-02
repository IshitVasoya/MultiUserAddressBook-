<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanel_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
              <h2>Contact List</h2>  
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
              <asp:Label runat="server" Id="lblMessage" EnableViewState="false" ForeColor="Red" /><br />
          <asp:Label runat="server" Id="lblSucces" EnableViewState="False" ForeColor="Green" /> 
            </div>
        </div><br />
        <div class="row">
            <div class="col-md-12">
                <div>
                    <asp:HyperLink runat="server" ID="hlAddContact" Text="Add New Contact" CssClass="btn btn-default" NavigateUrl="~/AdminPanel/Contact/Add" />
                </div><br />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" style="overflow: scroll;">
                    <asp:GridView ID="gvContact" runat="server" CssClass="table table-hover table-responsive overflowx table-success" AutoGenerateColumns="false" OnRowCommand="gvContact_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" 
                                        CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID").ToString() %>' OnClientClick="return confirm('Are you sure you want to delete?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit ">
                                <ItemTemplate>
                                    <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-primary" NavigateUrl='<%# "~/AdminPanel/Contact/Edit/" + CommonDropDownFillMethods.Base64Encode( Eval("ContactID").ToString()) %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:BoundField  DataField="ContactID" HeaderText="ID" />--%>
                            <asp:BoundField  DataField="CountryName" HeaderText="Country Name" />
                            <asp:BoundField  DataField="StateName" HeaderText="State Name" />
                            <asp:BoundField  DataField="CityName" HeaderText="Citry Name" />
                            <asp:BoundField  DataField="ContactCategoryName" HeaderText="ContactCategory Name" />
                            <asp:BoundField  DataField="ContactName" HeaderText="Contact Name" />
                            <asp:TemplateField HeaderText="Contact Photo">
                                <ItemTemplate>
                                    <asp:Image runat="server" ID="imgContactPhotoPath" ImageUrl='<%# Eval("ContactPhotoPath") %>' Height="50px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField  DataField="PhotoAttribute" HeaderText="Photo Attribute" />
                            <asp:BoundField  DataField="ContactNo" HeaderText="Contact No" />
<%--                            <asp:BoundField  DataField="WhatsAppNo" HeaderText="WhatsApp No" />--%>
                            <asp:BoundField  DataField="BirthDate" DataFormatString="{0:dd'/'MM'/'yyyy}" HeaderText="BirthDate" />
                            <asp:BoundField  DataField="Email" HeaderText="Email" />
                            <asp:BoundField  DataField="Age" HeaderText="Age" />
                            <asp:BoundField  DataField="Address" HeaderText="Address" />
                            <%--<asp:BoundField  DataField="BloodGroup" HeaderText="BloodGroup" />
                            <asp:BoundField  DataField="FacebookID" HeaderText="FacebookID" />
                            <asp:BoundField  DataField="LinkedINID" HeaderText="LinkedINID" />--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
    </div>
</asp:Content>

