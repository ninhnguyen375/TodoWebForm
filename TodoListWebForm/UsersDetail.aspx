<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="UsersDetail.aspx.cs" Inherits="TodoListWebForm.UsersDetail" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <div>
        <h2>User Details</h2>
        <div><b>Name: </b><asp:Label runat="server" ID="lbName" Text=""></asp:Label></div>
        <div><b>Email: </b><asp:Label runat="server" ID="lbEmail" Text=""></asp:Label></div>
        <div><b>Role: </b><asp:Label runat="server" ID="lbRole" Text=""></asp:Label></div>
        <asp:GridView CssClass="table table-borderless" ID="GridTasks" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
                <asp:BoundField DataField="startDate" HeaderText="startDate" 
                    SortExpression="startDate" />
                <asp:BoundField DataField="endDate" HeaderText="endDate" 
                    SortExpression="endDate" />
                <asp:BoundField DataField="status" HeaderText="status" 
                    SortExpression="status" />
                <asp:BoundField DataField="private" HeaderText="private" 
                    SortExpression="private" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
