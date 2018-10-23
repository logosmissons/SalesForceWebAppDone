<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmShowUserProfile.aspx.cs" Inherits="SalesForceWebApp.CrmShowUserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="User Profile: "></asp:Label><br />
        <asp:GridView ID="gvUserProfile" runat="server"></asp:GridView>
        <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" />

    </div>
    </form>
</body>
</html>
