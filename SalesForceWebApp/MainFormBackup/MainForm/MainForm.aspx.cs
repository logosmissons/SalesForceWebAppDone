using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text;
using System.Drawing;

using Microsoft.AspNet.Identity;
using System.Security.Principal;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

using Microsoft.AspNet.Identity.Owin;


namespace SalesForceWebApp
{
    public partial class Register : System.Web.UI.Page
    {
        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "5Speed2oflight5";

        enum ZipCodePostBackLocation { PrimarySuccess, PrimaryFailure, ChurchSuccess, ChurchFailure, BillingSuccess, BillingFailure };

        protected string strHouseholdId = null;
        protected string strContactId = null;
        protected string strUserEmail = string.Empty;

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        //private SForce.QueryResult queryResult = null;
        private SForce.QueryResult queryResultContactId = null;
        private SForce.QueryResult queryResultContactForMichaelJordan = null;
        //private SForce.QueryResult queryResultHouseholdInfoFromContact = null;
        private SForce.QueryResult queryResultMembershipInfo = null;
        private SForce.QueryResult queryResultFamilyInfo = null;

        private SForce.QueryResult queryResultSpouseProgram = null;
        private SForce.QueryResult queryResultChildrenProgram = null;

        private SForce.QueryResult queryResultSpouseInfo = null;
        private SForce.QueryResult queryResultPaymentInfo = null;
        private SForce.QueryResult queryResultChildrenInfo = null;

        //private SForce.QueryResult queryResultSpouseInfoFromContact = null;
        //private SForce.QueryResult queryResultChildrenInfoFromContact = null;
        //private SForce.QueryResult queryResultGiftSendingInfoFromContact = null;

        private SForce.Contact ctContactId = null;
        private SForce.Contact ctMichaelJordan = null;
        private SForce.Contact ctMembershipInfo = null;
        private SForce.Contact ctFamilyInfo = null;

        //private SForce.Contact ctSpouseInfo = null;
        //private SForce.Contact ctChildrenInfo = null;

        //private SForce.causeview__Payment__c paymentInfo = null;
        //private SForce.Membership__c paymentInfoInMembership = null;
        private SForce.Contact ctPaymentInfoInMembership = null;

        //private string strQueryContactForPortalUser = null;

        private string strQueryContactForContactID = null;

        private string strQueryContactForMichaelJordan = null;

        private string strQueryMembershipInfo = null;

        private string strQueryFamilyInfo = null;

        private string strQueryPaymentInfoInMembership = null;

        private string strQueryPaymentInfo = null;


        //private string strQuerySpouseProgram = null;
        //private string strQueryChildrenProgram = null;
        //private string strQuerySpouseInfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Don't erase this line!!!
                ///////////////////////////////////////////////////////////////////////////////
                //lblPersonalInfoUpdateMessage.Text = Context.User.Identity.Name.ToString();
                ///////////////////////////////////////////////////////////////////////////////

                strUserEmail = Context.User.Identity.Name.ToString();
                //Session["UserEmail"] = strUserEmail;

                //SetSQLStatementForPortalUser();



                if (Context.User.Identity.Name != null &&
                    Context.User.Identity.IsAuthenticated)
                {

                    //// This block of code shoud be placed in Login.aspx.cs
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    //SetSQLStatementForPortalUser();
                    //InitializedSfdcbinding();

                    //SForce.QueryResult qrPortalUser = Sfdcbinding.query(strQueryContactForPortalUser);
                    //if (qrPortalUser.size > 0)
                    //{
                    //    SForce.Contact ctPortalUser = (SForce.Contact)qrPortalUser.records[0];

                    //    if (ctPortalUser.causeview__PortalUser__c == true)
                    //    {
                    //        //LoadPersonalInfo();
                    //    }
                    //}
                    //////////////////////////////////////////////////////////////


                    LoadPersonalInfo();
                }
                // This is working
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}",
                //    txtChurchName.ClientID), true);

            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //SetFocus(txtChurchName);

            InitializedHiddenFields();

        }

        protected void InitializedHiddenFields()
        {
            hdnAddressBorderColor.Value = "black";
            hdnAddressBorderWidth.Value = "1";
            hdnAddressFontColor.Value = "black";

            hdnBillingCityBorderColor.Value = "black";
            hdnBillingCityBorderWidth.Value = "1";
            hdnBillingCityFontColor.Value = "black";

            hdnBillingStateBorderColor.Value = "black"; ;
            hdnBillingStateBorderWidth.Value = "1";
            hdnBillingStateFontColor.Value = "black";

            hdnBillingStreetBorderColor.Value = "black";
            hdnBillingStreetBorderWidth.Value = "1";
            hdnBillingStreetFontColor.Value = "black";

            hdnBillingZipCodeBorderColor.Value = "black";
            hdnBillingZipCodeBorderWidth.Value = "1";
            hdnBillingZipCodeFontColor.Value = "black";

            hdnChurchCityBorderColor.Value = "black";
            hdnChurchCityBorderWidth.Value = "1";
            hdnChurchCityFontColor.Value = "black";

            hdnChurchNameBorderColor.Value = "black";
            hdnChurchNameBorderWidth.Value = "1";
            hdnChurchNameFontColor.Value = "black";

            hdnChurchStateBorderColor.Value = "black";
            hdnChurchStateBorderWidth.Value = "1";
            hdnChurchStateFontColor.Value = "black";

            hdnChurchStreetBorderColor.Value = "black";
            hdnChurchStreetBorderWidth.Value = "1";
            hdnChurchStreetFontColor.Value = "black";

            hdnChurchTelephoneBorderColor.Value = "black";
            hdnChurchTelephoneBorderWidth.Value = "1";
            hdnChurchTelephoneFontColor.Value = "black";

            hdnChurchZipBorderColor.Value = "black";
            hdnChurchZipBorderWidth.Value = "1";
            hdnChurchZipFontColor.Value = "black";

            hdnCityBorderColor.Value = "black";
            hdnCityBorderWidth.Value = "1";
            hdnCityFontColor.Value = "black";

            hdnEmailBorderColor.Value = "black";
            hdnEmailBorderWidth.Value = "1";
            hdnEmailFontColor.Value = "black";

            hdnFirstNameBorderWidth.Value = "black";
            hdnFirstNameBorderWidth.Value = "1";
            hdnFirstNameFontColor.Value = "black";

            hdnLastNameBorderColor.Value = "black";
            hdnLastNameBorderWidth.Value = "1";
            hdnLastNameFontColor.Value = "black";

            hdnMiddleNameBorderColor.Value = "black";
            hdnMiddleNameBorderWidth.Value = "1";
            hdnMiddleNameFontColor.Value = "black";

            hdnMobilePhoneBorderColor.Value = "black";
            hdnMobilePhoneBorderWidth.Value = "1";
            hdnMobilePhoneFontColor.Value = "black";

            hdnOtherPhoneBorderColor.Value = "black";
            hdnOtherPhoneBorderWidth.Value = "1";
            hdnOtherPhoneFontColor.Value = "black";

            hdnPastorBorderColor.Value = "black"; ;
            hdnPastorBorderWidth.Value = "1";
            hdnPastorFontColor.Value = "black";

            hdnStateBorderColor.Value = "black";
            hdnStateBorderWidth.Value = "1";
            hdnStateFontColor.Value = "black";

            hdnTelephoneBorderColor.Value = "black";
            hdnTelephoneBorderWidth.Value = "1";
            hdnTelephoneFontColor.Value = "black";

            hdnZipCodeBorderColor.Value = "black";
            hdnZipCodeBorderWidth.Value = "1";
            hdnZipCodeFontColor.Value = "black";

        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected SForce.Contact GetContactIdForLogInUser(string strSqlStatement)
        {
            queryResultContactId = Sfdcbinding.query(strSqlStatement);

            SForce.Contact contact = null;

            if (queryResultContactId.size > 0)
            {
                contact = (SForce.Contact)queryResultContactId.records[0];
                Session["ContactId"] = contact.Id.ToString();
            }

            return contact;
        }

        protected void InitializeSQLStatements()
        {
            //strQueryContactForContactID = "select id from Contact where Email = 'seanjones@emailz.com'";

            strQueryContactForContactID = "select id from Contact where Email = '" + strUserEmail + "'";

            //strQueryContactForContactID = "select id, causeview__PortalUser__c, Email from Contact where Email = '" + strUserEmail + "'";


            ctContactId = GetContactIdForLogInUser(strQueryContactForContactID);

            strContactId = ctContactId.Id.ToString();

            strQueryContactForMichaelJordan = "select Id, c4g_Membership__r.Name, causeview__Suffix__c, FirstName, LastName, MiddleName, Birthdate, Gender__c, " +
                                                         "Social_Security_Number__c, MailingAddress, Email, Phone, MobilePhone, OtherPhone, causeview__Solicit_Codes__c, " +
                                                         "causeview__Household_Role__c from Contact where Id = '" + strContactId + "'";

            strQueryMembershipInfo = "select c4g_Qualifies_for_Medicare__c, c4g_Qualifies_for_Medicare_A_and_B__c, c4g_Plan__r.Name, Birthdate, " +
                                                "c4g_Church__r.Name, c4g_Church__r.Senior_Pastor_s_Name__c, c4g_Church__r.ShippingAddress, c4g_Church__r.Phone, " +
                                                "c4g_Membership__r.Owner.Name, c4g_Membership__r.Invoice_Delivery__c " +
                                                "from Contact where Id = '" + strContactId + "'";

            strQueryFamilyInfo = "select causeview__Household__c from Contact where Id = '" + strContactId + "'";

            strQueryPaymentInfoInMembership = "select c4g_Membership__r.Payment_Method__c, c4g_Membership__r.Payment_Frequency__c from Contact " +
                                     "where Id = '" + strContactId + "'";

            strQueryPaymentInfo = "select causeview__Payment_Type__c, causeview__Donation__r.causeview__Gift_Type__c from causeview__Payment__c " +
                                                "where causeview__Donation__r.causeview__Constituent__r.Id = '" + strContactId + "'";

        }

        protected void LoadPersonalInfo()
        {

            InitializedSfdcbinding();
            InitializeSQLStatements();

            queryResultContactForMichaelJordan = Sfdcbinding.query(strQueryContactForMichaelJordan);

            if (queryResultContactForMichaelJordan.size > 0)
            {
                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                if (ctMichaelJordan.c4g_Membership__r != null)
                {
                    if (ctMichaelJordan.c4g_Membership__r.Name != null) txtPrimaryID.Text = ctMichaelJordan.c4g_Membership__r.Name;
                }
                else if (ctMichaelJordan.c4g_Membership__r == null)
                {
                    txtPrimaryID.Text = "";
                }

                if (ctMichaelJordan.Email != null)
                {
                    txtEmail.Text = ctMichaelJordan.Email;
                    hdnEmail.Value = ctMichaelJordan.Email;
                }
                else if (ctMichaelJordan.Email == null) txtEmail.Text = "";

                if (ctMichaelJordan.causeview__Suffix__c != null)
                {
                    switch (ctMichaelJordan.causeview__Suffix__c)
                    {
                        case "Jr.":
                            ddlTitle.SelectedIndex = 0;
                            break;
                        case "Mr.":
                            ddlTitle.SelectedIndex = 1;
                            break;
                        case "Mrs.":
                            ddlTitle.SelectedIndex = 2;
                            break;
                        case "Ms.":
                            ddlTitle.SelectedIndex = 3;
                            break;
                        default:
                            ddlTitle.SelectedIndex = 4;
                            break;
                    }
                }

                if (ctMichaelJordan.LastName != null)
                {
                    txtLastName.Text = ctMichaelJordan.LastName;
                    hdnLastName.Value = ctMichaelJordan.LastName;
                }
                else if (ctMichaelJordan.LastName == null) txtLastName.Text = "";

                if (ctMichaelJordan.FirstName != null)
                {
                    txtFirstName.Text = ctMichaelJordan.FirstName;
                    hdnFirstName.Value = ctMichaelJordan.FirstName;
                }
                else if (ctMichaelJordan.FirstName == null) txtFirstName.Text = "";

                if (ctMichaelJordan.MiddleName != null)
                {
                    txtMiddleName.Text = ctMichaelJordan.MiddleName;
                    hdnMiddleName.Value = ctMichaelJordan.MiddleName;
                }
                else if (ctMichaelJordan.MiddleName == null) txtMiddleName.Text = "";

                if (ctMichaelJordan.Birthdate != null)
                {
                    DateTime dtMemberBirthdate = new DateTime((int)ctMichaelJordan.Birthdate.Value.Year, (int)ctMichaelJordan.Birthdate.Value.Month, (int)ctMichaelJordan.Birthdate.Value.Day);
                    StringBuilder sbMemberBirthdate = new StringBuilder();
                    sbMemberBirthdate.Append(dtMemberBirthdate.ToShortDateString());
                    txtDateOfBirth.Text = sbMemberBirthdate.ToString();
                }
                else txtDateOfBirth.Text = "";

                if (ctMichaelJordan.Gender__c == "M") rbMale.Checked = true;
                else if (ctMichaelJordan.Gender__c == "F") rbFemale.Checked = true;
                else if (ctMichaelJordan.Gender__c == null)
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = false;
                }

                if (ctMichaelJordan.Social_Security_Number__c != null) txtSSN.Text = ctMichaelJordan.Social_Security_Number__c;
                else if (ctMichaelJordan.Social_Security_Number__c == null) txtSSN.Text = "";

                if (ctMichaelJordan.Phone != null)
                {
                    txtTelephone1.Text = ctMichaelJordan.Phone;
                    hdnTelephone.Value = ctMichaelJordan.Phone;
                }
                else if (ctMichaelJordan.Phone == null) txtTelephone1.Text = "";

                if (ctMichaelJordan.MobilePhone != null)
                {
                    txtTelephone2.Text = ctMichaelJordan.MobilePhone;
                    hdnMobilePhone.Value = ctMichaelJordan.MobilePhone;
                }
                else if (ctMichaelJordan.MobilePhone == null) txtTelephone2.Text = "";

                if (ctMichaelJordan.OtherPhone != null)
                {
                    txtTelephone3.Text = ctMichaelJordan.OtherPhone;
                    hdnOtherPhone.Value = ctMichaelJordan.OtherPhone;
                }
                else if (ctMichaelJordan.OtherPhone == null) txtTelephone3.Text = "";

                if (ctMichaelJordan.MailingAddress != null)
                {
                    txtAddress.Text = ctMichaelJordan.MailingAddress.street;
                    hdnAddress.Value = ctMichaelJordan.MailingAddress.street;

                    txtZipCode.Text = ctMichaelJordan.MailingAddress.postalCode;
                    hdnZipCode.Value = ctMichaelJordan.MailingAddress.postalCode;

                    txtState.Text = ctMichaelJordan.MailingAddress.state;
                    hdnState.Value = ctMichaelJordan.MailingAddress.street;

                    txtCity.Text = ctMichaelJordan.MailingAddress.city;
                    hdnCity.Value = ctMichaelJordan.MailingAddress.city;

                }
            }

            queryResultMembershipInfo = Sfdcbinding.query(strQueryMembershipInfo);

            if (queryResultMembershipInfo.size > 0)
            {
                ctMembershipInfo = (SForce.Contact)queryResultMembershipInfo.records[0];

                if (ctMembershipInfo.c4g_Qualifies_for_Medicare__c == true)
                {
                    rbYesQualifyForMedicare.Checked = true;
                    rbNoQualifyForMedicare.Checked = false;
                }
                else if (ctMembershipInfo.c4g_Qualifies_for_Medicare__c == false)
                {
                    rbYesQualifyForMedicare.Checked = false;
                    rbNoQualifyForMedicare.Checked = true;
                }

                if (ctMembershipInfo.c4g_Qualifies_for_Medicare_A_and_B__c == true)
                {
                    rbMedicareABYes.Checked = true;
                    rbMedicareABNo.Checked = false;
                }
                else if (ctMembershipInfo.c4g_Qualifies_for_Medicare_A_and_B__c == false)
                {
                    rbMedicareABYes.Checked = false;
                    rbMedicareABNo.Checked = true;
                }

                if (ctMembershipInfo.c4g_Plan__r != null)
                {
                    if (ctMembershipInfo.c4g_Plan__r.Name != null)
                    {
                        ddlPartipantsProgram.Items.Add(new ListItem(ctMembershipInfo.c4g_Plan__r.Name));
                        ddlPartipantsProgram.SelectedIndex = 0;
                    }
                }

                //String strQueryHousehold = "select causeview__Household__c from Contact where Email = 'seanjones@emailz.com'";

                String strQueryHouseholdRole = "select causeview__Household_Role__c from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrHouseholdRole = Sfdcbinding.query(strQueryHouseholdRole);

                SForce.Contact ctHouseholdRole = null;

                if (qrHouseholdRole.size > 0)
                {
                    ctHouseholdRole = (SForce.Contact)qrHouseholdRole.records[0];
                }

                String strHouseholdRole = String.Empty;
                if (ctHouseholdRole.causeview__Household_Role__c != null) strHouseholdRole = ctHouseholdRole.causeview__Household_Role__c.ToString();

                String strQueryHousehold = "select causeview__Household__c from Contact where Id = '" + strContactId + "'";
                SForce.QueryResult qrHousehold = Sfdcbinding.query(strQueryHousehold);

                if (strHouseholdRole == "Head of Household")
                {
                    //String strQueryHousehold = "select causeview__Household__c from Contact where Id = '" + strContactId + "'";
                    //SForce.QueryResult qrHousehold = Sfdcbinding.query(strQueryHousehold);

                    if (qrHousehold.size > 0)
                    {
                        SForce.Contact ctHousehold = (SForce.Contact)qrHousehold.records[0];

                        String strSpouseInfo = "select c4g_Plan__r.Name, Birthdate from Contact where causeview__Household__c = '" + ctHousehold.causeview__Household__c.ToString() +
                                                "' " + "and causeview__Household_Role__c = 'Spouse'";

                        queryResultSpouseProgram = Sfdcbinding.query(strSpouseInfo);

                        if (queryResultSpouseProgram.size > 0)
                        {
                            SForce.Contact ctSpouse = (SForce.Contact)queryResultSpouseProgram.records[0];

                            ddlSpouseProgram.Items.Add(new ListItem(ctSpouse.c4g_Plan__r.Name));
                            ddlSpouseProgram.SelectedIndex = 0;
                        }

                        String strChildrenInfo = "select c4g_Plan__r.Name, Birthdate from Contact where causeview__Household__c = '" + ctHousehold.causeview__Household__c.ToString() +
                                                 "' " + "and causeview__Household_Role__c = 'Child'";

                        queryResultChildrenProgram = Sfdcbinding.query(strChildrenInfo);

                        if (queryResultChildrenProgram.size > 0)
                        {
                            SForce.Contact ctChild = (SForce.Contact)queryResultChildrenProgram.records[0];

                            ddlChildrenProgram.Items.Add(ctChild.c4g_Plan__r.Name);
                            ddlChildrenProgram.SelectedIndex = 0;
                        }
                    }
                }
                else if (strHouseholdRole == "Spouse")
                {
                    if (qrHousehold.size > 0)
                    {
                        SForce.Contact ctHousehold = (SForce.Contact)qrHousehold.records[0];

                        String strHouseholdInfo = "select c4g_Plan__r.Name, Birthdate from Contact where causeview__Household__c = '" + ctHousehold.causeview__Household__c.ToString() +
                                                "' " + "and causeview__Household_Role__c = 'Head of Household'";

                        queryResultSpouseProgram = Sfdcbinding.query(strHouseholdInfo);

                        if (queryResultSpouseProgram.size > 0)
                        {
                            SForce.Contact ctSpouse = (SForce.Contact)queryResultSpouseProgram.records[0];

                            ddlSpouseProgram.Items.Add(new ListItem(ctSpouse.c4g_Plan__r.Name));
                            ddlSpouseProgram.SelectedIndex = 0;
                        }

                        String strChildrenInfo = "select c4g_Plan__r.Name, Birthdate from Contact where causeview__Household__c = '" + ctHousehold.causeview__Household__c.ToString() +
                                                 "' " + "and causeview__Household_Role__c = 'Child'";

                        queryResultChildrenProgram = Sfdcbinding.query(strChildrenInfo);

                        if (queryResultChildrenProgram.size > 0)
                        {
                            SForce.Contact ctChild = (SForce.Contact)queryResultChildrenProgram.records[0];

                            ddlChildrenProgram.Items.Add(ctChild.c4g_Plan__r.Name);
                            ddlChildrenProgram.SelectedIndex = 0;
                        }
                    }
                }

                if (ctMembershipInfo.c4g_Church__r != null)
                {
                    if (ctMembershipInfo.c4g_Church__r.Name != null)
                    {
                        txtChurchName.Text = ctMembershipInfo.c4g_Church__r.Name;
                        hdnChurchName.Value = ctMembershipInfo.c4g_Church__r.Name;
                    }
                    if (ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c != null)
                    {
                        txtSeniorPastor.Text = ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c;
                        hdnSeniorPastor.Value = ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c;
                    }
                    if (ctMembershipInfo.c4g_Church__r.ShippingAddress != null)
                    {
                        txtChurchStreet.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.street;
                        hdnChurchStreet.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.street;

                        txtChurchZip.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.postalCode;
                        hdnChurchStreet.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.postalCode;

                        txtChurchState.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.state;
                        hdnChurchState.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.state;

                        txtChurchCity.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.city;
                        hdnChurchCity.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.city;
                    }
                    if (ctMembershipInfo.c4g_Church__r.Phone != null)
                    {
                        txtChurchTelephone.Text = ctMembershipInfo.c4g_Church__r.Phone;
                        hdnChurchTelephone.Value = ctMembershipInfo.c4g_Church__r.Phone;
                    }
                }
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Family Information
            //////////////////////////////////


            queryResultFamilyInfo = Sfdcbinding.query(strQueryFamilyInfo);

            if (queryResultFamilyInfo.size > 0)
            {
                ctFamilyInfo = (SForce.Contact)queryResultFamilyInfo.records[0];

                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                SForce.Contact spouse = null;

                String strQueryForChildren = "select FirstName, LastName, Birthdate, c4g_Membership_Start_Date__c, causeview__Household_Role__c from Contact " +
                             "where causeview__Household__c = '" + ctFamilyInfo.causeview__Household__c.ToString() + "' " +
                             "and causeview__Household_Role__c = 'Child'";

                if (ctMichaelJordan.causeview__Household_Role__c == "Head of Household")
                {
                    String strQueryForSpouse = "select FirstName, LastName, Birthdate,  c4g_Membership_Start_Date__c, causeview__Household_Role__c from Contact " +
                                               "where causeview__Household__c = '" + ctFamilyInfo.causeview__Household__c.ToString() +
                                               "' and causeview__Household_Role__c = 'Spouse'";

                    queryResultSpouseInfo = Sfdcbinding.query(strQueryForSpouse);

                    if (queryResultSpouseInfo.size > 0)
                    {
                        spouse = (SForce.Contact)queryResultSpouseInfo.records[0];

                        StringBuilder sbName = new StringBuilder();
                        sbName.Append(spouse.LastName);
                        sbName.Append(", ");
                        sbName.Append(spouse.FirstName);
                        lblSpouseName.Text = sbName.ToString();

                        lblSpouseNameLabel.Visible = true;
                        lblSpouseName.Visible = true;

                        DateTime dtSpouseBirthdate = new DateTime();

                        if (spouse.Birthdate != null) dtSpouseBirthdate = new DateTime((int)spouse.Birthdate.Value.Year, (int)spouse.Birthdate.Value.Month, (int)spouse.Birthdate.Value.Day);

                        StringBuilder sbBirthDate = new StringBuilder();
                        sbBirthDate.Append(dtSpouseBirthdate.ToShortDateString());
                        lblSpouseDateOfBirth.Text = sbBirthDate.ToString();

                        lblSpouseDateOfBirthLabel.Visible = true;
                        lblSpouseDateOfBirth.Visible = true;

                        DateTime dtSpouseStartDate = new DateTime();

                        if (spouse.c4g_Membership_Start_Date__c != null) dtSpouseStartDate = new DateTime((int)spouse.c4g_Membership_Start_Date__c.Value.Year,
                                                                                                          (int)spouse.c4g_Membership_Start_Date__c.Value.Month,
                                                                                                          (int)spouse.c4g_Membership_Start_Date__c.Value.Day);
                        StringBuilder sbStartDate = new StringBuilder();
                        sbStartDate.Append(dtSpouseStartDate.ToShortDateString());
                        lblSpouseStartDate.Text = sbStartDate.ToString();

                        lblSpouseStartDateLabel.Visible = true;
                        lblSpouseStartDate.Visible = true;
                    }
                    else
                    {
                        lblSpouseName.Width = 300;
                        lblSpouseName.Text = "No spouse has been added";
                        lblSpouseName.Visible = true;
                    }

                    queryResultChildrenInfo = Sfdcbinding.query(strQueryForChildren);

                    if (queryResultChildrenInfo.size > 0)
                    {
                        SForce.Contact[] child = new SForce.Contact[queryResultChildrenInfo.size];

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            child[i] = (SForce.Contact)queryResultChildrenInfo.records[i];
                        }

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        StringBuilder[] sbChildName = new StringBuilder[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenDateOfBirth = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenDateOfBirth = new DateTime[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenStartDate = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenStartDate = new DateTime[queryResultChildrenInfo.size];

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            if (child[i] != null)
                            {

                                sbChildName[i] = new StringBuilder();

                                sbChildName[i].Append(child[i].LastName);
                                sbChildName[i].Append(", ");
                                sbChildName[i].Append(child[i].FirstName);

                                if (child[i].Birthdate != null)
                                {
                                    dtChildrenDateOfBirth[i] = new DateTime((int)child[i].Birthdate.Value.Year, (int)child[i].Birthdate.Value.Month, (int)child[i].Birthdate.Value.Day);
                                    sbChildrenDateOfBirth[i] = new StringBuilder();
                                    sbChildrenDateOfBirth[i].Append(dtChildrenDateOfBirth[i].ToShortDateString());
                                }

                                if (child[i].c4g_Membership_Start_Date__c != null)
                                {

                                    dtChildrenStartDate[i] = new DateTime((int)child[i].c4g_Membership_Start_Date__c.Value.Year,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Month,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Day);
                                    sbChildrenStartDate[i] = new StringBuilder();
                                    sbChildrenStartDate[i].Append(dtChildrenStartDate[i].ToShortDateString());
                                }
                            }

                            switch (i)
                            {
                                case 0:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName1.Text = sbChildName[i].ToString();
                                        lblChildName1.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth1.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate1.Visible = true;
                                    }
                                    break;
                                case 1:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName2.Text = sbChildName[i].ToString();
                                        lblChildName2.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth2.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate2.Visible = true;
                                    }
                                    break;
                                case 2:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName3.Text = sbChildName[i].ToString();
                                        lblChildName3.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth3.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate3.Visible = true;
                                    }
                                    break;
                                case 3:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName4.Text = sbChildName[i].ToString();
                                        lblChildName4.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth4.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate4.Visible = true;
                                    }
                                    break;
                                case 4:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName5.Text = sbChildName[i].ToString();
                                        lblChildName5.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth5.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate5.Visible = true;
                                    }
                                    break;
                                case 5:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName6.Text = sbChildName[i].ToString();
                                        lblChildName6.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth6.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate6.Visible = true;
                                    }
                                    break;
                                case 6:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName7.Text = sbChildName[i].ToString();
                                        lblChildName7.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth7.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate7.Visible = true;
                                    }
                                    break;
                                case 7:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName8.Text = sbChildName[i].ToString();
                                        lblChildName8.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth8.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate8.Visible = true;
                                    }
                                    break;
                                case 8:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName9.Text = sbChildName[i].ToString();
                                        lblChildName9.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth9.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate9.Visible = true;
                                    }
                                    break;
                                case 9:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName10.Text = sbChildName[i].ToString();
                                        lblChildName10.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth10.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate10.Visible = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lblChildName1.Visible = true;
                        lblChildName1.Width = 300;
                        lblChildName1.Text = "No child has been added";
                    }


                }
                else if (ctMichaelJordan.causeview__Household_Role__c == "Spouse")
                {
                    String strQueryForHead = "select FirstName, LastName, Birthdate, c4g_Membership_Start_Date__c, causeview__Household_Role__c from Contact " +
                                             "where causeview__Household__c = '" + ctFamilyInfo.causeview__Household__c.ToString() + "' " +
                                             "and causeview__Household_Role__c = 'Head of Household'";

                    queryResultSpouseInfo = Sfdcbinding.query(strQueryForHead);

                    if (queryResultSpouseInfo.size > 0)
                    {

                        spouse = (SForce.Contact)queryResultSpouseInfo.records[0];

                        StringBuilder sbName = new StringBuilder();
                        sbName.Append(spouse.LastName);
                        sbName.Append(", ");
                        sbName.Append(spouse.FirstName);
                        lblSpouseName.Text = sbName.ToString();

                        lblSpouseNameLabel.Visible = true;
                        lblSpouseName.Visible = true;

                        DateTime dtSpouseBirthdate = new DateTime();

                        if (spouse.Birthdate != null)
                        {
                            dtSpouseBirthdate = new DateTime((int)spouse.Birthdate.Value.Year, (int)spouse.Birthdate.Value.Month, (int)spouse.Birthdate.Value.Day);

                            StringBuilder sbBirthDate = new StringBuilder();
                            sbBirthDate.Append(dtSpouseBirthdate.ToShortDateString());
                            lblSpouseDateOfBirth.Text = sbBirthDate.ToString();
                        }

                        lblSpouseDateOfBirthLabel.Visible = true;
                        lblSpouseDateOfBirth.Visible = true;

                        DateTime dtSpouseStartDate = new DateTime();

                        if (spouse.c4g_Membership_Start_Date__c != null)
                        {
                            dtSpouseStartDate = new DateTime((int)spouse.c4g_Membership_Start_Date__c.Value.Year, (int)spouse.c4g_Membership_Start_Date__c.Value.Month, (int)spouse.c4g_Membership_Start_Date__c.Value.Day);
                            StringBuilder sbStartDate = new StringBuilder();
                            sbStartDate.Append(dtSpouseStartDate.ToShortDateString());
                            lblSpouseStartDate.Text = sbStartDate.ToString();
                        }

                        lblSpouseStartDateLabel.Visible = true;
                        lblSpouseStartDate.Visible = true;

                    }
                    else
                    {
                        lblSpouseName.Width = 300;
                        lblSpouseName.Text = "No spouse has been added";
                        lblSpouseName.Visible = true;
                    }

                    queryResultChildrenInfo = Sfdcbinding.query(strQueryForChildren);

                    if (queryResultChildrenInfo.size > 0)
                    {
                        SForce.Contact[] child = new SForce.Contact[queryResultChildrenInfo.size];


                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            child[i] = (SForce.Contact)queryResultChildrenInfo.records[i];

                        }

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        StringBuilder[] sbChildName = new StringBuilder[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenDateOfBirth = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenDateOfBirth = new DateTime[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenStartDate = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenStartDate = new DateTime[queryResultChildrenInfo.size];

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            if (child[i] != null)
                            {
                                sbChildName[i] = new StringBuilder();

                                sbChildName[i].Append(child[i].LastName);
                                sbChildName[i].Append(", ");
                                sbChildName[i].Append(child[i].FirstName);

                                dtChildrenDateOfBirth[i] = new DateTime();

                                if (child[i].Birthdate != null)
                                {
                                    dtChildrenDateOfBirth[i] = new DateTime((int)child[i].Birthdate.Value.Year, (int)child[i].Birthdate.Value.Month, (int)child[i].Birthdate.Value.Day);
                                    sbChildrenDateOfBirth[i] = new StringBuilder();
                                    sbChildrenDateOfBirth[i].Append(dtChildrenDateOfBirth[i].ToShortDateString());
                                }

                                if (child[i].c4g_Membership_Start_Date__c != null)
                                {
                                    dtChildrenStartDate[i] = new DateTime((int)child[i].c4g_Membership_Start_Date__c.Value.Year,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Month,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Day);
                                    sbChildrenStartDate[i] = new StringBuilder();
                                    sbChildrenStartDate[i].Append(dtChildrenStartDate[i].ToShortDateString());
                                }
                            }

                            switch (i)
                            {
                                case 0:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName1.Text = sbChildName[i].ToString();
                                        lblChildName1.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth1.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate1.Visible = true;
                                    }
                                    //lblChildName1.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName1.Visible = true;
                                    //lblChildDateOfBirth1.Visible = true;
                                    //lblChildStartDate1.Visible = true;
                                    break;
                                case 1:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName2.Text = sbChildName[i].ToString();
                                        lblChildName2.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth2.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate2.Visible = true;
                                    }

                                    //lblChildName2.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName2.Visible = true;
                                    //lblChildDateOfBirth2.Visible = true;
                                    //lblChildStartDate2.Visible = true;
                                    break;
                                case 2:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName3.Text = sbChildName[i].ToString();
                                        lblChildName3.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth3.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate3.Visible = true;
                                    }

                                    //lblChildName3.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName3.Visible = true;
                                    //lblChildDateOfBirth3.Visible = true;
                                    //lblChildStartDate3.Visible = true;
                                    break;
                                case 3:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName4.Text = sbChildName[i].ToString();
                                        lblChildName4.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth4.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate4.Visible = true;
                                    }


                                    //lblChildName4.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName4.Visible = true;
                                    //lblChildDateOfBirth4.Visible = true;
                                    //lblChildStartDate4.Visible = true;
                                    break;
                                case 4:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName5.Text = sbChildName[i].ToString();
                                        lblChildName5.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth5.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate5.Visible = true;
                                    }


                                    //lblChildName5.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName5.Visible = true;
                                    //lblChildDateOfBirth5.Visible = true;
                                    //lblChildStartDate5.Visible = true;
                                    break;
                                case 5:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName6.Text = sbChildName[i].ToString();
                                        lblChildName6.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth6.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate6.Visible = true;
                                    }


                                    //lblChildName6.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName6.Visible = true;
                                    //lblChildDateOfBirth6.Visible = true;
                                    //lblChildStartDate6.Visible = true;
                                    break;
                                case 6:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName7.Text = sbChildName[i].ToString();
                                        lblChildName7.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth7.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate7.Visible = true;
                                    }


                                    //lblChildName7.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName7.Visible = true;
                                    //lblChildDateOfBirth7.Visible = true;
                                    //lblChildStartDate7.Visible = true;
                                    break;
                                case 7:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName8.Text = sbChildName[i].ToString();
                                        lblChildName8.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth8.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate8.Visible = true;
                                    }

                                    //lblChildName8.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName8.Visible = true;
                                    //lblChildDateOfBirth8.Visible = true;
                                    //lblChildStartDate8.Visible = true;
                                    break;

                                case 8:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName9.Text = sbChildName[i].ToString();
                                        lblChildName9.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth9.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate9.Visible = true;
                                    }

                                    //lblChildName9.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName9.Visible = true;
                                    //lblChildDateOfBirth9.Visible = true;
                                    //lblChildStartDate9.Visible = true;
                                    break;

                                case 9:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName10.Text = sbChildName[i].ToString();
                                        lblChildName10.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth10.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate10.Visible = true;
                                    }


                                    //lblChildName10.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName10.Visible = true;
                                    //lblChildDateOfBirth10.Visible = true;
                                    //lblChildStartDate10.Visible = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lblChildName1.Visible = true;
                        lblChildName1.Width = 300;
                        lblChildName1.Text = "No child has been added";
                    }

                }
                else if (ctMichaelJordan.causeview__Household_Role__c == "Child")
                {
                    lblSpouseName.Width = 300;
                    lblSpouseName.Text = "No spouse has been added.";
                    lblSpouseName.Visible = true;

                    lblChildName1.Visible = true;
                    lblChildName1.Width = 300;
                    lblChildName1.Text = "No child has been added";
                }

                //String strQueryForChildren = "select FirstName, LastName, Birthdate, c4g_Membership_Start_Date__c, causeview__Household_Role__c from Contact " +
                //             "where causeview__Household__c = '" + ctFamilyInfo.causeview__Household__c.ToString() + "' " +
                //             "and causeview__Household_Role__c = 'Child'";

                //queryResultChildrenInfo = Sfdcbinding.query(strQueryForChildren);

                //if (queryResultChildrenInfo.size > 0)
                //{
                //    SForce.Contact[] child = new SForce.Contact[queryResultChildrenInfo.size];


                //    for (int i = 0; i < queryResultChildrenInfo.size; i++)
                //    {
                //        child[i] = (SForce.Contact)queryResultChildrenInfo.records[i];

                //    }

                //    lblChildrenName.Visible = true;
                //    lblChildrenDateOfBirth.Visible = true;
                //    lblChildrenStartDate.Visible = true;

                //    StringBuilder[] sbChildName = new StringBuilder[queryResultChildrenInfo.size];
                //    StringBuilder[] sbChildrenDateOfBirth = new StringBuilder[queryResultChildrenInfo.size];
                //    DateTime[] dtChildrenDateOfBirth = new DateTime[queryResultChildrenInfo.size];
                //    StringBuilder[] sbChildrenStartDate = new StringBuilder[queryResultChildrenInfo.size];
                //    DateTime[] dtChildrenStartDate = new DateTime[queryResultChildrenInfo.size];

                //    lblChildrenName.Visible = true;
                //    lblChildrenDateOfBirth.Visible = true;
                //    lblChildrenStartDate.Visible = true;

                //    for (int i = 0; i < queryResultChildrenInfo.size; i++)
                //    {
                //        if (child[i] != null)
                //        {

                //            sbChildName[i] = new StringBuilder();

                //            sbChildName[i].Append(child[i].LastName);
                //            sbChildName[i].Append(", ");
                //            sbChildName[i].Append(child[i].FirstName);

                //            dtChildrenDateOfBirth[i] = new DateTime((int)child[i].Birthdate.Value.Year, (int)child[i].Birthdate.Value.Month, (int)child[i].Birthdate.Value.Day);
                //            sbChildrenDateOfBirth[i] = new StringBuilder();
                //            sbChildrenDateOfBirth[i].Append(dtChildrenDateOfBirth[i].ToShortDateString());

                //            dtChildrenStartDate[i] = new DateTime((int)child[i].c4g_Membership_Start_Date__c.Value.Year,
                //                                                    (int)child[i].c4g_Membership_Start_Date__c.Value.Month,
                //                                                    (int)child[i].c4g_Membership_Start_Date__c.Value.Day);
                //            sbChildrenStartDate[i] = new StringBuilder();
                //            sbChildrenStartDate[i].Append(dtChildrenStartDate[i].ToShortDateString());
                //        }

                //        switch (i)
                //        {
                //            case 0:
                //                lblChildName1.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName1.Visible = true;
                //                lblChildDateOfBirth1.Visible = true;
                //                lblChildStartDate1.Visible = true;
                //                break;
                //            case 1:
                //                lblChildName2.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName2.Visible = true;
                //                lblChildDateOfBirth2.Visible = true;
                //                lblChildStartDate2.Visible = true;
                //                break;
                //            case 2:
                //                lblChildName3.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName3.Visible = true;
                //                lblChildDateOfBirth3.Visible = true;
                //                lblChildStartDate3.Visible = true;
                //                break;
                //            case 3:
                //                lblChildName4.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName4.Visible = true;
                //                lblChildDateOfBirth4.Visible = true;
                //                lblChildStartDate4.Visible = true;
                //                break;
                //            case 4:
                //                lblChildName5.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName5.Visible = true;
                //                lblChildDateOfBirth5.Visible = true;
                //                lblChildStartDate5.Visible = true;
                //                break;
                //            case 5:
                //                lblChildName6.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName6.Visible = true;
                //                lblChildDateOfBirth6.Visible = true;
                //                lblChildStartDate6.Visible = true;
                //                break;
                //            case 6:
                //                lblChildName7.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName7.Visible = true;
                //                lblChildDateOfBirth7.Visible = true;
                //                lblChildStartDate7.Visible = true;
                //                break;
                //            case 7:
                //                lblChildName8.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName8.Visible = true;
                //                lblChildDateOfBirth8.Visible = true;
                //                lblChildStartDate8.Visible = true;
                //                break;

                //            case 8:
                //                lblChildName9.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName9.Visible = true;
                //                lblChildDateOfBirth9.Visible = true;
                //                lblChildStartDate9.Visible = true;
                //                break;

                //            case 9:
                //                lblChildName10.Text = sbChildName[i].ToString();
                //                lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                //                lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                //                lblChildName10.Visible = true;
                //                lblChildDateOfBirth10.Visible = true;
                //                lblChildStartDate10.Visible = true;
                //                break;
                //            default:
                //                break;
                //        }
                //    }
                //}
                //else
                //{
                //    lblChildName1.Visible = true;
                //    lblChildName1.Width = 300;
                //    lblChildName1.Text = "No child has been added";
                //}
            }




            ///// End of family information
            ////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////

            queryResultPaymentInfo = Sfdcbinding.query(strQueryPaymentInfoInMembership);

            if (queryResultPaymentInfo.size > 0)
            {
                //paymentInfo = (SForce.causeview__Payment__c)queryResultPaymentInfo.records[0];
                //paymentInfoInMembership = (SForce.Membership__c)queryResultPaymentInfo.records[0];
                ctPaymentInfoInMembership = (SForce.Contact)queryResultPaymentInfo.records[0];

                if (ctPaymentInfoInMembership.c4g_Membership__r != null)
                {
                    if (ctPaymentInfoInMembership.c4g_Membership__r.Payment_Method__c == "Check") rbCheck.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.Payment_Method__c == "ACH/PAD") rbBankACH.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.Payment_Method__c == "Credit Card") rbCreditCard.Checked = true;

                    if (ctPaymentInfoInMembership.c4g_Membership__r.Payment_Frequency__c == "Recurring") rbRecurring.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.Payment_Frequency__c == "One Time Gift") rbOneTime.Checked = true;
                }
            }



            //    if (paymentInfo.causeview__Payment_Type__c == "Check") rbCheck.Checked = true;
            //    if (paymentInfo.causeview__Payment_Type__c == "ACH/PAD") rbBankACH.Checked = true;
            //    if (paymentInfo.causeview__Payment_Type__c == "Credit Card") rbCreditCard.Checked = true;

            //    if (paymentInfo.causeview__Donation__r != null)
            //    {
            //        if (paymentInfo.causeview__Donation__r.causeview__Gift_Type__c == "Recurring") rbRecurring.Checked = true;
            //        if (paymentInfo.causeview__Donation__r.causeview__Gift_Type__c == "One Time Gift") rbOneTime.Checked = true;
            //    }
            //}

            //String strQueryMemberAddress = "select MailingAddress, OtherAddress from Contact where Email = 'seanjones@emailz.com'";
            String strQueryMemberAddress = "select MailingAddress, OtherAddress from Contact where Id = '" + strContactId + "'";
            SForce.QueryResult qrMemberAddress = Sfdcbinding.query(strQueryMemberAddress);

            if (qrMemberAddress.size > 0)
            {
                SForce.Contact ctMemeberAddresses = (SForce.Contact)qrMemberAddress.records[0];

                if (ctMemeberAddresses.OtherAddress != null)
                {
                    chkBillingAddress.Checked = true;

                    ////////////////////////////////////////////

                    txtBillingStreet.Text = ctMemeberAddresses.OtherAddress.street;
                    hdnBillingStreetAddress.Value = ctMemeberAddresses.OtherAddress.street;

                    txtBillingZipCode.Text = ctMemeberAddresses.OtherAddress.postalCode;
                    hdnBillingZipCode.Value = ctMemeberAddresses.OtherAddress.postalCode;

                    txtBillingState.Text = ctMemeberAddresses.OtherAddress.state;
                    hdnBillingState.Value = ctMemeberAddresses.OtherAddress.state;

                    txtBillingCity.Text = ctMemeberAddresses.OtherAddress.city;
                    hdnBillingCity.Value = ctMemeberAddresses.OtherAddress.city;

                    pnlBillingAddress.Visible = true;
                    pnlBillingStreet.Visible = true;
                    pnlBillingZipCode.Visible = true;
                    pnlBillingState.Visible = true;
                    pnlBillingCity.Visible = true;
                    pnlBillingSaveButton.Visible = true;

                    lblBillingStreet.Visible = true;
                    lblBillingZipCode.Visible = true;
                    lblBillingState.Visible = true;
                    lblBillingCity.Visible = true;

                    txtBillingStreet.Visible = true;
                    txtBillingZipCode.Visible = true;
                    txtBillingState.Visible = true;
                    txtBillingCity.Visible = true;
                    btnBillingAddressSave.Text = "Update";
                    btnBillingAddressSave.Visible = true;

                }
            }

            if (ctMembershipInfo.c4g_Membership__r != null)
            {
                if (ctMembershipInfo.c4g_Membership__r.Owner != null)
                {
                    if (ctMembershipInfo.c4g_Membership__r.Owner.Name1 != null)
                    {
                        ddlReferredBy.Items.Add(new ListItem(ctMembershipInfo.c4g_Membership__r.Owner.Name1.ToString()));
                        ddlReferredBy.SelectedIndex = 0;
                    }
                }

                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Email")
                {
                    chkEmail.Checked = true;
                    chkPostal.Checked = false;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Postal")
                {
                    chkEmail.Checked = false;
                    chkPostal.Checked = true;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Both")
                {
                    chkEmail.Checked = true;
                    chkPostal.Checked = true;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Neither")
                {
                    chkEmail.Checked = false;
                    chkPostal.Checked = false;
                }
            }


            if (queryResultContactForMichaelJordan.size > 0)
            {
                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                if (ctMichaelJordan.causeview__Solicit_Codes__c != null)
                {
                    if (ctMichaelJordan.causeview__Solicit_Codes__c.Contains("Allow Postal Mail"))
                    {
                        rbYesJoinMailing.Checked = true;
                        rbNoJoinMailing.Checked = false;
                    }
                    else
                    {
                        rbYesJoinMailing.Checked = false;
                        rbNoJoinMailing.Checked = true;
                    }

                    if (ctMichaelJordan.causeview__Solicit_Codes__c.Contains("Allow SMS Messages"))
                    {
                        rbYesAllowMessages.Checked = true;
                        rbNoAllowMessages.Checked = false;
                    }
                    else
                    {
                        rbYesAllowMessages.Checked = false;
                        rbNoAllowMessages.Checked = true;
                    }

                    if (ctMichaelJordan.causeview__Solicit_Codes__c.Contains("No Communication of Any Kind"))
                    {
                        rbYesJoinMailing.Checked = false;
                        rbNoJoinMailing.Checked = true;
                        rbYesAllowMessages.Checked = false;
                        rbNoAllowMessages.Checked = true;
                    }
                }
            }

            //lblPersonalInfoUpdateMessage.Text = Session["EmailConfirmed"].ToString();
        }

        protected void chkBillingAddress_CheckedChanged(object sender, EventArgs e)
        {

            if (chkBillingAddress.Checked)
            {
                lblBillingStreet.Visible = true;
                lblBillingZipCode.Visible = true;
                lblBillingState.Visible = true;
                lblBillingCity.Visible = true;

                txtBillingStreet.Visible = true;
                txtBillingZipCode.Visible = true;
                txtBillingState.Visible = true;
                txtBillingCity.Visible = true;
                btnBillingAddressSave.Visible = true;

                pnlBillingStreet.Visible = true;
                pnlBillingZipCode.Visible = true;
                pnlBillingState.Visible = true;
                pnlBillingCity.Visible = true;
                pnlBillingSaveButton.Visible = true;
                pnlBillingAddress.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
            }
            else if (!chkBillingAddress.Checked)
            {

                if (txtBillingStreet.Text != String.Empty &&
                    txtBillingZipCode.Text != String.Empty &&
                    txtBillingState.Text != String.Empty &&
                    txtBillingCity.Text != String.Empty)
                {

                    String strHousehold_Id = (String)Session["ContactId"];

                    SForce.Contact ctDeleteBillingAddress = new SForce.Contact();

                    ctDeleteBillingAddress.Id = strHousehold_Id;
                    ctDeleteBillingAddress.OtherStreet = " ";
                    ctDeleteBillingAddress.OtherPostalCode = " ";
                    ctDeleteBillingAddress.OtherState = " ";
                    ctDeleteBillingAddress.OtherCity = " ";

                    InitializedSfdcbinding();

                    SForce.SaveResult[] srDelete = Sfdcbinding.update(new SForce.sObject[] { ctDeleteBillingAddress });

                    if (srDelete[0].success)
                    {

                        txtBillingStreet.Text = "";
                        txtBillingZipCode.Text = "";
                        txtBillingState.Text = "";
                        txtBillingCity.Text = "";

                        lblBillingStreet.Visible = false;
                        lblBillingZipCode.Visible = false;
                        lblBillingState.Visible = false;
                        lblBillingCity.Visible = false;

                        txtBillingStreet.Visible = false;
                        txtBillingZipCode.Visible = false;
                        txtBillingState.Visible = false;
                        txtBillingCity.Visible = false;
                        btnBillingAddressSave.Visible = false;

                        pnlBillingStreet.Visible = false;
                        pnlBillingZipCode.Visible = false;
                        pnlBillingState.Visible = false;
                        pnlBillingCity.Visible = false;
                        pnlBillingSaveButton.Visible = false;
                        pnlBillingAddress.Visible = false;

                        chkBillingAddress.Checked = false;

                        mpeBillingAddressDeletionConfirmation.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
                    }
                    else
                    {
                        mpeBillingAddressDeletionFailed.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
                    }
                }
                else
                {
                    lblBillingStreet.Visible = false;
                    lblBillingZipCode.Visible = false;
                    lblBillingState.Visible = false;
                    lblBillingCity.Visible = false;

                    txtBillingStreet.Text = String.Empty;
                    txtBillingZipCode.Text = String.Empty;
                    txtBillingState.Text = String.Empty;
                    txtBillingCity.Text = String.Empty;

                    txtBillingStreet.Visible = false;
                    txtBillingZipCode.Visible = false;
                    txtBillingState.Visible = false;
                    txtBillingCity.Visible = false;
                    btnBillingAddressSave.Visible = false;

                    pnlBillingStreet.Visible = false;
                    pnlBillingZipCode.Visible = false;
                    pnlBillingState.Visible = false;
                    pnlBillingCity.Visible = false;
                    pnlBillingSaveButton.Visible = false;
                    pnlBillingAddress.Visible = false;

                    chkBillingAddress.Checked = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);

                }
            }
            RestoreAllTextBoxStyle();
        }

        protected void InitializeTextBoxStyle(TextBox txtObject, HiddenField hdnBorderWidth, HiddenField hdnBorderColor, HiddenField hdnFontColor)
        {
            hdnBorderWidth.Value = "1";
            hdnBorderColor.Value = "black";
            hdnFontColor.Value = "black";

            txtObject.BorderWidth = 1;
            txtObject.BorderColor = Color.Black;
            txtObject.ForeColor = Color.Black;
        }

        protected void btnBillingAddressSave_Click(object sender, EventArgs e)
        {

            String strContact_Id = (String)Session["ContactId"];

            SForce.Contact ctUpdate = new SForce.Contact();

            ctUpdate.Id = strContact_Id;
            ctUpdate.OtherStreet = txtBillingStreet.Text;
            ctUpdate.OtherPostalCode = txtBillingZipCode.Text;
            ctUpdate.OtherState = txtBillingState.Text;
            ctUpdate.OtherCity = txtBillingCity.Text;

            InitializedSfdcbinding();

            SForce.SaveResult[] saveResults = Sfdcbinding.update(new SForce.sObject[] { ctUpdate });

            if (saveResults[0].success)
            {
                btnBillingAddressSave.Text = "Update";
                mpeBillingAddressSaveConfirmation.Show();

            }
            else
            {
                mpeBillingAddressSaveFailure.Show();
            }
        }

        protected void txtBillingZipCode_TextChanged(object sender, EventArgs e)
        {
            if (txtBillingZipCode.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtBillingZipCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtBillingState.Text = dr["state_code"].ToString();
                    txtBillingCity.Text = dr["name"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", btnBillingAddressSave.ClientID), true);
                    InitializeTextBoxStyle(txtBillingState, hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);
                    InitializeTextBoxStyle(txtBillingCity, hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);
                }
                else
                {
                    txtBillingState.Text = "";
                    txtBillingCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void btnBillingZipCodeHidden_Click(object sender, EventArgs e)
        {
            if (rfvBillingZipCode.IsValid && revBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Black;
                txtBillingZipCode.ForeColor = Color.Black;
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
            }
            if (!rfvBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Red;
                txtBillingZipCode.ForeColor = Color.Red;
                txtBillingZipCode.Text = "Zip code required!";
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
            }
            if (!revBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Red;
                txtBillingZipCode.ForeColor = Color.Red;
                txtBillingZipCode.Text = "Invalid zip code!";
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);

            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txtZip = (TextBox)sender;

            if (txtZip.Text.Length == 5)
            {
                //MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtZip.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtState.Text = dr["state_code"].ToString();
                    txtCity.Text = dr["name"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchName.ClientID), true);
                    InitializeTextBoxStyle(txtState, hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);
                    InitializeTextBoxStyle(txtCity, hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);
                }
                else
                {
                    txtState.Text = "";
                    txtCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void RestoreTextBoxStyle(TextBox txtObject, HiddenField borderWidth, HiddenField borderColor, HiddenField fontColor)
        {
            txtObject.BorderWidth = Int32.Parse(borderWidth.Value);

            switch (borderColor.Value)
            {
                case "red":
                    txtObject.BorderColor = Color.Red;
                    break;
                case "black":
                    txtObject.BorderColor = Color.Black;
                    break;
            }

            switch (fontColor.Value)
            {
                case "red":
                    txtObject.ForeColor = Color.Red;
                    break;
                case "black":
                    txtObject.ForeColor = Color.Black;
                    break;
            }
        }

        protected void RestoreAllTextBoxStyle()
        {
            // Restore text box style after postback of Zip code lookup
            RestoreTextBoxStyle(txtEmail, hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);
            RestoreTextBoxStyle(txtLastName, hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);
            RestoreTextBoxStyle(txtFirstName, hdnFirstNameBorderWidth, hdnFirstNameBorderColor, hdnFirstNameFontColor);
            RestoreTextBoxStyle(txtTelephone1, hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);
            RestoreTextBoxStyle(txtTelephone2, hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);
            RestoreTextBoxStyle(txtTelephone3, hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);
            RestoreTextBoxStyle(txtAddress, hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor);
            RestoreTextBoxStyle(txtZipCode, hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);
            RestoreTextBoxStyle(txtState, hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);
            RestoreTextBoxStyle(txtCity, hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);
            RestoreTextBoxStyle(txtChurchName, hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);
            RestoreTextBoxStyle(txtSeniorPastor, hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);
            RestoreTextBoxStyle(txtChurchStreet, hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);
            RestoreTextBoxStyle(txtChurchZip, hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);
            RestoreTextBoxStyle(txtChurchState, hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);
            RestoreTextBoxStyle(txtChurchCity, hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);
            RestoreTextBoxStyle(txtChurchTelephone, hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);
            RestoreTextBoxStyle(txtBillingStreet, hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);
            RestoreTextBoxStyle(txtBillingZipCode, hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);
            RestoreTextBoxStyle(txtBillingState, hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);
            RestoreTextBoxStyle(txtBillingCity, hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);
        }

        protected void btnZipCodeHidden_Click(object sender, EventArgs e)
        {

            if (rfvZipCode.IsValid && revZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Black;
                txtZipCode.ForeColor = Color.Black;
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);

            }
            if (!rfvZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Red;
                txtZipCode.ForeColor = Color.Red;
                txtZipCode.Text = "Zip code required!";
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
            }
            if (!revZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Red;
                txtZipCode.ForeColor = Color.Red;
                txtZipCode.Text = "Invalid zip code!";
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
            }
        }

        protected void txtChurchZipCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txtZip = (TextBox)sender;

            if (txtZip.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtZip.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtChurchState.Text = dr["state_code"].ToString();
                    txtChurchCity.Text = dr["name"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchTelephone.ClientID), true);
                    InitializeTextBoxStyle(txtChurchState, hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);
                    InitializeTextBoxStyle(txtChurchCity, hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);
                }
                else
                {
                    txtChurchState.Text = "";
                    txtChurchCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void btnChurchZipCodeHidden_Click(object sender, EventArgs e)
        {
            if (rfvChurchZip.IsValid && revChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Black;
                txtChurchZip.BorderColor = Color.Black;
                txtChurchZip.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
            }
            if (!rfvChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Red;
                txtChurchZip.BorderColor = Color.Red;
                txtChurchZip.Height = 25;

                txtChurchZip.Text = "Zip code required!";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
            }
            if (!revChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Red;
                txtChurchZip.BorderColor = Color.Red;
                txtChurchZip.Height = 25;

                txtChurchZip.Text = "Invalid zip code!";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);

            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    // change this code to client side code
        //    //LoadPersonalInfo();
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                String strContactId = (String)Session["ContactId"];
                StringBuilder sbErrorMessage = new StringBuilder();

                SForce.Contact ctPersonalInfo = new SForce.Contact();

                ctPersonalInfo.Id = strContactId;
                ctPersonalInfo.Email = txtEmail.Text;
                ctPersonalInfo.LastName = txtLastName.Text;
                ctPersonalInfo.FirstName = txtFirstName.Text;
                ctPersonalInfo.MiddleName = txtMiddleName.Text;

                ctPersonalInfo.Phone = txtTelephone1.Text;
                ctPersonalInfo.MobilePhone = txtTelephone2.Text;
                ctPersonalInfo.OtherPhone = txtTelephone3.Text;

                ctPersonalInfo.MailingStreet = txtAddress.Text;
                ctPersonalInfo.MailingCity = txtCity.Text;
                ctPersonalInfo.MailingState = txtState.Text;
                ctPersonalInfo.MailingPostalCode = txtZipCode.Text;

                InitializedSfdcbinding();

                SForce.SaveResult[] srPersonalInfo = Sfdcbinding.update(new SForce.sObject[] { ctPersonalInfo });

                if (srPersonalInfo == null)
                {
                    sbErrorMessage.Append("Personal Information udpate failed<br />");
                }
                else
                {
                    if (srPersonalInfo[0].success)
                    {
                        lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                    }
                    else
                    {
                        lblPersonalInfoUpdateMessage.Text = srPersonalInfo[0].errors[0].message;
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////

                //InitializedSfdcbinding();

                //string strQueryForChurchId = "select c4g_Church__r.Id from Contact where Email = 'seanjones@emailz.com'";
                string strQueryForChurchId = "select c4g_Church__r.Id from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrChurchId = Sfdcbinding.query(strQueryForChurchId);
                String strChurchId = String.Empty;

                SForce.SaveResult[] srChurchInfo = null;

                if (qrChurchId.size > 0)
                {
                    SForce.Contact ctChurchId = (SForce.Contact)qrChurchId.records[0];
                    if (ctChurchId.c4g_Church__r != null)
                    {
                        //strChurchId = ctChurchId.c4g_Church__r.Id;

                        SForce.Account acctChurchInfo = new SForce.Account();

                        
                        
                        //acctChurchInfo.Id = strChurchId;
                        acctChurchInfo.Id = ctChurchId.c4g_Church__r.Id;
                        acctChurchInfo.Name = txtChurchName.Text;
                        acctChurchInfo.Senior_Pastor_s_Name__c = txtSeniorPastor.Text;

                        acctChurchInfo.ShippingStreet = txtChurchStreet.Text;
                        acctChurchInfo.ShippingPostalCode = txtChurchZip.Text;
                        acctChurchInfo.ShippingState = txtChurchState.Text;
                        acctChurchInfo.ShippingCity = txtChurchCity.Text;
                        acctChurchInfo.Phone = txtChurchTelephone.Text;

                        srChurchInfo = Sfdcbinding.update(new SForce.sObject[] { acctChurchInfo });

                        if (srChurchInfo[0].success)
                        {
                            lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                        }
                        else
                        {
                            lblPersonalInfoUpdateMessage.Text = srChurchInfo[0].errors[0].message;
                        }
                    }
                }
                else sbErrorMessage.Append("Church information update failed<br />");


                // update payment information; payment method and payment frequency
                // payment information is in membership object
                ///////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryPaymentId = "select Id from causeview__Payment__c " +
                //                           "where causeview__Donation__r.causeview__Constituent__r.Email = 'seanjones@emailz.com'";

                //string strQueryPaymentId = "select Id from causeview__Payment__c " +
                //                           "where causeview__Donation__r.causeview__Constituent__r.Id = '" + strContactId + "'";

                string strQueryMembershipId = "select c4g_Membership__r.Id from Contact " +
                                              "where Id = '" + strContactId + "'";

                SForce.QueryResult qrMembershipInfo = Sfdcbinding.query(strQueryMembershipId);
                //SForce.causeview__Payment__c paymentId = null;
                //string strPaymentId = String.Empty;
                
                SForce.SaveResult[] srMembershipInfo = null;
                string strMembershipId = String.Empty;

                //if (qrPaymentInfoId.size > 0)
                if (qrMembershipInfo.size > 0)
                {
                    //paymentId = (SForce.causeview__Payment__c)qrPaymentInfoId.records[0];
                    //strPaymentId = paymentId.Id;
                    //SForce.Membership__c membershipInfo = (SForce.Membership__c)qrMembershipInfo.records[0];
                    SForce.Contact contact = (SForce.Contact)qrMembershipInfo.records[0];

                    //strMembershipId = membershipInfo.Id;

                    SForce.Membership__c updateMembershipInfo = new SForce.Membership__c();

                    updateMembershipInfo.Id = contact.c4g_Membership__r.Id;
                    if (rbCheck.Checked) updateMembershipInfo.Payment_Method__c = "Check";
                    if (rbBankACH.Checked) updateMembershipInfo.Payment_Method__c = "ACH/PAD";
                    if (rbCreditCard.Checked) updateMembershipInfo.Payment_Method__c = "Credit Card";

                    if (rbRecurring.Checked) updateMembershipInfo.Payment_Frequency__c = "Recurring";
                    if (rbOneTime.Checked) updateMembershipInfo.Payment_Frequency__c = "One Time";

                    if (chkEmail.Checked && chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Both";
                    if (chkEmail.Checked && !chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Email";
                    if (!chkEmail.Checked && chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Postal";
                    if (!chkEmail.Checked && !chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Neither";

                    srMembershipInfo = Sfdcbinding.update(new SForce.sObject[] { updateMembershipInfo });
                    if (srMembershipInfo[0].success)
                    {
                        lblPersonalInfoUpdateMessage.Text = "Update succeeded";
                    }
                    else
                    {
                        lblPersonalInfoUpdateMessage.Text = srMembershipInfo[0].errors[0].message;
                    }
                   

                    //SForce.causeview__Payment__c updatePaymentInfo = new SForce.causeview__Payment__c();

                    //updatePaymentInfo.Id = strPaymentId;
                    //updatePaymentInfo.Id = paymentId.Id;
                    //if (rbCheck.Checked) updatePaymentInfo.causeview__Payment_Type__c = "Check";
                    //if (rbBankACH.Checked) updatePaymentInfo.causeview__Payment_Type__c = "ACH/PAD";
                    //if (rbCreditCard.Checked) updatePaymentInfo.causeview__Payment_Type__c = "Credit Card";

                    //srPaymentInfo = Sfdcbinding.update(new SForce.sObject[] { updatePaymentInfo });
                    //if (srPaymentInfo[0].success)
                    //{
                    //    lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                    //}
                    //else
                    //{
                    //    lblPersonalInfoUpdateMessage.Text = srPaymentInfo[0].errors[0].message;
                    //}
                }
                //else sbErrorMessage.Append("No payment information to update<br />");


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForTransactionId = "select Id from causeview__Gift__c where causeview__Constituent__r.Email = 'seanjones@emailz.com'";
                //string strQueryForTransactionId = "select Id from causeview__Gift__c where causeview__Constituent__r.Id = '" + strContactId + "'";

                //SForce.QueryResult qrGiftFrequency = Sfdcbinding.query(strQueryForTransactionId);
                //SForce.causeview__Gift__c frequencyId = null;
                //string strFrequencyId = String.Empty;
                //SForce.SaveResult[] srFrequencyInfo = null;

                //if (qrGiftFrequency.size > 0)
                //{
                //    frequencyId = (SForce.causeview__Gift__c)qrGiftFrequency.records[0];
                //    //strFrequencyId = frequencyId.Id;

                //    SForce.causeview__Gift__c updateFrequencyInfo = new SForce.causeview__Gift__c();
                //    //updateFrequencyInfo.Id = strFrequencyId;
                //    updateFrequencyInfo.Id = frequencyId.Id;
                //    if (rbRecurring.Checked) updateFrequencyInfo.causeview__Gift_Type__c = "Recurring";
                //    if (rbOneTime.Checked) updateFrequencyInfo.causeview__Gift_Type__c = "One Time Gift";

                //    srFrequencyInfo = Sfdcbinding.update(new SForce.sObject[] { updateFrequencyInfo });
                //    if (srFrequencyInfo[0].success)
                //    {
                //        lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                //    }
                //    else
                //    {
                //        lblPersonalInfoUpdateMessage.Text = srFrequencyInfo[0].errors[0].message;
                //    }
                //}
                //else sbErrorMessage.Append("No gift frequency information to update<br />");

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForInvoiceDelivery = "select Id from Membership__c where Paying_Member__r.Email = 'seanjones@emailz.com'";
                //string strQueryForInvoiceDelivery = "select Id from Membership__c where Paying_Member__r.Id = '" + strContactId + "'";

                //SForce.QueryResult qrInvoiceDelivery = Sfdcbinding.query(strQueryForInvoiceDelivery);
                //SForce.Membership__c memId = null;
                //SForce.SaveResult[] srInvoiceDelivery = null;

                //if (qrInvoiceDelivery.size > 0)
                //{
                //    memId = (SForce.Membership__c)qrInvoiceDelivery.records[0];
                //    SForce.Membership__c memInvoiceDelivery = new SForce.Membership__c();

                //    memInvoiceDelivery.Id = memId.Id;
                //    if (chkEmail.Checked && chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Both";
                //    if (chkEmail.Checked && !chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Email";
                //    if (!chkEmail.Checked && chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Postal";
                //    if (!chkEmail.Checked && !chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Neither";

                //    srInvoiceDelivery = Sfdcbinding.update(new SForce.sObject[] { memInvoiceDelivery });

                //    if (srInvoiceDelivery[0].success)
                //    {
                //        lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                //    }
                //    else
                //    {
                //        lblPersonalInfoUpdateMessage.Text = srInvoiceDelivery[0].errors[0].message;
                //    }
                //}
                //else sbErrorMessage.Append("Invoice delivery information update failed<br />");


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForSolicitCode = "select id, causeview__Solicit_Codes__c from Contact where Email = 'seanjones@emailz.com'";
                string strQueryForSolicitCode = "select id, causeview__Solicit_Codes__c from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrSolicitCode = Sfdcbinding.query(strQueryForSolicitCode);
                SForce.Contact ctContactSolicitCode = null;
                SForce.SaveResult[] srSolicitCode = null;

                if (qrSolicitCode.size > 0)
                {
                    ctContactSolicitCode = (SForce.Contact)qrSolicitCode.records[0];

                    SForce.Contact ctSolicitCode = new SForce.Contact();

                    ctSolicitCode.Id = ctContactSolicitCode.Id;
                    ctSolicitCode.causeview__Solicit_Codes__c = ctContactSolicitCode.causeview__Solicit_Codes__c;
                    lblPaymentInfoUpdateMessage.Text = ctContactSolicitCode.causeview__Solicit_Codes__c;

                    if (ctSolicitCode.causeview__Solicit_Codes__c != null)
                    //if (ctContactSolicitCode.causeview__Solicit_Codes__c != null)
                    {
                        if (rbYesJoinMailing.Checked && !rbNoJoinMailing.Checked)
                        {
                            if (!ctSolicitCode.causeview__Solicit_Codes__c.Contains("Allow Postal Mail"))
                                ctSolicitCode.causeview__Solicit_Codes__c = String.Concat(ctSolicitCode.causeview__Solicit_Codes__c, "; Allow Postal Mail");
                        }
                        if (!rbYesJoinMailing.Checked && rbNoJoinMailing.Checked)
                        {
                            if (ctSolicitCode.causeview__Solicit_Codes__c.Contains("Allow Postal Mail"))
                                ctSolicitCode.causeview__Solicit_Codes__c = ctSolicitCode.causeview__Solicit_Codes__c.Replace("Allow Postal Mail", "");
                        }
                        if (rbYesAllowMessages.Checked && !rbNoAllowMessages.Checked)
                        {
                            if (!ctSolicitCode.causeview__Solicit_Codes__c.Contains("Allow SMS Messages"))
                                ctSolicitCode.causeview__Solicit_Codes__c = String.Concat(ctSolicitCode.causeview__Solicit_Codes__c, "; Allow SMS Messages");
                        }
                        if (!rbYesAllowMessages.Checked && rbNoAllowMessages.Checked)
                        {
                            if (ctSolicitCode.causeview__Solicit_Codes__c.Contains("Allow SMS Messages"))
                                ctSolicitCode.causeview__Solicit_Codes__c = ctSolicitCode.causeview__Solicit_Codes__c.Replace("Allow SMS Messages", "");
                        }

                        srSolicitCode = Sfdcbinding.update(new SForce.sObject[] { ctSolicitCode });

                        if (srSolicitCode[0].success)
                        {
                            lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                        }
                        else
                        {
                            lblPersonalInfoUpdateMessage.Text = srSolicitCode[0].errors[0].message;
                        }
                    }
                }
                else sbErrorMessage.Append("Solicitcode update failed");

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                // Show whether or not the update is successful
                if (srPersonalInfo[0].success &&
                    srChurchInfo[0].success &&
                    srMembershipInfo[0].success &
                    srSolicitCode[0].success)
                {
                    mpeSucceeded.Show();
                }
                else
                {
                    mpeFailed.Show();
                }




                //if (srPersonalInfo != null &&
                //    srChurchInfo != null &&
                //    srPaymentInfo != null &&
                //    srFrequencyInfo != null &&
                //    srInvoiceDelivery != null &&
                //    srSolicitCode != null)
                //{
                //    if (srPersonalInfo[0].success &&
                //        srChurchInfo[0].success &&
                //        srPaymentInfo[0].success &&
                //        srFrequencyInfo[0].success &&
                //        srInvoiceDelivery[0].success &&
                //        srSolicitCode[0].success)
                //    {
                //        mpeSucceeded.Show();
                //    }
                //    else
                //    {
                //        mpeFailed.Show();
                //    }
                //}
                //else
                //{
                //    lblPartialUpdateFailureMessage.Text = sbErrorMessage.ToString();
                //    mpePartialUpdateFailure.Show();
                //}
            }
            else if (!Page.IsValid)
            {
                StringBuilder sbControlsFailedValidation = new StringBuilder();

                if (!rfvMemberEmail.IsValid) sbControlsFailedValidation.Append("Member email is required.<br />");
                if (!revMemberEmail.IsValid && (txtEmail.Text != rfvMemberEmail.InitialValue)) sbControlsFailedValidation.Append("Member email is invalid.<br />");

                if (!rfvLastName.IsValid) sbControlsFailedValidation.Append("Member last name is required.<br />");
                if (!rfvFirstName.IsValid) sbControlsFailedValidation.Append("Member first name is required.<br />");

                if (!rfvPhone.IsValid) sbControlsFailedValidation.Append("Phone number is required.<br />");
                if (!revPhone.IsValid && (txtTelephone1.Text != rfvPhone.InitialValue)) sbControlsFailedValidation.Append("Phone number is invalid.<br />");

                if (!revMobilePhone.IsValid) sbControlsFailedValidation.Append("Mobile phone number is invalid.<br />");
                if (!revOtherPhone.IsValid) sbControlsFailedValidation.Append("Other phone number is invalid.<br />");

                if (!rfvStreetAddress.IsValid) sbControlsFailedValidation.Append("Street address is required.<br />");
                if (!rfvZipCode.IsValid) sbControlsFailedValidation.Append("Zip code is required.<br />");
                if (!revZipCode.IsValid && (txtZipCode.Text != rfvZipCode.InitialValue)) sbControlsFailedValidation.Append("Zip code is invalid.<br />");
                if (!rfvState.IsValid) sbControlsFailedValidation.Append("State is required.<br />");
                if (!rfvCity.IsValid) sbControlsFailedValidation.Append("City is required.<br />");

                if (!rfvChurchName.IsValid) sbControlsFailedValidation.Append("Church name is required.<br />");
                if (!rfvSeniorPastor.IsValid) sbControlsFailedValidation.Append("Pastor name is required.<br />");

                if (!rfvChurchStreet.IsValid) sbControlsFailedValidation.Append("Church address is required.<br />");
                if (!rfvChurchZip.IsValid) sbControlsFailedValidation.Append("Church zip code is required.<br />");
                if (!revChurchZip.IsValid && (txtChurchZip.Text != rfvChurchZip.InitialValue)) sbControlsFailedValidation.Append("Church zip code is invalid.<br />");
                if (!rfvChurchState.IsValid) sbControlsFailedValidation.Append("Church state is required.<br />");
                if (!rfvChurchCity.IsValid) sbControlsFailedValidation.Append("Church city is required.<br />");
                if (!rfvChurchPhone.IsValid) sbControlsFailedValidation.Append("Church phone number is required.<br />");

                if (!rfvBillingStreet.IsValid) sbControlsFailedValidation.Append("Billing address is required.<br />");
                if (!rfvBillingZipCode.IsValid) sbControlsFailedValidation.Append("Billing zip code is required.<br />");
                if (!revBillingZipCode.IsValid && (txtBillingZipCode.Text != rfvBillingZipCode.InitialValue)) sbControlsFailedValidation.Append("Billing zip code is invalid.<br />");
                if (!rfvBillingState.IsValid) sbControlsFailedValidation.Append("Billing state is required.<br />");
                if (!rfvBillingCity.IsValid) sbControlsFailedValidation.Append("Billing city is required.<br />");

                lblPageValidationFailure.Text = sbControlsFailedValidation.ToString();

                mpePageValidationFailed.Show();

                RestoreAllTextBoxStyle();
            }
        }

        protected void btnModalOk_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.ID == "btnModalOk") mpeSucceeded.Hide();
            if (btn.ID == "btnModalFailure") mpeFailed.Hide();
        }

        protected void btnBillingAddressSaveConfirmation_Click(object sender, EventArgs e)
        {
            mpeBillingAddressSaveConfirmation.Hide();
        }

        protected void btnBillingAddrSavingFailure_Click(object sender, EventArgs e)
        {
            mpeBillingAddressSaveFailure.Hide();
        }

        protected void btnBillingAddrDeletionConfirmation_Click(object sender, EventArgs e)
        {
            mpeBillingAddressDeletionConfirmation.Hide();
        }

        protected void btnBillingAddrDeletionFailed_Click(object sender, EventArgs e)
        {
            mpeBillingAddressDeletionFailed.Hide();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Response.Redirect("~/Account/Login.aspx");
        }
    }
}