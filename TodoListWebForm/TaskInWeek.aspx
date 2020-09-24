<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TaskInWeek.aspx.cs"
    Inherits="TodoListWebForm.TaskInWeek" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid task-in-week">
        <div class="row">
            <div class="col task-in-week__col">
                <div class="task-in-week__title">Monday</div>
                <div class="task-in-week__items">
                    <asp:DataList
                        ClientIDMode="Predictable"
                        ID="mondayDataList" runat="server">
                        <ItemTemplate>
                            <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                                <div class="task">
                                    <div class="d-flex">
                                        <div class="task__private">
                                            <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'>
                                            </i>
                                            <span class="mr-1"></span>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                        </div>
                                        <div class="d-flex justify-content-between align-items-start">
                                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-dark text-white" : "task__status bg-success text-white") %>'
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
                                            DataSource='<%# Eval("Users") %>'>
                                            <ItemTemplate>
                                                <a href="/usersDetail?id=<%# Eval("id") %>">
                                                    <div data-toggle="tooltip" data-placement="top" class="task__users__item" title='<%# Eval("name") %>'>
                                                        <i class="fas fa-user"></i>
                                                    </div>
                                                </a>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="d-flex justify-content-end mt-1">
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
                    hi
                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Wednesday</div>
                <div class="task-in-week__items">
                    hi
                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Thursday</div>
                <div class="task-in-week__items">
                    hi
                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Friday</div>
                <div class="task-in-week__items">
                    hi
                </div>
            </div>

            <div class="col task-in-week__col">
                <div class="task-in-week__title">Saturday</div>
                <div class="task-in-week__items">
                    hi
                </div>
            </div>
        </div>
    </div>
</asp:Content>
