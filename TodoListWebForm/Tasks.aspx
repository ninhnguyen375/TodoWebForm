<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="TodoListWebForm.Tasks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-12">
                <h3 class="font-weight-bold">Table Tasks</h3>
                <div class="row my-3">
                    <div class="col-12">
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                            <i class="fas fa-plus mr-1"></i>Add new task
                       
                        </button>
                    </div>
                </div>
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

                            <asp:TemplateField ControlStyle-Width="180px" HeaderText="Employee">
                                <ItemTemplate>
                                    <asp:DataList
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


                            <asp:BoundField ControlStyle-Width="180px" ControlStyle-CssClass="form-control" DataField="startDate" HeaderText="Start Date"></asp:BoundField>
                            <asp:BoundField ControlStyle-Width="180px" ControlStyle-CssClass="form-control" DataField="endDate" HeaderText="End Date"></asp:BoundField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Scope">
                                <ItemTemplate>
                                    <asp:Label CssClass='<%# Convert.ToBoolean(Eval("private")) ? "badge badge-dark" : "badge badge-warning" %>' ID="Label4" runat="server" Text='<%# Convert.ToBoolean(Eval("private")) ? "Private" : "Public" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ControlStyle-CssClass="btn btn-danger btn-sm" ButtonType="Button" HeaderText="Actions" ShowDeleteButton="True" ShowHeader="True" DeleteText="Delete" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col-2">
                        <h3>Monday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="mondayDataList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-2">
                        <h3>Tuesday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="tuesdayDataList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-2">
                        <h3>Wednesday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="wednesdayDataList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-2">
                        <h3>Thursday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="thursdayDataList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-2">
                        <h3>Friday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="fridayDataList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:DataList
                                        runat="server"
                                        DataSource='<%# Eval("Partners") %>'>
                                        <ItemTemplate>
                                            <div class="task-of-week__user" title='<%# Eval("email") %>'>
                                                <i class="fas fa-user"></i>
                                                <%# Eval("name") %>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="col-2">
                        <h3>Saturday</h3>
                        <asp:DataList CssClass="task-of-week" runat="server" ID="saturdayDatList">
                            <ItemTemplate>
                                <div class="task-of-week__item">
                                    <p class="task-of-week__title"><%# Eval("title") %></p>
                                    <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "badge badge-primary" : (Eval("status").Equals("expired") ? "badge badge-danger" : "badge badge-success") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Form add new task</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div>
                                <div class="form-group">
                                    <label for="title">Title</label>
                                    <asp:TextBox ID="title" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Title is required" ControlToValidate="title" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label for="startDate">Start Date</label>
                                    <asp:TextBox TextMode="Date" ID="startDate" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Start Ddate is required" ControlToValidate="startDate" runat="server" />
                                </div>
                                <div class="form-group">
                                    <label for="endDate">End Date</label>
                                    <asp:TextBox TextMode="Date" ID="endDate" class="form-control" runat="server"></asp:TextBox>
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
                                <div class="d-flex justify-content-end">
                                    <asp:Button CssClass="btn btn-primary" Text="Add new task" runat="server" OnClick="HandleCreateTask" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
