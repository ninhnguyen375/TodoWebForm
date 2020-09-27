<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="TasksDetail.aspx.cs" Inherits="TodoListWebForm.TasksDetail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h3 class="font-weight-bold">TASK DETAIL</h3>
        <div class="row">
            <div class="col">
                <div class="card">
                    <div class="card-body">
                        <div>
                            <div class="form-group">
                                <label for="title">Title</label>
                                <asp:TextBox TextMode="MultiLine" ID="title" class="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Title is required" ControlToValidate="title" runat="server" />
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-6">
                                    <label for="startDate">Start Date</label>
                                    <asp:TextBox TextMode="Date" ID="startDate" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Start Ddate is required" ControlToValidate="startDate" runat="server" />
                                </div>
                                <div class="form-group col-lg-6">
                                    <label for="endDate">End Date</label>
                                    <asp:TextBox TextMode="Date" ID="endDate" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" CssClass="text-danger" ErrorMessage="End Date is required" ControlToValidate="endDate" runat="server" />
                                    <asp:CompareValidator Display="Dynamic" CssClass="text-danger" runat="server" ErrorMessage="End date needs greater than start date" Type="Date" ControlToValidate="endDate" ControlToCompare="startDate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-lg-6">
                                    <label for="endDate">Partners</label>
                                    <asp:GridView EnablePersistedSelection="true" DataKeyNames="id" ID="usersGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-sm" BorderColor="#eeeeee">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
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
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="status">Status</label>
                                        <select class="form-control" runat="server" id="status">
                                            <option value="inprogress">In Progress</option>
                                            <option value="done">Done</option>
                                            <option value="expired">Expired</option>
                                        </select>
                                        <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Status is required"
                                            ControlToValidate="status" runat="server" />
                                    </div>

                                    <div class="form-group">
                                        <label for="privateScope">Private</label>
                                        <asp:CheckBox runat="server" ID="privateScope" />
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div>
                                        Attached File: <asp:HyperLink Text="Download" ID="attachedFile" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="mt-4 d-flex justify-content-end" style="position: sticky;bottom: 10px">
                                <% if (Session["role"].Equals("admin") || (isOwner)) { %>
                                    <asp:Button OnClientClick="return confirm('Are you sure?')" CssClass="btn btn-danger fa-icon mr-2" OnClick="HandleDeleteTask" Text="  DELETE TASK" runat="server" />
                                    <asp:Button CssClass="btn btn-primary fa-icon" OnClick="handleUpdateTaskDetail" Text="  UPDATE TASK" runat="server" />
                                <% } else if (isPartner) { %>
                                    <asp:Button CssClass="btn btn-primary" OnClick="handleUpdateTaskDetail" Text="UPDATE TASK" runat="server" />
                                <% } %>
                            </div>

                            <div class="row mt-5">
                                <h3 class="font-weight-bold ml-3">User comment:</h3>
                                <asp:DataList CssClass="col-lg-12" ID="commentsDataList" runat="server">
                                    <ItemTemplate>
                                        <div class="comment">
                                            <div class="row">
                                                <div class="col-12">
                                                    <h3 class="comment__name"><%# Eval("name") %> - <%# Eval("email") %></h3>
                                                    <p class="comment__content <%# Convert.ToBoolean(Eval("isRemind")) ? "alert-danger" : "" %>">
                                                        <span class="<%# Convert.ToBoolean(Eval("isRemind")) ? "badge badge-danger p-3" : "" %>">
                                                            <i class="<%# Convert.ToBoolean(Eval("isRemind")) ? "fas fa-bell" : "" %>" style="font-size: 1.7em"></i>
                                                        </span>
                                                        <span class="ml-2"><%# Eval("content") %></span>
                                                    </p>

                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="row mt-4">
                                <div class="col-12">
                                    <asp:TextBox Display="Dynamic" CssClass="w-100 p-4 form-control" TextMode="MultiLine" runat="server" ID="chatBox"></asp:TextBox>
                                    <asp:Button OnClick="handleSubmitComment" CssClass="btn btn-primary mt-2" Text="  SUBMIT COMMENT" runat="server" />
                                    <% if (Session["role"].Equals("admin"))
                                        { %>
                                    <asp:Button CssClass="btn btn-warning ml-2 mt-2" OnClick="handleRemindTask" Text="  REMIND TASK" runat="server" />
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
