<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TaskInWeek.aspx.cs"
    Inherits="TodoListWebForm.TaskInWeek" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid task-in-week">
        <div class="row justify-content-between">
            <div class="d-flex align-items-center">
                <asp:TextBox ID="selectWeek" CssClass="form-control mr-3" Width="200" TextMode="Week" runat="server"></asp:TextBox>
                <span class="mr-1">MY TASK ONLY:</span>
                <asp:CheckBox runat="server" ID="cbOnlyMine" />
                <asp:Button EnableViewState=false CssClass="btn btn-primary ml-3 fa-icon" OnClientClick="handleSelectWeek" Text="  FILTER" runat="server" OnClick="handleSelectWeek" />
            </div>
            <button type="button" data-toggle="modal" data-target="#addTaskModal" class="btn btn-primary">
                <i class="fas fa-plus mr-1"></i>
                ADD NEW TASK
            </button>
        </div>
        <div class="row mt-2">
            <div class="col task-in-week__col">
                <div class="task-in-week__title">Monday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="mondayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                            <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                                <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                            </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Tuesday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="tuesdayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                        <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                            <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                        </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Wednesday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="wednesdayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                        <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                            <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                        </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Thursday</div>
               <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="thursdayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                        <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                            <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                        </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Friday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="fridayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                        <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                            <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                        </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>
            
            <div class="col task-in-week__col">
                <div class="task-in-week__title">Saturday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="saturdayDatList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex justify-content-between">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-danger text-white" : "task__status bg-success text-white") %>'
                                                ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                        </div>
                                    </div>
                                    <div class="mt-2">
                                        <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    </div>
                                    <div class="task__users d-flex justify-content-end">
                                        <asp:DataList
                                            ID="DataList2"
                                            runat="server"
                                            DataSource='<%# Eval("Partners") %>'>
                                            <ItemTemplate>
                                                <% if(Session["role"].Equals("admin")) { %>
                                                    <a href="/usersDetail?id=<%# Eval("id") %>">
                                                        <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                        </div>
                                                    </a>
                                                <% } else { %>
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                            <i class="fas fa-user"></i>
                                                    </div>
                                                <% } %>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-between align-items-center mt-1">
                                        <div data-toggle="<%# Convert.ToBoolean(Eval("hasRemind")) ? "tooltip" : "" %>" data-placement="top" title="Admin Has Reminded For This Task">
                                            <i class="<%# Convert.ToBoolean(Eval("hasRemind")) ? "fas fa-bell text-danger" : "" %>" style="font-size: 1.3em"></i>
                                        </div>
                                        <div class="d-flex">
                                            <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                            <span class="">-</span>
                                            <asp:Label CssClass="task__date" ID="Label6" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>
        </div>

        <div class="modal fade" id="addTaskModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">ADD NEW TASK</h5>
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
                                <asp:RequiredFieldValidator Display="Dynamic" CssClass="text-danger" ErrorMessage="End Date is required" ControlToValidate="endDate" runat="server" />
                                <asp:CompareValidator Display="Dynamic" CssClass="text-danger" runat="server" ErrorMessage="End date needs greater than start date" Type="Date" ControlToValidate="endDate" ControlToCompare="startDate" Operator="GreaterThanEqual"></asp:CompareValidator>
                            </div>
                            <div class="form-group">
                                <label for="endDate">File</label>
                                <asp:FileUpload ID="fileInput" class="form-control-file" runat="server"></asp:FileUpload>
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
                                <asp:Label runat="server" AssociatedControlID="privateScope">Private</asp:Label>
                                <asp:CheckBox runat="server" ID="privateScope" />
                            </div>
                            <div class="d-flex justify-content-end">
                                <asp:Button CssClass="btn btn-primary" Text="  ADD NEW TASK" runat="server" OnClick="HandleCreateTask" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
