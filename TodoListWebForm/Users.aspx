<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TodoListWebForm.Users" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <h3><b>USER MANAGER</b></h3>
    <asp:GridView
        BorderColor="#eeeeee"
        OnRowDeleting="GridView1_RowDeleting"
        CssClass="table"
        ID="GridView1" 
        runat="server" 
        AutoGenerateColumns="False" 
        DataKeyNames="id"
        OnRowEditing="GridView1_RowEditing"
        OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating"
        OnRowDataBound="gvChild_RowDataBound"
        >
        <Columns>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="name" HeaderText="NAME" >
            </asp:BoundField>
            <asp:BoundField ReadOnly="true" DataField="email" HeaderText="EMAIL" SortExpression="email" />
            <asp:BoundField ReadOnly="true" DataField="role" HeaderText="ROLE" SortExpression="role" />
            <asp:CommandField
                ItemStyle-HorizontalAlign="Center" 
                ControlStyle-CssClass="btn btn-primary font-weight-bold mt-1 btn-sm"
                ButtonType="Button" 
                HeaderText="ACTION" 
                ShowDeleteButton="True"
                HeaderStyle-CssClass="text-center"
                EditText="  EDIT"
                DeleteText="  DELETE"
                UpdateText="  UPDATE"
                CancelText="  CANCEL"
                ShowEditButton="True" ShowHeader="True">
            </asp:CommandField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink 
                        CssClass="btn btn-light mt-1 btn-sm"
                        ID="HyperLink1" 
                        runat="server" 
                        NavigateUrl='<%# Eval("id", "/usersDetail?id={0}") %>'
                        Text="Details">
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

