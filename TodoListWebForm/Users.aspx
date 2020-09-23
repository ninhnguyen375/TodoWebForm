﻿<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TodoListWebForm.Users" %>

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
            <%--<asp:BoundField ControlStyle-CssClass="form-control" DataField="name" HeaderText="NAME" >
            </asp:BoundField>--%>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <a class="text-primary" href="/usersDetail?id=<%# Eval("id") %>"><%# Eval("name") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField ReadOnly="true" DataField="email" HeaderText="EMAIL" SortExpression="email" />
            <asp:BoundField ReadOnly="true" DataField="role" HeaderText="ROLE" SortExpression="role" />
            <asp:CommandField
                ItemStyle-HorizontalAlign="Center" 
                ControlStyle-CssClass="btn btn-primary mt-1 btn-sm"
                ButtonType="Button" 
                HeaderText="ACTION" 
                ShowDeleteButton="True"
                HeaderStyle-CssClass="text-center"
                ShowEditButton="True" ShowHeader="True">
            </asp:CommandField>
        </Columns>
    </asp:GridView>
</asp:Content>

