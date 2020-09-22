<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TasksDetail.aspx.cs" Inherits="TodoListWebForm.TasksDetail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>Task Detail</h3>
        <div class="form-group">
            <label for="title">Title</label>
            <asp:TextBox ID="title" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Title is required" ControlToValidate="title" runat="server" />
            </div>
        <div class="form-group">
            <label for="startDate">Start Date</label>
            <asp:TextBox TextMode="DateTimeLocal" ID="startDate" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Start Ddate is required" ControlToValidate="startDate" runat="server" />
            </div>
        <div class="form-group">
            <label for="endDate">End Date</label>
            <asp:TextBox TextMode="DateTimeLocal" ID="endDate" class="form-control" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="End Date is required" ControlToValidate="endDate" runat="server" />
            </div>
        <div class="form-group">
            <label for="endDate">Select partner</label>
            <asp:GridView EnablePersistedSelection="true"  DataKeyNames="id" ID="usersGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="checkbox" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField ControlStyle-CssClass="form-control" DataField="id" HeaderText="ID">
                        <ControlStyle CssClass="form-control"></ControlStyle>
                    </asp:BoundField>
                    <asp:BoundField ControlStyle-CssClass="form-control" DataField="name" HeaderText="Name">
                        <ControlStyle CssClass="form-control"></ControlStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="form-group">
            <label for="endDate">Private</label>
            <asp:CheckBox runat="server" ID="privateScope" />
            </div>
        <asp:Button Text="Add new task" runat="server" />
    </div>
</asp:Content>