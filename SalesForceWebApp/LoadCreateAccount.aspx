<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadCreateAccount.aspx.cs" Inherits="SalesForceWebApp.LoadCreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <%--    <script language="javascript">--%>
    <script type="text/javascript">

        var iLoopCounter = 1;
        var iMaxLoop = 6;
        var iIntervalId;

        function BeginPageLoad() {
            location.href = "<%= Request.QueryString["Page"] %>"
            iIntervalId = window.setInterval("iLoopCounter=UpdateProgressMeter(iLoopCounter, iMaxLoop)", 500);
        }

        function EndPageLoad() {
            window.clearInterval(iIntervalId);
            ProgressMeter.innerText = "Page Loaded -- Not Transferring";
        }

        function UpdateProgressMeter(iCurrentLoopCounter, iMaximumLoops) {

            iCurrentLoopCounter += 1;

            if (iCurrentLoopCounter <= iMaximumLoops) {
                ProgressMeter.innerText += ".";
                return iCurrentLoopCounter;
            }
            else {
                ProgressMeter.innerText = "";
                return 1;
            }
        }

    </script>
</head>
<body onload="BeginPageLoad()" onunload="EndPageLoad()">
    <form id="form1" runat="server">
            <asp:Table runat="server" BorderWidth="0" CellPadding="0" CellSpacing="0" Width="400" Height="100" style="margin-left: auto; margin-right: auto; margin-top: 400px; ">
                <asp:TableRow>
                    <asp:TableCell runat="server" HorizontalAlign="Left" VerticalAlign="Middle" >
                        <span id="MessageText">Processing Order&nbsp;-- Please Wait</span>
                        <span id="ProgressMeter" style="WIDTH:25px;TEXT-ALIGN:left"></span>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
    </form>
</body>
</html>
