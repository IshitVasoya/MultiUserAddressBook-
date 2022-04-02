<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="AdminPanel_Country_CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
          <h2>Country List</h2>  
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
          <asp:Label runat="server" Id="lblMessage" EnableViewState="False" ForeColor="Red" /><br />
          <asp:Label runat="server" Id="lblSucces" EnableViewState="False" ForeColor="Green" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-12">
            <div>
                <asp:HyperLink runat="server" ID="hlAddCountry" Text="Add New Country" CssClass="btn btn-default" NavigateUrl="~/AdminPanel/Country/Add" />
            </div><br />
            <div>
                <asp:GridView ID="gvCountry" runat="server" CssClass="table table-hover table-responsive overflowx table-success" AutoGenerateColumns="false" OnRowCommand="gvCountry_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" 
                                    CommandName="DeleteRecord" CommandArgument='<%# Eval("CountryID").ToString() %>' OnClientClick="return confirm('Are you sure you want to delete?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit ">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-primary" NavigateUrl='<%# "~/AdminPanel/Country/Edit/" + CommonDropDownFillMethods.Base64Encode( Eval("CountryID").ToString()) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField  DataField="CountryID" HeaderText="ID" />--%>
                        <asp:BoundField  DataField="CountryName" HeaderText="Country" />
                        <asp:BoundField  DataField="CountryCode" HeaderText="CountryCode" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

