<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TodoListWebForm.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <h3 class="font-weight-bold">Table Tasks</h3>
                <div class="task__wrap">
                    <asp:GridView BorderColor="#eeeeee" DataKeyNames="id" CssClass="table" runat="server" ID="tasksGridView" AutoGenerateColumns="False" OnRowDeleting="HandleDeleteTask">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <span><%# Eval("id") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <a class="text-primary" target="_blank" href="/tasksDetail?id=<%# Eval("id") %>"><%# Eval("title") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ControlStyle-Width="200px" HeaderText="Employee">
                                <ItemTemplate>
                                    <asp:DataList
                                        BorderWidth="0"
                                        runat="server"
                                        DataSource='<%# Eval("Partners") %>'>
                                        <ItemTemplate>
                                            <div title='<%# Eval("email") %>'>
                                                <i class="fas fa-user"></i>
                                                <%# Eval("name") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField ControlStyle-Width="200px" ControlStyle-CssClass="form-control" DataField="startDate" HeaderText="Start Date"></asp:BoundField>
                            <asp:BoundField ControlStyle-Width="200px" ControlStyle-CssClass="form-control" DataField="endDate" HeaderText="End Date"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label CssClass='<%# Convert.ToBoolean(Eval("private")) ? "badge badge-dark" : "badge badge-warning" %>' ID="Label4" runat="server" Text='<%# Convert.ToBoolean(Eval("private")) ? "Private" : "Public" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ControlStyle-CssClass="btn btn-danger btn-sm" ButtonType="Button" HeaderText="Actions" ShowDeleteButton="True" ShowHeader="True" DeleteText="Delete" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="col-12">
                <div>
                    <h3 class="font-weight-bold">Form add task</h3>
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
                        <asp:GridView CssClass="table" BorderColor="#eeeeee" EnablePersistedSelection="true" DataKeyNames="id" ID="usersGridView" runat="server" AutoGenerateColumns="False">
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
                    <asp:Button CssClass="btn btn-primary" Text="Add new task" runat="server" OnClick="HandleCreateTask" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
