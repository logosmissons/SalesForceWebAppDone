using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.Threading;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;


namespace SalesForceWebApp
{

    public enum HouseholdRoles { Primary, Spouse, Child }

    public class MemberSmokingDrugAlcohol
    {
        public int AccountCreationStepCode { get; set; }
        public String AccountId { get; set; }
        public HouseholdRoles HouseholdRole { get; set; }
        public String ContactId { get; set; }
        public String Name { get; set; }
        public Boolean bCurrentSmoker { get; set; }
        public Boolean bFormerSmoker { get; set; }
        public Boolean bCurrentDrug { get; set; }
        public Boolean bFormerDrug { get; set; }
        public Boolean bAlcohol { get; set; }
    }

    public partial class PersonalDetails : System.Web.UI.Page
    {
        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;
        protected SForce.SaveResult[] saveResults = null;

        protected String strQueryForState = null;
        protected String strConnectionString = null;

        protected string strFirstName = null;
        protected string strLastName = null;
        protected string strMiddleName = null;
        protected string strEmail = null;

        protected String strAccountId = null;

        protected List<MemberSmokingDrugAlcohol> lstMemberSDA = new List<MemberSmokingDrugAlcohol>();

        //protected List<Smoking_Drug_Alcohol> lstSmoking_Drug_Alcohol = new List<Smoking_Drug_Alcohol>();
        protected void Page_Init(object sender, EventArgs e)
        {
            InitializedSfdcbinding();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

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


            //strEmail = (string)Session["Email"];
            //strLastName = (string)Session["LastName"];
            //strFirstName = (string)Session["FirstName"];

            //txtEmail.Text = strEmail;
            //txtLastName.Text = strLastName;
            //txtFirstName.Text = strFirstName;

            if ((String)Session["Email"] != String.Empty)
            {
                strEmail = (String)Session["Email"];
                txtEmail.Text = strEmail;
            }
            if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];
            else
            {
                String strQueryForAccountIdOnEmail = "select Id from Account where cmm_Email__c = '" + strEmail + "'";

                SForce.QueryResult qrAccountId = Sfdcbinding.query(strQueryForAccountIdOnEmail);

                if (qrAccountId.size > 0)
                {
                    SForce.Account acctHousehold = qrAccountId.records[0] as SForce.Account;

                    strAccountId = acctHousehold.Id;
                }
            }

            //if (!IsPostBack)
            //{


            if ((String)Session["LastName"] != null && (String)Session["FirstName"] != null)
            {
                if ((String)Session["LastName"] != String.Empty) txtLastName.Text = (String)Session["LastName"];
                if ((String)Session["FirstName"] != String.Empty) txtFirstName.Text = (String)Session["FirstName"];

            }
            //else if ((String)Session["LastName"] == null || (String)Session["FirstName"] == null)
            //{

            if (!IsPostBack)
            {
                String strQueryForPrimaryContactInfo = "select Email, Title, LastName, FirstName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c, " +
                                                           "Phone, HomePhone, MobilePhone, OtherPhone from Contact " +
                                                           "where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Head of Household'";
                

                SForce.QueryResult qrPrimaryContactInfo = Sfdcbinding.query(strQueryForPrimaryContactInfo);

                if (qrPrimaryContactInfo.size > 0)
                {
                    SForce.Contact ctPrimary = qrPrimaryContactInfo.records[0] as SForce.Contact;

                    txtEmail.Text = ctPrimary.Email;
                    switch (ctPrimary.Title)
                    {
                        case "Jr.":
                            ddlTitle.SelectedIndex = 1;
                            break;
                        case "Mr.":
                            ddlTitle.SelectedIndex = 2;
                            break;
                        case "Mrs.":
                            ddlTitle.SelectedIndex = 3;
                            break;
                        case "Ms.":
                            ddlTitle.SelectedIndex = 4;
                            break;
                    }
                    txtLastName.Text = ctPrimary.LastName;
                    txtFirstName.Text = ctPrimary.FirstName;
                    txtMiddleName.Text = ctPrimary.MiddleName;

                    int? nYear = null;
                    int? nMonth = null;
                    int? nDay = null;

                    if (ctPrimary.Birthdate != null)
                    {
                        nYear = ctPrimary.Birthdate.Value.Year;
                        nMonth = ctPrimary.Birthdate.Value.Month;
                        nDay = ctPrimary.Birthdate.Value.Day;
                        DateTime dtPrimaryBirthdate = new DateTime((int)nYear, (int)nMonth, (int)nDay);
                        txtDateOfBirth.Text = dtPrimaryBirthdate.ToString("MM/dd/yyyy");
                    }

                    if (ctPrimary.cmm_Gender__c == "Male") rbGenderList.SelectedIndex = 0;
                    else if (ctPrimary.cmm_Gender__c == "Female") rbGenderList.SelectedIndex = 1;

                    if (ctPrimary.Social_Security_Number__c != null)
                    {
                        txtSSN.Text = ctPrimary.Social_Security_Number__c;
                        txtSSN.ReadOnly = true;
                        revSSN.Enabled = false;
                    }

                    if (ctPrimary.Phone != null) txtTelephone1.Text = ctPrimary.Phone;
                    if (ctPrimary.MobilePhone != null) txtTelephone2.Text = ctPrimary.MobilePhone;
                    if (ctPrimary.OtherPhone != null) txtTelephone3.Text = ctPrimary.OtherPhone;

                }


                String strQueryForHousehold = "select ShippingAddress from Account where Id = '" + strAccountId + "' and cmm_Email__c = '" + strEmail + "'";

                SForce.QueryResult qrHousehold = Sfdcbinding.query(strQueryForHousehold);

                if (qrHousehold.size > 0)
                {
                    SForce.Account acctHousehold = qrHousehold.records[0] as SForce.Account;

                    if (acctHousehold.ShippingAddress != null)
                    {

                        txtAddress.Text = acctHousehold.ShippingAddress.street;
                        txtZipCode.Text = acctHousehold.ShippingAddress.postalCode;

                        InitializeStateNames();
                        ddlState.SelectedValue = acctHousehold.ShippingAddress.state;

                        String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";
                        String strQueryForCityName = "select distinct name, state_code from city where state_code = '" + ddlState.SelectedValue.ToString() + "' order by name";

                        MySqlConnection conn = new MySqlConnection(strConnectionString);
                        MySqlCommand cmdCityName = new MySqlCommand(strQueryForCityName, conn);
                        DataSet dsCity = new DataSet();
                        MySqlDataAdapter da = new MySqlDataAdapter(cmdCityName);

                        da.Fill(dsCity);
                        ddlCity.DataSource = dsCity.Tables[0];
                        ddlCity.DataTextField = "name";
                        ddlCity.DataValueField = "name";
                        ddlCity.DataBind();

                        ddlCity.SelectedValue = acctHousehold.ShippingAddress.city;

                    }
                }

                if (qrPrimaryContactInfo.size == 0 && qrHousehold.size == 0)
                {
                    //if (!IsPostBack)
                    //{

                    //calexDateOfBirth.DefaultView = AjaxControlToolkit.CalendarDefaultView.Years;

                    // This section is commented out, is needed for production code
                    txtSSN.ReadOnly = false;
                    revSSN.Enabled = true;
                    //revSSN.EnableClientScript = true;

                    String strQueryForStateName = "select distinct name, state_code from state order by name";
                    String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";

                    MySqlConnection connState = new MySqlConnection(strConnectionString);
                    MySqlCommand cmdState = new MySqlCommand(strQueryForStateName, connState);

                    DataSet dsState = new DataSet();

                    MySqlDataAdapter da = new MySqlDataAdapter(cmdState);

                    da.Fill(dsState);

                    ddlState.DataSource = dsState.Tables[0];
                    ddlState.DataTextField = "name";
                    ddlState.DataValueField = "state_code";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                    ddlState.SelectedIndex = 0;
 
                }
                LoadPrimarySDAInfo();
            }
        }

        protected void LoadPrimarySDAInfo()
        {
            String strQueryForPrimarySDA = "select cmm_Account__c, cmm_Contact__c, cmm_Name__c, " +
                                           "cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c " +
                                           "from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' and " +
                                           "cmm_Household_Role__c = 'Head of Household'";

            SForce.QueryResult qrPrimarySDA = Sfdcbinding.query(strQueryForPrimarySDA);

            if (qrPrimarySDA.size > 0)
            {
                SForce.tmp_SmokingDrugAlcohol__c tmpPrimarySDA = qrPrimarySDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                if (tmpPrimarySDA.cmm_bCurrentSmoker__c == "Yes")
                {
                    btnCurrentSmokerYes.BackColor = Color.Red;
                    btnCurrentSmokerYes.ForeColor = Color.White;
                    btnCurrentSmokerNo.BackColor = Color.LightGray;
                    btnCurrentSmokerNo.ForeColor = Color.Black;
                    hdnCurrentSmokerYes.Value = "red";
                    hdnCurrentSmokerNo.Value = "lightgrey";
                }
                else if (tmpPrimarySDA.cmm_bCurrentSmoker__c == "No")
                {
                    btnCurrentSmokerYes.BackColor = Color.LightGray;
                    btnCurrentSmokerYes.ForeColor = Color.Black;
                    btnCurrentSmokerNo.BackColor = Color.Blue;
                    btnCurrentSmokerNo.ForeColor = Color.White;
                    hdnCurrentSmokerYes.Value = "lightgrey";
                    hdnCurrentSmokerNo.Value = "blue";
                }
                if (tmpPrimarySDA.cmm_bFormerSmoker__c == "Yes")
                {
                    btnFormerSmokerYes.BackColor = Color.Green;
                    btnFormerSmokerYes.ForeColor = Color.White;
                    btnFormerSmokerNo.BackColor = Color.LightGray;
                    btnFormerSmokerNo.ForeColor = Color.Black;
                    hdnFormerSmokerYes.Value = "green";
                    hdnFormerSmokerNo.Value = "lightgrey";
                }
                else if (tmpPrimarySDA.cmm_bFormerSmoker__c == "No")
                {
                    btnFormerSmokerYes.BackColor = Color.LightGray;
                    btnFormerSmokerYes.ForeColor = Color.Black;
                    btnFormerSmokerNo.BackColor = Color.Blue;
                    btnFormerSmokerNo.ForeColor = Color.White;
                    hdnFormerSmokerYes.Value = "lightgrey";
                    hdnFormerSmokerNo.Value = "blue";
                }
                if (tmpPrimarySDA.cmm_bCurrentDrug__c == "Yes")
                {
                    btnNarcoticYes.BackColor = Color.Red;
                    btnNarcoticYes.ForeColor = Color.White;
                    btnNarcoticNo.BackColor = Color.LightGray;
                    btnNarcoticNo.ForeColor = Color.Black;
                    hdnNarcoticYes.Value = "red";
                    hdnNarcoticNo.Value = "lightgrey";
                }
                else if (tmpPrimarySDA.cmm_bCurrentDrug__c == "No")
                {
                    btnNarcoticYes.BackColor = Color.LightGray;
                    btnNarcoticYes.ForeColor = Color.Black;
                    btnNarcoticNo.BackColor = Color.Blue;
                    btnNarcoticNo.ForeColor = Color.White;
                    hdnNarcoticYes.Value = "lightgrey";
                    hdnNarcoticNo.Value = "blue";
                }
                if (tmpPrimarySDA.cmm_bFormerDrug__c == "Yes")
                {
                    btnFormerNarcoticYes.BackColor = Color.Green;
                    btnFormerNarcoticYes.ForeColor = Color.White;
                    btnFormerNarcoticNo.BackColor = Color.LightGray;
                    btnFormerNarcoticNo.ForeColor = Color.Black;
                    hdnFormerNarcoticYes.Value = "green";
                    hdnFormerNarcoticNo.Value = "lightgrey";
                }
                else if (tmpPrimarySDA.cmm_bFormerDrug__c == "No")
                {
                    btnFormerNarcoticYes.BackColor = Color.LightGray;
                    btnFormerNarcoticYes.ForeColor = Color.Black;
                    btnFormerNarcoticNo.BackColor = Color.Blue;
                    btnFormerNarcoticNo.ForeColor = Color.White;
                    hdnFormerNarcoticYes.Value = "lightgrey";
                    hdnFormerNarcoticNo.Value = "blue";
                }
                if (tmpPrimarySDA.cmm_bAlcohol__c == "Yes")
                {
                    btnAlcoholYes.BackColor = Color.Red;
                    btnAlcoholYes.ForeColor = Color.White;
                    btnAlcoholNo.BackColor = Color.LightGray;
                    btnAlcoholNo.ForeColor = Color.Black;
                    hdnAlcoholYes.Value = "red";
                    hdnAlcoholNo.Value = "lightgrey";
                }
                else if (tmpPrimarySDA.cmm_bAlcohol__c == "No")
                {
                    btnAlcoholYes.BackColor = Color.LightGray;
                    btnAlcoholYes.ForeColor = Color.Black;
                    btnAlcoholNo.BackColor = Color.Blue;
                    btnAlcoholNo.ForeColor = Color.White;
                    hdnAlcoholYes.Value = "lightgrey";
                    hdnAlcoholNo.Value = "blue";
                }
            }
        }


        protected void InitializeStateNames()
        {
            String strQueryForStateName = "select distinct name, state_code from state order by name";
            String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";

            MySqlConnection connState = new MySqlConnection(strConnectionString);
            MySqlCommand cmdState = new MySqlCommand(strQueryForStateName, connState);

            DataSet dsState = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmdState);

            da.Fill(dsState);

            ddlState.DataSource = dsState.Tables[0];
            ddlState.DataTextField = "name";
            ddlState.DataValueField = "state_code";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlState.SelectedIndex = 0;

        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void Set_smoking_drug_alcohol_buttons()
        {
            if (hdnCurrentSmokerYes.Value == "red")
            {
                btnCurrentSmokerYes.BackColor = Color.Red;
                btnCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnCurrentSmokerYes.Value == "lightgrey")
            {
                btnCurrentSmokerYes.BackColor = Color.LightGray;
                btnCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnCurrentSmokerNo.Value == "blue")
            {
                btnCurrentSmokerNo.BackColor = Color.Blue;
                btnCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnCurrentSmokerNo.Value == "lightgrey")
            {
                btnCurrentSmokerNo.BackColor = Color.LightGray;
                btnCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnFormerSmokerYes.Value == "green")
            {
                btnFormerSmokerYes.BackColor = Color.Green;
                btnFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnFormerSmokerYes.Value == "lightgrey")
            {
                btnFormerSmokerYes.BackColor = Color.LightGray;
                btnFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnFormerSmokerNo.Value == "blue")
            {
                btnFormerSmokerNo.BackColor = Color.Blue;
                btnFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnFormerSmokerNo.Value == "lightgrey")
            {
                btnFormerSmokerNo.BackColor = Color.LightGray;
                btnFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnNarcoticYes.Value == "red")
            {
                btnNarcoticYes.BackColor = Color.Red;
                btnNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnNarcoticYes.Value == "lightgrey")
            {
                btnNarcoticYes.BackColor = Color.LightGray;
                btnNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnNarcoticNo.Value == "blue")
            {
                btnNarcoticNo.BackColor = Color.Blue;
                btnNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnNarcoticNo.Value == "lightgrey")
            {
                btnNarcoticNo.BackColor = Color.LightGray;
                btnNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnFormerNarcoticYes.Value == "green")
            {
                btnFormerNarcoticYes.BackColor = Color.Green;
                btnFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnFormerNarcoticYes.Value == "lightgrey")
            {
                btnFormerNarcoticYes.BackColor = Color.LightGray;
                btnFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnFormerNarcoticNo.Value == "blue")
            {
                btnFormerNarcoticNo.BackColor = Color.Blue;
                btnFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnFormerNarcoticNo.Value == "lightgrey")
            {
                btnFormerNarcoticNo.BackColor = Color.LightGray;
                btnFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnAlcoholYes.Value == "red")
            {
				btnAlcoholYes.BackColor = Color.Red;
                btnAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnAlcoholYes.Value == "lightgrey")
            {
                btnAlcoholYes.BackColor = Color.LightGray;
                btnAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnAlcoholNo.Value == "blue")
            {
                btnAlcoholNo.BackColor = Color.Blue;
                btnAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnAlcoholNo.Value == "lightgrey")
            {
                btnAlcoholNo.BackColor = Color.LightGray;
                btnAlcoholNo.ForeColor = Color.Black;
            }

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {

            Set_smoking_drug_alcohol_buttons();

            if (IsValid && 
				btnCurrentSmokerYes.BackColor == Color.LightGray && 
				btnNarcoticYes.BackColor == Color.LightGray && 
				btnAlcoholYes.BackColor == Color.LightGray)
            {
                String strAccountName = strLastName + " (" + strFirstName + ") Household";

                Session["Title"] = ddlTitle.SelectedItem.Text;
                Session["MiddleName"] = txtMiddleName.Text;
                Session["DateOfBirth"] = txtDateOfBirth.Text;
                //if (rbMale.Checked) Session["Gender"] = "Male";
                //if (rbFemale.Checked) Session["Gender"] = "Female";
                Session["SSN"] = txtSSN.Text;
                Session["Telephone1"] = txtTelephone1.Text;
                Session["Telephone2"] = txtTelephone2.Text;
                Session["Telephone3"] = txtTelephone3.Text;
                Session["StreetAddress"] = txtAddress.Text;

                // This section should uncommented in production code
                Session["ZipCode"] = txtZipCode.Text;
                Session["State"] = ddlState.SelectedItem.Text;
                Session["City"] = ddlCity.SelectedItem.Text;

                //Session["ZipCode"] = "60714";
                //Session["State"] = "Illinois";
                //Session["City"] = "Niles";

                //InitializedSfdcbinding();


                //String strQueryForAccountIdOnEmail = "select Id from Account where cmm_Email__c = '" + strEmail + "'";

                //SForce.QueryResult qrAccountId = Sfdcbinding.query(strQueryForAccountIdOnEmail);

                //if (qrAccountId.size > 0)
                //{
                //SForce.Account acctHouseholdId = qrAccountId.records[0] as SForce.Account;

                //}


                //SForce.SaveResult[] srNewContact = Sfdcbinding.create(new SForce.sObject[] { ctPrimary });

                //String strPrimaryContactId = null;

                //if (srNewContact[0].success)
                //{
                //    lblSaveSuccessFailure.Text += "Contact added successfully";

                //    strPrimaryContactId = srNewContact[0].id;
                //    Session["ContactId"] = srNewContact[0].id;
                //}
                //else
                //{
                //    lblSaveSuccessFailure.Text += srNewContact[0].errors[0].message;
                //}

                Boolean bUpdatePrimarySuccess = false;
                String strPrimaryContactId = null;
                String strQueryForPrimaryId = "select Id from Contact where Email = '" + txtEmail.Text + "'";

                SForce.QueryResult qrPrimary = Sfdcbinding.query(strQueryForPrimaryId);

                if (qrPrimary.size > 0)
                {

                    SForce.Contact ctPrimary = new SForce.Contact();
                    ctPrimary.Id = qrPrimary.records[0].Id;
                    ctPrimary.cmm_Household__c = strAccountId;
                    //ctPrimary.Email = txtEmail.Text;
                    ctPrimary.Title = ddlTitle.SelectedItem.Text;
                    //ctPrimary.LastName = txtLastName.Text;
                    //ctPrimary.FirstName = txtFirstName.Text;
                    ctPrimary.MiddleName = txtMiddleName.Text;
                    ctPrimary.cmm_Household_Role__c = "Head of Household";

                    String strBirthDate = txtDateOfBirth.Text;

                    String[] arryBirthDate = strBirthDate.Split('/');

                    int nBirthMonth = Int32.Parse(arryBirthDate[0]);
                    int nBirthDay = Int32.Parse(arryBirthDate[1]);
                    int nBirthYear = Int32.Parse(arryBirthDate[2]);

                    ctPrimary.Birthdate = new DateTime(nBirthYear, nBirthMonth, nBirthDay);
                    ctPrimary.BirthdateSpecified = true;

                    ctPrimary.cmm_Gender__c = rbGenderList.SelectedItem.Text;

                    ctPrimary.Social_Security_Number__c = txtSSN.Text;
                    ctPrimary.Phone = txtTelephone1.Text;
                    ctPrimary.HomePhone = txtTelephone1.Text;
                    ctPrimary.MobilePhone = txtTelephone2.Text;
                    ctPrimary.OtherPhone = txtTelephone3.Text;

                    SForce.SaveResult[] srUpdatePrimary = Sfdcbinding.update(new SForce.sObject[] { ctPrimary });

                    if (srUpdatePrimary[0].success)
                    {
                        strPrimaryContactId = qrPrimary.records[0].Id;
                        Session["ContactId"] = qrPrimary.records[0].Id;
                        bUpdatePrimarySuccess = true;
                    }
                }


                String strQueryForContactId = "select Name from Contact where Email = '" + txtEmail.Text + "'";

                SForce.QueryResult qrContactId = Sfdcbinding.query(strQueryForContactId);

                //String strContactId = strPrimaryContactId;
                //String strMemberName = null;

                if (qrContactId.size > 0)
                {
                    SForce.Contact ctName = (SForce.Contact)qrContactId.records[0];
                    //strContactId = ctId.Id;
                    //strMemberName = ctId.Name;
                    //Session["ContactId"] = strContactId;

                    //               MemberSmokingDrugAlcohol primarySDA = new MemberSmokingDrugAlcohol();

                    //               primarySDA.AccountId = strAccountId;
                    //               primarySDA.HouseholdRole = HouseholdRoles.Primary;
                    //               primarySDA.ContactId = strPrimaryContactId;
                    //               primarySDA.Name = ctName.Name;
                    //               primarySDA.bCurrentSmoker = false;
                    //               primarySDA.bCurrentDrug = false;
                    //primarySDA.bAlcohol = false;
                    //               if (btnFormerSmokerYes.BackColor == Color.Green) primarySDA.bFormerSmoker = true;
                    //               if (btnFormerSmokerNo.BackColor == Color.Blue) primarySDA.bFormerSmoker = false;
                    //               if (btnFormerNarcoticYes.BackColor == Color.Green) primarySDA.bFormerDrug = true;
                    //               if (btnFormerNarcoticNo.BackColor == Color.Blue) primarySDA.bFormerDrug = false;
                    //               //if (btnAlcoholYes.BackColor == Color.Green) primarySDA.bAlcohol = true;
                    //               //if (btnAlcoholNo.BackColor == Color.Blue) primarySDA.bAlcohol = false;

                    //               lstMemberSDA.Add(primarySDA);
                    //               Session["MemberSmokingDrugAlcohol"] = lstMemberSDA;

                    // Save lstMemberSDA to salesforce for incomplete application - done
                    string strQueryForPrimarySDA = "select Id from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' and " +
                                                   "cmm_Household_Role__c = 'Head of Household'";

                    SForce.QueryResult qrTmpPrimarySDA = Sfdcbinding.query(strQueryForPrimarySDA);
                    if (qrTmpPrimarySDA.size > 0)
                    {
                        SForce.tmp_SmokingDrugAlcohol__c tmpHeadSDA = qrTmpPrimarySDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                        SForce.tmp_SmokingDrugAlcohol__c tmpPrimarySDA = new SForce.tmp_SmokingDrugAlcohol__c();

                        tmpPrimarySDA.Id = tmpHeadSDA.Id;
                        tmpPrimarySDA.cmm_bCurrentSmoker__c = "No";
                        tmpPrimarySDA.cmm_bCurrentDrug__c = "No";
                        tmpPrimarySDA.cmm_bAlcohol__c = "No";
                        if (btnFormerSmokerYes.BackColor == Color.Green) tmpPrimarySDA.cmm_bFormerSmoker__c = "Yes";
                        if (btnFormerSmokerNo.BackColor == Color.Blue) tmpPrimarySDA.cmm_bFormerSmoker__c = "No";
                        if (btnFormerNarcoticYes.BackColor == Color.Green) tmpPrimarySDA.cmm_bFormerDrug__c = "Yes";
                        if (btnFormerNarcoticNo.BackColor == Color.Blue) tmpPrimarySDA.cmm_bFormerDrug__c = "No";

                        SForce.SaveResult[] srUpdateTmpPrimarySDA = Sfdcbinding.update(new SForce.sObject[] { tmpPrimarySDA });

                        if (srUpdateTmpPrimarySDA[0].success)
                        {
                            // temporary instance of salesforc object is udpated successfully
                        }


                    }
                    else if (qrTmpPrimarySDA.size == 0)
                    {
                        SForce.tmp_SmokingDrugAlcohol__c tmpPrimarySDA = new SForce.tmp_SmokingDrugAlcohol__c();

                        tmpPrimarySDA.cmm_Account_Creation_Step_Code__c = 3;
                        tmpPrimarySDA.cmm_Account_Creation_Step_Code__cSpecified = true;
                        tmpPrimarySDA.cmm_Account__c = strAccountId;
                        tmpPrimarySDA.cmm_Contact__c = strPrimaryContactId;
                        tmpPrimarySDA.cmm_Household_Role__c = "Head of Household";
                        tmpPrimarySDA.cmm_Name__c = ctName.Name;
                        tmpPrimarySDA.cmm_bCurrentSmoker__c = "No";
                        tmpPrimarySDA.cmm_bCurrentDrug__c = "No";
                        tmpPrimarySDA.cmm_bAlcohol__c = "No";
                        if (btnFormerSmokerYes.BackColor == Color.Green) tmpPrimarySDA.cmm_bFormerSmoker__c = "Yes";
                        if (btnFormerSmokerNo.BackColor == Color.Blue) tmpPrimarySDA.cmm_bFormerSmoker__c = "No";
                        if (btnFormerNarcoticYes.BackColor == Color.Green) tmpPrimarySDA.cmm_bFormerDrug__c = "Yes";
                        if (btnFormerNarcoticNo.BackColor == Color.Blue) tmpPrimarySDA.cmm_bFormerDrug__c = "No";
                        //if (btnAlcoholYes.BackColor == Color.Green) tmpPrimarySDA.cmm_bAlcohol__c = "Yes";
                        //if (btnAlcoholNo.BackColor == Color.Blue) tmpPrimarySDA.cmm_bAlcohol__c = "No";

                        SForce.SaveResult[] srTmpPrimarySDA = Sfdcbinding.create(new SForce.sObject[] { tmpPrimarySDA });

                        if (srTmpPrimarySDA[0].success)
                        {
                            // temporary instance of salesforce object is created successfully
                        }
                    }
                }


                //SForce.Account acctUpdate = new SForce.Account();

                //if (strAccountId != null) acctUpdate.Id = strAccountId;
                //if (strPrimaryContactId != null) acctUpdate.cmm_Contact__c = strPrimaryContactId;

                //SForce.SaveResult[] updateResultsAccount = Sfdcbinding.update(new SForce.sObject[] { acctUpdate });

                //if (updateResultsAccount[0].success)
                //{
                //    lblSaveSuccessFailure.Text += "Account update succeeded";
                //}
                //else
                //{
                //    lblSaveSuccessFailure.Text += updateResultsAccount[0].errors[0].message;
                //}

                //SForce.Contact ctUpdate = new SForce.Contact();

                //if (strPrimaryContactId != null) ctUpdate.Id = strPrimaryContactId;
                //if (strAccountId != null) ctUpdate.cmm_Household__c = strAccountId;

                //SForce.SaveResult[] updateResultsContact = Sfdcbinding.update(new SForce.sObject[] { ctUpdate });

                //if (updateResultsContact[0].success)
                //{
                //    lblSaveSuccessFailure.Text += "Contact update succeeded";
                //}
                //else
                //{
                //    lblSaveSuccessFailure.Text += updateResultsContact[0].errors[0].message;
                //}




                Boolean bUpdateAcctHouseholdSuccess = false;

                SForce.Account acctHousehold = new SForce.Account();
                acctHousehold.Id = strAccountId;
                acctHousehold.cmm_Account_Creation_Step_Code__c = "3";
                acctHousehold.cmm_Contact__c = strPrimaryContactId;
                acctHousehold.ShippingStreet = txtAddress.Text;
                acctHousehold.ShippingCity = ddlCity.SelectedValue;
                acctHousehold.ShippingState = ddlState.SelectedValue;
                acctHousehold.ShippingPostalCode = txtZipCode.Text;

                acctHousehold.BillingStreet = txtAddress.Text;
                acctHousehold.BillingCity = ddlCity.SelectedValue;
                acctHousehold.BillingState = ddlState.SelectedValue;
                acctHousehold.BillingPostalCode = txtZipCode.Text;

                SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { acctHousehold });

                if (updateResults[0].success)
                {
                    // The Household account updated with Shipping address
                    //Session["PreviousPage"] = "PersonalDetails";
                    //Response.Redirect("~/FamilyDetails.aspx");
                    bUpdateAcctHouseholdSuccess = true;
                }

                if (bUpdatePrimarySuccess && bUpdateAcctHouseholdSuccess)
                {
                    Session["PreviousPage"] = "PersonalDetails";
                    Response.Redirect("~/FamilyDetails.aspx");

                }


                //if (updateResultsAccount[0].success && updateResultsContact[0].success)
                //{

                //    //SForce.Account acctPrimary = new SForce.Account();
                //    //if (strAccountId != null) acctPrimary.Id = strAccountId;
                //    //acctPrimary.cmm_Account_Creation_Step_Code__c = "2";

                //    //SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

                //    //if (srAccount[0].success)
                //    //{
                //    //    Session["PreviousPage"] = "CreateAccount";
                //    //    Response.Redirect("~/PersonalDetails.aspx");
                //    //}

                //    SForce.Account acctPrimary = new SForce.Account();
                //    acctPrimary.Id = strAccountId;
                //    acctPrimary.cmm_Account_Creation_Step_Code__c = "3";

                //    acctPrimary.BillingStreet = txtAddress.Text;
                //    acctPrimary.BillingCity = ddlCity.SelectedItem.Text;
                //    acctPrimary.BillingState = ddlState.SelectedItem.Text;
                //    acctPrimary.BillingPostalCode = txtZipCode.Text;

                //    SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

                //    if (srAccount[0].success)
                //    {
                //        // the update has succeeded
                //        Session["PreviousPage"] = "PersonalDetails";
                //        Response.Redirect("~/FamilyDetails.aspx");
                //    }
                //}
            }
            else
            {
                // user is current smoker or taking narcotic drug
                mpeUserSmokingDrugAlcohol.Show();
            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            // This section of code should be uncommented for production code

            if (txtZipCode.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtZipCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ddlState.Items.Clear();
                    ddlCity.Items.Clear();

                    ddlState.Items.Add(new ListItem(dr["state_code"].ToString()));
                    ddlState.SelectedIndex = 0;
                    ddlCity.Items.Add(new ListItem(dr["name"].ToString()));
                    ddlCity.SelectedIndex = 0;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
                }
                conn.Close();
            }


        }

        protected void ddlState_TextChanged(object sender, EventArgs e)
        {
            // This section of code should be uncommented for production code


            String strConnectionString = @"Data Source=10.1.10.79; Port=3306; Database=cmmworld_admin; User ID=Hj_p007; Password='speed009'";

            String strQueryForCityName = "select distinct name, state_code from city where state_code = '" + ddlState.SelectedValue.ToString() + "' order by name";

            MySqlConnection conn = new MySqlConnection(strConnectionString);

            MySqlCommand cmdCityName = new MySqlCommand(strQueryForCityName, conn);

            DataSet dsCity = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(cmdCityName);

            da.Fill(dsCity);

            ddlCity.DataSource = dsCity.Tables[0];
            ddlCity.DataTextField = "name";
            ddlCity.DataValueField = "name";
            ddlCity.DataBind();

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            mpeUserSmokingDrugAlcohol.Hide();
        }
    }
}