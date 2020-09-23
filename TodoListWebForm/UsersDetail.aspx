<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="UsersDetail.aspx.cs" Inherits="TodoListWebForm.UsersDetail" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <div class="row">
        <div class="col">
            <h3><b>USER DETAIL</b></h3>
            <div>
                <b>Name: </b>
                <asp:Label runat="server" ID="lbName" Text=""></asp:Label>
            </div>
            <div>
                <b>Email: </b>
                <asp:Label runat="server" ID="lbEmail" Text=""></asp:Label>
            </div>
            <div>
                <b>Role: </b>
                <asp:Label runat="server" ID="lbRole" Text=""></asp:Label>
            </div>
        </div>
        <div class="col">
            <h5><b>Tasks of user:</b></h5>
            <asp:DataList
                ClientIDMode="Predictable"
                ID="DataList1" runat="server">
                <ItemTemplate>
                    <a href='<%# Eval("id", "/tasksDetail?id={0}") %>'>
                        <div class="task">
                        <div class="d-flex justify-content-between mb-4 align-items-start">
                            <asp:Label CssClass="task__title" ID="Label1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                            <asp:Label CssClass='<%# Eval("status").Equals("inprogress") ? "task__status bg-primary text-white" : (Eval("status").Equals("expired") ? "task__status bg-dark text-white" : "task__status bg-success text-white") %>' ID="Label4" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </div>
                        <div class="d-flex justify-content-between">
                            <div class="d-flex">
                                <div class="task__date">
                                    <i class='<%# Convert.ToInt32(Eval("private")) == 1 ? "fas fa-lock" : "fas fa-globe" %>'></i>
                                    <span class="mr-1"></span>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToInt32(Eval("private")) == 1 ? "private" : "public" %>'></asp:Label>
                                </div>
                                <div class="task__users ml-1">
                                    <asp:DataList
                                        ID="DataList2"
                                        runat="server"
                                        DataSource='<%# Eval("Users") %>'>
                                        <ItemTemplate>
                                            <div class="task__users__item" title='<%# Eval("name") %>'>
                                                <i class="fas fa-user"></i>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <div>
                                <asp:Label CssClass="task__date" ID="Label2" runat="server" Text='<%# Eval("startDate") %>'></asp:Label>
                                <span class="ml-1 mr-1">-</span>
                                <asp:Label CssClass="task__date" ID="Label3" runat="server" Text='<%# Eval("endDate") %>'></asp:Label>
                            </div>
                        </div>
                    </div>
                    </a>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Content>
