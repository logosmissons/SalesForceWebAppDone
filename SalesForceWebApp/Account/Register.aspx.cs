using System;
using System.Linq;
using System.Web;
using System.Web.UI;
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

namespace SalesForceWebApp.Account
{
    public partial class Register : Page
    {
        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        private SForce.QueryResult queryResultContactId = null;

        private SForce.Contact ctContactId = null;

        private string strQueryContactForContactID = string.Empty;
        private string strSQLSetEmailConfirmed = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Don't erase this line!!!
                ///////////////////////////////////////////////////////////////////////////////
                //lblPersonalInfoUpdateMessage.Text = Context.User.Identity.Name.ToString();
                ///////////////////////////////////////////////////////////////////////////////

                //SetSQLStatementForConfirmEmail();

                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                //InitializedSfdcbinding();
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

        protected void SetSQLStatementForConfirmEmail()
        {
            strQueryContactForContactID = "select id, causeview__PortalUser__c, Email from Contact where Email = '" + Email.Text + "'";
        }

        protected void SetSQLStatementEmailConfirmed()
        {
            strSQLSetEmailConfirmed = "Update AspNetUsers Set EmailConfirmed = @ConfirmEmail where Email = @EmailAddr";
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {

            //SetSQLStatementForConfirmEmail();

            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            //InitializedSfdcbinding();

            //SForce.QueryResult qrConfirmEmail = Sfdcbinding.query(strQueryContactForContactID);

            //if (qrConfirmEmail.size > 0)
            //{
            //SForce.Contact ctConfirmEmail = (SForce.Contact)qrConfirmEmail.records[0];

            //if (ctConfirmEmail.causeview__PortalUser__c == true)
            //{
            //Session["EmailConfirmed"] = "Email Confirmed field is true.";
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            IdentityResult result = manager.Create(user, Password.Text);

            bool bCreateUserSucceeded = result.Succeeded;

            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                //String strConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                SqlCommand cmd = new SqlCommand();
                strSQLSetEmailConfirmed = "Update AspNetUsers Set EmailConfirmed = @bEmailConfirmed where Email = @EmailAddr";
                cmd.CommandText = strSQLSetEmailConfirmed;
                cmd.Connection = cnn;

                cmd.Parameters.Add("@bEmailConfirmed", SqlDbType.Bit);
                cmd.Parameters["@bEmailConfirmed"].Value = true;
                cmd.Parameters.Add("@EmailAddr", SqlDbType.NVarChar);
                cmd.Parameters["@EmailAddr"].Value = Email.Text;

                try
                {
                    cnn.Open();
                    int nRowsAffected = cmd.ExecuteNonQuery();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    ErrorMessage.Text = ex.Message;
                }


                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //IdentityHelper.RedirectToReturnUrl("~\MainForm.aspx", Response);
                Response.Redirect("~/MainForm.aspx", false);
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
                //ErrorMessage.Text = "Error: User password is not created!";
            }
            //}
            //else
            //{
            //    ErrorMessage.Text = "Error: You are not a portal user!";
            //}
            //}
            //else if (qrConfirmEmail.size == 0)
            //{
            //    ErrorMessage.Text = "You are not a CMM member.";
            //}
        }
    }
}