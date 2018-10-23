<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="SalesForceWebApp.CreateAccount" %>

<%@ Register Assembly="GoogleRecaptcha" Namespace="GoogleRecaptcha" TagPrefix="grecaptcha" %>

<%--<%@ PreviousPageType VirtualPath="~/PersonalInfo.aspx" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

</head>
<%--<body onload="gReCaptcha()">--%>
<body>
    <form id="form1" runat="server">

    <%--This is Google reCaptcha code: should be uncommented in production code  --%>  
    
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>

        <script type="text/javascript">
            var onloadCallback = function () {
                grecaptcha.render('dvCaptcha', {
                    'sitekey': '<%=ReCaptcha_Key %>',
                    'callback': function (response) {
                        $.ajax({
                            type: "POST",
                            url: "CreateAccount.aspx/VerifyCaptcha",
                            data: "{response: '" + response + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {

                                var recaptcha_response = grecaptcha.getResponse();

                                if (recaptcha_response.length > 0)
                                {
                                    $('[id*=txtCaptcha]').val('Success');
                                    $('[id*=rfvCaptcha]').hide();
                                } else {
                                    $('[id*=txtCaptcha]').val();
                                    $('[id*=rfvCaptcha]').show();
                                }
                            }
                        });
                    }
                });
            };
        </script>

             <style type="text/css">
                .LargeChkBoxClass input {
                    width: 20px;
                    height: 20px;
                    vertical-align: bottom;
                }

                .tdLargeChkBox {
                    height: 40px;
                    width: 200px;
                    padding: 0px;
                    text-align: left;
                    /*border-color: lightgray;
                    border-style: solid;
                    border-width: 1px;*/
                }


                .tdLabel {
                    text-align: left;
                    height: 35px;
                    width: 110px;
                    font-size: medium;
                    font-weight: normal;
                    padding: 0px;
                    /*border-color: lightgray;
                    border-style: solid;
                    border-width: 1px;*/
                }

                .tdTextBox {
                    height: 35px;
                    width: 255px;
                    /*padding: 10px;*/
                    /*border-color: lightgray;
                    border-style: solid;
                    border-width: 1px;*/
                }

                /*.trElement {
                    height: 30px;
                    width: 250px;
                    margin: 10px;
                    padding: 10px;

                    border-color: gray;
                    border-style: solid;
                    border-width: 1px;

                }*/

                .modalBackground {
                    background-color: gray;
                    filter: alpha(opacity=50);
                    opacity: 0.7;
                }

                .pnlBackGround {
                    position: fixed;
                    font-size: small;
                    font-weight: normal;
                    top: 10%;
                    left: 10px;
                    width: 320px;
                    height: 150px;
                    text-align: center;
                    background-color: white;
                    border: solid 1px black;
                }

                /*.reCaptchaSuccessMessage 
                {
                    font-size: medium;
                    color: blue;
                }
                .reCaptchaFailureMessage
                {
                    font-size: medium;
                    color: red;
                }*/

            </style>


        <%--    <asp:ScriptManager ID="smUserRegister" runat="server"></asp:ScriptManager>--%>
        <div>




            <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
                <p>The Christian Mutual Med-Aid</p>
            </div>

            <asp:Panel ID="pnlCreateAccount" runat="server" GroupingText="Create you account" HorizontalAlign="Center" BorderWidth="0" BorderStyle="Solid" BorderColor="Black" Width="480px" Height="600px"
                Font-Bold="true" Font-Size="X-Large" Style="vertical-align: top; margin-top: 100px; margin-left: auto; margin-right: auto;">
                <table border="0" style="margin-top: 20px; width: 380px; padding-left: 10px;">
                    <tr>
                        <td class="tdLabel">First name</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtFirstName" runat="server" Height="30px" Width="270px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 5px;">
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="First name is required!"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Last name</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtLastName" runat="server" Height="30px" Width="270px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 5px;">
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Last name is required!"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Email</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtEmail" runat="server" Height="30px" Width="270px" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 5px;">
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Email is required!"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ErrorMessage="Email format is incorrect!"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Password</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtPassword" runat="server" Height="30px" Width="270px" TextMode="Password" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 5px;">
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"
                                ErrorMessage="Password is required!"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td class="tdLabel">Confirm password</td>
                        <td class="tdTextBox">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" Height="30px" Width="270px" TextMode="Password" Text=""></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="height: 28px; text-align: left; padding-left: 5px;">
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"
                                ErrorMessage="Enter the password again!"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"
                                ErrorMessage="Two passwords have to match!"></asp:CompareValidator>
                        </td>
                    </tr>


                    
    
                <%--The reCaptha validation code: should be uncommented in production code  --%>  
                    <tr>
                        <td colspan="2" style="padding-left: 60px;">
                            <div id="dvCaptcha"></div>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" style="height: 28px; text-align: center; padding-left: 5px;">
                            <asp:TextBox ID="txtCaptcha" runat="server" Text="" Style="display: none;" />
                            <asp:RequiredFieldValidator ID = "rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha"
                                runat="server" ForeColor = "Red" Display = "Dynamic" />

                        </td>
                    </tr>


                    <tr>
                        <td colspan="2" style="padding-left: 5px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Height="40px" Width="400px"
                                Style="margin-top: 5px; margin-bottom: 5px;" OnClick="BtnSubmit_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblErrorMessage" runat="server" Text="Error: "></asp:Label>
                        </td>
                    </tr>

                </table>

            </asp:Panel>
        </div>
    </form>
</body>
</html>
