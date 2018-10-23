using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GoogleRecaptcha;
//using Recaptcha;
//using GoogleRecaptcha;
//using GoogleRecaptchaWebForms;
using System.Drawing;
using System.Net;
using System.Web.Services;

namespace SalesForceWebApp
{
    public partial class AccountSetup : System.Web.UI.Page
    {

        protected static string ReCaptcha_Key = "6LdoqSUUAAAAAB42UxVhGWvT6bVdP4h9ZTzoxN6q";
        protected static string ReCaptcha_Secret = "6LdoqSUUAAAAAO0J4kS-XdZq51SjstAi8V6uD4j7";

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //lblSeverSideCode.Text = "The personal information is submitted.";

            if (Page.IsValid)
            {
                // Send email to User who entered the name and
            }
        }
    }
}