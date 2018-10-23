<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HealthHistory.aspx.cs" Inherits="SalesForceWebApp.HealthHistory" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <style type="text/css">
        
        .AddNewTreatmentReportButton
        {
            margin: 5px;
        }

        .txtScrollBar
        {
            overflow: auto;
        }

        .gvTreatmentHistory
        {
            margin-bottom: 15px;
        }

        .tcTitlePatientName
        {
            height: 50px;
            width: 400px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;
        }

        .tcTitleHouseholdRole
        {
            height: 50px;
            width: 400px;
            border-width: 1px;
            border-style: solid;
            border-color: gray;
            font-size: small;
        }

        .tcTitleTreatmentDate
        {
            height: 50px;
            width: 400px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;
        }

        .tcTitleTreatmentDetails
        {
            height: 90px;
            width: 400px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }

        .tcTitlePhysicianInfo
        {
            height: 90px;
            width: 400px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }



        .tcContentPatientName
        {
            height: 50px;
            width: 600px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }

        .tcContentHouseholdRole
        {
            height: 50px;
            width: 600px;
            border-width: 1px;
            border-style: solid;
            border-color: gray;
            font-size: small;
        }

        .tcContentTreatmentDate
        {
            height: 50px;
            width: 600px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }

        .tcContentTreatmentDetails
        {
            height: 80px;
            width: 600px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }

        .tcContentPhysicianInfo
        {
            height: 80px;
            width: 600px; 
            border-width: 1px; 
            border-style: solid; 
            border-color: gray;
            font-size: small;

        }

        .trSeparator
        {
            height: 5px;
        }

        .tableTreatmentHistory
        {
            border-collapse: collapse;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 10px;
            width: 1000px;
        }

    </style>

    <script type="text/javascript">

        function toggleYes(button)
        {
            var btnYes = document.getElementById(button.id);

            var siblings = btnYes.parentElement.childNodes;

            var hdnYes = siblings[1];
            var btnNo = siblings[2];
            var hdnNo = siblings[3];

            if (btnYes.style.backgroundColor == 'lightgrey')
            {
                btnYes.style.backgroundColor = 'red';
                hdnYes.value = 'red';
                btnYes.style.color = 'white';
                btnNo.style.backgroundColor = 'lightgrey';
                hdnNo.value = 'lightgrey';
                btnNo.style.color = 'black';
            }
            else if (btnYes.style.backgroundColor == 'red')
            {
                btnYes.style.backgroundColor = 'lightgrey';
                hdnYes.value = 'lightgrey';
                btnYes.style.color = 'black';
                btnNo.style.backgroundColor = 'blue';
                hdnNo.value = 'blue';
                btnNo.style.color = 'white';
            }

            console.log('hdnYes value: ' + hdnYes.value);
            console.log('hdnNo value: ' + hdnNo.value);
        }

        function toggleNo(button)
        {
            var btnNo = document.getElementById(button.id);

            var siblings = btnNo.parentElement.childNodes;

            var btnYes = siblings[0];
            var hdnYes = siblings[1];
            var hdnNo = siblings[3];

            if (btnNo.style.backgroundColor == 'blue') {
                btnNo.style.backgroundColor = 'lightgrey';
                hdnNo.value = 'lightgrey';
                btnNo.style.color = 'black';
                btnYes.style.backgroundColor = 'red';
                hdnYes.value = 'red';
                btnYes.style.color = 'white';
            }
            else if (btnNo.style.backgroundColor = 'lightgrey') {
                btnNo.style.backgroundColor = 'blue';
                hdnNo.value = 'blue';
                btnNo.style.color = 'white';
                btnYes.style.backgroundColor = 'lightgrey';
                hdnYes.value = 'lightgrey';
                btnYes.style.color = 'black';
            }
        }

    </script>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="smHealthHistory" runat="server"></asp:ScriptManager>
        <div style="text-align: center; font-family: Arial; font-size: larger; font-weight: bold;">Health History: 건강 확인서</div><br /><br />
        <div>
<%--            <asp:Panel ID="pnlHealthHistory" runat="server" HorizontalAlign="Center">

            </asp:Panel>--%>
            <asp:Panel ID="pnlHealthHistory" runat="server"></asp:Panel>
<%--            <asp:UpdatePanel ID="upnlHealthHistory" runat="server">
                <ContentTemplate>

                </ContentTemplate>
            </asp:UpdatePanel>--%>
            <asp:Panel ID="pnlTreatmentInfoInstruction" runat="server">

            </asp:Panel>
            <asp:UpdatePanel ID="upnlTreatmentHistory" runat="server"  UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:ListView ID="lvTreatmentHistory" runat="server" OnItemDataBound="lvTreatmentHistory_ItemDataBound" OnItemDeleting="lvTreatmentHistory_ItemDeleting" >
                        <LayoutTemplate>
                            <table id="tblTreatmentHistory" runat="server" cellpadding="5" class="tableTreatmentHistory" >
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                        </LayoutTemplate>

                        <ItemTemplate>
                            <tr runat="server">
                                <td runat="server" class="tcTitlePatientName" >
                                    <asp:Label ID="lblPatientName" runat="server" Text="Name | 이름" Width="300" ></asp:Label>
                                </td>

                                <td runat="server" class="tcContentPatientName" >
                                    <asp:HiddenField ID="hdnAccountCreationStepCode" runat="server" />
                                    <asp:HiddenField ID="hdnMedicalTreatmentId" runat="server" />
                                    <asp:HiddenField ID="hdnAccountId" runat="server" />
                                    <asp:HiddenField ID="hdnContactId" runat="server" />
                                    <asp:DropDownList ID="ddlPatientName" runat="server" Height="25" Width="200" OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:Button ID="btnRemove" runat="server" Text="X" Width="20" Height="20" Font-Size="Small" CommandName="Delete"
                                        style="text-align: center; vertical-align: middle; position: relative; float: right; "  />
                                </td>
                            </tr>

                            <tr runat="server">
                                <td runat="server" class="tcTitleHouseholdRole" >
                                    <asp:Label ID="lblHouseholdRole" runat="server" Text="Member Type" Width="300" ></asp:Label>
                                </td>

                                <td runat="server" class="tcContentHouseholdRole">
                                    <asp:DropDownList ID="ddlHouseholdRole" runat="server" Height="25" Width="200" >
                                        <asp:ListItem>Head of Household</asp:ListItem>
                                        <asp:ListItem>Spouse</asp:ListItem>
                                        <asp:ListItem>Child</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr runat="server">
                                <td runat="server" class="tcTitleTreatmentDate" >
                                    <asp:Label ID="lblTreatmentDate" runat="server" Text="Treatment Date | 치료일자" Width="300" ></asp:Label>
                                </td>

                                <td runat="server" class="tcContentTreatmentDate" >
                                    <asp:TextBox ID="txtTreatmentDate" runat="server" Height="25" Width="200" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calTreatmentDate" runat="server" TargetControlID="txtTreatmentDate" DefaultView="Years" />
                                </td>
                            </tr>

                            <tr runat="server">
                                <td runat="server" class="tcTitleTreatmentDetails" >
                                    <asp:Label ID="lblTreatmentDetailsEN" runat="server" Text="Diagnosis | Duration | Results | Tests Performed | Medication | Outcome" Width="380"></asp:Label><br />
                                    <asp:Label ID="lblTreatmentDetailsKO" runat="server" Text=" 병명 | 기간 | 결과 | 검사 | 투약 | 경과" Width="380"></asp:Label>
                                </td>
                                <td runat="server" class="tcContentTreatmentDetails" >
                                    <asp:TextBox ID="txtTreatmentDetails" runat="server" TextMode="MultiLine" CssClass="txtScrollBar" Height="35" Width="350" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr runat="server">
                                <td runat="server" class="tcTitlePhysicianInfo" >
                                    <asp:Label ID="lblPhysicianInfoEN1" runat="server" Text="Attending Physician's Name," Width="380" ></asp:Label><br />
                                    <asp:Label ID="lblPhysicianInfoEN2" runat="server" Text="Address and Phone Number" Width="380" ></asp:Label><br />
                                    <asp:Label ID="lblPhysicianInfoKO" runat="server" Text="의사이름, 주소 및 전화번호" Width="380" ></asp:Label>
                                </td>
                                <td runat="server" class="tcContentPhysicianInfo">
                                    <asp:TextBox ID="txtPhysicianInfo" runat="server" TextMode="MultiLine" CssClass="txtScrollBar" Height="35" Width="300" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr runat="server" class="trSeparator" />

                        </ItemTemplate>
                    </asp:ListView>

                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Panel ID="pnlAgreementText" runat="server" HorizontalAlign="Center">
                <asp:Table ID="tblAgreementText" runat="server" HorizontalAlign="Center" Width="1000"
                    BorderColor="Gray" BorderStyle="Solid" BorderWidth="1" CellPadding="5" CellSpacing="5">
                    <asp:TableRow>
                        <asp:TableCell Font-Size="Small">
                            By signing below, I attest that the participating members of my family are Christians who live by Biblical principles. 
                            All applying members of my family attend church regularly; all abstain from tabacco; all abstain from the use of illegal drugs 
                            and the improper or unauthorized use of prescription medications or over-the-counter medications; all follow the Biblical teaching 
                            on the use of alcohol; and all commit to follow the commands of Jesus Christ described in the Bible. I declare that the information 
                            on this application is complete and true the best of my knowledge. (이 신청서에 서명함으로써 CMM에 가입하는 본인과 본인의 가족들은 모두 
                            성경적인 가치관에 의해 살고 있음을 확인합니다. CMM에 가입하는 본인과 가족들은 교회에 정기적으로 출석하고 있으며, 금주, 금연하며 불법 약물, 의약품 및 
                            비 의약품의 오/남용 하지 않으며, 음주에 관해 성경의 가르침을 따르며, 성경에 기록된 대로 예수 그리스도의 명령/가르침을 따르고 있음을 확인합니다. 
                            본인은 이 신청서에 기록된 내용이 본인이 알고있는 한 정확하며 완전하다는 점을 재확인합니다.)
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Font-Size="Small">
                            Christian Mutual Med-Aid(CMM) acts only as facilitator to bring together members with resources and desires to help other members 
                            with medical costs. I understand and agree that as a CMM member, any controversy or disagreement with CMM will be resolved 
                            through Christian alternate dispute resolution (Christian mediation, Christian arbitration) as detailed in the Guidelines and I waive 
                            any right to file a lawsuit or claim against Logos Missions, Inc. or its officers, directors or employees, I understand my false statement 
                            on this form (front/back) may be cause for immediate termination from CMM. I understand I will receive Gift reminders by the 10th of each month. 
                            I promised to send my gifts for these persons and will pray for them. CMM will receive my monthly gift by the 1st of each month. 
                            (기독의료상조회는 회원과 의료비 나눔의 물질을 연결하는 조력자의 역할을 수행하며, 기독의료상조회의 회원으로서 모든 이의 제기는 가이드라인에 명시되어 있는 대로 
                            기독교인 중재 그룹의 권면에 따라 해결될 것을 이해하며, 로고스 선교회나 임직원에게 소송을 제기할 권리를 포기합니다. 이 신청서에 부정확한 내용이 기입된 경우 
                            회원 자격이 박탈될 수 있음을 이해합니다. 매월 10일 경에 회비 안내서를 받게 될 것이며, 회원의 의료비를 돕기 위해 매월 1일까지 회비를 납부하고 기도로 도울 것을 약속합니다.)
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>

<%--            <asp:Panel ID="pnlSignature" runat="server" HorizontalAlign="Center">
                <asp:Table ID="tblConfirmation" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell Width="40" Height="20" HorizontalAlign="Left">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="*" ForeColor="Red" />
                        </asp:TableCell>
                        <asp:TableCell Width="900" Height="20" HorizontalAlign="Left">
                            <asp:Label ID="lblAgreementText" runat="server" Text="I would like to schedule my required membership confirmation call with CMM on "
                                 Font-Size="Medium" style="text-align: left;"></asp:Label>
                            <asp:TextBox ID="txtComfirmationDay" runat="server" Width="30" Text="DD" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:Label ID="lblSlash" runat="server" Width="5" Text="/" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="txtComfirmationMonth" runat="server" Width="30" Text="MM" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:Label ID="lblAt" runat="server" Text=" at " Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="txtComfirmationTime" runat="server" Width="45" Text="00:00" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:DropDownList ID="ddlAMPM" runat="server" Width="60" Height="20">
                                <asp:ListItem>A.M.</asp:ListItem>
                                <asp:ListItem>P.M.</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="20" Height="20"></asp:TableCell>
                        <asp:TableCell Width="900" Height="20" HorizontalAlign="Left">
                            <asp:Label ID="lblConfirmation" runat="server" Font-Size="Medium" Text="I understand my application cannot be approved without the required confirmation call."
                                style="text-align: left;"></asp:Label><br />
                            <asp:Label ID="lblCMMPhone" runat="server" Text="CMM can be reached at 773-777-8889 Ext. 5001 during 9:00 A.M. to 5:30 P.M.(Central Time)."
                                style="text-align: left;" Font-Size="Medium" ></asp:Label><br />
                            <asp:Label ID="lblConfirmationTextKorean" runat="server" Font-Size="Medium" style="text-align: left;"
                                Text="CMM과 가입 확인을 위해 통화 가능한 시간을 적어 주세요. 773-777-8889(Ext. 5001)로 확인전화 주셔야 가입이 완료됩니다. CMM 업무시간은 오전 9:00부터 오후 5:30(중부시간)입니다.)">
                            </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>--%>


                <%-- Add every member's signature here including children not just primary and spouse --%>
<%--                <asp:Table ID="tblSignature" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblPrimarySignature" runat="server" Text="Signature of Primary"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtPrimarySignature" runat="server" Width="350" ></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblPrimarySignatureDate" runat="server" Text="Date" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtPrimarySignatureDate" runat="server" Width="150" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calPrimarySignatureDate" runat="server" TargetControlID="txtPrimarySignatureDate" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:Label ID="lblSpouseSignature" runat="server" Text="Signature of Spouse"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSpouseSignature" runat="server" Width="350" ></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Label ID="lblSpouseSignatureDate" runat="server" Text="Date" ></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtSpouseSignatureDate" runat="server" Width="150"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calSpouseSignatureDate" runat="server" TargetControlID="txtSpouseSignatureDate" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

            </asp:Panel>--%>
            <asp:Panel ID="pnlButtons" runat="server" Width="1000" style="margin-left: auto; margin-right: auto; margin-top: 20px; margin-bottom: 50px; ">
                <asp:Button ID="btnPrev" runat="server" Text="Previous" Width="100" Height="25" style="float: left;" OnClick="btnPrev_Click" />
<%--                <asp:Button ID="btnSubmitApplication" runat="server" Text="Submit Application" Width="150" Height="25" style="float: right;" />--%>
                <asp:Button ID="btnNext" runat="server" Text="Next" Width="100" Height="25" style="float: right;" OnClick="btnNext_Click" />
            </asp:Panel>
        </div>

    </div>
    </form>
</body>
</html>
