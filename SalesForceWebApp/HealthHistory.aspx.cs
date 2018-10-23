using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

using AjaxControlToolkit;

namespace SalesForceWebApp
{

    public class TreatmentInfo
    {
		public int AccountCreationStepCode { get; set; }
		public String AccountId { get; set; }
		public String ContactId { get; set; }
		public String Id { get; set; }
        public String Name { get; set; }
        public String HouseholdRole { get; set; }
        public DateTime? TreatmentDate { get; set; }
        public String TreatmentDescription { get; set; }
        public String PhysicianInfo { get; set; }
    }

  //  public class MedicalInfo
  //  {
  //      public int AccountCreationStepCode { get; set; }
  //      public String AccountId { get; set; }
  //      public String ContactId { get; set; }
		//public String Id { get; set; }
  //      public String Name { get; set; }
  //      public HouseholdRoles HouseholdRole { get; set; }
  //      public DateTime TreatmentDate { get; set; }
  //      public String TreatmentDescription { get; set; }
  //      public String PhysicianInfo { get; set; }
  //  }


    public class PatientNames
    {
        public String Name { get; set; }
    }

    public class DiseaseRecord
    {
        public int AccountCreationStepCode { get; set; }
        public String AccountId { get; set; }
        public String ContactId { get; set; }
        public String HouseholdRole { get; set; }
        public String Name { get; set; }
        public Boolean[] DiagnosedDisease { get; set; }
    }

    public partial class HealthHistory : System.Web.UI.Page
    {

        private String strAccountId = null;
        //private String strContactId = null;
        private int nNumberOfFamilyMembers = 0;
		protected String strTreatmentInfoContactId = null;

        /// <summary>
        /// The credential to access Salesforce
        /// </summary>
        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;

        /// <summary>
        /// 
        /// </summary>


        List<TreatmentInfo> lstTreatmentHistory = new List<TreatmentInfo>();
        //List<MedicalInfo> lstMedicalHistory = new List<MedicalInfo>();
        List<PatientNames> lstPatientNames = new List<PatientNames>();
        List<DiseaseRecord> lstDiseaseRecord = new List<DiseaseRecord>();
        //List<TreatmentInfo> lstTreatmentHistory;


        TableHeaderRow headerRow = null;
        TableHeaderCell headerCell = null;

        TableRow headRow = null;
        TableCell headCell = null;

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

        protected const String strTreatmentInstruction = "For every question answered \"yes\", please provide the following information.<br />" +
                                                     "위의 질문에 \"예\"라고 대답하신 경우, 그 내용을 기록해 주십시오.";

        protected const String strNoRegistersFound = "No registers found";

        //protected const String strAnswerHeadingName = "Name<br />이름";
        //protected const String strAnswerHeadingTreatmentDate = "Treatment Date<br />치료일자";
        //protected const String strAnswerHeadingDiagnosis = "Diagnosis | Duration | Results | Test Performed | Medication | Outcome<br />" +
        //                                                   "   병명        기간      결과          검사            투약       경과";
        //protected const String strAnswerHeadingPhysicianInfo = "Attending Physician's Name,<br />Address and Phone Number<br />의사이름, 주소 및 전화번호";

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            // if the Session variable below is null, retrieve AccountId from Salesforce
            //if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];

            //String strQueryForPatients = "select Id, Name, cmm_Household_Role__c from Contact where cmm_Household__r.Id = '" + strAccountId + "'";
            //SForce.QueryResult qrPatients = Sfdcbinding.query(strQueryForPatients);

            //if (qrPatients.size > 0)
            //{
            //    for (int i = 0; i < nNumberOfFamilyMembers; i++)
            //    {
            //        SForce.Contact ctPatient = (SForce.Contact)qrPatients.records[i];
            //        lstPatientNames.Add(new PatientNames { Name = ctPatient.Name });
            //        lstDiseaseRecord.Add(new DiseaseRecord
            //        {
            //            AccountCreationStepCode = 7,
            //            AccountId = strAccountId,
            //            ContactId = ctPatient.Id,
            //            HouseholdRole = ctPatient.cmm_Household_Role__c,
            //            Name = ctPatient.Name,
            //            DiagnosedDisease = new Boolean[9]
            //        });

            //        for (int j = 0; j < 9; j++)
            //        {
            //            lstDiseaseRecord[i].DiagnosedDisease[j] = false;
            //        }
            //    }
            //}

            if (!IsPostBack)
            {
                SetYesNoButton();
				LoadTreatmentInfo();
            }

			//LoadTreatmentInfo();

            //    for (int nColumn = 1; nColumn <= nNumberOfFamilyMembers; nColumn++)
            //    {
            //        for (int nRow = 1; nRow <= 9; nRow++)
            //        {

            //            HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nRow + "_" + nColumn);
            //            HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nRow + "_" + nColumn);

            //            Button btnYes = (Button)pnlHealthHistory.FindControl("btnYes_" + nRow + "_" + nColumn);
            //            Button btnNo = (Button)pnlHealthHistory.FindControl("btnNo_" + nRow + "_" + nColumn);

            //            if (hdnYes.Value == "red")
            //            {
            //                btnYes.BackColor = Color.Red;
            //                btnYes.ForeColor = Color.White;
            //            }
            //            if (hdnYes.Value == "lightgrey")
            //            {
            //                btnYes.BackColor = Color.LightGray;
            //                btnYes.ForeColor = Color.Black;
            //            }

            //            if (hdnNo.Value == "blue")
            //            {
            //                btnNo.BackColor = Color.Blue;
            //                btnNo.ForeColor = Color.White;
            //            }
            //            if (hdnNo.Value == "lightgrey")
            //            {
            //                btnNo.BackColor = Color.LightGray;
            //                btnNo.ForeColor = Color.Black;
            //            }
            //        }
            //    }
            //}

            //SetYesNoButton();

            //if ((List<DiseaseRecord>)Session["DiseaseRecord"] != null)
            //{
            //    Table table = (Table)pnlHealthHistory.FindControl("HealthHistory");

            //    lstDiseaseRecord = Session["DiseaseRecord"] as List<DiseaseRecord>;

            //    // 11/20/17 retrieve DiseaseRecord from tmp_DiseaseRecord here

            //    for (int i = 0; i < nNumberOfFamilyMembers; i++)
            //    {
            //        for (int j = 0; j < 9; j++)
            //        {
            //            if (lstDiseaseRecord[i].DiagnosedDisease[j] == true)
            //            {
            //                int nRow = j + 1;
            //                int nColumn = i + 1;

            //                Button btnYes = (Button)pnlHealthHistory.FindControl("btnYes_" + nRow + "_" + nColumn);
            //                Button btnNo = (Button)pnlHealthHistory.FindControl("btnNo_" + nRow + "_" + nColumn);

            //                btnYes.BackColor = Color.Red;
            //                btnYes.ForeColor = Color.White;

            //                btnNo.BackColor = Color.LightGray;
            //                btnNo.ForeColor = Color.Black;

            //                HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nRow + "_" + nColumn);
            //                HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nRow + "_" + nColumn);

            //                hdnYes.Value = "red";
            //                hdnNo.Value = "lightgrey";

            //            }
            //        }
            //    }
            //}

            //if ((List<TreatmentInfo>)Session["TreatmentHistory"] != null)
            //{
            //    lstTreatmentHistory = Session["TreatmentHistory"] as List<TreatmentInfo>;

            //    lvTreatmentHistory.DataSource = lstTreatmentHistory;
            //    lvTreatmentHistory.DataBind();
            //}
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

            InitializedSfdcbinding();

            // if the Session variable is null, retrieve AccountId from Salesforce

            if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];

            nNumberOfFamilyMembers = NumberOfFamilyMembers();

            Session["NumberOfFamilyMembers"] = nNumberOfFamilyMembers;

            Table table = new Table();
            table.ID = "HealthHistory";
            table.HorizontalAlign = HorizontalAlign.Center;

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

            //pnlHealthHistory.Controls.Add(table);
            pnlHealthHistory.Controls.Add(table);

            Table tblTreatmentHistoryInstruction = new Table();
            tblTreatmentHistoryInstruction.ID = "TreatmentHistory";
            tblTreatmentHistoryInstruction.HorizontalAlign = HorizontalAlign.Center;

            TableCell tcInstruction = new TableCell();
            Label lblInstruction = new Label();
            lblInstruction.Text = strTreatmentInstruction;
            tcInstruction.Controls.Add(lblInstruction);

            Button btnAddReport = new Button();
            btnAddReport.Width = 80;
            btnAddReport.Height = 25;
            btnAddReport.Text = "+ Add";
            btnAddReport.CssClass = "AddNewTreatmentReportButton";
            btnAddReport.Click += new EventHandler(AddNewTreatmentReport);

            tcInstruction.Controls.Add(btnAddReport);

            tcInstruction.BackColor = Color.LightPink;
            tcInstruction.BorderColor = Color.Gray;
            tcInstruction.BorderStyle = BorderStyle.Solid;
            tcInstruction.BorderWidth = 1;
            tcInstruction.Width = 1000;
            TableRow trInstruction = new TableRow();
            trInstruction.Cells.Add(tcInstruction);
            tblTreatmentHistoryInstruction.Rows.Add(trInstruction);

            pnlTreatmentInfoInstruction.Controls.Add(tblTreatmentHistoryInstruction);

            //SetYesNoButton();
            //if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];

            String strQueryForPatients = "select Id, Name, cmm_Household_Role__c from Contact where cmm_Household__r.Id = '" + strAccountId + "'";
            SForce.QueryResult qrPatients = Sfdcbinding.query(strQueryForPatients);

            if (qrPatients.size > 0)
            {
                for (int i = 0; i < nNumberOfFamilyMembers; i++)
                {
                    SForce.Contact ctPatient = (SForce.Contact)qrPatients.records[i];
                    lstPatientNames.Add(new PatientNames { Name = ctPatient.Name });
                    lstDiseaseRecord.Add(new DiseaseRecord
                    {
                        AccountCreationStepCode = 7,
                        AccountId = strAccountId,
                        ContactId = ctPatient.Id,
                        HouseholdRole = ctPatient.cmm_Household_Role__c,
                        Name = ctPatient.Name,
                        DiagnosedDisease = new Boolean[9]
                    });

                    for (int j = 0; j < 9; j++)
                    {
                        lstDiseaseRecord[i].DiagnosedDisease[j] = false;
                    }
                }
            }

        }

		protected void ddlPatientName_SelectedIndexChanged(object sender, EventArgs e)
		{
			DropDownList ddlPatientName = (DropDownList)sender;

			String strPatientName = ddlPatientName.SelectedValue;

			String strQueryForPatientContactId = "select Id from Contact where cmm_Household__c ='" + strAccountId + "' and Name = '" + strPatientName + "'";

			SForce.QueryResult qrPatientId = Sfdcbinding.query(strQueryForPatientContactId);

			if (qrPatientId.size == 1)
			{
				SForce.Contact ctPatientId = qrPatientId.records[0] as SForce.Contact;

				//strTreatmentInfoContactId = ctPatientId.Id;

				foreach (ListViewItem itemRow in lvTreatmentHistory.Items)
				{
					DropDownList ddlPatient = (DropDownList)itemRow.FindControl("ddlPatientName");

					if (strPatientName == ddlPatient.SelectedValue)
					{
						HiddenField hdnId = (HiddenField)itemRow.FindControl("hdnContactId");

						hdnId.Value = ctPatientId.Id;
					}
				}
			}
		}

		protected void AddNewTreatmentReport(object sender, EventArgs e)
        {
			//lvTreatmentHistory.Items.Clear();
			lstTreatmentHistory.Clear();

			//String strAccountId = null;

            foreach (ListViewItem item in lvTreatmentHistory.Items)
            {
				HiddenField hdnMedicalTreatmentId = item.FindControl("hdnMedicalTreatmentId") as HiddenField;
				HiddenField hdnAccountId = item.FindControl("hdnAccountId") as HiddenField;
				//strAccountId = hdnAccountId.Value;
				HiddenField hdnContactId = item.FindControl("hdnContactId") as HiddenField;
				DropDownList ddlPatient = item.FindControl("ddlPatientName") as DropDownList;
                DropDownList ddlHouseholdRole = item.FindControl("ddlHouseholdRole") as DropDownList;
                TextBox txtTreatmentDate = item.FindControl("txtTreatmentDate") as TextBox;
				String[] strTreatmentDate = txtTreatmentDate.Text.Split('/');
                if (strTreatmentDate.Length == 1)
                {
                    strTreatmentDate = new String[3];
                    strTreatmentDate[0] = "1";
                    strTreatmentDate[1] = "1";
                    strTreatmentDate[2] = "1000";
                }
                TextBox txtTreatmentDetails = item.FindControl("txtTreatmentDetails") as TextBox;
                TextBox txtPhysicianInfo = item.FindControl("txtPhysicianInfo") as TextBox;

				String strMedicalTreatmentId = "";
				String strContactId = "";

				if (hdnMedicalTreatmentId.Value != "") strMedicalTreatmentId = hdnMedicalTreatmentId.Value;
				if (hdnContactId.Value != "") strContactId = hdnContactId.Value;

				lstTreatmentHistory.Add(new TreatmentInfo
				{
					AccountCreationStepCode = 7,
					Id = strMedicalTreatmentId,
					AccountId = hdnAccountId.Value,
					ContactId = strContactId,
					Name = ddlPatient.SelectedValue,
					HouseholdRole = ddlHouseholdRole.SelectedValue,
					TreatmentDate = new DateTime(Int32.Parse(strTreatmentDate[2]), Int32.Parse(strTreatmentDate[0]), Int32.Parse(strTreatmentDate[1])),
					TreatmentDescription = txtTreatmentDetails.Text,
					PhysicianInfo = txtPhysicianInfo.Text
				});
			}

			String strQueryForPrimaryName = "select Id, Name from Contact where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Head of Household'";

			SForce.QueryResult qrPrimaryName = Sfdcbinding.query(strQueryForPrimaryName);

			if (qrPrimaryName.size > 0)
			{
				SForce.Contact ctHeadOfHousehold = qrPrimaryName.records[0] as SForce.Contact;

				lstTreatmentHistory.Add(new TreatmentInfo
				{
					AccountCreationStepCode = 7,
					AccountId = strAccountId,
					ContactId = ctHeadOfHousehold.Id,
					Name = ctHeadOfHousehold.Name,
					HouseholdRole = "Head of Household",
					TreatmentDate = null,
					TreatmentDescription = null,
					PhysicianInfo = null
				});
			}


			//lstTreatmentHistory.Add(new TreatmentInfo { AccountCreationStepCode = 7,
			//											AccountId = strAccountId,
			//											ContactId = null,
			//											Name = null,
			//											HouseholdRole = null,
			//											TreatmentDate = null,
			//											TreatmentDescription = null,
			//											PhysicianInfo = null });

            lvTreatmentHistory.DataSource = lstTreatmentHistory;
            lvTreatmentHistory.DataBind();

			//foreach (ListViewItem item in lvTreatmentHistory.Items)
			//{
			//	DropDownList ddlPatientName = item.FindControl("ddlPatientName") as DropDownList;
			//	;
			//}

			RestoreYesNoButton();
        }

		protected void RestoreYesNoButton()
		{
			int nNumberOfFamilyMembers = NumberOfFamilyMembers();

			for (int nColumn = 1; nColumn <= nNumberOfFamilyMembers; nColumn++)
			{
				for (int nRow = 1; nRow <= 9; nRow++)
				{
					HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nRow + "_" + nColumn);
					HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nRow + "_" + nColumn);

					Button btnYes = (Button)pnlHealthHistory.FindControl("btnYes_" + nRow + "_" + nColumn);
					Button btnNo = (Button)pnlHealthHistory.FindControl("btnNo_" + nRow + "_" + nColumn);

					if (hdnYes.Value == "red" && hdnNo.Value == "lightgrey")
					{
						btnYes.BackColor = Color.Red;
						btnYes.ForeColor = Color.White;
						btnNo.BackColor = Color.LightGray;
						btnNo.ForeColor = Color.Black;
					}
					if (hdnYes.Value == "lightgrey" && hdnNo.Value == "blue")
					{
						btnYes.BackColor = Color.LightGray;
						btnYes.ForeColor = Color.Black;
						btnNo.BackColor = Color.Blue;
						btnNo.ForeColor = Color.White;
					}
				}
			}
		}

		protected void lvTreatmentHistory_ItemDataBound(object sender, ListViewItemEventArgs e)
		{ 
			if (e.Item.ItemType == ListViewItemType.DataItem)
			{
				HiddenField hdnAccountCreationStep = e.Item.FindControl("hdnAccountCreationStepCode") as HiddenField;
				HiddenField hdnMedicalTreatmentId = e.Item.FindControl("hdnMedicalTreatmentId") as HiddenField;
				HiddenField hdnAccountId = e.Item.FindControl("hdnAccountId") as HiddenField;
				HiddenField hdnContactId = e.Item.FindControl("hdnContactId") as HiddenField;
				DropDownList ddlPatientName = e.Item.FindControl("ddlPatientName") as DropDownList;
				DropDownList ddlHouseholdRole = e.Item.FindControl("ddlHouseholdRole") as DropDownList;
				TextBox txtTreatmentDate = e.Item.FindControl("txtTreatmentDate") as TextBox;
				TextBox txtTreatmentDetails = e.Item.FindControl("txtTreatmentDetails") as TextBox;
				TextBox txtPhysicianInfo = e.Item.FindControl("txtPhysicianInfo") as TextBox;

				//if ((lstTreatmentHistory[e.Item.DataItemIndex].AccountId == null) &&
				if ((lstTreatmentHistory[e.Item.DataItemIndex].ContactId == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].Id == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].Name == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].HouseholdRole == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDate == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDescription == null) &&
					(lstTreatmentHistory[e.Item.DataItemIndex].PhysicianInfo == null))
				{
					if (nNumberOfFamilyMembers > 0)
					{
						for (int i = 0; i < nNumberOfFamilyMembers; i++)
						{
							hdnAccountCreationStep.Value = lstTreatmentHistory[e.Item.DataItemIndex].AccountCreationStepCode.ToString();
							hdnAccountId.Value = lstTreatmentHistory[e.Item.DataItemIndex].AccountId;
							ddlPatientName.Items.Add(lstPatientNames[i].Name);
							ddlPatientName.SelectedIndex = 0;
						}
					}

				}
				else if ((lstTreatmentHistory[e.Item.DataItemIndex].AccountId != null) ||
						 (lstTreatmentHistory[e.Item.DataItemIndex].ContactId != null) ||
						 (lstTreatmentHistory[e.Item.DataItemIndex].Id != null) ||
						 (lstTreatmentHistory[e.Item.DataItemIndex].Name != null) ||
                         (lstTreatmentHistory[e.Item.DataItemIndex].HouseholdRole != null) ||
                         (lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDate != null) ||
                         (lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDescription != null) ||
                         (lstTreatmentHistory[e.Item.DataItemIndex].PhysicianInfo != null))
                {
                    if (nNumberOfFamilyMembers > 0)
                    {
                        for (int i = 0; i < nNumberOfFamilyMembers; i++)
                        {
                            ddlPatientName.Items.Add(lstPatientNames[i].Name);
                        }
                        ddlPatientName.SelectedValue = lstTreatmentHistory[e.Item.DataItemIndex].Name;
                    }
					hdnAccountId.Value = lstTreatmentHistory[e.Item.DataItemIndex].AccountId;

					hdnAccountCreationStep.Value = lstTreatmentHistory[e.Item.DataItemIndex].AccountCreationStepCode.ToString();
					if (lstTreatmentHistory[e.Item.DataItemIndex].ContactId != "") hdnContactId.Value = lstTreatmentHistory[e.Item.DataItemIndex].ContactId;
					if (lstTreatmentHistory[e.Item.DataItemIndex].Id != "") hdnMedicalTreatmentId.Value = lstTreatmentHistory[e.Item.DataItemIndex].Id;
					ddlHouseholdRole.SelectedValue = lstTreatmentHistory[e.Item.DataItemIndex].HouseholdRole;
					if (lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDate != null)
					{
						txtTreatmentDate.Text = lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDate.Value.ToString("MM/dd/yyyy");
					}
					txtTreatmentDetails.Text = lstTreatmentHistory[e.Item.DataItemIndex].TreatmentDescription;
					txtPhysicianInfo.Text = lstTreatmentHistory[e.Item.DataItemIndex].PhysicianInfo;
				}

            }
        }

        protected void lvTreatmentHistory_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            foreach (ListViewItem itemRow in lvTreatmentHistory.Items)
            {
				HiddenField hdnMedicalTreatmentId = itemRow.FindControl("hdnMedicalTreatmentId") as HiddenField;
				HiddenField hdnAccountId = itemRow.FindControl("hdnAccountId") as HiddenField;
				HiddenField hdnContactId = itemRow.FindControl("hdnContactId") as HiddenField;
                DropDownList ddlPatient = itemRow.FindControl("ddlPatientName") as DropDownList;
                DropDownList ddlHouseholdRold = itemRow.FindControl("ddlHouseholdRole") as DropDownList;
                TextBox txtTreatmentDate = itemRow.FindControl("txtTreatmentDate") as TextBox;
				String[] strTreatmentDate = txtTreatmentDate.Text.Split('/');
                if (strTreatmentDate.Length == 1)
                {
                    strTreatmentDate = new String[3];
                    strTreatmentDate[0] = "1";
                    strTreatmentDate[1] = "1";
                    strTreatmentDate[2] = "1000";
                }
                TextBox txtTreatmentDetails = itemRow.FindControl("txtTreatmentDetails") as TextBox;
                TextBox txtPhysicianInfo = itemRow.FindControl("txtPhysicianInfo") as TextBox;

                lstTreatmentHistory.Add(new TreatmentInfo
                {
					Id = hdnMedicalTreatmentId.Value,
					AccountId = hdnAccountId.Value,
					ContactId = hdnContactId.Value,
                    Name = ddlPatient.SelectedValue,
                    HouseholdRole = ddlHouseholdRold.SelectedValue,
                    TreatmentDate = new DateTime(Int32.Parse(strTreatmentDate[2]), Int32.Parse(strTreatmentDate[0]), Int32.Parse(strTreatmentDate[1])),
                    TreatmentDescription = txtTreatmentDetails.Text,
                    PhysicianInfo = txtPhysicianInfo.Text
                });

            }

            lstTreatmentHistory.RemoveAt(e.ItemIndex);

            lvTreatmentHistory.DataSource = lstTreatmentHistory;
            lvTreatmentHistory.DataBind();
            upnlTreatmentHistory.Update();

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
            headCell.BorderColor = Color.Gray;
            headCell.Width = 400;
            headCell.Font.Size = FontUnit.Smaller;
            headCell.HorizontalAlign = HorizontalAlign.Left;

            headRow.BorderWidth = 1;
            headRow.BorderStyle = BorderStyle.Solid;
            headRow.BorderColor = Color.Gray;

            headRow.Cells.Add(headCell);

            table.Rows.Add(headRow);

            PopulateTableHeader(table);

        }

        protected void PopulateTableHeader(Table table)
        {

            String strQueryForHouseholdMembers = "select Name, cmm_Household_Role__c from Contact where cmm_Household__r.Id = '" + strAccountId + "'";

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
                        tc.ID = "Head";
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;


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
                        tc.ID = "Spouse";
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;

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
                        tc.ID = "Child" + i;
                        tc.HorizontalAlign = HorizontalAlign.Center;
                        tc.Width = 600 / qrHouseholdMembers.size;

                        headRow.Cells.Add(tc);
                        table.Rows.Add(headRow);
                    }
                }
            }
        }

        protected int NumberOfFamilyMembers()
        {
            String strQueryForHouseholdMembers = "select Id, Name from Contact where cmm_Household__c = '" + strAccountId + "'";

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

                HiddenField hdnYes = new HiddenField();
                hdnYes.ID = "hdnYes_" + nRow + "_" + i;
                hdnYes.Value = "lightgrey";


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

                HiddenField hdnNo = new HiddenField();
                hdnNo.ID = "hdnNo_" + nRow + "_" + i;
                hdnNo.Value = "blue";

                btnYes.Attributes.Add("onclick", "toggleYes(this); return false;");
                btnNo.Attributes.Add("onclick", "toggleNo(this); return false;");

                tcAnswer = new TableCell();
                tcAnswer.ColumnSpan = 1;
                tcAnswer.BorderWidth = 1;
                tcAnswer.BorderColor = Color.Gray;
                tcAnswer.BorderStyle = BorderStyle.Solid;
                tcAnswer.Width = 600 / nNumberOfFamilyMembers;
                tcAnswer.HorizontalAlign = HorizontalAlign.Center;
                tcAnswer.VerticalAlign = VerticalAlign.Middle;
                tcAnswer.Controls.Add(btnYes);
                tcAnswer.Controls.Add(hdnYes);
                tcAnswer.Controls.Add(btnNo);
                tcAnswer.Controls.Add(hdnNo);

                tr.Cells.Add(tcAnswer);

            }

            table.Rows.Add(tr);
        }

        private void BtnNo_Click(object sender, EventArgs e)
        {
            Button btnNo = (Button)sender;

            ControlCollection sibling = btnNo.Parent.Controls;
            Button btnYes = sibling[0] as Button;

            if (btnNo.BackColor == Color.LightGray)
            {
                btnNo.BackColor = Color.Blue;
                btnNo.ForeColor = Color.White;
                btnYes.BackColor = Color.LightGray;
                btnYes.ForeColor = Color.Black;
            }
            else if (btnNo.BackColor == Color.Blue)
            {
                btnNo.BackColor = Color.LightGray;
                btnNo.ForeColor = Color.Black;
                btnYes.BackColor = Color.Red;
                btnYes.ForeColor = Color.White;
            }
        }

        private void BtnYes_Click(object sender, EventArgs e)
        {
            Button btnYes = (Button)sender;

            ControlCollection sibling = btnYes.Parent.Controls;
            Button btnNo = sibling[1] as Button;

            if (btnYes.BackColor == Color.LightGray)
            {
                btnYes.BackColor = Color.Red;
                btnYes.ForeColor = Color.White;
                btnNo.BackColor = Color.LightGray;
                btnNo.ForeColor = Color.Black;
            }
            else if (btnYes.BackColor == Color.Red)
            {
                btnYes.BackColor = Color.LightGray;
                btnYes.ForeColor = Color.Black;
                btnNo.BackColor = Color.Blue;
                btnNo.ForeColor = Color.White;
            }
        }

        protected void SetYesNoButton()
        {

            String strQueryForTmpDiseaseRecord = "select Arthritis_back_nervous_system__c, Congenital_disease_or_other_condition__c, Diagnosed_with_Allergy_Respiratory__c, " +
                                                 "Diagnosed_with_Cardiovascular_issues__c, Eyes_nose_ears_hands_feet_conditions__c, Prostate_or_female_reprdct_conditions__c, " +
                                                 "Stomach_liver_colon_kidney_conditions__c, Thyroid_tumor_cancer_medical_conditions__c, Treated_in_last_12_months__c, " +
                                                 "cmm_Contact__c, cmm_Contact__r.cmm_Household_Role__c, cmm_Name__c " +
                                                 "from tmp_Disease_Record__c where cmm_Account__c = '" + strAccountId + "' order by cmm_Contact__r.Birthdate";

            SForce.QueryResult qrTmpDiseaseRecord = Sfdcbinding.query(strQueryForTmpDiseaseRecord);

            if (qrTmpDiseaseRecord.size > 0)
            {
                List<DiseaseRecord> lstDiseaseRecord = new List<DiseaseRecord>();
                Table table = (Table)pnlHealthHistory.FindControl("HealthHistory");

				for (int nRecord = 0; nRecord < qrTmpDiseaseRecord.size; nRecord++)
				{
					int nColumn = nRecord + 1;
					SForce.tmp_Disease_Record__c tmpDiseaseRecord = qrTmpDiseaseRecord.records[nRecord] as SForce.tmp_Disease_Record__c;

					if (tmpDiseaseRecord.cmm_Contact__r.cmm_Household_Role__c == "Head of Household")
					{
						for (int nRow = 1; nRow <= 9; nRow++)
						{
							if (nColumn == 1 && nRow == 1) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Treated_in_last_12_months__c);
							if (nColumn == 1 && nRow == 2) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Cardiovascular_issues__c);
							if (nColumn == 1 && nRow == 3) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Allergy_Respiratory__c);
							if (nColumn == 1 && nRow == 4) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Arthritis_back_nervous_system__c);
							if (nColumn == 1 && nRow == 5) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Eyes_nose_ears_hands_feet_conditions__c);
							if (nColumn == 1 && nRow == 6) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Stomach_liver_colon_kidney_conditions__c);
							if (nColumn == 1 && nRow == 7) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Thyroid_tumor_cancer_medical_conditions__c);
							if (nColumn == 1 && nRow == 8) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Prostate_or_female_reprdct_conditions__c);
							if (nColumn == 1 && nRow == 9) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Congenital_disease_or_other_condition__c);
						}
					}
				}
				for (int nRecord = 0; nRecord < qrTmpDiseaseRecord.size; nRecord++)
				{
					int nColumn = nRecord + 1;
					SForce.tmp_Disease_Record__c tmpDiseaseRecord = qrTmpDiseaseRecord.records[nRecord] as SForce.tmp_Disease_Record__c;

					if (tmpDiseaseRecord.cmm_Contact__r.cmm_Household_Role__c == "Spouse")
					{
						for (int nRow = 1; nRow <= 9; nRow++)
						{
							if (nColumn == 2 && nRow == 1) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Treated_in_last_12_months__c);
							if (nColumn == 2 && nRow == 2) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Cardiovascular_issues__c);
							if (nColumn == 2 && nRow == 3) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Allergy_Respiratory__c);
							if (nColumn == 2 && nRow == 4) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Arthritis_back_nervous_system__c);
							if (nColumn == 2 && nRow == 5) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Eyes_nose_ears_hands_feet_conditions__c);
							if (nColumn == 2 && nRow == 6) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Stomach_liver_colon_kidney_conditions__c);
							if (nColumn == 2 && nRow == 7) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Thyroid_tumor_cancer_medical_conditions__c);
							if (nColumn == 2 && nRow == 8) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Prostate_or_female_reprdct_conditions__c);
							if (nColumn == 2 && nRow == 9) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Congenital_disease_or_other_condition__c);
						}
					}
				}
				for (int nRecord = 0; nRecord < qrTmpDiseaseRecord.size; nRecord++)
				{
					int nColumn = nRecord + 1;
					SForce.tmp_Disease_Record__c tmpDiseaseRecord = qrTmpDiseaseRecord.records[nRecord] as SForce.tmp_Disease_Record__c;

					if (tmpDiseaseRecord.cmm_Contact__r.cmm_Household_Role__c == "Child")
                    {
                        int nChildId = nRecord;
                        TableCell tc = table.FindControl("Child" + nChildId) as TableCell;

                        if (tc.Text == tmpDiseaseRecord.cmm_Name__c)
                        {
                            for (int nRow = 1; nRow <= 9; nRow++)
                            {
                                if (nColumn >= 3 && nRow == 1) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Treated_in_last_12_months__c);
                                if (nColumn >= 3 && nRow == 2) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Cardiovascular_issues__c);
                                if (nColumn >= 3 && nRow == 3) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Diagnosed_with_Allergy_Respiratory__c);
                                if (nColumn >= 3 && nRow == 4) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Arthritis_back_nervous_system__c);
                                if (nColumn >= 3 && nRow == 5) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Eyes_nose_ears_hands_feet_conditions__c);
                                if (nColumn >= 3 && nRow == 6) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Stomach_liver_colon_kidney_conditions__c);
                                if (nColumn >= 3 && nRow == 7) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Thyroid_tumor_cancer_medical_conditions__c);
                                if (nColumn >= 3 && nRow == 8) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Prostate_or_female_reprdct_conditions__c);
                                if (nColumn >= 3 && nRow == 9) SetEachYesNoButton(nColumn, nRow, (Boolean)tmpDiseaseRecord.Congenital_disease_or_other_condition__c);
                            }
                        }
                    }
                }
            }
            //for (int nColumn = 1; nColumn <= nNumberOfFamilyMembers; nColumn++)
            //{
            //    for (int nRow = 1; nRow <= 9; nRow++)
            //    {

            //        HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nRow + "_" + nColumn);
            //        HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nRow + "_" + nColumn);

            //        Button btnYes = (Button)pnlHealthHistory.FindControl("btnYes_" + nRow + "_" + nColumn);
            //        Button btnNo = (Button)pnlHealthHistory.FindControl("btnNo_" + nRow + "_" + nColumn);

            //        if (hdnYes.Value == "red")
            //        {
            //            btnYes.BackColor = Color.Red;
            //            btnYes.ForeColor = Color.White;
            //        }
            //        if (hdnYes.Value == "lightgrey")
            //        {
            //            btnYes.BackColor = Color.LightGray;
            //            btnYes.ForeColor = Color.Black;
            //        }

            //        if (hdnNo.Value == "blue")
            //        {
            //            btnNo.BackColor = Color.Blue;
            //            btnNo.ForeColor = Color.White;
            //        }
            //        if (hdnNo.Value == "lightgrey")
            //        {
            //            btnNo.BackColor = Color.LightGray;
            //            btnNo.ForeColor = Color.Black;
            //        }
            //    }
            //}
        }

        protected void SetEachYesNoButton(int nColumn, int nRow, Boolean bDiseaseRecord)
        {
            HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nRow + "_" + nColumn);
            HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nRow + "_" + nColumn);

            Button btnYes = (Button)pnlHealthHistory.FindControl("btnYes_" + nRow + "_" + nColumn);
            Button btnNo = (Button)pnlHealthHistory.FindControl("btnNo_" + nRow + "_" + nColumn);

            if (bDiseaseRecord == true)
            {
                hdnYes.Value = "red";
                hdnNo.Value = "lightgrey";

                btnYes.BackColor = Color.Red;
                btnYes.ForeColor = Color.White;

                btnNo.BackColor = Color.LightGray;
                btnNo.ForeColor = Color.Black;
            }
            if (bDiseaseRecord == false)
            {
                hdnYes.Value = "lightgrey";
                hdnNo.Value = "blue";

                btnYes.BackColor = Color.LightGray;
                btnYes.ForeColor = Color.Black;

                btnNo.BackColor = Color.Blue;
                btnNo.ForeColor = Color.White;
            }
        }

		protected void LoadTreatmentInfo()
		{

            //lstTreatmentHistory = new List<TreatmentInfo>();

			String strQueryForHousehold = "select Id, Name, cmm_Household_Role__c from Contact where cmm_Household__c = '" + strAccountId + "'";

			SForce.QueryResult qrContactIdForHousehold = Sfdcbinding.query(strQueryForHousehold);

			if (qrContactIdForHousehold.size > 0)
			{

				String strQueryForMedicalTreatmentInfo = "select Id, cmm_Contact__c, cmm_Account__c, cmm_Name__c, cmm_Household_Role__c, cmm_Treatment_Date__c, " +
														 "cmm_Treatment_Description__c, cmm_Physician_Info__c from tmp_Medical_History__c " + 
														 "where cmm_Account__c = '" + strAccountId + "' order by cmm_Contact__r.Birthdate";

				SForce.QueryResult qrMedicalTreatmentInfo = Sfdcbinding.query(strQueryForMedicalTreatmentInfo);

				if (qrMedicalTreatmentInfo.size > 0)
				{

					for (int nColumn = 1; nColumn <= qrMedicalTreatmentInfo.size; nColumn++)
					{
						SForce.tmp_Medical_History__c tmpMedicalTreatmentRecord = qrMedicalTreatmentInfo.records[nColumn - 1] as SForce.tmp_Medical_History__c;

						if (tmpMedicalTreatmentRecord.cmm_Household_Role__c == "Head of Household")
						{
							DateTime dtTreatmentDate = (DateTime)tmpMedicalTreatmentRecord.cmm_Treatment_Date__c;

							lstTreatmentHistory.Add(new TreatmentInfo
							{
								Id = tmpMedicalTreatmentRecord.Id,
								AccountId = tmpMedicalTreatmentRecord.cmm_Account__c,
								ContactId = tmpMedicalTreatmentRecord.cmm_Contact__c,
								Name = tmpMedicalTreatmentRecord.cmm_Name__c,
								HouseholdRole = tmpMedicalTreatmentRecord.cmm_Household_Role__c,
								TreatmentDate = dtTreatmentDate,
								TreatmentDescription = tmpMedicalTreatmentRecord.cmm_Treatment_Description__c,
								PhysicianInfo = tmpMedicalTreatmentRecord.cmm_Physician_Info__c
							});
						}
					}

					for (int nColumn = 1; nColumn <= qrMedicalTreatmentInfo.size; nColumn++)
					{
						SForce.tmp_Medical_History__c tmpMedicalTreatmentRecord = qrMedicalTreatmentInfo.records[nColumn - 1] as SForce.tmp_Medical_History__c;

						if (tmpMedicalTreatmentRecord.cmm_Household_Role__c == "Spouse")
						{
							DateTime dtTreatmentDate = (DateTime)tmpMedicalTreatmentRecord.cmm_Treatment_Date__c;

							lstTreatmentHistory.Add(new TreatmentInfo
							{
								Id = tmpMedicalTreatmentRecord.Id,
								AccountId = tmpMedicalTreatmentRecord.cmm_Account__c,
								ContactId = tmpMedicalTreatmentRecord.cmm_Contact__c,
								Name = tmpMedicalTreatmentRecord.cmm_Name__c,
								HouseholdRole = tmpMedicalTreatmentRecord.cmm_Household_Role__c,
								TreatmentDate = dtTreatmentDate,
								TreatmentDescription = tmpMedicalTreatmentRecord.cmm_Treatment_Description__c,
								PhysicianInfo = tmpMedicalTreatmentRecord.cmm_Physician_Info__c
							});
						}
					}

					for (int nColumn = 1; nColumn <= qrMedicalTreatmentInfo.size; nColumn++)
					{
						SForce.tmp_Medical_History__c tmpMedicalTreatmentRecord = qrMedicalTreatmentInfo.records[nColumn - 1] as SForce.tmp_Medical_History__c;

						if (tmpMedicalTreatmentRecord.cmm_Household_Role__c == "Child")
						{
							DateTime dtTreatmentDate = (DateTime)tmpMedicalTreatmentRecord.cmm_Treatment_Date__c;

							lstTreatmentHistory.Add(new TreatmentInfo
							{
								Id = tmpMedicalTreatmentRecord.Id,
								AccountId = tmpMedicalTreatmentRecord.cmm_Account__c,
								ContactId = tmpMedicalTreatmentRecord.cmm_Contact__c,
								Name = tmpMedicalTreatmentRecord.cmm_Name__c,
								HouseholdRole = tmpMedicalTreatmentRecord.cmm_Household_Role__c,
								TreatmentDate = dtTreatmentDate,
								TreatmentDescription = tmpMedicalTreatmentRecord.cmm_Treatment_Description__c,
								PhysicianInfo = tmpMedicalTreatmentRecord.cmm_Physician_Info__c
							});
						}
					}
				}                

				//lvTreatmentHistory.Items.Clear();
				lvTreatmentHistory.DataSource = lstTreatmentHistory;
				lvTreatmentHistory.DataBind();
			}
			

			//if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];

			//String strQueryForHousehold = "select Id, Name, cmm_Household_Role__c from Contact where cmm_Household__c = '" + strAccountId + "'";

			//SForce.QueryResult qrContactIdForHousehold = Sfdcbinding.query(strQueryForHousehold);

			//if (qrContactIdForHousehold.size > 0)
			//{
			//	//tpnlHealthHistory.Height = 1200 + 300 * qrContactIdForHousehold.size;

			//	string strQueryForNumberOfTreatmentRecord = "select Id from Medical_History__c where Contact__r.cmm_Household__c = '" + strAccountId + "'";

			//	SForce.QueryResult qrNumberOfTreatmentRecord = Sfdcbinding.query(strQueryForNumberOfTreatmentRecord);

			//	//if (qrNumberOfTreatmentRecord.size > 0) tpnlHealthHistory.Height = 1500 + 385 * qrNumberOfTreatmentRecord.size;

			//	for (int i = 0; i < qrContactIdForHousehold.size; i++)
			//	{
			//		SForce.Contact ctHouseholdMember = qrContactIdForHousehold.records[i] as SForce.Contact;

			//		String strQueryForTreatmentInfo = "select Treatment_Date__c, Treatment_Details__c, Physician_Information__c from Medical_History__c " +
			//										  "where Contact__c = '" + ctHouseholdMember.Id + "'";

			//		SForce.QueryResult qrTreatmentRecord = Sfdcbinding.query(strQueryForTreatmentInfo);

			//		if (qrTreatmentRecord.size > 0)
			//		{

			//			for (int nTreatmentNo = 0; nTreatmentNo < qrTreatmentRecord.size; nTreatmentNo++)
			//			{
			//				SForce.Medical_History__c treatmentRecord = qrTreatmentRecord.records[nTreatmentNo] as SForce.Medical_History__c;

			//				lstTreatmentHistory.Add(new TreatmentInfo
			//				{
			//					Name = ctHouseholdMember.Name,
			//					HouseholdRole = ctHouseholdMember.cmm_Household_Role__c,
			//					TreatmentDate = treatmentRecord.Treatment_Date__c.ToString(),
			//					TreatmentDescription = treatmentRecord.Treatment_Details__c,
			//					PhysicianInfo = treatmentRecord.Physician_Information__c
			//				});
			//			}
			//		}
			//	}
			//}

			//lvTreatmentHistory.Items.Clear();
			//lvTreatmentHistory.DataSource = lstTreatmentHistory;
			//lvTreatmentHistory.DataBind();
		}

        protected void btnNext_Click(object sender, EventArgs e)
        {

            //InitializedSfdcbinding();
            //SetYesNoButton();

			//foreach (ListViewItem item in lvTreatmentHistory.Items)
			//{
			//	HiddenField hdnMedicalTreatmentId = item.FindControl("hdnMedicalTreatmentId") as HiddenField;
			//	DropDownList ddlPatient = item.FindControl("ddlPatientName") as DropDownList;
			//	DropDownList ddlHouseholdRole = item.FindControl("ddlHouseholdRole") as DropDownList;
			//	TextBox txtTreatmentDate = item.FindControl("txtTreatmentDate") as TextBox;
			//	TextBox txtTreatmentDetails = item.FindControl("txtTreatmentDetails") as TextBox;
			//	TextBox txtPhysicianInfo = item.FindControl("txtPhysicianInfo") as TextBox;
			//}


            String strQueryForPatients = "select Name, Id from Contact where cmm_Household__r.Id = '" + strAccountId + "'";
            SForce.QueryResult qrPatients = Sfdcbinding.query(strQueryForPatients);

            if (qrPatients.size > 0)
            {
                Table tblHealthHistory = pnlHealthHistory.FindControl("HealthHistory") as Table;

                if (tblHealthHistory != null)
                {

                    for (int nColumn = 0; nColumn < qrPatients.size; nColumn++)
                    {

                        for (int nRow = 0; nRow < 9; nRow++)
                        {
                            int nMemberIndex = nColumn + 1;
                            int nDiseaseIndex = nRow + 1;

                            HiddenField hdnYes = (HiddenField)pnlHealthHistory.FindControl("hdnYes_" + nDiseaseIndex + "_" + nMemberIndex);
                            HiddenField hdnNo = (HiddenField)pnlHealthHistory.FindControl("hdnNo_" + nDiseaseIndex + "_" + nMemberIndex);

                            if (hdnYes != null)
                            {
                                if (hdnYes.Value == "red")
                                {
                                    lstDiseaseRecord[nColumn].DiagnosedDisease[nRow] = true;
                                }
                                if (hdnYes.Value == "lightgrey")
                                {
                                    lstDiseaseRecord[nColumn].DiagnosedDisease[nRow] = false;
                                }
                            }
                            //Button btnYes = tblHealthHistory.FindControl("btnYes_" + nDiseaseIndex + "_" + nMemberIndex) as Button;

                            //if (btnYes != null)
                            //{
                            //    if (btnYes.BackColor == Color.Red)
                            //    {
                            //        lstDiseaseRecord[nColumn].DiagnosedDisease[nRow] = true;
                            //    }
                            //    if (btnYes.BackColor == Color.LightGray)
                            //    {
                            //        lstDiseaseRecord[nColumn].DiagnosedDisease[nRow] = false;
                            //    }
                            //}
                        }
                    }
                    Session["DiseaseRecord"] = lstDiseaseRecord;
                    lstTreatmentHistory.Clear();

                    String strQueryForTreatmentForHousehold = "select Id from tmp_Medical_History__c where cmm_Account__c = '" + strAccountId + "'";

                    SForce.QueryResult qrTreatmentForHousehold = Sfdcbinding.query(strQueryForTreatmentForHousehold);

                    if (qrTreatmentForHousehold.size > 0)
                    {
                        String[] Ids = new String[qrTreatmentForHousehold.size];

                        for (int i = 0; i < qrTreatmentForHousehold.size; i++)
                        {
                            SForce.tmp_Medical_History__c tmpTreatmentRecord = qrTreatmentForHousehold.records[i] as SForce.tmp_Medical_History__c;

                            Ids[i] = tmpTreatmentRecord.Id;
                        }

                        SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(Ids);

                        SForce.DeleteResult deleteResult = deleteResults[0];
                        if (deleteResult.success)
                        {
                            // Treatment History for Household deleted
                        }
                    }

                    foreach (ListViewItem item in lvTreatmentHistory.Items)
					{
						HiddenField hdnAccountCreationStep = item.FindControl("hdnAccountCreationStepCode") as HiddenField;
						HiddenField hdnMedicalTreatmentId = item.FindControl("hdnMedicalTreatmentId") as HiddenField;
						HiddenField hdnAccountId = item.FindControl("hdnAccountId") as HiddenField;
						HiddenField hdnContactId = item.FindControl("hdnContactId") as HiddenField;
						DropDownList ddlPatientName = item.FindControl("ddlPatientName") as DropDownList;
						DropDownList ddlHouseholdRole = item.FindControl("ddlHouseholdRole") as DropDownList;
						TextBox txtTreatmentDate = item.FindControl("txtTreatmentDate") as TextBox;
						String[] strTreatmentDate = txtTreatmentDate.Text.Split('/');
						TextBox txtTreatmentDetails = item.FindControl("txtTreatmentDetails") as TextBox;
						TextBox txtPhysicianInfo = item.FindControl("txtPhysicianInfo") as TextBox;

                        lstTreatmentHistory.Add(new TreatmentInfo
                        {
                            AccountCreationStepCode = Int32.Parse(hdnAccountCreationStep.Value),
                            AccountId = hdnAccountId.Value,
                            ContactId = hdnContactId.Value,
                            Name = ddlPatientName.SelectedValue,
                            HouseholdRole = ddlHouseholdRole.SelectedValue,
                            TreatmentDate = new DateTime(Int32.Parse(strTreatmentDate[2]), Int32.Parse(strTreatmentDate[0]), Int32.Parse(strTreatmentDate[1])),
                            TreatmentDescription = txtTreatmentDetails.Text,
                            PhysicianInfo = txtPhysicianInfo.Text
                        });

						//if (hdnMedicalTreatmentId.Value == "")
						//{
							SForce.tmp_Medical_History__c tmpTreatmentRecord = new SForce.tmp_Medical_History__c();

							tmpTreatmentRecord.cmm_Account_Creation_Step_Code__c = Int32.Parse(hdnAccountCreationStep.Value);
							tmpTreatmentRecord.cmm_Account_Creation_Step_Code__cSpecified = true;
							tmpTreatmentRecord.cmm_Account__c = hdnAccountId.Value;
							tmpTreatmentRecord.cmm_Contact__c = hdnContactId.Value;
							tmpTreatmentRecord.cmm_Name__c = ddlPatientName.SelectedValue;
							tmpTreatmentRecord.cmm_Household_Role__c = ddlHouseholdRole.SelectedValue;
							tmpTreatmentRecord.cmm_Treatment_Date__c = new DateTime(Int32.Parse(strTreatmentDate[2]),
																					Int32.Parse(strTreatmentDate[0]), 
																					Int32.Parse(strTreatmentDate[1]));
							tmpTreatmentRecord.cmm_Treatment_Date__cSpecified = true;
							tmpTreatmentRecord.cmm_Treatment_Description__c = txtTreatmentDetails.Text;
							tmpTreatmentRecord.cmm_Physician_Info__c = txtPhysicianInfo.Text;

							SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { tmpTreatmentRecord });

							if (saveResults[0].success)
							{

							}
						//}
						//if (hdnMedicalTreatmentId.Value != "")
						//{
						//	SForce.tmp_Medical_History__c tmpTreatmentRecord = new SForce.tmp_Medical_History__c();

						//	tmpTreatmentRecord.Id = hdnMedicalTreatmentId.Value;
						//	tmpTreatmentRecord.cmm_Account__c = hdnAccountId.Value;
						//	tmpTreatmentRecord.cmm_Contact__c = hdnContactId.Value;
						//	tmpTreatmentRecord.cmm_Name__c = ddlPatientName.SelectedValue;
						//	tmpTreatmentRecord.cmm_Household_Role__c = ddlHouseholdRole.SelectedValue;
						//	tmpTreatmentRecord.cmm_Treatment_Date__c = new DateTime(Int32.Parse(strTreatmentDate[2]),
						//															Int32.Parse(strTreatmentDate[0]),
						//															Int32.Parse(strTreatmentDate[1]));
						//	tmpTreatmentRecord.cmm_Treatment_Date__cSpecified = true;
						//	tmpTreatmentRecord.cmm_Treatment_Description__c = txtTreatmentDetails.Text;
						//	tmpTreatmentRecord.cmm_Physician_Info__c = txtPhysicianInfo.Text;

						//	SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { tmpTreatmentRecord });

						//	if (updateResults[0].success)
						//	{

						//	}
						//}

						//lstTreatmentHistory.Add(new TreatmentInfo
						//{
						//	AccountCreationStepCode = 7,
						//	AccountId = hdnAccountId.Value,
						//	ContactId = hdnContactId.Value,
						//	Id = hdnMedicalTreatmentId.Value,
						//	Name = ddlPatientName.SelectedValue,
						//	HouseholdRole = ddlHouseholdRole.SelectedValue,
						//	TreatmentDate = new DateTime(Int32.Parse(strTreatmentDate[2]), Int32.Parse(strTreatmentDate[0]), Int32.Parse(strTreatmentDate[1])),
						//	TreatmentDescription = txtTreatmentDetails.Text,
						//	PhysicianInfo = txtPhysicianInfo.Text
						//});
					}




       //                 if (hdnMedicalTreatmentId.Value != null &&
							//hdnAccountId.Value != null &&
							//hdnContactId != null &&
							//ddlPatientName.SelectedValue != null &&
       //                     ddlHouseholdRole.SelectedValue != null &&
       //                     txtTreatmentDate.Text != null &&
       //                     txtTreatmentDetails.Text != null &&
       //                     txtPhysicianInfo.Text != null)
       //                 {
       //                     String strQueryForPatientId = "select Id from Contact where cmm_Household__r.Id = '" + strAccountId + "' and " +
       //                                                   "Name = '" + ddlPatientName.SelectedValue + "' and " +
       //                                                   "cmm_Household_Role__c = '" + ddlHouseholdRole.SelectedValue + "'";
       //                     SForce.QueryResult qrPatientId = Sfdcbinding.query(strQueryForPatientId);

       //                     if (qrPatients.size > 0)
       //                     {
       //                         SForce.Contact ctPatientId = qrPatientId.records[0] as SForce.Contact;
       //                         //HouseholdRoles? role = null;

       //                         //if (ddlHouseholdRole.SelectedValue == "Head of Household") role = HouseholdRoles.Primary;
       //                         //if (ddlHouseholdRole.SelectedValue == "Spouse") role = HouseholdRoles.Spouse;
       //                         //if (ddlHouseholdRole.SelectedValue == "Child") role = HouseholdRoles.Child;

       //                         int nYear = 0;
       //                         int nDay = 0;
       //                         int nMonth = 0;

       //                         if (txtTreatmentDate.Text != String.Empty)
       //                         {
       //                             String[] strTreatmentDate = txtTreatmentDate.Text.Split('/');
       //                             nMonth = Int32.Parse(strTreatmentDate[0]);
       //                             nDay = Int32.Parse(strTreatmentDate[1]);
       //                             nYear = Int32.Parse(strTreatmentDate[2]);
       //                         }
       //                         DateTime dtTreatmentDate = new DateTime(nYear, nMonth, nDay);

							//	lstTreatmentHistory.Add(new TreatmentInfo
							//	{
							//		AccountCreationStepCode = 7,
							//		AccountId = strAccountId,
							//		ContactId = ctPatientId.Id,
							//		Name = ddlPatientName.SelectedValue,
							//		HouseholdRole = ddlHouseholdRole.SelectedValue,
							//		TreatmentDate = dtTreatmentDate,
							//		TreatmentDescription = txtTreatmentDetails.Text,
							//		PhysicianInfo = txtPhysicianInfo.Text
							//	});
       //                      }

        //                    lstTreatmentHistory.Add(new TreatmentInfo
        //                    {
								//Id = hdnMedicalTreatmentId.Value,
        //                        Name = ddlPatientName.SelectedValue,
        //                        HouseholdRole = ddlHouseholdRole.SelectedValue,
        //                        TreatmentDate = txtTreatmentDate.Text,
        //                        TreatmentDescription = txtTreatmentDetails.Text,
        //                        PhysicianInfo = txtPhysicianInfo.Text
        //                    });
                        //}
                    //}

                    //Session["MedicalHistory"] = lstMedicalHistory;
                    Session["TreatmentHistory"] = lstTreatmentHistory;

                    // Save both lstDiseaseRecord and lstMedicalHistory to salesforce for incomplete application

                    //Session["DiseaseRecord"] = lstDiseaseRecord;

                    List<SForce.tmp_Disease_Record__c> lstTmpDiseaseRecord = new List<SForce.tmp_Disease_Record__c>();
                    foreach (DiseaseRecord record in lstDiseaseRecord)
                    {
                        SForce.tmp_Disease_Record__c tmpDiseaseRecord = new SForce.tmp_Disease_Record__c();

                        tmpDiseaseRecord.cmm_Account_Creation_Step_Code__c = record.AccountCreationStepCode;
                        tmpDiseaseRecord.cmm_Account_Creation_Step_Code__cSpecified = true;

                        tmpDiseaseRecord.cmm_Account__c = record.AccountId;
                        tmpDiseaseRecord.cmm_Contact__c = record.ContactId;
                        tmpDiseaseRecord.cmm_Name__c = record.Name;

                        tmpDiseaseRecord.Treated_in_last_12_months__c = record.DiagnosedDisease[0];
                        tmpDiseaseRecord.Treated_in_last_12_months__cSpecified = true;

                        tmpDiseaseRecord.Diagnosed_with_Cardiovascular_issues__c = record.DiagnosedDisease[1];
                        tmpDiseaseRecord.Diagnosed_with_Cardiovascular_issues__cSpecified = true;

                        tmpDiseaseRecord.Diagnosed_with_Allergy_Respiratory__c = record.DiagnosedDisease[2];
                        tmpDiseaseRecord.Diagnosed_with_Allergy_Respiratory__cSpecified = true;

                        tmpDiseaseRecord.Arthritis_back_nervous_system__c = record.DiagnosedDisease[3];
                        tmpDiseaseRecord.Arthritis_back_nervous_system__cSpecified = true;

                        tmpDiseaseRecord.Eyes_nose_ears_hands_feet_conditions__c = record.DiagnosedDisease[4];
                        tmpDiseaseRecord.Eyes_nose_ears_hands_feet_conditions__cSpecified = true;

                        tmpDiseaseRecord.Stomach_liver_colon_kidney_conditions__c = record.DiagnosedDisease[5];
                        tmpDiseaseRecord.Stomach_liver_colon_kidney_conditions__cSpecified = true;

                        tmpDiseaseRecord.Thyroid_tumor_cancer_medical_conditions__c = record.DiagnosedDisease[6];
                        tmpDiseaseRecord.Thyroid_tumor_cancer_medical_conditions__cSpecified = true;

                        tmpDiseaseRecord.Prostate_or_female_reprdct_conditions__c = record.DiagnosedDisease[7];
                        tmpDiseaseRecord.Prostate_or_female_reprdct_conditions__cSpecified = true;

                        tmpDiseaseRecord.Congenital_disease_or_other_condition__c = record.DiagnosedDisease[8];
                        tmpDiseaseRecord.Congenital_disease_or_other_condition__cSpecified = true;

                        lstTmpDiseaseRecord.Add(tmpDiseaseRecord);
                    }

                    foreach (SForce.tmp_Disease_Record__c record in lstTmpDiseaseRecord)
                    {
						String strQueryForTmpDiseaseRecord = "select Id from tmp_Disease_Record__c where cmm_Account__c = '" + record.cmm_Account__c + "' " +
                                                             "and cmm_Contact__c = '" + record.cmm_Contact__c + "'";

                        SForce.QueryResult qrTmpDiseaseRecord = Sfdcbinding.query(strQueryForTmpDiseaseRecord);

                        if (qrTmpDiseaseRecord.size == 0)
                        {
                            SForce.SaveResult[] srTmpDiseaseRecords = Sfdcbinding.create(new SForce.sObject[] { record });

                            if (srTmpDiseaseRecords[0].success)
                            {
                                // Temporary object is saved successfully
                            }

                        }
                        else if (qrTmpDiseaseRecord.size > 0)
                        {
                            SForce.tmp_Disease_Record__c tmpRecord = qrTmpDiseaseRecord.records[0] as SForce.tmp_Disease_Record__c;

                            record.Id = tmpRecord.Id;

                            SForce.SaveResult[] srUpdateTmpDiseaseRecords = Sfdcbinding.update(new SForce.sObject[] { record });

                            if (srUpdateTmpDiseaseRecords[0].success)
                            {

                            }
                        }

                    }

                    //Session["MedicalHistory"] = lstMedicalHistory;

					//foreach (ListViewItem item in lvTreatmentHistory.Items)
					//{
					//	HiddenField hdnTreatmentRecordId = item.FindControl("hdnMedicalTreatmentId") as HiddenField;
					//	DropDownList ddlPatientName = item.FindControl("ddlPatientName") as DropDownList;
					//	DropDownList ddlHouseholdRole = item.FindControl("ddlHouseholdRole") as DropDownList;
					//	TextBox txtTreatmentDate = item.FindControl("txtTreatmentDate") as TextBox;
					//	TextBox txtTreatmentDetails = item.FindControl("txtTreamtmentDetails") as TextBox;
					//	TextBox txtPhysicianInfo = item.FindControl("txtPhysicianInfo") as TextBox;

					//	if (hdnTreatmentRecordId.Value == null)
					//	{
					//		SForce.tmp_Medical_History__c tmpTreatmentRecord = new SForce.tmp_Medical_History__c();

					//		tmpTreatmentRecord.cmm_Account__c = strAccountId;
							
					//	}
					//}



					// This section of code might be deleted
                    //List<SForce.tmp_Medical_History__c> lstTmpMedicalHistory = new List<SForce.tmp_Medical_History__c>();
      //              foreach (MedicalInfo info in lstMedicalHistory)
      //              {
      //                  SForce.tmp_Medical_History__c tmpInfo = new SForce.tmp_Medical_History__c();

      //                  tmpInfo.cmm_Account_Creation_Step_Code__c = info.AccountCreationStepCode;
      //                  tmpInfo.cmm_Account_Creation_Step_Code__cSpecified = true;

      //                  tmpInfo.cmm_Account__c = info.AccountId;
						//tmpInfo.cmm_Contact__c = info.ContactId;
						//tmpInfo.Id = info.Id;
      //                  tmpInfo.cmm_Name__c = info.Name;
      //                  //tmpInfo.cmm_Household_Role__c = info.HouseholdRole.ToString();
      //                  switch (info.HouseholdRole)
      //                  {
      //                      case HouseholdRoles.Primary:
      //                          tmpInfo.cmm_Household_Role__c = "Head of Household";
      //                          break;
      //                      case HouseholdRoles.Spouse:
      //                          tmpInfo.cmm_Household_Role__c = "Spouse";
      //                          break;
      //                      case HouseholdRoles.Child:
      //                          tmpInfo.cmm_Household_Role__c = "Child";
      //                          break;
      //                  }

      //                  //String[] strTreatmentDate = info.TreatmentDate.Split('/');
      //                  //int nMonth = Int32.Parse(strTreatmentDate[0]);
      //                  //int nDay = Int32.Parse(strTreatmentDate[1]);
      //                  //int nYear = Int32.Parse(strTreatmentDate[2]);

      //                  //tmpInfo.cmm_Treatment_Date__c = new DateTime(nYear, nMonth, nDay);
      //                  tmpInfo.cmm_Treatment_Date__c = info.TreatmentDate;
      //                  tmpInfo.cmm_Treatment_Date__cSpecified = true;
      //                  tmpInfo.cmm_Treatment_Description__c = info.TreatmentDescription;
      //                  tmpInfo.cmm_Physician_Info__c = info.PhysicianInfo;

      //                  lstTmpMedicalHistory.Add(tmpInfo);
      //              }

       //             foreach (SForce.tmp_Medical_History__c med_info in lstTmpMedicalHistory)
       //             {
       //                 String strQueryForTmpMedicalHistory = "select Id from tmp_Medical_History__c where cmm_Account__c = '" + med_info.cmm_Account__c + "' " +
       //                                                       "and cmm_Contact__c = '" + med_info.cmm_Contact__c + "'";

       //                 SForce.QueryResult qrTmpMedicalHistory = Sfdcbinding.query(strQueryForTmpMedicalHistory);

       //                 if (qrTmpMedicalHistory.size == 0)
       //                 {
       //                     SForce.SaveResult[] srTmpMedInfo = Sfdcbinding.create(new SForce.sObject[] { med_info });

       //                     if (srTmpMedInfo[0].success)
       //                     {
       //                         // Temporary object is save successfully
       //                     }
       //                 }
       //                 else if (qrTmpMedicalHistory.size > 0)
       //                 {
							//med_info.Id = qrTmpMedicalHistory.records[0].Id;

       //                     SForce.SaveResult[] srUpdateTmpMedInfo = Sfdcbinding.update(new SForce.sObject[] { med_info });

       //                     if (srUpdateTmpMedInfo[0].success)
       //                     {

       //                     }
       //                 }
       //             }
                }
            }

            SForce.Account acctPrimary = new SForce.Account();
            acctPrimary.Id = strAccountId;
            acctPrimary.cmm_Account_Creation_Step_Code__c = "7";

            SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

            if (srAccount[0].success)
            {
                Session["PreviousPage"] = "HealthHistory";
                Response.Redirect("~/Agreement.aspx");
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Session["PreviousPage"] = "HealthHistory";
            //Response.Redirect("~/Agreement.aspx");
            Response.Redirect("~/PaymentInfo.aspx");
        }


	}
}