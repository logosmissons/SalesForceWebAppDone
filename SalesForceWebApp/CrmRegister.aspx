<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmRegister.aspx.cs" Inherits="SalesForceWebApp.CrmRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style type="text/css">
        .ValidatorErrorMessage
        {
            color: red;
        }
        
        </style>

    <form id="form1" runat="server">

    <div>
        <asp:Label ID="lblUserEmail" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="txtUserEmail" onblur="validate(this)" runat="server" Text=""></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email required!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserEmail"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Invalid email!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
         <br /><br />
        <asp:Label ID="lblUserFirstName" runat="server" Text="First Name: "></asp:Label>
        <asp:TextBox ID="txtUserFirstName" runat="server" Text=""></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter your first name!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserFirstName"></asp:RequiredFieldValidator> <br /><br />
        <asp:Label ID="lblUserLastName" runat="server" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="txtUserLastName" runat="server" Text=""></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="Please enter your last name!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserLastName"></asp:RequiredFieldValidator> <br /><br />
        <asp:Label ID="lblUserPassword" runat="server" Text="Password: "></asp:Label>
        <asp:TextBox ID="txtUserPassword" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter the password!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserPassword"></asp:RequiredFieldValidator> 
        <asp:CustomValidator ID="cvPassword" runat="server" ErrorMessage="The password should be at least 6 characters long!" CssClass="ValidatorErrorMessage" ControlToValidate="txtUserPassword" OnServerValidate="cvPassword_ServerValidate"></asp:CustomValidator>
        <br /><br />
        <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: "></asp:Label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please enter the password, again!" CssClass="ValidatorErrorMessage" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cvConfirmPassword" runat="server" ErrorMessage="Passwords do not match!" CssClass="ValidatorErrorMessage" ControlToCompare="txtUserPassword" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
    </div>
        <div>
            <asp:ValidationSummary ID="vsNewUser" DisplayMode="BulletList" EnableClientScript="true" CssClass="ValidatorErrorMessage" HeaderText="You must enter your login information!" runat="server" />
        </div>
        <br /><br /><br />
        <div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>
        <br /><br />
        <asp:Label ID="lblNewID" runat="server" Text="New ID: "></asp:Label>
        <asp:TextBox ID="txtNewID" runat="server" Text =""></asp:TextBox>
    </form>
</body>
</html>
