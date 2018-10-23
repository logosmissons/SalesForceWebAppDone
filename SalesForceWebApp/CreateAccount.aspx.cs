using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Services;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using SalesForceWebApp.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

using System.Security.Principal;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;


namespace SalesForceWebApp
{
    public partial class CreateAccount : System.Web.UI.Page
    {

        protected static string ReCaptcha_Key = "6LdoqSUUAAAAAB42UxVhGWvT6bVdP4h9ZTzoxN6q";
        protected static string ReCaptcha_Secret = "6LdoqSUUAAAAAO0J4kS-XdZq51SjstAi8V6uD4j7";

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        //private SForce.QueryResult queryResultContactId = null;

        //private SForce.Contact ctContactId = null;

        private string strQueryContactForContactID = string.Empty;
        private string strSQLSetEmailConfirmed = string.Empty;

        protected string strAccountName = null;
        protected String strAccountId = null;

        public String FirstName
        {
            get
            {
                return txtFirstName.Text;
            }
            set
            {
                txtFirstName.Text = value;
            }
        }

        public String LastName
        {
            get
            {
                return txtLastName.Text;
            }
            set
            {
                txtLastName.Text = value;
            }
        }

        public String MemberEmail
        {
            get
            {
                return txtEmail.Text;
            }
            set
            {
                txtEmail.Text = value;
                txtEmail.ReadOnly = true;
            }
        }

        public String AccountName
        {
            get
            {
                return strAccountName;
            }
            set
            {
                strAccountName = value;
            }

        }

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        //protected void SetSQLStatementForConfirmEmail()
        //{
        //    strQueryContactForContactID = "select id, causeview__PortalUser__c, Email from Contact where Email = '" + MemberEmail + "'";
        //}

        protected void SetSQLStatementEmailConfirmed()
        {
            strSQLSetEmailConfirmed = "Update AspNetUsers Set EmailConfirmed = @ConfirmEmail where Email = @EmailAddr";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                //if (PreviousPage != null)
                //{
                //    FirstName = PreviousPage.FirstName;
                //    LastName = PreviousPage.LastName;
                //    MemberEmail = PreviousPage.Email;
                //}

                FirstName = (string)Session["FirstName"];
                LastName = (string)Session["LastName"];
                MemberEmail = (string)Session["Email"];

                //AccountName = LastName + " (" + FirstName + ") Household";

                //Session["AccountName"] = AccountName;

                //lblErrorMessage.Text = AccountName;

                //SetSQLStatementForConfirmEmail();

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;


            }

            InitializedSfdcbinding();
            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];
            else
            {
                String strQueryForHousehold = "select Id from Account where cmm_Email__c = '" + txtEmail.Text + "'";

                SForce.QueryResult qrAcctHousehold = Sfdcbinding.query(strQueryForHousehold);

                if (qrAcctHousehold.size > 0) strAccountId = qrAcctHousehold.records[0].Id;

            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //lblSeverSideCode.Text = "The personal information is submitted.";

            //if (Page.IsValid)
            //{


            /////// Should be uncommented for the application to work

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = MemberEmail, Email = MemberEmail };
            IdentityResult result = manager.Create(user, txtPassword.Text);

            bool bCreateUserSucceeded = result.Succeeded;

            if (result.Succeeded)
            {

                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand();
                strSQLSetEmailConfirmed = "Update AspNetUsers Set EmailConfirmed = @bEmailConfirmed where Email = @EmailAddr";
                cmd.CommandText = strSQLSetEmailConfirmed;
                cmd.Connection = cnn;

                cmd.Parameters.Add("@bEmailConfirmed", SqlDbType.Bit);
                cmd.Parameters["@bEmailConfirmed"].Value = true;
                cmd.Parameters.Add("@EmailAddr", SqlDbType.NVarChar);
                cmd.Parameters["@EmailAddr"].Value = MemberEmail;

                try
                {
                    cnn.Open();
                    int nRowsAffected = cmd.ExecuteNonQuery();
                    cnn.Close();

                }
                catch (Exception ex)
                {
                    //ErrorMessage.Text = ex.Message;
                }
                //signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            //}

            /// Should be uncommented up to this line
            ////////////////////////////////////////////////////////////////

            //lblReCaptchaError.Text = "Recaptcha Error!";

            //lblErrorMessage.Text = "This is ReCaptcha succeeded";

            //if (reCaptcha.Validate())
            //{
            //    lblReCaptchaError.Text = "ReCaptcha validation succeeded";
            //}
            //else
            //{
            //    lblReCaptchaError.Text = "ReCaptcha validation failed";
            //}

            //String strQueryForAcctId = "select Id from Account where cmm_Email__c = '" + MemberEmail + "'";

            //SForce.QueryResult qrAccountId = Sfdcbinding.query(strQueryForAcctId);

            //if (qrAccountId.size > 0)
            //{
            //SForce.Account acctPrimaryId = qrAccountId.records[0] as SForce.Account;

            SForce.Account acctPrimary = new SForce.Account();
            if (strAccountId != null) acctPrimary.Id = strAccountId;
            acctPrimary.cmm_Account_Creation_Step_Code__c = "2";

            SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

            if (srAccount[0].success)
            {

                SForce.Contact ctPrimary = new SForce.Contact();

                ctPrimary.cmm_Household__c = strAccountId;
                ctPrimary.Email = txtEmail.Text;
                ctPrimary.LastName = txtLastName.Text;
                ctPrimary.FirstName = txtFirstName.Text;
                ctPrimary.cmm_Household_Role__c = "Head of Household";

                SForce.SaveResult[] srPrimary = Sfdcbinding.create(new SForce.sObject[] { ctPrimary });

                if (srPrimary[0].success)
                {
                    SForce.Account acctHousehold = new SForce.Account();

                    acctHousehold.Id = strAccountId;
                    acctHousehold.cmm_Contact__c = srPrimary[0].id;

                    SForce.SaveResult[] updateHousehold = Sfdcbinding.update(new SForce.sObject[] { acctHousehold });

                    if (updateHousehold[0].success)
                    {

                    }

                    SForce.Contact ctUpdate = new SForce.Contact();
                    ctUpdate.Id = srPrimary[0].id;
                    ctUpdate.cmm_Household__c = strAccountId;

                    SForce.SaveResult[] srUpdatePrimary = Sfdcbinding.update(new SForce.sObject[] { ctUpdate });

                    if (srUpdatePrimary[0].success)
                    {

                    }
                }

                var signInResult = signInManager.PasswordSignIn(txtEmail.Text, txtPassword.Text, false, shouldLockout: false);

                switch (signInResult)
                {
                    case SignInStatus.Success:
                        Session["PreviousPage"] = "CreateAccount";
                        Response.Redirect("~/PersonalDetails.aspx");
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("~/Account/Lockout.aspx");
                        break;
                    case SignInStatus.Failure:
                        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        Response.Redirect("~/Account/Login.aspx");
                        break;
                }
            }
        }
    }
}