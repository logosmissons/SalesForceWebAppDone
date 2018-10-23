<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agreement.aspx.cs" Inherits="SalesForceWebApp.Agreement" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <style type="text/css">
                .LargeChkBoxClass input {
                    width: 25px;
                    height: 25px;
                }

                .MediumChkBoxClass input {
                    width: 20px;
                    height: 20px;
                    position: relative;
                    vertical-align: middle;
                }

                .MediumChkBoxClass
                {
                    display: inline-block;
                    position: relative;
                    vertical-align: text-bottom;
                }

                .tcLargeChkBoxClass {
                    border-width: 1px;
                    border-style: solid;
                    border-color: gray;
                    font-size: small;
                }

                .tcAgreementHeaderText {
                    border-width: 1px;
                    border-style: solid;
                    border-color: gray;
                    font-size: small;
                    background-color: lightskyblue;
                }

                .tcAgreementContentText {
                    border-width: 1px;
                    border-style: solid;
                    border-color: gray;
                    font-family: Arial;
                    font-size: small;
                    background-color: antiquewhite;
                }

                .lblAgreementSentenceEN {
                    display: block;
                    font-family: Arial;
                    font-size: small;
                    margin-bottom: 10px;
                }

                .lblAgreementSentenceKO {
                    display: block;
                    font-family: Arial;
                    font-size: small;
                }



                .tcLargeChkBoxConfirmationClass {
                    border-width: 1px;
                    border-style: solid;
                    border-color: gray;
                    font-size: small;
                    background-color: peachpuff;
                    height: 100px;
                }

                .tcAgreementConfirmationText {
                    border-width: 1px;
                    border-style: solid;
                    border-color: gray;
                    font-family: Arial;
                    font-size: small;
                    background-color: peachpuff;
                    height: 100px;
                }

                .lblAgreementConfirmationTextEN {
                    display: block;
                    font-family: Arial;
                    font-size: small;
                    margin-bottom: 10px;
                }

                .lblAgreementConfirmationTextKO {
                    display: block;
                    font-family: Arial;
                    font-size: small;
                }

            </style>


            <asp:Panel ID="pnlAgreement" runat="server" Style="margin-left: auto; margin-top: auto;">
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
                            <asp:CheckBox ID="chkAgreementItem1" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem2" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem3" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem4" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem5" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem6" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem7" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem8" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem9" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem10" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem11" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem12" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem13" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem14" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem15" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem16" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem17" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem18" runat="server" CssClass="LargeChkBoxClass" />
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
                            <asp:CheckBox ID="chkAgreementItem19" runat="server" CssClass="LargeChkBoxClass" />
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

                <asp:Table ID="tblConfirmation" runat="server"  HorizontalAlign="Center" CellPadding="10" CellSpacing="0" style="margin-top: 20px;"
                     Width="900" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" >

                    <asp:TableRow>
                        <asp:TableCell ID="tcConfirmation" runat="server" Width="100" CssClass="tcLargeChkBoxConfirmationClass" HorizontalAlign="Center" >
                            <asp:CheckBox ID="chkConfirmation" runat="server" CssClass="LargeChkBoxClass" />
                        </asp:TableCell>

                        <asp:TableCell ID="tcConfirmationText" runat="server" Width="800" CssClass="tcAgreementConfirmationText" >
                            <asp:Label ID="lblConfirmationTextEN" runat="server" CssClass="lblAgreementConfirmationTextEN">
                                I/We have read and fully understand the CMM membership agreement as stated, I/My family would like to become active members of CMM.
                            </asp:Label>
                            <asp:Label ID="lblConfirmationTextKO" runat="server" CssClass="lblAgreementConfirmationTextKO">
                                본인(가족)은 위에 명시된 기독의료상조회의 회원 동의서를 숙지했으며 아래에 서명함으로써 기독의료상조회의 회원으로 가입하기 원합니다.
                            </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>

             <asp:Panel ID="pnlSignature" runat="server" HorizontalAlign="Center" style="margin-top: 25px;">
                <asp:Table ID="tblMembershipConfirmation" runat="server" HorizontalAlign="Center" Width="900">
                    <asp:TableRow>
                        <asp:TableCell Width="40" Height="20" HorizontalAlign="Left">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="*" Font-Size="Medium" ForeColor="Red" CssClass="MediumChkBoxClass" />
                        </asp:TableCell>
                        <asp:TableCell Width="860" Height="20" HorizontalAlign="Left">
                            <asp:Label ID="lblAgreementText" runat="server" Text="I would like to schedule my required membership confirmation call with CMM on "
                                 Font-Size="Medium" style="text-align: left;"></asp:Label>
                            <asp:TextBox ID="txtComfirmationDay" runat="server" Width="30" Text="DD" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:Label ID="lblSlash" runat="server" Width="5" Text="/" Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="txtComfirmationMonth" runat="server" Width="30" Text="MM" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:Label ID="lblAt" runat="server" Text=" at " Font-Size="Small"></asp:Label>
                            <asp:TextBox ID="txtComfirmationTime" runat="server" Width="45" Text="00:00" ForeColor="LightBlue" Font-Size="Medium"></asp:TextBox>
                            <asp:DropDownList ID="ddlAMPM" runat="server" Width="60" Height="20" style="margin-left: 5px;">
                                <asp:ListItem>A.M.</asp:ListItem>
                                <asp:ListItem>P.M.</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="20" Height="20"></asp:TableCell>
                        <asp:TableCell Width="880" Height="20" HorizontalAlign="Left">
                            <asp:Label ID="lblConfirmation" runat="server" Font-Size="Medium" Text="I understand my application cannot be approved without the required confirmation call."
                                style="text-align: left;"></asp:Label><br />
                            <asp:Label ID="lblCMMPhone" runat="server" Text="CMM can be reached at 773-777-8889 Ext. 5001 during 9:00 A.M. to 5:30 P.M.(Central Time)."
                                style="text-align: left;" Font-Size="Medium" ></asp:Label><br />
                            <asp:Label ID="lblConfirmationTextKorean" runat="server" Font-Size="Small" style="text-align: left;"
                                Text="CMM과 가입 확인을 위해 통화 가능한 시간을 적어 주세요. 773-777-8889(Ext. 5001)로 확인전화 주셔야 가입이 완료됩니다. CMM 업무시간은 오전 9:00부터 오후 5:30(중부시간)입니다.)" >
                            </asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>

            <asp:Panel ID="pnlButtons" runat="server" Width="900" style="margin-left: auto; margin-right: auto; margin-top: 20px; margin-bottom: 100px; ">
                <asp:Button ID="btnPrev" runat="server" Text="Previous" Width="100" Height="25" style="float: left;"
                     OnClick="btnPrev_Click" />
                <asp:Button ID="btnSubmitApplication" runat="server" Text="Submit Application" Width="150" Height="25" style="float: right;"
                     OnClick="btnSubmitApplication_Click" />
            </asp:Panel>

        </div>
    </form>
</body>
</html>
