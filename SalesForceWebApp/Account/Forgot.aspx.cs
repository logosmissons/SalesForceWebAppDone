using System;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SalesForceWebApp.Models;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace SalesForceWebApp.Account
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Forgot(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user's email address
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(Email.Text);
                if (user == null || !manager.IsEmailConfirmed(user.Id))
                {
                    FailureText.Text = "The user either does not exist or is not confirmed.";
                    ErrorMessage.Visible = true;
                    return;
                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send email with the code and the redirect to reset password page
                //string code = manager.GeneratePasswordResetToken(user.Id);
                //string callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request);
                //manager.SendEmail(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>.");
                loginForm.Visible = false;
                DisplayEmail.Visible = true;

                // have to send email link to member email here
                MailAddress from = new MailAddress("info@christianmutual.org", "Christian Mutual MedAid", System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress("hyungpark@logosmissions.com");

                MailMessage message = new MailMessage(from, to);
                //message.Body = "Please click <a href=\"localhost/SalesforceApp/Account/ResetPassword.aspx\" > here</a> to reset password.";

                //message.Body = "Please click here to reset password.";
                //message.IsBodyHtml = true;
                //message.BodyEncoding = System.Text.Encoding.UTF8;
                //message.Subject = "Testing for Member portal";
                //message.SubjectEncoding = Encoding.UTF8;

                //SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = "localhost";

                //smtpClient.Send(message);
                //String strUserEmail = Email.Text;

                Response.Redirect("ResetPassword.aspx?UserEmail=" + Email.Text);


            }
        }
    }
}