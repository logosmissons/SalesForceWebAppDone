<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="SalesForceWebApp.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/MainForm.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script src='<%= Page.ResolveClientUrl("~/Scripts/MainForm.js") %>' type="text/javascript"></script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="smRegister" runat="server"></asp:ScriptManager>
        <div>
            <div style="text-align: left; font-size: large; color: blue; margin-left: 100px;">Please do not close the window. After you finish the updating, please click Log Out button at the bottom right side of window.</div>
            <br />
            <ajaxToolkit:TabContainer ID="tabContainerRegister" runat="server" CssClass="TabContainer">
                <ajaxToolkit:TabPanel ID="tpnlGeneralInfo" runat="server" HeaderText="General Information" CssClass="TopTabPanel">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPersonalInfo" runat="server" GroupingText="Personal Information" CssClass="topLeftPanel">
                            <asp:Panel ID="pnlPrimaryID" runat="server" Width="505px" Height="50px" CssClass="floatLeft">
                                <asp:Label ID="lblPrimaryID" runat="server" Text="Primary ID" Width="100px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtPrimaryID" runat="server" Width="500px" ReadOnly="True" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>
                            <asp:Panel ID="pnlEmail" runat="server" Width="505px" Height="50px" CssClass="floatLeft">
                                <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtEmail" runat="server" Width="500px" CssClass="TextBox"
                                    onblur="txt_validate_Required_and_Email (this, 'Email address required!', 'Email address invalid!', hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);"
                                    onfocus="txt_got_Focus_Required_and_Email (this, 'Email address required!', 'Email address invalid!', hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);"
                                    onkeypress="OnValidateEmailOnBlur(event, this, 24);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnEmail);" TabIndex="1"></asp:TextBox>
                                <asp:HiddenField ID="hdnEmail" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnEmailFontColor" runat="server" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="rfvMemberEmail" runat="server" EnableClientScript="False" ControlToValidate="txtEmail" InitialValue="Email address required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revMemberEmail" runat="server" EnableClientScript="False" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTitle" runat="server" Width="70px" Height="50px" CssClass="floatTitle">

                                <asp:Label ID="lblTitle" runat="server" Text="Title" Width="60px" CssClass="pnlLabel"></asp:Label>
                                <asp:DropDownList ID="ddlTitle" runat="server" Width="60px" CssClass="ddlTitle">
                                    <%--                                    <asp:ListItem Text="Jr." Value="0"></asp:ListItem>--%>
                                    <asp:ListItem Text="Mr." Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Mrs." Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Ms." Value="2"></asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlLastName" runat="server" Width="150px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblLastName" runat="server" Text="Last Name" Width="145px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtLastName" runat="server" Width="140px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Last name required!', hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'Last name required!', hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnLastName);" TabIndex="2"></asp:TextBox>

                                <asp:HiddenField ID="hdnLastName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnLastNameFontColor" runat="server" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" EnableClientScript="False" InitialValue="Last name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlFirstName" runat="server" Width="150px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblFirstName" runat="server" Text="First Name" Width="145px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtFirstName" runat="server" Width="140px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'First name required!', hdnFirstNameBorderWidth, hdnFirstNameBorderColor, hdnFirstNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'First name required!', hdnFirstNameBorderWidth, hdnFirstNameBorderColor, hdnFirstNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnFirstName);" TabIndex="3"></asp:TextBox>
                                <asp:HiddenField ID="hdnFirstName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnFirstNameFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" EnableClientScript="False" InitialValue="First name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlMiddleName" runat="server" Width="115px" Height="50px" CssClass="floatSubPanel">
                                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" Width="100px" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtMiddleName" runat="server" Width="112px" CssClass="TextBox"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnMiddleName);" TabIndex="4"></asp:TextBox>
                                <asp:HiddenField ID="hdnMiddleName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMiddleNameFontColor" runat="server" ClientIDMode="Static" />

                            </asp:Panel>


                            <asp:Panel ID="pnlDateOfBirth" runat="server" Height="50px" CssClass="floatDateOfBirth">
                                <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth" Width="120px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtDateOfBirth" runat="server" Width="142px" ReadOnly="True" TextMode="DateTime" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>

                            <asp:Panel ID="pnlGender" runat="server" GroupingText="Gender" CssClass="floatPanelGender">

                                <asp:RadioButton ID="rbMale" runat="server" Width="60px" Text="Male" Enabled="False" GroupName="Gender" Font-Size="Small" CssClass="floatLeftGenderRadioButton" />
                                <asp:RadioButton ID="rbFemale" runat="server" Width="78px" Text="Female" Enabled="False" GroupName="Gender" Font-Size="Small" CssClass="floatLeftGenderRadioButton" />
                            </asp:Panel>

                            <asp:Panel ID="pnlSSN" runat="server" Height="50px" CssClass="floatSSN">
                                <asp:Label ID="lblSSN" runat="server" Text="SSN" Width="100px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtSSN" runat="server" Width="156px" ReadOnly="True" CssClass="TextBox"></asp:TextBox>
                            </asp:Panel>

                            <br />

                            <asp:Panel ID="pnlTelephone1" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone1" runat="server" Text="Telephone" Width="150px" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtTelephone1" runat="server" Width="150px" CssClass="TextBox"
                                    onblur="txt_validate_Required_and_Telephone (this, 'Phone number required!', 'Invalid phone number!', hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);"
                                    onfocus="txt_got_Focus_Required_and_Telephone (this, 'Phone number required!', 'Invalid phone number!', hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnTelephone);" TabIndex="5"></asp:TextBox>
                                <asp:HiddenField ID="hdnTelephone" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnTelephoneFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone1" InitialValue="Phone number required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone1" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTelephone2" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone2" runat="server" Text="Mobile Phone" Width="150px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTelephone2" runat="server" Width="158px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnMobilePhone);" TabIndex="6"></asp:TextBox>

                                <asp:HiddenField ID="hdnMobilePhone" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnMobilePhoneFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RegularExpressionValidator ID="revMobilePhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone2" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlTelephone3" runat="server" Height="50px" CssClass="floatTelephone">
                                <asp:Label ID="lblTelephone3" runat="server" Text="Other Phone" Width="165px" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtTelephone3" runat="server" Width="160px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);"
                                    onkeypress="OnValidateTelephoneOnBlur(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnOtherPhone);" TabIndex="7"></asp:TextBox>
                                <asp:HiddenField ID="hdnOtherPhone" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnOtherPhoneFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RegularExpressionValidator ID="revOtherPhone" runat="server" EnableClientScript="False" ControlToValidate="txtTelephone3" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"></asp:RegularExpressionValidator>
                            </asp:Panel>


                            <asp:Panel ID="pnlAddress" runat="server" Height="50px" Width="500px" CssClass="floatLeft">
                                <asp:Label ID="lblAddress" runat="server" Width="500px" Text="Address" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtAddress" runat="server" Width="500px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Your address required!', hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor)"
                                    onfocus="txt_got_Focus(this, 'Your address required!', hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnAddress);" TabIndex="8"></asp:TextBox>
                                <asp:HiddenField ID="hdnAddress" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnAddressFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvStreetAddress" runat="server" InitialValue="Street address required!" EnableClientScript="False" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>

                            </asp:Panel>


                            <asp:Panel ID="pnlZipCode" runat="server" CssClass="floatZipCode" DefaultButton="btnZipCodeHidden">
                                <asp:Label ID="lblZipCode" runat="server" Width="113px" Text="Zip Code" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtZipCode" runat="server" Width="113px" CssClass="TextBox"
                                    OnTextChanged="txtZipCode_TextChanged"
                                    onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);"
                                    onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnZipCode);"
                                    TabIndex="9"></asp:TextBox>
                                <asp:HiddenField ID="hdnZipCode" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnZipCodeFontColor" runat="server" ClientIDMode="Static" />

                                <asp:Button ID="btnZipCodeHidden" runat="server" OnClick="btnZipCodeHidden_Click" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtZipCode" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtZipCode" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlState" runat="server" CssClass="floatLeftState">
                                <asp:Label ID="lblState" runat="server" Text="State" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtState" runat="server" Width="75px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'State required!', hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);"
                                    onfocus="txt_got_Focus(this, 'State required!', hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnState);" TabIndex="10"></asp:TextBox>
                                <asp:HiddenField ID="hdnState" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnStateFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" EnableClientScript="False" InitialValue="State required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlCity" runat="server" CssClass="floatLeftCity">
                                <asp:Label ID="lblCity" runat="server" Width="290px" Text="City" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtCity" runat="server" Width="286px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'City name required!', hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor)"
                                    onfocus="txt_got_Focus(this, 'City name required!', hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);"
                                    onkeypress="OnMoveFocusToChurchNameOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnCity);" TabIndex="11"></asp:TextBox>
                                <asp:HiddenField ID="hdnCity" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnCityFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" EnableClientScript="False" ControlToValidate="txtCity" InitialValue="City name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </asp:Panel>



                        <asp:Panel ID="pnlMemberInformation" runat="server" GroupingText="Member Information" CssClass="topRightPanel">

                            <asp:Panel ID="pnlQualifyForMedicare" runat="server" GroupingText="Qualify for Medicare" CssClass="floatLeftMedicare">
                                <asp:RadioButton ID="rbYesQualifyForMedicare" runat="server" GroupName="QualifyMedicare" Enabled="False" Font-Size="Small" Text="Yes" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbNoQualifyForMedicare" runat="server" GroupName="QualifyMedicare" Enabled="False" Font-Size="Small" Text="No" CssClass="floatLeftRadioButton" />
                            </asp:Panel>
                            <asp:Panel ID="pnlMedicareAB" runat="server" GroupingText="Medicare A and B" CssClass="floatLeftMedicare">
                                <asp:RadioButton ID="rbMedicareABYes" runat="server" GroupName="MedicareAB" Enabled="False" Font-Size="Small" Text="Yes" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbMedicareABNo" runat="server" GroupName="MedicareAB" Enabled="False" Font-Size="Small" Text="No" CssClass="floatLeftRadioButton" />
                            </asp:Panel>

                            <asp:Panel ID="pnlParticipantProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblParticipantProgram" runat="server" Text="Parcitipant's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlPartipantsProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>
                            </asp:Panel>
                            <asp:Panel ID="pnlSpouseProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblSpouseProgram" runat="server" Text="Spouse's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlSpouseProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>

                            </asp:Panel>

                            <asp:Panel ID="pnlChildrenProgram" runat="server" CssClass="floatLeftPrograms">
                                <asp:Label ID="lblChildrenProgram" runat="server" Text="Children's Program" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlChildrenProgram" runat="server" Enabled="False" Width="162px" Height="28px" CssClass="DropDownList">
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchName" runat="server" Width="334px" CssClass="floatLeftChurchName">
                                <asp:Label ID="lblChurchName" runat="server" Text="Church Name" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtChurchName" runat="server" Width="327px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Name of church required!', hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);"
                                    onfocus="txt_got_Focus(this, 'Name of church required!', hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchName);" TabIndex="12"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchName" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchNameFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchName" runat="server" EnableClientScript="False" ControlToValidate="txtChurchName" InitialValue="Name of church required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlSeniorPastor" runat="server" Width="158px" CssClass="floatLeftSeniorPastor">
                                <asp:Label ID="lblSeniorPastor" runat="server" Text="Senior Pastor" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtSeniorPastor" runat="server" Width="157px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Pastor name required!', hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);"
                                    onfocus="txt_got_Focus(this, 'Pastor name required!', hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnSeniorPastor);" TabIndex="13"></asp:TextBox>
                                <asp:HiddenField ID="hdnSeniorPastor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnPastorFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvSeniorPastor" runat="server" EnableClientScript="False" ControlToValidate="txtSeniorPastor" InitialValue="Pastor name required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchStreet" runat="server" Width="500px" CssClass="floatLeftChurchAddress">
                                <asp:Label ID="lblChurchStreet" runat="server" Width="497px" Text="Address" CssClass="pnlLabel"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtChurchStreet" runat="server" Width="497px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Church address required!', hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);"
                                    onfocus="txt_got_Focus(this, 'Church address required!', hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchStreet);" TabIndex="14"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchStreet" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStreetFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchStreet" runat="server" EnableClientScript="False" ControlToValidate="txtChurchStreet" InitialValue="Church address required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchZip" runat="server" CssClass="floatZipCode" DefaultButton="btnChurchZipCodeHidden">
                                <asp:Label ID="lblChurchZip" runat="server" Width="113px" Text="Zip Code" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchZip" runat="server" Width="113px" CssClass="TextBox"
                                    OnTextChanged="txtChurchZipCode_TextChanged"
                                    onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);"
                                    onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchZip);"
                                    TabIndex="15"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchZip" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchZipFontColor" runat="server" ClientIDMode="Static" />

                                <asp:Button ID="btnChurchZipCodeHidden" runat="server" OnClick="btnChurchZipCodeHidden_Click" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvChurchZip" runat="server" EnableClientScript="False" ControlToValidate="txtChurchZip" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revChurchZip" runat="server" EnableClientScript="False" ControlToValidate="txtChurchZip" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchState" runat="server" CssClass="floatLeftState">
                                <asp:Label ID="lblChurchState" runat="server" Text="State" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchState" runat="server" Width="75px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'State required!', hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);"
                                    onfocus="txt_got_Focus(this, 'State required!', hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchState);" TabIndex="16"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchState" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchStateFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchState" runat="server" EnableClientScript="False" ControlToValidate="txtChurchState" InitialValue="State required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchCity" runat="server" CssClass="floatLeftCity">
                                <asp:Label ID="lblChurchCity" runat="server" Width="283px" Text="City" CssClass="pnlLabel"></asp:Label>
                                <asp:TextBox ID="txtChurchCity" runat="server" Width="283px" CssClass="TextBox"
                                    onblur="txt_validate_RequiredField(this, 'Name of city required!', hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);"
                                    onfocus="txt_got_Focus(this, 'Name of city required!', hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);"
                                    onkeypress="OnMoveFocusOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchCity);" TabIndex="17"></asp:TextBox>
                                <asp:HiddenField ID="hdnChurchCity" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchCityFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchCity" runat="server" EnableClientScript="False" ControlToValidate="txtChurchCity" InitialValue="Name of city required!"></asp:RequiredFieldValidator>
                            </asp:Panel>

                            <asp:Panel ID="pnlChurchTelephone" runat="server" Width="160px" CssClass="floatLeftChurchTelephone">
                                <asp:Label ID="lblChurchTelephone" runat="server" Text="Church Telephone" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtChurchTelephone" runat="server" Width="154px" CssClass="TextBox"
                                    onblur="txt_validate_Telephone (this, 'Invalid phone number!', hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);"
                                    onfocus="txt_got_Focus_Telephone (this, 'Invalid phone number!', hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);"
                                    onkeypress="OnMoveFocusToChkBillingAddrTelephoneOnEnter(event, this, 26);"
                                    onkeydown="OnEscapeKeyPressed(event, this, hdnChurchTelephone);" TabIndex="18"></asp:TextBox>

                                <asp:HiddenField ID="hdnChurchTelephone" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneBorderWidth" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneBorderColor" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdnChurchTelephoneFontColor" runat="server" ClientIDMode="Static" />

                                <asp:RequiredFieldValidator ID="rfvChurchPhone" runat="server" EnableClientScript="False" ControlToValidate="txtChurchTelephone" InitialValue="Invalid phone number!"></asp:RequiredFieldValidator>
                            </asp:Panel>
                            <asp:Panel ID="pnlMembershipInfoPlaceHolder" runat="server" Width="495px" Height="31px" CssClass="floatLeft">
                            </asp:Panel>


                        </asp:Panel>

                        <br />

                        <asp:Panel ID="pnlFamilyInformation" runat="server" GroupingText="Family Information" CssClass="downLeftPanel">
                            <asp:Panel ID="pnlFamilyInfoSpouse" runat="server" GroupingText="Spouse" CssClass="floatSpouse">
                                <asp:Panel ID="pnlSpouseInfo" runat="server" Width="230px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseNameLabel" runat="server" Width="225px" Text="Name" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseName" runat="server" Width="225px" CssClass="SpouseName" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                                <asp:Panel ID="pnlSpouseDateOfBirth" runat="server" Width="110px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseDateOfBirthLabel" runat="server" Width="100px" Text="Date of Birth" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseDateOfBirth" runat="server" Width="100px" CssClass="SpouseDateOfBirth" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                                <asp:Panel ID="pnlSpouseStartDate" runat="server" Width="110px" CssClass="floatSubPanelSpouse">
                                    <asp:Label ID="lblSpouseStartDateLabel" runat="server" Width="100px" Text="Start Date" CssClass="pnlLabelSpouseInfo" Visible="False"></asp:Label><br />
                                    <asp:Label ID="lblSpouseStartDate" runat="server" Width="100px" CssClass="SpouseStartDate" Font-Size="14px" Visible="False"></asp:Label>
                                </asp:Panel>

                            </asp:Panel>

                            <asp:Panel ID="pnlFamilyInfoChildren" runat="server" GroupingText="Children" CssClass="floatChildren">
                                <asp:Panel ID="pnlChildrenName" runat="server" Width="230px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenName" runat="server" Width="225px" CssClass="pnlLabelChildrenInfo" Text="Name" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName1" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName2" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName3" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName4" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName5" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName6" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName7" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName8" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName9" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildName10" runat="server" Width="225px" CssClass="lblChildrenName" Visible="False"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="pnlChildrenDateOfBirth" runat="server" Width="110px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenDateOfBirth" runat="server" Width="100px" CssClass="pnlLabelChildrenInfo" Text="Date of Birth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth1" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth2" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth3" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth4" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth5" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth6" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth7" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth8" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth9" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildDateOfBirth10" runat="server" Width="100px" CssClass="lblChildrenDateOfBirth" Visible="False"></asp:Label>

                                </asp:Panel>
                                <asp:Panel ID="pnlChildrenStartDate" runat="server" Width="110px" CssClass="floatSubPanelChildren">
                                    <asp:Label ID="lblChildrenStartDate" runat="server" Width="100px" CssClass="pnlLabelChildrenInfo" Text="Start Date" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate1" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate2" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate3" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate4" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate5" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate6" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate7" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate8" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate9" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>
                                    <asp:Label ID="lblChildStartDate10" runat="server" Width="100px" CssClass="lblChildrenStartDate" Visible="False"></asp:Label>

                                </asp:Panel>
                            </asp:Panel>
                        </asp:Panel>


                        <asp:Panel ID="pnlGiftSendingInformation" runat="server" GroupingText="Gift Sending Information" CssClass="downRightPanel">
                            <asp:Panel ID="pnlPaymentMethod" runat="server" Width="500px" GroupingText="Method:" CssClass="floatLeftPaymentMethod">
                                <asp:RadioButton ID="rbCheck" runat="server" Width="100px" Text="Check" GroupName="PaymentMethod" Font-Size="Small" TabIndex="19" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbBankACH" runat="server" AutoPostBack="True" Width="100px" Text="Bank ACH" GroupName="PaymentMethod" Font-Size="Small" TabIndex="20" CssClass="floatLeftRadioButton"
                                    OnCheckedChanged="OnRBtnBankACH_Selected" />
                                <ajaxToolkit:ModalPopupExtender ID="mpePaymentBankACHDialogBox" runat="server" TargetControlID="btnPaymentBankACHHidden" PopupControlID="pnlPaymentBankACHDialogBox" DynamicServicePath="" BehaviorID="_content_mpePaymentBankACHDialogBox"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlPaymentBankACHDialogBox" runat="server" Style="height: 250px; width: 250px;" CssClass="pnlBackGround">
                                    <div style="max-height: 150px; text-align: left; margin-top: 30px; margin-left: 50px; margin-right: 20px;">

                                        <br />
                                        <asp:Label ID="lblRoutingNumber" runat="server" Text="Bank Routing Number"></asp:Label><br />
                                        <asp:TextBox ID="txtRoutingNumber" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number"></asp:Label><br />
                                        <asp:TextBox ID="txtAccountNumber" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <br />
                                    </div>
                                    <asp:Button ID="btnPaymentBankACHOk" runat="server" Text="Ok" Width="80px" OnClick="btnPaymentBankACH_Save" />
                                    <asp:Button ID="btnPaymentBankACHCancel" runat="server" Text="Cancel" Width="80px" OnClick="btnPaymentBankACH_Cancel" />
                                </asp:Panel>
                                <asp:Button ID="btnPaymentBankACHHidden" runat="server" Text="Button" Style="display: none;" />

                                <asp:RadioButton ID="rbCreditCard" runat="server" AutoPostBack="True" Width="100px" Text="Credit Card" GroupName="PaymentMethod" Font-Size="Small"
                                    TabIndex="21" CssClass="floatLeftRadioButton"
                                    OnCheckedChanged="OnRBtnCreditCard_Selected" />

                                <ajaxToolkit:ModalPopupExtender ID="mpePaymentCreditCardDialogBox" runat="server" TargetControlID="btnPaymentCreditCardHidden" PopupControlID="pnlPaymentCreditCardInfoDialogBox" DynamicServicePath="" BehaviorID="_content_mpePaymentCreditCardDialogBox"></ajaxToolkit:ModalPopupExtender>
                                <asp:Panel ID="pnlPaymentCreditCardInfoDialogBox" runat="server" CssClass="pnlCreditCardInfoDialogBoxBackground">
                                    <div style="height: 20px; width: 420px; text-align: center; margin-top: 20px; font-size: medium; font-weight: bold;">
                                        Please enter your credit card information
                                    </div>

                                    <div style="height: 240px; width: 280px; text-align: left; margin-top: 20px; margin-left: 50px; margin-right: 20px;">

                                        <br />
                                        <asp:Label ID="lblCreditCardNumber" runat="server" Text="Credit Card Number"></asp:Label><br />
                                        <asp:TextBox ID="txtCreditCardNumber" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date"></asp:Label><br />
                                        <asp:TextBox ID="txtExpirationDate" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblSecurityCode" runat="server" Text="Security Code"></asp:Label><br />
                                        <asp:TextBox ID="txtSecurityCode" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblNameOnCreditCard" runat="server" Text="Name On The Credit Card"></asp:Label><br />
                                        <asp:TextBox ID="txtNameOfCreditCard" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnPaymentCreditCardOk" runat="server" Text="Ok" Style="width: 80px;" OnClick="btnPaymentCreditCardInfo_Save" />
                                    <asp:Button ID="btnPaymentCreditCardCancel" runat="server" Text="Cancel" Style="width: 80px;" OnClick="btnPaymentCreditCardInfo_Cancel" />

                                </asp:Panel>
                                <asp:Button ID="btnPaymentCreditCardHidden" runat="server" Text="Button" Style="display: none;" />

                            </asp:Panel>
                            <asp:Panel ID="pnlFrequency" runat="server" Width="500px" GroupingText="Frequency" CssClass="floatLeftPaymentFrequency">
                                <asp:RadioButton ID="rbRecurring" runat="server" Width="100px" Text="Recurring" Font-Size="Small" GroupName="Frequency" TabIndex="22" CssClass="floatLeftRadioButton" />
                                <asp:RadioButton ID="rbOneTime" runat="server" Width="100px" Text="One Time" Font-Size="Small" GroupName="Frequency" TabIndex="23" CssClass="floatLeftRadioButton" />
                            </asp:Panel>
                            <asp:CheckBox ID="chkBillingAddress" runat="server" TabIndex="24" Text="If your billing address is DIFFERENT from the one you provided, please click this box." Font-Size="Small" CssClass="BillingAddressDifferent"
                                onkeypress="MoveFromChkBillingOnEnter(event, this);" OnCheckedChanged="chkBillingAddress_CheckedChanged" AutoPostBack="True" />

                            <asp:Panel ID="pnlBillingAddress" Visible="False" runat="server">
                                <asp:Panel ID="pnlBillingStreet" Visible="False" runat="server" Width="490px" CssClass="floatLeftBillingAddress">
                                    <asp:Label ID="lblBillingStreet" runat="server" Width="490px" Visible="False" CssClass="pnlLabel" Text="Address"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingStreet" runat="server" Width="490px" Visible="False" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing street required!', hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing street required!', hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingStreetAddress);" TabIndex="25"></asp:TextBox>

                                    <asp:HiddenField ID="hdnBillingStreetAddress" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetBorderWidth" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetBorderColor" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStreetFontColor" runat="server" ClientIDMode="Static" />

                                    <asp:RequiredFieldValidator ID="rfvBillingStreet" runat="server" EnableClientScript="False" ControlToValidate="txtBillingStreet" InitialValue="Billing street required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingZipCode" runat="server" Visible="False" CssClass="floatZipCodeBillingAddress" DefaultButton="btnBillingZipCodeHidden">
                                    <asp:Label ID="lblBillingZipCode" runat="server" Visible="False" Width="115px" CssClass="pnlLabel" Text="Zip Code"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingZipCode" runat="server" CssClass="TextBox" Width="115px"
                                        onblur="txt_validate_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);"
                                        onfocus="txt_got_Focus_Required_and_ZipCode (this, 'Zip code required!', 'Invalid zip code!', hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);"
                                        OnTextChanged="txtBillingZipCode_TextChanged"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingZipCode);" TabIndex="26"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingZipCode" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeBorderWidth" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeBorderColor" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingZipCodeFontColor" runat="server" ClientIDMode="Static" />

                                    <asp:Button ID="btnBillingZipCodeHidden" runat="server" OnClick="btnBillingZipCodeHidden_Click" Style="display: none" />
                                    <asp:RequiredFieldValidator ID="rfvBillingZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtBillingZipCode" InitialValue="Zip code required!"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revBillingZipCode" runat="server" EnableClientScript="False" ControlToValidate="txtBillingZipCode" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingState" runat="server" Visible="False" CssClass="floatLeftStateBillingAddress">
                                    <asp:Label ID="lblBillingState" runat="server" Visible="False" CssClass="pnlLabel" Text="State"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingState" runat="server" Visible="False" Width="75px" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing state required!', hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing state required!', hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingState);" TabIndex="27"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingState" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateBorderWidth" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateBorderColor" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingStateFontColor" runat="server" ClientIDMode="Static" />

                                    <asp:RequiredFieldValidator ID="rfvBillingState" runat="server" EnableClientScript="False" ControlToValidate="txtBillingState" InitialValue="Billing state required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingCity" runat="server" Visible="False" CssClass="floatLeftCityBillingAddress">
                                    <asp:Label ID="lblBillingCity" runat="server" Width="285px" Visible="False" CssClass="pnlLabel" Text="City"></asp:Label><br />
                                    <asp:TextBox ID="txtBillingCity" runat="server" Visible="False" Width="277px" CssClass="TextBox"
                                        onblur="txt_validate_RequiredField(this, 'Billing city required!', hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);"
                                        onfocus="txt_got_Focus(this, 'Billing city required!', hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);"
                                        onkeypress="OnMoveFocusOnEnter(event, this, 30);"
                                        onkeydown="OnEscapeKeyPressed(event, this, hdnBillingCity);" TabIndex="28"></asp:TextBox>
                                    <asp:HiddenField ID="hdnBillingCity" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityBorderWidth" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityBorderColor" runat="server" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnBillingCityFontColor" runat="server" ClientIDMode="Static" />

                                    <asp:RequiredFieldValidator ID="rfvBillingCity" runat="server" EnableClientScript="False" ControlToValidate="txtBillingCity" InitialValue="Billing city required!"></asp:RequiredFieldValidator>
                                </asp:Panel>

                                <asp:Panel ID="pnlBillingSaveButton" runat="server" Visible="False" CssClass="pnlBillingAddressSaveButton">
                                    <asp:Button ID="btnBillingAddressSave" runat="server" Visible="False" Text="Save" CssClass="BillingAddressSaveButton" OnClick="btnBillingAddressSave_Click" UseSubmitBehavior="False" TabIndex="29" />
                                </asp:Panel>
                            </asp:Panel>

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressSaveConfirmation" runat="server" TargetControlID="btnBillingAddressSaveHidden" PopupControlID="pnlBillingAddressSaveConfirmation" BehaviorID="_content_mpeBillingAddressSaveConfirmation" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlBillingAddressSaveConfirmation" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address is saved successfully!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrSaveConfirmation" runat="server" Text="Ok" OnClick="btnBillingAddressSaveConfirmation_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressSaveHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressSaveFailure" runat="server" TargetControlID="btnBillingAddressSaveFailureHidden" PopupControlID="pnlBillingAddressSaveFailure" BehaviorID="_content_mpeBillingAddressSaveFailure" DynamicServicePath=""></ajaxToolkit:ModalPopupExtender>
                            <asp:Panel ID="pnlBillingAddressSaveFailure" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address saving failed!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrSavingFailure" runat="server" Text="Ok" OnClick="btnBillingAddrSavingFailure_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressSaveFailureHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressDeletionConfirmation" runat="server" TargetControlID="btnBillingAddressDeletionConfirmationHidden"
                                PopupControlID="pnlBillingAddressDeletionConfirmation" BehaviorID="_content_mpeBillingAddressDeletionConfirmation" DynamicServicePath="">
                            </ajaxToolkit:ModalPopupExtender>

                            <asp:Panel ID="pnlBillingAddressDeletionConfirmation" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address is deleted successfully!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrDeletionConfirmation" runat="server" Text="Ok" OnClick="btnBillingAddrDeletionConfirmation_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressDeletionConfirmationHidden" runat="server" Text="Button" Style="display: none;" />

                            <ajaxToolkit:ModalPopupExtender ID="mpeBillingAddressDeletionFailed" runat="server" TargetControlID="btnBillingAddressDeletionFailedHidden"
                                PopupControlID="pnlBillingAddressDeletionFailed" BehaviorID="_content_mpeBillingAddressDeletionFailed" DynamicServicePath="">
                            </ajaxToolkit:ModalPopupExtender>

                            <asp:Panel ID="pnlBillingAddressDeletionFailed" runat="server" CssClass="pnlBackGround">
                                <br />
                                <br />
                                <span>Your Billing address deletion failed!
                                    <br />
                                    <br />
                                </span>
                                <asp:Button ID="btnBillingAddrDeletionFailed" runat="server" Text="Ok" OnClick="btnBillingAddrDeletionFailed_Click" />
                            </asp:Panel>
                            <asp:Button ID="btnBillingAddressDeletionFailedHidden" runat="server" Text="Button" Style="display: none;" />

                            <asp:Panel ID="pnlReferredBy" runat="server" Width="162px" Height="60px" CssClass="floatLeft">
                                <asp:Label ID="lblReferredBy" runat="server" Width="100px" Text="Referred By" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlReferredBy" runat="server" Enabled="False" Height="28px" Width="155px" OnSelectedIndexChanged="ddlReferredBy_SelectedIndexChanged"
                                    CssClass="DropDownList" AutoPostBack="true">
                                </asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlReferredByMembership" runat="server" Width="162px" Height="60px" CssClass="floatLeft">
                                <asp:Label ID="lblReferredByMembership" runat="server" Width="150px" Text="Membership" CssClass="pnlLabel"></asp:Label><br />
                                <asp:TextBox ID="txtReferredByMembership" runat="server" Enabled="false" Width="155px" Height="22px" CssClass="TextBox" AutoPostBack="true"
                                    OnTextChanged="txtReferredByMembership_TextChanged" ></asp:TextBox>
                            </asp:Panel>

                            <asp:Panel ID="pnlReferredByContact" runat="server" Width="162px" Height="60px" CssClass="floatLeft">
                                <asp:Label ID="lblReferredByContact" runat="server" Width="150px" Text="Referred By Contact" CssClass="pnlLabel"></asp:Label><br />
                                <asp:DropDownList ID="ddlReferredByContact" runat="server" Enabled="false" Height="28px" Width="155px" CssClass="DropDownList"></asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel ID="pnlNotifyByContainer" runat="server" Width="500px" CssClass="NotifyByContainer">
                                <asp:Panel ID="pnlNotifyBy" runat="server" CssClass="floatLeftNotifyBy" GroupingText="Notify By:">
                                    <asp:CheckBox ID="chkEmail" runat="server" Width="60px" Text="Email" Font-Size="Small" CssClass="floatLeftCheckBox" TabIndex="30"
                                        onkeypress="MoveToChkPostalOnEnter(event, this);" />
                                    <asp:CheckBox ID="chkPostal" runat="server" Width="60px" Text="Postal" Font-Size="Small" CssClass="floatLeftCheckBox" TabIndex="31"
                                        onkeypress="MoveToEmailOnEnter(event, this);" />
                                </asp:Panel>
                            </asp:Panel>

                            <asp:Panel ID="pnlJoinMailing" runat="server" Width="180px" CssClass="floatLeftJoinMailing" GroupingText="Join Mailing:">
                                <asp:RadioButton ID="rbYesJoinMailing" runat="server" Width="60px" Text="Yes" Font-Size="Small" GroupName="JoinMailing" CssClass="floatLeftRadioButton" TabIndex="32" />
                                <asp:RadioButton ID="rbNoJoinMailing" runat="server" Width="60px" Text="No" Font-Size="Small" GroupName="JoinMailing" CssClass="floatLeftRadioButton" TabIndex="33" />
                            </asp:Panel>

                            <asp:Panel ID="pnlAllowMessages" runat="server" Width="180px" CssClass="floatLeftAllowMessages" GroupingText="Allow Messages">
                                <asp:RadioButton ID="rbYesAllowMessages" runat="server" Width="60px" Text="Yes" Font-Size="Small" GroupName="AllowMessages" CssClass="floatLeftRadioButton" TabIndex="34"
                                    onkeydown="MoveToEmailOnTab(event, this);"
                                    onkeypress="MoveToEmailOnEnter(event, this);" />
                                <asp:RadioButton ID="rbNoAllowMessages" runat="server" Width="60px" Text="No" Font-Size="Small" GroupName="AllowMessages" CssClass="floatLeftRadioButton" TabIndex="35"
                                    onkeydown="MoveToEmailOnTab(event, this);"
                                    onkeypress="MoveToEmailOnEnter(event, this);" />
                            </asp:Panel>

                            <asp:Panel ID="pnlGiftSendingInfoBottom" runat="server" Width="500px" CssClass="floatLeft" Height="12px">
                            </asp:Panel>

                        </asp:Panel>

                        <ajaxToolkit:ModalPopupExtender ID="mpeSucceeded" runat="server" PopupControlID="pnlConfirmation"
                            BackgroundCssClass="modalBackground" TargetControlID="btnHiddenSuccess" BehaviorID="_content_mpeSucceeded" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlConfirmation" runat="server" CssClass="pnlBackGround">
                            <br />
                            <br />
                            <span>Your information updated successfully!
                                <br />
                                <br />
                            </span>

                            <asp:Button ID="btnModalOk" runat="server" Text="Ok" OnClick="btnModalOk_Click" />
                        </asp:Panel>
                        <asp:Button ID="btnHiddenSuccess" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpeFailed" runat="server" TargetControlID="btnHiddenFailure" PopupControlID="pnlFailure"
                            BackgroundCssClass="modalBackground" BehaviorID="_content_mpeFailed" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlFailure" runat="server" CssClass="pnlBackGround">
                            <br />
                            <br />
                            <span>The update failed!
                                <br />
                                <br />
                            </span>
                            <asp:Button ID="btnModalFailure" runat="server" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnHiddenFailure" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpePageValidationFailed" runat="server" PopupControlID="pnlPageValidationFailed"
                            BackgroundCssClass="modalBackground" TargetControlID="btnPageValidationFailureHidden" BehaviorID="_content_mpePageValidationFailed" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlPageValidationFailed" runat="server" CssClass="pnlPageValidationFailure">
                            <br />
                            <asp:Label ID="lblPageValidationFailedMessage" runat="server" Text="Error: Please correct following (highlighted) fields." ForeColor="Red" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                            <div id="PageValidationFailed" runat="server" style="max-height: 150px; overflow: auto; text-align: left; margin-left: 30px; margin-right: 20px; border: 1px solid black;">
                                <span style="width: 260px; border-width: medium; border-color: black;">
                                    <asp:Label ID="lblPageValidationFailure" runat="server" CssClass="pnlLabelChildrenInfo"></asp:Label>
                                </span>
                            </div>
                            <br />
                            <asp:Button ID="btnModalPageValidationFailed" runat="server" Width="100px" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnPageValidationFailureHidden" runat="server" Text="Button" Style="display: none;" />

                        <ajaxToolkit:ModalPopupExtender ID="mpePartialUpdateFailure" runat="server" PopupControlID="pnlPartialUpdateFailure"
                            BackgroundCssClass="modalBackground" TargetControlID="btnPartialUpdateFailureHidden" BehaviorID="_content_mpePartialUpdateFailure" DynamicServicePath="">
                        </ajaxToolkit:ModalPopupExtender>

                        <asp:Panel ID="pnlPartialUpdateFailure" runat="server" CssClass="pnlPageValidationFailure">
                            <br />
                            <asp:Label ID="lblPartialUpdateFailure" runat="server" Text="Error: Partial update failure" ForeColor="Red" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                            <div id="PartialUpdateFailure" runat="server" style="max-height: 150px; overflow: auto; text-align: left; margin-left: 30px; margin-right: 20px; border: 1px solid black;">
                                <span style="width: 260px; border-width: medium; border-color: black;">
                                    <asp:Label ID="lblPartialUpdateFailureMessage" runat="server" CssClass="pnlLabelChildrenInfo"></asp:Label>
                                </span>
                            </div>
                            <br />
                            <asp:Button ID="btnModalPartialUpdateFailure" runat="server" Width="100px" Text="Ok" />
                        </asp:Panel>
                        <asp:Button ID="btnPartialUpdateFailureHidden" runat="server" Text="Button" Style="display: none;" />

                        <br />


                        <asp:Panel ID="pnlMessageLabel" runat="server" CssClass="GeneralInfoLabelPanel">
                            <asp:Label ID="lblHdnTelephoneBorderWidth" runat="server" Text="Hidden Border Width: "></asp:Label><br />
                            <asp:Label ID="lblHdnTelephoneBorderColor" runat="server" Text="Hidden Border Color: "></asp:Label><br />
                            <asp:Label ID="lblHdnTelephoneForeColor" runat="server" Text="Hidden Font Color: "></asp:Label>
                        </asp:Panel>

                        <br />

                        <asp:Panel ID="pnlUpdateCancel" runat="server" Width="400px" CssClass="GeneralInfoButtonPanel">

                            <asp:Button ID="btnLogout" runat="server" Width="80px" CssClass="GeneralInfoUpdateCancelButton" Text="Log Out" OnClick="btnLogout_Click" UseSubmitBehavior="False" />
                            <asp:Button ID="btnUpdate" runat="server" Width="80px" CssClass="GeneralInfoUpdateCancelButton" Text="Update" OnClick="btnUpdate_Click" UseSubmitBehavior="False" />
                        </asp:Panel>



                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlHealthHistory" runat="server" HeaderText="Health History" Height="1500px" CssClass="TopTabPanel">
                    <ContentTemplate>
                        <div style="font-size: larger; font-weight: bold; text-align: center; margin-bottom: 10px;">Health History: 건강 확인서 </div>
                        <br />

                        <asp:Panel ID="pnlHealthHistory" runat="server"></asp:Panel>

                        <asp:Panel ID="pnlTreatmentHistory" runat="server" Style="margin-top: 20px; margin-bottom: 20px; margin-left: 50px;">
                            <asp:ListView ID="lvTreatmentHistory" runat="server" OnItemDataBound="lvTreatmentHistory_ItemDataBound">
                                <LayoutTemplate>
                                    <table id="tblTreatmentHistory" runat="server" cellpadding="5" class="tableTreatmentHistory">
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </LayoutTemplate>

                                <ItemTemplate>
                                    <tr runat="server">
                                        <td runat="server" class="tcTitlePatientName">
                                            <asp:Label ID="lblPatientName" runat="server" Text="Name | 이름" Width="550"></asp:Label>
                                        </td>

                                        <td runat="server" class="tcContentPatientName">
                                            <%--                                            <asp:DropDownList ID="ddlPatientName" runat="server" Height="25" Width="200"></asp:DropDownList>--%>
                                            <asp:TextBox ID="txtPatientName" runat="server" Height="25" Width="200" ReadOnly="true"></asp:TextBox>
                                            <%--                                            <asp:Button ID="btnRemove" runat="server" Text="X" Width="20" Height="20" Font-Size="Small" CommandName="Delete"
                                                Style="text-align: center; vertical-align: middle; position: relative; float: right;" />--%>
                                        </td>
                                    </tr>

                                    <tr runat="server">
                                        <td runat="server" class="tcTitleHouseholdRole">
                                            <asp:Label ID="lblHouseholdRole" runat="server" Text="Member Type" Width="550"></asp:Label>
                                        </td>

                                        <td runat="server" class="tcContentHouseholdRole">
                                            <%--                                            <asp:DropDownList ID="ddlHouseholdRole" runat="server" Height="25" Width="200">
                                                <asp:ListItem>Head of Household</asp:ListItem>
                                                <asp:ListItem>Spouse</asp:ListItem>
                                                <asp:ListItem>Child</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtHouseholdRole" runat="server" Height="25" Width="200" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr runat="server">
                                        <td runat="server" class="tcTitleTreatmentDate">
                                            <asp:Label ID="lblTreatmentDate" runat="server" Text="Treatment Date | 치료일자" Width="550"></asp:Label>
                                        </td>

                                        <td runat="server" class="tcContentTreatmentDate">
                                            <asp:TextBox ID="txtTreatmentDate" runat="server" Height="25" Width="200" ReadOnly="true"></asp:TextBox>
                                            <%--                                            <ajaxToolkit:CalendarExtender ID="calTreatmentDate" runat="server" TargetControlID="txtTreatmentDate" DefaultView="Years" />--%>
                                        </td>
                                    </tr>

                                    <tr runat="server">
                                        <td runat="server" class="tcTitleTreatmentDetails">
                                            <asp:Label ID="lblTreatmentDetailsEN" runat="server" Text="Diagnosis | Duration | Results | Tests Performed | Medication | Outcome" Width="550"></asp:Label><br />
                                            <asp:Label ID="lblTreatmentDetailsKO" runat="server" Text=" 병명 | 기간 | 결과 | 검사 | 투약 | 경과" Width="550"></asp:Label>
                                        </td>
                                        <td runat="server" class="tcContentTreatmentDetails">
                                            <asp:TextBox ID="txtTreatmentDetails" runat="server" TextMode="MultiLine" CssClass="txtScrollBar" Height="50" Width="350" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr runat="server">
                                        <td runat="server" class="tcTitlePhysicianInfo">
                                            <asp:Label ID="lblPhysicianInfoEN1" runat="server" Text="Attending Physician's Name, Address and Phone Number" Width="550"></asp:Label><br />
                                            <%--                                            <asp:Label ID="lblPhysicianInfoEN2" runat="server" Text="Address and Phone Number" Width="580"></asp:Label><br />--%>
                                            <asp:Label ID="lblPhysicianInfoKO" runat="server" Text="의사이름, 주소 및 전화번호" Width="550"></asp:Label>
                                        </td>
                                        <td runat="server" class="tcContentPhysicianInfo">
                                            <asp:TextBox ID="txtPhysicianInfo" runat="server" TextMode="MultiLine" CssClass="txtScrollBar" Height="50" Width="300" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr runat="server" class="trSeparator" />

                                </ItemTemplate>
                            </asp:ListView>
                        </asp:Panel>


                        <%--                        <asp:Table ID="tblHealthHistory" Width="1000px" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" GridLines="Both" HorizontalAlign="Center" >
                            <asp:TableHeaderRow runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" GridLines="Both" HorizontalAlign="Center" >
                                <asp:TableHeaderCell ColumnSpan="2" style="padding-top: 15px; padding-bottom: 15px; padding-left: 100px; padding-right: 100px; ">
                                    Has any person listed received medical attention and/or had surgery done in any hospital or similar institution?<br />
                                    Please check below, and if answer to any of the listed is YES, explain it in the box below:<br />
                                    아래에 기재된 사항에 이상이 있거나 있었다면 (V)하시고, 그 내용을 아래칸에 기록하여 주시기 바랍니다.
                                </asp:TableHeaderCell>
                            </asp:TableHeaderRow>

                            <asp:TableRow ID="trHealthHistory1" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Please record the new member’s health history. This documentation serves to protect our present members. If a member provides false statement on this form, this may be a cause for immediate termination from Christian Mutual Med-Aid. 기존 회원들의 보호를 위해 새 신청자는 사실을 기록해야 합니다. 사실이 아닌 기록을 했을 경우에는 회원 자격이 박탈 될 수도 있습니다.
                                </asp:TableCell>
                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:Label ID="lblMemberName" runat="server" Text="Harris J. Park" ></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory2" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Have any person listed been treated by a doctor during the last year? (Including annual check-up) <br /> 지난 1년간 치료를 받은 일이 있습니까? (정기검진 포함)
                                </asp:TableCell>
                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkIsTreatedByDoctor" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory3" runat="server" BackColor="LightGray" >
                                <asp:TableCell width="350px" CssClass="MedicalCondition" >
                                    Diagnosed with high blood pressure, diabetes, heart, vascular disease.<br />고혈압, 당뇨병, 심장병, 뇌졸증 및 혈관 질환
                                </asp:TableCell>
                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkHighBloodPressure" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory4" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Diagnosed with allergy, asthma or respiratory problem<br />알레르기, 천식 및 호흡기 관련 질환
                                </asp:TableCell>
                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkAllergy" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>

                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory5" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Diagnosed with arthritis, rheumatoid arthritis, chronic back pain, nerve system etc.<br />관절염, 류마티스, 척추 및 신경계통 관련 질환 등
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkArthritis" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>

                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory6" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Medical conditions related to eyes, nose, ears, hands, feet<br />눈, 코, 귀, 손 발 관련 질환
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkEye" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>

                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory7" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Medical conditions related to stomach, liver, colon, kidney etc.<br />위, 간, 대장, 신장 등 장기 관련 질환
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkStomach" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory8" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Medical conditions related to thyroid, tumor, cancer etc.<br />갑상선, 각종 종양 및 암
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkThyroid" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory9" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Medical conditions related to prostate or female reproduction organs<br />부인과 질환 또는 전립선 관련 질환
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkProstate" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="trHealthHistory10" runat="server" BackColor="LightGray" >
                                <asp:TableCell Width="350px" CssClass="MedicalCondition" >
                                    Congenital disease and other medical conditions<br />선천적 질환 및 기타 다른 질환
                                </asp:TableCell>

                                <asp:TableCell Width="450px" HorizontalAlign="Center" VerticalAlign="Middle" >
                                    <asp:CheckBox ID="chkCongenital" runat="server" Text="" CssClass="LargeChkBoxClass" />
                                </asp:TableCell>
                            </asp:TableRow>

                        </asp:Table>--%>

                        <asp:Table ID="tblOtherConcerns" runat="server" Width="1000px" Height="50px" BackColor="White"
                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" GridLines="Both" HorizontalAlign="Center">
                            <asp:TableHeaderRow runat="server" BackColor="Gray">
                                <asp:TableHeaderCell runat="server" Height="20px" HorizontalAlign="Left" Style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px;">Other Concerns</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow ID="trOtherConcern" runat="server">
                                <asp:TableCell Height="40px" Style="padding-left: 5px; padding-top: 5px; padding-bottom: 5px;">
                                    <asp:Label ID="lblOtherConcern" runat="server" Text="No register found"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>


                        <%--<asp:Panel ID="pnlAttest" runat="server" Width="1000px" Height="300px" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center">--%>
                        <asp:Panel ID="pnlAttest" runat="server" CssClass="clsAttest">
                            <div style="padding: 15px; font-size: small; font-weight: normal; text-align: left; margin-top: 15px;">
                                I attest that the participating members of my family are Christians by biblical principles. All applying members of my family attend church regularly; 
                                alll abstain from tobacco; all abstain from the use of drugs illegally; all follow the biblical teaching on the use of alcohol; 
                                and all commit to follow the commands of Jesus Christ described in the Bible. I declare that the information on this application 
                                is complete and true to the best of my knowledge.
                                <br />
                                본인(가족)은 성서의 가르침에 따르는 성도임을 증언합니다. 신청한 가족들은 정기적으로 교회에 출석하고, 흡연과 불법 약물 복용, 음주를 하지 않고, 성경적 결혼관을 지니고, 
                                예수 그리스도의 명령을 따라 살아가기로 헌신 하였습니다. 본인은 이 신청서에 있는 내용이 모두 완전하고 진실한 것음을 확인합니다.
                            </div>

                            <div style="padding: 15px; font-size: small; font-weight: normal; text-align: left; margin-bottom: 15px;">
                                Christian Mutual Med-Aid (CMM) acts only as a facilitator to bring together members with resources and desires to help other members with medical costs. 
                                I will not bring suit, legal claim or demand of any sort of unpaid medical expenses against Logos Missions, Inc. 
                                I understand my false statement on this online application form may be a cause for immediate termination from CMM. 
                                I understand I will receive reminders by the 10th of each month for each person in need. 
                                I promise to send my gifts for these persons and will pray for them. CMM will receive my monthly gift by the 1st of each month.
                                <br />
                                CMM은 의료비를 통해 회원들끼리 서로 돕도록 하는 역할을 수행합니다. 본인은 로고스선교회를 상대로 미지급된 의료비에 대하여 소송이나 법적 청구 및 요구를 하지 않겠습니다. 
                                본인은 온라인 신청서를 허위기재 했을 경우, CMM에 의해 즉각 회원 자격이 취소됨을 받아들입니다. 본인은 회원들의 필요를 위해 매월 10일에 발송되는 회비 안내서를 받는 것에 동의합니다. 
                                본인은 도움이 필요한 이들을 위해 회비를 보내고 기도할 것을 약속합니다. CMM은 매월 1일까지 본인의 회비를 받게 될 것입니다.
                            </div>

                        </asp:Panel>



                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlAgreement" runat="server" HeaderText="Agreement" Height="2200px" CssClass="TopTabPanel">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAgreement" runat="server" CssClass="clsAgreementPanel">

                            <asp:Panel ID="pnlAgreementHeading" runat="server" HorizontalAlign="Center" Style="margin-top: 100px; margin-bottom: 50px;">
                                <asp:Label ID="lblAgreementHeading" runat="server" Text="Membership Agreement & Checklist of Understanding"
                                    Font-Bold="true" Font-Size="Larger" ForeColor="Black" Font-Names="Arial"></asp:Label>
                            </asp:Panel>
                            <asp:Table ID="tblAgreement" runat="server" HorizontalAlign="Center" CellPadding="10" CellSpacing="0" Width="900" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell ID="thrAgreement" runat="server" ColumnSpan="2" Font-Names="Arial" Font-Size="Medium" HorizontalAlign="Center" BackColor="LightBlue"
                                        CssClass="tcAgreementHeaderText" Height="80">
                            The Primary Member, on behalf of the entire household, must read, initial, and sign the following:<br />
                            주 회원 (Primary Member)이 가족 구성원을 대표하여 아래의 내용을 읽고, 각 항목에 영문 이니셜을 적고 서명합니다.
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem1" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem1" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence1" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence1EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that CMM members share one another’s burdens according to the Biblical teachings of 
                                Galatians 6:2, 10(b), Acts 4:35(b), and 2 Corinthians 8:14.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence1KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 성경의 가르침(갈라디아서 6:2, 6:10(b), 사도행전 4:35(b), 고린도후서 8:14)에 따라서, 기독의료상조회 회
                                원들이서로의 짐을 나누어 지는 것을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem2" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem2" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence2" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence2EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that CMM is a health care sharing ministry, not a health insurance company, and, as
                                such, that CMM guarantees nothing to its participating members. I (We) further understand that CMM is
                                not approved nor endorsed by the Department of Insurance in my (our) State of residence, and that my
                                claims or losses are not protected by my (our) State’s Guaranty Fund.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence2KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 기독의료상조회가 건강보험회사가 아니라 의료비 나눔 선교회임을 이해하며, 의료비 나눔에 동참한 회원
                                들에게 어떤 보장도 주어지지 않는다는 것을 이해합니다. 그뿐 아니라 기독의료상조회는 본인(가족)이 거주하는 주의
                                보험국에서 인증 받거나 보증되지 않음을 이해하며, 지불 청구나 손실은 거주 하는 주의 보증 기금에 의해 보호받지 못
                                함을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem3" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem3" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence3" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence3EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I am a (We are) Christian(s) that live(s) according to Biblical principles and attend(s) church regularly.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence3KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인 (가족)은 주일 성수를 하며 성경의 가르침에 따라 살아가는 기독교인임을 확인합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem4" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem4" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence4" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence4EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) abstain from tobacco, illegal drugs, the improper or unauthorized use of prescription medications or
                                over-the-counter medications, and alcohol. I (We) also do not pursue homosexuality.                            </asp:Label>
                                        <asp:Label ID="lblAgreementSentence4KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족) 은 흡연, 불법 약물 복용, 처방약 및 기타 약물 오남용과 알코올 복용을 하지 않으며 동성애의 삶을 추구하지
                                않는다는 사실을 확인합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem5" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem5" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence5" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence5EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that my (our) monthly Gift to support other members is due by the 1st of the month.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence5KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 다른 회원을 돕기 위해 매월 1일까지 회비를 납부해야 함을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem6" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem6" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence6" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence6EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that qualifying medical bills for the new member will be eligible for sharing after a 90-
                                day waiting period.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence6KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 가입 후 90일간의 대기기간이 있음을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem7" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem7" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence7" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence7EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that the eligibility of my (our) submitted medical bills is determined in accordance with
                                the CMM Guidelines.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence7KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인 (가족)은 제출한 의료비의 지원 여부가 CMM Guidelines에 의해 결정됨을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem8" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem8" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence8" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence8EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that medical expenses that occurred prior to my (our) membership will not be supported
                                by CMM.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence8KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 기독의료상조회 가입 이전에 발생한 의료비는 지원되지 않음을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem9" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem9" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence9" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence9EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that I am responsible for my (our) medical bills regardless of whether CMM will support
                                my (our) medical bills or whether CMM continues to operate.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence9KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 본인(가족)의 의료비 지원 여부 및 기독의료상조회의 존재 여부에 관계없이 본인(가족)의 의료비에 대한
                                책임이 본인(가족)에게 있음을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem10" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem10" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence10" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence10EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that I (we) must notify CMM prior to seeking medical services.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence10KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 의료 기관을 방문하기 전에 CMM 에 보고해야 하는 의무가 있음을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem11" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem11" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence11" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence11EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that I (we) must register as a Self-Payer with all medical providers.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence11KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 의료 기관을 방문할 때, Self-Payer로 등록해야 한다는 것을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem12" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem12" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence12" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence12EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that I must request discounts, fee adjustments, or financial assistance, such as Charity
                                Care, from all medical providers.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence12KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 할인, 가격 조정 혹은 Charity Care와 같은 재정 지원 프로그램을 의료 기관에 요청해야 함을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem13" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem13" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence13" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence13EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that all members must support and contribute to another member’s excess medical bills
                                when the qualifying medical need exceeds $150,000 through Burden-Sharing.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence13KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인 (가족) 은 어느 회원이 지원 요청한 의료비가 $150,000을 초과할 경우, Burden-Sharing프로그램을 통해 모든 회원
                                들이 초과한 의료비를 나누어 지원해야 함을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem14" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem14" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence14" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence14EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that my (our) monthly Gift will increase by 0.1% per dollar shared if my (our) shared
                                need exceeds $10,000.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence14KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 $10,000 이상의 의료비를 지원받을 경우, 지원받은 금액의 0.1%가 월 회비에 추가됨을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem15" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem15" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence15" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence15EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that no legal contract or obligation exists between CMM and the individual member
                                regarding indemnification of medical expenses.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence15KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 기독의료상조회와 회원 간에 의료비 보증이나 의료비 배상에 관한 어떤 법적 계약이나 의무도 존재하지
                                않음을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem16" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem16" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence16" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence16EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that CMM members submit monthly Gifts for the purpose of sharing one another’s
                                burdens. As such, I (we) further understand that using a shared Gift for a purpose other than the intended
                                purpose would be an abuse of trust. By doing so, I (we) understand that my (our) medical bills submitted
                                for sharing will be refused, my (our) membership will be terminated, and I (we) will not be eligible to reapply.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence16KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 서로의 짐을 나누어 지기 위해 기독의료상조회 회원들이 매월 회비를 납부하고 있음을 이해합니다. 따라
                                서 상조회를 통해 지원 받은 의료비를 다른 목적으로 사용하는것은 회원들의 믿음을 악용하는 것이며, 이러한 상황이
                                발생했을 경우에 제출된 의료비는 지원이 불가하고, 회원 자격이 박탈되며, 재가입이 불가능함을 이해합니다.                            
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem17" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem17" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence17" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence17EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that my (our) membership account must be current and in good standing, in order to
                                have eligible medical needs shared within the program.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence17KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)이 의료비 지원을 받으려면, 회원 자격을 계속 유지해야 하며 미납된 회비가 없어야 함을 이해합니다.                            
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem18" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem18" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence18" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence18EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand that a portion of my monthly Gift(s) is used to support the ministry of Logos Missions,
                                Inc.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence18KO" runat="server" CssClass="lblAgreementSentenceKO">
                                나/본인은 월 회비의 일부분이 로고스선교회 운영비로 사용되어짐을 이해합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell ID="tcAgreementItem19" runat="server" Width="60" CssClass="tcLargeChkBoxClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkAgreementItem19" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcAgreementSentence19" runat="server" Width="800" HorizontalAlign="Left" CssClass="tcAgreementContentText">
                                        <asp:Label ID="lblAgreementSentence19EN" runat="server" CssClass="lblAgreementSentenceEN">
                                I (We) understand and agree as a CMM member that any controversy or disagreement with CMM will be
                                resolved through Biblically-based mediation or Christian Alternate Dispute Resolution as detailed in the
                                CMM Guidelines. I (We) waive any right to file a lawsuit or claim against Logos Missions, Inc. or its
                                officers, directors, or employees. I (We) will not seek any unpaid medical expenses from Logos Missions,
                                Inc. or its officers, directors, or employees.
                                        </asp:Label>
                                        <asp:Label ID="lblAgreementSentence19KO" runat="server" CssClass="lblAgreementSentenceKO">
                                본인(가족)은 기독의료상조회의 회원으로서, 기독의료상조회와 의견 불일치 혹은 논란이 생길 경우에 가이드라인에 명
                                시되어 있는 대로 기독교적인 합의 혹은 대체 방법을 통해 해소해야 함을 이해하고 동의하며, 로고스 선교회와 선교회
                                의 임직원을 상대로 한 소송이나 법적 청구 권리를 포기하며, 미납된 의료비를 로고스 선교회와 선교회의 임직원에게
                                청구하지 않을 것을 확인합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>

                            <asp:Table ID="tblConfirmation" runat="server" HorizontalAlign="Center" CellPadding="10" CellSpacing="0" Style="margin-top: 20px;"
                                Width="900" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">

                                <asp:TableRow>
                                    <asp:TableCell ID="tcConfirmation" runat="server" Width="100" CssClass="tcLargeChkBoxConfirmationClass" HorizontalAlign="Center">
                                        <asp:CheckBox ID="chkConfirmation" runat="server" CssClass="LargeChkBoxClass" Checked="true" Enabled="false" />
                                    </asp:TableCell>

                                    <asp:TableCell ID="tcConfirmationText" runat="server" Width="800" CssClass="tcAgreementConfirmationText">
                                        <asp:Label ID="lblConfirmationTextEN" runat="server" CssClass="lblAgreementConfirmationTextEN">
                                I/We have read and fully understand the CMM membership agreement as stated, I/My family would like to become active members of CMM.
                                        </asp:Label>
                                        <asp:Label ID="lblConfirmationTextKO" runat="server" CssClass="lblAgreementConfirmationTextKO">
                                본인(가족)은 위에 명시된 기독의료상조회의 회원 동의서를 숙지했으며 아래에 서명함으로써 기독의료상조회의 회원으로 가입하기 원합니다.
                                        </asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>


                            </asp:Table>

                            <asp:Table ID="tblSignature" runat="server" HorizontalAlign="Center" CellPadding="10" CellSpacing="0" Style="margin-top: 20px; margin-bottom: 20px;"
                                Width="900" Height="100" BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                                <asp:TableRow>
                                    <asp:TableCell ID="tcPrimarySignature" runat="server" Width="650">
                                        <asp:TextBox ID="txtPrimarySignature" runat="server" ReadOnly="true" Font-Size="Large" Height="50" Width="600" Style="text-align: center;"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcSignedDate" runat="server" Width="250">
                                        <asp:TextBox ID="txtSignedDate" runat="server" ReadOnly="true" Font-Size="Large" Width="200" Height="50" Style="text-align: center;"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>


                            <%--                            <div id="AgreementTitle" style="text-align: center; font-family: Arial; font-size: large; font-weight: bolder; margin-top: 25px; margin-bottom: 15px;">FOLLOW UP PARTICIPANT FORM</div>

                            <div id="headerNoteEnglish" style="text-align: left; font-size: small; margin-top: 10px; margin-bottom: 10px; padding-left: 20px; padding-right: 20px;">
                                Christian Mutual Med-Aid (CMM) is a program created to help fulfill biblical admonitions. CMM has no control over the actions of churches or participants. 
                                Whether you send your gift to the one in need, through the church, or do not send a gift at all?it is purely your decision.
                            </div>

                            <div id="headerNoteKorean" style="text-align: left; font-size: small; margin-top: 10px; margin-bottom: 10px; padding-left: 20px; padding-right: 20px;">
                                기독의료상조회(이하 ‘CMM’)는 예수님의 사랑을 실천하려는 도구이므로 교회나 개인들이 보내는 월 회비를 통제하지 않습니다. 
                                CMM의 근본취지는 회원들로 하여금 월 회비를 직접 혹은 교회를 통하여 의료비가 필요한 이들에게 보내는 것이며, 본인 스스로의 결정에 따라 이 방법을 따르지 않을 수도 있습니다.
                            </div>

                            <asp:Table ID="tblCheckListOfUnderstanding" runat="server" Width="980px" BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black"
                                GridLines="Both" HorizontalAlign="Center" CellPadding="10" CellSpacing="10">

                                <asp:TableHeaderRow ID="thrCheckList" runat="server">
                                    <asp:TableHeaderCell ColumnSpan="2" BackColor="LightBlue">
                                        <div style="font-size: medium; font-weight: bold; text-align: center; font-family: Arial; padding-top: 5px; padding-bottom: 5px;">CHECKLIST OF UNDERSTANDING</div><br />
                                        <div style="font-size: small; font-weight: normal; text-align: center; margin-top: 5px;">Christian Mutual Med-Aid must have this form on file for all participants. Please read carefully and check each of the boxes:</div>
                                        <div style="font-size: small; font-weight: normal; text-align: center; margin-top: 5px;">CMM은 모든 회원들로부터 본 양식서를 받아 보관합니다. 주의하여 읽으시고 각 항목에 체크 하십시오.</div>
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>

                                <asp:TableRow ID="trCheckListRow1" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList1" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>

                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div style="padding: 5px; font-size: smaller; font-weight: normal; text-align: left; margin-top: 5px;">
                                            I / We understand that the participants of CMM participate because they desire to share the needs of others and have their own needs shared in a manner
                                            as closely as possible to that outlined in Scripture (Gal 6:2 and 6:10(b), Act 4:35(b) and II Cor 8:14).
                                        </div>

                                        <div style="padding: 5px; font-size: smaller; font-weight: normal; text-align: left; margin-top: 5px;">
                                            본인(가족)은 성경 말씀 갈6:2과 6:10, 행4:35 그리고 고후 8:14을 토대로 하여CMM의 프로그램이 회원들의 자발적인 참여로 인하여 의료비가 필요한 이들을 위해 나눔을 실천한다는 것을 이해합니다. 
                                        </div>

                                    </asp:TableCell>

                                </asp:TableRow>


                                <asp:TableRow ID="trCheckListRow2" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px" Height="200px">
                                        <asp:CheckBox ID="chkList2" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>

                                    <asp:TableCell Width="960px" BackColor="#ccccff" Height="200px">
                                        <div class="AgreementCheckList">
                                            I / We understand that my monthly check contribution to CMM will help in the following ways:
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 기독의료상조회(이하 ‘CMM’)로 보내는 월 회비가 아래와 같이 사용됨을 이해합니다.
                                        </div>
                                        <div>
                                            <ul class="AgreementCheckList">
                                                <li>Keep information that concerns my / my family's membership<br />CMM은 회원이나 가족에 대한 정보를 보관합니다.</li>
                                                <li>Share qualifying needs as outlined in the CMM Guidelines<br />CMM은 의료비 도움이 필요한 이들을 가이드라인에 의거하여 결정합니다.</li>
                                                <li>Print, publish, and mail a newsletter each month listing those in need so that I may pray for them, send cards or letters of 
                                                    encouragement and/or gift of money if I so choose<br />
                                                    CMM소식을 위해 출판 및 인쇄, 우송을 위한 경비를 지출하며 도움을 요청하는 이들을 위해 기도하며 카드나 편지, 위로금을 보내어 용기를 주는 데 사용합니다.</li>
                                                <li>Included with my monthly gift contribution, I will receive 12 issues of the Korean Christian Journal.<br />
                                                    CMM은 매월 CMM 소식을 알리기 위해 크리스찬 저널을 발행하여 회원에게 보내드립니다.</li>
                                            </ul>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow3" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList3" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>

                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList">
                                            I / We understand that eleven months of the year my gift will be shared with participants in need and one month of the year my gift will go towards 
                                            my CMM membership fee. I understand that these funds will be used at the discretion of Logos Missions, Inc.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 1년 중 11개월 회비는 의료비가 필요한 이들을 위해 사용되며, 나머지1개월의 회비는CMM 연회비로 쓰임을 이해합니다. 이 회비는CMM의 활동을 위해 쓰이는 것을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow4" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList4" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />

                                    </asp:TableCell>

                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList">
                                            I / We understand that the participants are Christians living by biblical principles, and that all participants attend church regularly 
                                            (3 out of 4 weeks, weather and health permitting). 
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 예수 그리스도를 구주로 믿으며 성경적 가정과 가치관을 가지며, 정기적으로 교회에 출석해야 함을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow5" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList5" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that the participants of CMM abstain from tobacco, illegal drugs, abuse of alcohol and a homosexual lifestyle.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 흡연, 불법 약물 복용, 음주나 문란한 성생활을 하지 않음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow6" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList6" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that if my medical needs are ever submitted to CMM for support, they may accept or reject them for support as they see fit according to the CMM Guidelines.                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은CMM이 의료비 지불 청구서에 대한 지불 가부를 결정함을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow7" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList7" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                       <div class="AgreementCheckList" >
                                            I / We understand that I need to notify CMM for hospital or any other medical facilities visits for medical needs.
                                       </div>
                                       <div class="AgreementCheckList">
                                            본인(가족)은 치료를 위해 의료기관을 방문할 때, 이를 CMM 사무실에 알려야 함을 이해합니다.
                                       </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow8" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList8" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that if I ever have any medical needs, I remain responsible for payment to the provider whether or not other members may send money to help share those needs. 
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)이 의료비를 필요로 할 때에는 모든 청구서에 대한 책임이 본인에게 있음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow9" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList9" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that I need to submit ‘NEEDS PROCESSING FORMS’ and ‘ITEMIZED BILLS’ for medical support. 
                                         </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 의료비 요청을 위해서 ‘NEEDS PROCESSING FORMS’과 ‘ITEMIZED BILLS’을 첨부하여 제출하여야 함을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow10" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList10" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that my monthly check needs to be submitted by 1st of every month to support other participants who are in need. 
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 CMM이 회원들의 의료비를 지불할 수 있도록 매월 회비를 1일까지 보내어 도와 주는 것을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow11" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList11" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that if a participant submits qualifying medical needs exceeding over $150,000, then all the participants of CMM 
                                            will support and contribute the participant in need.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 $150,000이상 초과한 회원들의 의료비가 나왔을 경우에 전체회원이 서로 나누어 의료비를 지불하는 것을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow12" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList12" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that if my medical expenses are exceed over $10,000, then then my monthly gift will increase by 0.1%. 
                                        </div>
                                        <div class="AgreementCheckList">
                                           본인(가족)은 치료비가 $10,000 이상을 넘을 경우, 전체 회원의 회비인상 요인을 방지하기 위해 0.1%가 회비에 추가됨을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow13" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList13" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I/ We understand that when visiting medical facilities, I am to register as a Self-Payer.
                                        </div>
                                        <div class="AgreementCheckList">
                                           본인(가족)은 의료기관 방문시 Self Payer로 등록해야 하는 것을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListRow14" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList14" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that if I ever have any medical needs, I will apply for charity care through the medical provider or will request a self-pay discount at the time of service.
                                        </div>
                                        <div class="AgreementCheckList">
                                           본인(가족)은 CMM의 의료비 지출을 줄이기 위해 의료진 또는 병원기관 할인 요청을 해야 됨을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListBox15" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList15" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that CMM cannot share any medical needs that occurred before participating in the program.
                                        </div>
                                        <div class="AgreementCheckList">
                                           본인(가족)은 가입 이전의 증상과 치료와 연관된 의료비를 CMM에 제출할 수 없음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListBox16" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList16" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that I am under no compulsion to give to the medical needs of other participants in the CMM. I understand that I have no legal right to receive money 
                                            from CMM itself or other participants if I have a medical need which I would like to share. I will not bring suit, legal claim or demand of any sort of 
                                            unpaid medical expenses against CMM.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 의료비를 지불하라고CMM에 속한 다른 회원들을 강요하지 않습니다. 다른 회원들과 함께 상조하려는 것이 법적 의무가 없듯이, 본인의 의료비가 필요한 경우에도CMM이나 다른 회원들로부터 
                                            재정적 지원을 받아야 한다는 법적 권리가 없으며 가이드라인에 저촉된 의료비 문제로 CMM을 상대로 고소할 수 없음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListBox17" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList17" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand CMM is a Health Care Sharing Ministry (HCSM) and NOT insurance. Therefore, there are no guarantees given to those who participate in the program. 
                                            In addition, CMM is not approved or endorsed by the Department of Insurance in my state and claims or losses are not protected by the state guaranty fund.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은CMM의 프로그램이 일반보험회사가 아님을 이해하며 이에 따라 어떠한 법적 보증을 하지 않는 것과 주정부 보험국에 손해배상 지불청구를 할 수 없음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListBox18" runat="server" BackColor="White">
                                    <asp:TableCell Width="20px">
                                        <asp:CheckBox ID="chkList18" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                    </asp:TableCell>
                                    <asp:TableCell Width="960px" BackColor="#ccccff">
                                        <div class="AgreementCheckList" >
                                            I / We understand that participants send money for one another out of a desire to share each other’s burden, and so it would be an abuse of their trust if I use the money 
                                            I receive for some purpose other than payment of that need and will be immediately terminated. I understand that if I do so I will not be eligible to re-apply or 
                                            to have additional needs shared. I understand my false statement on an application form may be cause for immediate termination from CMM.
                                        </div>
                                        <div class="AgreementCheckList">
                                            본인(가족)은 모든 회원들의 회비가 의료비의 짐을 서로 나누기 위해 사용되므로, 본인이 필요할 때 지원 받는 재정은 꼭 의료비 지불을 위해 사용해야 함을 이해합니다. 만일 다른 목적으로 사용하였을 
                                            경우 남은 의료비도 지원되지 않을 뿐 아니라 회원 자격도 상실되어 재가입할 수 없음을 이해합니다. 또한 회원가입 신청서의 내용을 허위 기재하였을 경우 회원자격이 박탈될 수 있음을 이해합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trCheckListBox19" runat="server" BackColor="#ffcccc">
                                    <asp:TableCell ColumnSpan="2">
                                        <div style="text-align: center;">
                                            <asp:CheckBox ID="chkAttest" runat="server" Checked="true" Enabled="false" CssClass="chklstAgreement" />
                                        </div>
                                        <div style="font-size: smaller; text-align: center;">
                                            I/We have read and fully understand the CMM membership agreement as stated. I/My family would like to become active members of CMM.<br />
                                            본인(가족)은 위에 명시된 기독의료상조회의 회원 동의서를 숙지 했으며 아래에 서명함으로써 기독의료상조회의 회원으로 가입하기 원합니다.
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>--%>
                        </asp:Panel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlGiftHistory" runat="server" HeaderText="Gift History" Height="650px">
                    <ContentTemplate>
                        <asp:Panel ID="pnlGiftHistorySummary" runat="server" GroupingText="Summary" Font-Bold="true" Font-Size="X-Large" Width="1080px" Style="margin-top: 15px;">
                            <asp:Panel ID="pnlGiftHistoryTable" runat="server" Width="1050px"></asp:Panel>

                            <%--                            <asp:Table ID="tblGiftHistorySummary" runat="server" Font-Size="Small" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" GridLines="Both"
                                Width="1050px" HorizontalAlign="Center" CellPadding="5" CellSpacing="5" Style="margin-top: 15px;">
                                <asp:TableHeaderRow runat="server" BackColor="LightGray" Font-Size="Small" Height="35px">
                                    <asp:TableHeaderCell Width="80px"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="600px" Font-Bold="false" Font-Size="Small" Style="text-align: left;">Member</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="180px" Font-Bold="false" Font-Size="Small">Total</asp:TableHeaderCell>
                                </asp:TableHeaderRow>

                                <asp:TableRow ID="trMemberName" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Name</asp:TableCell>
                                    <asp:TableCell ID="tcMemberName" runat="server" Font-Bold="false" Font-Size="Small">Park, Harris J.</asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="false" Font-Size="Small"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trMemberID" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Id</asp:TableCell>
                                    <asp:TableCell ID="tcMemberID" runat="server" Font-Bold="false" Font-Size="Small">55505</asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="false" Font-Size="Small"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trRegDate" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Reg. Date</asp:TableCell>
                                    <asp:TableCell ID="tcRegDate" runat="server" Font-Bold="false" Font-Size="Small">06/24/2009</asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="false" Font-Size="Small"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trProgram" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Program</asp:TableCell>
                                    <asp:TableCell ID="tcProgram" runat="server" Font-Bold="false" Font-Size="Small">Gold Plus</asp:TableCell>
                                    <asp:TableCell runat="server" Font-Bold="false" Font-Size="Small"></asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trGiftAmount" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Gift Amount</asp:TableCell>
                                    <asp:TableCell ID="tcGiftAmount" runat="server" Font-Bold="false" Font-Size="Small">$175.00</asp:TableCell>
                                    <asp:TableCell ID="tcGiftTotal" runat="server" Font-Bold="false" Font-Size="Small" HorizontalAlign="Center" ForeColor="DarkBlue" Style="font-weight: bold;">$175.00</asp:TableCell>
                                </asp:TableRow>

                                
                            </asp:Table>--%>

                            <asp:Panel ID="pnlCurrentBalance" runat="server" BackColor="LightGray" Style="float: right; vertical-align: bottom; padding: 5px; margin-top: 15px; width: auto; height: 30px;" Font-Size="Small">
                                <asp:Label ID="lblCurrentBalanceValue" runat="server" Font-Bold="true" Font-Size="Small" Text="175.00" ForeColor="DarkBlue" Style="float: right; margin-left: 0px; margin-right: 5px; margin-top: 6px;"></asp:Label>
                                <asp:Label ID="lblCurrentBalanceLabel" runat="server" Font-Bold="false" Font-Size="Small" Text="Current Balance: $" Style="float: right; margin-left: 5px; margin-right: 0px; margin-top: 6px;"></asp:Label>
                            </asp:Panel>
                        </asp:Panel>


                        <asp:Panel ID="pnlGiftHistory" runat="server" GroupingText="Gift History" Font-Bold="true" Font-Size="X-Large" Width="1080px" Style="margin-top: 15px; margin-bottom: 60px;">
                            <asp:Panel ID="pnlDownloadButtons" runat="server" Width="1050px" Style="float: left; vertical-align: top;">
                                <asp:Button ID="btnDownloadGiftReminder" runat="server" Text="Download Gift Reminder" Font-Bold="true" Font-Size="Small" Height="30px" Style="float: right; vertical-align: middle;" />
                                <asp:Button ID="btnDownloadReport" runat="server" Text="Download Report" Font-Bold="true" Font-Size="Small" Height="30px" Style="float: right; vertical-align: middle;" />
                            </asp:Panel>

                            <%--                            <asp:Panel ID="pnlGiftHistoryMonthButtons" runat="server" Font-Bold="false" Font-Size="Small" Width="900px"
                                    style="float:left ; margin-top: 40px; margin-bottom: 20px; margin-left: 120px; vertical-align:top; " >
                                <asp:Button ID="btnPrev" runat="server" Text="<< Prev" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" BorderColor="White" Font-Size="Small" style="float: left; " />
                                <asp:Button ID="btnGiftHistory1" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; "/>
                                <asp:Button ID="btnGiftHistory2" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory3" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory4" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory5" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory6" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory7" runat="server" Height="30px" Width="80px" BackColor="#336699" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnGiftHistory8" runat="server" Height="30px" Width="80px" BackColor="#003366" ForeColor="White" BorderWidth="1px" Font-Size="Small" BorderColor="White" style="float: left; " />
                                <asp:Button ID="btnNext" runat="server" Text=">> Next" Height="30px" Width="80px" BackColor="#336699" BorderWidth="1px" ForeColor="White" BorderColor="White" Font-Size="Small" style="float: left; " />
                            </asp:Panel>--%>

                            <asp:Panel ID="pnlEntrySearchBox" runat="server" Style="margin-top: 60px; margin-bottom: 5px;">
                                <asp:Table runat="server" HorizontalAlign="Center" Width="1050px" Height="50px" Font-Bold="false" Font-Size="Small">
                                    <asp:TableRow runat="server" Height="40px">
                                        <asp:TableCell Width="40px" VerticalAlign="Middle">
                                      <asp:Label runat="server" Text="Show" Width="30px" ></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle">
                                            <asp:DropDownList ID="ddlEntries" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="60px">
                                                <asp:ListItem Selected="true">10</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle">
                                       <asp:Label runat="server" Text="entries" ></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server" HorizontalAlign="Right" VerticalAlign="Middle" Width="900px">
                                            <asp:TextBox ID="TextBox1" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="180px" BorderWidth="2px" BorderStyle="Solid" BorderColor="LightBlue" AutoPostBack="true"
                                                Style="float: right;"></asp:TextBox>

                                        </asp:TableCell>

                                    </asp:TableRow>
                                </asp:Table>
                            </asp:Panel>



                            <asp:ListView ID="lvGiftHistory" runat="server" InsertItemPosition="None">
                                <LayoutTemplate>
                                    <table id="tblGiftHistoryLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small;">
                                        <tr style="height: 40px; font-size: small; font-weight: bold;">
                                            <td id="tdChkHeaderGiftHistory" style="width: 30px; background-color: lightgray;">
                                                <asp:CheckBox ID="chkHeaderGiftHistory" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                            </td>
                                            <td id="tdSortByGiftNo" runat="server" style="width: 100px; background-color: lightgray; vertical-align: middle;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByGiftNo" runat="server" Text="Gift No" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Blue" Style="margin: 5px;"
                                                    OnClick="OnLnkGHSortByGiftNo_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByDate" runat="server" style="width: 100px; background-color: lightgray; vertical-align: middle;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByDate" runat="server" Text="Date" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkGHSortByDate_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByAmount" runat="server" style="width: 170px; background-color: lightgray; vertical-align: middle;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByAmount" runat="server" Text="Amount" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkGHSortByAmount_Click"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByCode" runat="server" style="width: 100px; background-color: lightgray; vertical-align: middle;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByCode" runat="server" Text="Code" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkGHSortByCode_Click"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNote" runat="server" style="width: 550px; background-color: lightgray; vertical-align: middle;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByNote" runat="server" Text="Note" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkGHSortByNote_Click"></asp:LinkButton>
                                            </td>

                                        </tr>

                                        <asp:PlaceHolder ID="itemplaceholder" runat="server"></asp:PlaceHolder>


                                        <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                            <td colspan="5">
                                                <asp:Label ID="lblGiftShowing" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                            </td>

                                            <td colspan="2" align="right">
                                                <asp:DataPager ID="itemGiftDataPager" runat="server" PageSize="5" style="margin-right: 10px;">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" />
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>

                                    </table>
                                </LayoutTemplate>

                                <EmptyDataTemplate>

                                    <table id="tblEmptyGiftHistoryLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small;">
                                        <tr style="height: 40px; font-size: small;">
                                            <td style="width: 30px; background-color: gray;">
                                                <asp:CheckBox ID="chkEmptyHeaderGiftHistory" runat="server" Enabled="false" CssClass="LargeChkBoxClass" />
                                            </td>
                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptySortByGiftNo" runat="server" Text="Gift No" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptySortByDate" runat="server" Text="Date" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 170px; background-color: gray;">
                                                <asp:Label ID="lblEmptySortByAmount" runat="server" Text="Amount" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptySortByCode" runat="server" Text="Code" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 550px; background-color: gray;">
                                                <asp:Label ID="lblEmptySortByNote" runat="server" Text="Note" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>

                                        </tr>

                                        <tr>
                                            <td colspan="7" align="center" height="25px" style="font-size: small; font-weight: normal;">No data available in table</td>
                                        </tr>


                                        <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                            <td colspan="5">
                                                <asp:Label ID="lblGiftShowing" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                            </td>

                                            <td colspan="2" align="right">
                                                <asp:DataPager ID="itemGiftDataPager" runat="server" PageSize="5" style="margin-right: 10px;">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" />
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>

                                    </table>


                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 30px; align-content: center; background-color: white;">
                                            <asp:CheckBox ID="chkGiftHistory" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("nGiftNumber") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("strDate") %>' Style="margin: 5px;"> </asp:Label>
                                        </td>
                                        <td style="width: 170px; background-color: white;">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Amount") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Code") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 550px; background-color: white;">
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("strNote") %>' Style="margin: 5px;"></asp:Label>
                                        </td>

                                    </tr>

                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td style="width: 30px; align-content: center; background-color: lightblue;">
                                            <asp:CheckBox ID="chkGiftHistory" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("nGiftNumber") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("strDate") %>' Style="margin: 5px;"> </asp:Label>
                                        </td>
                                        <td style="width: 170px; background-color: lightblue;">
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("Amount") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("Code") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 550px; background-color: lightblue;">
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("strNote") %>' Style="margin: 5px;"></asp:Label>
                                        </td>

                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </asp:Panel>

                        <asp:Button ID="btnGiftHistoryLogout" runat="server" OnClick="btnLogout_Click" Text="Log out" Style="float: right;" />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>

                <%--                <ajaxToolkit:TabPanel ID="tpnlNeedsProcessing" runat="server" HeaderText="Needs Processing">
                    <ContentTemplate>
                        <asp:Panel ID="pnlNPSummary" runat="server" GroupingText="Summary" Width="1050px" Font-Bold="true" Font-Size="X-Large" Style="margin-top: 15px;">
                            <asp:Table ID="tblNPSummary" runat="server" Font-Size="Small" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" ForeColor="Black" GridLines="Both"
                                Width="1050px" HorizontalAlign="Center" CellPadding="5" CellSpacing="5" Style="margin-top: 15px; margin-bottom: 25px;">
                                <asp:TableHeaderRow runat="server" BackColor="LightGray" Font-Size="Small" Height="35px">
                                    <asp:TableHeaderCell Width="80px"></asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="600px" Font-Bold="false" Font-Size="Small" Style="text-align: left;">Member</asp:TableHeaderCell>
                                </asp:TableHeaderRow>

                                <asp:TableRow ID="trNPMemberName" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Name</asp:TableCell>
                                    <asp:TableCell ID="tcNPMemberName" runat="server" Font-Bold="false" Font-Size="Small">Park, Harris J.</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trNPMemberId" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Id</asp:TableCell>
                                    <asp:TableCell ID="tcNPMemberId" runat="server" Font-Bold="false" Font-Size="Small">55505</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="tNPMemberRegDate" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Reg. Date</asp:TableCell>
                                    <asp:TableCell ID="tcNPRegDate" runat="server" Font-Bold="false" Font-Size="Small">06/24/2009</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trNPMemberProgram" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Program</asp:TableCell>
                                    <asp:TableCell ID="tcNPMemberProgram" runat="server" Font-Bold="false" Font-Size="Small">Gold Plus</asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="trNPMemberGiftAmount" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">Gift Amount</asp:TableCell>
                                    <asp:TableCell ID="tcNPGiftAmount" runat="server" Font-Bold="false" Font-Size="Small">$175.00</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trNPTotal" runat="server" BackColor="White" Font-Size="Small">
                                    <asp:TableCell runat="server" Font-Bold="true" Font-Size="Small" Width="80px">NP Total</asp:TableCell>
                                    <asp:TableCell ID="tcNPTotal" runat="server" Font-Bold="false" Font-Size="Small">$175.00</asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <asp:Panel ID="pnlNeedsProcessing" runat="server" GroupingText="Needs Processing" Width="1050px" Font-Bold="true" Font-Size="X-Large" Style="padding: 0px; margin-top: 15px; margin-bottom: 60px;">
                            <asp:Panel ID="pnlShowEntriy" runat="server" Style="margin-top: 20px; margin-bottom: 5px; padding: 0px;">
                                <asp:Table runat="server" HorizontalAlign="Center" Width="1050px" Height="50px" Font-Bold="false" Font-Size="Small" Style="margin: 0px;">
                                    <asp:TableRow runat="server" Height="40px">
                                        <asp:TableCell Width="40px" VerticalAlign="Middle">
                                      <asp:Label runat="server" Text="Show" Width="30px" ></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle">
                                            <asp:DropDownList ID="ddlNPEntries" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="60px">
                                                <asp:ListItem Selected="true">10</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                                <asp:ListItem>100</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell VerticalAlign="Middle" Width="30px">
                                       <asp:Label runat="server" Text="entries" ></asp:Label>
                                        </asp:TableCell>
                                        <asp:TableCell runat="server" HorizontalAlign="Right" VerticalAlign="Middle" Width="1000px">
                                            <asp:TextBox ID="txtNeedProcessingSearch" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="180px" BorderWidth="2px" BorderStyle="Solid" BorderColor="LightBlue" AutoPostBack="true"
                                                Style="float: right;"></asp:TextBox>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:Panel>



                            <asp:ListView ID="lvNeedsProcessing" runat="server" InsertItemPosition="None">
                                <LayoutTemplate>
                                    <table id="tblNeedsProcessingLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small;">
                                        <tr style="height: 40px; font-size: small; font-weight: bold;">
                                            <td id="tdNPHeaderNeedsProcessing" runat="server" style="width: 30px; background-color: lightgray;">
                                                <asp:CheckBox ID="chkHeaderNeedsProcessing" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                            </td>
                                            <td id="tdSortByNPDate" runat="server" style="width: 100px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkNeedsProcessingDate" runat="server" Text="Date" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Blue" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByDate_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNPPatient" runat="server" style="width: 150px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByPatient" runat="server" Text="Patient" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByPatient_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNPHospital" runat="server" style="width: 200px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByHospital" runat="server" Text="Hospital" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByHospital_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNPAmount" runat="server" style="width: 100px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByAmount" runat="server" Text="Amount" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByAmount_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNPBalance" runat="server" style="width: 100px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByBalance" runat="server" Text="Balance" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByBalance_Clicked"></asp:LinkButton>
                                            </td>
                                            <td id="tdSortByNPMemo" runat="server" style="width: 300px; background-color: lightgray;" class="arrow-down">
                                                <asp:LinkButton ID="lnkSortByMemo" runat="server" Text="Memo" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                    OnClick="OnLnkNPSortByMemo_Clicked"></asp:LinkButton>
                                            </td>
                                        </tr>

                                        <asp:PlaceHolder ID="itemplaceholder" runat="server"></asp:PlaceHolder>


                                        <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                            <td colspan="5">
                                                <asp:Label ID="lblNeedsProcessingShowing" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                            </td>

                                            <td colspan="2" align="right">
                                                <asp:DataPager ID="itemNeedsProcessingDataPager" runat="server" PageSize="5" style="margin-right: 10px;">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" />
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>

                                    </table>
                                </LayoutTemplate>


                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 30px; align-content: center; background-color: white;">
                                            <asp:CheckBox ID="chkNeedsProcessing" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="lblNPDate" runat="server" Text='<%# Eval("strDate") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 150px; background-color: white;">
                                            <asp:Label ID="lblNPPatient" runat="server" Text='<%# Eval("strPatient") %>' Style="margin: 5px;"> </asp:Label>
                                        </td>
                                        <td style="width: 200px; background-color: white;">
                                            <asp:Label ID="lblNPHospital" runat="server" Text='<%# Eval("strHospital") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="lblNPAmount" runat="server" Text='<%# Eval("Amount") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: white;">
                                            <asp:Label ID="lblNPBalance" runat="server" Text='<%# Eval("Balance") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 300px; background-color: white;">
                                            <asp:Label ID="lblNPMemo" runat="server" Text='<%# Eval("strMemo") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                    </tr>

                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td style="width: 30px; align-content: center; background-color: lightblue;">
                                            <asp:CheckBox ID="chkAltNeedsProcessing" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="lblAltDate" runat="server" Text='<%# Eval("strDate") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 150px; background-color: lightblue;">
                                            <asp:Label ID="lblAltPatient" runat="server" Text='<%# Eval("strPatient") %>' Style="margin: 5px;"> </asp:Label>
                                        </td>
                                        <td style="width: 200px; background-color: lightblue;">
                                            <asp:Label ID="lblAltHospital" runat="server" Text='<%# Eval("strHospital") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="lblAltAmount" runat="server" Text='<%# Eval("Amount") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: lightblue;">
                                            <asp:Label ID="lblAltBalance" runat="server" Text='<%# Eval("Balance") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td style="width: 300px; background-color: lightblue;">
                                            <asp:Label ID="lblAltMemo" runat="server" Text='<%# Eval("strMemo") %>' Style="margin: 5px;"></asp:Label>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>

                                <EmptyDataTemplate>
                                    <table id="tblNeedsProcessingEmptyLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small;">
                                        <tr style="height: 40px; font-size: small; font-weight: bold;">
                                            <td style="width: 30px; background-color: gray;">
                                                <asp:CheckBox ID="chkEmptyNeedsProcessing" runat="server" CssClass="LargeChkBoxClass" />
                                            </td>
                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptyNPSortByDate" runat="server" Text="Date" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 150px; background-color: gray;">

                                                <asp:Label ID="lblEmptyNPSortByPatient" runat="server" Text="Patient" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>



                                            </td>
                                            <td style="width: 200px; background-color: gray;">
                                                <asp:Label ID="lblEmptyNPSortByHospital" runat="server" Text="Hospital" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>

                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptyNPSortByAmount" runat="server" Text="Amount" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>


                                            </td>
                                            <td style="width: 100px; background-color: gray;">
                                                <asp:Label ID="lblEmptyNPSortByBalance" runat="server" Text="Balance" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                            <td style="width: 300px; background-color: gray;">
                                                <asp:Label ID="lblEmptyNPSortByMemo" runat="server" Text="Memo" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="7" align="center" height="25px" style="font-size: small; font-weight: normal;">No data available in table</td>
                                        </tr>

                                        <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                            <td colspan="5">
                                                <asp:Label ID="lblGiftShowing" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                            </td>

                                            <td colspan="2" align="right">
                                                <asp:DataPager ID="itemNeedsProcessingDataPager" runat="server" PageSize="5" Visible="true" style="margin-right: 10px;">
                                                    <Fields>
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                        <asp:NumericPagerField ButtonType="Link" />
                                                        <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </asp:Panel>
                        <asp:Button ID="btnNeedsProcessingLogout" runat="server" OnClick="btnLogout_Click" Text="Log out" Style="float: right;" />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>--%>
                <ajaxToolkit:TabPanel ID="tpnlFiles" runat="server" HeaderText="Files" Height="500">
                    <ContentTemplate>

                        <asp:Panel ID="pnlFilesEntrySearch" runat="server" Style="margin-top: 20px; margin-bottom: 5px; padding: 0px;">
                            <asp:Table runat="server" HorizontalAlign="Center" Width="1050px" Height="50px" Font-Bold="false" Font-Size="Small" Style="margin: 0px; margin-left: 20px;">
                                <asp:TableRow runat="server" Height="40px">
                                    <asp:TableCell Width="40px" VerticalAlign="Middle">
                                      <asp:Label runat="server" Text="Show" Width="30px" ></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle">
                                        <asp:DropDownList ID="ddlFilesEntries" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="60px">
                                            <asp:ListItem Selected="true">10</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                        </asp:DropDownList>
                                    </asp:TableCell>
                                    <asp:TableCell VerticalAlign="Middle" Width="30px">
                                       <asp:Label runat="server" Text="entries" ></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell runat="server" HorizontalAlign="Right" VerticalAlign="Middle" Width="1000px">
                                        <asp:TextBox ID="txtFilesSearch" runat="server" BackColor="White" ForeColor="Black" Height="30px" Width="180px" BorderWidth="2px" BorderStyle="Solid" BorderColor="LightBlue" AutoPostBack="true"
                                            Style="float: right;"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:Panel>

                        <asp:ListView ID="lvFiles" runat="server" InsertItemPosition="None">
                            <LayoutTemplate>
                                <table id="tblFilesLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small; border-collapse: collapse; margin-left: 20px;">
                                    <tr class="trFilesHeader">
                                        <td id="tdFiles" runat="server" style="width: 30px; background-color: lightgray;">
                                            <asp:CheckBox ID="chkFiles" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td id="tdFilesSortByFile" runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:LinkButton ID="lnkSortByFilesFile" runat="server" Text="File" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                OnClick="OnLnkFilesSortByFile_Clicked"></asp:LinkButton>
                                        </td>
                                        <td id="tdFilesSortByCategory" runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:LinkButton ID="lnkSortByFilesCategory" runat="server" Text="Patient" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                OnClick="OnLnkFilesSortByCategory_Clicked"></asp:LinkButton>
                                        </td>
                                        <td id="tdFilesSortByMember" runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:LinkButton ID="lnkSortByFilesMember" runat="server" Text="Hospital" Font-Size="Small" Font-Bold="true" Font-Underline="false" ForeColor="Black" Style="margin: 5px;"
                                                OnClick="OnLnkFilesSortByMember_Clicked"></asp:LinkButton>
                                        </td>
                                    </tr>

                                    <tr style="height: 40px; border-bottom-style: solid; border-bottom-color: gray; border-bottom-width: 1px;">
                                        <td style="width=30px; background-color: lightgray;"></td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:TextBox ID="txtFilesFile" runat="server" Height="25px" Width="320px" BackColor="White" Font-Bold="false" ForeColor="Black" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                        </td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:DropDownList ID="ddlFilesCategory" runat="server" DataTextField="All" Height="28px" Width="320px" BackColor="White" Font-Bold="false" ForeColor="Black"></asp:DropDownList>
                                        </td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:DropDownList ID="ddlFilesMember" runat="server" DataTextField="All" Height="28px" Width="300px" Bak="White" Font-Bold="false" ForeColor="Black"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <asp:PlaceHolder ID="itemplaceholder" runat="server"></asp:PlaceHolder>


                                    <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                        <td colspan="2">
                                            <asp:Label ID="lblFilesFileEntries" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                        </td>

                                        <td colspan="2" align="right">
                                            <asp:DataPager ID="itemFilesDataPager" runat="server" PageSize="5" style="margin-right: 10px;">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>

                                </table>
                            </LayoutTemplate>


                            <ItemTemplate>
                                <tr style="height: 25px;">

                                    <%--                                        <asp:Label ID="lblNPDate" runat="server" Text='<%# Eval("strDate") %>' style="margin: 5px;"></asp:Label>--%>


                                    <td style="width: 30px; background-color: white;"></td>
                                    <td style="width: 360px; background-color: white;">
                                        <asp:Label ID="lblNPPatient" runat="server" Text='<%# Eval("strFile") %>' Style="margin: 5px;"> </asp:Label>
                                    </td>
                                    <td style="width: 360px; background-color: white;">
                                        <asp:Label ID="lblNPHospital" runat="server" Text='<%# Eval("strCategory") %>' Style="margin: 5px;"></asp:Label>
                                    </td>
                                    <td style="width: 360px; background-color: white;">
                                        <asp:Label ID="lblNPAmount" runat="server" Text='<%# Eval("strMemberName") %>' Style="margin: 5px;"></asp:Label>
                                    </td>

                                </tr>

                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="height: 25px;">

                                    <td style="width: 30px; background-color: lightblue;"></td>
                                    <td style="width: 360px; background-color: lightblue;">
                                        <asp:Label ID="lblNPPatient" runat="server" Text='<%# Eval("strFile") %>' Style="margin: 5px;"> </asp:Label>
                                    </td>
                                    <td style="width: 360px; background-color: lightblue;">
                                        <asp:Label ID="lblNPHospital" runat="server" Text='<%# Eval("strCategory") %>' Style="margin: 5px;"></asp:Label>
                                    </td>
                                    <td style="width: 360px; background-color: lightblue;">
                                        <asp:Label ID="lblNPAmount" runat="server" Text='<%# Eval("strMemberName") %>' Style="margin: 5px;"></asp:Label>
                                    </td>

                                </tr>
                            </AlternatingItemTemplate>

                            <EmptyDataTemplate>
                                <table id="tblEmptyFilesLayout" cellspacing="0" style="width: 1050px; padding: 5px; font-size: small; border-collapse: collapse; margin-left: 20px;">
                                    <tr style="height: 40px; font-size: small; font-weight: bold; border-bottom-style: solid; border-bottom-color: gray; border-bottom-width: 1px;">
                                        <td runat="server" style="width: 30px; background-color: lightgray;">
                                            <asp:CheckBox ID="chkFiles" runat="server" CssClass="LargeChkBoxClass" Style="margin: 5px;" />
                                        </td>
                                        <td runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:Label ID="lblEmptyFilesByFile" runat="server" Text="File" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                        </td>
                                        <td runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:Label ID="lblEmptyFilesByCategory" runat="server" Text="Category" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>
                                        </td>
                                        <td runat="server" style="width: 360px; background-color: lightgray;" class="arrow-down">
                                            <asp:Label ID="lblEmptyFilesByMember" runat="server" Text="Member" Font-Size="Small" Font-Bold="true" Style="margin: 5px;"></asp:Label>

                                        </td>
                                    </tr>

                                    <tr style="height: 40px; border-bottom-style: solid; border-bottom-color: gray; border-bottom-width: 1px;">
                                        <td style="width=30px; background-color: lightgray;"></td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:TextBox ID="txtFilesFile" runat="server" Height="25px" Width="320px" BackColor="White" Font-Bold="false" ForeColor="Black" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                        </td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:DropDownList ID="ddlFilesCategory" runat="server" DataTextField="All" Height="28px" Width="320px" BackColor="White" Font-Bold="false" ForeColor="Black"></asp:DropDownList>
                                        </td>

                                        <td style="width: 360px; background-color: lightgray;">
                                            <asp:DropDownList ID="ddlFilesMember" runat="server" DataTextField="All" Height="28px" Width="300px" Bak="White" Font-Bold="false" ForeColor="Black"></asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="4" align="center" height="25px" style="font-size: small; font-weight: normal; margin: 5px;">No data available in table</td>
                                    </tr>

                                    <tr style="height: 50px; background-color: lightgray; vertical-align: middle;">
                                        <td colspan="2">
                                            <asp:Label ID="lblFilesShowing" runat="server" Text="Showing" Font-Size="Small" Font-Bold="false" Style="margin-left: 10px;"></asp:Label>
                                        </td>

                                        <td colspan="2" align="right">
                                            <asp:DataPager ID="itemFilesDataPager" runat="server" PageSize="5" Visible="true" style="margin-right: 10px;">
                                                <Fields>
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" />
                                                    <asp:NumericPagerField ButtonType="Link" />
                                                    <asp:NextPreviousPagerField ButtonType="Button" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>

                        </asp:ListView>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tpnlPassword" runat="server" HeaderText="Password" Font-Size="Large" Height="500">
                    <ContentTemplate>

                        <asp:Panel ID="pnlChangePassword" runat="server" GroupingText="Change password" Font-Bold="true" Font-Size="Large" HorizontalAlign="Center" Style="margin-top: 100px; margin-left: 420px; margin-right: 420px;">

                            <asp:Label ID="lblNewPassword" runat="server" Text="New password" Width="200px" Height="25px" Font-Size="Medium" Font-Bold="false" Style="margin-top: 10px; text-align: left;"></asp:Label><br />
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Width="200px" Height="25px" Style="margin-left: 10px; margin-right: 10px;"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword" ErrorMessage="New password required!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="Confirm Password" Width="200px" Height="25px" Font-Size="Medium" Font-Bold="false" Style="margin-top: 10px; text-align: left;"></asp:Label><br />
                            <asp:TextBox ID="txtNewPasswordConfirmation" runat="server" TextMode="Password" Width="200px" Height="25px" Style="margin-left: 10px; margin-right: 10px;"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtNewPasswordConfirmation" ErrorMessage="Please type new password again!" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator><br />
                            <asp:Label ID="lblPasswordMismatchError" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <asp:Button ID="btnNewPasswordSubmit" runat="server" Text="Reset Password" OnClientClick="OnPasswordReset(event)" OnClick="btnNewPasswordSubmit_Click" Width="300px" Height="35px"
                                Style="margin-top: 35px; margin-bottom: 20px;" />

                        </asp:Panel>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>


        </div>
    </form>
</body>
</html>
