<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewSubmit.aspx.cs" Inherits="SalesForceWebApp.ReviewSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <style type="text/css">
        .pnlPersonalDetails
        {
            border-style: solid;
            border-width: 1px;
            border-color: gray;
            margin-left: 100px;
            padding: 20px;
            align-self: center;
        }

    </style>
    <form id="form1" runat="server">
    <div>

        <asp:Panel ID="pnlReviewPersonalDetails" runat="server" Width="400" Height="600" CssClass="pnlPersonalDetails" >
            <asp:Label ID="lblSectionTitle" runat="server" Width="380" Height="35" Text="Personal Details" Font-Names="Arial" Font-Bold="true" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblEmailLabel" runat="server" Text="Email: " Width="100" Height="20" ></asp:Label>
            <asp:Label ID="lblEmail" runat="server" Width="250" Text="" Height="20"></asp:Label><br />
            <asp:Label ID="lblNameLabel" runat="server" Text="Name: " Width="100" Height="20"></asp:Label>
            <asp:Label ID="lblName" runat="server" Width="250" Text="" Height="20"></asp:Label><br />
            <asp:Label ID="lblBirthdateLabel" runat="server" Text="Birthdate: " Width="100" Height="20"></asp:Label>
            <asp:Label ID="lblBirthdate" runat="server" Width="250" Text="" Height="20"></asp:Label><br />
            <asp:Label ID="lblGenderLabel" runat="server" Text="Gender: " Width="100" Height="20"></asp:Label>
            <asp:Label ID="lblGender" runat="server" Width="250" Text="" Height="20"></asp:Label><br />
            <asp:Label ID="lblSSNLabel" runat="server" Text="SSN: " Width="100" Height="20"></asp:Label>
            <asp:Label ID="lblSSN" runat="server" Width="250" Text="" Height="20"></asp:Label><br />
            <asp:Label ID="lblTelephone1Label" runat="server" Text="Telephone 1: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblTelephone1" runat="server" Text="" Width="200" Height="20"></asp:Label><br />
            <asp:Label ID="lblTelephone2Label" runat="server" Text="Telephone 2: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblTelephone2" runat="server" Text="" Width="200" Height="20"></asp:Label><br />
            <asp:Label ID="lblTelephone3Label" runat="server" Text="Telephone 3: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblTelephone3" runat="server" Text="" Width="200" Height="20"></asp:Label><br />
            <asp:Label ID="lblAddressLabel" runat="server" Text="Street: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblAddress" runat="server" Text="" Width="150" Height="20"></asp:Label><br />
            <asp:Label ID="lblZipCodeLabel" runat="server" Text="Zip: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblZipCode" runat="server" Text="" Width="150" Height="20"></asp:Label><br />
            <asp:Label ID="lblStateLabel" runat="server" Text="State: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblState" runat="server" Text="" Width="100" Height="20"></asp:Label><br />
            <asp:Label ID="lblCityLabel" runat="server" Text="City: " Width="150" Height="20"></asp:Label>
            <asp:Label ID="lblCity" runat="server" Text="" Width="200" Height="20"></asp:Label>
            <asp:Panel ID="pnlSmokingDrugAlcohol" runat="server" Width="280" Height="200">
                <asp:Panel ID="pnlSmoking" runat="server" Width="100" Height="50">
                    <asp:Label ID="lblCurrentSmoker" runat="server" Text="Current Smoker" Width="150" Height="20"></asp:Label><br />
                    <asp:Button ID="btnCurrentSmokerYes" runat="server" Text="Yes" Width="30" Height="20" />
                    <asp:Button ID="btnCurrentSmokerNo" runat="server" Text="No" Width="30" Height="20" />
                </asp:Panel>
                <asp:Panel ID="pnlFormerSmoker" runat="server" Width="100" Height="50">
                    <asp:Label ID="lblFormerSmoker" runat="server" Text="Former Smoker" Width="150" Height="20"></asp:Label><br />
                    <asp:Button ID="btnFormerSmokerYes" runat="server" Text="Yes" Width="30" Height="20" />
                    <asp:Button ID="btnFormerSmokerNo" runat="server" Text="No" Width="30" Height="20" />
                </asp:Panel><br />
                <asp:Panel ID="pnlCurrentDrug" runat="server" Width="100" Height="50">
                    <asp:Label ID="lblCurrentDrug" runat="server" Text="Currently Taking Drug" Width="150" Height="20"></asp:Label><br />
                    <asp:Button ID="btnCurrentDrugYes" runat="server" Text="Yes" Width="30" Height="20" />
                    <asp:Button ID="btnCurrentDrugNo" runat="server" Text="No" Width="30" Height="20" />
                </asp:Panel>
                <asp:Panel ID="pnlFormerDrug" runat="server" Widht="100" Height="50">
                    <asp:Label ID="lblFormerlyDrug" runat="server" Text="Formerly Taking Drug" Width="150" Height="20"></asp:Label><br />
                    <asp:Button ID="btnFormerlyDrugYes" runat="server" Text="Yes" Width="30" Height="20" />
                    <asp:Button ID="btnFormerlyDrugNo" runat="server" Text="No" Width="30" Height="20" />
                </asp:Panel>
            </asp:Panel>

            


        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
