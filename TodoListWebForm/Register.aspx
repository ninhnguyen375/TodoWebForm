<%@ Page Title="Register" MasterPageFile="~/Site.Master"  Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TodoListWebForm.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-4">
                <h2><%: Title %></h2>   
                <div class="<%: !lbError.Text.Equals("") ? "alert alert-danger" : "" %>" role="alert">
                  <asp:Label Text="" runat="server" ID="lbError" />
                </div>
                <div class="form-group">
                    <label for="tbName">Name</label>
                    <asp:TextBox ID="tbName" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Name is required" ControlToValidate="tbName" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbEmail">Email address</label>
                    <asp:TextBox ID="tbEmail" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Email is required" ControlToValidate="tbEmail" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbPassword">Password</label>
                    <asp:TextBox class="form-control" ID="tbPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Password is required" ControlToValidate="tbPassword" runat="server" />
                </div>
                <div class="form-group">
                    <label for="tbConfirmPassword">Confirm Password</label>
                    <asp:TextBox class="form-control" ID="tbConfirmPassword" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="text-danger" ErrorMessage="Confirm Password is required" ControlToValidate="tbConfirmPassword" runat="server" />
                </div>
                <div class="form-group">
                    <asp:Button CssClass="btn btn-primary w-100" Text="Register" OnClick="HandleClickRegister" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

