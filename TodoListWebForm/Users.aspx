<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TodoListWebForm.Users" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <h3>USER MANAGER</h3>
    <asp:GridView CssClass="table table-dark" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id">
        <Columns>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="name" HeaderText="Name" SortExpression="name" />
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="email" HeaderText="Email" SortExpression="email" />
            <asp:BoundField ControlStyle-CssClass="form-control" ReadOnly="true" DataField="role" HeaderText="Role" SortExpression="role" />
            <asp:ButtonField ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" CommandName="Delete" Text="Delete" />
            <asp:ButtonField ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" CommandName="Edit" HeaderText="" ShowHeader="True" Text="Edit" />
        </Columns>
    </asp:GridView>
</asp:Content>

