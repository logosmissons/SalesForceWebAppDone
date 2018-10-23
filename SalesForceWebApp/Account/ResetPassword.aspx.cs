using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using SalesForceWebApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace SalesForceWebApp.Account
{
    public partial class ResetPassword : Page
    {
        protected string StatusMessage
        {
            get;
            private set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Email.Text = Request.QueryString["UserEmail"];
        }

        protected void Reset_Click(object sender, EventArgs e)
        {

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (manager != null)
            {
                var user = manager.FindByEmail(Email.Text);
                if (user == null)
                {
                    ErrorMessage.Text = "No user found";
                    return;
                }

                var code = manager.GeneratePasswordResetToken(user.Id);
                if (code == null)
                {
                    ErrorMessage.Text = "No reset token";
                    return;
                }

                var result = manager.ResetPassword(user.Id, code, Password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/ResetPasswordConfirmation.aspx");
                    return;
                }
                else
                {
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                    return;
                }
            }
            else
            {
                ErrorMessage.Text = "Password has not been reset";
                return;
            }

            //if (code != null)
            //{
            //    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //    var user = manager.FindByName(Email.Text);
            //    if (user == null)
            //    {
            //        ErrorMessage.Text = "No user found";
            //        return;
            //    }
            //    var result = manager.ResetPassword(user.Id, code, Password.Text);
            //    if (result.Succeeded)
            //    {
            //        Response.Redirect("~/Account/ResetPasswordConfirmation");
            //        return;
            //    }
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //    return;
            //}





            //ErrorMessage.Text = "An error has occurred";



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // This is working Password Reset code
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //ApplicationDbContext context = new ApplicationDbContext();
            //UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(context);
            //String userId = Email.Text;
            //String newPassword = Password.Text;
            //if (newPassword.Length >= 6)
            //{
            //    UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(store);
            //    String hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);

            //    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            //    SqlCommand cmd = new SqlCommand();

            //    String strSqlResetPassword = "Update AspNetUsers Set PasswordHash = @PasswordHashed where Email = @EmailAddress";
            //    cmd.CommandText = strSqlResetPassword;
            //    cmd.Connection = cnn;

            //    cmd.Parameters.Add("@PasswordHashed", SqlDbType.NVarChar);
            //    cmd.Parameters["@PasswordHashed"].Value = hashedNewPassword;
            //    cmd.Parameters.Add("@EmailAddress", SqlDbType.NVarChar);
            //    cmd.Parameters["@EmailAddress"].Value = userId;

            //    try
            //    {
            //        cnn.Open();
            //        cmd.ExecuteNonQuery();
            //        cnn.Close();
            //        Response.Redirect("~/Account/ResetPasswordConfirmation.aspx");

            //    }
            //    catch (Exception ex)
            //    {
            //        ErrorMessage.Text = ex.Message;
            //    }
            //}
            //else
            //{
            //    ErrorMessage.Text = "Password is at least 6 characters long!";
            //}

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        }
    }
}