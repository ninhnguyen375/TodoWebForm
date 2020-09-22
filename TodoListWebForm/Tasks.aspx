<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TodoListWebForm.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h3>Table Tasks</h3>
        <asp:GridView DataKeyNames="id" CssClass="table table-borderless" runat="server" ID="tasksGridView" AutoGenerateColumns="False" OnRowDeleting="HandleDeleteTask">
        <Columns>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="id" HeaderText="ID">
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="title" HeaderText="Title" >
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="startDate" HeaderText="Start Date">
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="endDate" HeaderText="End Date" >
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="status" HeaderText="Status" >
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="form-control" DataField="private" HeaderText="Private" >
<ControlStyle CssClass="form-control"></ControlStyle>
            </asp:BoundField>
            <asp:CommandField ButtonType="Button" HeaderText="Actions" ShowDeleteButton="True" ShowHeader="True" />
        </Columns>
        </asp:GridView>

        <div>
            <h3>Form add task</h3>
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
            <asp:Button Text="Add new task" runat="server" OnClick="HandleCreateTask" />
        </div>
    </div>
</asp:Content>