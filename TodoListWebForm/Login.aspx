<%@ Page Title="LOGIN" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="TodoListWebForm.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-4 card">
                <h2><b><%: Title %></b></h2>

                <div class="form-group">
                    <label for="tbEmail">Email address</label>
                    <div class="with-effect">
                        <asp:TextBox TextMode="Email" placeholder="Input your email" ID="tbEmail" class="effect w-100" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Email is required"
                        ControlToValidate="tbEmail" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbPassword">Password</label>
                    <div class="with-effect">
                        <asp:TextBox placeholder="Input your password" class="effect w-100" ID="tbPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Password is required"
                        ControlToValidate="tbPassword" runat="server" />
                </div>
                <asp:Label Text="Email or password incorrect" runat="server" ID="lbError" class="form-text text-danger" />
                <div class="mt-2"></div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary w-100" Text="  LOGIN" OnClick="HandleClickLogin" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
