using System;

using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

namespace SalesForceWebApp
{
    public partial class MembershipDetails : System.Web.UI.Page
    {

        protected String strAccountId = String.Empty;
        protected String strPrimaryContactId = String.Empty;
        protected String strSpouseId = String.Empty;
        protected String strEmail = String.Empty;
        protected List<String> lstChildId = new List<string>();

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;
        protected SForce.SaveResult[] saveResults = null;

        /// <summary>
        /// SQL statements
        /// </summary>
        protected string strQueryForSpouse = null;
        protected string strQueryForChildren = null;

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            InitializedSfdcbinding();
            InitializeSQLQueryForSpouse();
            InitializeSQLQueryForChildren();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //InitializedSfdcbinding();

            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];

            if ((String)Session["Email"] != null) strEmail = (String)Session["Email"];

            if ((String)Session["ContactId"] != null) strPrimaryContactId = (String)Session["ContactId"];
            else
            {
                String strQueryForPrimaryContactId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                                     "and cmm_Household_Role__c = 'Head of Household'";

                SForce.QueryResult qrPrimaryContactId = Sfdcbinding.query(strQueryForPrimaryContactId);

                if (qrPrimaryContactId.size > 0)
                {
                    SForce.Contact ctPrimaryContactId = qrPrimaryContactId.records[0] as SForce.Contact;

                    strPrimaryContactId = ctPrimaryContactId.Id;
                }
            }
            if ((String)Session["SpouseId"] != null) strSpouseId = (String)Session["SpouseId"];
            else
            {
                String strQueryForSpouseId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                             "and cmm_Household_Role__c = 'Spouse'";

                SForce.QueryResult qrSpouseId = Sfdcbinding.query(strQueryForSpouseId);

                if (qrSpouseId.size > 0)
                {
                    SForce.Contact ctSpouseId = qrSpouseId.records[0] as SForce.Contact;

                    strSpouseId = ctSpouseId.Id;
                }
            }

            String strQueryForChildrenId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                           "and cmm_Household_Role__c = 'Child'";

            SForce.QueryResult qrChildrenId = Sfdcbinding.query(strQueryForChildrenId);

            if (qrChildrenId.size > 0)
            {
                for (int i = 0; i < qrChildrenId.size; i++)
                {
                    SForce.Contact ctChildId = qrChildrenId.records[i] as SForce.Contact;

                    lstChildId.Add(ctChildId.Id);
                }
            }

            String strQueryForMedicareDetails = "select c4g_Qualifies_for_Medicare__c, c4g_Qualifies_for_Medicare_A_and_B__c from Contact " +
                                      " where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Head of Household'";

            SForce.QueryResult qrQualifyForMedicare = Sfdcbinding.query(strQueryForMedicareDetails);

            if (qrQualifyForMedicare.size > 0)
            {
                SForce.Contact ctQualifyForMedicare = qrQualifyForMedicare.records[0] as SForce.Contact;

                if (ctQualifyForMedicare.c4g_Qualifies_for_Medicare__c == true && ctQualifyForMedicare.c4g_Qualifies_for_Medicare__cSpecified == true)
                {
                    rbListMedicareYesNo.SelectedIndex = 0;
                }
                else if (ctQualifyForMedicare.c4g_Qualifies_for_Medicare__c == false && ctQualifyForMedicare.c4g_Qualifies_for_Medicare__cSpecified == true)
                {
                    rbListMedicareYesNo.SelectedIndex = 1;
                }

                if (ctQualifyForMedicare.c4g_Qualifies_for_Medicare_A_and_B__c == true && ctQualifyForMedicare.c4g_Qualifies_for_Medicare_A_and_B__cSpecified == true)
                {
                    rbListMedicareAandB.SelectedIndex = 0;
                }
                else if (ctQualifyForMedicare.c4g_Qualifies_for_Medicare_A_and_B__c == false && ctQualifyForMedicare.c4g_Qualifies_for_Medicare_A_and_B__cSpecified == true)
                {
                    rbListMedicareAandB.SelectedIndex = 1;
                }
            }

            if (!IsPostBack)
            {

                String strQueryForMemberPlan = "select c4g_Plan__r.Name from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                               "and cmm_Household_Role__c = 'Head of Household'";

                SForce.QueryResult qrMemberPlan = Sfdcbinding.query(strQueryForMemberPlan);

                if (qrMemberPlan.size > 0)
                {

                    SForce.Contact ctMemberPlan = qrMemberPlan.records[0] as SForce.Contact;

                    if (ctMemberPlan.c4g_Plan__r != null)
                    {
                        switch (ctMemberPlan.c4g_Plan__r.Name)
                        {
                            case "Gold Medi-I":
                                ddlParticipantProgram.SelectedIndex = 1;
                                break;
                            case "Gold Medi-II":
                                ddlParticipantProgram.SelectedIndex = 2;
                                break;
                            case "Gold Plus":
                                ddlParticipantProgram.SelectedIndex = 3;
                                break;
                            case "Gold":
                                ddlParticipantProgram.SelectedIndex = 4;
                                break;
                            case "Silver":
                                ddlParticipantProgram.SelectedIndex = 5;
                                break;
                            case "Bronze":
                                ddlParticipantProgram.SelectedIndex = 6;
                                break;
                        }
                    }

                }

                String strQueryForSpousePlan = "select c4g_Plan__r.Name from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                               "and cmm_Household_Role__c = 'Spouse'";

                SForce.QueryResult qrSpousePlan = Sfdcbinding.query(strQueryForSpousePlan);

                if (qrSpousePlan.size > 0)
                {
                    SForce.Contact ctSpousePlan = qrSpousePlan.records[0] as SForce.Contact;

                    if (ctSpousePlan.c4g_Plan__r != null)
                    {
                        switch (ctSpousePlan.c4g_Plan__r.Name)
                        {
                            case "Gold Medi-I":
                                ddlSpouseProgram.SelectedIndex = 1;
                                break;
                            case "Gold Medi-II":
                                ddlSpouseProgram.SelectedIndex = 2;
                                break;
                            case "Gold Plus":
                                ddlSpouseProgram.SelectedIndex = 3;
                                break;
                            case "Gold":
                                ddlSpouseProgram.SelectedIndex = 4;
                                break;
                            case "Silver":
                                ddlSpouseProgram.SelectedIndex = 5;
                                break;
                            case "Bronze":
                                ddlSpouseProgram.SelectedIndex = 6;
                                break;
                        }
                    }
                }

                String strQueryForChildren = "select c4g_Plan__r.Name from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                             "and cmm_Household_Role__c = 'Child'";

                SForce.QueryResult qrChildrenProgram = Sfdcbinding.query(strQueryForChildren);

                if (qrChildrenProgram.size > 0)
                {
                    SForce.Contact ctChildProgram = qrChildrenProgram.records[0] as SForce.Contact;

                    if (ctChildProgram.c4g_Plan__r != null)
                    {
                        switch (ctChildProgram.c4g_Plan__r.Name)
                        {
                            case "Gold Plus":
                                ddlChildrenProgram.SelectedIndex = 1;
                                break;
                            case "Gold":
                                ddlChildrenProgram.SelectedIndex = 2;
                                break;
                            case "Silver":
                                ddlChildrenProgram.SelectedIndex = 3;
                                break;
                            case "Bronze":
                                ddlChildrenProgram.SelectedIndex = 4;
                                break;
                        }
                    }
                }
            }

            String strQueryForChurchId = "select Id, c4g_Church__c from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                         "and cmm_Household_Role__c = 'Head of Household'";

            SForce.QueryResult qrChurchId  = Sfdcbinding.query(strQueryForChurchId);

            if (qrChurchId.size > 0)
            {
                SForce.Contact ctChurchId = qrChurchId.records[0] as SForce.Contact;

                String strQueryForChurchInfo = "select Name, Senior_Pastor_s_Name__c, ShippingAddress, Phone from Account where RecordTypeId = '01237000000R6cjAAC' and " +
                                   "Id = '" + ctChurchId.c4g_Church__c + "'";

                SForce.QueryResult qrAcctChurchInfo = Sfdcbinding.query(strQueryForChurchInfo);

                if (qrAcctChurchInfo.size > 0)
                {
                    SForce.Account acctChurchInfo = qrAcctChurchInfo.records[0] as SForce.Account;

                    txtChurchName.Text = acctChurchInfo.Name;
                    txtSeniorPastor.Text = acctChurchInfo.Senior_Pastor_s_Name__c;
                    txtChurchStreetAddress.Text = acctChurchInfo.ShippingAddress.street;
                    txtChurchZipCode.Text = acctChurchInfo.ShippingAddress.postalCode;
                    ddlChurchState.Items.Add(new ListItem(acctChurchInfo.ShippingAddress.state));
                    ddlChurchState.SelectedIndex = 0;
                    ddlChurchCity.Items.Add(new ListItem(acctChurchInfo.ShippingAddress.city));
                    ddlChurchCity.SelectedIndex = 0;
                    txtChurchPhone.Text = acctChurchInfo.Phone;
                }
            }

            //SForce.DescribeSObjectResult describeSObjectResult = Sfdcbinding.describeSObject("Contact");

            //// Get the fields
            //if (describeSObjectResult != null)
            //{
            //    SForce.Field[] fields = describeSObjectResult.fields;

            //    for (int i = 0; i < fields.Length; i++)
            //    {
            //        SForce.Field field = fields[i];

            //        if (field.name == "Referral_Source__c")
            //        {
            //            SForce.PicklistEntry[] pickListValues = field.picklistValues;
            //            if (pickListValues != null)
            //            {
            //                //ddlReferredBy.Items.Clear();
            //                foreach (SForce.PicklistEntry pickListEntry in pickListValues)
            //                {
            //                    ddlReferredBy.Items.Add(pickListEntry.value);
            //                }
            //            }
            //        }
            //    }
            //}


            //String strQueryForChurchInfo = "select Name, Senior_Pastor_s_Name__c, ShippingAddress, Phone from Account where RecordTypeId = '01237000000R6cjAAC' and " +
            //                   "Id = '" + strAccountId + "'";

            //String strQueryForChurchInfo = "select Name, Senior_Pastor_s_Name__c, ShippingAddress, Phone from Account where Id = '" + strAccountId + "'";

            if (!IsPostBack)
            {
                ////////////////////////////////////////////////////////////////////////
                // if no spouse is added, then clear the ddlSpouseProgram dropdownlist
                SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);
                if (qrSpouse.size == 0) ddlSpouseProgram.Items.Clear();

                // if no child is added, then clear the ddlChildProgram dropdownlist
                SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildren);
                if (qrChild.size == 0) ddlChildrenProgram.Items.Clear();

                SForce.DescribeSObjectResult describeSObjectResult = Sfdcbinding.describeSObject("Membership__c");

                // Get the fields
                if (describeSObjectResult != null)
                {
                    SForce.Field[] fields = describeSObjectResult.fields;

                    for (int i = 0; i < fields.Length; i++)
                    {
                        SForce.Field field = fields[i];

                        if (field.name == "Referred_By__c")
                        {
                            SForce.PicklistEntry[] pickListValues = field.picklistValues;
                            if (pickListValues != null)
                            {
                                //ddlReferredBy.Items.Clear();
                                foreach (SForce.PicklistEntry pickListEntry in pickListValues)
                                {
                                    ddlReferredBy.Items.Add(pickListEntry.value);
                                }
                            }
                        }
                    }
                }

            }

            //strAccountID = (String)Session["AccountId"];
            //strSpouseId = (String)Session["ContactId"];
        }

        protected void InitializeSQLQueryForSpouse()
        {
            strQueryForSpouse = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";
        }

        protected void InitializeSQLQueryForChildren()
        {
            strQueryForChildren = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Child'";
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            rfvMedicareEligibility.Validate();
            rfvMedicareAandB.Validate();
            rfvParticipantProgram.Validate();

            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];

            if ((String)Session["ContactId"] != null) strPrimaryContactId = (String)Session["ContactId"];
            else
            {
                String strQueryForPrimaryContactId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                                     "and cmm_Household_Role__c = 'Head of Household'";

                SForce.QueryResult qrPrimaryContactId = Sfdcbinding.query(strQueryForPrimaryContactId);

                if (qrPrimaryContactId.size > 0)
                {
                    SForce.Contact ctPrimaryContactId = qrPrimaryContactId.records[0] as SForce.Contact;

                    strPrimaryContactId = ctPrimaryContactId.Id;
                }
            }
            if ((String)Session["SpouseId"] != null) strSpouseId = (String)Session["SpouseId"];
            else
            {
                String strQueryForSpouseId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " +
                                             "and cmm_Household_Role__c = 'Spouse'";

                SForce.QueryResult qrSpouseId = Sfdcbinding.query(strQueryForSpouseId);

                if (qrSpouseId.size > 0)
                {
                    SForce.Contact ctSpouseId = qrSpouseId.records[0] as SForce.Contact;

                    strSpouseId = ctSpouseId.Id;
                
                }
            }


            SForce.Contact ctUpdate = new SForce.Contact();

            ctUpdate.Id = strPrimaryContactId;
            ctUpdate.cmm_Household__c = strAccountId;
            ctUpdate.cmm_Household_Role__c = "Head of Household";

            // whether or not qualifying medicare
            if (rbListMedicareYesNo.SelectedIndex == 0)
            {
                ctUpdate.c4g_Qualifies_for_Medicare__c = true;
                ctUpdate.c4g_Qualifies_for_Medicare__cSpecified = true;
            }
            if (rbListMedicareYesNo.SelectedIndex == 1)
            {
                ctUpdate.c4g_Qualifies_for_Medicare__c = false;
                ctUpdate.c4g_Qualifies_for_Medicare__cSpecified = true;
            }

            // whether or not qualifying medicare A and B
            if (rbListMedicareAandB.SelectedIndex == 0)
            {
                ctUpdate.c4g_Qualifies_for_Medicare_A_and_B__c = true;
                ctUpdate.c4g_Qualifies_for_Medicare_A_and_B__cSpecified = true;
            }
            if (rbListMedicareAandB.SelectedIndex == 1)
            {
                ctUpdate.c4g_Qualifies_for_Medicare_A_and_B__c = false;
                ctUpdate.c4g_Qualifies_for_Medicare_A_and_B__cSpecified = true;
            }

            String strPrimaryProgram = ddlParticipantProgram.SelectedValue.Substring(0, ddlParticipantProgram.SelectedValue.IndexOf('(')).Trim();

            String strQueryForPrimaryProgramId = "select Id from c4g_Plan__c where Name = '" + strPrimaryProgram + "'";

            SForce.QueryResult qrPrimaryProgramId = Sfdcbinding.query(strQueryForPrimaryProgramId);

            if (qrPrimaryProgramId.size > 0)
            {
                SForce.c4g_Plan__c primaryPlan = qrPrimaryProgramId.records[0] as SForce.c4g_Plan__c;

                ctUpdate.c4g_Plan__c = primaryPlan.Id;

                SForce.SaveResult[] updateResultsPrimary = Sfdcbinding.update(new SForce.sObject[] { ctUpdate });

                if (updateResultsPrimary[0].success)
                {
                    // the primary member's program info added successfully
                }
            }

            String strSpouseProgram = String.Empty;
            
            if (ddlSpouseProgram.Items.Count != 0) strSpouseProgram = ddlSpouseProgram.SelectedValue.Substring(0, ddlSpouseProgram.SelectedValue.IndexOf('(')).Trim();

            String strQueryForSpouseProgramId = "select Id from c4g_Plan__c where Name = '" + strSpouseProgram + "'";

            SForce.QueryResult qrSpouseProgramId = Sfdcbinding.query(strQueryForSpouseProgramId);

            if (qrSpouseProgramId.size > 0)
            {
                SForce.c4g_Plan__c spousePlan = qrSpouseProgramId.records[0] as SForce.c4g_Plan__c;

                SForce.Contact ctSpouseUpdate = new SForce.Contact();
                ctSpouseUpdate.Id = strSpouseId;
                ctSpouseUpdate.c4g_Plan__c = spousePlan.Id;

                SForce.SaveResult[] updateResultsSpouse = Sfdcbinding.update(new SForce.sObject[] { ctSpouseUpdate });

                if (updateResultsSpouse[0].success)
                {
                    // the spouse's program info is added
                }
            }

            String strChildProgram = String.Empty;
            if (ddlChildrenProgram.Items.Count != 0) strChildProgram = ddlChildrenProgram.SelectedValue.Substring(0, ddlChildrenProgram.SelectedValue.IndexOf('(')).Trim();

            String strQueryForChildProgramId = "select Id from c4g_Plan__c where Name = '" + strChildProgram + "'";

            SForce.QueryResult qrChildProgramId = Sfdcbinding.query(strQueryForChildProgramId);

            if (qrChildProgramId.size > 0)
            {
                SForce.c4g_Plan__c childPlan = qrChildProgramId.records[0] as SForce.c4g_Plan__c;

                foreach (String strChildId in lstChildId)
                {
                    SForce.Contact ctChildUpdate = new SForce.Contact();

                    ctChildUpdate.Id = strChildId;
                    ctChildUpdate.c4g_Plan__c = childPlan.Id;

                    SForce.SaveResult[] updateResultsChild = Sfdcbinding.update(new SForce.sObject[] { ctChildUpdate });

                    if (updateResultsChild[0].success)
                    {
                        // the child's program info is added
                    }
                }
            }

            ///// This section insert church info to membership detail aspx

            String strQueryForHeadOfHouseholdId = "select Id from Contact where cmm_Household__c = '" + strAccountId + "' " + "and cmm_Household_Role__c = 'Head of Household'";

            SForce.QueryResult qrHeadOfHouseholdId = Sfdcbinding.query(strQueryForHeadOfHouseholdId);

            //String strHeadOfHouseholdId = null;

            if (qrHeadOfHouseholdId.size > 0)
            {
                SForce.Contact ctHead = (SForce.Contact)qrHeadOfHouseholdId.records[0];

                String strHeadOfHouseholdId = ctHead.Id;


                SForce.Account acctChurchInfo = new SForce.Account();

                
                acctChurchInfo.RecordTypeId = "01237000000R6cjAAC";
                acctChurchInfo.Name = txtChurchName.Text;
                acctChurchInfo.Senior_Pastor_s_Name__c = txtSeniorPastor.Text;

                if (txtChurchStreetAddress.Text != String.Empty) acctChurchInfo.ShippingStreet = txtChurchStreetAddress.Text;
                if (txtChurchZipCode.Text != String.Empty) acctChurchInfo.ShippingPostalCode = txtChurchZipCode.Text;
                if (ddlChurchState.Items != null) acctChurchInfo.ShippingState = ddlChurchState.SelectedItem.Text;
                if (ddlChurchCity.Items != null) acctChurchInfo.ShippingCity = ddlChurchCity.SelectedItem.Text;
                if (txtChurchPhone.Text != String.Empty) acctChurchInfo.Phone = txtChurchPhone.Text;

                SForce.SaveResult[] saveAcctResults = Sfdcbinding.create(new SForce.sObject[] { acctChurchInfo });

                if (saveAcctResults[0].success)
                {
                    SForce.Contact ctHeadOfHousehold = new SForce.Contact();

                    ctHeadOfHousehold.Id = strHeadOfHouseholdId;
                    //ctHeadOfHousehold.Referral_Source__c = ddlReferredBy.SelectedValue;
                    ctHeadOfHousehold.c4g_Church__c = saveAcctResults[0].id;

                    SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctHeadOfHousehold });

                    if (updateResults[0].success)
                    {
                        // notify the user that church information added to household
                        
                    }
                }
            }

            if (ddlReferredBy.SelectedValue != "")
            {
                if (ddlReferredBy.SelectedValue == "Member Referral" && txtMembershipNumber.Text != "" && ddlReferrerName.SelectedValue != "")
                {

                    String strMembershipNumber = String.Empty;

                    if (txtMembershipNumber.Text.Contains("MEMB-"))
                    {
                        strMembershipNumber = txtMembershipNumber.Text;
                    }
                    else
                    {
                        strMembershipNumber = txtMembershipNumber.Text;
                        if (txtMembershipNumber.Text.Length < 7)
                        {
                            for (int i = txtMembershipNumber.Text.Length; i < 7; i++)
                            {
                                strMembershipNumber = '0' + strMembershipNumber;
                            }
                        }
                        strMembershipNumber = "MEMB-" + strMembershipNumber;
                    }

                    String strQueryForContact = "select Id from Contact where c4g_Membership__r.Name = '" + strMembershipNumber + "' and Name = '" + ddlReferrerName.SelectedValue + "'";

                    SForce.QueryResult qrContactForMembership = Sfdcbinding.query(strQueryForContact);

                    if (qrContactForMembership.size > 0)
                    {
                        SForce.Contact ctReferrer = qrContactForMembership.records[0] as SForce.Contact;
                        SForce.Membership__c memReferralInfo = new SForce.Membership__c();

                        memReferralInfo.Paying_Member__c = strPrimaryContactId;
                        memReferralInfo.Referred_By__c = ddlReferredBy.SelectedValue;
                        memReferralInfo.Referrer__c = ctReferrer.Id;
                        if ((String)Session["Email"] != null) memReferralInfo.Email__c = (String)Session["Email"];

                        SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { memReferralInfo });

                        if (saveResults[0].success)
                        {
                            // Membership object is created successfully
                            String strQueryForFamilyMembers = "select Id from Contact where cmm_Household__c = '" + strAccountId + "'";
                            SForce.QueryResult qrFamilyMembers = Sfdcbinding.query(strQueryForFamilyMembers);

                            if (qrFamilyMembers.size > 0)
                            {
                                for (int i = 0; i < qrFamilyMembers.size; i++)
                                {
                                    SForce.Contact ctFamilyMemberId = qrFamilyMembers.records[i] as SForce.Contact;

                                    SForce.Contact ctFamilyMember = new SForce.Contact();
                                    ctFamilyMember.Id = ctFamilyMemberId.Id;
                                    ctFamilyMember.c4g_Membership__c = saveResults[0].id;

                                    SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctFamilyMember });
                                    if (updateResults[0].success)
                                    {
                                        // Membership object added to each member of the household
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    SForce.Membership__c memReferralInfo = new SForce.Membership__c();

                    memReferralInfo.Paying_Member__c = strPrimaryContactId;
                    memReferralInfo.Referred_By__c = ddlReferredBy.SelectedValue;
                    if ((String)Session["Email"] != null) memReferralInfo.Email__c = (String)Session["Email"];

                    SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { memReferralInfo });

                    if (saveResults[0].success)
                    {
                        // Membership object is created successfully

                        // Membership object is created successfully
                        String strQueryForFamilyMembers = "select Id from Contact where cmm_Household__c = '" + strAccountId + "'";
                        SForce.QueryResult qrFamilyMembers = Sfdcbinding.query(strQueryForFamilyMembers);

                        if (qrFamilyMembers.size > 0)
                        {
                            for (int i = 0; i < qrFamilyMembers.size; i++)
                            {
                                SForce.Contact ctFamilyMemberId = qrFamilyMembers.records[i] as SForce.Contact;

                                SForce.Contact ctFamilyMember = new SForce.Contact();
                                ctFamilyMember.Id = ctFamilyMemberId.Id;
                                ctFamilyMember.c4g_Membership__c = saveResults[0].id;

                                SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctFamilyMember });
                                if (updateResults[0].success)
                                {
                                    // Membership object added to each member of the household
                                }
                            }
                        }

                    }

                }
            }




            SForce.Account acctPrimary = new SForce.Account();
            acctPrimary.Id = strAccountId;
            acctPrimary.cmm_Account_Creation_Step_Code__c = "5";

            SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

            if (srAccount[0].success)
            {
                Session["PreviousPage"] = "MembershipDetails";
                Response.Redirect("~/PaymentInfo.aspx");
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Session["PreviousPage"] = "MembershipDetails";
            Response.Redirect("~/FamilyDetails.aspx");
        }

        protected void ddlChurchState_TextChanged(object sender, EventArgs e)
        {

            String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";

            String strQueryForCityName = "select distinct name, state_code from city where state_code = '" + ddlChurchState.SelectedValue.ToString() + "' order by name";

            MySqlConnection conn = new MySqlConnection(strConnectionString);

            MySqlCommand cmdCityName = new MySqlCommand(strQueryForCityName, conn);

            DataSet dsCity = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmdCityName);

            da.Fill(dsCity);

            ddlChurchCity.DataSource = dsCity.Tables[0];
            ddlChurchCity.DataTextField = "name";
            ddlChurchCity.DataValueField = "name";
            ddlChurchCity.DataBind();

        }

        protected void txtChurchZipCode_TextChanged(object sender, EventArgs e)
        {
            if (txtChurchZipCode.Text.Length == 5)            //txtZipCode.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtChurchZipCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ddlChurchState.Items.Clear();
                    ddlChurchCity.Items.Clear();

                    ddlChurchState.Items.Add(new ListItem(dr["state_code"].ToString()));
                    ddlChurchState.SelectedIndex = 0;
                    ddlChurchCity.Items.Add(new ListItem(dr["name"].ToString()));
                    ddlChurchCity.SelectedIndex = 0;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
                }
                conn.Close();
            }
        }

        protected void txtMembershipNumber_TextChanged(object sender, EventArgs e)
        {

            if (ddlReferredBy.SelectedValue == "Member Referral")
            {
                TextBox txtMembershipNumber = (TextBox)sender;

                String strMembershipNumber = String.Empty;

                if (txtMembershipNumber.Text.Contains("MEMB-"))
                {
                    strMembershipNumber = txtMembershipNumber.Text;
                }
                else
                {
                    strMembershipNumber = txtMembershipNumber.Text;
                    if (txtMembershipNumber.Text.Length < 7)
                    {
                        for (int i = txtMembershipNumber.Text.Length; i < 7; i++)
                        {
                            strMembershipNumber = '0' + strMembershipNumber;
                        }
                    }
                    strMembershipNumber = "MEMB-" + strMembershipNumber;
                }

                String strQueryForMembership = "select Name from Contact where c4g_Membership__r.Name = '" + strMembershipNumber + "'";

                SForce.QueryResult qrMembership = Sfdcbinding.query(strQueryForMembership);
                if (qrMembership.size > 0)
                {
                    ddlReferrerName.Items.Clear();
                    ddlReferrerName.Enabled = true;

                    for (int i = 0; i < qrMembership.size; i++)
                    {
                        SForce.Contact ctReferrer = qrMembership.records[i] as SForce.Contact;
                        ddlReferrerName.Items.Add(ctReferrer.Name);
                    }
                }
            }
        }

        protected void ddlReferredBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReferrer = (DropDownList)sender;

            if (ddlReferrer.SelectedValue == "Member Referral")
            {
                txtMembershipNumber.Enabled = true;
            }
            else
            {
                txtMembershipNumber.Text = String.Empty;
                txtMembershipNumber.Enabled = false;
                ddlReferrerName.Items.Clear();
                ddlReferrerName.Enabled = false;
            }
        }
    }
}