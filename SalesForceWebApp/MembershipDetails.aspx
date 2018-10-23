<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MembershipDetails.aspx.cs" Inherits="SalesForceWebApp.MembershipDetails" %>

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
                height: 23px;
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
                margin-top: 3px;
                margin-left: 3px;
                margin-right: 3px;
                margin-bottom: 3px;
                text-align: left;
                /*border: solid 1px gray;*/
            }

    </style>
 
    <script src='<%= Page.ResolveClientUrl("~/Scripts/personal-info.js") %>' type="text/javascript"></script>

    <script type="text/javascript">
        function DisableClientValidation()
        {
            var valMedicareEligibility = document.getElementById("<%=rfvMedicareEligibility.ClientID%>");
            var valMedicareAandB = document.getElementById("<%=rfvMedicareAandB.ClientID%>");
            var valParticipantProgram = document.getElementById("<%=rfvParticipantProgram.ClientID%>");
            var valChurchName = document.getElementById("<%=rfvChurchName.ClientID%>");
            var valSeniorPaster = document.getElementById("<%=rfvSeniorPastor.ClientID%>");

            ValidatorEnable(valMedicareEligibility, false);
            ValidatorEnable(valMedicareAandB, false);
            ValidatorEnable(valParticipantProgram, false);
            ValidatorEnable(valChurchName, false);
            ValidatorEnable(valSeniorPaster, false);
        }


        function EnableClientValidation()
        {

            var valMedicareEligibility = document.getElementById("<%=rfvMedicareEligibility.ClientID%>");
            var valMedicareAandB = document.getElementById("<%=rfvMedicareAandB.ClientID%>");
            var valParticipantProgram = document.getElementById("<%=rfvParticipantProgram.ClientID%>");
            var valChurchName = document.getElementById("<%=rfvChurchName.ClientID%>");
            var valSeniorPaster = document.getElementById("<%=rfvSeniorPastor.ClientID%>");

            ValidatorEnable(valMedicareEligibility, true);
            ValidatorEnable(valMedicareAandB, true);
            ValidatorEnable(valParticipantProgram, true);
            ValidatorEnable(valChurchName, true);
            ValidatorEnable(valSeniorPaster, true);

        }

    </script>
    
    <form id="form1" runat="server">
    <div>
         <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
            <p>The Christian Mutual Med-Aid</p>
         </div>
        <asp:Panel ID="pnlMemebershipDetails" runat="server" Width="800px" Height="580px" HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid" BorderColor="Gray" 
                Font-Bold="true" Font-Size="X-Large" Style="vertical-align: top; margin-top: 20px; margin-left: auto; margin-right: auto; padding-top: 10px; padding-left: 30px; padding-right: 30px; ">
            <div style="text-align: center; font-size: x-large; font-weight: bold; margin-top: 20px; margin-bottom: 35px; ">
                <p style="font-family: Arial;">Membership Details Page</p>
            </div>
            <asp:Panel ID="pnlQualifyForMedicare" runat="server" Height="70px" Width="390px" CssClass="floatLeft" >
                <asp:Label ID="lblQualifyForMedicare" runat="server" Text="Qualify for Medicare" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" ></asp:Label><br />
                <asp:Panel ID="pnlQualifyForMedicareRadidButton" runat="server" Height="35px" Width="370px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                    style="margin-top: 5px; text-align: left; padding-top: 5px; padding-left: 10px; " >
                    <asp:RadioButtonList ID="rbListMedicareYesNo" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" ValidationGroup="MedicareEligibility" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvMedicareEligibility" runat="server" ControlToValidate="rbListMedicareYesNo" ErrorMessage="Medicare eligibility required" ValidationGroup="MedicareEligibility"
                     ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlMedicareAnB" runat="server" Height="70px" Width="390px" CssClass="floatLeft">
                <asp:Label ID="lblMedicareAnB" runat="server" Text="Medicare A and B" Font-Bold="true"  Font-Size="Medium" Font-Names="Arial" ></asp:Label><br />
                <asp:Panel ID="pnlMedicareAnBRadioButton" runat="server" Height="35px" Width="370px" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                    style="margin-top: 5px; text-align: left; padding-top: 5px; padding-left: 10px;">
                    <asp:RadioButtonList ID="rbListMedicareAandB" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" ValidationGroup="MedicareAandB" Font-Bold="true" Font-Size="Small" Font-Names="Arial" >
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem>No</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:Panel>
                <asp:RequiredFieldValidator ID="rfvMedicareAandB" runat="server" ControlToValidate="rbListMedicareAandB" ErrorMessage="Indicate whether you have Medicare A and B" ValidationGroup="MedicareAandB"
                    ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlParticipantProgram" runat="server" Height="70px" Width="240px" CssClass="floatLeft" style="margin-top: 10px;">
                <asp:Label ID="lblParticipantProgram" runat="server" Text="Participant's Program" Font-Bold="true" Font-Names="Arial" Font-Size="Medium" ForeColor="#ff3300"></asp:Label><br />
                <asp:DropDownList ID="ddlParticipantProgram" runat="server" Height="35px" Width="220px">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Gold Medi-I($135)</asp:ListItem>
                    <asp:ListItem>Gold Medi-II($175)</asp:ListItem>
                    <asp:ListItem>Gold Plus($175)</asp:ListItem>
                    <asp:ListItem>Gold($135)</asp:ListItem>
                    <asp:ListItem>Silver($80)</asp:ListItem>
                    <asp:ListItem>Bronze($40)</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvParticipantProgram" runat="server" ControlToValidate="ddlParticipantProgram" ErrorMessage="A program required" InitialValue="--Select--"></asp:RequiredFieldValidator>

            </asp:Panel>

            <asp:Panel ID="pnlSpouseProgram" runat="server" Height="70px" Width="240px" CssClass="floatLeft"  style="margin-top: 10px;">
                <asp:Label ID="lblSpouseProgram" runat="server" Text="Spouse's Program" Font-Bold="true" Font-Names="Arial" Font-Size="Medium" ForeColor="#ff3300"></asp:Label><br />
                <asp:DropDownList ID="ddlSpouseProgram" runat="server" Height="35px" Width="220px" >
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Gold Medi-I($135)</asp:ListItem>
                    <asp:ListItem>Gold Medi-II($175)</asp:ListItem>
                    <asp:ListItem>Gold Plus($175)</asp:ListItem>
                    <asp:ListItem>Gold($135)</asp:ListItem>
                    <asp:ListItem>Silver($80)</asp:ListItem>
                    <asp:ListItem>Bronze($40)</asp:ListItem>
                </asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="pnlChildrenProgram" runat="server" Height="70px" Width="240px" CssClass="floatLeft"  style="margin-top: 10px; ">
                <asp:Label ID="lblChildrenProgram" runat="server" Text="Children's Program" Font-Bold="true" Font-Names="Arial" Font-Size="Medium" ForeColor="#ff3300"></asp:Label><br />
                <asp:DropDownList ID="ddlChildrenProgram" runat="server" Height="35px" Width="220px">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Gold Plus($175)</asp:ListItem>
                    <asp:ListItem>Gold($135)</asp:ListItem>
                    <asp:ListItem>Silver($80)</asp:ListItem>
                    <asp:ListItem>Bronze($40)</asp:ListItem>
                </asp:DropDownList>

            </asp:Panel>

            <asp:Panel ID="pnlChurchName" runat="server" Height="70px" Width="300px" CssClass="floatLeft">
                <asp:Label ID="lblChurchName" runat="server" Text="Church Name" Font-Bold="true" Font-Names="Arial" Font-Size="Medium" ></asp:Label><br />
                <asp:TextBox ID="txtChurchName" runat="server" CssClass="TextBox" Width="280px" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvChurchName" runat="server" ControlToValidate="txtChurchName" ErrorMessage="Church name required"
                    ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlSeniorPastor" runat="server" Height="70px" Width="300px" CssClass="floatLeft">
                <asp:Label ID="lblSeniorPastor" runat="server" Text="Senior Pastor" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:TextBox ID="txtSeniorPastor" runat="server" CssClass="TextBox" Width="280px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSeniorPastor" runat="server" ControlToValidate="txtSeniorPastor" ErrorMessage="Senior pastor name required"
                    ForeColor="Red" Font-Bold="false" Font-Size="Small" Display="Dynamic" ></asp:RequiredFieldValidator>
            </asp:Panel>

            <asp:Panel ID="pnlChurchStreetAddress" runat="server" Height="70px" Width="780px" CssClass="floatLeft" >
                <asp:Label ID="lblChurchStreetAddress" runat="server" Text="Church Address" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:TextBox ID="txtChurchStreetAddress" runat="server" CssClass="TextBox" Width="380px"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnlChurchZipCode" runat="server" Height="70px" Width="160px" CssClass="floatLeft">
                <asp:Label ID="lblChurchZipCode" runat="server" Text="Zip Code" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:TextBox ID="txtChurchZipCode" runat="server" CssClass="TextBox" Width="140px" AutoPostBack="true" OnTextChanged="txtChurchZipCode_TextChanged"></asp:TextBox>

            </asp:Panel>

            <asp:Panel ID="pnlChurchState" runat="server" Height="70px" Width="160px" CssClass="floatLeft" >
                <asp:Label ID="lblChurchState" runat="server" Text="State" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:DropDownList ID="ddlChurchState" runat="server" Width="160px" AutoPostBack="true" CssClass="DropDownList" OnTextChanged="ddlChurchState_TextChanged"></asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="pnlChurchCity" runat="server" Height="70px" Width="240px" CssClass="floatLeft">
                <asp:Label ID="lblChurchCity" runat="server" Text="City" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:DropDownList ID="ddlChurchCity" runat="server" Width="220px" CssClass="DropDownList" ></asp:DropDownList>
            </asp:Panel>

            <asp:Panel ID="pnlChurchPhone" runat="server" Height="70px" Width="200px" CssClass="floatLeft">
                <asp:Label ID="lblChurchPhone" runat="server" Text="Church Phone" Font-Bold="true" Font-Names="Arial" Font-Size="Medium"></asp:Label><br />
                <asp:TextBox ID="txtChurchPhone" runat="server" onclick="mouse_left_click_phone(event, this);" onfocus="mouse_left_click_phone(event, this);"
                    onfocusout="phone_number_lost_focus(event, this)" onkeypress="accept_phone_number(event, this);" onkeydown="filter_control_character_phone_number(event, this);"
                    Width="180px" CssClass="TextBox"></asp:TextBox>
            </asp:Panel>

            <br />
            <asp:Panel ID="pnlReferredBy" runat="server" Height="70px" Width="280px" CssClass="floatLeft">
                <asp:Label ID="lblReferredBy" runat="server" Text="ReferredBy" CssClass="pnlLabel"></asp:Label><br />
                <asp:DropDownList ID="ddlReferredBy" runat="server" Width="260px" CssClass="DropDownList" AutoPostBack="true" OnSelectedIndexChanged="ddlReferredBy_SelectedIndexChanged" ></asp:DropDownList><br />
                <asp:RequiredFieldValidator ID="rfvReferredBy" runat="server" ControlToValidate="ddlReferredBy" ErrorMessage="Referred by required" ForeColor="Red" Font-Bold="true"
                    Font-Size="Small" Display="Dynamic"></asp:RequiredFieldValidator>
            </asp:Panel>
            
            <asp:Panel ID="pnlMembershipNumber" runat="server" Height="70px" Width="240px" CssClass="floatLeft">
                <asp:Label ID="lblMembershipNumber" runat="server" Text="Membership" CssClass="pnlLabel"></asp:Label><br />
                <asp:TextBox ID="txtMembershipNumber" runat="server" CssClass="TextBox" Width="230px" Enabled="false" AutoPostBack="true" OnTextChanged="txtMembershipNumber_TextChanged"></asp:TextBox>
            </asp:Panel>

            <asp:Panel ID="pnlReferrerName" runat="server" Height="70px" Width="250px" CssClass="floatLeft">
                <asp:Label ID="lblReferrerName" runat="server" Text="Referrer's Name" CssClass="pnlLabel"></asp:Label><br />
                <asp:DropDownList ID="ddlReferrerName" runat="server" Width="240px" Enabled="false" CssClass="DropDownList"></asp:DropDownList>
            </asp:Panel>

        </asp:Panel>

        <asp:Panel ID="pnlPrevNextButton" runat="server" Width="800px" Height="40px" style="margin-top: 15px; margin-left : auto; margin-right: auto; " >
            <asp:Button ID="btnPrevious" runat="server" Text="Previous" Height="30px" Width="120px" UseSubmitBehavior="false" OnClientClick="DisableClientValidation();" OnClick="btnPrevious_Click" style="float: left;" />
            <asp:Button ID="btnNext" runat="server" Text="Next" Height="30px" Width="120px" UseSubmitBehavior="false" OnClientClick="EnableClientValidation();" OnClick="btnNext_Click" style="float: right; " />

        </asp:Panel>

    
    </div>
    </form>
</body>
</html>
