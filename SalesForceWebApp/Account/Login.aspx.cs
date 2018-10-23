using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SalesForceWebApp.Models;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;


namespace SalesForceWebApp.Account
{
    public partial class Login : Page
    {

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        private string strQueryContactForPortalUser = null;

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;


        protected void Page_Load(object sender, EventArgs e)
        {

            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            InitializedSfdcbinding();

            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                //var user = new ApplicationUser() { UserName = MemberEmail, Email = MemberEmail };
                //IdentityResult result = manager.Create(user, txtPassword.Text);


                // Confirm the user email whether or not the user is portal user
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                //SetSQLStatementForPortalUser();
                //InitializedSfdcbinding();

                //SForce.QueryResult qrPortalUser = Sfdcbinding.query(strQueryContactForPortalUser);
                //if (qrPortalUser.size > 0)
                //{
                //SForce.Contact ctPortalUser = (SForce.Contact)qrPortalUser.records[0];

                //if (ctPortalUser.causeview__PortalUser__c == true)
                //{
                // Code for Login and Response.Redirect to MainForm.aspx page should be here

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                HttpBrowserCapabilities browser = Request.Browser;

                switch (result)
                {
                    case SignInStatus.Success:
                        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        //if (browser.Type.Contains("Firefox") ||
                        //    browser.Type.Contains("Chrome") ||
                        //    browser.Type.Contains("Edge") ||
                        //    browser.Type.Contains("InternetExplorer11") ||
                        //    browser.Type.ToUpper().Contains("IE"))
                        //{
                        //    Response.Redirect("~/MainForm.aspx", false);
                        //}
                        //if (browser.Type.Contains("Safari")) Response.Redirect("~/MainFormSafari.aspx", false);
                        String strQueryForAccountCreationStep = "select cmm_Account_Creation_Step_Code__c, Id from Account where cmm_Email__c = '" + Email.Text + "'";

                        SForce.QueryResult qrAcctCreationStep = Sfdcbinding.query(strQueryForAccountCreationStep);
                    
                        if (qrAcctCreationStep.size > 0)
                        {
                            SForce.Account acctCreationStep = qrAcctCreationStep.records[0] as SForce.Account;

                            Session["Email"] = Email.Text;
                            Session["AccountId"] = acctCreationStep.Id;
                            Session["PreviousPage"] = "Login";

                            switch (acctCreationStep.cmm_Account_Creation_Step_Code__c)
                            {
                                case "2":
                                    Response.Redirect("~/PersonalDetails.aspx");
                                    break;
                                case "3":
                                    Response.Redirect("~/FamilyDetails.aspx");
                                    break;
                                case "4":
                                    Response.Redirect("~/MembershipDetails.aspx");
                                    break;
                                case "5":
                                    Response.Redirect("~/PaymentInfo.aspx");
                                    break;
                                case "6":
                                    Response.Redirect("~/HealthHistory.aspx");
                                    break;
                                case "7":
                                    Response.Redirect("~/Agreement.aspx");
                                    break;
                                case "8":
                                    // Show the message informing the user is already registered, tell the user to log on instead of registering
                                    Response.Redirect("~/MainForm.aspx");
                                    break;
                            }
                        }

                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("/Account/Lockout");
                        break;
                    //case SignInStatus.RequiresVerification:
                    //    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                    //                                    Request.QueryString["ReturnUrl"],
                    //                                    RememberMe.Checked),
                    //                      true);
                    //    break;
                    case SignInStatus.Failure:
                        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        //Response.Redirect("~/Account/Login.aspx");
                        lblFailureText.Text = "Invalid login attempt";
                        pnlLoginFailureText.Visible = true;

                        break;
                    default:
                        lblFailureText.Text = "Invalid login attempt";
                        pnlLoginFailureText.Visible = true;
                        break;
                }
                //}
                //else
                //{
                //    FailureText.Text = "You are not a portal user";
                //    ErrorMessage.Visible = true;
                //}
                //}
            }
        }

        //protected void SetSQLStatementForPortalUser()
        //{
        //    strQueryContactForPortalUser = "select id, causeview__PortalUser__c, Email from Contact where Email = '" + Email.Text + "'";
        //}
 
    }
}