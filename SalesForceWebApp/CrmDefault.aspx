<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmDefault.aspx.cs" Inherits="SalesForceWebApp.CrmDefault" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
    
<body>
    <style type="text/css">
        .Buttons
        {
            margin-top: 50px;
            margin-left: 50px;
            margin-right: 50px;
            margin-bottom: 50px;
        }
        </style>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnLogin" runat="server" CssClass="Buttons" Text="Login" />
        <asp:Button ID="btnRegister" runat="server" CssClass="Buttons" Text="Register" OnClick="btnRegister_Click" />
    </div>
    </form>
</body>
</html>
