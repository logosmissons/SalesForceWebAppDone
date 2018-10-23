<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmLogin.aspx.cs" Inherits="SalesForceWebApp.CrmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        This Crm Login Page. <br /><br />
        <asp:Label ID="Label1" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox> <br /><br />
    </div>


        <div>
            <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" />
        </div>
        <div>
            <asp:Label ID="lblLoginError" runat="server" Text=""></asp:Label>
        </div>
        <div>
        <asp:GridView ID="gvLogin" runat="server"></asp:GridView>

        </div>
    </form>
</body>
</html>
