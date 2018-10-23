using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;
using System.Data;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

namespace SalesForceWebApp
{
    public partial class PaymentInfo : System.Web.UI.Page
    {

        private String strAccountId = null;
        private String strContactId = null;
        private String strMemberEmail = String.Empty;

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;
        protected SForce.SaveResult[] saveResults = null;

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            // if Session variable below is null, retrieve AccountId from Salesforce
            if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];
            if ((String)Session["Email"] != String.Empty) strMemberEmail = (String)Session["Email"];
                

            InitializedSfdcbinding();

            if (!IsPostBack)
            {

                String strQueryPaymentInfoForEmail = "select Id, Paying_Member__c, cmm_Payment_Method__c, cmm_Bank_Account__c, cmm_Credit_Card_Info__c, cmm_Payment_Frequency__c, " +
                                                     "Invoice_Delivery__c " +
                                                     "from Membership__c where Email__c = '" + strMemberEmail + "'";

                SForce.QueryResult qrPaymentInfo = Sfdcbinding.query(strQueryPaymentInfoForEmail);

                if (qrPaymentInfo.size > 0)
                {
                    SForce.Membership__c memPaymentInfo = qrPaymentInfo.records[0] as SForce.Membership__c;

                    if (memPaymentInfo.cmm_Payment_Method__c == "ACH/PAD") rbListPaymentMethod.SelectedIndex = 0;
                    else if (memPaymentInfo.cmm_Payment_Method__c == "Credit Card") rbListPaymentMethod.SelectedIndex = 1;

                    if (memPaymentInfo.cmm_Payment_Frequency__c == "Recurring")
                    {
                        rbListPaymentFrequency.SelectedIndex = 0;
                    }
                    else if (memPaymentInfo.cmm_Payment_Frequency__c == "One Time Gift")
                    {
                        rbListPaymentFrequency.SelectedIndex = 1;
                    }

                    if (memPaymentInfo.cmm_Payment_Method__c == "ACH/PAD")
                    {
                        String strQueryForBankAccountInfo = "select Id, Account_Type__c, Account_Owner_Name__c, Bank_Name__c, Routing_Number__c, Account_Number__c " +
                                                            "from BankAccount__c where Id = '" + memPaymentInfo.cmm_Bank_Account__c + "'";

                        SForce.QueryResult qrBankAccountInfo = Sfdcbinding.query(strQueryForBankAccountInfo);

                        if (qrBankAccountInfo.size > 0)
                        {
                            SForce.BankAccount__c bankAcctInfo = qrBankAccountInfo.records[0] as SForce.BankAccount__c;

                            pnlBankInformation.Visible = true;
                            pnlCreditCardInformation.Visible = false;

                            if (bankAcctInfo.Account_Type__c == "Checking") rbListAccountType.SelectedIndex = 0;
                            else if (bankAcctInfo.Account_Type__c == "Saving") rbListAccountType.SelectedIndex = 1;

                            txtBankName.Text = bankAcctInfo.Bank_Name__c;
                            txtAccountOwnerName.Text = bankAcctInfo.Account_Owner_Name__c;
                            txtRoutingNumber.Text = bankAcctInfo.Routing_Number__c;
                            txtAccountNumber.Text = bankAcctInfo.Account_Number__c;
                        }
                    }
                    else if (memPaymentInfo.cmm_Payment_Method__c == "Credit Card")
                    {
                        String strQueryForCreditCardInfo = "select Id, Card_Type__c, Name_on_the_card__c, Credit_Card_Number__c, CVV__c, Expiration_Date__c " +
                                                           "from Credit_Card_Info__c where Id = '" + memPaymentInfo.cmm_Credit_Card_Info__c + "'";

                        SForce.QueryResult qrCreditCardInfo = Sfdcbinding.query(strQueryForCreditCardInfo);

                        if (qrCreditCardInfo.size > 0)
                        {
                            SForce.Credit_Card_Info__c creditCardInfo = qrCreditCardInfo.records[0] as SForce.Credit_Card_Info__c;

                            pnlBankInformation.Visible = false;
                            pnlCreditCardInformation.Visible = true;

                            if (creditCardInfo.Card_Type__c == "Master Card") ddlCreditCardType.SelectedIndex = 0;
                            else if (creditCardInfo.Card_Type__c == "Visa Card") ddlCreditCardType.SelectedIndex = 1;
                            else if (creditCardInfo.Card_Type__c == "Discover Card") ddlCreditCardType.SelectedIndex = 2;

                            txtNameOnCard.Text = creditCardInfo.Name_on_the_card__c;
                            txtCreditCardNumber.Text = creditCardInfo.Credit_Card_Number__c;
                            txtCreditCardCVV.Text = creditCardInfo.CVV__c;
                            DateTime dtExpirationDate = creditCardInfo.Expiration_Date__c.Value;
                            ddlExpirationDateMonth.SelectedIndex = dtExpirationDate.Month - 1;

                            ddlExpirationDateYear.Items.Clear();
                            int nExpirationYear = DateTime.Today.Year;

                            for (int i = nExpirationYear; i < nExpirationYear + 15; i++)
                            {
                                ddlExpirationDateYear.Items.Add(new ListItem(i.ToString()));
                            }

                            ddlExpirationDateYear.SelectedValue = dtExpirationDate.Year.ToString();
                        }
                    }
                    //else if (memPaymentInfo.cmm_Payment_Method__c == "")
                    //{
                    //    ddlExpirationDateYear.Items.Clear();

                    //    int nYear = DateTime.Today.Year;

                    //    for (int i = nYear; i < nYear + 15; i++)
                    //    {
                    //        ddlExpirationDateYear.Items.Add(new ListItem(i.ToString()));
                    //    }
                    //}

                    String strQueryForReferralSolicit = "select Paying_Member__r.cmm_Solicit_Codes__c, Invoice_Delivery__c " +
                                                        "from Membership__c where Id = '" + memPaymentInfo.Id + "'";

                    SForce.QueryResult qrOtherAddressReferralSolicitCode = Sfdcbinding.query(strQueryForReferralSolicit);

                    if (qrOtherAddressReferralSolicitCode.size > 0)
                    {
                        SForce.Membership__c memOtherAddressReferralSolicitCode = qrOtherAddressReferralSolicitCode.records[0] as SForce.Membership__c;

                        if (memOtherAddressReferralSolicitCode.Invoice_Delivery__c == "Email") chkNotifyByEmail.Checked = true;
                        if (memOtherAddressReferralSolicitCode.Invoice_Delivery__c == "Postal Mail") chkNotifyByPostal.Checked = true;
                        if (memOtherAddressReferralSolicitCode.Invoice_Delivery__c == "Both")
                        {
                            chkNotifyByEmail.Checked = true;
                            chkNotifyByPostal.Checked = true;
                        }
                        if (memOtherAddressReferralSolicitCode.Invoice_Delivery__c == "Neither")
                        {
                            chkNotifyByEmail.Checked = false;
                            chkNotifyByPostal.Checked = false;
                        }

                        if (memOtherAddressReferralSolicitCode.Paying_Member__r.cmm_Solicit_Codes__c != null)
                        {
                            if (memOtherAddressReferralSolicitCode.Paying_Member__r.cmm_Solicit_Codes__c.Contains("Allow Postal Mail;")) rbListJoinMailing.SelectedIndex = 0;
                            else rbListJoinMailing.SelectedIndex = 1;

                            if (memOtherAddressReferralSolicitCode.Paying_Member__r.cmm_Solicit_Codes__c.Contains("Allow SMS Messages;")) rbListAllowMessages.SelectedIndex = 0;
                            else rbListAllowMessages.SelectedIndex = 1;
                            if (memOtherAddressReferralSolicitCode.Paying_Member__r.cmm_Solicit_Codes__c.Contains("No Communication of Any Kind;"))
                            {
                                rbListJoinMailing.SelectedIndex = 1;
                                rbListAllowMessages.SelectedIndex = 1;
                            }
                        }
                    }

                    String strQueryForBillingAddress = "select BillingAddress, ShippingAddress from Account where cmm_Email__c = '" + strMemberEmail + "'";

                    SForce.QueryResult qrBothAddress = Sfdcbinding.query(strQueryForBillingAddress);

                    if (qrBothAddress.size > 0)
                    {
                        SForce.Account acctBothAddress = qrBothAddress.records[0] as SForce.Account;

                        if ((acctBothAddress.BillingAddress.street != acctBothAddress.ShippingAddress.street) ||
                            (acctBothAddress.BillingAddress.city != acctBothAddress.ShippingAddress.city) ||
                            (acctBothAddress.BillingAddress.state != acctBothAddress.ShippingAddress.state) ||
                            (acctBothAddress.BillingAddress.postalCode != acctBothAddress.ShippingAddress.postalCode))
                        {
                            chkBillingAddressDifferent.Checked = true;
                            pnlBillingAddress.Visible = true;

                            txtBillingStreetAddress.Text = acctBothAddress.BillingAddress.street;

                            ddlBillingCity.Items.Add(new ListItem(acctBothAddress.BillingAddress.city));
                            ddlBillingCity.SelectedIndex = 0;

                            ddlBillingState.Items.Add(new ListItem(acctBothAddress.BillingAddress.state));
                            ddlBillingState.SelectedIndex = 0;

                            txtBillingZipCode.Text = acctBothAddress.BillingAddress.postalCode;
                        }
                    }

                }
                else
                { 
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
                    //                foreach (SForce.PicklistEntry pickListEntry in pickListValues)
                    //                {
                    //                    ddlReferredBy.Items.Add(pickListEntry.value);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    //if (!IsPostBack)
                    //{
                    //rbListPaymentMethod.SelectedIndex = 0;
                    //rbListPaymentFrequency.SelectedIndex = 0;

                    //}

                    ddlExpirationDateYear.Items.Clear();

                    int nYear = DateTime.Today.Year;

                    for (int i = nYear; i < nYear + 15; i++)
                    {
                        ddlExpirationDateYear.Items.Add(new ListItem(i.ToString()));
                    }
                }
            }
            //if (rfvPaymentMethod.IsValid) ;
            //if (rfvPaymentMethod.IsValid) ;
            //if (rfvPaymentFrequency.IsValid) ;
            //if (rfvBankAccountType.IsValid) ;
            //if (rfvBankName.IsValid) ;
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            Session["PreviousPage"] = "PaymentInfo";

            //foreach (BaseValidator val in Page.Validators)
            //{
            //    val.EnableClientScript = false;
            //}

            Response.Redirect("~/MembershipDetails.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            //foreach (BaseValidator val in Page.Validators)
            //{
            //    val.EnableClientScript = true;
            //}

            if (Page.IsValid)
            {
                // if the Session variable below is null, retrieve email address from Salesforce
                String strEmail = (String)Session["Email"];
                String strQueryForPayingMemberId = "select Id from Contact where Email = '" + strEmail + "'";
                SForce.QueryResult qrPayingMember = Sfdcbinding.query(strQueryForPayingMemberId);

                String strPayingMemberId = String.Empty;

                if (qrPayingMember.size > 0)
                {
                    SForce.Contact ctPayingMember = (SForce.Contact)qrPayingMember.records[0];
                    strPayingMemberId = (String)ctPayingMember.Id;
                }

                //String strQueryPaymentInfoForEmail = "select Id, Paying_Member__c, cmm_Payment_Method__c, cmm_Bank_Account__c, cmm_Credit_Card_Info__c, cmm_Payment_Frequency__c, " +
                //                                     "Invoice_Delivery__c " +
                //                                     "from Membership__c where Email__c = '" + strMemberEmail + "'";

                String strQueryPaymentInfoForEmail = "select Id, Paying_Member__c, cmm_Payment_Method__c, cmm_Payment_Frequency__c, " + 
                                                     "cmm_Bank_Account__c, cmm_Credit_Card_Info__c, Invoice_Delivery__c " +
                                                     "from Membership__c where Email__c = '" + strMemberEmail + "'";

                SForce.QueryResult qrMembershipId = Sfdcbinding.query(strQueryPaymentInfoForEmail);

                if (qrMembershipId.size > 0)
                {
                    SForce.Membership__c currentMembership = new SForce.Membership__c();
                    currentMembership.Id = qrMembershipId.records[0].Id;

                    if (strPayingMemberId != String.Empty) currentMembership.Paying_Member__c = strPayingMemberId;
                    currentMembership.Email__c = strEmail;
                    if (rbListPaymentMethod.SelectedItem.Text == "Bank ACH") currentMembership.cmm_Payment_Method__c = "ACH/PAD";
                    if (rbListPaymentMethod.SelectedItem.Text == "Credit Card") currentMembership.cmm_Payment_Method__c = "Credit Card";

                    if (rbListPaymentFrequency.SelectedItem.Text == "Recurring") currentMembership.cmm_Payment_Frequency__c = "Recurring";
                    if (rbListPaymentFrequency.SelectedItem.Text == "One Time") currentMembership.cmm_Payment_Frequency__c = "One Time Gift";

                    if (chkNotifyByEmail.Checked && chkNotifyByPostal.Checked) currentMembership.Invoice_Delivery__c = "Both";
                    if (chkNotifyByEmail.Checked && !chkNotifyByPostal.Checked) currentMembership.Invoice_Delivery__c = "Email";
                    if (!chkNotifyByEmail.Checked && chkNotifyByPostal.Checked) currentMembership.Invoice_Delivery__c = "Postal Mail";
                    if (!chkNotifyByEmail.Checked && !chkNotifyByPostal.Checked) currentMembership.Invoice_Delivery__c = "Neither";

                    SForce.SaveResult[] membershipUpdateResults = Sfdcbinding.update(new SForce.sObject[] { currentMembership });

                    if (membershipUpdateResults[0].success)
                    {

                    }

                    if (rbListPaymentMethod.SelectedIndex == 0) // Bank ACH
                    {
                        String strQueryForBankAccountId = "select cmm_Bank_Account__c from Membership__c where Id = '" + currentMembership.Id + "'";

                        SForce.QueryResult qrCurrentMembership = Sfdcbinding.query(strQueryForBankAccountId);

                        if (qrCurrentMembership.size > 0) // get the current Bank Account Info
                        {
                            SForce.Membership__c membership = qrCurrentMembership.records[0] as SForce.Membership__c;

                            if (membership.cmm_Bank_Account__c != null)
                            {

                                SForce.BankAccount__c currentBankInfo = new SForce.BankAccount__c();

                                currentBankInfo.Id = membership.cmm_Bank_Account__r.Id;
                                currentBankInfo.Account_Type__c = rbListAccountType.SelectedValue;
                                currentBankInfo.Bank_Name__c = txtBankName.Text;
                                currentBankInfo.Account_Owner_Name__c = txtAccountOwnerName.Text;
                                currentBankInfo.Routing_Number__c = txtRoutingNumber.Text;
                                currentBankInfo.Account_Number__c = txtAccountNumber.Text;

                                SForce.SaveResult[] bankInfoUpdateResults = Sfdcbinding.update(new SForce.sObject[] { currentBankInfo });

                                if (bankInfoUpdateResults[0].success)
                                {
                                    SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                    UpdateHeadOfHouseholdCommunication();
                                    UpdateAccountCreationStep("6");
                                    Session["PreviousPage"] = "PaymentInfo";
                                    Response.Redirect("HealthHistory.aspx");
                                }
                            }
                            else
                            {
                                SForce.BankAccount__c newBankInfo = new SForce.BankAccount__c();

                                newBankInfo.Account_Type__c = rbListAccountType.SelectedItem.Value;
                                newBankInfo.Bank_Name__c = txtBankName.Text;
                                newBankInfo.Account_Owner_Name__c = txtAccountOwnerName.Text;
                                newBankInfo.Routing_Number__c = txtRoutingNumber.Text;
                                newBankInfo.Account_Number__c = txtAccountNumber.Text;

                                SForce.SaveResult[] saveBankInfo = Sfdcbinding.create(new SForce.sObject[] { newBankInfo });

                                if (saveBankInfo[0].success)
                                {
                                    SForce.Membership__c membershipUpdateForBankInfo = new SForce.Membership__c();

                                    membershipUpdateForBankInfo.Id = currentMembership.Id;
                                    membershipUpdateForBankInfo.cmm_Bank_Account__c = saveBankInfo[0].id;

                                    SForce.SaveResult[] memberUpdate = Sfdcbinding.update(new SForce.sObject[] { membershipUpdateForBankInfo });
                                    if (memberUpdate[0].success)
                                    {

                                    }

                                    // delete the current credit card info

                                    String strQueryForCreditCardInfoId = "select cmm_Credit_Card_Info__c from Membership__c where Id = '" + currentMembership.Id + "'";

                                    SForce.QueryResult qrCreditCardInfoId = Sfdcbinding.query(strQueryForCreditCardInfoId);

                                    if (qrCreditCardInfoId.size > 0)
                                    {
                                        SForce.Membership__c mem = qrCreditCardInfoId.records[0] as SForce.Membership__c;

                                        String[] strCreditCardInfoId = new String[] { mem.cmm_Credit_Card_Info__r.Id };
                                        SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(strCreditCardInfoId);

                                        if (deleteResults[0].success)
                                        {
                                            // The credit card info deleted successfully
                                            SForce.Membership__c memUpdate = new SForce.Membership__c();
                                            memUpdate.Id = currentMembership.Id;
                                            memUpdate.cmm_Credit_Card_Info__c = null;

                                            SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { memUpdate });

                                            if (updateResults[0].success)
                                            {
                                                // The membership updated successfully
                                                SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                                UpdateHeadOfHouseholdCommunication();
                                                UpdateAccountCreationStep("6");
                                                Session["PreviousPage"] = "PaymentInfo";
                                                Response.Redirect("HealthHistory.aspx");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (rbListPaymentMethod.SelectedIndex == 1) // Credit card
                    {
                        // 10/27/17 begin here
                        // update credit card object on current credit card info or create new credit card object for new credit card info
                        String strQueryForCurrentCreditCard = "select cmm_Credit_Card_Info__r.Id from Membership__c where Id = '" + currentMembership.Id + "'";

                        SForce.QueryResult qrCurrentCreditCard = Sfdcbinding.query(strQueryForCurrentCreditCard);

                        if (qrCurrentCreditCard.size > 0)   // The credit card info exist in salesforce
                        {
                            SForce.Membership__c memCreditCardInfoId = qrCurrentCreditCard.records[0] as SForce.Membership__c;

                            if (memCreditCardInfoId.cmm_Credit_Card_Info__c != null)
                            {

                                SForce.Credit_Card_Info__c credit_card_info = new SForce.Credit_Card_Info__c();
                                credit_card_info.Id = memCreditCardInfoId.cmm_Credit_Card_Info__r.Id;
                                credit_card_info.Card_Type__c = ddlCreditCardType.SelectedValue;
                                credit_card_info.Name_on_the_card__c = txtNameOnCard.Text;
                                credit_card_info.Credit_Card_Number__c = txtCreditCardNumber.Text;
                                credit_card_info.CVV__c = txtCreditCardCVV.Text;
                                DateTime dtExpirationDate = new DateTime(Int32.Parse(ddlExpirationDateYear.SelectedValue), Int32.Parse(ddlExpirationDateMonth.SelectedValue), 1);
                                credit_card_info.Expiration_Date__c = dtExpirationDate;
                                credit_card_info.Expiration_Date__cSpecified = true;

                                SForce.SaveResult[] updateCreditCardInfo = Sfdcbinding.update(new SForce.sObject[] { credit_card_info });

                                if (updateCreditCardInfo[0].success)
                                {
                                    // update credit card info succeeded
                                    //SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                    //UpdateAccountCreationStep("6");
                                    //Session["PreviousPage"] = "PaymentInfo";
                                    //Response.Redirect("HealthHistory.aspx");

                                    SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                    UpdateHeadOfHouseholdCommunication();
                                    UpdateAccountCreationStep("6");
                                    Session["PreviousPage"] = "PaymentInfo";
                                    Response.Redirect("HealthHistory.aspx");


                                }
                            }
                            else
                            {
                                SForce.Credit_Card_Info__c newCreditCardInfo = new SForce.Credit_Card_Info__c();

                                newCreditCardInfo.Card_Type__c = ddlCreditCardType.SelectedValue;
                                newCreditCardInfo.Name_on_the_card__c = txtNameOnCard.Text;
                                newCreditCardInfo.Credit_Card_Number__c = txtCreditCardNumber.Text;
                                newCreditCardInfo.CVV__c = txtCreditCardCVV.Text;
                                newCreditCardInfo.Expiration_Date__c = new DateTime(Int32.Parse(ddlExpirationDateYear.SelectedValue), Int32.Parse(ddlExpirationDateMonth.SelectedValue), 1);
                                newCreditCardInfo.Expiration_Date__cSpecified = true;

                                SForce.SaveResult[] saveCreditCardInfo = Sfdcbinding.create(new SForce.sObject[] { newCreditCardInfo });

                                if (saveCreditCardInfo[0].success)
                                {
                                    // new credit card info created successfully
                                    SForce.Membership__c membershipUpdateForCreditCardInfo = new SForce.Membership__c();
                                    membershipUpdateForCreditCardInfo.Id = currentMembership.Id;
                                    membershipUpdateForCreditCardInfo.cmm_Credit_Card_Info__c = saveCreditCardInfo[0].id;

                                    SForce.SaveResult[] membershipUpdate = Sfdcbinding.update(new SForce.sObject[] { membershipUpdateForCreditCardInfo });
                                    if (membershipUpdate[0].success)
                                    {
                                        // membership updated successfully
                                    }

                                    // delete the current bank account info
                                    String strQueryForBankAccountInfoId = "select cmm_Bank_Account__c from Membership__c where Id = '" + currentMembership.Id + "'";

                                    SForce.QueryResult qrBankAccountInfoId = Sfdcbinding.query(strQueryForBankAccountInfoId);

                                    if (qrBankAccountInfoId.size > 0)
                                    {
                                        SForce.Membership__c mem = qrBankAccountInfoId.records[0] as SForce.Membership__c;

                                        if (mem.cmm_Bank_Account__c != null)
                                        {
                                            String[] strBankAccountInfoId = new String[] { mem.cmm_Bank_Account__c };
                                            SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(strBankAccountInfoId);

                                            if (deleteResults[0].success)
                                            {
                                                SForce.Membership__c memUpdate = new SForce.Membership__c();

                                                memUpdate.Id = currentMembership.Id;
                                                memUpdate.cmm_Bank_Account__c = null;

                                                SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { memUpdate });

                                                if (updateResults[0].success)
                                                {
                                                    // he membership updated successfully
                                                    //SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                                    //UpdateAccountCreationStep("6");
                                                    //Session["PreviousPage"] = "PaymentInfo";
                                                    //Response.Redirect("HealthHistory.aspx");

                                                    SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                                    UpdateHeadOfHouseholdCommunication();
                                                    UpdateAccountCreationStep("6");
                                                    Session["PreviousPage"] = "PaymentInfo";
                                                    Response.Redirect("HealthHistory.aspx");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                            UpdateHeadOfHouseholdCommunication();
                                            UpdateAccountCreationStep("6");
                                            Session["PreviousPage"] = "PaymentInfo";
                                            Response.Redirect("HealthHistory.aspx");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Boolean bUpdateFamilyMember = false;

                    SForce.Membership__c newMembership = new SForce.Membership__c();

                    if (strPayingMemberId != String.Empty) newMembership.Paying_Member__c = strPayingMemberId;
                    newMembership.Email__c = strEmail;
                    if (rbListPaymentMethod.SelectedItem.Text == "Bank ACH") newMembership.cmm_Payment_Method__c = "ACH/PAD";
                    if (rbListPaymentMethod.SelectedItem.Text == "Credit Card") newMembership.cmm_Payment_Method__c = "Credit Card";

                    if (rbListPaymentFrequency.SelectedItem.Text == "Recurring") newMembership.cmm_Payment_Frequency__c = "Recurring";
                    if (rbListPaymentFrequency.SelectedItem.Text == "One Time") newMembership.cmm_Payment_Frequency__c = "One Time Gift";

                    if (chkNotifyByEmail.Checked && chkNotifyByPostal.Checked) newMembership.Invoice_Delivery__c = "Both";
                    if (chkNotifyByEmail.Checked && !chkNotifyByPostal.Checked) newMembership.Invoice_Delivery__c = "Email";
                    if (!chkNotifyByEmail.Checked && chkNotifyByPostal.Checked) newMembership.Invoice_Delivery__c = "Postal Mail";
                    if (!chkNotifyByEmail.Checked && !chkNotifyByPostal.Checked) newMembership.Invoice_Delivery__c = "Neither";

                    SForce.SaveResult[] saveMemberships = Sfdcbinding.create(new SForce.sObject[] { newMembership });

                    String strMembershipId = String.Empty;

                    if (saveMemberships[0].success)
                    {
                        strMembershipId = saveMemberships[0].id;
                        Session["MembershipId"] = strMembershipId;
                    }

                    String strQueryForFamilyMembersId = "select Id from Contact where (cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Head of Household') or " +
                                                        "(cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse') or " +
                                                        "(cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Child')";

                    SForce.QueryResult qrFamilyMembersId = Sfdcbinding.query(strQueryForFamilyMembersId);

                    if (qrFamilyMembersId.size > 0)
                    {
                        String[] strFamilyMemberIds = new String[qrFamilyMembersId.size];
                        SForce.Contact[] ctFamilyMembers = new SForce.Contact[qrFamilyMembersId.size];

                        for (int i = 0; i < qrFamilyMembersId.size; i++)
                        {
                            ctFamilyMembers[i] = (SForce.Contact)qrFamilyMembersId.records[i];
                            if (strMembershipId != null) ctFamilyMembers[i].c4g_Membership__c = strMembershipId;

                            SForce.SaveResult[] srUpdateFamilyMember = Sfdcbinding.update(new SForce.sObject[] { ctFamilyMembers[i] });

                            if (srUpdateFamilyMember[0].success)
                            {
                                //SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                //String strStep = "6";
                                //UpdateAccountCreationStep(strStep);
                                //UpdateHeadOfHousehold();
                                //Session["PreviousPage"] = "PaymentInfo";
                                //Response.Redirect("~/HealthHistory.aspx");
                                bUpdateFamilyMember = true;
                            }
                        }
                    }

                    if (bUpdateFamilyMember)
                    {

                        if (rbListPaymentMethod.SelectedIndex == 0)
                        {
                            SForce.BankAccount__c bankAcct = new SForce.BankAccount__c();
                            bankAcct.Account_Type__c = rbListAccountType.SelectedItem.Text;
                            bankAcct.Bank_Name__c = txtBankName.Text;
                            bankAcct.Account_Owner_Name__c = txtAccountOwnerName.Text;
                            bankAcct.Routing_Number__c = txtRoutingNumber.Text;
                            bankAcct.Account_Number__c = txtAccountNumber.Text;

                            SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { bankAcct });
                            if (saveResults[0].success)
                            {
                            }

                            SForce.Membership__c membership = new SForce.Membership__c();

                            membership.Id = strMembershipId;
                            membership.cmm_Bank_Account__c = saveResults[0].id;

                            SForce.SaveResult[] srUpdateMemberships = Sfdcbinding.update(new SForce.sObject[] { membership });

                            if (srUpdateMemberships[0].success)
                            {
                                SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                UpdateHeadOfHouseholdCommunication();
                                UpdateAccountCreationStep("6");
                                Session["PreviousPage"] = "PaymentInfo";
                                Response.Redirect("HealthHistory.aspx");

                            }
                        }

                        if (rbListPaymentMethod.SelectedIndex == 1)
                        {
                            SForce.Credit_Card_Info__c creditCardInfo = new SForce.Credit_Card_Info__c();

                            creditCardInfo.Card_Type__c = ddlCreditCardType.SelectedItem.Text;
                            creditCardInfo.Name_on_the_card__c = txtNameOnCard.Text;
                            creditCardInfo.Credit_Card_Number__c = txtCreditCardNumber.Text;
                            creditCardInfo.CVV__c = txtCreditCardCVV.Text;

                            DateTime dtExpirationDate = new DateTime(Int32.Parse(ddlExpirationDateYear.SelectedItem.Text), Int32.Parse(ddlExpirationDateMonth.SelectedItem.Text), 1);
                            creditCardInfo.Expiration_Date__c = dtExpirationDate;
                            creditCardInfo.Expiration_Date__cSpecified = true;

                            SForce.SaveResult[] srCreditCardInfo = Sfdcbinding.create(new SForce.sObject[] { creditCardInfo });
                            if (srCreditCardInfo[0].success)
                            {
                            }

                            SForce.Membership__c membership = new SForce.Membership__c();

                            membership.Id = strMembershipId;
                            membership.cmm_Credit_Card_Info__c = srCreditCardInfo[0].id;

                            SForce.SaveResult[] srUpdateMemberships = Sfdcbinding.update(new SForce.sObject[] { membership });

                            if (srUpdateMemberships[0].success)
                            {
                                SaveBillingAddress(chkBillingAddressDifferent.Checked);
                                UpdateHeadOfHouseholdCommunication();
                                UpdateAccountCreationStep("6");
                                Session["PreviousPage"] = "PaymentInfo";
                                Response.Redirect("HealthHistory.aspx");
                            }
                        }
                    }
                }
            }
        }

        protected Boolean UpdateHeadOfHouseholdCommunication ()
        {
            String strQueryForHeadOfHouseholdId = "select Id from Contact where Email = '" + strMemberEmail + "' " +
                                                  "and cmm_Household_Role__c = 'Head of Household'";
            SForce.QueryResult qrHeadOfHouseholdId = Sfdcbinding.query(strQueryForHeadOfHouseholdId);
            if (qrHeadOfHouseholdId.size > 0)
            {
                SForce.Contact ctHeadId = (SForce.Contact)qrHeadOfHouseholdId.records[0];

                SForce.Contact ctHeadOfHousehold = new SForce.Contact();
                ctHeadOfHousehold.Id = ctHeadId.Id;
                //ctHeadOfHousehold.Referral_Source__c = ddlReferredBy.SelectedItem.Text;

                ctHeadOfHousehold.cmm_Solicit_Codes__c = String.Empty;
                ctHeadOfHousehold.cmm_Solicit_Codes__c += "Allow Email; ";
                if (rbListJoinMailing.SelectedIndex == 0) ctHeadOfHousehold.cmm_Solicit_Codes__c += "Allow Postal Mail; ";
                if (rbListAllowMessages.SelectedIndex == 0) ctHeadOfHousehold.cmm_Solicit_Codes__c += "Allow SMS Messages; ";
                if (rbListJoinMailing.SelectedIndex == 1 && rbListAllowMessages.SelectedIndex == 1)
                    ctHeadOfHousehold.cmm_Solicit_Codes__c = "No Communication of Any Kind";

                SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctHeadOfHousehold });
                if (updateResults[0].success)
                {
                    return true;
                }
                else return false;
            }
            else return false;

        }

        protected Boolean UpdateAccountCreationStep (String strStep)
        {
            SForce.Account acctPrimary = new SForce.Account();
            acctPrimary.Id = strAccountId;
            acctPrimary.cmm_Account_Creation_Step_Code__c = strStep;

            SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

            if (srAccount[0].success)
            {
                //Session["PreviousPage"] = "PaymentInfo";
                return true;
                //Response.Redirect("~/HealthHistory.aspx");
            }
            else return false;
        }

        protected Boolean SaveBillingAddress(Boolean bBillingAddressDifferent)
        {
            if (bBillingAddressDifferent)
            {
                String strQueryForHouseholdIdOnEmail = "select Id from Account where cmm_Email__c = '" + strMemberEmail + "'";

                SForce.QueryResult qrHouseholdId = Sfdcbinding.query(strQueryForHouseholdIdOnEmail);

                if (qrHouseholdId.size > 0)
                {
                    SForce.Account Household = qrHouseholdId.records[0] as SForce.Account;

                    SForce.Account acctHousehold = new SForce.Account();
                    acctHousehold.Id = Household.Id;

                    acctHousehold.BillingStreet = txtBillingStreetAddress.Text;
                    acctHousehold.BillingCity = ddlBillingCity.SelectedValue;
                    acctHousehold.BillingState = ddlBillingState.SelectedValue;
                    acctHousehold.BillingPostalCode = txtBillingZipCode.Text;

                    SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { acctHousehold });

                    if (updateResults[0].success)
                    {
                        // The Household billing address added successfully
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        protected void rbListPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbListPaymentMethod.SelectedIndex == 0)
            {
                pnlBankInformation.Visible = true;

                rfvBankAccountType.Enabled = true;
                rfvBankName.Enabled = true;
                rfvAccountOwnerName.Enabled = true;
                rfvRoutingNumber.Enabled = true;
                rfvAccountNumber.Enabled = true;

                pnlCreditCardInformation.Visible = false;
                rfvCreditCardType.Enabled = false;
                rfvNameOnCard.Enabled = false;
                rfvCreditCardNumber.Enabled = false;
                rfvCreditCardCvv.Enabled = false;
                rfvExpirationDateMonth.Enabled = false;
                rfvExpirationDateYear.Enabled = false;

                if (chkBillingAddressDifferent.Checked) pnlPaymentInformation.Height = 1140;
                else pnlPaymentInformation.Height = 960;
            }
            if (rbListPaymentMethod.SelectedIndex == 1)
            {
                pnlBankInformation.Visible = false;

                rfvBankAccountType.Enabled = false;
                rfvBankName.Enabled = false;
                rfvAccountOwnerName.Enabled = false;
                rfvRoutingNumber.Enabled = false;
                rfvAccountNumber.Enabled = false;

                pnlCreditCardInformation.Visible = true;

                rfvCreditCardType.Enabled = true;
                rfvNameOnCard.Enabled = true;
                rfvCreditCardNumber.Enabled = true;
                rfvCreditCardCvv.Enabled = true;
                rfvExpirationDateMonth.Enabled = true;
                rfvExpirationDateYear.Enabled = true;

                ddlExpirationDateYear.Items.Clear();

                int nYear = DateTime.Today.Year;

                for (int i = nYear; i < nYear + 15; i++)
                {
                    ddlExpirationDateYear.Items.Add(new ListItem(i.ToString()));
                }

                if (chkBillingAddressDifferent.Checked) pnlPaymentInformation.Height = 1140;
                else pnlPaymentInformation.Height = 950;
            }
        }

        protected void chkBillingAddressDifferent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBillingAddressDifferent.Checked)
            {
                pnlBillingAddress.Visible = true;
                rfvBillingStreetAddress.Enabled = true;
                rfvBillingZipCode.Enabled = true;
                rfvBillingState.Enabled = true;
                rfvBillingCity.Enabled = true;

                if (rbListPaymentMethod.SelectedIndex == 0) pnlPaymentInformation.Height = 1000;
                if (rbListPaymentMethod.SelectedIndex == 1) pnlPaymentInformation.Height = 1100;
            }
            else if (!chkBillingAddressDifferent.Checked)
            {

                String strQueryForHouseholdIdOnEmail = "select Id, ShippingAddress from Account where cmm_Email__c = '" + strMemberEmail + "'";

                SForce.QueryResult qrHousehold = Sfdcbinding.query(strQueryForHouseholdIdOnEmail);

                if (qrHousehold.size > 0)
                {
                    SForce.Account Household = qrHousehold.records[0] as SForce.Account;

                    SForce.Account acctHousehold = new SForce.Account();
                    acctHousehold.Id = Household.Id;

                    acctHousehold.BillingStreet = Household.ShippingAddress.street;
                    acctHousehold.BillingCity = Household.ShippingAddress.city;
                    acctHousehold.BillingState = Household.ShippingAddress.state;
                    acctHousehold.BillingPostalCode = Household.ShippingAddress.postalCode;

                    //String[] strNoBillingAddress = { "BillingStreet", "BillingCity", "BillingState", "BillingPostalCode" };
                    //acctHousehold.fieldsToNull = strNoBillingAddress;

                    //acctHousehold.BillingStreet = txtBillingStreetAddress.Text;
                    //acctHousehold.BillingCity = ddlBillingCity.SelectedValue;
                    //acctHousehold.BillingState = ddlBillingState.SelectedValue;
                    //acctHousehold.BillingPostalCode = txtBillingZipCode.Text;

                    SForce.SaveResult[] updateBillingAddressResults = Sfdcbinding.update(new SForce.sObject[] { acctHousehold });

                    if (updateBillingAddressResults[0].success)
                    {
                        // The billing address deleted
                    }
                }


                pnlBillingAddress.Visible = false;
                rfvBillingStreetAddress.Enabled = false;
                rfvBillingZipCode.Enabled = false;
                rfvBillingState.Enabled = false;
                rfvBillingCity.Enabled = false;

                if (rbListPaymentMethod.SelectedIndex == 0) pnlPaymentInformation.Height = 1000;
                if (rbListPaymentMethod.SelectedIndex == 1) pnlPaymentInformation.Height = 950;


            }
        }

        protected void txtBillingZipCode_TextChanged(object sender, EventArgs e)
        {

            if (txtBillingZipCode.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtBillingZipCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ddlBillingState.Items.Clear();
                    ddlBillingCity.Items.Clear();

                    ddlBillingState.Items.Add(new ListItem(dr["state_code"].ToString()));
                    ddlBillingState.SelectedIndex = 0;
                    ddlBillingCity.Items.Add(new ListItem(dr["name"].ToString()));
                    ddlBillingCity.SelectedIndex = 0;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
                }
                conn.Close();
            }
        }
    }
}