<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryList.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>ContacCategory List</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Label runat="server"  ID="lblMessage" EnableViewState="false" ForeColor="Red" /><br />
          <asp:Label runat="server" Id="lblSucces" EnableViewState="False" ForeColor="Green" />
        </div>
    </div><br />
    <div class="row">
        <div class="col-md-12">
            <div>
                <asp:HyperLink runat="server" ID="hlAddContactCategory" Text="Add New ContactCategory" CssClass="btn btn-default" NavigateUrl="~/AdminPanel/ContactCategory/Add" />
            </div><br />
            <div>
                <asp:GridView ID="gvContactCategory" runat="server" CssClass="table table-hover table-responsive overflowx table-success" AutoGenerateColumns="false" OnRowCommand="gvContactCategory_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" 
                                    CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactCategoryID").ToString() %>' OnClientClick="return confirm('Are you sure you want to delete?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit ">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-primary text-decoration-none" NavigateUrl='<%# "~/AdminPanel/ContactCategory/Edit/" + CommonDropDownFillMethods.Base64Encode( Eval("ContactCategoryID").ToString()) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField  DataField="ContactCategoryID" HeaderText="ID" />--%>
                        <asp:BoundField  DataField="ContactCategoryName" HeaderText="ContactCategory Name" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

