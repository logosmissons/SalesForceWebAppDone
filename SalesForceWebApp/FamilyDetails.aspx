<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FamilyDetails.aspx.cs" EnableEventValidation="false" Inherits="SalesForceWebApp.FamilyDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Content/FamilyDetails.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <script src='<%= Page.ResolveClientUrl("~/Scripts/personal-info.js") %>' type="text/javascript"></script>
        <script src='<%= Page.ResolveClientUrl("~/Scripts/smoking-drug-alcohol.js") %>' type="text/javascript"></script>
        <script type="text/javascript">

            function ShowAddSpousePopup() {
                $find("AddSpouse").show();

                return false;
            }

            function HideAddSpousePopup() {
                $find("AddSpouse").hide();

                return false;
            }

            function cvChildGenderClientValidation(oSrc, args) {
                args.IsValid = false;

                if (args.Value == "Male" || args.Value == "Female") args.IsValid = true;
            }

            function HideEditChildInfo() {
                var mpeChildEditInfo = $find("EditChild");
                mpeChildEditInfo.hide();
            }
        </script>

        <asp:ScriptManager ID="smFamiliyDetails" runat="server"></asp:ScriptManager>


        <div>
            <%--<asp:Panel ID="pnlFamilyDetails" runat="server" Height="500px" Width="840px" GroupingText="Family Details" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" >--%>

            <div style="text-align: center; font-size: xx-large; font-weight: bolder; margin-top: 25px; margin-bottom: 10px;">
                <p>The Christian Mutual Med-Aid</p>
            </div>

            <asp:Panel ID="pnlFamilyDetails" runat="server" Height="550px" Width="840px" GroupingText="Family Details" Font-Bold="true" Font-Size="Large" Font-Names="Arial"
                Style="vertical-align: middle; margin-top: 100px; margin-left: auto; margin-right: auto;">
                <asp:Panel ID="pnlSpouse" runat="server" Height="150px" Width="780px" Font-Size="Small">
                    <asp:UpdatePanel ID="upnlSpouseAddRemove" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlSpouseHeader" runat="server" Height="35px" Width="780px" Style="margin-top: 30px;">
                                <asp:Label ID="lblSpouse" runat="server" Text="Spouse" Width="80px"></asp:Label>
                                <asp:Button ID="btnAddSpouse" runat="server" Text="+ Add" CausesValidation="false" OnClick="btnAddSpouse_Click" />
                                <asp:Button ID="btnRemoveSpouse" runat="server" Text="- Remove" CausesValidation="false" Enabled="false" OnClick="btnRemoveSpouse_Click" />

                                <ajaxToolkit:ModalPopupExtender ID="mpeRemoveSpouse" runat="server" PopupControlID="pnlRemoveSpouseConfirmation" TargetControlID="btnRemoveSpouseHidden" BehaviorID="RemoveSpouse"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="pnlRemoveSpouseConfirmation" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        Confirmation
                                    </div>
                                    <div class="body">
                                        Do you want to delete the Spouse?
                                    </div>
                                    <div class="footer">
                                        <asp:Button ID="btnRemoveSpouseConfirm" runat="server" Text="Yes" Width="80px" Style="vertical-align: middle; margin-left: 60px;"
                                            OnClick="btnRemoveSpouseConfirm_Click" />
                                        <asp:Button ID="btnRemoveSpouseCancel" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />
                                    </div>

                                </asp:Panel>
                                <asp:Button ID="btnRemoveSpouseHidden" runat="server" Text="Button" Style="display: none;" />

                                <ajaxToolkit:ModalPopupExtender ID="mpeSpouseUnchecked" runat="server" PopupControlID="pnlSpouseUnchecked" TargetControlID="btnSpouseUnchecked" BehaviorID="SpouseUnchecked"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="pnlSpouseUnchecked" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        Error
                                    </div>
                                    <div class="body">
                                        You have not selected the spouse
                                    </div>
                                    <div class="footer">
                                        <asp:Button ID="btnSpouseUncheckedOk" runat="server" Text="Ok" OnClick="btnSpouseUncheckedOk_Click" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
                                        <%--                                        <asp:Button ID="Button2" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />--%>
                                    </div>

                                </asp:Panel>
                                <asp:Button ID="btnSpouseUnchecked" runat="server" Text="Button" Style="display: none;" />
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%-- From this line on update panel --%>

                    <asp:UpdatePanel ID="upnlSpouse" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvSpouse" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" AutoGenerateColumns="false" BackColor="White" BorderWidth="2px" CellPadding="5"
                                AlternatingRowStyle-BackColor="LightGray">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectSpouse" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="30px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="30px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpousedName" runat="server" Text="Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("SpouseName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="200px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="200px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Birth" SortExpression="DateOfBirth">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpouseDateOfBirth" runat="server" Text="Date of Birth"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateOfBirth" runat="server" Text='<%# Bind("SpouseDateOfBirth") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="140px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="140px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SSN" SortExpression="SSN">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpouseSSN" runat="server" Text="SSN"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSSN" runat="server" Text='<%# Bind("SpouseSSN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="120px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="120px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpouseGender" runat="server" Text="Gender"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%# Bind("SpouseGender") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="90px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="90px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpouseEdit" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSpouseEdit" runat="server" Text="Edit" CausesValidation="false" CommandArgument='<%# Eval("ContactId") %>' OnClick="btnSpouseEdit_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSpouseDelete" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnSpouseDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ContactId") %>' CommandName="Delete" Text="Delete" OnClick="btnSpouseDelete_Click" />

                                            <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteSpouse" runat="server" TargetControlID="btnSpouseDelete" Enabled="true" DisplayModalPopupID="mpeDeleteSpouse" />
                                            <ajaxToolkit:ModalPopupExtender ID="mpeDeleteSpouse" runat="server" PopupControlID="pnlDeleteSpouse" TargetControlID="btnSpouseDelete"
                                                OkControlID="btnDeleteSpouseConfirm" CancelControlID="btnDeleteSpouseCancel" BackgroundCssClass="modalBackground">
                                            </ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="pnlDeleteSpouse" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                                <div class="header">
                                                    Confirmation
                                                </div>
                                                <div class="body">
                                                    Do you want to delete the Spouse?
                                                </div>
                                                <div class="footer">
                                                    <asp:Button ID="btnDeleteSpouseConfirm" runat="server" Text="Yes" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
                                                    <asp:Button ID="btnDeleteSpouseCancel" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />
                                                </div>

                                            </asp:Panel>

                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <%-- This section of code will be deleted --%>
                    <%--                    <asp:UpdatePanel ID="uplSpouse" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:ListView ID="lvSpouse" runat="server" InsertItemPosition="None">
                                <LayoutTemplate>
                                    <table id="tblSpouse" style="width: 760px; height: 30px;">

                                        <tr>
                                            <th class="SpouseHeader" style="height: 30px; width: 35px;"></th>
                                            <th class="SpouseHeader" style="height: 30px; width: 220px; text-align: left; padding-left: 5px;">Name</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 160px; text-align: left; padding-left: 5px;">Date of Birth</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 140px; text-align: left; padding-left: 5px;">SSN</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 100px; text-align: left; padding-left: 5px;">Gender</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 180px; text-align: left; padding-left: 5px;">Action</th>
                                        </tr>

                                        <asp:PlaceHolder ID="itemplaceholder" runat="server"></asp:PlaceHolder>

                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSelectSpouse" runat="server" Style="width: 35px;" />
                                        </td>
                                        <td style="width: 220px; background-color: white; text-align: left; padding-left: 5px;">
                                            <asp:Label ID="lblSpouseName" runat="server" Text='<%# Eval("SpouseName") %>'></asp:Label>
                                        </td>
                                        <td style="width: 160px; background-color: white; text-align: left; padding-left: 5px;">
                                            <asp:Label ID="lblSpouseDateOfBirth" runat="server" Text='<%# Eval("SpouseDateOfBirth") %>'></asp:Label>
                                        </td>
                                        <td style="width: 140px; background-color: white; text-align: left; padding-left: 5px;">
                                            <asp:Label ID="lblSpouseSSN" runat="server" Text='<%# Eval("SpouseSSN") %>'></asp:Label>
                                        </td>
                                        <td style="width: 100px; background-color: white; text-align: left; padding-left: 5px;">
                                            <asp:Label ID="lblSpouseGender" runat="server" Text='<%# Eval("SpouseGender") %>'></asp:Label>
                                        </td>
                                        <td style="width: 180px; background-color: white; text-align: left; padding-left: 5px;">
                                            <asp:Button ID="btnEditSpouse" runat="server" Text="Edit" Style="margin-right: 5px;" OnClick="btnEditSpouse_Click" /><asp:Button ID="btnDeleteSpouse" runat="server" Text="Delete" OnClick="btnDeleteSpouse_Click" Style="margin-left: 5px;" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <table id="tblEmptySpouse" style="width: 760px; height: 30px;">
                                        <tr>
                                            <th class="SpouseHeader" style="height: 30px; width: 35px;"></th>
                                            <th class="SpouseHeader" style="height: 30px; width: 220px; text-align: left; padding-left: 5px;">Name</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 160px; text-align: left; padding-left: 5px;">Date of Birth</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 140px; text-align: left; padding-left: 5px;">SSN</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 100px; text-align: left; padding-left: 5px;">Gender</th>
                                            <th class="SpouseHeader" style="height: 30px; width: 180px; text-align: left; padding-left: 5px;">Action</th>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>

                    <%-- Up to this line will be deleted --%>
                </asp:Panel>

                <asp:Panel ID="pnlChildren" runat="server" Height="350px" Width="780px" Font-Size="Small">
                    <asp:UpdatePanel ID="upnlAddRemoveChildren" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlChildrenHeader" runat="server" Height="35px" Width="780px">
                                <asp:Label ID="lblChildren" runat="server" Text="Children" Width="80px"></asp:Label>
                                <asp:Button ID="btnAddChild" runat="server" Text="+ Add" CausesValidation="false" OnClick="btnAddChild_Click" />
                                <asp:Button ID="btnRemoveChildren" runat="server" Text="- Remove" CausesValidation="false" OnClick="btnRemoveChildren_Click" />



                                <ajaxToolkit:ModalPopupExtender ID="mpeRemoveChildren" runat="server" PopupControlID="pnlRemoveChildrenConfirmation" TargetControlID="btnRemoveChildrenHidden" BehaviorID="RemoveChildren"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="pnlRemoveChildrenConfirmation" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        Confirmation
                                    </div>
                                    <div class="body">
                                        Do you want to delete the children
                                    </div>
                                    <div class="footer">
                                        <asp:Button ID="btnRemoveChildrenConfirm" runat="server" Text="Yes" OnClick="btnRemoveChildrenConfirm_Click" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
                                        <asp:Button ID="btnRemoveChildrenCancel" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />
                                    </div>

                                </asp:Panel>
                                <asp:Button ID="btnRemoveChildrenHidden" runat="server" Text="Button" Style="display: none;" />



                                <ajaxToolkit:ModalPopupExtender ID="mpeChildrenUnselected" runat="server" PopupControlID="pnlChildrenUnchecked" TargetControlID="btnChildrenUncheckedHidden" BehaviorID="ChildrenUnchecked"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>

                                <asp:Panel ID="pnlChildrenUnchecked" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                    <div class="header">
                                        Error
                                    </div>
                                    <div class="body">
                                        You have not selected the children
                                    </div>
                                    <div class="footer">
                                        <asp:Button ID="btnChildrenUncheckedOk" runat="server" Text="Ok" OnClick="btnChildrenUncheckedOk_Click" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
                                        <%--                                        <asp:Button ID="Button2" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />--%>
                                    </div>

                                </asp:Panel>
                                <asp:Button ID="btnChildrenUncheckedHidden" runat="server" Text="Button" Style="display: none;" />


                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="upnlChildren" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="gvChildren" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" AutoGenerateColumns="false" BackColor="White" BorderWidth="2px" CellPadding="5">
                                <%--                                <RowStyle BackColor="White" />
                                <AlternatingRowStyle BackColor="LightGray" />--%>
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectedChild" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="30px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="30px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildName" runat="server" Text="Name"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("ChildName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="200px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="200px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of Birth" SortExpression="DateOfBirth">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildDateOfBirth" runat="server" Text="Date of Birth"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateOfBirth" runat="server" Text='<%# Bind("ChildDateOfBirth") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="140px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="140px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SSN" SortExpression="SSN">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildSSN" runat="server" Text="SSN"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSSN" runat="server" Text='<%# Bind("ChildSSN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="120px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="120px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gender" SortExpression="Gender">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildGender" runat="server" Text="Gender"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGender" runat="server" Text='<%# Bind("ChildGender") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="90px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="90px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildEdit" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnChildEdit" runat="server" Text="Edit" CausesValidation="false" CommandArgument='<%# Eval("ContactId") %>' OnClick="btnChildEdit_Click" />
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Delete">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblChildDelete" runat="server"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="btnChildDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("ContactId") %>' CommandName="Delete" Text="Delete" OnClick="btnChildDelete_Click" />

                                            <ajaxToolkit:ConfirmButtonExtender ID="cbeDeleteChild" runat="server" TargetControlID="btnChildDelete" Enabled="true" DisplayModalPopupID="mpeDeleteChild" />
                                            <ajaxToolkit:ModalPopupExtender ID="mpeDeleteChild" runat="server" PopupControlID="pnlDeleteChild" TargetControlID="btnChildDelete"
                                                OkControlID="btnDeleteChildConfirm" CancelControlID="btnDeleteChildCancel" BackgroundCssClass="modalBackground">
                                            </ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="pnlDeleteChild" runat="server" Width="300px" Height="150px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
                                                <div class="header">
                                                    Confirmation
                                                </div>
                                                <div class="body">
                                                    Do you want to delete the child?
                                                </div>
                                                <div class="footer">
                                                    <asp:Button ID="btnDeleteChildConfirm" runat="server" Text="Yes" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
                                                    <asp:Button ID="btnDeleteChildCancel" runat="server" Text="No" Width="80px" Style="vertical-align: middle; margin-left: 10px;" />
                                                </div>

                                            </asp:Panel>

                                        </ItemTemplate>
                                        <HeaderStyle BackColor="LightBlue" Height="25px" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                        <ItemStyle BackColor="White" Width="70px" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>


            </asp:Panel>
            <br />
            <asp:Panel ID="pnlFamilyDetailsButton" runat="server" Width="840px" Style="margin-top: 15px; margin-left: auto; margin-right: auto;">
                <asp:Button ID="btnPrevious" runat="server" Text="Previous" OnClick="btnPrevious_Click" />

                <asp:Label ID="lblEditResult" runat="server" Text="Result: "></asp:Label>
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" Style="float: right;" />

            </asp:Panel>
        </div>

        <ajaxToolkit:ModalPopupExtender ID="mpeAddSpouse" runat="server" PopupControlID="pnlAddSpouse" TargetControlID="btnAddSpouseHidden" BehaviorID="AddSpouse"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlAddSpouse" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="pnlBackGround">
            <asp:UpdatePanel ID="upnlAddSpouse" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlAddSpouseDetails" runat="server" Width="750" Height="450" Style="margin-left: auto; margin-right: auto;">

                        <asp:Label ID="lblAddSpouseTitle" runat="server" Width="580px" Text="Add Spouse" Font-Bold="true" Font-Size="X-Large" Font-Names="Arial" Style="margin-top: 20px; margin-bottom: 30px;"></asp:Label><br />
                        <asp:Panel ID="pnlSpouseLastName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblSpouseLastName" runat="server" Height="20px" Width="180px" Text="Last Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtSpouseLastName" runat="server" Height="30px" Width="180px" CssClass="TextBox" Style="border-left-color: red; border-left-style: solid; border-left-width: 2px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSpouseLastName" runat="server" ControlToValidate="txtSpouseLastName" ErrorMessage="Spouse last name required" Font-Size="Small" Font-Bold="false" ForeColor="Red"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlSpouseFirstName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblSpouseFirstName" runat="server" Height="20px" Width="180px" Text="First Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtSpouseFirstName" runat="server" Height="30px" Width="180px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSpouseFirstName" runat="server" ControlToValidate="txtSpouseFirstName" ErrorMessage="Spouse first name required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlSpouseMiddleName" runat="server" Width="160px" CssClass="pnlLabel">
                            <asp:Label ID="lblSpouseMiddleName" runat="server" Height="20px" Width="160px" Text="Middle Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtSpouseMiddleName" runat="server" Height="30px" Width="160px" CssClass="TextBox"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="pnlSpouseDateOfBirth" runat="server" Width="240px" CssClass="pnlLabel">
                            <asp:Label ID="lblpnlSpouseDateOfBirth" runat="server" Height="20px" Width="230px" Text=" Date of Birth" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtSpouseDateOfBirth" runat="server" Height="30px" Width="230px" CssClass="TextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calExSpouseDateOfBirth" runat="server" TargetControlID="txtSpouseDateOfBirth" DefaultView="Years" />
                            <asp:RequiredFieldValidator ID="rfvSpouseDateOfBirth" runat="server" ControlToValidate="txtSpouseDateOfBirth" ErrorMessage="Spouse date of birth required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlSpouseGender" runat="server" Width="312px" CssClass="pnlLabel">
                            <asp:Label ID="lblSpouseGender" runat="server" Height="20px" Width="312px" Text="Gender" Font-Bold="true"></asp:Label><br />
                            <asp:Panel ID="pnlGenderRadioButtonList" runat="server" Height="34px" Width="312px" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 3px;">
                                <asp:RadioButtonList ID="rbListSpouseGender" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" ValidationGroup="SpouseGender" Font-Size="Small" Font-Names="Arial" Font-Bold="true">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                            <asp:RequiredFieldValidator ID="rfvSpouseGender" runat="server" ControlToValidate="rbListSpouseGender" ErrorMessage="Gender required" ValidationGroup="SpouseGender" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>

                        <asp:Panel ID="pnlSpouseSSN" runat="server" Height="50px" Width="300px" CssClass="pnlLabel" Style="margin-left: 275px; margin-right: 10px; margin-bottom: 40px;">
                            <asp:Label ID="lblpnlSpouseSSN" runat="server" Height="20px" Width="290px" Text="SSN"></asp:Label>
                            <asp:TextBox ID="txtSpouseSSN" runat="server" onclick="mouse_left_click_ssn(event, this);" onfocus="mouse_left_click_ssn(event, this);"
                                onfocusout="ssn_lost_focus(event, this)" onkeypress="accept_social_security_number(event, this);" onkeydown="filter_control_character_ssn(event, this);"
                                Height="30px" Width="290px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSpouseSSN" runat="server" ControlToValidate="txtSpouseSSN" ErrorMessage="Spouse SSN required" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revSpouseSSN" runat="server" ControlToValidate="txtSpouseSSN" ErrorMessage="Invalid SSN" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"
                                ValidationExpression="\d{3}-\d{2}-\d{4}"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlSpouseSmokingDrugAlcohol" runat="server" HorizontalAlign="Center" Height="300px" Width="750px" Style="margin-left: auto; margin-right: auto;">
                        <asp:Table ID="tblSpouseSmokingDrugAlcohol" runat="server">

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is a current smoker.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is a former smoker.</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcSpouseCurrentSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnSpouseCurrentSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseCurrentSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnSpouseCurrentSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseCurrentSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcSpouseFormerSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnSpouseFormerSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseFormerSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnSpouseFormerSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseFormerSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is currently using narcotic drugs.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse formerly used narcotic drugs.</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcSpouseNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnSpouseNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnSpouseNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcSpouseFormerNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnSpouseFormerNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseFormerNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnSpouseFormerNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseFormerNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse abuse alcohol not following the Biblical teaching on the use of it.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial"></asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcSpouseAlcohol" runat="server" ColumnSpan="1" HorizontalAlign="Left">
                                    <asp:Button ID="btnSpouseAlcoholYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseAlcoholYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnSpouseAlcoholNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnSpouseAlcoholNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>

                    <asp:Panel ID="pnlConfirmCancel" runat="server" Width="750" Height="40" Style="margin-left: auto; margin-right: auto;">

                        <asp:Button ID="btnSpouseClose" runat="server" OnClick="btnSpouseClose_Click" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Cancel"
                            CausesValidation="false" Height="25px" Width="120px" Style="vertical-align: bottom; float: right; margin: 5px;" />
                        <asp:Button ID="btnSpouseConfirm" runat="server" OnClick="btnSpouseConfirm_Click" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Confirm"
                            Height="25px" Width="120px" Style="vertical-align: bottom; float: right; margin: 5px;" />
                    </asp:Panel>
                    <br />
                    <asp:Label ID="lblResult" runat="server" Text="Result"></asp:Label>

                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnAddSpouseHidden" runat="server" Text="Button" Style="display: none;" />


        <ajaxToolkit:ModalPopupExtender ID="mpeEditSpouse" runat="server" PopupControlID="pnlEditSpouse" TargetControlID="btnEditSpouseHidden" BehaviorID="EditSpouse"></ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlEditSpouse" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="pnlBackGround">
            <asp:UpdatePanel ID="upnlEditSpouse" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlEditSpouseDetails" runat="server" Width="750" Height="450" Style="margin-left: auto; margin-right: auto;">
                        <asp:Label ID="lblEditSpouseTitle" runat="server" Width="580px" Text="Edit Spouse" Font-Bold="true" Font-Size="X-Large" Font-Names="Arial" Style="margin-top: 20px; margin-bottom: 30px;"></asp:Label>
                        <asp:Panel ID="pnlEditSpouseLastName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditSpouseLastName" runat="server" Height="20px" Width="180px" Text="Last Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditSpouseLastName" runat="server" Height="30px" Width="180px" CssClass="TextBox" Style="border-left-color: red; border-left-style: solid; border-left-width: 2px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditSpouseLastName" runat="server" ControlToValidate="txtEditSpouseLastName" ErrorMessage="Spouse last name required" Font-Size="Small" Font-Bold="false" ForeColor="Red"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditSpouseFirstName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditSpouseFirstName" runat="server" Height="20px" Width="180px" Text="First Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditSpouseFirstName" runat="server" Height="30px" Width="180px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditSpouseFirstName" runat="server" ControlToValidate="txtEditSpouseFirstName" ErrorMessage="Spouse first name required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditSpouseMiddleName" runat="server" Width="160px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditSpouseMiddleName" runat="server" Height="20px" Width="160px" Text="Middle Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditSpouseMiddleName" runat="server" Height="30px" Width="160px" CssClass="TextBox"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditSpouseDateOfBirth" runat="server" Width="240px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditSpouseDateOfBirth" runat="server" Height="20px" Width="230px" Text=" Date of Birth" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditSpouseDateOfBirth" runat="server" Height="30px" Width="230px" CssClass="TextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calEditSpouseDateOfBirth" runat="server" TargetControlID="txtEditSpouseDateOfBirth" DefaultView="Years" />
                            <asp:RequiredFieldValidator ID="rfvEditSpouseDateOfBirth" runat="server" ControlToValidate="txtEditSpouseDateOfBirth" ErrorMessage="Spouse date of birth required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditSpouseGender" runat="server" Width="312px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditSpouseGender" runat="server" Height="20px" Width="312px" Text="Gender" Font-Bold="true"></asp:Label><br />
                            <asp:Panel ID="pnlEditSpouseGenderRadioButtonList" runat="server" Height="34px" Width="312px" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 3px;">
                                <asp:RadioButtonList ID="rbListEditSpouseGender" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" ValidationGroup="SpouseGender" Font-Size="Small" Font-Names="Arial" Font-Bold="true">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                            <asp:RequiredFieldValidator ID="rfvEditSpouseGender" runat="server" ControlToValidate="rbListEditSpouseGender" ErrorMessage="Gender required" ValidationGroup="SpouseGender" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>

                        <asp:Panel ID="pnlEditSpouseSSN" runat="server" Height="50px" Width="300px" CssClass="pnlLabel" Style="margin-left: 275px; margin-right: 10px; margin-bottom: 40px;">
                            <asp:Label ID="lblEditSpouseSSN" runat="server" Height="20px" Width="290px" Text="SSN"></asp:Label>
                            <asp:TextBox ID="txtEditSpouseSSN" runat="server" Height="30px" Width="290px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditSpouseSSN" runat="server" ControlToValidate="txtEditSpouseSSN" ErrorMessage="Spouse SSN required" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEditSpouseSSN" runat="server" ControlToValidate="txtEditSpouseSSN" ErrorMessage="Invalid SSN" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"
                                ValidationExpression="\d{3}-\d{2}-\d{4}"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlEditSpouseSmokingDrugAlcohol" runat="server" HorizontalAlign="Center" Height="300px" Width="750px" Style="margin-left: auto; margin-right: auto;">
                        <asp:Table ID="tblEditSpouseSmokingDrugAlcohol" runat="server">

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is a current smoker.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is a former smoker.</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditSpouseCurrentSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditSpouseCurrentSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseCurrentSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditSpouseCurrentSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseCurrentSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcEditSpouseFormerSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditSpouseFormerSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseFormerSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditSpouseFormerSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseFormerSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse is currently using narcotic drugs.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse formerly used narcotic drugs.</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditSpouseNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditSpouseNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditSpouseNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcEditSpouseFormerNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditSpouseFormerNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseFormerNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditSpouseFormerNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseFormerNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">Your spouse abuse alcohol not following the Biblical teaching on the use of it.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial"></asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditSpouseAlcohol" runat="server" ColumnSpan="1" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditSpouseAlcoholYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseAlcoholYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditSpouseAlcoholNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditSpouseAlcoholNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>


                    <asp:Button ID="btnCancelSpouse" runat="server" OnClick="btnCancelSpouse_Click" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Cancel" CausesValidation="false" Height="25px" Width="120px" Style="vertical-align: bottom; float: right; margin: 5px;" />
                    <asp:Button ID="btnUpdateSpouse" runat="server" OnClick="btnUpdateSpouse_Click" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Update" Height="25px" Width="120px" Style="vertical-align: bottom; float: right; margin: 5px;" />
                    <br />
                    <asp:Label ID="lblUpdateResult" runat="server" Text="Result"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnEditSpouseHidden" runat="server" Text="Button" Style="display: none;" />





        <ajaxToolkit:ModalPopupExtender ID="mpeDeleteSpouseConfirmation" runat="server" PopupControlID="pnlDeleteSpouse" TargetControlID="btnDeleteSpouseHidden" BehaviorID="DeleteSpouse"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlDeleteSpouse" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="pnlDlgBackGround">
            <asp:Label ID="lblDeleteSpouseConfirmation" runat="server" Text="Are your sure to delete your spouse information?"></asp:Label><br />
            <br />
            <br />
            <asp:Button ID="btnDeleteSpouseYes" runat="server" Text="Yes" OnClick="btnDeleteSpouseYes_Click" Width="80px" Style="vertical-align: middle; margin-left: 60px;" />
            <asp:Button ID="btnDeleteSpouseNo" runat="server" Text="No" OnClick="btnDeleteSpouseNo_Click" Width="80px" Style="vertical-align: middle; margin-left: 40px;" /><br />
            <br />
        </asp:Panel>
        <asp:Button ID="btnDeleteSpouseHidden" runat="server" Text="Button" Style="display: none;" />

        <ajaxToolkit:ModalPopupExtender ID="mpeAddChild" runat="server" PopupControlID="pnlAddChild" TargetControlID="btnAddChildHidden" BehaviorID="AddChild"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlAddChild" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="pnlBackGround">

            <asp:UpdatePanel ID="upnlAddChild" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlAddChildDetail" runat="server" Width="750" Height="450" Style="margin-left: auto; margin-right: auto;">
                        <asp:Label ID="lblAddChild" runat="server" Width="580px" Text="Add Child" Font-Bold="true" Font-Size="X-Large" Font-Names="Arial" Style="margin-top: 20px; margin-bottom: 30px;"></asp:Label><br />
                        <asp:Panel ID="pnlChildLastName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblChildLastName" runat="server" Height="20px" Width="180px" Text="Last Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtChildLastName" runat="server" Height="30px" Width="180px" CssClass="TextBox" Style="border-left-color: red; border-left-style: solid; border-left-width: 2px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvChildLastName" runat="server" ControlToValidate="txtChildLastName" EnableClientScript="false" ErrorMessage="Child last name required" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlChildFirstName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblChildFirstName" runat="server" Height="20px" Width="180px" Text="First Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtChildFirstName" runat="server" Height="30px" Width="180px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvChildFirstName" runat="server" ControlToValidate="txtChildFirstName" ErrorMessage="Child first name required" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlChildMiddleName" runat="server" Width="160px" CssClass="pnlLabel">
                            <asp:Label ID="lblChildMiddleName" runat="server" Height="20px" Width="160px" Text="Middle Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtChildMiddleName" runat="server" Height="30px" Width="160px" CssClass="TextBox"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="pnlChildDateOfBirth" runat="server" Width="240px" CssClass="pnlLabel">
                            <asp:Label ID="lblChildDateOfBirth" runat="server" Height="20px" Width="230px" Text=" Date of Birth" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtChildDateOfBirth" runat="server" Height="30px" Width="230px" CssClass="TextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="cadExChildDateOfBirth" runat="server" TargetControlID="txtChildDateOfBirth" DefaultView="Years" />
                            <asp:RequiredFieldValidator ID="rfvChildDateOfBirth" runat="server" ControlToValidate="txtChildDateOfBirth" ErrorMessage="Child's date of birth required" Display="Dynamic" ForeColor="Red" Font-Bold="false" Font-Size="Small"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlChildGender" runat="server" Width="312px" CssClass="pnlLabel">
                            <asp:Label ID="lblChildGender" runat="server" Height="20px" Width="312px" Text="Gender" Font-Bold="true"></asp:Label><br />
                            <asp:Panel ID="pnlChildGenderRadioButton" runat="server" Height="34px" Width="312px" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 3px;">
                                <asp:RadioButtonList ID="rbListChildGender" runat="server" CausesValidation="true" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" ValidationGroup="ChildGender" Font-Size="Small" Font-Names="Arial" Font-Bold="true">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList><br />
                                <asp:RequiredFieldValidator ID="rfvChildGender" runat="server" ControlToValidate="rbListChildGender" ErrorMessage="Child gender required" ValidationGroup="ChildGender" Display="Dynamic"
                                    ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                            </asp:Panel>
                        </asp:Panel>

                        <asp:Panel ID="pnlChildSSN" runat="server" Height="50px" Width="300px" CssClass="pnlLabel" Style="margin-left: 275px; margin-right: 10px; margin-bottom: 40px;">
                            <asp:Label ID="lblChildSSN" runat="server" Height="20px" Width="290px" Text="SSN"></asp:Label>
                            <asp:TextBox ID="txtChildSSN" runat="server" onclick="mouse_left_click_ssn(event, this);" onfocus="mouse_left_click_ssn(event, this);"
                                onfocusout="ssn_lost_focus(event, this)" onkeypress="accept_social_security_number(event, this);" onkeydown="filter_control_character_ssn(event, this);"
                                Height="30px" Width="290px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvChildSSN" runat="server" ControlToValidate="txtChildSSN" ErrorMessage="Child SSN required" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revChildSSN" runat="server" ControlToValidate="txtChildSSN" ErrorMessage="Invalid SSN" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"
                                ValidationExpression="\d{3}-\d{2}-\d{4}"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </asp:Panel>

                    <asp:Panel ID="pnlChildSmokingDrugAlcohol" runat="server" HorizontalAlign="Center" Height="300px" Width="780px" Style="margin-left: auto; margin-right: auto;">
                        <asp:Table ID="tblChildSmokingDrugAlcohol" runat="server">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is a current smoker.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child was a former smoker.</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcChildCurrentSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnChildCurrentSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildCurrentSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnChildCurrentSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildCurrentSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcChildFormerSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnChildFormerSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildFormerSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnChildFormerSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildFormerSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is currently using narcotic drugs.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is a formerly used narcotic drugs.</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcChildNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnChildNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnChildNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcChildFormerNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnChildFormerNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildFormerNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnChildFormerNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildFormerNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child abuses alcohol not following the Biblical teaching on the use of it.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial"></asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcChildAlcohol" runat="server" ColumnSpan="1" HorizontalAlign="Left">
                                    <asp:Button ID="btnChildAlcoholYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildAlcoholYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnChildAlcoholNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnChildAlcoholNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>

                    <asp:Panel ID="pnlChilldConfirmCancel" runat="server">
                        <asp:Button ID="btnChildClose" runat="server" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Cancel" Height="25px" Width="120px" OnClick="btnChildClose_Click" Style="vertical-align: bottom; float: right; margin: 5px;" />
                        <asp:Button ID="btnChildConfirm" runat="server" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Confirm" Height="25px" Width="120px" OnClick="btnChildConfirm_Click" CausesValidation="true"
                            Style="vertical-align: bottom; float: right; margin: 5px;" />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:Panel>
        <asp:Button ID="btnAddChildHidden" runat="server" Text="Button" Style="display: none;" />

        <ajaxToolkit:ModalPopupExtender ID="mpeEditChild" runat="server" PopupControlID="pnlEditChild" TargetControlID="btnEditChildHidden" BehaviorID="EditChild"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlEditChild" runat="server" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="pnlBackGround">
            <asp:UpdatePanel ID="upnlEditChild" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlEditChildDetail" runat="server" Width="750" Height="450" Style="margin-left: auto; margin-right: auto;">
                        <asp:Label ID="lblEditChildTitle" runat="server" Width="580px" Text="Edit Child" Font-Bold="true" Font-Size="X-Large" Font-Names="Arial" Style="margin-top: 20px; margin-bottom: 30px;"></asp:Label>
                        <asp:Panel ID="pnlEditChildLastName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditChildLastName" runat="server" Height="20px" Width="180px" Text="Last Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditChildLastName" runat="server" Height="30px" Width="180px" CssClass="TextBox" Style="border-left-color: red; border-left-style: solid; border-left-width: 2px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditChildLastName" runat="server" ControlToValidate="txtEditChildLastName" ErrorMessage="Child last name required" Font-Size="Small" Font-Bold="false" ForeColor="Red"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditChildFirstName" runat="server" Width="190px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditChildFirstName" runat="server" Height="20px" Width="180px" Text="First Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditChildFirstName" runat="server" Height="30px" Width="180px" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEditChildFirstName" runat="server" ControlToValidate="txtEditChildFirstName" ErrorMessage="Child first name required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditChildMiddleName" runat="server" Width="160px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditChildMiddleName" runat="server" Height="20px" Width="160px" Text="Middle Name" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditChildMiddleName" runat="server" Height="30px" Width="160px" CssClass="TextBox"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditChildDateOfBirth" runat="server" Width="240px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditChildDateOfBirth" runat="server" Height="20px" Width="230px" Text=" Date of Birth" Font-Bold="true"></asp:Label><br />
                            <asp:TextBox ID="txtEditChildDateOfBirth" runat="server" Height="30px" Width="230px" CssClass="TextBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="calEditChildDateOfBirth" runat="server" TargetControlID="txtEditChildDateOfBirth" DefaultView="Years" />
                            <asp:RequiredFieldValidator ID="rfvEditChildDateOfBirth" runat="server" ControlToValidate="txtEditChildDateOfBirth" ErrorMessage="Child date of birth required" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>
                        <asp:Panel ID="pnlEditChildGender" runat="server" Width="312px" CssClass="pnlLabel">
                            <asp:Label ID="lblEditChildGender" runat="server" Height="20px" Width="312px" Text="Gender" Font-Bold="true"></asp:Label><br />
                            <asp:Panel ID="pnlEditChildGenderRadioButtonList" runat="server" Height="34px" Width="312px" BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Style="margin-top: 3px;">
                                <asp:RadioButtonList ID="rbListEditChildGender" runat="server" RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" ValidationGroup="ChildGender" Font-Size="Small" Font-Names="Arial" Font-Bold="true">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:Panel>
                            <asp:RequiredFieldValidator ID="rfvEditChildGender" runat="server" ControlToValidate="rbListEditChildGender" ErrorMessage="Gender required" ValidationGroup="ChildGender" Display="Dynamic" ForeColor="Red" Font-Size="Small" Font-Bold="false"></asp:RequiredFieldValidator>
                        </asp:Panel>

                        <asp:Panel ID="pnlEditChildSSN" runat="server" Height="50px" Width="300px" CssClass="pnlLabel" Style="margin-left: 275px; margin-right: 10px; margin-bottom: 40px;">
                            <asp:Label ID="lblEditChildSSN" runat="server" Height="20px" Width="290px" Text="SSN"></asp:Label>
                            <asp:TextBox ID="txtEditChildSSN" runat="server" Height="30px" Width="290px" ReadOnly="true" CssClass="TextBox"></asp:TextBox>
                        </asp:Panel>
                    </asp:Panel>



                    <asp:Panel ID="pnlEditChildSmokingDrugAlcohol" runat="server" HorizontalAlign="Center" Height="300px" Width="780px" Style="margin-left: auto; margin-right: auto;">
                        <asp:Table ID="tblEditChildSmokingDrugAlcohol" runat="server">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is a current smoker.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child was a former smoker.</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditChildCurrentSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditChildCurrentSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildCurrentSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditChildCurrentSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildCurrentSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcEditChildFormerSmoker" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditChildFormerSmokerYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildFormerSmokerYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditChildFormerSmokerNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildFormerSmokerNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is currently using narcotic drugs.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child is a formerly used narcotic drugs.</asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditChildNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditChildNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditChildNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                                <asp:TableCell ID="tcEditChildFormerNarcotic" runat="server" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditChildFormerNarcoticYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleFormerYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildFormerNarcoticYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditChildFormerNarcoticNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleFormerNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildFormerNarcoticNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial">The child drinks alcohol on a regular basis.</asp:TableCell>
                                <asp:TableCell ColumnSpan="1" HorizontalAlign="Left" VerticalAlign="Bottom" Width="350" Height="40" Font-Bold="true" Font-Names="Arial"></asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server">
                                <asp:TableCell ID="tcEditChildAlcohol" runat="server" ColumnSpan="1" HorizontalAlign="Left">
                                    <asp:Button ID="btnEditChildAlcoholYes" runat="server" Text="Yes" Width="40" Height="35" Font-Bold="true" BackColor="LightGray" ForeColor="Black" BorderWidth="0"
                                        OnClientClick="toggleYes(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildAlcoholYes" runat="server" Value="lightgrey" />
                                    <asp:Button ID="btnEditChildAlcoholNo" runat="server" Text="No" Width="40" Height="35" Font-Bold="true" BackColor="Blue" ForeColor="White" BorderWidth="0"
                                        OnClientClick="toggleNo(this); return false;" Style="text-align: center; vertical-align: middle; font-family: Arial;" />
                                    <asp:HiddenField ID="hdnEditChildAlcoholNo" runat="server" Value="blue" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:Panel>
                    <asp:Panel ID="pnlEditChildConfirmCancel" runat="server">
                        <asp:Button ID="btnEditChildCancel" runat="server" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Cancel"
                            Height="25px" Width="120px" Style="vertical-align: bottom; float: right; margin: 5px;" OnClientClick="HideEditChildInfo();"
                            OnClick="btnEditChildCancel_Click" />
                        <asp:Button ID="btnEditChildUpdateConfirm" runat="server" Font-Bold="true" Font-Size="Medium" Font-Names="Arial" Text="Update" Height="25px" Width="120px"
                            OnClick="btnEditChildUpdateConfirm_Click"
                            Style="vertical-align: bottom; float: right; margin: 5px;" />
                        <br />
                        <asp:Label ID="lblEditChildUpdateResult" runat="server" Text="Result"></asp:Label>
                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Button ID="btnEditChildHidden" runat="server" Text="Button" Style="display: none;" />


        <ajaxToolkit:ModalPopupExtender ID="mpeSpouseSmokingDrug" runat="server" PopupControlID="pnlSpouseSmokingDrug" TargetControlID="btnSpouseSmokingDrugHidden" BehaviorID="SpouseSmokingDrug"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlSpouseSmokingDrug" runat="server" Width="300px" Height="210px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
            <div class="header">
                Alert
            </div>
            <div class="body">
                If your spouse is currently smoking, using narcotic drug or drink alcohol, your spouse can't be CMM member.
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnSpouseOk" runat="server" Text="Ok" Width="80px" Style="vertical-align: middle;" OnClick="btnSpouseOk_Click" />
            </div>
        </asp:Panel>
        <asp:Button ID="btnSpouseSmokingDrugHidden" runat="server" Text="Button" Style="display: none;" />

        <ajaxToolkit:ModalPopupExtender ID="mpeUpdateSpouseSmokingDrugDrinking" runat="server" PopupControlID="pnlUpdateSpouseSmokingDrugDrinking"
            TargetControlID="btnUpdateSpouseSmokingDrugDrinkingHidden" BehaviorID="UpdateSpouseSmokingDrug" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlUpdateSpouseSmokingDrugDrinking" runat="server" Width="300px" Height="210px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
            <div class="header">
                Alert
            </div>
            <div class="body">
                If your spouse is currently smoking, using narcotic drug or drink alcohol, your spouse can't be CMM member.
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnUpdateSpouseOk" runat="server" Text="Ok" Width="80px" Style="vertical-align: middle;" OnClick="btnUpdateSpouseOk_Click" />
            </div>
        </asp:Panel>
        <asp:Button ID="btnUpdateSpouseSmokingDrugDrinkingHidden" runat="server" Text="Button" Style="display: none;" />

        <ajaxToolkit:ModalPopupExtender ID="mpeChildSmokingDrug" runat="server" PopupControlID="pnlChildSmokingDrug" TargetControlID="btnChildSmokingDrugHidden" BehaviorID="ChildSmokingDrug"
            BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlChildSmokingDrug" runat="server" Width="300px" Height="210px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
            <div class="header">
                Alert
            </div>
            <div class="body">
                If your child is currently smoking, using narcotic drug or drink alcohol, your child can't be CMM member.
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnChildOk" runat="server" Text="Ok" Width="80px" Style="vertical-align: middle;" OnClick="btnChildOk_Click" />
            </div>
        </asp:Panel>
        <asp:Button ID="btnChildSmokingDrugHidden" runat="server" Text="Button" Style="display: none;" />

        <ajaxToolkit:ModalPopupExtender ID="mpeEditChildSmokingDrugDrink" runat="server" PopupControlID="pnlEditChildSmokingDrugDrink" TargetControlID="btnEditChildSmokingDrugAlcoholHidden"
             BehaviorID="EditChildSmokingDrugAlcohol" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlEditChildSmokingDrugDrink" runat="server" Width="300px" Height="210px" Font-Bold="true" Font-Size="Large" Font-Names="Arial" CssClass="modalPopup" Style="display: none;">
            <div class="header">
                Alert
            </div>
            <div class="body">
                If your child is currently smoking, using narcotic drug or drink alcohol, your child can't be CMM member.
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnEditChildOk" runat="server" Text="Ok" Width="80px" Style="vertical-align: middle;" OnClick="btnEditChildOk_Click" />
            </div>
        </asp:Panel>
        <asp:Button ID="btnEditChildSmokingDrugAlcoholHidden" runat="server" Text="Button" Style="display: none;" />


    </form>
</body>
</html>
