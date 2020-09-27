<%@ Page Title="LOGIN" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="TodoListWebForm.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <h2><b><%: Title %></b></h2>

                <div class="form-group">
                    <label for="tbEmail">Email address</label>
                    <asp:TextBox ID="tbEmail" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Email is required"
                        ControlToValidate="tbEmail" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbPassword">Password</label>
                    <asp:TextBox class="form-control" ID="tbPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Password is required"
                        ControlToValidate="tbPassword" runat="server" />
                </div>
                <asp:Label Text="Email or password incorrect" runat="server" ID="lbError" class="form-text text-danger" />
                <div class="mt-2"></div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary" Text="  LOGIN" OnClick="HandleClickLogin" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
