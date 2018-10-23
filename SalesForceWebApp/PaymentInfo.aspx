<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentInfo.aspx.cs" Inherits="SalesForceWebApp.PaymentInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

        <style type="text/css">
            /*.pnlLabel {
                height: 80px;
                text-align: left;
                font-size: small;
                font-weight: normal;
                float: left;
                margin: 5px;
            }*/

            .TextBox {
                font-weight: normal;
                height: 30px;
                margin-top: 3px;
                margin-left: 0px;
                margin-bottom: 5px;
            }

            .pnlLabel {
                font-family: Arial;
                font-size: medium;
                font-weight: bold; 
                float: left;
                height: 20px;
                /*margin-bottom: 0px;*/
                /*border: solid 1px gray;*/
            }

            .largeRadioButton input {
                height: 20px;
                width: 20px;
                vertical-align: text-bottom;

            }

            .largeCheckBox {
                font-weight: bold;
                font-size: small;
                font-family: Arial;
            }

            .largeCheckBox input {
                height: 20px;
                width: 20px;
                vertical-align: middle;

            }

            /*.largeRadioButton
            {
                margin-bottom: 20px;
            }*/

            .DropDownList {
                height: 30px;
            }

            .floatLeft {
                float: left;
                font-weight: normal;
                margin-top: 20px;
                margin-left: 3px;
                margin-right: 3px;
                margin-bottom: 3px;
                text-align: left;
            }

            .floatLeftBankInfo {
                float: left;
                font-weight: normal;
                margin-top: 20px;
                margin-left: 3px;
                margin-right: 3px;
                margin-bottom: 20px;
                text-align: center;
            }

            .floatLeftBankInfo > fieldset {
                height: 200px;
            }

            .floatLeftCreditCardInfo 
            {
                float: left;
                font-weight: normal;
                margin-top: 20px;
                margin-left: 3px;
                margin-right: 3px;
                margin-bottom: 20px;
                text-align: center;

            }

            .floatLeftCreditCardInfo > fieldset {
                height: 320px;
            }

            .floatPnlBillingAddress {
                float: left;
                font-weight: normal;
                margin-top: 20px;
                margin-left: 3px;
                margin-right: 3px;
                margin-bottom: 20px;
                text-align: center;

            }

            .floatPnlBillingAddress > fieldset {
                height: 220px;
            }

    </style>

    <script src='<%= Page.ResolveClientUrl("~/Scripts/personal-info.js") %>' type="text/javascript"></script>

    <script type="text/javascript">

        function DisableClientValidation()
        {
            var valPaymentMethod = document.getElementById("<%=rfvPaymentMethod.ClientID %>");
            var valPaymentFrequency = document.getElementById("<%=rfvPaymentFrequency.ClientID%>");
            var valBankAccountType = document.getElementById("<%=rfvBankAccountType.ClientID%>");
            var valBankName = document.getElementById("<%=rfvBankName.ClientID%>");
            var valAccountOwnerName = document.getElementById("<%=rfvAccountOwnerName.ClientID%>");
            var valRoutingNumber = document.getElementById("<%=rfvRoutingNumber.ClientID%>");
            var valAccountNumber = document.getElementById("<%=rfvAccountNumber.ClientID%>");
            var valCreditCardType = document.getElementById("<%=rfvCreditCardType.ClientID%>");
            var valNameOnCard = document.getElementById("<%=rfvNameOnCard.ClientID%>");
            var valCardNumber = document.getElementById("<%=rfvCreditCardNumber.ClientID%>");
            var valCVV = document.getElementById("<%=rfvCreditCardCvv.ClientID%>");
            var valExpirationMonth = document.getElementById("<%=rfvExpirationDateMonth.ClientID%>");
            var valExpirationYear = document.getElementById("<%=rfvExpirationDateYear.ClientID%>");
            var valBillingStreet = document.getElementById("<%=rfvBillingStreetAddress.ClientID%>");
            var valBillingZipCode = document.getElementById("<%=rfvBillingZipCode.ClientID%>");
            var valBillingState = document.getElementById("<%=rfvBillingState.ClientID%>");
            var valBillingCity = document.getElementById("<%=rfvBillingCity.ClientID%>");
<%--            var valReferredBy = document.getElementById("<%=rfvReferredBy.ClientID%>");--%>
            var valNotifyBy = document.getElementById("<%=cvNotifyByEmailPostal.ClientID%>");
            var valJoinMailing = document.getElementById("<%=rfvJoinMailing.ClientID%>");
            var valAllowMessages = document.getElementById("<%=rfvAllowMessages.ClientID%>");

            ValidatorEnable(valPaymentMethod, false);
            ValidatorEnable(valPaymentFrequency, false);
            ValidatorEnable(valBankAccountType, false);
            ValidatorEnable(valBankName, false);
            ValidatorEnable(valAccountOwnerName, false);
            ValidatorEnable(valRoutingNumber, false);
            ValidatorEnable(valAccountNumber, false);
            ValidatorEnable(valCreditCardType, false);
            ValidatorEnable(valNameOnCard, false);
            ValidatorEnable(valCardNumber, false);
            ValidatorEnable(valCVV, false);
            ValidatorEnable(valExpirationMonth, false);
            ValidatorEnable(valExpirationYear, false);
            ValidatorEnable(valBillingStreet, false);
            ValidatorEnable(valBillingZipCode, false);
            ValidatorEnable(valBillingState, false);
            ValidatorEnable(valBillingCity, false);
            //ValidatorEnable(valReferredBy, false);
            ValidatorEnable(valNotifyBy, false);
            ValidatorEnable(valJoinMailing, false);
            ValidatorEnable(valAllowMessages, false);

        }

        function EnableClientValidation()
        {
            var valPaymentMethod = document.getElementById("<%=rfvPaymentMethod.ClientID %>");
            var valPaymentFrequency = document.getElementById("<%=rfvPaymentFrequency.ClientID%>");
            var valBankAccountType = document.getElementById("<%=rfvBankAccountType.ClientID%>");
            var valBankName = document.getElementById("<%=rfvBankName.ClientID%>");
            var valAccountOwnerName = document.getElementById("<%=rfvAccountOwnerName.ClientID%>");
            var valRoutingNumber = document.getElementById("<%=rfvRoutingNumber.ClientID%>");
            var valAccountNumber = document.getElementById("<%=rfvAccountNumber.ClientID%>");
            var valCreditCardType = document.getElementById("<%=rfvCreditCardType.ClientID%>");
            var valNameOnCard = document.getElementById("<%=rfvNameOnCard.ClientID%>");
            var valCardNumber = document.getElementById("<%=rfvCreditCardNumber.ClientID%>");
            var valCVV = document.getElementById("<%=rfvCreditCardCvv.ClientID%>");
            var valExpirationMonth = document.getElementById("<%=rfvExpirationDateMonth.ClientID%>");
            var valExpirationYear = document.getElementById("<%=rfvExpirationDateYear.ClientID%>");
            var valBillingStreet = document.getElementById("<%=rfvBillingStreetAddress.ClientID%>");
            var valBillingZipCode = document.getElementById("<%=rfvBillingZipCode.ClientID%>");
            var valBillingState = document.getElementById("<%=rfvBillingState.ClientID%>");
            var valBillingCity = document.getElementById("<%=rfvBillingCity.ClientID%>");
<%--            var valReferredBy = document.getElementById("<%=rfvReferredBy.ClientID%>");--%>
            var valNotifyBy = document.getElementById("<%=cvNotifyByEmailPostal.ClientID%>");
            var valJoinMailing = document.getElementById("<%=rfvJoinMailing.ClientID%>");
            var valAllowMessages = document.getElementById("<%=rfvAllowMessages.ClientID%>");

            ValidatorEnable(valPaymentMethod, true);
            ValidatorEnable(valPaymentFrequency, true);
            ValidatorEnable(valBankAccountType, true);
            ValidatorEnable(valBankName, true);
            ValidatorEnable(valAccountOwnerName, true);
            ValidatorEnable(valRoutingNumber, true);
            ValidatorEnable(valAccountNumber, true);
            ValidatorEnable(valCreditCardType, true);
            ValidatorEnable(valNameOnCard, true);
            ValidatorEnable(valCardNumber, true);
            ValidatorEnable(valCVV, true);
            ValidatorEnable(valExpirationMonth, true);
            ValidatorEnable(valExpirationYear, true);
            ValidatorEnable(valBillingStreet, true);
            ValidatorEnable(valBillingZipCode, true);
            ValidatorEnable(valBillingState, true);
            ValidatorEnable(valBillingCity, true);
            //ValidatorEnable(valReferredBy, true);
            ValidatorEnable(valNotifyBy, true);
            ValidatorEnable(valJoinMailing, true);
            ValidatorEnable(valAllowMessages, true);

        }

    </script>

    <form id="form1" runat="server">
    <div>
         <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
            <p>The Christian Mutual Med-Aid</p>
         </div>

        <asp:Panel ID="pnlPaymentInformation" runat="server" Height="840px" Width="800px" HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" BorderColor="Gray" 
                Font-Bold="true" Font-Size="X-Large" Style="vertical-align: top; margin-top: 20px; margin-left: auto; margin-right: auto; padding-top: 10px; padding-left: 10px; padding-right: 10px; " >
            <div style="text-align: center; font-size: x-large; font-weight: bold; margin-top: 20px; margin-bottom: 35px; ">
                <p style="font-family: Arial;">Payment Information Page</p>
            </div>

            <asp:Panel ID="pnlPaymentMethod" runat="server" Height="85px" Width="390px" CssClass="floatLeft">
                <asp:Label ID="lblPaymentMethod" runat="server" Text="Method" CssClass="pnlLabel"></asp:Label><br />
                <asp:Panel ID="pnlBankACHorCreditCard" runat="server" Height="35px" Width="370px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                    style="margin-top: 5px; text-align: left; padding-top: 5px; padding-left: 10px; " >
                    <asp:RadioButtonList ID="rbListPaymentMethod" runat="server" AutoPostBack="true" RepeatColumns="2" RepeatDirection="Horizontal" Font-Bold="true" Font-Size="Small" Font-Names="Arial" OnSelectedIndexChanged="rbListPaymentMethod_SelectedIndexChanged" >
                        <asp:ListItem>Bank ACH</asp:ListItem>
                        <asp:ListItem>Credit Card</asp:ListItem>
                    </asp:RadioButtonList>

                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvPaymentMethod" runat="server" ControlToValidate="rbListPaymentMethod" ErrorMessage="Payment method required" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlPaymentFrequency" runat="server" Height="85px" Width="390px" CssClass="floatLeft">
                <asp:Label ID="lblPaymentFrequency" runat="server" Text="Frequency" CssClass="pnlLabel" ></asp:Label><br />
                <asp:Panel ID="pnlRecurringOneTime" runat="server" Height="35px" Width="370px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                    style="margin-top: 5px; text-align: left; padding-top: 5px; padding-left: 10px; " >
                    <asp:RadioButtonList ID="rbListPaymentFrequency" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                        <asp:ListItem>Recurring</asp:ListItem>
                        <asp:ListItem>One Time</asp:ListItem>
                    </asp:RadioButtonList>

                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvPaymentFrequency" runat="server" ControlToValidate="rbListPaymentFrequency" ErrorMessage="Payment frequency required" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                    Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlBankInformation" runat="server" HorizontalAlign="Left" Visible="true" Height="280px" Width="780px" CssClass="floatLeftBankInfo" BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid"
                style="padding-top: 10px; margin-top: 20px; " >
                <asp:Label ID="lblBankInfoTitle" runat="server" Text="Bank Account Information" Font-Bold="true" Font-Names="Arial" Font-Size="X-Large" style="padding: 5px;"></asp:Label><br />
                <asp:Panel ID="pnlBankAccountType" runat="server" Height="70px" Width="360px" CssClass="floatLeft" style="margin-top: 10px; padding-left: 15px;">
                    <asp:Label ID="lblBankAccountType" runat="server" Text="Type of Bank Account" CssClass="pnlLabel"></asp:Label><br />
                    <asp:Panel ID="pnlAccountType" runat="server" Height="35px" Width="350px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                        style="margin-top: 5px; text-align: left; padding-top: 5px; padding-left: 20px; " >
                        <asp:RadioButtonList ID="rbListAccountType" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                            <asp:ListItem>Checking</asp:ListItem>
                            <asp:ListItem>Saving</asp:ListItem>
                        </asp:RadioButtonList>

                    </asp:Panel>
                    <asp:RequiredFieldValidator ID="rfvBankAccountType" runat="server" ControlToValidate="rbListAccountType" ErrorMessage="Please choose account type" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlBankName" runat="server" Height="90px" Width="340px" CssClass="floatLeft" style="margin-top: 10px; text-align: left; padding-left: 10px; " >
                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtBankName" runat="server" Width="330px" CssClass="TextBox"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvBankName" runat="server" ControlToValidate="txtBankName" ErrorMessage="Bank Name required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>

                </asp:Panel><br />

                <asp:Panel ID="pnlAccountOwnerName" runat="server" Height="90px" Width="235px" CssClass="floatLeft" style="padding-left: 15px;">
                    <asp:Label ID="lblAccountOwnerName" runat="server" Text="Account Owner's Name" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtAccountOwnerName" runat="server" Width="230px" CssClass="TextBox"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAccountOwnerName" runat="server" ControlToValidate="txtAccountOwnerName" ErrorMessage="Account owner name required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlRoutingNumber" runat="server" Height="90px" Width="235px" CssClass="floatLeft">
                    <asp:Label ID="lblRoutingNumber" runat="server" Text="Routing Nubmer" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtRoutingNumber" runat="server" Width="230px" CssClass="TextBox" ></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvRoutingNumber" runat="server" ControlToValidate="txtRoutingNumber" ErrorMessage="Routing number required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlAccountNumber" runat="server" Height="90px" Width="235px" CssClass="floatLeft">
                    <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtAccountNumber" runat="server" Width="230px" CssClass="TextBox" ></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server" ControlToValidate="txtAccountNumber" ErrorMessage="Account number required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
            </asp:Panel>


            <asp:Panel ID="pnlCreditCardInformation" runat="server" GroupingText="Credit Card Information" HorizontalAlign="Left" Visible="false" Height="340px" Width="780px" CssClass="floatLeftCreditCardInfo" >
                <asp:Panel ID="pnlCreditCardType" runat="server" Height="70px" Width="740px" CssClass="floatLeft">
                    <asp:Label ID="lblCreditCardType" runat="server" Text="Card Type" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:DropDownList ID="ddlCreditCardType" runat="server" Height="35px" Width="350px" >
                        <asp:ListItem>Master Card</asp:ListItem>
                        <asp:ListItem>Visa Card</asp:ListItem>
                        <asp:ListItem>Discover Card</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvCreditCardType" runat="server" Enabled="false" ControlToValidate="ddlCreditCardType" ErrorMessage="Credit card type required" ValidationGroup="CreditCardType" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>

                <asp:Panel ID="pnlNameOnCard" runat="server" Height="100px" Width="360px" CssClass="floatLeft">
                    <asp:Label ID="lblNameOnCard" runat="server" Text="Name as it is printed on card" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtNameOnCard" runat="server" Width="340px" CssClass="TextBox" ></asp:TextBox><br />
                    <asp:RequiredFieldValidator id="rfvNameOnCard" runat="server" Enabled="false" ControlToValidate="txtNameOnCard" ErrorMessage="Name of card required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
                
                <asp:Panel ID="pnlCreditCardNumber" runat="server" Height="100px" Width="260px" CssClass="floatLeft">
                    <asp:Label ID="lblCreditCardNumber" runat="server" Text="Credit Card Number" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtCreditCardNumber" runat="server" onclick="mouse_left_click(event, this);" onfocus="mouse_left_click(event, this);"
                        onfocusout="credit_card_lost_focus(event, this)" onkeypress="accept_credit_card_number(event, this);" onkeydown="filter_control_character_credit_card(event, this);"
                        Width="240px" CssClass="TextBox"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvCreditCardNumber" runat="server" Enabled="false" ControlToValidate="txtCreditCardNumber" ErrorMessage="Credit card number required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
                
                <asp:Panel ID="pnlCreditCardCVV" runat="server" Height="100px" Width="90px" CssClass="floatLeft">
                    <asp:Label ID="lblCreditCardCVV" runat="server" Text="CVV" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtCreditCardCVV" runat="server" onkeypress="accept_cvv(event, this);" Width="80px" CssClass="TextBox"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvCreditCardCvv" runat="server" Enabled="false" ControlToValidate="txtCreditCardCVV" ErrorMessage="CVV required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
                
                <asp:Panel ID="pnlExpirationDate" runat="server" Height="70px" Width="500px" CssClass="floatLeft">
                    <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date" CssClass="pnlLabel"></asp:Label><br />
                    <asp:DropDownList ID="ddlExpirationDateMonth" runat="server" Height="35px" Width="240px" >
                        <asp:ListItem Text="January" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="February" Value="2"></asp:ListItem>
                        <asp:ListItem Text="March" Value="3"></asp:ListItem>
                        <asp:ListItem Text="April" Value="4"></asp:ListItem>
                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                        <asp:ListItem Text="June" Value="6"></asp:ListItem>
                        <asp:ListItem Text="July" Value="7"></asp:ListItem>
                        <asp:ListItem Text="August" Value="8"></asp:ListItem>
                        <asp:ListItem Text="September" Value="9"></asp:ListItem>
                        <asp:ListItem Text="October" Value="10"></asp:ListItem>
                        <asp:ListItem Text="November" Value="11"></asp:ListItem>
                        <asp:ListItem Text="December" Value="12"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlExpirationDateYear" runat="server" Height="35px" Width="240px" >
                    </asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvExpirationDateMonth" runat="server" Enabled="false" ControlToValidate="ddlExpirationDateMonth" ErrorMessage="Expiration month required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="rfvExpirationDateYear" runat="server" Enabled="false" ControlToValidate="ddlExpirationDateYear" ErrorMessage="Expiration year required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
            </asp:Panel>

            <asp:Panel ID="pnlBillingAddressDifferent" runat="server" style="text-align: left; ">
                <asp:CheckBox ID="chkBillingAddressDifferent" runat="server" AutoPostBack="true" Text="If your billing address is DIFFERENT from the one you provided, please click this box."
                     OnCheckedChanged="chkBillingAddressDifferent_CheckedChanged" CssClass="largeCheckBox" />
            </asp:Panel>

            <asp:Panel ID="pnlBillingAddress" runat="server" GroupingText="Billing Address" Visible="false" Height="220px" Width="780px" Font-Bold="true" Font-Names="Arial" Font-Size="Medium" HorizontalAlign="Left" CssClass="floatPnlBillingAddress">
                <asp:Panel ID="pnlBillingStreetAddress" runat="server" Height="70px" Width="740px" CssClass="floatLeft" >
                    <asp:Label ID="lblBillingStreetAddress" runat="server" Text="Address" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtBillingStreetAddress" runat="server" Width="720px" CssClass="TextBox"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvBillingStreetAddress" runat="server" Enabled="false" ControlToValidate="txtBillingStreetAddress" ErrorMessage="Billing adddress required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlBillingZipCode" runat="server" Height="70px" Width="150px" CssClass="floatLeft" >
                    <asp:Label ID="lblBillingZipCode" runat="server" Text="Zip Code" CssClass="pnlLabel"></asp:Label><br />
                    <asp:TextBox ID="txtBillingZipCode" runat="server" Width="130px" CssClass="TextBox" AutoPostBack="true"
                        OnTextChanged="txtBillingZipCode_TextChanged"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvBillingZipCode" runat="server" Enabled="false" ControlToValidate="txtBillingZipCode" ErrorMessage="Billing zip code required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlBillingState" runat="server" Height="70px" Width="180px" CssClass="floatLeft" >
                    <asp:Label ID="lblBillingState" runat="server" Text="State" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:DropDownList ID="ddlBillingState" runat="server" Height="35px" Width="160px"></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvBillingState" runat="server" Enabled="false" ControlToValidate="ddlBillingState" ErrorMessage="Billing state required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>

                </asp:Panel>

                <asp:Panel id="pnlBillingCity" runat="server" Height="70px" Width="400px" CssClass="floatLeft" >
                    <asp:Label ID="lblBillingCity" runat="server" Text="City" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:DropDownList ID="ddlBillingCity" runat="server" Height="35px" Width="370px" ></asp:DropDownList><br />
                    <asp:RequiredFieldValidator ID="rfvBillingCity" runat="server" Enabled="false" ControlToValidate="ddlBillingCity" ErrorMessage="Billing city required"
                        ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>
            </asp:Panel>

<%--            <asp:Panel ID="pnlReferredBy" runat="server" Height="70px" Width="380px" CssClass="floatLeft" >
                <asp:Label ID="lblReferredBy" runat="server" Text="Referred By" CssClass="pnlLabel"></asp:Label> <br />
                <asp:DropDownList ID="ddlReferredBy" runat="server" Height="35px" Width="360px" ></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvReferredBy" runat="server" ControlToValidate="ddlReferredBy" ErrorMessage="Referred by required" ForeColor="Red"
                    Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>--%>

            <asp:Panel ID="pnlNotifyBy" runat="server" Height="70px" Width="380px" CssClass="floatLeft" >
                <asp:Label ID="lblNotifyBy" runat="server" Text="Notify by" CssClass="pnlLabel" ></asp:Label><br />
                <asp:Panel ID="pnlNotifyByEmailPostal" runat="server" Height="35px" Width="360px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" style="padding-top: 5px; padding-left: 10px;"  >
                    <asp:CheckBox ID="chkNotifyByEmail" runat="server" Text="Email" />
                    <asp:CheckBox ID="chkNotifyByPostal" runat="server" Text="Postal" />
                </asp:Panel><br />
                <asp:CustomValidator ID="cvNotifyByEmailPostal" runat="server" ErrorMessage="Please choose notify by method"></asp:CustomValidator>

            </asp:Panel>

            <asp:Panel ID="pnlJoinMailing" runat="server" Height="70px" Width="380px" CssClass="floatLeft">
                <asp:Label ID="lblJoinMailing" runat="server" Text="Join Mailing" CssClass="pnlLabel" ></asp:Label><br />
                <asp:Panel ID="pnlJoinMailingYesNo" runat="server" Height="35px" Width="360px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" style="padding-top: 5px; padding-left: 10px;" >
                    <asp:RadioButtonList ID="rbListJoinMailing" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvJoinMailing" runat="server" ControlToValidate="rbListJoinMailing" ErrorMessage="Whether to join mailing" ForeColor="Red" Font-Bold="false"
                    Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlAllowMessage" runat="server" Height="70px" Width="380px" CssClass="floatLeft">
                <asp:Label ID="lblAllowMessages" runat="server" Text="Allow Messages" CssClass="pnlLabel" ></asp:Label><br />
                <asp:Panel ID="pnlAllowMessagesYesNo" runat="server" Height="35px" Width="360px"  BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" style="padding-top: 5px; padding-left: 10px;" >
                    <asp:RadioButtonList ID="rbListAllowMessages" runat="server" CausesValidation="true" RepeatColumns="2" RepeatDirection="Horizontal" ValidationGroup="AllowMessages" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvAllowMessages" runat="server" ControlToValidate="rbListAllowMessages" ErrorMessage="Whether to allow mailing" ForeColor="Red" Font-Bold="false" 
                    Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlPrevNextButton" runat="server" Height="35px" Width="800px" CssClass="floatLeft" >
                <asp:Button ID="btnPrevious" runat="server" Height="30px" Width="120px" Text="Prev" OnClick="btnPrevious_Click" OnClientClick="DisableClientValidation();" style="float: left; " />
                <asp:Button ID="btnNext" runat="server"  Height="30px" Width="120px" Text="Next" OnClick="btnNext_Click" OnClientClick="EnableClientValidation();" style="float: right; " />
            </asp:Panel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
