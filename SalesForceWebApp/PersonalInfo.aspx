<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="SalesForceWebApp.PersonalInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="~/Content/PersonalInfo.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
            <p>The Christian Mutual Med-Aid</p>
        </div>

        <asp:Panel ID="pnlPersonalInfo" runat="server" GroupingText="Enter your name and email" HorizontalAlign="Center" BorderWidth="0" BorderStyle="Solid" BorderColor="Black" Width="400px" Height="600px"
                Font-Bold="true" Font-Size="X-Large" style="vertical-align: top; margin-top: 100px; margin-left: auto; margin-right: auto; " >
               <table border="0" style="margin-top: 40px; width: 400px; padding-left: 5px; " >
                    <tr>
                        <td class="tdLabel">First name</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtFirstName" runat="server" Height="30px" Width="300px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 10px;">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="First name is required!"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Last name</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtLastName" runat="server" Height="30px" Width="300px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 10px;">
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Last name is required!"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Email</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtEmail" runat="server" Height="30px" Width="300px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 10px;">
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Email is required!"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Email format is incorrect!"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                   <tr>
                       <td colspan="2" style="align-content: center; margin: 5px; font-size: medium; color:darkblue; ">
                           <p>You will receive the confirmation email after you click <b>'Confirm'</b> button below. Please follow the link in the email to create your Christian Mutual MedAid account.</p>
                       </td>
                   </tr>
                   <tr>
                       <td colspan="2">
                           <asp:Button ID="btnConfirmPersonalInfo" runat="server" Text="Confirm" Font-Bold="true" Font-Size="Medium" Height="40px" Width="250px" Style="margin-top: 5px; margin-bottom: 5px;" OnClick="btnConfirmPersonalInfo_Click" />
                       </td>
                   </tr>
              </table>

        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
