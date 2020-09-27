<%@ Page MasterPageFile="~/Site.Admin.Master" Language="C#" AutoEventWireup="true"
    CodeBehind="UsersDetail.aspx.cs" Inherits="TodoListWebForm.UsersDetail" %>

<asp:Content ID="BodyContentAdmin" ContentPlaceHolderID="MainContentAdmin" runat="server">
    <div class="row">
        <div class="col card">
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
    </div>
</asp:Content>
