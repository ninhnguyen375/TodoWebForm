<%@ Page MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TodoListWebForm.UserProfile" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-4 card">
                <h4><b>USER PROFILE</b></h4>
                <hr />
                <div class="d-flex justify-content-between">
                    <div><b>Name:</b></div>
                    <div><asp:Label Text="" ID="lbUserName" runat="server" /></div>
                </div>
                <div class="d-flex justify-content-between mt-2">
                    <div><b>Email:</b></div>
                    <div><asp:Label Text="" ID="lbUserEmail" runat="server" /></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
