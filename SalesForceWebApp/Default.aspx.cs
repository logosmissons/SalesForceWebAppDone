using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

namespace SalesForceWebApp
{
    public partial class _Default : Page
    {
        protected string strUserName;
        protected string strPasswd;


        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;
        protected SForce.QueryResult queryResult = null;

        protected SForce.QueryResult queryResultTask = null;

        protected string strQuerySelectFromContact;
        protected string strQuerySelectFromMembership;

        protected SForce.QueryResult queryResultMembership;

        protected string strQueryMembershipIdInContact;
        protected string strQueryContact;
        protected string strQueryMembership;
        protected string strQueryContactInnerJoinMembership;
        protected string strQueryContactLeftOuterJoinMembership;

        protected string strQuerySelectFromContactIncludeMembership;

        protected string strQueryMembershipInnerJoinContact;

        protected string strQueryTask;

        SForce.Contact contact = null;

        protected int nCurrentIndex;
        protected int nTotalRecord;
        protected int nIdEdited;

        protected int nCurrentIndexTask;

        protected void Page_Load(object sender, EventArgs e)
        {
            strUserName = "harrispark@kcj777.com.uatsandbox";
            strPasswd = "speed5of5light5";

            if (!IsPostBack)
            {
                nCurrentIndex = 0;
                nTotalRecord = 0;
                nCurrentIndexTask = 0;
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                //Sfdcbinding = new SForce.SforceService();
                //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
                //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
                //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

                //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
                //strQuery = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
                //queryResult = Sfdcbinding.query(strQuery);

                //nTotalRecord = queryResult.size - 1;

                btnCreateNewContact.Enabled = false;
                btnSave.Enabled = false;
                btnEdit.Enabled = false;
                btnUpdate.Enabled = false;
                btnCancel.Enabled = false;
                btnDelete.Enabled = false;

                txtFirstName.ReadOnly = true;
                txtLastName.ReadOnly = true;
                txtStreetAddress.ReadOnly = true;
                txtCity.ReadOnly = true;
                txtState.ReadOnly = true;
                txtZip.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtMobilePhone.ReadOnly = true;
                txtEmail.ReadOnly = true;

                //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
                //strQuerySelectFromMembership = "select Id, Membership.c4g_Membership_Status__c, c4g_Number_of_Gold_Medi_Members__c from Membership nulls last";
                //strQuerySelectFromContactIncludeMembership = "select Contact.Id, Contact.FirstName, Contact.LastName " +
                //                                                "(select Membership.c4g_Membership_Status__c, c4g_Number_of_Gold_Medi_Members__c, " +
                //                                                "c4g_Number_of_Gold_Members__c, c4g_Number_of_Gold_Plus_Members__c, " +
                //                                                "Number_of_Members_Units__c, Paying_Member__c, " +
                //                                                "Registration_Date__c, c4g_Start_Date__c " +
                //                                                "from Membership) from Contact " +
                //                                                "where Contact.Id in (select Membership.ContactId from Membership) nulls last";

            }

            //strQuerySelectFromContact = "select Id, c4g_Membership__c, Individual_ID__c, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";


            strQueryMembershipIdInContact = "select Contact.c4g_Membership__c from Contact order by LastName nulls last";



            strQuerySelectFromMembership = "select Id, Name, c4g_Membership_Status__c from Membership__c";



            /// This is the current working inner join version
            strQueryContactInnerJoinMembership = "select Id, Individual_ID__c, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone, " +
                                                 "(select Name, Id from Memberships__r) from Contact " +
                                                 "where Contact.c4g_Membership__c in (select Membership__c.Id from Membership__c) " +
                                                 "order by Contact.LastName, Contact.FirstName nulls last";


            // SELECT Name, (SELECT Name FROM Job_Applications__r) FROM Position__c

            // This is the Left Outer Join version.
            strQueryContactLeftOuterJoinMembership = "select Id, Individual_ID__c, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone, " +
                                                     "c4g_Membership__r.Name, c4g_Membership__r.Id from Contact " +
                                                     "order by Contact.LastName, Contact.FirstName nulls last";

            strQueryTask = "select Id, Description from Task";
                
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            // This is Contact Inner Join Membership
            //queryResult = Sfdcbinding.query(strQueryContactInnerJoinMembership);

            // This is Contact Outer Join Membership

            queryResultTask = Sfdcbinding.query(strQueryTask);
            queryResult = Sfdcbinding.query(strQueryContactLeftOuterJoinMembership);

            nTotalRecord = queryResult.size - 1;

            ////////////////////////////////////////////////////////////////////////////////
            // Testing for select from membership
            //SForce.SforceService sfdcbindingMembership = new SForce.SforceService();
            //SForce.LoginResult LoginResult = sfdcbindingMembership.login(strUserName, strPasswd);
            //sfdcbindingMembership.Url = LoginResult.serverUrl;
            //sfdcbindingMembership.SessionHeaderValue = new SForce.SessionHeader();

            //sfdcbindingMembership.SessionHeaderValue.sessionId = LoginResult.sessionId;
            //SForce.QueryResult QueryResultMembership = sfdcbindingMembership.query(strQuerySelectFromMembership);



        }


        protected void btnLoad_Click(object sender, EventArgs e)
        {

            //Sfdcbinding = new SForce.SforceService();
            //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            //nTotalRecord = queryResult.size - 1;

            if (queryResult.size > 0)
            {
                contact = (SForce.Contact)queryResult.records[nCurrentIndex];

                txtIndividualId.Text = contact.Individual_ID__c;
                txtId.Text = contact.Id;
                //SForce.Membership__c member = (SForce.Membership__c) contact.Memberships__r.records[0];

                if (contact.c4g_Membership__r != null)
                {
                    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                    txtMembershipNameInContact.Text = contact.c4g_Membership__r.Name;
                }
                else
                {
                    txtMembershipId.Text = "";
                    txtMembershipNameInContact.Text = "";
                }


                //if (contact.c4g_Membership__r != null)
                //{
                //    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                //    txtMembershipInContact.Text = contact.c4g_Membership__r.Name;
                //}
                //else if (contact.c4g_Membership__r == null)
                //{
                //    txtMembershipId.Text = "";
                //    txtMembershipInContact.Text = "";
                //}
              

                //txtMembershipId.Text = member.Id;
                //txtMembershipNameInContact.Text = member.Name; ;
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;

                if (contact.MailingAddress != null)
                {
                    txtStreetAddress.Text = contact.MailingAddress.street;
                    txtCity.Text = contact.MailingAddress.city;
                    txtState.Text = contact.MailingAddress.state;
                    txtZip.Text = contact.MailingAddress.postalCode;
                }
                else if (contact.MailingAddress == null)
                {
                    txtStreetAddress.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                }
                txtMobilePhone.Text = contact.MobilePhone;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;
            }

            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnCreateNewContact.Enabled = true;
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            lblErrorMessage.Text = "";

            ////////////////////////////////////////////////////////////////////////////////
            // Testing for select from membership
            SForce.SforceService sfdcbindingMembership = new SForce.SforceService();
            SForce.LoginResult LoginResult = sfdcbindingMembership.login(strUserName, strPasswd);
            sfdcbindingMembership.Url = LoginResult.serverUrl;
            sfdcbindingMembership.SessionHeaderValue = new SForce.SessionHeader();

            sfdcbindingMembership.SessionHeaderValue.sessionId = LoginResult.sessionId;
            queryResultMembership = sfdcbindingMembership.query(strQuerySelectFromMembership);


            //strQueryMembershipIdInContact

            /////////////////////////////////////
            // Testing for Membership field in Contact
            SForce.QueryResult queryMembershipInContact = sfdcbindingMembership.query(strQueryMembershipIdInContact);
            //SForce.QueryResult queryContactInnerJoinMembership = sfdcbindingMembership.query(strQueryContactInnerJoinMembership);

            if (queryMembershipInContact.size > 0)
            {
                SForce.Contact contactMembership = (SForce.Contact)queryMembershipInContact.records[0];

                txtMembershipInContact.Text = contactMembership.c4g_Membership__c;
            }

            if (queryResultMembership.size > 0)
            {
                SForce.Membership__c membership = (SForce.Membership__c)queryResultMembership.records[0];
                //SForce.Contact contact = (SForce.Contact)queryResultMembership.records[0];

                txtContactId.Text = membership.Id;
                txtMembershipName.Text = membership.Name;
                txtMembershipStatus.Text = membership.c4g_Membership_Status__c;

                //txtContactId.Text = contact.Id;
                //txtMembershipName.Text = contact.Name; ;  //membership.Name;
                //txtMembershipStatus.Text = contact.c4g_Membership_Status__c;   //membership.c4g_Membership_Status__c;
                int nMemberIndex = 0;
                Session["MemberIndex"] = nMemberIndex;
            }

            if (queryResultTask.size > 0)
            {

                SForce.Task task = (SForce.Task)queryResultTask.records[nCurrentIndexTask];

                txtTaskName.Text = task.Id;
                txtTaskComment.Text = task.Description;

                Session["TaskIndex"] = ++nCurrentIndexTask;

                Session["CurrentTaskId"] = task.Id;
            }

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //Sfdcbinding = new SForce.SforceService();
            //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            //nTotalRecord = queryResult.size - 1;

            if (Session["CurrentIndex"] != null)
            {
                nCurrentIndex = (int)Session["CurrentIndex"];
            }

            if (nCurrentIndex > 0)
            {
                nCurrentIndex--;
                contact = (SForce.Contact)queryResult.records[nCurrentIndex];

                if (contact.c4g_Membership__r != null)
                {
                    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                    txtMembershipNameInContact.Text = contact.c4g_Membership__r.Name;
                }
                else
                {
                    txtMembershipId.Text = "";
                    txtMembershipNameInContact.Text = "";
                }



                //if (contact.Memberships__r != null)
                //{
                //    SForce.Membership__c member = (SForce.Membership__c)contact.Memberships__r.records[0];
                //    txtMembershipId.Text = member.Id;
                //    txtMembershipNameInContact.Text = member.Name; ;
                //}
                //else if (contact.Memberships__r == null)
                //{
                //    txtMembershipId.Text = "";
                //    txtMembershipNameInContact.Text = "";
                //}

                //if (contact.c4g_Membership__r != null)
                //{
                //    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                //    txtMembershipInContact.Text = contact.c4g_Membership__r.Name;
                //}
                //else if (contact.c4g_Membership__r == null)
                //{
                //    txtMembershipId.Text = "";
                //    txtMembershipInContact.Text = "";
                //}


                Session["CurrentIndex"] = nCurrentIndex;
                txtId.Text = contact.Id;
                txtIndividualId.Text = contact.Individual_ID__c;

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                if (contact.MailingAddress != null)
                {
                    txtStreetAddress.Text = contact.MailingAddress.street;
                    txtCity.Text = contact.MailingAddress.city;
                    txtState.Text = contact.MailingAddress.state;
                    txtZip.Text = contact.MailingAddress.postalCode;
                }
                else if (contact.MailingAddress == null)
                {
                    txtStreetAddress.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                }

                txtMobilePhone.Text = contact.MobilePhone;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;

                if (btnNext.Enabled == false)
                {
                    btnNext.Enabled = true;
                }

                if (btnLast.Enabled == false)
                {
                    btnLast.Enabled = true;
                }

                if (nCurrentIndex == 0)
                {
                    btnFirst.Enabled = false;
                    btnPrev.Enabled = false;
                }

                string strId = contact.Id;
                Session["CurrentId"] = strId;
            }
            lblErrorMessage.Text = "";



        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            //Sfdcbinding = new SForce.SforceService();
            //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            //nTotalRecord = queryResult.size - 1;

            if (Session["CurrentIndex"] != null)
            {
                nCurrentIndex = (int)Session["CurrentIndex"];
            }

            if (nCurrentIndex < nTotalRecord)
            {
                ++nCurrentIndex;
                contact = (SForce.Contact)queryResult.records[nCurrentIndex];

                if (contact.c4g_Membership__r != null)
                {
                    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                    txtMembershipNameInContact.Text = contact.c4g_Membership__r.Name;
                }
                else
                {
                    txtMembershipId.Text = "";
                    txtMembershipNameInContact.Text = "";
                }

                //if (contact.Memberships__r != null)
                //{
                //    SForce.Membership__c member = (SForce.Membership__c)contact.Memberships__r.records[0];
                //    txtMembershipId.Text = member.Id;
                //    txtMembershipNameInContact.Text = member.Name; ;
                //}
                //else if (contact.Memberships__r == null)
                //{
                //    txtMembershipId.Text = "";
                //    txtMembershipNameInContact.Text = "";
                //}

                //if (contact.c4g_Membership__r != null)
                //{
                //    txtMembershipId.Text = contact.c4g_Membership__r.Id;
                //    txtMembershipInContact.Text = contact.c4g_Membership__r.Name;
                //}
                //else if (contact.c4g_Membership__r == null)
                //{
                //    txtMembershipId.Text = "";
                //    txtMembershipInContact.Text = "";
                //}



                Session["CurrentIndex"] = nCurrentIndex;
                txtId.Text = contact.Id;
                txtIndividualId.Text = contact.Individual_ID__c;

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                if (contact.MailingAddress != null)
                {
                    txtStreetAddress.Text = contact.MailingAddress.street;
                    txtCity.Text = contact.MailingAddress.city;
                    txtState.Text = contact.MailingAddress.state;
                    txtZip.Text = contact.MailingAddress.postalCode;
                }
                else if (contact.MailingAddress == null)
                {
                    txtStreetAddress.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                }

                txtMobilePhone.Text = contact.MobilePhone;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;

                if (btnPrev.Enabled == false)
                {
                    btnPrev.Enabled = true;
                }

                if (btnFirst.Enabled == false)
                {
                    btnFirst.Enabled = true;
                }

                if (nCurrentIndex == nTotalRecord)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }

                string strId = contact.Id;
                Session["CurrentId"] = strId;

            }
            lblErrorMessage.Text = "";



        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            //Sfdcbinding = new SForce.SforceService();
            //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            //nTotalRecord = queryResult.size - 1;

            //nCurrentIndex = 0;

            contact = (SForce.Contact)queryResult.records[nCurrentIndex];
            txtId.Text = contact.Id;
            txtIndividualId.Text = contact.Individual_ID__c;

            if (contact.c4g_Membership__r != null)
            {
                txtMembershipId.Text = contact.c4g_Membership__r.Id;
                txtMembershipNameInContact.Text = contact.c4g_Membership__r.Name;
            }
            else
            {
                txtMembershipId.Text = "";
                txtMembershipNameInContact.Text = "";
            }


            //if (contact.Memberships__r != null)
            //{
            //    SForce.Membership__c member = (SForce.Membership__c)contact.Memberships__r.records[0];
            //    txtMembershipId.Text = member.Id;
            //    txtMembershipNameInContact.Text = member.Name; ;
            //}
            //else if (contact.Memberships__r == null)
            //{
            //    txtMembershipId.Text = "";
            //    txtMembershipNameInContact.Text = "";
            //}

            //if (contact.c4g_Membership__r != null)
            //{
            //    txtMembershipId.Text = contact.c4g_Membership__r.Id;
            //    txtMembershipInContact.Text = contact.c4g_Membership__r.Name;
            //}
            //else if (contact.c4g_Membership__r == null)
            //{
            //    txtMembershipId.Text = "";
            //    txtMembershipInContact.Text = "";
            //}


            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            if (contact.MailingAddress != null)
            {
                txtStreetAddress.Text = contact.MailingAddress.street;
                txtCity.Text = contact.MailingAddress.city;
                txtState.Text = contact.MailingAddress.state;
                txtZip.Text = contact.MailingAddress.postalCode;
            }
            else if (contact.MailingAddress == null)
            {
                txtStreetAddress.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                txtZip.Text = "";
            }

            txtMobilePhone.Text = contact.MobilePhone;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;
            Session["CurrentIndex"] = nCurrentIndex;

            btnPrev.Enabled = false;
            btnFirst.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;

            string strId = contact.Id;
            Session["CurrentId"] = strId;

            btnFirst.Enabled = false;
            lblErrorMessage.Text = "";


        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            //Sfdcbinding = new SForce.SforceService();
            //CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            //Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            //Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

            //Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
            //strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
            //queryResult = Sfdcbinding.query(strQuerySelectFromContact);

            //nTotalRecord = queryResult.size - 1;

            nCurrentIndex = nTotalRecord;

            contact = (SForce.Contact)queryResult.records[nCurrentIndex];
            txtId.Text = contact.Id;
            txtIndividualId.Text = contact.Individual_ID__c;

            if (contact.c4g_Membership__r != null)
            {
                txtMembershipId.Text = contact.c4g_Membership__r.Id;
                txtMembershipNameInContact.Text = contact.c4g_Membership__r.Name;
            }
            else
            {
                txtMembershipId.Text = "";
                txtMembershipNameInContact.Text = "";
            }


            //if (contact.Memberships__r != null)
            //{
            //    SForce.Membership__c member = (SForce.Membership__c)contact.Memberships__r.records[0];
            //    txtMembershipId.Text = member.Id;
            //    txtMembershipNameInContact.Text = member.Name; ;
            //}
            //else if (contact.Memberships__r == null)
            //{
            //    txtMembershipId.Text = "";
            //    txtMembershipNameInContact.Text = "";
            //}

            //            if (contact.c4g_Membership__r != null)
            //{
            //    txtMembershipId.Text = contact.c4g_Membership__r.Id;
            //    txtMembershipInContact.Text = contact.c4g_Membership__r.Name;
            //}
            //else if (contact.c4g_Membership__r == null)
            //{
            //    txtMembershipId.Text = "";
            //    txtMembershipInContact.Text = "";
            //}


            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;

            if (contact.MailingAddress != null)
            {
                txtStreetAddress.Text = contact.MailingAddress.street;
                txtCity.Text = contact.MailingAddress.city;
                txtState.Text = contact.MailingAddress.state;
                txtZip.Text = contact.MailingAddress.postalCode;
            }
            else if (contact.MailingAddress == null)
            {
                txtStreetAddress.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                txtZip.Text = "";
            }

            txtMobilePhone.Text = contact.MobilePhone;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;
            Session["CurrentIndex"] = nCurrentIndex;

            string strId = contact.Id;
            Session["CurrentId"] = strId;

            btnNext.Enabled = false;
            btnLast.Enabled = false;
            btnPrev.Enabled = true;
            btnFirst.Enabled = true;

            lblErrorMessage.Text = "";



        }

        protected void btnCreateNewContact_Click(object sender, EventArgs e)
        {
            txtFirstName.ReadOnly = false;
            txtLastName.ReadOnly = false;
            txtStreetAddress.ReadOnly = false;
            txtCity.ReadOnly = false;
            txtState.ReadOnly = false;
            txtZip.ReadOnly = false;
            txtMobilePhone.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtEmail.ReadOnly = false;


            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtStreetAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtMobilePhone.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";

            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            txtFirstName.Focus();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text == null) lblErrorMessage.Text = "Error: You must enter your last name";
            else
            {
                SForce.Contact sfdcContact = new SForce.Contact();
                sfdcContact.FirstName = txtFirstName.Text;
                sfdcContact.LastName = txtLastName.Text;
                
                sfdcContact.MailingStreet = txtStreetAddress.Text;
                sfdcContact.MailingCity = txtCity.Text;
                sfdcContact.MailingState = txtState.Text;
                sfdcContact.MailingPostalCode = txtZip.Text;
                sfdcContact.MobilePhone = txtMobilePhone.Text;
                sfdcContact.Phone = txtPhone.Text;
                sfdcContact.Email = txtEmail.Text;

                SForce.SforceService sfdcbinding = new SForce.SforceService();
                SForce.LoginResult login_result = null;

                login_result = sfdcbinding.login(strUserName, strPasswd);
                sfdcbinding.Url = login_result.serverUrl;
                sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
                sfdcbinding.SessionHeaderValue.sessionId = login_result.sessionId;
                SForce.SaveResult[] saveResults = sfdcbinding.create(new SForce.sObject[] { sfdcContact });

                if (saveResults[0].success)
                {
                    lblErrorMessage.Text = "You added a new record successfully.";
                    nCurrentIndex = nTotalRecord;
                    Session["CurrentIndex"] = nCurrentIndex;
                    nTotalRecord++;

                    Sfdcbinding = new SForce.SforceService();
                    CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
                    Sfdcbinding.Url = CurrentLoginResult.serverUrl;
                    Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

                    Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
                    strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
                    queryResult = Sfdcbinding.query(strQuerySelectFromContact);

                    nTotalRecord = queryResult.size - 1;

                }
                else
                {
                    lblErrorMessage.Text = saveResults[0].errors[0].message;
                }

                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
                btnLast.Enabled = true;

                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            nCurrentIndex = (int)Session["CurrentIndex"];

            contact = (SForce.Contact)queryResult.records[nCurrentIndex];

            string strId = contact.Id;
            Session["CurrentId"] = strId;

            btnUpdate.Enabled = true;
            btnCancel.Enabled = true;

            txtFirstName.ReadOnly = false;
            txtLastName.ReadOnly = false;
            txtStreetAddress.ReadOnly = false;
            txtCity.ReadOnly = false;
            txtState.ReadOnly = false;
            txtZip.ReadOnly = false;
            txtMobilePhone.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtEmail.ReadOnly = false;

            txtFirstName.Focus();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strId = (string)Session["CurrentId"];

            SForce.Contact contact_update = new SForce.Contact();

            contact_update.Id = strId;

            contact_update.FirstName = txtFirstName.Text;
            contact_update.LastName = txtLastName.Text;

            contact_update.MailingStreet = txtStreetAddress.Text;
            contact_update.MailingCity = txtCity.Text;
            contact_update.MailingState = txtState.Text;
            contact_update.MailingPostalCode = txtZip.Text;
            contact_update.MobilePhone = txtMobilePhone.Text;
            contact_update.Phone = txtPhone.Text;
            contact_update.Email = txtEmail.Text;

            SForce.SforceService sfdcbinding = new SForce.SforceService();

            SForce.LoginResult login_result = null;

            login_result = sfdcbinding.login(strUserName, strPasswd);
            sfdcbinding.Url = login_result.serverUrl;
            sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            sfdcbinding.SessionHeaderValue.sessionId = login_result.sessionId;

            SForce.SaveResult[] saveResults = sfdcbinding.update(new SForce.sObject[] { contact_update });

            if (saveResults[0].success)
            {
                lblErrorMessage.Text = "The record id: " + saveResults[0].id + " is updated successfully.";

                Sfdcbinding = new SForce.SforceService();
                CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
                Sfdcbinding.Url = CurrentLoginResult.serverUrl;
                Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

                Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
                strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
                queryResult = Sfdcbinding.query(strQuerySelectFromContact);

                nTotalRecord = queryResult.size - 1;
            }
            else
            {
                lblErrorMessage.Text = "Error: " + saveResults[0].errors[0].message;
            }

            btnCancel.Enabled = false;
            btnUpdate.Enabled = false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strId = (string) Session["CurrentId"];

            String[] ids = new String[] { strId };

            SForce.SforceService sfdcbinding = new SForce.SforceService();

            SForce.LoginResult login_result = null;

            login_result = sfdcbinding.login(strUserName, strPasswd);
            sfdcbinding.Url = login_result.serverUrl;
            sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            sfdcbinding.SessionHeaderValue.sessionId = login_result.sessionId;

            SForce.DeleteResult[] deleteResults = sfdcbinding.delete(ids);
            SForce.DeleteResult deleteResult = deleteResults[0];

            if (deleteResult.success)
            {
                lblErrorMessage.Text = "The record id: " + deleteResult.id + " is deleted successfully.";
                int nCurrentIndex = (int)Session["CurrentIndex"];

                nCurrentIndex--;
                //contact = (SForce.Contact)queryResult.records[nCurrentIndex];

                Sfdcbinding = new SForce.SforceService();
                CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
                Sfdcbinding.Url = CurrentLoginResult.serverUrl;
                Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();

                Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
                strQuerySelectFromContact = "select Id, FirstName, LastName, MailingAddress, Email, MobilePhone, Phone from Contact order by LastName nulls last";
                queryResult = Sfdcbinding.query(strQuerySelectFromContact);

                nTotalRecord = queryResult.size - 1;

                contact = (SForce.Contact)queryResult.records[nCurrentIndex];

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;

                if (contact.MailingAddress != null)
                {
                    txtStreetAddress.Text = contact.MailingAddress.street;
                    txtCity.Text = contact.MailingAddress.city;
                    txtState.Text = contact.MailingAddress.state;
                    txtZip.Text = contact.MailingAddress.postalCode;
                }
                else if (contact.MailingAddress == null)
                {
                    txtStreetAddress.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                }

                txtMobilePhone.Text = contact.MobilePhone;
                txtPhone.Text = contact.Phone;
                txtEmail.Text = contact.Email;
                Session["CurrentIndex"] = nCurrentIndex;

                if (nCurrentIndex == 0)
                {
                    btnPrev.Enabled = false;
                    btnFirst.Enabled = false;
                }

                strId = contact.Id;
                Session["CurrentId"] = strId;

            }
            else
            {
                lblErrorMessage.Text = "Error: " + deleteResult.errors[0].message;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int nCurrentIndex = (int)Session["CurrentIndex"];

            contact = (SForce.Contact)queryResult.records[nCurrentIndex];
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;

            if (contact.MailingAddress != null)
            {
                txtStreetAddress.Text = contact.MailingAddress.street;
                txtCity.Text = contact.MailingAddress.city;
                txtState.Text = contact.MailingAddress.state;
                txtZip.Text = contact.MailingAddress.postalCode;
            }
            else if (contact.MailingAddress == null)
            {
                txtStreetAddress.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                txtZip.Text = "";
            }

            txtMobilePhone.Text = contact.MobilePhone;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;

            Session["CurrentIndex"] = nCurrentIndex;

            string strId = contact.Id;
            Session["CurrentId"] = strId;

            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtStreetAddress.ReadOnly = true;
            txtCity.ReadOnly = true;
            txtState.ReadOnly = true;
            txtZip.ReadOnly = true;
            txtMobilePhone.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtEmail.ReadOnly = true;

            if (nCurrentIndex > 0)
            {
                btnFirst.Enabled = true;
                btnPrev.Enabled = true;
            }
            else if (nCurrentIndex < nTotalRecord)
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }

            btnUpdate.Enabled = false;
            btnCancel.Enabled = false;

        }

        protected void btnMemberPrev_Click(object sender, EventArgs e)
        {

            SForce.SforceService sfdcbindingMembership = new SForce.SforceService();
            SForce.LoginResult LoginResult = sfdcbindingMembership.login(strUserName, strPasswd);
            sfdcbindingMembership.Url = LoginResult.serverUrl;
            sfdcbindingMembership.SessionHeaderValue = new SForce.SessionHeader();

            sfdcbindingMembership.SessionHeaderValue.sessionId = LoginResult.sessionId;
            queryResultMembership = sfdcbindingMembership.query(strQuerySelectFromMembership);


            int nMemberIndex = (int)Session["MemberIndex"];

            if (nMemberIndex > 0)
            {
                nMemberIndex--;
            }

            Session["MemberIndex"] = nMemberIndex;

            SForce.Membership__c membership = (SForce.Membership__c)queryResultMembership.records[nMemberIndex];
            txtContactId.Text = membership.Id; 
            txtMembershipName.Text = membership.Name;
            txtMembershipStatus.Text = membership.c4g_Membership_Status__c;
        }

        protected void btnMemberNext_Click(object sender, EventArgs e)
        {

            SForce.SforceService sfdcbindingMembership = new SForce.SforceService();
            SForce.LoginResult LoginResult = sfdcbindingMembership.login(strUserName, strPasswd);
            sfdcbindingMembership.Url = LoginResult.serverUrl;
            sfdcbindingMembership.SessionHeaderValue = new SForce.SessionHeader();

            sfdcbindingMembership.SessionHeaderValue.sessionId = LoginResult.sessionId;
            queryResultMembership = sfdcbindingMembership.query(strQuerySelectFromMembership);


            int nMemberIndex = (int)Session["MemberIndex"];

            if (nMemberIndex < queryResultMembership.size - 1)
            {
                nMemberIndex++;
            }

            Session["MemberIndex"] = nMemberIndex;

            SForce.Membership__c membership = (SForce.Membership__c)queryResultMembership.records[nMemberIndex];
            txtContactId.Text = membership.Id;
            txtMembershipName.Text = membership.Name;
            txtMembershipStatus.Text = membership.c4g_Membership_Status__c;

        }

        protected void btnTaskFirst_Click(object sender, EventArgs e)
        {
            if (queryResultTask.size > 0)
            {
                nCurrentIndexTask = 0;

                SForce.Task task = (SForce.Task)queryResultTask.records[nCurrentIndexTask];

                txtTaskName.Text = task.Id;
                txtTaskComment.Text = task.Description;

                Session["TaskIndex"] = nCurrentIndexTask;

                Session["CurrentTaskId"] = task.Id;

            }

        }

        protected void btnTaskPrev_Click(object sender, EventArgs e)
        {
            if (queryResultTask.size > 0)
            {
                nCurrentIndexTask = (int)Session["TaskIndex"];

                if (nCurrentIndexTask > 0) nCurrentIndexTask--;

                SForce.Task task = (SForce.Task)queryResultTask.records[nCurrentIndexTask];

                txtTaskName.Text = task.Id;
                txtTaskComment.Text = task.Description;

                Session["TaskIndex"] = nCurrentIndexTask;
                Session["CurrentTaskId"] = task.Id;

            }

        }

        protected void btnTaskNext_Click(object sender, EventArgs e)
        {
            if (queryResultTask.size > 0)
            {
                nCurrentIndexTask = (int)Session["TaskIndex"];

                if (nCurrentIndexTask < queryResultTask.size - 1) nCurrentIndexTask++;

                SForce.Task task = (SForce.Task)queryResultTask.records[nCurrentIndexTask];

                txtTaskName.Text = task.Id;
                txtTaskComment.Text = task.Description;

                Session["TaskIndex"] = nCurrentIndexTask;
                Session["CurrentTaskId"] = task.Id;

            }

        }

        protected void btnTaskLast_Click(object sender, EventArgs e)
        {
            if (queryResultTask.size > 0)
            {
                nCurrentIndexTask = queryResultTask.size - 1;

                SForce.Task task = (SForce.Task)queryResultTask.records[nCurrentIndexTask];

                txtTaskName.Text = task.Id;
                txtTaskComment.Text = task.Description;

                Session["TaskIndex"] = nCurrentIndexTask;
                Session["CurrentTaskId"] = task.Id;

            }
        }

        protected void btnEditComment_Click(object sender, EventArgs e)
        {
            txtTaskComment.Focus();
        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {

            //string strId = (string)Session["CurrentId"];

            SForce.Task task_update = new SForce.Task();

            string Id = (string)Session["CurrentTaskId"];

            task_update.Id = Id;
            task_update.Description = txtTaskComment.Text;

            //contact_update.Id = strId;

            //contact_update.FirstName = txtFirstName.Text;
            //contact_update.LastName = txtLastName.Text;

            //contact_update.MailingStreet = txtStreetAddress.Text;
            //contact_update.MailingCity = txtCity.Text;
            //contact_update.MailingState = txtState.Text;
            //contact_update.MailingPostalCode = txtZip.Text;
            //contact_update.MobilePhone = txtMobilePhone.Text;
            //contact_update.Phone = txtPhone.Text;
            //contact_update.Email = txtEmail.Text;



            SForce.SforceService sfdcbinding = new SForce.SforceService();

            SForce.LoginResult login_result = null;

            login_result = sfdcbinding.login(strUserName, strPasswd);
            sfdcbinding.Url = login_result.serverUrl;
            sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            sfdcbinding.SessionHeaderValue.sessionId = login_result.sessionId;

            SForce.SaveResult[] saveResults = sfdcbinding.update(new SForce.sObject[] { task_update });

            if (saveResults[0].success) lblTaskCommentUpdateResult.Text = "Success!";
            else lblTaskCommentUpdateResult.Text = saveResults[0].errors[0].message;

        }
    }
}