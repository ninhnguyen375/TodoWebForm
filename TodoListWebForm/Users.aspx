<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="TodoListWebForm.Users" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <div class="card">
        <div class="card-header"><b>USER MANAGER</b></div>
        <div class="card-body">
            <asp:GridView
                OnRowDeleting="GridView1_RowDeleting"
                CssClass="table table-borderless"
                ID="GridView1" 
                runat="server" 
                AutoGenerateColumns="False" 
                DataKeyNames="id"
                OnRowEditing="GridView1_RowEditing"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowUpdating="GridView1_RowUpdating"
                OnRowDataBound="gvChild_RowDataBound"
                >
                <SelectedRowStyle BackColor="#FFFF66" Font-Bold="True" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                        ReadOnly="True" SortExpression="id" />
                    <asp:BoundField ControlStyle-CssClass="form-control" DataField="name" 
                        HeaderText="name" SortExpression="name" >
                        <ControlStyle CssClass="form-control"></ControlStyle>
                    </asp:BoundField>
                    <asp:BoundField ReadOnly="true" DataField="email" HeaderText="email" SortExpression="email" />
                    <asp:BoundField ReadOnly="true" DataField="role" HeaderText="role" SortExpression="role" />
                    <asp:CommandField
                            ItemStyle-HorizontalAlign="Center" 
                            ControlStyle-CssClass="btn btn-primary mb-2"
                            ItemStyle-CssClass="d-flex"
                            ButtonType="Button" 
                            HeaderText="Action" 
                            ShowDeleteButton="True" 
                            HeaderStyle-CssClass="text-center"
                            ShowEditButton="True" ShowHeader="True">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:CommandField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <button class="btn">
                                <asp:HyperLink 
                                    ID="HyperLink1" 
                                    runat="server" 
                                    NavigateUrl='<%# Eval("id", "/usersDetail?id={0}") %>'
                                    Text="Details"></asp:HyperLink>
                            </button>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

