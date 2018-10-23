<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="SalesForceWebApp.PersonalDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/PersonalDetails.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <script src='<%= Page.ResolveClientUrl("~/Scripts/personal-info.js") %>' type="text/javascript"></script>
        <script src='<%= Page.ResolveClientUrl("~/Scripts/smoking-drug-alcohol.js") %>' type="text/javascript"></script>

        <asp:ScriptManager ID="smPersonalDetails" runat="server"></asp:ScriptManager>

        <div>

            <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
                <p>The Christian Mutual Med-Aid</p>
            </div>

            <asp:Panel ID="pnlPersonalDetails" runat="server" HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" BorderColor="Gray" Width="780px" Height="650px" 
                Font-Bold="true" Font-Size="X-Large" Style="vertical-align: top; margin-top: 20px; margin-left: auto; margin-right: auto; padding-top: 10px; padding-left: 30px; padding-right: 30px; ">

                <div style="text-align: center; font-size: x-large; font-weight: bold; margin-top: 20px; margin-bottom: 35px; ">
                    <p style="font-family: Arial;">Personal Details Page</p>
                </div>

                <asp:Panel ID="pnlEmail" runat="server" HorizontalAlign="Left" Height="80px" Width="760px" CssClass="floatLeft">
                    <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="TextBox" Width="740px" Enabled="false"></asp:TextBox>
                </asp:Panel>
                <br />

                <asp:Panel ID="pnlTitle" runat="server" HorizontalAlign="Left" Height="80px" Width="80px" CssClass="floatLeft">
                    <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="pnlLabel"></asp:Label><br />
                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="DropDownList" >
                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Jr." Value="1"></asp:ListItem>
                        <asp:ListItem Text="Mr." Value="2"></asp:ListItem>
                        <asp:ListItem Text="Mrs." Value="3"></asp:ListItem>
                        <asp:ListItem Text="Ms." Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>

                <asp:Panel ID="pnlLastName" runat="server" HorizontalAlign="Left" Height="80px" Width="250px" CssClass="floatLeft" >
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="TextBox" Width="240px" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>


                <asp:Panel ID="pnlFirstName" runat="server" HorizontalAlign="Left" Height="80px" Width="200px" CssClass="floatLeft" >
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="TextBox" Width="190px" ReadOnly="true" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>


                <asp:Panel ID="pnlMiddleName" runat="server" HorizontalAlign="Left" Height="80px" Width="200px" CssClass="floatLeft" >
                    <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="TextBox" Width="190px" ></asp:TextBox>
                </asp:Panel><br />

                <asp:Panel ID="pnlDateOfBirth" runat="server" HorizontalAlign="Left" Height="80px" Width="220px" CssClass="floatLeft" >
                    <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtDateOfBirth" runat="server" Width="200px" CssClass="TextBox" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="calexDateOfBirth"  runat="server" TargetControlID="txtDateOfBirth" DefaultView="Years" />
                    <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="txtDateOfBirth" ErrorMessage="Date of birth required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                </asp:Panel>


                <asp:Panel ID="pnlGender" runat="server" HorizontalAlign="Left" Height="80px" Width="260px" CssClass="floatLeft" >
                    <asp:Label ID="lblGender" runat="server" Text="Gender" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:Panel ID="pnlSubGender" runat="server" Height="28px" Width="250px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px">
                        <asp:RadioButtonList ID="rbGenderList" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" ValidationGroup="Gender" Font-Size="Small" Font-Names="Arial" Font-Bold="true" >
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:Panel>
                    <asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="rbGenderList" ErrorMessage="Gender required" ValidationGroup="Gender" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
                </asp:Panel>


                <asp:Panel ID="pnlSSN" runat="server" HorizontalAlign="Left" Height="80px" Width="270px" CssClass="floatLeft" >
                    <asp:Label ID="lblSSN" runat="server" Text="SSN" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtSSN" runat="server"  onclick="mouse_left_click_ssn(event, this);" onfocus="mouse_left_click_ssn(event, this);"
                            onfocusout="ssn_lost_focus(event, this)" onkeypress="accept_social_security_number(event, this);" onkeydown="filter_control_character_ssn(event, this);"
                            Width="250px" CssClass="TextBox" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSSN" runat="server" ControlToValidate="txtSSN" ErrorMessage="Social security number required" ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revSSN" runat="server" ControlToValidate="txtSSN" ErrorMessage="Invalid Social security number" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                         Display="Dynamic" ValidationExpression="\d{3}-\d{2}-\d{4}" ></asp:RegularExpressionValidator>
                </asp:Panel><br />

                <asp:Panel ID="pnlTelephone1" runat="server" HorizontalAlign="Left" Height="80px"  Width="245px" CssClass="floatLeft">
                    <asp:Label ID="lblTelephone1" runat="server" Text="Telephone #1" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtTelephone1" runat="server" onclick="mouse_left_click_phone(event, this);" onfocus="mouse_left_click_phone(event, this);"
                         onfocusout="phone_number_lost_focus(event, this)" onkeypress="accept_phone_number(event, this);" onkeydown="filter_control_character_phone_number(event, this);"
                         Width="235px" CssClass="TextBox" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTelephone1" runat="server" ControlToValidate="txtTelephone1" ErrorMessage="Phone number required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTelephone1" runat="server" ControlToValidate="txtTelephone1" ErrorMessage="Invalid phone number" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ></asp:RegularExpressionValidator>

                </asp:Panel>


                <asp:Panel ID="pn1Telephone2" runat="server" HorizontalAlign="Left" Height="80px"  Width="245px" CssClass="floatLeft">
                    <asp:Label ID="lblTelephone2" runat="server" Text="Telephone #2" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtTelephone2" runat="server" onclick="mouse_left_click_phone(event, this);" onfocus="mouse_left_click_phone(event, this);"
                        onfocusout="phone_number_lost_focus(event, this)" onkeypress="accept_phone_number(event, this);" onkeydown="filter_control_character_phone_number(event, this);"
                        Width="235px" CssClass="TextBox" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTelephone2" runat="server" ControlToValidate="txtTelephone2" ErrorMessage="Invalid phone number" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"  ></asp:RegularExpressionValidator>
                </asp:Panel>

                <asp:Panel ID="pnlTelephone3" runat="server" HorizontalAlign="Left" Height="80px"  Width="245px" CssClass="floatLeft">
                    <asp:Label ID="lblTelephone3" runat="server" Text="Telephone #3" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtTelephone3" runat="server" onclick="mouse_left_click_phone(event, this);" onfocus="mouse_left_click_phone(event, this);"
                        onfocusout="phone_number_lost_focus(event, this)" onkeypress="accept_phone_number(event, this);" onkeydown="filter_control_character_phone_number(event, this);"
                        Width="235px" CssClass="TextBox" ></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revTelephone3" runat="server" ControlToValidate="txtTelephone3" ErrorMessage="Invalid phone number" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" ></asp:RegularExpressionValidator>
                </asp:Panel> <br />

                <asp:Panel ID="pnlAddress" runat="server"  HorizontalAlign="Left" Height="80px" Width="760px" CssClass="floatLeft" >
                    <asp:Label ID="lblAddress" runat="server" Text="Address" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="TextBox" Width="740px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"></asp:RequiredFieldValidator>
                </asp:Panel>


                <asp:Panel ID="pnlZipCode" runat="server" HorizontalAlign="Left" Height="80px" Width="200px" CssClass="floatLeft" >
                    <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" CssClass="pnlLabel" ></asp:Label><br />
                    <asp:TextBox ID="txtZipCode" runat="server" Width="180px" CssClass="TextBox" AutoPostBack="true" OnTextChanged="txtZipCode_TextChanged" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Invalid Zip Code" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"
                        ValidationExpression="\d{5}(-\d{4})?" ></asp:RegularExpressionValidator>
                </asp:Panel>

                <asp:Panel ID="pnlState" runat="server" HorizontalAlign="Left" Height="80px" Width="200px"  CssClass="floatLeft" >
                    <asp:Label ID="lblState" runat="server" Text="State" CssClass="pnlLabel" ></asp:Label>  <br />
                    <asp:DropDownList ID="ddlState" runat="server" Width="180px" CssClass="DropDownList" AutoPostBack="true" OnTextChanged="ddlState_TextChanged" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" ErrorMessage="State required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:RequiredFieldValidator>
                </asp:Panel>

                <asp:Panel ID="pnlCity" runat="server" HorizontalAlign="Left" Height="80px" Width="360px" CssClass="floatLeft" >
                    <asp:Label ID="lblCity" runat="server" Text="City" CssClass="pnlLabel" ></asp:Label> <br />
                    <asp:DropDownList ID="ddlCity" runat="server" Width="340px" CssClass="DropDownList" ></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="City required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:RequiredFieldValidator>
                </asp:Panel>
              
            </asp:Panel><br />
            <asp:Panel ID="pnlSmokingDrugAlcohol" runat="server" HorizontalAlign="Center" Height="300px" Width="780px" style="margin-left: auto; margin-right: auto; ">
                <asp:Table ID="tblSmokingDrugAlcohol" runat="server">

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial">I am a current smoker.</asp:TableCell>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial">I am a former smoker.</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell ID="tcCurrentSmoker" runat="server" HorizontalAlign="Left">
                            <asp:Button ID="btnCurrentSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                OnClientClick="toggleYes(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />    
                            <asp:HiddenField ID="hdnCurrentSmokerYes" runat="server" Value="lightgrey" />
                            <asp:Button ID="btnCurrentSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                OnClientClick="toggleNo(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnCurrentSmokerNo" runat="server" Value="blue" />
                        </asp:TableCell>
                        <asp:TableCell ID="tcFormerSmoker" runat="server" HorizontalAlign="Left">
                            <asp:Button ID="btnFormerSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                OnClientClick="toggleFormerYes(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnFormerSmokerYes" runat="server" Value="lightgrey" />
                            <asp:Button ID="btnFormerSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                OnClientClick="toggleFormerNo(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnFormerSmokerNo" runat="server" Value="blue" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial">I am currently using narcotic drugs.</asp:TableCell>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial">I am a formerly used narcotic drugs.</asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server">
                        <asp:TableCell ID="tcNarcotic" runat="server" HorizontalAlign="Left">
                            <asp:Button ID="btnNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                OnClientClick="toggleYes(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnNarcoticYes" runat="server" Value="lightgrey" />
                            <asp:Button ID="btnNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                OnClientClick="toggleNo(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnNarcoticNo" runat="server" Value="blue" />
                        </asp:TableCell>
                        <asp:TableCell ID="tcFormerNarcotic" runat="server" HorizontalAlign="Left">
                            <asp:Button ID="btnFormerNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                OnClientClick="toggleFormerYes(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnFormerNarcoticYes" runat="server" Value="lightgrey" />
                            <asp:Button ID="btnFormerNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                OnClientClick="toggleFormerNo(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnFormerNarcoticNo" runat="server" Value="blue" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial">I abuse alcohol not following the Biblical teaching on the use of it.</asp:TableCell>
                        <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="300" Height="40" Font-Bold="true" Font-Names="Arial"></asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server">
                        <asp:TableCell ID="tcAlcohol" runat="server" ColumnSpan="1" HorizontalAlign="Left">
                            <asp:Button ID="btnAlcoholYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                OnClientClick="toggleYes(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnAlcoholYes" runat="server" Value="lightgrey" />
                            <asp:Button ID="btnAlcoholNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                OnClientClick="toggleNo(this); return false;" style="text-align: center; vertical-align: middle; font-family: Arial; " />
                            <asp:HiddenField ID="hdnAlcoholNo" runat="server" Value="blue" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="mpeUserSmokingDrugAlcohol" runat="server" PopupControlID="pnlUserSmokingDrugAlcohol" TargetControlID="btnUserSmokingDrugAlcoholHidden" BehaviorID="SmokingDrug"
                BackgroundCssClass="modalBackground" ></ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlUserSmokingDrugAlcohol" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                    <div class="header">
                        Alert
                    </div>
                    <div class="body">
                        If you are currently smoking, using narcotic drug or alcohol, you can't be CMM member.
                    </div>
                    <div class="footer" align="center" >
                        <asp:Button ID="btnOk" runat="server" Text="Ok" Width="80px" Style="vertical-align: middle;" OnClick="btnOk_Click" />
                    </div>
                </asp:Panel>
                <asp:Button ID="btnUserSmokingDrugAlcoholHidden" runat="server" Text="Button" Style="display: none;" />

            <asp:Panel ID="pnlPersonalDetailsNext" runat="server" Height="60px" Width="780px" HorizontalAlign="Center" 
                style="border-color: gray; border-style: solid; border-width: 1px;  margin-left: auto; margin-right: auto; padding: 5px;  ">
                <asp:Label ID="lblSaveSuccessFailure" runat="server" Text="The result: " Width="280px" style="float: left; text-align: left; " ></asp:Label>
                <asp:Button ID="btnNext" runat="server" Text="Next" Width="100px" UseSubmitBehavior="false" OnClick="btnNext_Click" 
                    style="vertical-align: bottom; margin-right: 20px; float: right; " />
            </asp:Panel>
   
    </div>
    </form>
</body>
</html>
