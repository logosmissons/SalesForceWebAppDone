<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SalesForceWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Label ID="lblId" runat="server" Text="Id: " ></asp:Label>
    <asp:TextBox ID="txtId" runat="server" Text=""></asp:TextBox>

    <asp:Label ID="lblMembershipId" runat="server" Text="Membership ID: "></asp:Label>
    <asp:TextBox ID="txtMembershipId" runat="server" Text=""></asp:TextBox>

    <asp:Label ID="lblIndividualId" runat="server" Text="Individual ID: "></asp:Label>
    <asp:TextBox ID="txtIndividualId" runat="server" Text=""></asp:TextBox>

    <asp:Label ID="lblMembershipNameInContact" runat="server" Text="Membership Name In Contact: " ></asp:Label>
    <asp:TextBox ID="txtMembershipNameInContact" runat="server" Text=""></asp:TextBox>

<br />
     <asp:Label ID="lblFirstName" runat="server" Text="FirstName: "></asp:Label>
    <asp:TextBox ID="txtFirstName" runat="server" Text=""></asp:TextBox> 

    <asp:Label ID="lblLastName" runat="server" Text="Last Name: "></asp:Label>
    <asp:TextBox ID="txtLastName" runat="server" Text=""></asp:TextBox>

    <br />
    <asp:Label ID="lblStreetAddress" runat="server" Text="Address: " ></asp:Label>
    <asp:TextBox ID="txtStreetAddress" runat="server" Text="" ></asp:TextBox>

    <br />
    <asp:Label ID="lblCity" runat="server" Text="City: " ></asp:Label>
    <asp:TextBox ID="txtCity" runat="server" Text="" ></asp:TextBox>

    <asp:Label ID="lblState" runat="server" Text="State: " ></asp:Label>
    <asp:TextBox ID="txtState" runat="server" Text=""></asp:TextBox>

    <asp:Label ID="lblZip" runat="server" Text="Zip: " ></asp:Label>
    <asp:TextBox ID="txtZip" runat="server" Text="" ></asp:TextBox>

    <br />
    <asp:Label ID="lblMobilePhone" runat="server" Text="Cell Phone: "></asp:Label>
    <asp:TextBox ID="txtMobilePhone" runat="server" Text="" ></asp:TextBox>

       
    <br />

    <asp:Label ID="lblPhone" runat="server" Text="Phone: "></asp:Label>
    <asp:TextBox ID="txtPhone" runat="server" Text=""></asp:TextBox> 


    <br />


    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" Text=""></asp:TextBox> 

    <br />

    <br />

    <asp:TextBox ID="txtResult" runat="server" TextMode="MultiLine"></asp:TextBox>

    <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
<br />

    <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="btnLoad_Click" /> <br />

    <asp:Button ID="btnFirst" runat="server" Text="First" OnClick="btnFirst_Click" />
    <asp:Button ID="btnPrev" runat="server" Text="Prev" OnClick="btnPrev_Click" />
    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
    <asp:Button ID="btnLast" runat="server" Text="Last" OnClick="btnLast_Click" />
    <br />
    <asp:Button ID="btnCreateNewContact" runat="server" Text="Create New" OnClick="btnCreateNewContact_Click" />
    
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />

    <br />
    <asp:Label ID="lblMembershipInContact" runat="server" Text="Membership in Contact"></asp:Label>
    <asp:TextBox ID="txtMembershipInContact" runat="server" Text=""></asp:TextBox>
    
    <hr /><br />

    <asp:Label ID="lblContactId" runat="server" Text="Contact Id: "></asp:Label>
    <asp:TextBox ID="txtContactId" runat="server" Text=""></asp:TextBox>

    <asp:Label ID="lblMembershipName" runat="server" Text="Membership Name: " ></asp:Label>
    <asp:TextBox ID="txtMembershipName" runat="server" Text="" ></asp:TextBox>

    <asp:Label ID="lblMembershipStatus" runat="server" Text="Membership Status: " >

    </asp:Label>
    <asp:TextBox ID="txtMembershipStatus" runat="server" Text=""></asp:TextBox> <br />


    <asp:Button ID="btnMemberPrev" runat="server" Text="Prev" OnClick="btnMemberPrev_Click" />
    <asp:Button ID="btnMemberNext" runat="server" Text="Next" OnClick="btnMemberNext_Click" />

    <hr />

    <asp:Label ID="lblTaskName" runat="server" Text="Name in Task: " ></asp:Label>
    <asp:TextBox ID="txtTaskName" runat="server" Text=""></asp:TextBox>
    <asp:Label ID="lblTaskComment" runat="server" Text="Comment in Task: "></asp:Label>
    <asp:TextBox ID="txtTaskComment" runat="server" Text="" TextMode="MultiLine" Columns="40" Rows="5"></asp:TextBox>

    <br />
    <asp:Button ID="btnTaskFirst" runat="server" Text="First Task" OnClick="btnTaskFirst_Click" />
    <asp:Button ID="btnTaskPrev" runat="server" Text="Prev Task" OnClick="btnTaskPrev_Click" />
    <asp:Button ID="btnTaskNext" runat="server" Text="Next Task" OnClick="btnTaskNext_Click" />
    <asp:Button ID="btnTaskLast" runat="server" Text="Last Task" OnClick="btnTaskLast_Click" />

    <br />
    <asp:Button ID="btnEditComment" runat="server" Text="Edit Comment" OnClick="btnEditComment_Click" />
    <asp:Button ID="btnUpdateComment" runat="server" Text="Update Comment" OnClick="btnUpdateComment_Click" />

    <br />
    <asp:Label ID="lblTaskCommentUpdateResult" runat="server" Text="" ></asp:Label>


<%--        <asp:Label ID="lblMembershipFirstName" runat="server" Text="Membership FirstName: " ></asp:Label>
    <asp:TextBox ID="txtMembershipFirstName" runat="server" Text="" ></asp:TextBox>

    <asp:Label ID="lblMembershipLastName" runat="server" Text="Membership LastName: ">
    </asp:Label>
    <asp:TextBox ID="txtMembershipLastName" runat="server" Text="" ></asp:TextBox>--%>


</asp:Content>