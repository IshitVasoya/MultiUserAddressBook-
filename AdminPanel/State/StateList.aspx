<%@ Page Title="" Language="C#" MasterPageFile="~/Content/MultipleUserAddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
          <h2>State List</h2>  
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
          <asp:Label runat="server" Id="lblMessage" EnableViewState="false" ForeColor="Red" /><br />
          <asp:Label runat="server" Id="lblSucces" EnableViewState="False" ForeColor="Green" /><br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div>
                <asp:HyperLink runat="server" ID="hlAddState" Text="Add New State" CssClass="btn btn-default" NavigateUrl="~/AdminPanel/State/Add" />
            </div><br />
            <div>
                <asp:GridView ID="gvState" runat="server" CssClass="table table-hover table-responsive overflowx table-success" AutoGenerateColumns="false" OnRowCommand="gvState_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnDelete" Text="Delete" CssClass="btn btn-danger btn-sm" 
                                    CommandName="DeleteRecord" CommandArgument='<%# Eval("StateID").ToString() %>' OnClientClick="return confirm('Are you sure you want to delete?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit ">
                            <ItemTemplate>
                                <asp:HyperLink runat="server" ID="hlEdit" Text="Edit" CssClass="btn btn-primary" NavigateUrl='<%# "~/AdminPanel/State/Edit/" + CommonDropDownFillMethods.Base64Encode( Eval("StateID").ToString()) %>'></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField  DataField="StateID" HeaderText="ID" />
                        <asp:BoundField  DataField="CountryName" HeaderText="Country Name" />
                        <asp:BoundField  DataField="StateName" HeaderText="State Name" />
                        <asp:BoundField  DataField="StateCode" HeaderText="State Code" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

