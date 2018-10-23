using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Security.Principal;
using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;

namespace SalesForceWebApp
{
    public class GiftHistory
    {
        public int nGiftNumber { get; set; }
        //public DateTime dtGiftDate { get; set; }
        public string strDate { get; set; }
        public decimal Amount { get; set; }
        public char Code { get; set; }
        public string strNote { get; set; }
        //public string strAction { get; set; }
    }

    public class NeedsProcessing
    {
        public string strDate { get; set; }
        public string strPatient { get; set; }
        public string strHospital { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string strMemo { get; set; }
    }

    public class Files
    {
        public string strFile { get; set; }
        public string strCategory { get; set; }
        public string strMemberName { get; set; }
    }

    public partial class MainForm : System.Web.UI.Page
    {
        //protected int nActiveTabIndex = 0;

        protected enum MainTabPage { GeneralInfoPage, HealthHistoryPage, AgreementPage, GiftHistoryPage, NeedsProcessingPage, FilesPage, PasswordPage };

        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        enum ZipCodePostBackLocation { PrimarySuccess, PrimaryFailure, ChurchSuccess, ChurchFailure, BillingSuccess, BillingFailure };

        protected String strAccountId = String.Empty;
        protected string strHouseholdId = null;
        protected string strContactId = null;
        protected string strUserEmail = string.Empty;

        protected int nNumberOfFamilyMembers = 0;

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        //private SForce.QueryResult queryResult = null;
        private SForce.QueryResult queryResultContactId = null;
        private SForce.QueryResult queryResultContactForMichaelJordan = null;
        //private SForce.QueryResult queryResultHouseholdInfoFromContact = null;
        private SForce.QueryResult queryResultMembershipInfo = null;
        private SForce.QueryResult queryResultFamilyInfo = null;

        private SForce.QueryResult queryResultSpouseProgram = null;
        private SForce.QueryResult queryResultChildrenProgram = null;

        private SForce.QueryResult queryResultSpouseInfo = null;
        private SForce.QueryResult queryResultPaymentInfo = null;
        private SForce.QueryResult queryResultChildrenInfo = null;

        //private SForce.QueryResult queryResultSpouseInfoFromContact = null;
        //private SForce.QueryResult queryResultChildrenInfoFromContact = null;
        //private SForce.QueryResult queryResultGiftSendingInfoFromContact = null;

        private SForce.Contact ctContactId = null;
        private SForce.Contact ctMichaelJordan = null;
        private SForce.Contact ctMembershipInfo = null;
        private SForce.Contact ctFamilyInfo = null;

        //private SForce.Contact ctSpouseInfo = null;
        //private SForce.Contact ctChildrenInfo = null;

        //private SForce.causeview__Payment__c paymentInfo = null;
        //private SForce.Membership__c paymentInfoInMembership = null;
        private SForce.Contact ctPaymentInfoInMembership = null;

        //private string strQueryContactForPortalUser = null;

        private string strQueryContactForContactID = null;

        private string strQueryContactForMichaelJordan = null;

        private string strQueryMembershipInfo = null;

        private string strQueryFamilyInfo = null;

        private string strQueryPaymentInfoInMembership = null;

        private string strQueryPaymentInfo = null;

        private DateTime dtToday = DateTime.Today;

        enum Month { Jan, Feb, Mar, Apr, May, June, July, Aug, Sept, Oct, Nov, Dec };

        private int nYear;
        private Month nMonth;

        private String strMonthYear = null;

        /// <summary>
        /// The health history tab variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        TableHeaderRow headerRow = null;
        TableHeaderCell headerCell = null;

        TableRow headRow = null;
        TableCell headCell = null;

        List<TreatmentInfo> lstTreatmentHistory = new List<TreatmentInfo>();

        protected const String strHeading = "Has any person listed received medical attention and/or had surgery done in any hospital or similar institution?<br />" +
                                "Please select \"Yes\" or \"No\" below, and if answer to any of the listed is YES, explain it in the box below;<br />" +
                                "아래에 기재된 사항에 \"Yes\" 나 \"No\"를 선택하시고, 만일 \"Yes\"에 표하였으면 그 내용을 아래칸에 기록하여 주십시오.";
        protected const String strHeader = "Please record the new member’s health history. This documentation serves to protect our present members. " +
                                           "If a member provides false statement on this form, this may be a cause for immediate termination from Christian Mutual Med-Aid.<br />" +
                                           "기존 회원들의 보호를 위해 새 신청자는 사실을 기록해야 합니다. 사실이 아닌 기록을 했을 경우에는 회원 자격이 박탈 될 수도 있습니다.";
        protected const String strQuestion1 = "Have any person listed been treated by a doctor during the last year? (Including annual check-up)<br /> 지난 1년간 치료를 받은 일이 있습니까? (정기검진 포함)";
        protected const String strQuestion2 = "Diagnosed with high blood pressure, diabetes, heart, vascular disease.<br />고혈압, 당뇨병, 심장병, 뇌졸증 및 혈관 질환";
        protected const String strQuestion3 = "Diagnosed with allergy, asthma or respiratory problem<br />알레르기, 천식 및 호흡기 관련 질환";
        protected const String strQuestion4 = "Diagnosed with arthritis, rheumatoid arthritis, chronic back pain, nerve system etc.<br />관절염, 류마티스, 척추 및 신경계통 관련 질환 등";
        protected const String strQuestion5 = "Medical conditions related to eyes, nose, ears, hands, feet<br />눈, 코, 귀, 손 발 관련 질환";
        protected const String strQuestion6 = "Medical conditions related to stomach, liver, colon, kidney etc.<br />위, 간, 대장, 신장 등 장기 관련 질환";
        protected const String strQuestion7 = "Medical conditions related to thyroid, tumor, cancer etc.<br />갑상선, 각종 종양 및 암";
        protected const String strQuestion8 = "Medical conditions related to prostate or female reproduction organs<br />부인과 질환 또는 전립선 관련 질환";
        protected const String strQuestion9 = "Congenital disease and other medical conditions<br />선천적 질환 및 기타 다른 질환";

        protected const String strCurrentSmoker = "Am a current smoker<br />현재 흡연을 했습니다.";
        protected const String strFormerSmoker = "Am a former smoker<br />과거에 흡연을 했습니다.";
        protected const String strCurrentDrug = "Is currently using narcotic drugs<br />현재 마약을 사용합니다.";
        protected const String strFormerDrug = "Have formerly used narcotic drug<br />과거에 마약을 사용했습니다.";
        protected const String strDrinkingAlcohol = "I abuse alcohol not following the Biblical teaching on the use of it.<br />술에 대한 성경의 가르침을 따르지 않고 남용합니다.";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Header.DataBind();
            if (!IsPostBack)
            {
                // Don't erase this line!!!
                ///////////////////////////////////////////////////////////////////////////////
                //lblPersonalInfoUpdateMessage.Text = Context.User.Identity.Name.ToString();
                ///////////////////////////////////////////////////////////////////////////////

                //strUserEmail = Context.User.Identity.Name.ToString();
                //Session["UserEmail"] = strUserEmail;

                //SetSQLStatementForPortalUser();

                if (Context.User.Identity.Name != null &&
                    Context.User.Identity.IsAuthenticated)
                {
                    //// This block of code shoud be placed in Login.aspx.cs
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                    LoadPersonalInfo();
                }
                else if (!Context.User.Identity.IsAuthenticated)
                {

                }

                // This is working
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}",
                //    txtChurchName.ClientID), true);


            }

            DateTime dtDate = new DateTime(2017, 2, 14);

            var giftHistory = new List<GiftHistory>();
            giftHistory.Add(new GiftHistory() { nGiftNumber = 1, strDate = dtToday.ToShortDateString(), Amount = 175, Code = 'P', strNote = "Gold Plus - Main Member" });
            giftHistory.Add(new GiftHistory() { nGiftNumber = 2, strDate = dtDate.ToShortDateString(), Amount = 175, Code = 'P', strNote = "Gold - Main Member" });

            lvGiftHistory.DataSource = giftHistory;
            lvGiftHistory.DataBind();

            //var needsProcessingList = new List<NeedsProcessing>();

            //needsProcessingList.Add(new NeedsProcessing() { strDate = DateTime.Today.ToShortDateString(), strPatient = "John, Kim", strHospital = "Swedish", Amount = 30000, Balance = 30000, strMemo = "This should be paid by August." });
            //needsProcessingList.Add(new NeedsProcessing() { strDate = DateTime.Today.ToShortDateString(), strPatient = "John, Kim", strHospital = "Cook County Hospital", Amount = 20000, Balance = 20000, strMemo = "This is for his liver damage" });

            //lvNeedsProcessing.DataSource = needsProcessingList;
            //lvNeedsProcessing.DataBind();

            var files = new List<Files>();

            //files.Add(new SalesForceWebApp.Files() { strFile = "Application1.pdf", strCategory = "Application Form", strMemberName = "Harris J. Park" });
            lvFiles.DataSource = files;
            lvFiles.DataBind();

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //SetFocus(txtChurchName);

            InitializedHiddenFields();
            InitializedSfdcbinding();

            strUserEmail = Context.User.Identity.Name.ToString();

            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];
            else
            {
                String strQueryForAccountId = "select Id from Account where cmm_Email__c = '" + strUserEmail + "'";

                SForce.QueryResult qrAccountId = Sfdcbinding.query(strQueryForAccountId);

                if (qrAccountId.size > 0)
                {
                    SForce.Account acctId = qrAccountId.records[0] as SForce.Account;

                    strAccountId = acctId.Id;
                }
            }


            nNumberOfFamilyMembers = NumberOfFamilyMembers();
            Session["NumberOfFamilyMembers"] = nNumberOfFamilyMembers;

            Table table = new Table();
            table.ID = "HealthHistory";
            table.HorizontalAlign = HorizontalAlign.Center;
            table.Width = 1000;
            table.BackColor = Color.White;
            table.BorderColor = Color.Black;


            InitializeHealthHistoryTableHeading(table);
            InitizlizeHealthHistoryHeader(table);

            HealthHistoryAddNewRow(table, strQuestion1, 1);
            HealthHistoryAddNewRow(table, strQuestion2, 2);
            HealthHistoryAddNewRow(table, strQuestion3, 3);
            HealthHistoryAddNewRow(table, strQuestion4, 4);
            HealthHistoryAddNewRow(table, strQuestion5, 5);
            HealthHistoryAddNewRow(table, strQuestion6, 6);
            HealthHistoryAddNewRow(table, strQuestion7, 7);
            HealthHistoryAddNewRow(table, strQuestion8, 8);
            HealthHistoryAddNewRow(table, strQuestion9, 9);
            HealthHistoryAddNewRow(table, strCurrentSmoker, 10);
            HealthHistoryAddNewRow(table, strFormerSmoker, 11);
            HealthHistoryAddNewRow(table, strCurrentDrug, 12);
            HealthHistoryAddNewRow(table, strFormerDrug, 13);
            HealthHistoryAddNewRow(table, strDrinkingAlcohol, 14);

            pnlHealthHistory.Controls.Add(table);

            PopulateMedicalHistory();

            //PopulateSmokingDrugAlcohol();

            //InitializeSmokingDrugDrinking();

        }

        protected void BuildSmokingDrugAlcohol()
        {

            Table tblSmokingDrugAlcohol = new Table();
            TableRow trSDARecord = new TableRow();
            TableCell tcSDADescription = new TableCell();
            TableCell tcSDARecord = new TableCell();

            String strQueryForContactIdsInHousehold = "select Id, Name from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrContactIds = Sfdcbinding.query(strQueryForContactIdsInHousehold);

            int nRowCounter, nColumnCounter;

            if (qrContactIds.size > 0)
            {

                for (int nColumn = 0; nColumn < qrContactIds.size; nColumn++)
                {
                    //TableRow trMedicalTreatmentRecord = new TableRow();
                    String strQueryForMemberSmokingDrugAlcohol = "select cmm_Currently_Smoking__c, cmm_Former_Smoker__c, " +
                                             "cmm_Currently_Taking_Narcotic_Drug__c, cmm_Formerly_Taking_Narcotic_Drug__c, " +
                                             "cmm_Drinking_Alcohol__c, Contact__r.cmm_Household_Role__c, Contact__r.Name from Applicant__c " +
                                             "where Contact__c = '" + qrContactIds.records[nColumn].Id + "'";

                    SForce.QueryResult qrApplicantSmokingDrugAlcohol = Sfdcbinding.query(strQueryForMemberSmokingDrugAlcohol);

                    nColumnCounter = nColumn + 1;

                    if (qrApplicantSmokingDrugAlcohol.size > 0)
                    {
                        SForce.Applicant__c applicant = qrApplicantSmokingDrugAlcohol.records[0] as SForce.Applicant__c;

                        for (int nRow = 0; nRow < 5; nRow++)
                        {
                            nRowCounter = nRow + 1;

                            switch (nRow)
                            {
                                case 0:
                                    tcSDADescription.Text = strCurrentSmoker;
                                    break;
                                case 1:
                                    tcSDADescription.Text = strFormerSmoker;
                                    break;
                                case 2:
                                    tcSDADescription.Text = strCurrentDrug;
                                    break;
                                case 3:
                                    tcSDADescription.Text = strFormerDrug;
                                    break;
                                case 4:
                                    tcSDADescription.Text = strDrinkingAlcohol;
                                    break;
                            }
                            tcSDADescription.ColumnSpan = 1;
                            tcSDADescription.BorderColor = Color.Gray;
                            tcSDADescription.BorderStyle = BorderStyle.Solid;
                            tcSDADescription.BorderWidth = 1;
                            tcSDADescription.Font.Size = FontUnit.Smaller;
                            tcSDADescription.HorizontalAlign = HorizontalAlign.Left;
                            tcSDADescription.Width = 200;
                            trSDARecord.Cells.Add(tcSDADescription);

                            Button btnYes = new Button();
                            btnYes.Text = "Yes";
                            btnYes.Font.Name = "Arial";
                            btnYes.Font.Bold = true;
                            btnYes.Width = 40;
                            btnYes.Height = 35;
                            //btnYes.BackColor = Color.LightGray;
                            btnYes.BorderWidth = 0;
                            btnYes.ID = "btnYes_" + nRowCounter + "_" + nColumnCounter;

                            Button btnNo = new Button();
                            btnNo.Text = "No";
                            btnNo.Font.Name = "Arial";
                            btnNo.Font.Bold = true;
                            btnNo.Width = 40;
                            btnNo.Height = 35;
                            //btnNo.BackColor = Color.Blue;
                            //btnNo.ForeColor = Color.White;
                            btnNo.BorderWidth = 0;
                            btnNo.ID = "btnNo_" + nRowCounter + "_" + nColumnCounter;

                            btnYes.Attributes.Add("onclick", "return false;");
                            btnNo.Attributes.Add("onclick", "return false;");

                            if (applicant.cmm_Currently_Smoking__c == true && nRow == 0)
                            {
                                btnYes.BackColor = Color.Red;
                                btnYes.ForeColor = Color.White;
                                btnNo.BackColor = Color.LightGray;
                                btnNo.ForeColor = Color.Black;
                            }
                            else if (applicant.cmm_Currently_Smoking__c == false && nRow == 0)
                            {
                                btnYes.BackColor = Color.LightGray;
                                btnYes.ForeColor = Color.Black;
                                btnNo.BackColor = Color.Blue;
                                btnNo.ForeColor = Color.White;
                            }

                            if (applicant.cmm_Former_Smoker__c == true && nRow == 1)
                            {
                                btnYes.BackColor = Color.Red;
                                btnYes.ForeColor = Color.White;
                                btnNo.BackColor = Color.LightGray;
                                btnNo.ForeColor = Color.Black;
                            }
                            else if (applicant.cmm_Former_Smoker__c == false && nRow == 1)
                            {
                                btnYes.BackColor = Color.LightGray;
                                btnYes.ForeColor = Color.Black;
                                btnNo.BackColor = Color.Blue;
                                btnNo.ForeColor = Color.White;
                            }

                            if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == true && nRow == 2)
                            {
                                btnYes.BackColor = Color.Red;
                                btnYes.ForeColor = Color.White;
                                btnNo.BackColor = Color.LightGray;
                                btnNo.ForeColor = Color.Black;
                            }
                            else if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == false && nRow == 2)
                            {
                                btnYes.BackColor = Color.LightGray;
                                btnYes.ForeColor = Color.Black;
                                btnNo.BackColor = Color.Blue;
                                btnNo.ForeColor = Color.White;

                            }

                            if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == true && nRow == 3)
                            {
                                btnYes.BackColor = Color.Red;
                                btnYes.ForeColor = Color.White;
                                btnNo.BackColor = Color.LightGray;
                                btnNo.ForeColor = Color.Black;

                            }
                            else if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == false && nRow == 3)
                            {
                                btnYes.BackColor = Color.LightGray;
                                btnYes.ForeColor = Color.Black;
                                btnNo.BackColor = Color.Blue;
                                btnNo.ForeColor = Color.White;

                            }

                            if (applicant.cmm_Drinking_Alcohol__c == true && nRow == 4)
                            {
                                btnYes.BackColor = Color.Red;
                                btnYes.ForeColor = Color.White;
                                btnNo.BackColor = Color.LightGray;
                                btnNo.ForeColor = Color.Black;

                            }
                            else if (applicant.cmm_Drinking_Alcohol__c == false && nRow == 4)
                            {
                                btnYes.BackColor = Color.LightGray;
                                btnYes.ForeColor = Color.Black;
                                btnNo.BackColor = Color.Blue;
                                btnNo.ForeColor = Color.White;

                            }


                            // 11/10/17 - if statements here for former smoker, drug, alcohol

                            tcSDARecord.ID = "tcRecord_" + nRowCounter + "_" + nColumnCounter;

                            tcSDARecord.Controls.Add(btnYes);
                            tcSDARecord.Controls.Add(btnNo);

                            trSDARecord.Cells.Add(tcSDARecord);

                            //tblTreatmentHistory.Rows.Add(trSDARecord);
                            tblSmokingDrugAlcohol.Rows.Add(trSDARecord);

                        }
                    }
                    //trSDARecord.Cells.Add(tcSDARecord);
                }

                //pnlTreatmentHistory.Controls.Add(tblTreatmentHistory);
                //pnlSmokingDrugAlcohol.Controls.Add(tblSmokingDrugAlcohol);
            }
        }

        protected void InitializeSmokingDrugDrinking()
        {
            Table tblSmokingDrugAlcohol = new Table();

            tblSmokingDrugAlcohol.ID = "HealthHistory";
            tblSmokingDrugAlcohol.HorizontalAlign = HorizontalAlign.Center;
            tblSmokingDrugAlcohol.Width = 1000;
            tblSmokingDrugAlcohol.BackColor = Color.White;
            tblSmokingDrugAlcohol.BorderColor = Color.Black;

            String strQueryForContactIdsInHousehold = "select Id, Name from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrContactIds = Sfdcbinding.query(strQueryForContactIdsInHousehold);

            int nRowCounter = 0;
            int nColumnCounter = 0;

            for (int nRow = 0; nRow < 5; nRow++)
            {
                TableRow trSDARecord = new TableRow();
                TableCell tcSDADescription = new TableCell();

                switch (nRow)
                {
                    case 0:
                        tcSDADescription.Text = strCurrentSmoker;
                        break;
                    case 1:
                        tcSDADescription.Text = strFormerSmoker;
                        break;
                    case 2:
                        tcSDADescription.Text = strCurrentDrug;
                        break;
                    case 3:
                        tcSDADescription.Text = strFormerDrug;
                        break;
                    case 4:
                        tcSDADescription.Text = strDrinkingAlcohol;
                        break;
                }
                tcSDADescription.ColumnSpan = 1;
                tcSDADescription.BorderColor = Color.Gray;
                tcSDADescription.BorderStyle = BorderStyle.Solid;
                tcSDADescription.BorderWidth = 1;
                tcSDADescription.Font.Size = FontUnit.Smaller;
                tcSDADescription.HorizontalAlign = HorizontalAlign.Left;
                tcSDADescription.Width = 200;
                trSDARecord.Cells.Add(tcSDADescription);


                for (int nColumn = 0; nColumn < qrContactIds.size; nColumn++)
                {

                    TableCell tcSDARecord = new TableCell();

                    tcSDARecord = new TableCell();
                    tcSDARecord.ColumnSpan = 1;
                    tcSDARecord.BorderWidth = 1;
                    tcSDARecord.BorderColor = Color.Gray;
                    tcSDARecord.BorderStyle = BorderStyle.Solid;
                    tcSDARecord.Width = 600 / qrContactIds.size;
                    tcSDARecord.HorizontalAlign = HorizontalAlign.Center;
                    tcSDARecord.VerticalAlign = VerticalAlign.Middle;


                    Button btnYes = new Button();
                    btnYes.Text = "Yes";
                    btnYes.Font.Name = "Arial";
                    btnYes.Font.Bold = true;
                    btnYes.Width = 40;
                    btnYes.Height = 35;
                    btnYes.BackColor = Color.LightGray;
                    btnYes.ForeColor = Color.Black;
                    btnYes.BorderWidth = 0;
                    btnYes.ID = "btnYes_" + nRowCounter + "_" + nColumnCounter;

                    Button btnNo = new Button();
                    btnNo.Text = "No";
                    btnNo.Font.Name = "Arial";
                    btnNo.Font.Bold = true;
                    btnNo.Width = 40;
                    btnNo.Height = 35;
                    btnNo.BackColor = Color.Blue;
                    btnNo.ForeColor = Color.White;
                    btnNo.BorderWidth = 0;
                    btnNo.ID = "btnNo_" + nRowCounter + "_" + nColumnCounter;

                    btnYes.Attributes.Add("onclick", "return false;");
                    btnNo.Attributes.Add("onclick", "return false;");

                    tcSDARecord.Controls.Add(btnYes);
                    tcSDARecord.Controls.Add(btnNo);

                    trSDARecord.Cells.Add(tcSDARecord);
                }

                tblSmokingDrugAlcohol.Rows.Add(trSDARecord);
            }

            //pnlSmokingDrugAlcohol.Controls.Add(tblSmokingDrugAlcohol);


        }

        protected void PopulateMedicalHistory()
        {
            String strQueryForContactIdsInHousehold = "select Id from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrContactIds = Sfdcbinding.query(strQueryForContactIdsInHousehold);

            if (qrContactIds.size > 0)
            {

                for (int i = 0; i < qrContactIds.size; i++)
                {

                    String strQueryForApplicant = "select Treated_in_last_12_months__c, Diagnosed_with_Cardiovascular_issues__c, Diagnosed_with_Allergy_Respiratory__c, " +
                                                  "Arthritis_back_nervous_system_iss__c, Eyes_nose_ears_hands_feet_conditions__c, Stomach_liver_colon_kidney_conditions__c, " +
                                                  "Thyroid_tumor_cancer_medical_conditions__c, Prostate_or_female_reprdct_conditions__c, Congenital_disease_or_other_condition__c, " +
                                                  "cmm_Currently_Smoking__c, cmm_Former_Smoker__c, cmm_Currently_Taking_Narcotic_Drug__c, cmm_Formerly_Taking_Narcotic_Drug__c, " +
                                                  "cmm_Drinking_Alcohol__c, " +
                                                  "Contact__r.cmm_Household_Role__c, Contact__r.Name " +
                                                  "from Applicant__c where Contact__c = '" + qrContactIds.records[i].Id + "'";


                    SForce.QueryResult qrApplicant = Sfdcbinding.query(strQueryForApplicant);

                    if (qrApplicant.size > 0)
                    {
                        Table tblApplicant = pnlHealthHistory.FindControl("HealthHistory") as Table;
                        SForce.Applicant__c applicant = qrApplicant.records[0] as SForce.Applicant__c;

                        TableCell tcHeadOfHousehold = tblApplicant.FindControl("tcHeadOfHousehold") as TableCell;

                        if (applicant.Contact__r.cmm_Household_Role__c == "Head of Household" && applicant.Contact__r.Name == tcHeadOfHousehold.Text)
                        {
                            for (int nRow = 1; nRow <= 14; nRow++)
                            {
                                int nColumnHouseholdRole = i + 1;
                                Button btnYes = tblApplicant.FindControl("btnYes_" + nRow + "_" + nColumnHouseholdRole) as Button;
                                Button btnNo = tblApplicant.FindControl("btnNo_" + nRow + "_" + nColumnHouseholdRole) as Button;

                                if (applicant.Treated_in_last_12_months__c == true && nRow == 1 && nColumnHouseholdRole == 1)

                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Treated_in_last_12_months__c == false && nRow == 1 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Diagnosed_with_Cardiovascular_issues__c == true && nRow == 2 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Diagnosed_with_Cardiovascular_issues__c == false && nRow == 2 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Diagnosed_with_Allergy_Respiratory__c == true && nRow == 3 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Diagnosed_with_Allergy_Respiratory__c == false && nRow == 3 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Arthritis_back_nervous_system_iss__c == true && nRow == 4 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Arthritis_back_nervous_system_iss__c == false && nRow == 4 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Eyes_nose_ears_hands_feet_conditions__c == true && nRow == 5 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Eyes_nose_ears_hands_feet_conditions__c == false && nRow == 5 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Stomach_liver_colon_kidney_conditions__c == true && nRow == 6 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Stomach_liver_colon_kidney_conditions__c == false && nRow == 6 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Thyroid_tumor_cancer_medical_conditions__c == true && nRow == 7 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Thyroid_tumor_cancer_medical_conditions__c == false && nRow == 7 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Prostate_or_female_reprdct_conditions__c == true && nRow == 8 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Prostate_or_female_reprdct_conditions__c == false && nRow == 8 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Congenital_disease_or_other_condition__c == true && nRow == 9 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Congenital_disease_or_other_condition__c == false && nRow == 9 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Currently_Smoking__c == true && nRow == 10 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Currently_Smoking__c == false && nRow == 10 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Former_Smoker__c == true && nRow == 11 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Former_Smoker__c == false && nRow == 11 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == true && nRow == 12 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == false && nRow == 12 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == true && nRow == 13 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == false && nRow == 13 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Drinking_Alcohol__c == true && nRow == 14 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Drinking_Alcohol__c == false && nRow == 14 && nColumnHouseholdRole == 1)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }
                            }
                        }


                        //for (int nColumn = 0; nColumn < qrContactIds.size; nColumn++)
                        //{
                        TableCell tcSpouse = tblApplicant.FindControl("tcSpouse") as TableCell;

                        if (applicant.Contact__r.cmm_Household_Role__c == "Spouse" && applicant.Contact__r.Name == tcSpouse.Text)
                        {
                            for (int nRow = 1; nRow <= 14; nRow++)
                            {
                                int nColumnHouseholdRole = i + 1;
                                Button btnYes = tblApplicant.FindControl("btnYes_" + nRow + "_" + nColumnHouseholdRole) as Button;
                                Button btnNo = tblApplicant.FindControl("btnNo_" + nRow + "_" + nColumnHouseholdRole) as Button;

                                if (applicant.Treated_in_last_12_months__c == true && nRow == 1 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Treated_in_last_12_months__c == false && nRow == 1 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Diagnosed_with_Cardiovascular_issues__c == true && nRow == 2 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Diagnosed_with_Cardiovascular_issues__c == false && nRow == 2 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Diagnosed_with_Allergy_Respiratory__c == true && nRow == 3 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Diagnosed_with_Allergy_Respiratory__c == false && nRow == 3 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Arthritis_back_nervous_system_iss__c == true && nRow == 4 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;
                                }
                                else if (applicant.Arthritis_back_nervous_system_iss__c == false && nRow == 4 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;
                                }

                                if (applicant.Eyes_nose_ears_hands_feet_conditions__c == true && nRow == 5 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Eyes_nose_ears_hands_feet_conditions__c == false && nRow == 5 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Stomach_liver_colon_kidney_conditions__c == true && nRow == 6 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Stomach_liver_colon_kidney_conditions__c == false && nRow == 6 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Thyroid_tumor_cancer_medical_conditions__c == true && nRow == 7 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Thyroid_tumor_cancer_medical_conditions__c == false && nRow == 7 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Prostate_or_female_reprdct_conditions__c == true && nRow == 8 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Prostate_or_female_reprdct_conditions__c == false && nRow == 8 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.Congenital_disease_or_other_condition__c == true && nRow == 9 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.Congenital_disease_or_other_condition__c == false && nRow == 9 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Currently_Smoking__c == true && nRow == 10 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Currently_Smoking__c == false && nRow == 10 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Former_Smoker__c == true && nRow == 11 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Former_Smoker__c == false && nRow == 11 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == true && nRow == 12 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == false && nRow == 12 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == true && nRow == 13 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == false && nRow == 13 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                                if (applicant.cmm_Drinking_Alcohol__c == true && nRow == 14 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.Red;
                                    btnYes.ForeColor = Color.White;
                                    btnNo.BackColor = Color.LightGray;
                                    btnNo.ForeColor = Color.Black;

                                }
                                else if (applicant.cmm_Drinking_Alcohol__c == false && nRow == 14 && nColumnHouseholdRole == 2)
                                {
                                    btnYes.BackColor = Color.LightGray;
                                    btnYes.ForeColor = Color.Black;
                                    btnNo.BackColor = Color.Blue;
                                    btnNo.ForeColor = Color.White;

                                }

                            }
                        }
                        //}

                        //for (int nColumn = 0; nColumn < qrContactIds.size; nColumn++)
                        //{
                        TableCell tcChild = tblApplicant.FindControl("tcChild_" + i) as TableCell;

                        if (tcChild != null)
                        {
                            if (applicant.Contact__r.cmm_Household_Role__c == "Child" && applicant.Contact__r.Name == tcChild.Text)
                            {
                                for (int nRow = 1; nRow <= 14; nRow++)
                                {
                                    int nColumnHouseholdRole = i + 1;
                                    Button btnYes = tblApplicant.FindControl("btnYes_" + nRow + "_" + nColumnHouseholdRole) as Button;
                                    Button btnNo = tblApplicant.FindControl("btnNo_" + nRow + "_" + nColumnHouseholdRole) as Button;

                                    if (applicant.Treated_in_last_12_months__c == true && nRow == 1 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;
                                    }
                                    else if (applicant.Treated_in_last_12_months__c == false && nRow == 1 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;
                                    }

                                    if (applicant.Diagnosed_with_Cardiovascular_issues__c == true && nRow == 2 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;
                                    }
                                    else if (applicant.Diagnosed_with_Cardiovascular_issues__c == false && nRow == 2 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;
                                    }

                                    if (applicant.Diagnosed_with_Allergy_Respiratory__c == true && nRow == 3 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;
                                    }
                                    else if (applicant.Diagnosed_with_Allergy_Respiratory__c == false && nRow == 3 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;
                                    }

                                    if (applicant.Arthritis_back_nervous_system_iss__c == true && nRow == 4 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;
                                    }
                                    else if (applicant.Arthritis_back_nervous_system_iss__c == false && nRow == 4 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;
                                    }

                                    if (applicant.Eyes_nose_ears_hands_feet_conditions__c == true && nRow == 5 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.Eyes_nose_ears_hands_feet_conditions__c == false && nRow == 5 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.Stomach_liver_colon_kidney_conditions__c == true && nRow == 6 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.Stomach_liver_colon_kidney_conditions__c == false && nRow == 6 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.Thyroid_tumor_cancer_medical_conditions__c == true && nRow == 7 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.Thyroid_tumor_cancer_medical_conditions__c == false && nRow == 7 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.Prostate_or_female_reprdct_conditions__c == true && nRow == 8 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.Prostate_or_female_reprdct_conditions__c == false && nRow == 8 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.Congenital_disease_or_other_condition__c == true && nRow == 9 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.Congenital_disease_or_other_condition__c == false && nRow == 9 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.cmm_Currently_Smoking__c == true && nRow == 10 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.cmm_Currently_Smoking__c == false && nRow == 10 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.cmm_Former_Smoker__c == true && nRow == 11 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.cmm_Former_Smoker__c == false && nRow == 11 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == true && nRow == 12 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.cmm_Currently_Taking_Narcotic_Drug__c == false && nRow == 12 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == true && nRow == 13 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.cmm_Formerly_Taking_Narcotic_Drug__c == false && nRow == 13 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                    if (applicant.cmm_Drinking_Alcohol__c == true && nRow == 14 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.Red;
                                        btnYes.ForeColor = Color.White;
                                        btnNo.BackColor = Color.LightGray;
                                        btnNo.ForeColor = Color.Black;

                                    }
                                    else if (applicant.cmm_Drinking_Alcohol__c == false && nRow == 14 && nColumnHouseholdRole > 2)
                                    {
                                        btnYes.BackColor = Color.LightGray;
                                        btnYes.ForeColor = Color.Black;
                                        btnNo.BackColor = Color.Blue;
                                        btnNo.ForeColor = Color.White;

                                    }

                                }
                            }
                        }
                        //}
                    }
                }
            }


        }

        protected void InitializeHealthHistoryTableHeading(Table table)
        {

            //nNumberOfFamilyMembers = NumberOfFamilyMembers();

            int NumberOfColumns = nNumberOfFamilyMembers + 1;

            headerRow = new TableHeaderRow();
            headerCell = new TableHeaderCell();
            headerCell.Text = strHeading;
            headerCell.ColumnSpan = NumberOfColumns;
            headerCell.Font.Name = "Arial";
            headerCell.Font.Size = FontUnit.Smaller;
            headerCell.Width = 1000;

            headerRow.Cells.Add(headerCell);

            table.BorderColor = Color.Gray;

            table.BorderStyle = BorderStyle.Solid;
            table.BorderWidth = 1;
            table.GridLines = GridLines.Both;
            table.CellPadding = 5;
            table.Rows.Add(headerRow);

        }

        protected void InitizlizeHealthHistoryHeader(Table table)
        {
            headRow = new TableRow();
            headCell = new TableCell();

            headCell.Font.Name = "Arial";
            headCell.Text = strHeader;

            headCell.ColumnSpan = 1;
            headCell.BorderWidth = 1;
            headCell.BorderStyle = BorderStyle.Solid;
            //headCell.BorderColor = Color.Gray;
            //headCell.Width = 350;
            //headCell.Font.Size = FontUnit.Smaller;
            //headCell.HorizontalAlign = HorizontalAlign.Left;
            headCell.CssClass = "MedicalCondition";

            headRow.BorderWidth = 1;
            headRow.BorderStyle = BorderStyle.Solid;
            headRow.BorderColor = Color.LightGray;

            headRow.Cells.Add(headCell);

            table.Rows.Add(headRow);

            PopulateTableHeader(table);

        }

        protected void PopulateTableHeader(Table table)
        {

            String strQueryForHouseholdMembers = "select Name, cmm_Household_Role__c from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrHouseholdMembers = Sfdcbinding.query(strQueryForHouseholdMembers);

            if (qrHouseholdMembers.size > 0)
            {
                for (int i = 0; i < qrHouseholdMembers.size; i++)
                {
                    SForce.Contact member = (SForce.Contact)qrHouseholdMembers.records[i];
                    if (member.cmm_Household_Role__c == "Head of Household")
                    {
                        TableCell tc = new TableCell();
                        tc.Text = member.Name;
                        tc.ColumnSpan = 1;
                        tc.BorderColor = Color.Gray;
                        tc.BorderStyle = BorderStyle.Solid;
                        tc.BorderWidth = 1;
                        tc.Font.Size = FontUnit.Smaller;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;
                        tc.ID = "tcHeadOfHousehold";


                        headRow.Cells.Add(tc);
                        table.Rows.Add(headRow);
                    }
                }
                for (int i = 0; i < qrHouseholdMembers.size; i++)
                {
                    SForce.Contact member = (SForce.Contact)qrHouseholdMembers.records[i];
                    if (member.cmm_Household_Role__c == "Spouse")
                    {
                        TableCell tc = new TableCell();
                        tc.Text = member.Name;
                        tc.ColumnSpan = 1;
                        tc.BorderColor = Color.Gray;
                        tc.BorderStyle = BorderStyle.Solid;
                        tc.BorderWidth = 1;
                        tc.Font.Size = FontUnit.Smaller;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;
                        tc.ID = "tcSpouse";

                        headRow.Cells.Add(tc);
                        table.Rows.Add(headRow);
                    }
                }
                for (int i = 0; i < qrHouseholdMembers.size; i++)
                {
                    SForce.Contact member = (SForce.Contact)qrHouseholdMembers.records[i];
                    if (member.cmm_Household_Role__c == "Child")
                    {
                        TableCell tc = new TableCell();
                        tc.Text = member.Name;
                        tc.ColumnSpan = 1;
                        tc.BorderColor = Color.Gray;
                        tc.BorderStyle = BorderStyle.Solid;
                        tc.BorderWidth = 1;
                        tc.Font.Size = FontUnit.Smaller;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;
                        tc.ID = "tcChild_" + i;

                        headRow.Cells.Add(tc);
                        table.Rows.Add(headRow);
                    }
                }
            }
        }

        protected void InitializedHiddenFields()
        {
            hdnAddressBorderColor.Value = "black";
            hdnAddressBorderWidth.Value = "1";
            hdnAddressFontColor.Value = "black";

            hdnBillingCityBorderColor.Value = "black";
            hdnBillingCityBorderWidth.Value = "1";
            hdnBillingCityFontColor.Value = "black";

            hdnBillingStateBorderColor.Value = "black"; ;
            hdnBillingStateBorderWidth.Value = "1";
            hdnBillingStateFontColor.Value = "black";

            hdnBillingStreetBorderColor.Value = "black";
            hdnBillingStreetBorderWidth.Value = "1";
            hdnBillingStreetFontColor.Value = "black";

            hdnBillingZipCodeBorderColor.Value = "black";
            hdnBillingZipCodeBorderWidth.Value = "1";
            hdnBillingZipCodeFontColor.Value = "black";

            hdnChurchCityBorderColor.Value = "black";
            hdnChurchCityBorderWidth.Value = "1";
            hdnChurchCityFontColor.Value = "black";

            hdnChurchNameBorderColor.Value = "black";
            hdnChurchNameBorderWidth.Value = "1";
            hdnChurchNameFontColor.Value = "black";

            hdnChurchStateBorderColor.Value = "black";
            hdnChurchStateBorderWidth.Value = "1";
            hdnChurchStateFontColor.Value = "black";

            hdnChurchStreetBorderColor.Value = "black";
            hdnChurchStreetBorderWidth.Value = "1";
            hdnChurchStreetFontColor.Value = "black";

            hdnChurchTelephoneBorderColor.Value = "black";
            hdnChurchTelephoneBorderWidth.Value = "1";
            hdnChurchTelephoneFontColor.Value = "black";

            hdnChurchZipBorderColor.Value = "black";
            hdnChurchZipBorderWidth.Value = "1";
            hdnChurchZipFontColor.Value = "black";

            hdnCityBorderColor.Value = "black";
            hdnCityBorderWidth.Value = "1";
            hdnCityFontColor.Value = "black";

            hdnEmailBorderColor.Value = "black";
            hdnEmailBorderWidth.Value = "1";
            hdnEmailFontColor.Value = "black";

            hdnFirstNameBorderWidth.Value = "black";
            hdnFirstNameBorderWidth.Value = "1";
            hdnFirstNameFontColor.Value = "black";

            hdnLastNameBorderColor.Value = "black";
            hdnLastNameBorderWidth.Value = "1";
            hdnLastNameFontColor.Value = "black";

            hdnMiddleNameBorderColor.Value = "black";
            hdnMiddleNameBorderWidth.Value = "1";
            hdnMiddleNameFontColor.Value = "black";

            hdnMobilePhoneBorderColor.Value = "black";
            hdnMobilePhoneBorderWidth.Value = "1";
            hdnMobilePhoneFontColor.Value = "black";

            hdnOtherPhoneBorderColor.Value = "black";
            hdnOtherPhoneBorderWidth.Value = "1";
            hdnOtherPhoneFontColor.Value = "black";

            hdnPastorBorderColor.Value = "black"; ;
            hdnPastorBorderWidth.Value = "1";
            hdnPastorFontColor.Value = "black";

            hdnStateBorderColor.Value = "black";
            hdnStateBorderWidth.Value = "1";
            hdnStateFontColor.Value = "black";

            hdnTelephoneBorderColor.Value = "black";
            hdnTelephoneBorderWidth.Value = "1";
            hdnTelephoneFontColor.Value = "black";

            hdnZipCodeBorderColor.Value = "black";
            hdnZipCodeBorderWidth.Value = "1";
            hdnZipCodeFontColor.Value = "black";

        }

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected SForce.Contact GetContactIdForLogInUser(string strSqlStatement)
        {
            queryResultContactId = Sfdcbinding.query(strSqlStatement);

            SForce.Contact contact = null;

            if (queryResultContactId.size > 0)
            {
                contact = (SForce.Contact)queryResultContactId.records[0];
                Session["ContactId"] = contact.Id.ToString();
            }

            return contact;
        }

        protected void InitializeSQLStatements()
        {
            //strQueryContactForContactID = "select id from Contact where Email = 'seanjones@emailz.com'";

            strQueryContactForContactID = "select id from Contact where Email = '" + strUserEmail + "'";

            //strQueryContactForContactID = "select id, causeview__PortalUser__c, Email from Contact where Email = '" + strUserEmail + "'";


            ctContactId = GetContactIdForLogInUser(strQueryContactForContactID);

            strContactId = ctContactId.Id.ToString();

            //strQueryContactForMichaelJordan = "select Id, c4g_Membership__r.Name, causeview__Suffix__c, FirstName, LastName, MiddleName, Birthdate, Gender__c, " +
            //                                             "Social_Security_Number__c, MailingAddress, Email, Phone, MobilePhone, OtherPhone, causeview__Solicit_Codes__c, " +
            //                                             "causeview__Household_Role__c from Contact where Id = '" + strContactId + "'";

            strQueryContactForMichaelJordan = "select Id, c4g_Membership__r.Name, Title, FirstName, LastName, MiddleName, Birthdate, cmm_Gender__c, " +
                                             "Social_Security_Number__c, MailingAddress, Email, Phone, MobilePhone, OtherPhone, cmm_Solicit_Codes__c, " +
                                             "cmm_Household_Role__c from Contact where Id = '" + strContactId + "'";


            strQueryMembershipInfo = "select c4g_Qualifies_for_Medicare__c, c4g_Qualifies_for_Medicare_A_and_B__c, c4g_Plan__r.Name, Birthdate, " +
                                                "c4g_Church__r.Name, c4g_Church__r.Senior_Pastor_s_Name__c, c4g_Church__r.ShippingAddress, c4g_Church__r.Phone, " +
                                                "c4g_Membership__r.Owner.Name, c4g_Membership__r.Invoice_Delivery__c, " +
                                                "c4g_Membership__r.Referred_By__c, c4g_Membership__r.Referrer__r.Name, c4g_Membership__r.Referrer__r.c4g_Membership__r.Name, " + 
                                                "c4g_Membership__r.Name " +
                                                "from Contact where Id = '" + strContactId + "'";

            //strQueryFamilyInfo = "select causeview__Household__c from Contact where Id = '" + strContactId + "'";
            strQueryFamilyInfo = "select cmm_Household__c from Contact where Id = '" + strContactId + "'";


            strQueryPaymentInfoInMembership = "select c4g_Membership__r.cmm_Payment_Method__c, c4g_Membership__r.cmm_Payment_Frequency__c from Contact " +
                                     "where Id = '" + strContactId + "'";

            //strQueryPaymentInfo = "select causeview__Payment_Type__c, causeview__Donation__r.causeview__Gift_Type__c from causeview__Payment__c " +
            //                                    "where causeview__Donation__r.causeview__Constituent__r.Id = '" + strContactId + "'";

            //strQueryPaymentInfo = "select causeview__Payment_Type__c, causeview__Donation__r.causeview__Gift_Type__c from causeview__Payment__c " +
            //                        "where causeview__Donation__r.causeview__Constituent__r.Id = '" + strContactId + "'";


        }

        protected void LoadPersonalInfo()
        {

            InitializedSfdcbinding();
            InitializeSQLStatements();

            queryResultContactForMichaelJordan = Sfdcbinding.query(strQueryContactForMichaelJordan);

            if (queryResultContactForMichaelJordan.size > 0)
            {
                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                if (ctMichaelJordan.c4g_Membership__r != null)
                {
                    if (ctMichaelJordan.c4g_Membership__r.Name != null) txtPrimaryID.Text = ctMichaelJordan.c4g_Membership__r.Name;
                }
                else if (ctMichaelJordan.c4g_Membership__r == null)
                {
                    txtPrimaryID.Text = "";
                }

                if (ctMichaelJordan.Email != null)
                {
                    txtEmail.Text = ctMichaelJordan.Email;
                    hdnEmail.Value = ctMichaelJordan.Email;
                }
                else if (ctMichaelJordan.Email == null) txtEmail.Text = "";

                if (ctMichaelJordan.Title != null)
                {
                    switch (ctMichaelJordan.Title)
                    {
                        //case "Jr.":
                        //    ddlTitle.SelectedIndex = 0;
                        //    break;
                        case "Mr.":
                            ddlTitle.SelectedIndex = 0;
                            break;
                        case "Mrs.":
                            ddlTitle.SelectedIndex = 1;
                            break;
                        case "Ms.":
                            ddlTitle.SelectedIndex = 2;
                            break;
                            //default:
                            //    ddlTitle.SelectedIndex = 3;
                            //    break;
                    }
                }

                if (ctMichaelJordan.LastName != null)
                {
                    txtLastName.Text = ctMichaelJordan.LastName;
                    hdnLastName.Value = ctMichaelJordan.LastName;
                }
                else if (ctMichaelJordan.LastName == null) txtLastName.Text = "";

                if (ctMichaelJordan.FirstName != null)
                {
                    txtFirstName.Text = ctMichaelJordan.FirstName;
                    hdnFirstName.Value = ctMichaelJordan.FirstName;
                }
                else if (ctMichaelJordan.FirstName == null) txtFirstName.Text = "";

                if (ctMichaelJordan.MiddleName != null)
                {
                    txtMiddleName.Text = ctMichaelJordan.MiddleName;
                    hdnMiddleName.Value = ctMichaelJordan.MiddleName;
                }
                else if (ctMichaelJordan.MiddleName == null) txtMiddleName.Text = "";

                if (ctMichaelJordan.Birthdate != null)
                {
                    DateTime dtMemberBirthdate = new DateTime((int)ctMichaelJordan.Birthdate.Value.Year, (int)ctMichaelJordan.Birthdate.Value.Month, (int)ctMichaelJordan.Birthdate.Value.Day);
                    StringBuilder sbMemberBirthdate = new StringBuilder();
                    sbMemberBirthdate.Append(dtMemberBirthdate.ToShortDateString());
                    txtDateOfBirth.Text = sbMemberBirthdate.ToString();
                }
                else txtDateOfBirth.Text = "";

                if (ctMichaelJordan.cmm_Gender__c == "Male") rbMale.Checked = true;
                else if (ctMichaelJordan.cmm_Gender__c == "Female") rbFemale.Checked = true;
                else if (ctMichaelJordan.cmm_Gender__c == null)
                {
                    rbMale.Checked = false;
                    rbFemale.Checked = false;
                }

                if (ctMichaelJordan.Social_Security_Number__c != null) txtSSN.Text = ctMichaelJordan.Social_Security_Number__c;
                else if (ctMichaelJordan.Social_Security_Number__c == null) txtSSN.Text = "";

                if (ctMichaelJordan.Phone != null)
                {
                    txtTelephone1.Text = ctMichaelJordan.Phone;
                    hdnTelephone.Value = ctMichaelJordan.Phone;
                }
                else if (ctMichaelJordan.Phone == null) txtTelephone1.Text = "";

                if (ctMichaelJordan.MobilePhone != null)
                {
                    txtTelephone2.Text = ctMichaelJordan.MobilePhone;
                    hdnMobilePhone.Value = ctMichaelJordan.MobilePhone;
                }
                else if (ctMichaelJordan.MobilePhone == null) txtTelephone2.Text = "";

                if (ctMichaelJordan.OtherPhone != null)
                {
                    txtTelephone3.Text = ctMichaelJordan.OtherPhone;
                    hdnOtherPhone.Value = ctMichaelJordan.OtherPhone;
                }
                else if (ctMichaelJordan.OtherPhone == null) txtTelephone3.Text = "";

                if (ctMichaelJordan.MailingAddress != null)
                {
                    txtAddress.Text = ctMichaelJordan.MailingAddress.street;
                    hdnAddress.Value = ctMichaelJordan.MailingAddress.street;

                    txtZipCode.Text = ctMichaelJordan.MailingAddress.postalCode;
                    hdnZipCode.Value = ctMichaelJordan.MailingAddress.postalCode;

                    txtState.Text = ctMichaelJordan.MailingAddress.state;
                    hdnState.Value = ctMichaelJordan.MailingAddress.street;

                    txtCity.Text = ctMichaelJordan.MailingAddress.city;
                    hdnCity.Value = ctMichaelJordan.MailingAddress.city;

                }
            }

            queryResultMembershipInfo = Sfdcbinding.query(strQueryMembershipInfo);

            if (queryResultMembershipInfo.size > 0)
            {
                ctMembershipInfo = (SForce.Contact)queryResultMembershipInfo.records[0];

                if (ctMembershipInfo.c4g_Qualifies_for_Medicare__c == true)
                {
                    rbYesQualifyForMedicare.Checked = true;
                    rbNoQualifyForMedicare.Checked = false;
                }
                else if (ctMembershipInfo.c4g_Qualifies_for_Medicare__c == false)
                {
                    rbYesQualifyForMedicare.Checked = false;
                    rbNoQualifyForMedicare.Checked = true;
                }

                if (ctMembershipInfo.c4g_Qualifies_for_Medicare_A_and_B__c == true)
                {
                    rbMedicareABYes.Checked = true;
                    rbMedicareABNo.Checked = false;
                }
                else if (ctMembershipInfo.c4g_Qualifies_for_Medicare_A_and_B__c == false)
                {
                    rbMedicareABYes.Checked = false;
                    rbMedicareABNo.Checked = true;
                }

                if (ctMembershipInfo.c4g_Plan__r != null)
                {
                    if (ctMembershipInfo.c4g_Plan__r.Name != null)
                    {
                        ddlPartipantsProgram.Items.Add(new ListItem(ctMembershipInfo.c4g_Plan__r.Name));
                        ddlPartipantsProgram.SelectedIndex = 0;
                    }
                }

                String strQueryHouseholdRole = "select cmm_Household_Role__c from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrHouseholdRole = Sfdcbinding.query(strQueryHouseholdRole);

                SForce.Contact ctHouseholdRole = null;

                if (qrHouseholdRole.size > 0)
                {
                    ctHouseholdRole = (SForce.Contact)qrHouseholdRole.records[0];
                }

                String strHouseholdRole = String.Empty;
                if (ctHouseholdRole.cmm_Household_Role__c != null) strHouseholdRole = ctHouseholdRole.cmm_Household_Role__c.ToString();

                String strQueryHousehold = "select cmm_Household__c from Contact where Id = '" + strContactId + "'";
                SForce.QueryResult qrHousehold = Sfdcbinding.query(strQueryHousehold);

                if (strHouseholdRole == "Head of Household")
                {
                    if (qrHousehold.size > 0)
                    {
                        SForce.Contact ctHousehold = (SForce.Contact)qrHousehold.records[0];

                        String strSpouseInfo = "select c4g_Plan__r.Name, Birthdate from Contact where cmm_Household__c = '" + ctHousehold.cmm_Household__c.ToString() +
                                                "' " + "and cmm_Household_Role__c = 'Spouse'";

                        queryResultSpouseProgram = Sfdcbinding.query(strSpouseInfo);

                        if (queryResultSpouseProgram.size > 0)
                        {
                            SForce.Contact ctSpouse = (SForce.Contact)queryResultSpouseProgram.records[0];

                            ddlSpouseProgram.Items.Add(new ListItem(ctSpouse.c4g_Plan__r.Name));
                            ddlSpouseProgram.SelectedIndex = 0;
                        }

                        String strChildrenInfo = "select c4g_Plan__r.Name, Birthdate from Contact where cmm_Household__c = '" + ctHousehold.cmm_Household__c.ToString() +
                                                 "' " + "and cmm_Household_Role__c = 'Child'";

                        queryResultChildrenProgram = Sfdcbinding.query(strChildrenInfo);

                        if (queryResultChildrenProgram.size > 0)
                        {
                            SForce.Contact ctChild = (SForce.Contact)queryResultChildrenProgram.records[0];

                            ddlChildrenProgram.Items.Add(ctChild.c4g_Plan__r.Name);
                            ddlChildrenProgram.SelectedIndex = 0;
                        }
                    }
                }
                else if (strHouseholdRole == "Spouse")
                {
                    if (qrHousehold.size > 0)
                    {
                        SForce.Contact ctHousehold = (SForce.Contact)qrHousehold.records[0];

                        String strHouseholdInfo = "select c4g_Plan__r.Name, Birthdate from Contact where cmm_Household__c = '" + ctHousehold.cmm_Household__c.ToString() +
                                                "' " + "and cmm_Household_Role__c = 'Head of Household'";

                        queryResultSpouseProgram = Sfdcbinding.query(strHouseholdInfo);

                        if (queryResultSpouseProgram.size > 0)
                        {
                            SForce.Contact ctSpouse = (SForce.Contact)queryResultSpouseProgram.records[0];

                            ddlSpouseProgram.Items.Add(new ListItem(ctSpouse.c4g_Plan__r.Name));
                            ddlSpouseProgram.SelectedIndex = 0;
                        }

                        String strChildrenInfo = "select c4g_Plan__r.Name, Birthdate from Contact where cmm_Household__c = '" + ctHousehold.cmm_Household__c.ToString() +
                                                 "' " + "and cmm_Household_Role__c = 'Child'";

                        queryResultChildrenProgram = Sfdcbinding.query(strChildrenInfo);

                        if (queryResultChildrenProgram.size > 0)
                        {
                            SForce.Contact ctChild = (SForce.Contact)queryResultChildrenProgram.records[0];

                            ddlChildrenProgram.Items.Add(ctChild.c4g_Plan__r.Name);
                            ddlChildrenProgram.SelectedIndex = 0;
                        }
                    }
                }

                if (ctMembershipInfo.c4g_Church__r != null)
                {
                    if (ctMembershipInfo.c4g_Church__r.Name != null)
                    {
                        txtChurchName.Text = ctMembershipInfo.c4g_Church__r.Name;
                        hdnChurchName.Value = ctMembershipInfo.c4g_Church__r.Name;
                    }
                    if (ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c != null)
                    {
                        txtSeniorPastor.Text = ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c;
                        hdnSeniorPastor.Value = ctMembershipInfo.c4g_Church__r.Senior_Pastor_s_Name__c;
                    }
                    if (ctMembershipInfo.c4g_Church__r.ShippingAddress != null)
                    {
                        txtChurchStreet.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.street;
                        hdnChurchStreet.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.street;

                        txtChurchZip.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.postalCode;
                        hdnChurchStreet.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.postalCode;

                        txtChurchState.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.state;
                        hdnChurchState.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.state;

                        txtChurchCity.Text = ctMembershipInfo.c4g_Church__r.ShippingAddress.city;
                        hdnChurchCity.Value = ctMembershipInfo.c4g_Church__r.ShippingAddress.city;
                    }
                    if (ctMembershipInfo.c4g_Church__r.Phone != null)
                    {
                        txtChurchTelephone.Text = ctMembershipInfo.c4g_Church__r.Phone;
                        hdnChurchTelephone.Value = ctMembershipInfo.c4g_Church__r.Phone;
                    }
                }
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Family Information
            //////////////////////////////////


            queryResultFamilyInfo = Sfdcbinding.query(strQueryFamilyInfo);

            if (queryResultFamilyInfo.size > 0)
            {
                ctFamilyInfo = (SForce.Contact)queryResultFamilyInfo.records[0];

                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                SForce.Contact spouse = null;

                String strQueryForChildren = "select FirstName, LastName, Birthdate, c4g_Membership_Start_Date__c, cmm_Household_Role__c from Contact " +
                             "where cmm_Household__c = '" + ctFamilyInfo.cmm_Household__c.ToString() + "' " +
                             "and cmm_Household_Role__c = 'Child'";

                if (ctMichaelJordan.cmm_Household_Role__c == "Head of Household")
                {
                    String strQueryForSpouse = "select FirstName, LastName, Birthdate,  c4g_Membership_Start_Date__c, cmm_Household_Role__c from Contact " +
                                               "where cmm_Household__c = '" + ctFamilyInfo.cmm_Household__c.ToString() +
                                               "' and cmm_Household_Role__c = 'Spouse'";

                    queryResultSpouseInfo = Sfdcbinding.query(strQueryForSpouse);

                    if (queryResultSpouseInfo.size > 0)
                    {
                        spouse = (SForce.Contact)queryResultSpouseInfo.records[0];

                        StringBuilder sbName = new StringBuilder();
                        sbName.Append(spouse.LastName);
                        sbName.Append(", ");
                        sbName.Append(spouse.FirstName);
                        lblSpouseName.Text = sbName.ToString();

                        lblSpouseNameLabel.Visible = true;
                        lblSpouseName.Visible = true;

                        DateTime dtSpouseBirthdate = new DateTime();

                        if (spouse.Birthdate != null) dtSpouseBirthdate = new DateTime((int)spouse.Birthdate.Value.Year, (int)spouse.Birthdate.Value.Month, (int)spouse.Birthdate.Value.Day);

                        StringBuilder sbBirthDate = new StringBuilder();
                        sbBirthDate.Append(dtSpouseBirthdate.ToShortDateString());
                        lblSpouseDateOfBirth.Text = sbBirthDate.ToString();

                        lblSpouseDateOfBirthLabel.Visible = true;
                        lblSpouseDateOfBirth.Visible = true;

                        DateTime dtSpouseStartDate = new DateTime();

                        if (spouse.c4g_Membership_Start_Date__c != null) dtSpouseStartDate = new DateTime((int)spouse.c4g_Membership_Start_Date__c.Value.Year,
                                                                                                          (int)spouse.c4g_Membership_Start_Date__c.Value.Month,
                                                                                                          (int)spouse.c4g_Membership_Start_Date__c.Value.Day);
                        StringBuilder sbStartDate = new StringBuilder();
                        sbStartDate.Append(dtSpouseStartDate.ToShortDateString());
                        lblSpouseStartDate.Text = sbStartDate.ToString();

                        lblSpouseStartDateLabel.Visible = true;
                        lblSpouseStartDate.Visible = true;
                    }
                    else
                    {
                        lblSpouseName.Width = 300;
                        lblSpouseName.Text = "No spouse has been added";
                        lblSpouseName.Visible = true;
                    }

                    queryResultChildrenInfo = Sfdcbinding.query(strQueryForChildren);

                    if (queryResultChildrenInfo.size > 0)
                    {
                        SForce.Contact[] child = new SForce.Contact[queryResultChildrenInfo.size];

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            child[i] = (SForce.Contact)queryResultChildrenInfo.records[i];
                        }

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        StringBuilder[] sbChildName = new StringBuilder[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenDateOfBirth = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenDateOfBirth = new DateTime[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenStartDate = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenStartDate = new DateTime[queryResultChildrenInfo.size];

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            if (child[i] != null)
                            {

                                sbChildName[i] = new StringBuilder();

                                sbChildName[i].Append(child[i].LastName);
                                sbChildName[i].Append(", ");
                                sbChildName[i].Append(child[i].FirstName);

                                if (child[i].Birthdate != null)
                                {
                                    dtChildrenDateOfBirth[i] = new DateTime((int)child[i].Birthdate.Value.Year, (int)child[i].Birthdate.Value.Month, (int)child[i].Birthdate.Value.Day);
                                    sbChildrenDateOfBirth[i] = new StringBuilder();
                                    sbChildrenDateOfBirth[i].Append(dtChildrenDateOfBirth[i].ToShortDateString());
                                }

                                if (child[i].c4g_Membership_Start_Date__c != null)
                                {

                                    dtChildrenStartDate[i] = new DateTime((int)child[i].c4g_Membership_Start_Date__c.Value.Year,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Month,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Day);
                                    sbChildrenStartDate[i] = new StringBuilder();
                                    sbChildrenStartDate[i].Append(dtChildrenStartDate[i].ToShortDateString());
                                }
                            }

                            switch (i)
                            {
                                case 0:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName1.Text = sbChildName[i].ToString();
                                        lblChildName1.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth1.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate1.Visible = true;
                                    }
                                    break;
                                case 1:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName2.Text = sbChildName[i].ToString();
                                        lblChildName2.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth2.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate2.Visible = true;
                                    }
                                    break;
                                case 2:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName3.Text = sbChildName[i].ToString();
                                        lblChildName3.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth3.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate3.Visible = true;
                                    }
                                    break;
                                case 3:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName4.Text = sbChildName[i].ToString();
                                        lblChildName4.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth4.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate4.Visible = true;
                                    }
                                    break;
                                case 4:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName5.Text = sbChildName[i].ToString();
                                        lblChildName5.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth5.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate5.Visible = true;
                                    }
                                    break;
                                case 5:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName6.Text = sbChildName[i].ToString();
                                        lblChildName6.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth6.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate6.Visible = true;
                                    }
                                    break;
                                case 6:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName7.Text = sbChildName[i].ToString();
                                        lblChildName7.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth7.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate7.Visible = true;
                                    }
                                    break;
                                case 7:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName8.Text = sbChildName[i].ToString();
                                        lblChildName8.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth8.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate8.Visible = true;
                                    }
                                    break;
                                case 8:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName9.Text = sbChildName[i].ToString();
                                        lblChildName9.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth9.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate9.Visible = true;
                                    }
                                    break;
                                case 9:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName10.Text = sbChildName[i].ToString();
                                        lblChildName10.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth10.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate10.Visible = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lblChildName1.Visible = true;
                        lblChildName1.Width = 300;
                        lblChildName1.Text = "No child has been added";
                    }
                }
                else if (ctMichaelJordan.cmm_Household_Role__c == "Spouse")
                {
                    String strQueryForHead = "select FirstName, LastName, Birthdate, c4g_Membership_Start_Date__c, cmm_Household_Role__c from Contact " +
                                             "where cmm_Household__c = '" + ctFamilyInfo.cmm_Household__c.ToString() + "' " +
                                             "and cmm_Household_Role__c = 'Head of Household'";

                    queryResultSpouseInfo = Sfdcbinding.query(strQueryForHead);

                    if (queryResultSpouseInfo.size > 0)
                    {

                        spouse = (SForce.Contact)queryResultSpouseInfo.records[0];

                        StringBuilder sbName = new StringBuilder();
                        sbName.Append(spouse.LastName);
                        sbName.Append(", ");
                        sbName.Append(spouse.FirstName);
                        lblSpouseName.Text = sbName.ToString();

                        lblSpouseNameLabel.Visible = true;
                        lblSpouseName.Visible = true;

                        DateTime dtSpouseBirthdate = new DateTime();

                        if (spouse.Birthdate != null)
                        {
                            dtSpouseBirthdate = new DateTime((int)spouse.Birthdate.Value.Year, (int)spouse.Birthdate.Value.Month, (int)spouse.Birthdate.Value.Day);

                            StringBuilder sbBirthDate = new StringBuilder();
                            sbBirthDate.Append(dtSpouseBirthdate.ToShortDateString());
                            lblSpouseDateOfBirth.Text = sbBirthDate.ToString();
                        }

                        lblSpouseDateOfBirthLabel.Visible = true;
                        lblSpouseDateOfBirth.Visible = true;

                        DateTime dtSpouseStartDate = new DateTime();

                        if (spouse.c4g_Membership_Start_Date__c != null)
                        {
                            dtSpouseStartDate = new DateTime((int)spouse.c4g_Membership_Start_Date__c.Value.Year, (int)spouse.c4g_Membership_Start_Date__c.Value.Month, (int)spouse.c4g_Membership_Start_Date__c.Value.Day);
                            StringBuilder sbStartDate = new StringBuilder();
                            sbStartDate.Append(dtSpouseStartDate.ToShortDateString());
                            lblSpouseStartDate.Text = sbStartDate.ToString();
                        }

                        lblSpouseStartDateLabel.Visible = true;
                        lblSpouseStartDate.Visible = true;

                    }
                    else
                    {
                        lblSpouseName.Width = 300;
                        lblSpouseName.Text = "No spouse has been added";
                        lblSpouseName.Visible = true;
                    }

                    queryResultChildrenInfo = Sfdcbinding.query(strQueryForChildren);

                    if (queryResultChildrenInfo.size > 0)
                    {
                        SForce.Contact[] child = new SForce.Contact[queryResultChildrenInfo.size];


                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            child[i] = (SForce.Contact)queryResultChildrenInfo.records[i];

                        }

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        StringBuilder[] sbChildName = new StringBuilder[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenDateOfBirth = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenDateOfBirth = new DateTime[queryResultChildrenInfo.size];
                        StringBuilder[] sbChildrenStartDate = new StringBuilder[queryResultChildrenInfo.size];
                        DateTime[] dtChildrenStartDate = new DateTime[queryResultChildrenInfo.size];

                        lblChildrenName.Visible = true;
                        lblChildrenDateOfBirth.Visible = true;
                        lblChildrenStartDate.Visible = true;

                        for (int i = 0; i < queryResultChildrenInfo.size; i++)
                        {
                            if (child[i] != null)
                            {
                                sbChildName[i] = new StringBuilder();

                                sbChildName[i].Append(child[i].LastName);
                                sbChildName[i].Append(", ");
                                sbChildName[i].Append(child[i].FirstName);

                                dtChildrenDateOfBirth[i] = new DateTime();

                                if (child[i].Birthdate != null)
                                {
                                    dtChildrenDateOfBirth[i] = new DateTime((int)child[i].Birthdate.Value.Year, (int)child[i].Birthdate.Value.Month, (int)child[i].Birthdate.Value.Day);
                                    sbChildrenDateOfBirth[i] = new StringBuilder();
                                    sbChildrenDateOfBirth[i].Append(dtChildrenDateOfBirth[i].ToShortDateString());
                                }

                                if (child[i].c4g_Membership_Start_Date__c != null)
                                {
                                    dtChildrenStartDate[i] = new DateTime((int)child[i].c4g_Membership_Start_Date__c.Value.Year,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Month,
                                                                            (int)child[i].c4g_Membership_Start_Date__c.Value.Day);
                                    sbChildrenStartDate[i] = new StringBuilder();
                                    sbChildrenStartDate[i].Append(dtChildrenStartDate[i].ToShortDateString());
                                }
                            }

                            switch (i)
                            {
                                case 0:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName1.Text = sbChildName[i].ToString();
                                        lblChildName1.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth1.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate1.Visible = true;
                                    }
                                    //lblChildName1.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth1.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate1.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName1.Visible = true;
                                    //lblChildDateOfBirth1.Visible = true;
                                    //lblChildStartDate1.Visible = true;
                                    break;
                                case 1:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName2.Text = sbChildName[i].ToString();
                                        lblChildName2.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth2.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate2.Visible = true;
                                    }

                                    //lblChildName2.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth2.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate2.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName2.Visible = true;
                                    //lblChildDateOfBirth2.Visible = true;
                                    //lblChildStartDate2.Visible = true;
                                    break;
                                case 2:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName3.Text = sbChildName[i].ToString();
                                        lblChildName3.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth3.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate3.Visible = true;
                                    }

                                    //lblChildName3.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth3.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate3.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName3.Visible = true;
                                    //lblChildDateOfBirth3.Visible = true;
                                    //lblChildStartDate3.Visible = true;
                                    break;
                                case 3:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName4.Text = sbChildName[i].ToString();
                                        lblChildName4.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth4.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate4.Visible = true;
                                    }


                                    //lblChildName4.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth4.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate4.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName4.Visible = true;
                                    //lblChildDateOfBirth4.Visible = true;
                                    //lblChildStartDate4.Visible = true;
                                    break;
                                case 4:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName5.Text = sbChildName[i].ToString();
                                        lblChildName5.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth5.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate5.Visible = true;
                                    }


                                    //lblChildName5.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth5.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate5.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName5.Visible = true;
                                    //lblChildDateOfBirth5.Visible = true;
                                    //lblChildStartDate5.Visible = true;
                                    break;
                                case 5:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName6.Text = sbChildName[i].ToString();
                                        lblChildName6.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth6.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate6.Visible = true;
                                    }


                                    //lblChildName6.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth6.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate6.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName6.Visible = true;
                                    //lblChildDateOfBirth6.Visible = true;
                                    //lblChildStartDate6.Visible = true;
                                    break;
                                case 6:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName7.Text = sbChildName[i].ToString();
                                        lblChildName7.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth7.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate7.Visible = true;
                                    }


                                    //lblChildName7.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth7.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate7.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName7.Visible = true;
                                    //lblChildDateOfBirth7.Visible = true;
                                    //lblChildStartDate7.Visible = true;
                                    break;
                                case 7:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName8.Text = sbChildName[i].ToString();
                                        lblChildName8.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth8.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate8.Visible = true;
                                    }

                                    //lblChildName8.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth8.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate8.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName8.Visible = true;
                                    //lblChildDateOfBirth8.Visible = true;
                                    //lblChildStartDate8.Visible = true;
                                    break;

                                case 8:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName9.Text = sbChildName[i].ToString();
                                        lblChildName9.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth9.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate9.Visible = true;
                                    }

                                    //lblChildName9.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth9.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate9.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName9.Visible = true;
                                    //lblChildDateOfBirth9.Visible = true;
                                    //lblChildStartDate9.Visible = true;
                                    break;

                                case 9:
                                    if (sbChildName[i] != null)
                                    {
                                        lblChildName10.Text = sbChildName[i].ToString();
                                        lblChildName10.Visible = true;
                                    }
                                    if (sbChildrenDateOfBirth[i] != null)
                                    {
                                        lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                        lblChildDateOfBirth10.Visible = true;
                                    }
                                    if (sbChildrenStartDate[i] != null)
                                    {
                                        lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                        lblChildStartDate10.Visible = true;
                                    }


                                    //lblChildName10.Text = sbChildName[i].ToString();
                                    //lblChildDateOfBirth10.Text = sbChildrenDateOfBirth[i].ToString();
                                    //lblChildStartDate10.Text = sbChildrenStartDate[i].ToString();
                                    //lblChildName10.Visible = true;
                                    //lblChildDateOfBirth10.Visible = true;
                                    //lblChildStartDate10.Visible = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lblChildName1.Visible = true;
                        lblChildName1.Width = 300;
                        lblChildName1.Text = "No child has been added";
                    }

                }
                else if (ctMichaelJordan.cmm_Household_Role__c == "Child")
                {
                    lblSpouseName.Width = 300;
                    lblSpouseName.Text = "No spouse has been added.";
                    lblSpouseName.Visible = true;

                    lblChildName1.Visible = true;
                    lblChildName1.Width = 300;
                    lblChildName1.Text = "No child has been added";
                }
            }

            ///// End of family information
            ////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////

            queryResultPaymentInfo = Sfdcbinding.query(strQueryPaymentInfoInMembership);

            if (queryResultPaymentInfo.size > 0)
            {
                //paymentInfo = (SForce.causeview__Payment__c)queryResultPaymentInfo.records[0];
                //paymentInfoInMembership = (SForce.Membership__c)queryResultPaymentInfo.records[0];
                ctPaymentInfoInMembership = (SForce.Contact)queryResultPaymentInfo.records[0];

                if (ctPaymentInfoInMembership.c4g_Membership__r != null)
                {
                    if (ctPaymentInfoInMembership.c4g_Membership__r.cmm_Payment_Method__c == "Check") rbCheck.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.cmm_Payment_Method__c == "ACH/PAD") rbBankACH.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.cmm_Payment_Method__c == "Credit Card") rbCreditCard.Checked = true;

                    if (ctPaymentInfoInMembership.c4g_Membership__r.cmm_Payment_Frequency__c == "Recurring") rbRecurring.Checked = true;
                    if (ctPaymentInfoInMembership.c4g_Membership__r.cmm_Payment_Frequency__c == "One Time Gift") rbOneTime.Checked = true;
                }
            }

            String strQueryMemberAddress = "select MailingAddress, OtherAddress from Contact where Id = '" + strContactId + "'";
            SForce.QueryResult qrMemberAddress = Sfdcbinding.query(strQueryMemberAddress);

            if (qrMemberAddress.size > 0)
            {
                SForce.Contact ctMemberAddresses = (SForce.Contact)qrMemberAddress.records[0];

                if (ctMemberAddresses.OtherAddress != null)
                {
                    chkBillingAddress.Checked = true;

                    tabContainerRegister.Height = 1200;
                    pnlUpdateCancel.CssClass = "GeneralInfoButtonWithBillingAddress";


                    ////////////////////////////////////////////

                    txtBillingStreet.Text = ctMemberAddresses.OtherAddress.street;
                    hdnBillingStreetAddress.Value = ctMemberAddresses.OtherAddress.street;

                    txtBillingZipCode.Text = ctMemberAddresses.OtherAddress.postalCode;
                    hdnBillingZipCode.Value = ctMemberAddresses.OtherAddress.postalCode;

                    txtBillingState.Text = ctMemberAddresses.OtherAddress.state;
                    hdnBillingState.Value = ctMemberAddresses.OtherAddress.state;

                    txtBillingCity.Text = ctMemberAddresses.OtherAddress.city;
                    hdnBillingCity.Value = ctMemberAddresses.OtherAddress.city;

                    pnlBillingAddress.Visible = true;
                    pnlBillingStreet.Visible = true;
                    pnlBillingZipCode.Visible = true;
                    pnlBillingState.Visible = true;
                    pnlBillingCity.Visible = true;
                    pnlBillingSaveButton.Visible = true;

                    lblBillingStreet.Visible = true;
                    lblBillingZipCode.Visible = true;
                    lblBillingState.Visible = true;
                    lblBillingCity.Visible = true;

                    txtBillingStreet.Visible = true;
                    txtBillingZipCode.Visible = true;
                    txtBillingState.Visible = true;
                    txtBillingCity.Visible = true;
                    btnBillingAddressSave.Text = "Update";
                    btnBillingAddressSave.Visible = true;

                }
            }

            SForce.DescribeSObjectResult describeSObjectResult = Sfdcbinding.describeSObject("Membership__c");

            // Get the fields
            if (describeSObjectResult != null)
            {
                SForce.Field[] fields = describeSObjectResult.fields;

                for (int i = 0; i < fields.Length; i++)
                {
                    SForce.Field field = fields[i];

                    if (field.name == "Referred_By__c")
                    {
                        SForce.PicklistEntry[] pickListValues = field.picklistValues;
                        if (pickListValues != null)
                        {
                            //ddlReferredBy.Items.Clear();
                            foreach (SForce.PicklistEntry pickListEntry in pickListValues)
                            {
                                ddlReferredBy.Items.Add(pickListEntry.value);
                            }
                        }
                    }
                }
            }

            if (ctMembershipInfo.c4g_Membership__r.Referred_By__c == "Member Referral")
            {
                //ddlReferredBy.Items.Add(new ListItem(ctMembershipInfo.c4g_Membership__r.Referred_By__c));
                //ddlReferredBy.SelectedIndex = 0;
                ddlReferredBy.SelectedValue = "Member Referral";
                ddlReferredBy.Enabled = true;

                txtReferredByMembership.Text = ctMembershipInfo.c4g_Membership__r.Referrer__r.c4g_Membership__r.Name;
                txtReferredByMembership.Enabled = true;

                String strQueryForMembership = "select Name from Contact where c4g_Membership__r.Name = '" + txtReferredByMembership.Text + "'";

                SForce.QueryResult qrMembership = Sfdcbinding.query(strQueryForMembership);

                if (qrMembership.size > 0)
                {
                    for (int i = 0; i < qrMembership.size; i++)
                    {
                        SForce.Contact ctMembership = qrMembership.records[i] as SForce.Contact;

                        ddlReferredByContact.Items.Add(new ListItem(ctMembership.Name));
                    }
                    ddlReferredByContact.SelectedValue = ctMembershipInfo.c4g_Membership__r.Referrer__r.Name;
                    ddlReferredByContact.Enabled = true;
                }
            }
            else
            {
                ddlReferredBy.Items.Add(new ListItem(ctMembershipInfo.c4g_Membership__r.Referred_By__c));
                ddlReferredBy.SelectedIndex = 0;
                txtReferredByMembership.Text = "";
                ddlReferredByContact.Items.Clear();
            }

            if (ctMembershipInfo.c4g_Membership__r != null)
            {
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Email")
                {
                    chkEmail.Checked = true;
                    chkPostal.Checked = false;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Postal")
                {
                    chkEmail.Checked = false;
                    chkPostal.Checked = true;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Both")
                {
                    chkEmail.Checked = true;
                    chkPostal.Checked = true;
                }
                if (ctMembershipInfo.c4g_Membership__r.Invoice_Delivery__c == "Neither")
                {
                    chkEmail.Checked = false;
                    chkPostal.Checked = false;
                }
            }


            if (queryResultContactForMichaelJordan.size > 0)
            {
                ctMichaelJordan = (SForce.Contact)queryResultContactForMichaelJordan.records[0];

                if (ctMichaelJordan.cmm_Solicit_Codes__c != null)
                {
                    if (ctMichaelJordan.cmm_Solicit_Codes__c.Contains("Allow Postal Mail"))
                    {
                        rbYesJoinMailing.Checked = true;
                        rbNoJoinMailing.Checked = false;
                    }
                    else
                    {
                        rbYesJoinMailing.Checked = false;
                        rbNoJoinMailing.Checked = true;
                    }

                    if (ctMichaelJordan.cmm_Solicit_Codes__c.Contains("Allow SMS Messages"))
                    {
                        rbYesAllowMessages.Checked = true;
                        rbNoAllowMessages.Checked = false;
                    }
                    else
                    {
                        rbYesAllowMessages.Checked = false;
                        rbNoAllowMessages.Checked = true;
                    }

                    if (ctMichaelJordan.cmm_Solicit_Codes__c.Contains("No Communication of Any Kind"))
                    {
                        rbYesJoinMailing.Checked = false;
                        rbNoJoinMailing.Checked = true;
                        rbYesAllowMessages.Checked = false;
                        rbNoAllowMessages.Checked = true;
                    }
                }
            }

            //lblPersonalInfoUpdateMessage.Text = Session["EmailConfirmed"].ToString();

            SetHiddenTelephoneLabels(hdnTelephoneBorderWidth.Value, hdnTelephoneBorderColor.Value, hdnTelephoneFontColor.Value);

            // From this line the members' health history would be added
            //

            //nNumberOfFamilyMembers = NumberOfFamilyMembers();
            //Session["NumberOfFamilyMembers"] = nNumberOfFamilyMembers;
            String strAccountId = String.Empty;

            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];

            String strQueryForHousehold = "select Id, Name, cmm_Household_Role__c from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrContactIdForHousehold = Sfdcbinding.query(strQueryForHousehold);

            if (qrContactIdForHousehold.size > 0)
            {
                //tpnlHealthHistory.Height = 1200 + 300 * qrContactIdForHousehold.size;

                string strQueryForNumberOfTreatmentRecord = "select Id from Medical_History__c where Contact__r.cmm_Household__c = '" + strAccountId + "'";

                SForce.QueryResult qrNumberOfTreatmentRecord = Sfdcbinding.query(strQueryForNumberOfTreatmentRecord);

                if (qrNumberOfTreatmentRecord.size > 0) tpnlHealthHistory.Height = 1500 + 385 * qrNumberOfTreatmentRecord.size;

                for (int i = 0; i < qrContactIdForHousehold.size; i++)
                {
                    SForce.Contact ctHouseholdMember = qrContactIdForHousehold.records[i] as SForce.Contact;

                    String strQueryForTreatmentInfo = "select Treatment_Date__c, Treatment_Details__c, Physician_Information__c from Medical_History__c " +
                                                      "where Contact__c = '" + ctHouseholdMember.Id + "'";

                    SForce.QueryResult qrTreatmentRecord = Sfdcbinding.query(strQueryForTreatmentInfo);

                    if (qrTreatmentRecord.size > 0)
                    {

                        for (int nTreatmentNo = 0; nTreatmentNo < qrTreatmentRecord.size; nTreatmentNo++)
                        {
                            SForce.Medical_History__c treatmentRecord = qrTreatmentRecord.records[nTreatmentNo] as SForce.Medical_History__c;

                            lstTreatmentHistory.Add(new TreatmentInfo
                            {
                                Name = ctHouseholdMember.Name,
                                HouseholdRole = ctHouseholdMember.cmm_Household_Role__c,
                                TreatmentDate = new DateTime(treatmentRecord.Treatment_Date__c.Value.Year, treatmentRecord.Treatment_Date__c.Value.Month, treatmentRecord.Treatment_Date__c.Value.Day),
                                TreatmentDescription = treatmentRecord.Treatment_Details__c,
                                PhysicianInfo = treatmentRecord.Physician_Information__c
                            });
                        }
                    }
                }
            }

            lvTreatmentHistory.Items.Clear();
            lvTreatmentHistory.DataSource = lstTreatmentHistory;
            lvTreatmentHistory.DataBind();

            String strQueryForPrimaryName = "select Name from Contact where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Head of Household'";

            SForce.QueryResult qrHouseholdName = Sfdcbinding.query(strQueryForPrimaryName);

            if (qrHouseholdName.size > 0)
            {
                SForce.Contact ctHeadOfHouseholdName = qrHouseholdName.records[0] as SForce.Contact;

                txtPrimarySignature.Text = ctHeadOfHouseholdName.Name;
            }

            String strQueryForSignatureDate = "select Registration_Date__c from Membership__c where Email__c = '" + strUserEmail + "'";

            SForce.QueryResult qrSignatureDate = Sfdcbinding.query(strQueryForSignatureDate);

            if (qrSignatureDate.size > 0)
            {
                SForce.Membership__c memSignatureDate = qrSignatureDate.records[0] as SForce.Membership__c;

                txtSignedDate.Text = memSignatureDate.Registration_Date__c.Value.ToString("MM/dd/yyyy");

            }

            String strQueryGiftHistory = "select cmm_Household_Role__c, Name, Individual_ID__c, c4g_Membership__r.Registration_Date__c, c4g_Plan__r.Name, " +
                                         "c4g_Monthly_Gift_Amount__c, c4g_Membership__r.c4g_Calculated_Monthly_Membership_Gift__c, c4g_Total_Shared_Amount__c " +
                                         "from Contact where cmm_Household__c = '" + strAccountId + "' order by Birthdate";

            SForce.QueryResult qrGiftHistory = Sfdcbinding.query(strQueryGiftHistory);

            if (qrGiftHistory.size > 0)
            {

                List<SForce.Contact> lstGiftHistory = new List<SForce.Contact>();

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    SForce.Contact ctGiftHistory = qrGiftHistory.records[i] as SForce.Contact;

                    if (ctGiftHistory.cmm_Household_Role__c == "Head of Household") lstGiftHistory.Add(ctGiftHistory);
                }
                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    SForce.Contact ctGiftHistory = qrGiftHistory.records[i] as SForce.Contact;

                    if (ctGiftHistory.cmm_Household_Role__c == "Spouse") lstGiftHistory.Add(ctGiftHistory);
                }
                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    SForce.Contact ctGiftHistory = qrGiftHistory.records[i] as SForce.Contact;

                    if (ctGiftHistory.cmm_Household_Role__c == "Child") lstGiftHistory.Add(ctGiftHistory);
                }

                Table tblGiftHistorySummary = new Table();
                tblGiftHistorySummary.GridLines = GridLines.Both;
                tblGiftHistorySummary.CssClass = "tblGiftHistorySummary";
                tblGiftHistorySummary.Width = 1040;
                tblGiftHistorySummary.CellPadding = 5;
                tblGiftHistorySummary.Font.Size = FontUnit.Medium;

                TableRow trHeading = new TableRow();
                TableCell tcEmpty = new TableCell();
                tcEmpty.Height = 30;
                tcEmpty.Width = 200;
                trHeading.Cells.Add(tcEmpty);

                TableRow trName = new TableRow();
                TableCell tcRowName = new TableCell();
                tcRowName.Height = 25;
                tcRowName.Width = 200;
                tcRowName.Text = "Name";
                trName.Cells.Add(tcRowName);

                TableRow trIndividualId = new TableRow();
                TableCell tcRowId = new TableCell();
                tcRowId.Height = 25;
                tcRowId.Width = 200;
                tcRowId.Text = "Id";
                trIndividualId.Cells.Add(tcRowId);

                TableRow trRegistrationDate = new TableRow();
                TableCell tcRowRegistrationDate = new TableCell();
                tcRowRegistrationDate.Height = 25;
                tcRowRegistrationDate.Width = 200;
                tcRowRegistrationDate.Text = "Reg. Date";
                trRegistrationDate.Cells.Add(tcRowRegistrationDate);

                TableRow trProgram = new TableRow();
                TableCell tcRowProgram = new TableCell();
                tcRowProgram.Height = 25;
                tcRowProgram.Width = 200;
                tcRowProgram.Text = "Program";
                trProgram.Cells.Add(tcRowProgram);

                TableRow trTotalShare = new TableRow();
                TableCell tcRowNPTotalShare = new TableCell();
                tcRowNPTotalShare.Height = 25;
                tcRowNPTotalShare.Width = 200;
                tcRowNPTotalShare.Text = "NP Total";
                trTotalShare.Cells.Add(tcRowNPTotalShare);

                TableRow trGiftAmount = new TableRow();
                TableCell tcRowGiftAmount = new TableCell();
                tcRowGiftAmount.Height = 25;
                tcRowGiftAmount.Width = 200;
                tcRowGiftAmount.Text = "Gift Amount";
                trGiftAmount.Cells.Add(tcRowGiftAmount);


                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcHouseholdRole = new TableCell();
                    tcHouseholdRole.Width = 800 / qrGiftHistory.size;
                    tcHouseholdRole.Font.Name = "Arial";
                    tcHouseholdRole.Font.Bold = false;
                    if (lstGiftHistory[i].cmm_Household_Role__c == "Head of Household") tcHouseholdRole.Text = "Member";
                    else tcHouseholdRole.Text = lstGiftHistory[i].cmm_Household_Role__c;
                    trHeading.Cells.Add(tcHouseholdRole);
                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        tcTotal.Text = "Total";
                        tcTotal.Font.Name = "Arial";
                        tcTotal.Font.Bold = false;
                        trHeading.Cells.Add(tcTotal);
                    }
                }
                tblGiftHistorySummary.Rows.Add(trHeading);

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcName = new TableCell();
                    tcName.Width = 800 / qrGiftHistory.size;
                    tcName.Font.Name = "Arial";
                    tcName.Font.Bold = false;
                    tcName.Text = lstGiftHistory[i].Name;
                    trName.Cells.Add(tcName);
                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        trName.Cells.Add(tcTotal);
                    }

                }
                tblGiftHistorySummary.Rows.Add(trName);

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcIndividualId = new TableCell();
                    tcIndividualId.Width = 800 / qrGiftHistory.size;
                    tcIndividualId.Font.Name = "Arial";
                    tcIndividualId.Font.Bold = false;
                    tcIndividualId.Text = lstGiftHistory[i].Individual_ID__c;
                    trIndividualId.Cells.Add(tcIndividualId);

                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        trIndividualId.Cells.Add(tcTotal);

                    }

                }
                tblGiftHistorySummary.Rows.Add(trIndividualId);

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcRegistrationDate = new TableCell();
                    tcRegistrationDate.Width = 800 / qrGiftHistory.size;
                    tcRegistrationDate.Font.Name = "Arial";
                    tcRegistrationDate.Font.Bold = false;
                    tcRegistrationDate.Text = lstGiftHistory[i].c4g_Membership__r.Registration_Date__c.Value.ToString("MM/dd/yyyy");
                    trRegistrationDate.Cells.Add(tcRegistrationDate);

                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        trRegistrationDate.Cells.Add(tcTotal);
                    }

                }
                tblGiftHistorySummary.Rows.Add(trRegistrationDate);

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcProgram = new TableCell();
                    tcProgram.Width = 800 / qrGiftHistory.size;
                    tcProgram.Font.Name = "Arial";
                    tcProgram.Font.Bold = false;
                    tcProgram.Text = lstGiftHistory[i].c4g_Plan__r.Name;
                    trProgram.Cells.Add(tcProgram);

                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        trProgram.Cells.Add(tcTotal);
                    }

                }
                tblGiftHistorySummary.Rows.Add(trProgram);

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcTotalShare = new TableCell();
                    tcTotalShare.Width = 800 / qrGiftHistory.size;
                    tcTotalShare.Font.Name = "Arial";
                    tcTotalShare.Font.Bold = false;
                    tcTotalShare.Text = lstGiftHistory[i].c4g_Total_Shared_Amount__c.ToString();
                    trTotalShare.Cells.Add(tcTotalShare);

                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        trTotalShare.Cells.Add(tcTotal);
                    }
                }
                tblGiftHistorySummary.Rows.Add(trTotalShare);

                Double? TotalGiftAmount = 0;
                Double? ZeroDollar = 0;
                Boolean bChildAmountAdded = false;

                for (int i = 0; i < qrGiftHistory.size; i++)
                {
                    TableCell tcGiftAmount = new TableCell();
                    tcGiftAmount.Width = 800 / qrGiftHistory.size;
                    tcGiftAmount.Font.Name = "Arial";
                    tcGiftAmount.Font.Bold = false;

                    if (lstGiftHistory[i].cmm_Household_Role__c == "Head of Household" || lstGiftHistory[i].cmm_Household_Role__c == "Spouse")
                        tcGiftAmount.Text = lstGiftHistory[i].c4g_Monthly_Gift_Amount__c.Value.ToString("C", CultureInfo.CurrentCulture);
                    if (lstGiftHistory[i].cmm_Household_Role__c == "Child" && bChildAmountAdded == true)
                        tcGiftAmount.Text = ZeroDollar.Value.ToString("C", CultureInfo.CurrentCulture);
                    if (lstGiftHistory[i].cmm_Household_Role__c == "Child" && bChildAmountAdded == false)
                    {
                        tcGiftAmount.Text = lstGiftHistory[i].c4g_Monthly_Gift_Amount__c.Value.ToString("C", CultureInfo.CurrentCulture);
                        bChildAmountAdded = true;
                    }
                    trGiftAmount.Cells.Add(tcGiftAmount);

                    if (i == qrGiftHistory.size - 1)
                    {
                        TableCell tcTotal = new TableCell();
                        tcTotal.Width = 200;
                        tcTotal.Text = lstGiftHistory[0].c4g_Membership__r.c4g_Calculated_Monthly_Membership_Gift__c.Value.ToString("C", CultureInfo.CurrentCulture);
                        trGiftAmount.Cells.Add(tcTotal);
                    }

                }
                tblGiftHistorySummary.Rows.Add(trGiftAmount);
                pnlGiftHistoryTable.Controls.Add(tblGiftHistorySummary);
            }

        }

        protected int NumberOfFamilyMembers()
        {
            String strQueryForHouseholdMembers = "select Name from Contact where cmm_Household__c = '" + strAccountId + "'";

            SForce.QueryResult qrHouseholdMembers = Sfdcbinding.query(strQueryForHouseholdMembers);

            return qrHouseholdMembers.size;
        }

        protected void HealthHistoryAddNewRow(Table table, String strQuestion, int nRow)
        {
            TableRow tr = new TableRow();

            TableCell tcQuestion = new TableCell();
            TableCell tcAnswer = new TableCell();

            tcQuestion.Text = strQuestion;
            tcQuestion.ColumnSpan = 1;
            tcQuestion.BorderColor = Color.Gray;
            tcQuestion.BorderStyle = BorderStyle.Solid;
            tcQuestion.BorderWidth = 1;
            tcQuestion.Font.Size = FontUnit.Smaller;
            tcQuestion.HorizontalAlign = HorizontalAlign.Left;
            tcQuestion.Width = 200;
            tr.Cells.Add(tcQuestion);

            for (int i = 1; i <= nNumberOfFamilyMembers; i++)
            {
                Button btnYes = new Button();
                btnYes.Text = "Yes";
                btnYes.Font.Name = "Arial";
                btnYes.Font.Bold = true;
                btnYes.Width = 40;
                btnYes.Height = 35;
                btnYes.BackColor = Color.LightGray;
                btnYes.BorderWidth = 0;
                btnYes.ID = "btnYes_" + nRow + "_" + i;
                //btnYes.Click += new EventHandler(BtnYes_Click);

                //HiddenField hdnYes = new HiddenField();
                //hdnYes.ID = "hdnYes_" + nRow + "_" + i;
                //hdnYes.Value = "lightgrey";


                Button btnNo = new Button();
                btnNo.Text = "No";
                btnNo.Font.Name = "Arial";
                btnNo.Font.Bold = true;
                btnNo.Width = 40;
                btnNo.Height = 35;
                btnNo.BackColor = Color.Blue;
                btnNo.ForeColor = Color.White;
                btnNo.BorderWidth = 0;
                btnNo.ID = "btnNo_" + nRow + "_" + i;
                //btnNo.Click += new EventHandler(BtnNo_Click);

                //HiddenField hdnNo = new HiddenField();
                //hdnNo.ID = "hdnNo_" + nRow + "_" + i;
                //hdnNo.Value = "blue";

                btnYes.Attributes.Add("onclick", "return false;");
                btnNo.Attributes.Add("onclick", "return false;");
                //btnYes.Attributes.Add("onclick", "toggleYes(this); return false;");
                //btnNo.Attributes.Add("onclick", "toggleNo(this); return false;");

                tcAnswer = new TableCell();
                tcAnswer.ColumnSpan = 1;
                tcAnswer.BorderWidth = 1;
                tcAnswer.BorderColor = Color.Gray;
                tcAnswer.BorderStyle = BorderStyle.Solid;
                tcAnswer.Width = 600 / nNumberOfFamilyMembers;
                tcAnswer.HorizontalAlign = HorizontalAlign.Center;
                tcAnswer.VerticalAlign = VerticalAlign.Middle;
                tcAnswer.Controls.Add(btnYes);
                //tcAnswer.Controls.Add(hdnYes);
                tcAnswer.Controls.Add(btnNo);
                //tcAnswer.Controls.Add(hdnNo);

                tcAnswer.ID = "tc_" + nRow + "_" + i;
                tr.ID = "tr_" + nRow + "_" + i;

                tr.Cells.Add(tcAnswer);

            }

            table.Rows.Add(tr);
        }

        void SetHiddenTelephoneLabels(String strTelephoneBorderWidth, String strTelephoneBorderColor, String strTelephoneForeColor)
        {
            lblHdnTelephoneBorderWidth.Text += strTelephoneBorderWidth;
            lblHdnTelephoneBorderColor.Text += strTelephoneBorderColor;
            lblHdnTelephoneForeColor.Text += strTelephoneForeColor;
        }

        protected void chkBillingAddress_CheckedChanged(object sender, EventArgs e)
        {

            if (chkBillingAddress.Checked)
            {
                tabContainerRegister.Height = 1200;
                pnlUpdateCancel.CssClass = "GeneralInfoButtonWithBillingAddress";

                lblBillingStreet.Visible = true;
                lblBillingZipCode.Visible = true;
                lblBillingState.Visible = true;
                lblBillingCity.Visible = true;

                txtBillingStreet.Visible = true;
                txtBillingZipCode.Visible = true;
                txtBillingState.Visible = true;
                txtBillingCity.Visible = true;
                btnBillingAddressSave.Visible = true;

                pnlBillingStreet.Visible = true;
                pnlBillingZipCode.Visible = true;
                pnlBillingState.Visible = true;
                pnlBillingCity.Visible = true;
                pnlBillingSaveButton.Visible = true;
                pnlBillingAddress.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
            }
            else if (!chkBillingAddress.Checked)
            {
                tabContainerRegister.Height = 1000;
                pnlUpdateCancel.CssClass = "GeneralInfoButtonPanel";

                if (txtBillingStreet.Text != String.Empty &&
                    txtBillingZipCode.Text != String.Empty &&
                    txtBillingState.Text != String.Empty &&
                    txtBillingCity.Text != String.Empty)
                {

                    String strHousehold_Id = (String)Session["ContactId"];

                    SForce.Contact ctDeleteBillingAddress = new SForce.Contact();

                    ctDeleteBillingAddress.Id = strHousehold_Id;
                    String[] strNoBillingAddress = { "OtherStreet", "OtherPostalCode", "OtherState", "OtherCity" };
                    ctDeleteBillingAddress.fieldsToNull = strNoBillingAddress;

                    //ctDeleteBillingAddress.OtherStreet = " ";
                    //ctDeleteBillingAddress.OtherPostalCode = " ";
                    //ctDeleteBillingAddress.OtherState = " ";
                    //ctDeleteBillingAddress.OtherCity = " ";

                    InitializedSfdcbinding();

                    SForce.SaveResult[] srDelete = Sfdcbinding.update(new SForce.sObject[] { ctDeleteBillingAddress });

                    if (srDelete[0].success)
                    {

                        txtBillingStreet.Text = "";
                        txtBillingZipCode.Text = "";
                        txtBillingState.Text = "";
                        txtBillingCity.Text = "";

                        lblBillingStreet.Visible = false;
                        lblBillingZipCode.Visible = false;
                        lblBillingState.Visible = false;
                        lblBillingCity.Visible = false;

                        txtBillingStreet.Visible = false;
                        txtBillingZipCode.Visible = false;
                        txtBillingState.Visible = false;
                        txtBillingCity.Visible = false;
                        btnBillingAddressSave.Visible = false;

                        pnlBillingStreet.Visible = false;
                        pnlBillingZipCode.Visible = false;
                        pnlBillingState.Visible = false;
                        pnlBillingCity.Visible = false;
                        pnlBillingSaveButton.Visible = false;
                        pnlBillingAddress.Visible = false;

                        chkBillingAddress.Checked = false;

                        mpeBillingAddressDeletionConfirmation.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
                    }
                    else
                    {
                        mpeBillingAddressDeletionFailed.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);
                    }
                }
                else
                {
                    lblBillingStreet.Visible = false;
                    lblBillingZipCode.Visible = false;
                    lblBillingState.Visible = false;
                    lblBillingCity.Visible = false;

                    txtBillingStreet.Text = String.Empty;
                    txtBillingZipCode.Text = String.Empty;
                    txtBillingState.Text = String.Empty;
                    txtBillingCity.Text = String.Empty;

                    txtBillingStreet.Visible = false;
                    txtBillingZipCode.Visible = false;
                    txtBillingState.Visible = false;
                    txtBillingCity.Visible = false;
                    btnBillingAddressSave.Visible = false;

                    pnlBillingStreet.Visible = false;
                    pnlBillingZipCode.Visible = false;
                    pnlBillingState.Visible = false;
                    pnlBillingCity.Visible = false;
                    pnlBillingSaveButton.Visible = false;
                    pnlBillingAddress.Visible = false;

                    chkBillingAddress.Checked = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus(); }}", chkBillingAddress.ClientID), true);

                }
            }
            RestoreAllTextBoxStyle();
        }

        protected void InitializeTextBoxStyle(TextBox txtObject, HiddenField hdnBorderWidth, HiddenField hdnBorderColor, HiddenField hdnFontColor)
        {
            hdnBorderWidth.Value = "1";
            hdnBorderColor.Value = "black";
            hdnFontColor.Value = "black";

            txtObject.BorderWidth = 1;
            txtObject.BorderColor = Color.Black;
            txtObject.ForeColor = Color.Black;
        }

        protected void btnBillingAddressSave_Click(object sender, EventArgs e)
        {

            String strContact_Id = (String)Session["ContactId"];

            SForce.Contact ctUpdate = new SForce.Contact();

            ctUpdate.Id = strContact_Id;
            ctUpdate.OtherStreet = txtBillingStreet.Text;
            ctUpdate.OtherPostalCode = txtBillingZipCode.Text;
            ctUpdate.OtherState = txtBillingState.Text;
            ctUpdate.OtherCity = txtBillingCity.Text;

            InitializedSfdcbinding();

            SForce.SaveResult[] saveResults = Sfdcbinding.update(new SForce.sObject[] { ctUpdate });

            if (saveResults[0].success)
            {
                btnBillingAddressSave.Text = "Update";
                mpeBillingAddressSaveConfirmation.Show();

            }
            else
            {
                mpeBillingAddressSaveFailure.Show();
            }
        }

        protected void txtBillingZipCode_TextChanged(object sender, EventArgs e)
        {
            if (txtBillingZipCode.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtBillingZipCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtBillingState.Text = dr["state_code"].ToString();
                    txtBillingCity.Text = dr["name"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", btnBillingAddressSave.ClientID), true);
                    InitializeTextBoxStyle(txtBillingState, hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);
                    InitializeTextBoxStyle(txtBillingCity, hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);
                }
                else
                {
                    txtBillingState.Text = "";
                    txtBillingCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void btnBillingZipCodeHidden_Click(object sender, EventArgs e)
        {
            if (rfvBillingZipCode.IsValid && revBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Black;
                txtBillingZipCode.ForeColor = Color.Black;
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
            }
            if (!rfvBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Red;
                txtBillingZipCode.ForeColor = Color.Red;
                txtBillingZipCode.Text = "Zip code required!";
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);
            }
            if (!revBillingZipCode.IsValid)
            {
                txtBillingZipCode.BorderWidth = 1;
                txtBillingZipCode.BorderColor = Color.Red;
                txtBillingZipCode.ForeColor = Color.Red;
                txtBillingZipCode.Text = "Invalid zip code!";
                txtBillingZipCode.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtBillingState.ClientID), true);

            }
        }

        protected void txtZipCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txtZip = (TextBox)sender;

            if (txtZip.Text.Length == 5)
            {
                //MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtZip.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtState.Text = dr["state_code"].ToString();
                    txtCity.Text = dr["name"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchName.ClientID), true);
                    InitializeTextBoxStyle(txtState, hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);
                    InitializeTextBoxStyle(txtCity, hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);
                }
                else
                {
                    txtState.Text = "";
                    txtCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void btnZipCodeHidden_Click(object sender, EventArgs e)
        {

            if (rfvZipCode.IsValid && revZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Black;
                txtZipCode.ForeColor = Color.Black;
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);

            }
            if (!rfvZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Red;
                txtZipCode.ForeColor = Color.Red;
                txtZipCode.Text = "Zip code required!";
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
            }
            if (!revZipCode.IsValid)
            {
                txtZipCode.BorderWidth = 1;
                txtZipCode.BorderColor = Color.Red;
                txtZipCode.ForeColor = Color.Red;
                txtZipCode.Text = "Invalid zip code!";
                txtZipCode.Height = 25;
                //txtZipCode.CssClass = "TextBox";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtState.ClientID), true);
            }
        }

        protected void txtChurchZipCode_TextChanged(object sender, EventArgs e)
        {
            TextBox txtZip = (TextBox)sender;

            if (txtZip.Text.Length == 5)
            {
                MySqlDataReader dr = null;
                String strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";
                MySqlConnection conn = new MySqlConnection(strConnectionString);
                String strQueryForStateCity = "select state_code, name from city where zip = '" + txtZip.Text + "'";
                MySqlCommand cmd = new MySqlCommand(strQueryForStateCity, conn);

                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtChurchState.Text = dr["state_code"].ToString();
                    txtChurchCity.Text = dr["name"].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchTelephone.ClientID), true);
                    InitializeTextBoxStyle(txtChurchState, hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);
                    InitializeTextBoxStyle(txtChurchCity, hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);
                }
                else
                {
                    txtChurchState.Text = "";
                    txtChurchCity.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
                }
                conn.Close();
            }
            RestoreAllTextBoxStyle();
        }

        protected void btnChurchZipCodeHidden_Click(object sender, EventArgs e)
        {
            if (rfvChurchZip.IsValid && revChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Black;
                txtChurchZip.BorderColor = Color.Black;
                txtChurchZip.Height = 25;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
            }
            if (!rfvChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Red;
                txtChurchZip.BorderColor = Color.Red;
                txtChurchZip.Height = 25;

                txtChurchZip.Text = "Zip code required!";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);
            }
            if (!revChurchZip.IsValid)
            {
                txtChurchZip.BorderWidth = 1;
                txtChurchZip.ForeColor = Color.Red;
                txtChurchZip.BorderColor = Color.Red;
                txtChurchZip.Height = 25;

                txtChurchZip.Text = "Invalid zip code!";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", String.Format("function pageLoad() {{ document.getElementById('{0}').focus();}}", txtChurchState.ClientID), true);

            }
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    // change this code to client side code
        //    //LoadPersonalInfo();
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            //lblHdnTelephoneBorderWidth.Text = hdnTelephoneBorderWidth.Value;
            //lblHdnTelephoneBorderColor.Text = hdnTelephoneBorderColor.Value;
            //lblHdnTelephoneForeColor.Text = hdnTelephoneFontColor.Value;

            RestoreAllTextBoxStyle();

            if (Page.IsValid &&
                txtEmail.Text != "Email address required!" && txtEmail.Text != "Email address invalid!" && txtEmail.BorderColor != Color.Red && txtEmail.ForeColor != Color.Red &&
                txtLastName.Text != "Last name required!" && txtLastName.BorderColor != Color.Red && txtLastName.ForeColor != Color.Red &&
                txtFirstName.Text != "First name required!" && txtFirstName.BorderColor != Color.Red && txtFirstName.ForeColor != Color.Red &&
                txtTelephone1.Text != "Phone number required!" && txtTelephone1.Text != "Invalid phone number!" && txtTelephone1.BorderColor != Color.Red && txtTelephone1.ForeColor != Color.Red &&
                txtTelephone2.Text != "Invalid phone number!" && txtTelephone2.BorderColor != Color.Red && txtTelephone2.ForeColor != Color.Red &&
                txtTelephone3.Text != "Invalid phone number!" && txtTelephone3.BorderColor != Color.Red && txtTelephone3.ForeColor != Color.Red &&
                txtAddress.Text != "Your address required!" && txtAddress.BorderColor != Color.Red && txtAddress.ForeColor != Color.Red &&
                txtZipCode.Text != "Zip code required!" && txtZipCode.Text != "Invalid zip code!" && txtZipCode.BorderColor != Color.Red && txtZipCode.ForeColor != Color.Red &&
                txtState.Text != "State required!" && txtState.BorderColor != Color.Red && txtState.ForeColor != Color.Red &&
                txtCity.Text != "City name required!" && txtCity.BorderColor != Color.Red && txtCity.ForeColor != Color.Red &&
                txtChurchName.Text != "Name of church required!" && txtChurchName.BorderColor != Color.Red && txtChurchName.ForeColor != Color.Red &&
                txtSeniorPastor.Text != "Pastor name required!" && txtSeniorPastor.BorderColor != Color.Red && txtSeniorPastor.ForeColor != Color.Red &&
                txtChurchStreet.Text != "Church address required!" && txtChurchStreet.BorderColor != Color.Red && txtChurchStreet.ForeColor != Color.Red &&
                txtChurchZip.Text != "Zip code required!" && txtChurchZip.Text != "Invalid zip code!" && txtChurchZip.BorderColor != Color.Red && txtChurchZip.ForeColor != Color.Red &&
                txtChurchState.Text != "State required!" && txtChurchState.BorderColor != Color.Red && txtChurchState.ForeColor != Color.Red &&
                txtChurchCity.Text != "Name of city required!" && txtChurchCity.BorderColor != Color.Red && txtChurchCity.ForeColor != Color.Red &&
                txtChurchTelephone.Text != "Invalid phone number!" && txtChurchTelephone.BorderColor != Color.Red && txtChurchTelephone.ForeColor != Color.Red)
            {

                String strContactId = (String)Session["ContactId"];
                StringBuilder sbErrorMessage = new StringBuilder();

                SForce.Contact ctPersonalInfo = new SForce.Contact();

                ctPersonalInfo.Id = strContactId;

                if (txtEmail.Text != hdnEmail.Value.ToString())
                {
                    //lblPersonalInfoUpdateMessage.Text = "Inside email account update";
                    //lblEmailUpdateMessage.Text = "Inside email update";
                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                    SqlCommand cmd = new SqlCommand();
                    string strSQLSetEmail = "Update AspNetUsers Set Email = @Email, UserName = @UserName where Email = @EmailAddr";
                    cmd.CommandText = strSQLSetEmail;
                    cmd.Connection = cnn;

                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters["@Email"].Value = txtEmail.Text;
                    cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
                    cmd.Parameters["@UserName"].Value = txtEmail.Text;
                    cmd.Parameters.Add("@EmailAddr", SqlDbType.NVarChar);
                    cmd.Parameters["@EmailAddr"].Value = hdnEmail.Value.ToString();

                    try
                    {
                        cnn.Open();
                        int nRowsAffected = cmd.ExecuteNonQuery();
                        cnn.Close();
                        //lblEmailUpdateMessage.Text = nRowsAffected.ToString();
                    }
                    catch (Exception ex)
                    {
                        //lblPersonalInfoUpdateMessage.Text = ex.Message;
                        //lblEmailUpdateMessage.Text = ex.Message;
                    }
                }

                ctPersonalInfo.Email = txtEmail.Text;
                ctPersonalInfo.LastName = txtLastName.Text;
                ctPersonalInfo.FirstName = txtFirstName.Text;
                ctPersonalInfo.MiddleName = txtMiddleName.Text;

                ctPersonalInfo.Phone = txtTelephone1.Text;
                ctPersonalInfo.MobilePhone = txtTelephone2.Text;
                ctPersonalInfo.OtherPhone = txtTelephone3.Text;

                ctPersonalInfo.MailingStreet = txtAddress.Text;
                ctPersonalInfo.MailingCity = txtCity.Text;
                ctPersonalInfo.MailingState = txtState.Text;
                ctPersonalInfo.MailingPostalCode = txtZipCode.Text;

                InitializedSfdcbinding();

                SForce.SaveResult[] srPersonalInfo = Sfdcbinding.update(new SForce.sObject[] { ctPersonalInfo });

                if (srPersonalInfo == null)
                {
                    sbErrorMessage.Append("Personal Information udpate failed<br />");
                }
                else
                {
                    if (srPersonalInfo[0].success)
                    {
                        //lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                    }
                    else
                    {
                        //lblPersonalInfoUpdateMessage.Text = srPersonalInfo[0].errors[0].message;
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////

                //InitializedSfdcbinding();

                //string strQueryForChurchId = "select c4g_Church__r.Id from Contact where Email = 'seanjones@emailz.com'";
                string strQueryForChurchId = "select c4g_Church__r.Id from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrChurchId = Sfdcbinding.query(strQueryForChurchId);
                String strChurchId = String.Empty;

                SForce.SaveResult[] srChurchInfo = null;

                if (qrChurchId.size > 0)
                {
                    SForce.Contact ctChurchId = (SForce.Contact)qrChurchId.records[0];
                    if (ctChurchId.c4g_Church__r != null)
                    {
                        //strChurchId = ctChurchId.c4g_Church__r.Id;

                        SForce.Account acctChurchInfo = new SForce.Account();



                        //acctChurchInfo.Id = strChurchId;
                        acctChurchInfo.Id = ctChurchId.c4g_Church__r.Id;
                        acctChurchInfo.Name = txtChurchName.Text;
                        acctChurchInfo.Senior_Pastor_s_Name__c = txtSeniorPastor.Text;

                        acctChurchInfo.ShippingStreet = txtChurchStreet.Text;
                        acctChurchInfo.ShippingPostalCode = txtChurchZip.Text;
                        acctChurchInfo.ShippingState = txtChurchState.Text;
                        acctChurchInfo.ShippingCity = txtChurchCity.Text;
                        acctChurchInfo.Phone = txtChurchTelephone.Text;

                        srChurchInfo = Sfdcbinding.update(new SForce.sObject[] { acctChurchInfo });

                        if (srChurchInfo[0].success)
                        {
                            //lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                        }
                        else
                        {
                            //lblPersonalInfoUpdateMessage.Text = srChurchInfo[0].errors[0].message;
                        }
                    }
                }
                else sbErrorMessage.Append("Church information update failed<br />");


                // update payment information; payment method and payment frequency
                // payment information is in membership object
                ///////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryPaymentId = "select Id from causeview__Payment__c " +
                //                           "where causeview__Donation__r.causeview__Constituent__r.Email = 'seanjones@emailz.com'";

                //string strQueryPaymentId = "select Id from causeview__Payment__c " +
                //                           "where causeview__Donation__r.causeview__Constituent__r.Id = '" + strContactId + "'";

                string strQueryMembershipId = "select c4g_Membership__r.Id from Contact " +
                                              "where Id = '" + strContactId + "'";

                SForce.QueryResult qrMembershipInfo = Sfdcbinding.query(strQueryMembershipId);
                //SForce.causeview__Payment__c paymentId = null;
                //string strPaymentId = String.Empty;

                SForce.SaveResult[] srMembershipInfo = null;
                string strMembershipId = String.Empty;

                //if (qrPaymentInfoId.size > 0)
                if (qrMembershipInfo.size > 0)
                {
                    //paymentId = (SForce.causeview__Payment__c)qrPaymentInfoId.records[0];
                    //strPaymentId = paymentId.Id;
                    //SForce.Membership__c membershipInfo = (SForce.Membership__c)qrMembershipInfo.records[0];
                    SForce.Contact contact = (SForce.Contact)qrMembershipInfo.records[0];

                    //strMembershipId = membershipInfo.Id;

                    SForce.Membership__c updateMembershipInfo = new SForce.Membership__c();

                    updateMembershipInfo.Id = contact.c4g_Membership__r.Id;
                    if (rbCheck.Checked) updateMembershipInfo.cmm_Payment_Method__c = "Check";
                    if (rbBankACH.Checked) updateMembershipInfo.cmm_Payment_Method__c = "ACH/PAD";
                    if (rbCreditCard.Checked) updateMembershipInfo.cmm_Payment_Method__c = "Credit Card";

                    if (rbRecurring.Checked) updateMembershipInfo.cmm_Payment_Frequency__c = "Recurring";
                    if (rbOneTime.Checked) updateMembershipInfo.cmm_Payment_Frequency__c = "One Time Gift";

                    if (chkEmail.Checked && chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Both";
                    if (chkEmail.Checked && !chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Email";
                    if (!chkEmail.Checked && chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Postal";
                    if (!chkEmail.Checked && !chkPostal.Checked) updateMembershipInfo.Invoice_Delivery__c = "Neither";

                    if (ddlReferredBy.SelectedValue == "Member Referral")
                    {
                        if (txtReferredByMembership.Text != "" && ddlReferredByContact.Items.Count != 0 )
                        {
                            String strQueryContactIdForMembership = "select Id from Contact where c4g_Membership__r.Referrer__r.c4g_Membership__r.Name = '" + txtReferredByMembership.Text + "' " +
                                                                    "and c4g_Membership__r.Referrer__r.Name = '" + ddlReferredByContact.SelectedValue + "'";

                            SForce.QueryResult qrContactIdForMembership = Sfdcbinding.query(strQueryContactIdForMembership);

                            if (qrContactIdForMembership.size > 0)
                            {
                                SForce.Contact ctContactIdForMembership = qrContactIdForMembership.records[0] as SForce.Contact;
                                updateMembershipInfo.Referred_By__c = ddlReferredBy.SelectedValue;
                                updateMembershipInfo.Referrer__c = ctContactIdForMembership.Id;
                            }
                        }
                        else
                        {
                            // error: user has to specify the membership number and the referred contact name
                        }
                    }
                    else
                    {
                        updateMembershipInfo.Referred_By__c = ddlReferredBy.SelectedValue;
                        // specify the updateMembershipInfo.Referred__c should be null
                    }

                    srMembershipInfo = Sfdcbinding.update(new SForce.sObject[] { updateMembershipInfo });
                    if (srMembershipInfo[0].success)
                    {
                        //lblPersonalInfoUpdateMessage.Text = "Update succeeded";
                    }
                    else
                    {
                        //lblPersonalInfoUpdateMessage.Text = srMembershipInfo[0].errors[0].message;
                    }


                    //SForce.causeview__Payment__c updatePaymentInfo = new SForce.causeview__Payment__c();

                    //updatePaymentInfo.Id = strPaymentId;
                    //updatePaymentInfo.Id = paymentId.Id;
                    //if (rbCheck.Checked) updatePaymentInfo.causeview__Payment_Type__c = "Check";
                    //if (rbBankACH.Checked) updatePaymentInfo.causeview__Payment_Type__c = "ACH/PAD";
                    //if (rbCreditCard.Checked) updatePaymentInfo.causeview__Payment_Type__c = "Credit Card";

                    //srPaymentInfo = Sfdcbinding.update(new SForce.sObject[] { updatePaymentInfo });
                    //if (srPaymentInfo[0].success)
                    //{
                    //    lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                    //}
                    //else
                    //{
                    //    lblPersonalInfoUpdateMessage.Text = srPaymentInfo[0].errors[0].message;
                    //}
                }
                //else sbErrorMessage.Append("No payment information to update<br />");


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForTransactionId = "select Id from causeview__Gift__c where causeview__Constituent__r.Email = 'seanjones@emailz.com'";
                //string strQueryForTransactionId = "select Id from causeview__Gift__c where causeview__Constituent__r.Id = '" + strContactId + "'";

                //SForce.QueryResult qrGiftFrequency = Sfdcbinding.query(strQueryForTransactionId);
                //SForce.causeview__Gift__c frequencyId = null;
                //string strFrequencyId = String.Empty;
                //SForce.SaveResult[] srFrequencyInfo = null;

                //if (qrGiftFrequency.size > 0)
                //{
                //    frequencyId = (SForce.causeview__Gift__c)qrGiftFrequency.records[0];
                //    //strFrequencyId = frequencyId.Id;

                //    SForce.causeview__Gift__c updateFrequencyInfo = new SForce.causeview__Gift__c();
                //    //updateFrequencyInfo.Id = strFrequencyId;
                //    updateFrequencyInfo.Id = frequencyId.Id;
                //    if (rbRecurring.Checked) updateFrequencyInfo.causeview__Gift_Type__c = "Recurring";
                //    if (rbOneTime.Checked) updateFrequencyInfo.causeview__Gift_Type__c = "One Time Gift";

                //    srFrequencyInfo = Sfdcbinding.update(new SForce.sObject[] { updateFrequencyInfo });
                //    if (srFrequencyInfo[0].success)
                //    {
                //        lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                //    }
                //    else
                //    {
                //        lblPersonalInfoUpdateMessage.Text = srFrequencyInfo[0].errors[0].message;
                //    }
                //}
                //else sbErrorMessage.Append("No gift frequency information to update<br />");

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForInvoiceDelivery = "select Id from Membership__c where Paying_Member__r.Email = 'seanjones@emailz.com'";
                //string strQueryForInvoiceDelivery = "select Id from Membership__c where Paying_Member__r.Id = '" + strContactId + "'";

                //SForce.QueryResult qrInvoiceDelivery = Sfdcbinding.query(strQueryForInvoiceDelivery);
                //SForce.Membership__c memId = null;
                //SForce.SaveResult[] srInvoiceDelivery = null;

                //if (qrInvoiceDelivery.size > 0)
                //{
                //    memId = (SForce.Membership__c)qrInvoiceDelivery.records[0];
                //    SForce.Membership__c memInvoiceDelivery = new SForce.Membership__c();

                //    memInvoiceDelivery.Id = memId.Id;
                //    if (chkEmail.Checked && chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Both";
                //    if (chkEmail.Checked && !chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Email";
                //    if (!chkEmail.Checked && chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Postal";
                //    if (!chkEmail.Checked && !chkPostal.Checked) memInvoiceDelivery.Invoice_Delivery__c = "Neither";

                //    srInvoiceDelivery = Sfdcbinding.update(new SForce.sObject[] { memInvoiceDelivery });

                //    if (srInvoiceDelivery[0].success)
                //    {
                //        lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                //    }
                //    else
                //    {
                //        lblPersonalInfoUpdateMessage.Text = srInvoiceDelivery[0].errors[0].message;
                //    }
                //}
                //else sbErrorMessage.Append("Invoice delivery information update failed<br />");


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //string strQueryForSolicitCode = "select id, causeview__Solicit_Codes__c from Contact where Email = 'seanjones@emailz.com'";
                string strQueryForSolicitCode = "select id, cmm_Solicit_Codes__c from Contact where Id = '" + strContactId + "'";

                SForce.QueryResult qrSolicitCode = Sfdcbinding.query(strQueryForSolicitCode);
                SForce.Contact ctContactSolicitCode = null;
                SForce.SaveResult[] srSolicitCode = null;

                if (qrSolicitCode.size > 0)
                {
                    ctContactSolicitCode = (SForce.Contact)qrSolicitCode.records[0];

                    SForce.Contact ctSolicitCode = new SForce.Contact();

                    ctSolicitCode.Id = ctContactSolicitCode.Id;
                    ctSolicitCode.cmm_Solicit_Codes__c = ctContactSolicitCode.cmm_Solicit_Codes__c;
                    //lblPaymentInfoUpdateMessage.Text = ctContactSolicitCode.causeview__Solicit_Codes__c;

                    if (ctSolicitCode.cmm_Solicit_Codes__c != null)
                    //if (ctContactSolicitCode.cmm_Solicit_Codes__c != null)
                    {
                        if (rbYesJoinMailing.Checked && !rbNoJoinMailing.Checked)
                        {
                            if (!ctSolicitCode.cmm_Solicit_Codes__c.Contains("Allow Postal Mail"))
                                ctSolicitCode.cmm_Solicit_Codes__c = String.Concat(ctSolicitCode.cmm_Solicit_Codes__c, "; Allow Postal Mail");
                        }
                        if (!rbYesJoinMailing.Checked && rbNoJoinMailing.Checked)
                        {
                            if (ctSolicitCode.cmm_Solicit_Codes__c.Contains("Allow Postal Mail"))
                                ctSolicitCode.cmm_Solicit_Codes__c = ctSolicitCode.cmm_Solicit_Codes__c.Replace("Allow Postal Mail", "");
                        }
                        if (rbYesAllowMessages.Checked && !rbNoAllowMessages.Checked)
                        {
                            if (!ctSolicitCode.cmm_Solicit_Codes__c.Contains("Allow SMS Messages"))
                                ctSolicitCode.cmm_Solicit_Codes__c = String.Concat(ctSolicitCode.cmm_Solicit_Codes__c, "; Allow SMS Messages");
                        }
                        if (!rbYesAllowMessages.Checked && rbNoAllowMessages.Checked)
                        {
                            if (ctSolicitCode.cmm_Solicit_Codes__c.Contains("Allow SMS Messages"))
                                ctSolicitCode.cmm_Solicit_Codes__c = ctSolicitCode.cmm_Solicit_Codes__c.Replace("Allow SMS Messages", "");
                        }

                        srSolicitCode = Sfdcbinding.update(new SForce.sObject[] { ctSolicitCode });

                        if (srSolicitCode[0].success)
                        {
                            //lblPersonalInfoUpdateMessage.Text = "Update succeeded!";
                        }
                        else
                        {
                            //lblPersonalInfoUpdateMessage.Text = srSolicitCode[0].errors[0].message;
                        }
                    }
                }
                else sbErrorMessage.Append("Solicitcode update failed");

                // Begin at this line for updating Referred by section



                //////////////////////////////////////////////////////////////////////////////////////////////////////
                // Show whether or not the update is successful
                if (srPersonalInfo[0].success &&
                    srChurchInfo[0].success &&
                    srMembershipInfo[0].success &
                    srSolicitCode[0].success)
                {
                    mpeSucceeded.Show();
                    SetHiddenTextFields();
                    InitializedHiddenFields();
                }
                else
                {
                    mpeFailed.Show();
                }

                //StringBuilder sbUpdateErrorMessage = new StringBuilder();

                //if (!srPersonalInfo[0].success) sbUpdateErrorMessage.Append(srPersonalInfo[0].errors[0].message + "<br />");
                //if (!srChurchInfo[0].success) sbUpdateErrorMessage.Append(srChurchInfo[0].errors[0].message + "<br />");
                //if (!srMembershipInfo[0].success) sbUpdateErrorMessage.Append(srMembershipInfo[0].errors[0].message + "<br />");
                //if (!srSolicitCode[0].success) sbUpdateErrorMessage.Append(srSolicitCode[0].errors[0].message + "<br />");

                //lblPersonalInfoUpdateMessage.Text = sbUpdateErrorMessage.ToString();





                //if (srPersonalInfo != null &&
                //    srChurchInfo != null &&
                //    srPaymentInfo != null &&
                //    srFrequencyInfo != null &&
                //    srInvoiceDelivery != null &&
                //    srSolicitCode != null)
                //{
                //    if (srPersonalInfo[0].success &&
                //        srChurchInfo[0].success &&
                //        srPaymentInfo[0].success &&
                //        srFrequencyInfo[0].success &&
                //        srInvoiceDelivery[0].success &&
                //        srSolicitCode[0].success)
                //    {
                //        mpeSucceeded.Show();
                //    }
                //    else
                //    {
                //        mpeFailed.Show();
                //    }
                //}
                //else
                //{
                //    lblPartialUpdateFailureMessage.Text = sbErrorMessage.ToString();
                //    mpePartialUpdateFailure.Show();
                //}
            }
            else if (!Page.IsValid)
            {
                StringBuilder sbControlsFailedValidation = new StringBuilder();

                if (!rfvMemberEmail.IsValid) sbControlsFailedValidation.Append("Member email is required.<br />");
                if (!revMemberEmail.IsValid && (txtEmail.Text != rfvMemberEmail.InitialValue)) sbControlsFailedValidation.Append("Member email is invalid.<br />");

                if (!rfvLastName.IsValid) sbControlsFailedValidation.Append("Member last name is required.<br />");
                if (!rfvFirstName.IsValid) sbControlsFailedValidation.Append("Member first name is required.<br />");

                if (!rfvPhone.IsValid) sbControlsFailedValidation.Append("Phone number is required.<br />");
                if (!revPhone.IsValid && (txtTelephone1.Text != rfvPhone.InitialValue)) sbControlsFailedValidation.Append("Phone number is invalid.<br />");

                if (!revMobilePhone.IsValid) sbControlsFailedValidation.Append("Mobile phone number is invalid.<br />");
                if (!revOtherPhone.IsValid) sbControlsFailedValidation.Append("Other phone number is invalid.<br />");

                if (!rfvStreetAddress.IsValid) sbControlsFailedValidation.Append("Street address is required.<br />");
                if (!rfvZipCode.IsValid) sbControlsFailedValidation.Append("Zip code is required.<br />");
                if (!revZipCode.IsValid && (txtZipCode.Text != rfvZipCode.InitialValue)) sbControlsFailedValidation.Append("Zip code is invalid.<br />");
                if (!rfvState.IsValid) sbControlsFailedValidation.Append("State is required.<br />");
                if (!rfvCity.IsValid) sbControlsFailedValidation.Append("City is required.<br />");

                if (!rfvChurchName.IsValid) sbControlsFailedValidation.Append("Church name is required.<br />");
                if (!rfvSeniorPastor.IsValid) sbControlsFailedValidation.Append("Pastor name is required.<br />");

                if (!rfvChurchStreet.IsValid) sbControlsFailedValidation.Append("Church address is required.<br />");
                if (!rfvChurchZip.IsValid) sbControlsFailedValidation.Append("Church zip code is required.<br />");
                if (!revChurchZip.IsValid && (txtChurchZip.Text != rfvChurchZip.InitialValue)) sbControlsFailedValidation.Append("Church zip code is invalid.<br />");
                if (!rfvChurchState.IsValid) sbControlsFailedValidation.Append("Church state is required.<br />");
                if (!rfvChurchCity.IsValid) sbControlsFailedValidation.Append("Church city is required.<br />");
                if (!rfvChurchPhone.IsValid) sbControlsFailedValidation.Append("Church phone number is required.<br />");

                if (!rfvBillingStreet.IsValid) sbControlsFailedValidation.Append("Billing address is required.<br />");
                if (!rfvBillingZipCode.IsValid) sbControlsFailedValidation.Append("Billing zip code is required.<br />");
                if (!revBillingZipCode.IsValid && (txtBillingZipCode.Text != rfvBillingZipCode.InitialValue)) sbControlsFailedValidation.Append("Billing zip code is invalid.<br />");
                if (!rfvBillingState.IsValid) sbControlsFailedValidation.Append("Billing state is required.<br />");
                if (!rfvBillingCity.IsValid) sbControlsFailedValidation.Append("Billing city is required.<br />");

                lblPageValidationFailure.Text = sbControlsFailedValidation.ToString();

                mpePageValidationFailed.Show();

                RestoreAllTextBoxStyle();
            }
        }

        protected void RestoreAllTextBoxStyle()
        {
            // Restore text box style after postback of Zip code lookup
            RestoreTextBoxStyle(txtEmail, hdnEmailBorderWidth, hdnEmailBorderColor, hdnEmailFontColor);
            RestoreTextBoxStyle(txtLastName, hdnLastNameBorderWidth, hdnLastNameBorderColor, hdnLastNameFontColor);
            RestoreTextBoxStyle(txtFirstName, hdnFirstNameBorderWidth, hdnFirstNameBorderColor, hdnFirstNameFontColor);
            RestoreTextBoxStyle(txtTelephone1, hdnTelephoneBorderWidth, hdnTelephoneBorderColor, hdnTelephoneFontColor);
            RestoreTextBoxStyle(txtTelephone2, hdnMobilePhoneBorderWidth, hdnMobilePhoneBorderColor, hdnMobilePhoneFontColor);
            RestoreTextBoxStyle(txtTelephone3, hdnOtherPhoneBorderWidth, hdnOtherPhoneBorderColor, hdnOtherPhoneFontColor);
            RestoreTextBoxStyle(txtAddress, hdnAddressBorderWidth, hdnAddressBorderColor, hdnAddressFontColor);
            RestoreTextBoxStyle(txtZipCode, hdnZipCodeBorderWidth, hdnZipCodeBorderColor, hdnZipCodeFontColor);
            RestoreTextBoxStyle(txtState, hdnStateBorderWidth, hdnStateBorderColor, hdnStateFontColor);
            RestoreTextBoxStyle(txtCity, hdnCityBorderWidth, hdnCityBorderColor, hdnCityFontColor);
            RestoreTextBoxStyle(txtChurchName, hdnChurchNameBorderWidth, hdnChurchNameBorderColor, hdnChurchNameFontColor);
            RestoreTextBoxStyle(txtSeniorPastor, hdnPastorBorderWidth, hdnPastorBorderColor, hdnPastorFontColor);
            RestoreTextBoxStyle(txtChurchStreet, hdnChurchStreetBorderWidth, hdnChurchStreetBorderColor, hdnChurchStreetFontColor);
            RestoreTextBoxStyle(txtChurchZip, hdnChurchZipBorderWidth, hdnChurchZipBorderColor, hdnChurchZipFontColor);
            RestoreTextBoxStyle(txtChurchState, hdnChurchStateBorderWidth, hdnChurchStateBorderColor, hdnChurchStateFontColor);
            RestoreTextBoxStyle(txtChurchCity, hdnChurchCityBorderWidth, hdnChurchCityBorderColor, hdnChurchCityFontColor);
            RestoreTextBoxStyle(txtChurchTelephone, hdnChurchTelephoneBorderWidth, hdnChurchTelephoneBorderColor, hdnChurchTelephoneFontColor);
            RestoreTextBoxStyle(txtBillingStreet, hdnBillingStreetBorderWidth, hdnBillingStreetBorderColor, hdnBillingStreetFontColor);
            RestoreTextBoxStyle(txtBillingZipCode, hdnBillingZipCodeBorderWidth, hdnBillingZipCodeBorderColor, hdnBillingZipCodeFontColor);
            RestoreTextBoxStyle(txtBillingState, hdnBillingStateBorderWidth, hdnBillingStateBorderColor, hdnBillingStateFontColor);
            RestoreTextBoxStyle(txtBillingCity, hdnBillingCityBorderWidth, hdnBillingCityBorderColor, hdnBillingCityFontColor);
        }

        protected void RestoreTextBoxStyle(TextBox txtObject, HiddenField borderWidth, HiddenField borderColor, HiddenField fontColor)
        {
            txtObject.BorderWidth = Int32.Parse(borderWidth.Value);

            switch (borderColor.Value)
            {
                case "red":
                    txtObject.BorderColor = Color.Red;
                    break;
                case "black":
                    txtObject.BorderColor = Color.Black;
                    break;
            }

            switch (fontColor.Value)
            {
                case "red":
                    txtObject.ForeColor = Color.Red;
                    break;
                case "black":
                    txtObject.ForeColor = Color.Black;
                    break;
            }
        }

        protected void SetHiddenTextFields()
        {
            hdnEmail.Value = txtEmail.Text;
            hdnLastName.Value = txtLastName.Text;
            hdnFirstName.Value = txtFirstName.Text;
            hdnMiddleName.Value = txtMiddleName.Text;
            hdnTelephone.Value = txtTelephone1.Text;
            hdnMobilePhone.Value = txtTelephone2.Text;
            hdnOtherPhone.Value = txtTelephone3.Text;
            hdnAddress.Value = txtAddress.Text;
            hdnZipCode.Value = txtZipCode.Text;
            hdnState.Value = txtState.Text;
            hdnCity.Value = txtCity.Text;
            hdnChurchName.Value = txtChurchName.Text;
            hdnSeniorPastor.Value = txtSeniorPastor.Text;
            hdnChurchStreet.Value = txtChurchStreet.Text;
            hdnChurchZip.Value = txtChurchZip.Text;
            hdnChurchState.Value = txtChurchState.Text;
            hdnChurchCity.Value = txtChurchCity.Text;
            hdnChurchTelephone.Value = txtChurchTelephone.Text;
        }
        protected void btnModalOk_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (btn.ID == "btnModalOk") mpeSucceeded.Hide();
            if (btn.ID == "btnModalFailure") mpeFailed.Hide();
        }

        protected void btnBillingAddressSaveConfirmation_Click(object sender, EventArgs e)
        {
            mpeBillingAddressSaveConfirmation.Hide();
        }

        protected void btnBillingAddrSavingFailure_Click(object sender, EventArgs e)
        {
            mpeBillingAddressSaveFailure.Hide();
        }

        protected void btnBillingAddrDeletionConfirmation_Click(object sender, EventArgs e)
        {
            mpeBillingAddressDeletionConfirmation.Hide();
        }

        protected void btnBillingAddrDeletionFailed_Click(object sender, EventArgs e)
        {
            mpeBillingAddressDeletionFailed.Hide();
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            Response.Redirect("~/Account/Login.aspx");
        }

        protected void OnRBtnBankACH_Selected(object sender, EventArgs e)
        {
            mpePaymentBankACHDialogBox.Show();
        }

        protected void btnPaymentBankACH_Save(object sender, EventArgs e)
        {
            mpePaymentBankACHDialogBox.Hide();
        }

        protected void btnPaymentBankACH_Cancel(object sender, EventArgs e)
        {
            mpePaymentBankACHDialogBox.Hide();
        }

        protected void OnRBtnCreditCard_Selected(object sender, EventArgs e)
        {
            mpePaymentCreditCardDialogBox.Show();
        }

        protected void btnPaymentCreditCardInfo_Save(object sender, EventArgs e)
        {
            mpePaymentCreditCardDialogBox.Hide();
        }

        protected void btnPaymentCreditCardInfo_Cancel(object sender, EventArgs e)
        {
            mpePaymentCreditCardDialogBox.Hide();
        }

        protected void OnLnkGHSortByGiftNo_Clicked(object sender, EventArgs e)
        {

            HtmlTableCell td = (HtmlTableCell)lvGiftHistory.FindControl("tdSortByGiftNo");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByGiftNo = (LinkButton)lvGiftHistory.FindControl("lnkSortByGiftNo");
            lnkBtnByGiftNo.ForeColor = Color.Blue;
            LinkButton lnkBtnByDate = (LinkButton)lvGiftHistory.FindControl("lnkSortByDate");
            lnkBtnByDate.ForeColor = Color.Black;
            LinkButton lnkBtnByAmount = (LinkButton)lvGiftHistory.FindControl("lnkSortByAmount");
            lnkBtnByAmount.ForeColor = Color.Black;
            LinkButton lnkBtnByCode = (LinkButton)lvGiftHistory.FindControl("lnkSortByCode");
            lnkBtnByCode.ForeColor = Color.Black;
            LinkButton lnkBtnByNote = (LinkButton)lvGiftHistory.FindControl("lnkSortByNote");
            lnkBtnByNote.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.GiftHistoryPage;
        }

        protected void OnLnkGHSortByDate_Clicked(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvGiftHistory.FindControl("tdSortByDate");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByGiftNo = (LinkButton)lvGiftHistory.FindControl("lnkSortByGiftNo");
            lnkBtnByGiftNo.ForeColor = Color.Black;
            LinkButton lnkBtnByDate = (LinkButton)lvGiftHistory.FindControl("lnkSortByDate");
            lnkBtnByDate.ForeColor = Color.Blue;
            LinkButton lnkBtnByAmount = (LinkButton)lvGiftHistory.FindControl("lnkSortByAmount");
            lnkBtnByAmount.ForeColor = Color.Black;
            LinkButton lnkBtnByCode = (LinkButton)lvGiftHistory.FindControl("lnkSortByCode");
            lnkBtnByCode.ForeColor = Color.Black;
            LinkButton lnkBtnByNote = (LinkButton)lvGiftHistory.FindControl("lnkSortByNote");
            lnkBtnByNote.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.GiftHistoryPage;
        }

        protected void OnLnkGHSortByAmount_Click(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvGiftHistory.FindControl("tdSortByAmount");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByGiftNo = (LinkButton)lvGiftHistory.FindControl("lnkSortByGiftNo");
            lnkBtnByGiftNo.ForeColor = Color.Black;
            LinkButton lnkBtnByDate = (LinkButton)lvGiftHistory.FindControl("lnkSortByDate");
            lnkBtnByDate.ForeColor = Color.Black;
            LinkButton lnkBtnByAmount = (LinkButton)lvGiftHistory.FindControl("lnkSortByAmount");
            lnkBtnByAmount.ForeColor = Color.Blue;
            LinkButton lnkBtnByCode = (LinkButton)lvGiftHistory.FindControl("lnkSortByCode");
            lnkBtnByCode.ForeColor = Color.Black;
            LinkButton lnkBtnByNote = (LinkButton)lvGiftHistory.FindControl("lnkSortByNote");
            lnkBtnByNote.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.GiftHistoryPage;
        }

        protected void OnLnkGHSortByCode_Click(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvGiftHistory.FindControl("tdSortByCode");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByGiftNo = (LinkButton)lvGiftHistory.FindControl("lnkSortByGiftNo");
            lnkBtnByGiftNo.ForeColor = Color.Black;
            LinkButton lnkBtnByDate = (LinkButton)lvGiftHistory.FindControl("lnkSortByDate");
            lnkBtnByDate.ForeColor = Color.Black;
            LinkButton lnkBtnByAmount = (LinkButton)lvGiftHistory.FindControl("lnkSortByAmount");
            lnkBtnByAmount.ForeColor = Color.Black;
            LinkButton lnkBtnByCode = (LinkButton)lvGiftHistory.FindControl("lnkSortByCode");
            lnkBtnByCode.ForeColor = Color.Blue;
            LinkButton lnkBtnByNote = (LinkButton)lvGiftHistory.FindControl("lnkSortByNote");
            lnkBtnByNote.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.GiftHistoryPage;
        }

        protected void OnLnkGHSortByNote_Click(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvGiftHistory.FindControl("tdSortByNote");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByGiftNo = (LinkButton)lvGiftHistory.FindControl("lnkSortByGiftNo");
            lnkBtnByGiftNo.ForeColor = Color.Black;
            LinkButton lnkBtnByDate = (LinkButton)lvGiftHistory.FindControl("lnkSortByDate");
            lnkBtnByDate.ForeColor = Color.Black;
            LinkButton lnkBtnByAmount = (LinkButton)lvGiftHistory.FindControl("lnkSortByAmount");
            lnkBtnByAmount.ForeColor = Color.Black;
            LinkButton lnkBtnByCode = (LinkButton)lvGiftHistory.FindControl("lnkSortByCode");
            lnkBtnByCode.ForeColor = Color.Black;
            LinkButton lnkBtnByNote = (LinkButton)lvGiftHistory.FindControl("lnkSortByNote");
            lnkBtnByNote.ForeColor = Color.Blue;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.GiftHistoryPage;
        }

        /// <summary>
        /// //////// NP Page 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        //protected void OnLnkNPSortByDate_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPDate");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Blue;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Black;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Black;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Black;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Black;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Black;

        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;
        //}

        //protected void OnLnkNPSortByPatient_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPPatient");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Black;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Blue;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Black;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Black;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Black;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Black;


        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;

        //}

        //protected void OnLnkNPSortByHospital_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPHospital");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Black;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Black;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Blue;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Black;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Black;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Black;


        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;

        //}

        //protected void OnLnkNPSortByAmount_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPAmount");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Black;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Black;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Black;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Blue;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Black;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Black;


        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;

        //}

        //protected void OnLnkNPSortByBalance_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPBalance");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Black;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Black;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Black;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Black;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Blue;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Black;


        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;

        //}

        //protected void OnLnkNPSortByMemo_Clicked(object sender, EventArgs e)
        //{
        //    HtmlTableCell td = (HtmlTableCell)lvNeedsProcessing.FindControl("tdSortByNPMemo");

        //    if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
        //    else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

        //    LinkButton lnkBtnNPByDate = (LinkButton)lvNeedsProcessing.FindControl("lnkNeedsProcessingDate");
        //    lnkBtnNPByDate.ForeColor = Color.Black;

        //    LinkButton lnkBtnByPatient = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByPatient");
        //    lnkBtnByPatient.ForeColor = Color.Black;

        //    LinkButton lnkBtnByHospital = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByHospital");
        //    lnkBtnByHospital.ForeColor = Color.Black;

        //    LinkButton lnkBtnByAmount = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByAmount");
        //    lnkBtnByAmount.ForeColor = Color.Black;

        //    LinkButton lnkBtnByBalance = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByBalance");
        //    lnkBtnByBalance.ForeColor = Color.Black;

        //    LinkButton lnkBtnByMemo = (LinkButton)lvNeedsProcessing.FindControl("lnkSortByMemo");
        //    lnkBtnByMemo.ForeColor = Color.Blue;


        //    tabContainerRegister.ActiveTabIndex = (int)MainTabPage.NeedsProcessingPage;

        //}

        protected void OnLnkFilesSortByFile_Clicked(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvFiles.FindControl("tdFilesSortByFile");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByFile = (LinkButton)lvFiles.FindControl("lnkSortByFilesFile");
            lnkBtnByFile.ForeColor = Color.Blue;

            LinkButton lnkBtnByCategory = (LinkButton)lvFiles.FindControl("lnkSortByFilesCategory");
            lnkBtnByCategory.ForeColor = Color.Black;

            LinkButton lnkBtnByMember = (LinkButton)lvFiles.FindControl("lnkSortByFilesMember");
            lnkBtnByMember.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.FilesPage;
        }

        protected void OnLnkFilesSortByCategory_Clicked(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvFiles.FindControl("tdFilesSortByCategory");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByFile = (LinkButton)lvFiles.FindControl("lnkSortByFilesFile");
            lnkBtnByFile.ForeColor = Color.Black;

            LinkButton lnkBtnByCategory = (LinkButton)lvFiles.FindControl("lnkSortByFilesCategory");
            lnkBtnByCategory.ForeColor = Color.Blue;

            LinkButton lnkBtnByMember = (LinkButton)lvFiles.FindControl("lnkSortByFilesMember");
            lnkBtnByMember.ForeColor = Color.Black;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.FilesPage;

        }

        protected void OnLnkFilesSortByMember_Clicked(object sender, EventArgs e)
        {
            HtmlTableCell td = (HtmlTableCell)lvFiles.FindControl("tdFilesSortByMember");

            if (td.Attributes["class"] == "arrow-up") td.Attributes["class"] = "arrow-down";
            else if (td.Attributes["class"] == "arrow-down") td.Attributes["class"] = "arrow-up";

            LinkButton lnkBtnByFile = (LinkButton)lvFiles.FindControl("lnkSortByFilesFile");
            lnkBtnByFile.ForeColor = Color.Black;

            LinkButton lnkBtnByCategory = (LinkButton)lvFiles.FindControl("lnkSortByFilesCategory");
            lnkBtnByCategory.ForeColor = Color.Black;

            LinkButton lnkBtnByMember = (LinkButton)lvFiles.FindControl("lnkSortByFilesMember");
            lnkBtnByMember.ForeColor = Color.Blue;

            tabContainerRegister.ActiveTabIndex = (int)MainTabPage.FilesPage;

        }

        protected void lvTreatmentHistory_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                //DropDownList ddlPatientName = e.Item.FindControl("ddlPatientName") as DropDownList;
                //DropDownList ddlHouseholdRole = e.Item.FindControl("ddlHouseholdRole") as DropDownList;
                TextBox txtPatientName = e.Item.FindControl("txtPatientName") as TextBox;
                TextBox txtHouseholdRole = e.Item.FindControl("txtHouseholdRole") as TextBox;
                TextBox txtTreatmentDate = e.Item.FindControl("txtTreatmentDate") as TextBox;
                TextBox txtTreatmentDetails = e.Item.FindControl("txtTreatmentDetails") as TextBox;
                TextBox txtPhysicianInfo = e.Item.FindControl("txtPhysicianInfo") as TextBox;

                //ddlPatientName.Items.Add(new ListItem(lstTreatmentHistory[e.Item.DataItemIndex].Name));
                //ddlHouseholdRole.SelectedValue = lstTreatmentHistory[e.Item.DataItemIndex].HouseholdRole;
                txtPatientName.Text = lstTreatmentHistory[e.Item.DataItemIndex].Name;
                txtHouseholdRole.Text = lstTreatmentHistory[e.Item.DataItemIndex].HouseholdRole;
                txtTreatmentDate.Text = lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDate.Value.ToString("MM/dd/yyyy");
                txtTreatmentDetails.Text = lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDescription;
                txtPhysicianInfo.Text = lstTreatmentHistory[e.Item.DataItemIndex].PhysicianInfo;
            }
        }

        protected void btnNewPasswordSubmit_Click(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (manager != null)
            {
                var user = manager.FindByEmail(strUserEmail);
                if (user == null)
                {
                    //ErrorMessage.Text = "No user found";
                    return;
                }

                var code = manager.GeneratePasswordResetToken(user.Id);
                if (code == null)
                {
                    //ErrorMessage.Text = "No reset token";
                    return;
                }

                var result = manager.ResetPassword(user.Id, code, txtNewPassword.Text);
                if (result.Succeeded)
                {
                    Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    Response.Redirect("~/Account/ResetPasswordConfirmation.aspx");
                    return;
                }
                else
                {
                    //ErrorMessage.Text = result.Errors.FirstOrDefault();
                    return;
                }
            }
            else
            {
                //ErrorMessage.Text = "Password has not been reset";
                return;
            }
        }

        protected void ddlReferredBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlReferred = (DropDownList)sender;

            if (ddlReferred.SelectedValue == "Member Referral")
            {
                txtReferredByMembership.Enabled = true;
                txtReferredByMembership.Text = "";
            }
            else
            {
                txtReferredByMembership.Text = "";
                txtReferredByMembership.Enabled = false;
                ddlReferredByContact.Items.Clear();
                ddlReferredByContact.Enabled = false;
            }
        }

        protected void txtReferredByMembership_TextChanged(object sender, EventArgs e)
        {
            if (ddlReferredBy.SelectedValue == "Member Referral")
            {
                TextBox txtMembershipNumber = (TextBox)sender;

                String strMembershipNumber = String.Empty;

                if (txtMembershipNumber.Text.Contains("MEMB-"))
                {
                    strMembershipNumber = txtMembershipNumber.Text;
                }
                else
                {
                    strMembershipNumber = txtMembershipNumber.Text;
                    if (txtMembershipNumber.Text.Length < 7)
                    {
                        for (int i = txtMembershipNumber.Text.Length; i < 7; i++)
                        {
                            strMembershipNumber = '0' + strMembershipNumber;
                        }
                    }
                    strMembershipNumber = "MEMB-" + strMembershipNumber;

                    txtMembershipNumber.Text = strMembershipNumber;
                }

                String strQueryForMembership = "select Name from Contact where c4g_Membership__r.Name = '" + strMembershipNumber + "'";

                SForce.QueryResult qrMembership = Sfdcbinding.query(strQueryForMembership);
                if (qrMembership.size > 0)
                {
                    ddlReferredByContact.Items.Clear();
                    ddlReferredByContact.Enabled = true;

                    for (int i = 0; i < qrMembership.size; i++)
                    {
                        SForce.Contact ctReferrer = qrMembership.records[i] as SForce.Contact;
                        ddlReferredByContact.Items.Add(ctReferrer.Name);
                    }
                }
            }
        }
    }
}