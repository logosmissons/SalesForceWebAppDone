using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;


namespace SalesForceWebApp
{
    public partial class PersonalInfo : System.Web.UI.Page
    {

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        //public String FirstName
        //{
        //    get; set;
        //}

        //public String LastName
        //{
        //    get; set;
        //}

        //public String Email
        //{
        //    get; set;
        //}

        protected void Page_Load(object sender, EventArgs e)
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            InitializedSfdcbinding();
            //Response.Write("<div id='mydiv' >");
            //Response.Write("_");
            //Response.Write("</div>");
            //Response.Write("<script>mydiv.innerText = '';</script>");


            //Response.Write("<script language=javascript>;");
            //Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
            //Response.Write("{var output; output = 'Loading';dots++;if(dots>=dotmax)dots=1;");
            //Response.Write("for(var x = 0;x < dots;x++){output += '.';}mydiv.innerText =  output;}");
            //Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; window.setInterval('ShowWait()', 1000);}");
            //Response.Write("function HideWait(){mydiv.style.visibility = 'hidden';window.clearInterval();}");
            //Response.Write("StartShowWait();</script>");
            //Response.Flush();
            //Thread.Sleep(10000) ;


            //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void btnConfirmPersonalInfo_Click(object sender, EventArgs e)
        {

            // Send the email to confirm that the user entered correct email address and send the link to CreateAccount page to allow user to create account
            //SmtpClient smtpClient = new SmtpClient("mail.logosmissions.com");

            //smtpClient.Port = 25;
            //smtpClient.Credentials = new System.Net.NetworkCredential("hyungpark", "0particle8");
            //smtpClient.EnableSsl = true;
            //smtpClient.UseDefaultCredentials = false;


            //MailAddress from = new MailAddress("hyungpark@logosmissions.com", "Christian Mutual MedAid", System.Text.Encoding.UTF8);
            //MailAddress to = new MailAddress(txtEmail.Text);

            //MailMessage message = new MailMessage(from, to);
            //message.Body = "This is test email from Christian Mutual MedAid";
            //message.BodyEncoding = System.Text.Encoding.UTF8;
            //message.Subject = "CMM Confirmation Email";
            //message.SubjectEncoding = System.Text.Encoding.UTF8;

            //smtpClient.Send(message);

            //String strQueryForEmail = "select Id from Account where cmm_Email__c = '" + txtEmail.Text + "'";
            //SForce.QueryResult qrAccountForEmail = Sfdcbinding.query(strQueryForEmail);

            //if (qrAccountForEmail.size == 0)
            //{

            Session["FirstName"] = txtFirstName.Text;
            Session["LastName"] = txtLastName.Text;
            Session["Email"] = txtEmail.Text;

            //FirstName = txtFirstName.Text;
            //LastName = txtLastName.Text;
            //Email = txtEmail.Text;

            //Server.Transfer("~/CreateAccount.aspx", true);

            //String strAccountName = strLastName + " (" + strFirstName + ") Household";

            String strAccountName = txtLastName.Text + " (" + txtFirstName.Text + ") Household";

            Session["AccountName"] = strAccountName;

            SForce.Account acctPrimary = new SForce.Account();
            acctPrimary.Name = strAccountName;
            acctPrimary.RecordType = new SForce.RecordType();
            acctPrimary.RecordType.Name = "Household";
            acctPrimary.cmm_Account_Creation_Step_Code__c = "1";
            acctPrimary.cmm_Email__c = txtEmail.Text;

            SForce.SaveResult[] srAcctPrimary = Sfdcbinding.create(new SForce.sObject[] { acctPrimary });

            if (srAcctPrimary[0].success)
            {
                Session["AccountId"] = srAcctPrimary[0].id;
                Session["PreviousPage"] = "PersonalInfo";
                Response.Redirect("~/CreateAccount.aspx");
            }
            else
            {
                // Error: creating account failed
            }
            //}
            //else if (qrAccountForEmail.size > 0)
            //{
            // Display message that indicates the email is already in salesforce database

            //String strQueryForAcctInfo = "select cmm_Account_Creation_Step_Code__c, Id from Account where cmm_Email__c = '" + txtEmail.Text + "'";

            //SForce.QueryResult qrAcctInfo = Sfdcbinding.query(strQueryForAcctInfo);

            //if (qrAcctInfo.size > 0)
            //{
            //    SForce.Account acctAcctInfo = qrAcctInfo.records[0] as SForce.Account;

            //    Session["FirstName"] = txtFirstName.Text;
            //    Session["LastName"] = txtLastName.Text;
            //    Session["Email"] = txtEmail.Text;

            //    Session["AccountId"] = acctAcctInfo.Id;

            //    String strAccountName = txtLastName.Text + " (" + txtFirstName.Text + ") Household";

            //    Session["AccountName"] = strAccountName;
            //    //Session["AccountId"] = acctAcctInfo.Id;
            //    Session["PreviousPage"] = "PersonalInfo";



            //    switch (acctAcctInfo.cmm_Account_Creation_Step_Code__c)
            //    {
            //        case "1":
            //            Response.Redirect("~/CreateAccount.aspx");
            //            break;
            //        case "2":
            //            Response.Redirect("~/PersonalDetails.aspx");
            //            break;
            //        case "3":
            //            Response.Redirect("~/FamilyDetails.aspx");
            //            break;
            //        case "4":
            //            Response.Redirect("~/MembershipDetails.aspx");
            //            break;
            //        case "5":
            //            Response.Redirect("~/PaymentInfo.aspx");
            //            break;
            //        case "6":
            //            Response.Redirect("~/HealthHistory.aspx");
            //            break;
            //        case "7":
            //            Response.Redirect("~/Agreement.aspx");
            //            break;
            //        case "8":
            //            // Show the message informing the user is already registered, tell the user to log on instead of registering
            //            Response.Redirect("~/Account/Login.aspx");
            //            break;
            //    }

            //}
            //}
        }
    }
}