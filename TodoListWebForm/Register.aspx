<%@ Page Title="REGISTER" MasterPageFile="~/Site.Master"  Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TodoListWebForm.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-4 card">
                <h2><b><%: Title %></b></h2>   
                <div class="<%: !lbError.Text.Equals("") ? "alert alert-danger" : "" %>" role="alert">
                  <asp:Label Text="" runat="server" ID="lbError" />
                </div>
                <div class="form-group">
                    <label for="tbName">Name</label>
                    <div class="with-effect">
                        <asp:TextBox placeholder="Input your name" ID="tbName" class="effect w-100" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Name is required" ControlToValidate="tbName" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbEmail">Email address</label>
                    <div class="with-effect">
                        <asp:TextBox TextMode="Email" placeholder="Input your email" ID="tbEmail" class="effect w-100" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Email is required" ControlToValidate="tbEmail" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbPassword">Password</label>
                    <div class="with-effect">
                        <asp:TextBox placeholder="Input your password" class="effect w-100" ID="tbPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Password is required" ControlToValidate="tbPassword" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbConfirmPassword">Confirm Password</label>
                    <div class="with-effect">
                        <asp:TextBox placeholder="Input your confirm password" class="effect w-100" ID="tbConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <span class="focus-border"></span>
                    </div>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Confirm Password is required" ControlToValidate="tbConfirmPassword" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary w-100" Text="  REGISTER" OnClick="HandleClickRegister" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

