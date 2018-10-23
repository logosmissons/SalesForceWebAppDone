using System;
using System.Collections.Generic;

//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Owin;

namespace SalesForceWebApp
{

    //public class TreatmentInfo
    //{
    //    //public int nIndex { get; set; }
    //    public String Name { get; set; }
    //    //public DateTime? dtTreatmentDate { get; set; }
    //    public String TreatmentDate { get; set; }
    //    public String TreatmentDescription { get; set; }
    //    public String PhysicianInfo { get; set; }
    //}

    //public class PatientNames
    //{
    //    public String Name { get; set; }
    //}

    //public class DiseaseRecord
    //{
    //    public String Name { get; set; }
    //    public Boolean[] DiagnosedDisease { get; set; }
    //}

    public partial class Agreement : System.Web.UI.Page
    {

        private String strAccountId = null;
        private String strPrimaryId = null;
        //private String strContactId = null;
        private int nNumberOfFamilyMembers = 0;
        private String strPrimaryEmail = null;
        private Boolean bApplicationErrorFlag = false;
        private Boolean bApplicantErrorFlag = false;
        private Boolean bMedicalHistoryErrorFlag = false;

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
        List<PatientNames> lstPatientNames = new List<PatientNames>();
        List<DiseaseRecord> lstDiseaseRecord = new List<DiseaseRecord>();
        List<MemberSmokingDrugAlcohol> lstMemberSDA = new List<MemberSmokingDrugAlcohol>();
        //List<MedicalInfo> lstMedicalHistory = new List<MedicalInfo>();

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected int NumberOfFamilyMembers()
        {
            String strQueryForHouseholdMembers = "select Name from Contact where cmm_Household__r.Id = '" + strAccountId + "'";

            SForce.QueryResult qrHouseholdMembers = Sfdcbinding.query(strQueryForHouseholdMembers);

            return qrHouseholdMembers.size;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //InitializedSfdcbinding();

            if ((String)Session["AccountId"] != null) strAccountId = (String)Session["AccountId"];
            if ((String)Session["Email"] != null) strPrimaryEmail = (String)Session["Email"];
            if ((String)Session["ContactId"] != null) strPrimaryId = (String)Session["ContactId"];
            else
            {
                String strPrimaryEmail = (String)Session["Email"];
                String strQueryForPrimaryId = "select Id from Contact where Email = '" + strPrimaryEmail + "'";    // 11/17/17 strPrimaryId should be replaced with Email

                SForce.QueryResult qrPrimaryId = Sfdcbinding.query(strQueryForPrimaryId);

                if (qrPrimaryId.size > 0)
                {
                    SForce.Contact ctPrimaryId = qrPrimaryId.records[0] as SForce.Contact;

                    strPrimaryId = ctPrimaryId.Id;
                }
            }

            // in the case of incomplete application, retrieve lstDiseaseRecord from salesforce
            //if (!IsPostBack)
            //{
                if ((List<DiseaseRecord>)Session["DiseaseRecord"] != null) lstDiseaseRecord = Session["DiseaseRecord"] as List<DiseaseRecord>;
                else
                {
                    String strQueryForDiseaseRecord = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Name__c, " +
                                                      "Treated_in_last_12_months__c, Diagnosed_with_Cardiovascular_issues__c, Diagnosed_with_Allergy_Respiratory__c, " +
                                                      "Arthritis_back_nervous_system__c, Eyes_nose_ears_hands_feet_conditions__c, Stomach_liver_colon_kidney_conditions__c, " +
                                                      "Thyroid_tumor_cancer_medical_conditions__c, Prostate_or_female_reprdct_conditions__c, Congenital_disease_or_other_condition__c, " +
                                                      "cmm_Contact__r.cmm_Household_Role__c " +
                                                      "from tmp_Disease_Record__c where cmm_Account__c = '" + strAccountId + "'";

                    SForce.QueryResult qrTempDiseaseRecord = Sfdcbinding.query(strQueryForDiseaseRecord);

                    if (qrTempDiseaseRecord.size > 0)
                    {
                        foreach (SForce.tmp_Disease_Record__c record in qrTempDiseaseRecord.records)
                        {

                            lstDiseaseRecord.Add(new DiseaseRecord
                            {
                                AccountCreationStepCode = (int)record.cmm_Account_Creation_Step_Code__c,
                                AccountId = record.cmm_Account__c,
                                ContactId = record.cmm_Contact__c,
                                Name = record.cmm_Name__c,
                                HouseholdRole = record.cmm_Contact__r.cmm_Household_Role__c,
                                DiagnosedDisease = new Boolean[9] { (Boolean)record.Treated_in_last_12_months__c,
                                                                (Boolean)record.Diagnosed_with_Cardiovascular_issues__c,
                                                                (Boolean)record.Diagnosed_with_Allergy_Respiratory__c,
                                                                (Boolean)record.Arthritis_back_nervous_system__c,
                                                                (Boolean)record.Eyes_nose_ears_hands_feet_conditions__c,
                                                                (Boolean)record.Stomach_liver_colon_kidney_conditions__c,
                                                                (Boolean)record.Thyroid_tumor_cancer_medical_conditions__c,
                                                                (Boolean)record.Prostate_or_female_reprdct_conditions__c,
                                                                (Boolean)record.Congenital_disease_or_other_condition__c }
                            });
                        }
                    }
                }

                // in the case of incomplete application, retrieve the number of family members from salesforce
                if (Session["NumberOfFamilyMembers"] != null)
                {
                    nNumberOfFamilyMembers = (int)Session["NumberOfFamilyMembers"];
                }
                else
                {
                    nNumberOfFamilyMembers = NumberOfFamilyMembers();
                }

                // in the case of incomplete application, retrieve lstMemberSDA from salesforce
                //if ((List<MemberSmokingDrugAlcohol>)Session["MemberSmokingDrugAlcohol"] != null) lstMemberSDA = Session["MemberSmokingDrugAlcohol"] as List<MemberSmokingDrugAlcohol>;
                //else
                //{
                    String strQueryForMemberSDA = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Name__c, cmm_Household_Role__c, " +
                                                  "cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c " +
                                                  "from tmp_SmokingDrugAlcohol__c where cmm_Account__r.Id = '" + strAccountId + "'";

                    SForce.QueryResult qrMemberSDA = Sfdcbinding.query(strQueryForMemberSDA);

                    if (qrMemberSDA.size > 0)
                    {
                        foreach (SForce.tmp_SmokingDrugAlcohol__c sda in qrMemberSDA.records)
                        {
                            MemberSmokingDrugAlcohol memberSDA = new MemberSmokingDrugAlcohol();

                            memberSDA.AccountCreationStepCode = (int)sda.cmm_Account_Creation_Step_Code__c;
                            memberSDA.AccountId = sda.cmm_Account__c;
                            switch (sda.cmm_Household_Role__c)
                            {
                                case "Head of Household":
                                    memberSDA.HouseholdRole = HouseholdRoles.Primary;
                                    break;
                                case "Spouse":
                                    memberSDA.HouseholdRole = HouseholdRoles.Spouse;
                                    break;
                                case "Child":
                                    memberSDA.HouseholdRole = HouseholdRoles.Child;
                                    break;
                            }
                            memberSDA.ContactId = sda.cmm_Contact__c;
                            memberSDA.Name = sda.cmm_Name__c;
                            memberSDA.bCurrentSmoker = (sda.cmm_bCurrentSmoker__c == "Yes");
                            memberSDA.bFormerSmoker = (sda.cmm_bFormerSmoker__c == "Yes");
                            memberSDA.bCurrentDrug = (sda.cmm_bCurrentDrug__c == "Yes");
                            memberSDA.bFormerDrug = (sda.cmm_bFormerDrug__c == "Yes");
                            memberSDA.bAlcohol = (sda.cmm_bAlcohol__c == "Yes");

                            lstMemberSDA.Add(memberSDA);
                        }
                    }
                //}

                // in the case of incomplete application, retrieve lstMedicalHistory from salesforce
                if ((List<TreatmentInfo>)Session["TreatmentHistory"] != null) lstTreatmentHistory = Session["TreatmentHistory"] as List<TreatmentInfo>;
                else
                {
                    String strQueryForTreatmentHistory = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Household_Role__c, " +
                                                         "cmm_Name__c, cmm_Treatment_Date__c, cmm_Treatment_Description__c, cmm_Physician_Info__c " +
                                                         "from tmp_Medical_History__c where cmm_Account__r.Id = '" + strAccountId + "'";

                    SForce.QueryResult qrTmpTreatmentHistory = Sfdcbinding.query(strQueryForTreatmentHistory);

                    if (qrTmpTreatmentHistory.size > 0)
                    {
                        foreach (SForce.tmp_Medical_History__c record in qrTmpTreatmentHistory.records)
                        {
                            TreatmentInfo treatment = new TreatmentInfo();

                            treatment.AccountCreationStepCode = (int)record.cmm_Account_Creation_Step_Code__c;
                            treatment.AccountId = record.cmm_Account__c;
                            treatment.ContactId = record.cmm_Contact__c;
                            treatment.Name = record.cmm_Name__c;
                            treatment.HouseholdRole = record.cmm_Household_Role__c;

                            //int nYear = record.cmm_Treatment_Date__c.Value.Year;
                            //int nMonth = record.cmm_Treatment_Date__c.Value.Month;
                            //int nDay = record.cmm_Treatment_Date__c.Value.Day;

                            treatment.TreatmentDate = new DateTime(record.cmm_Treatment_Date__c.Value.Year,
                                                                   record.cmm_Treatment_Date__c.Value.Month,
                                                                   record.cmm_Treatment_Date__c.Value.Day);


                            treatment.TreatmentDescription = record.cmm_Treatment_Description__c;
                            treatment.PhysicianInfo = record.cmm_Physician_Info__c;

                            lstTreatmentHistory.Add(treatment);
                        }
                    }
                }
            //}
   //         if ((List<DiseaseRecord>)Session["DiseaseRecord"] != null) lstDiseaseRecord = Session["DiseaseRecord"] as List<DiseaseRecord>;
   //         else
   //         {
   //             String strQueryForDiseaseRecord = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Name__c, " +
   //                                               "Treated_in_last_12_months__c, Diagnosed_with_Cardiovascular_issues__c, Diagnosed_with_Allergy_Respiratory__c, " +
   //                                               "Arthritis_back_nervous_system__c, Eyes_nose_ears_hands_feet_conditions__c, Stomach_liver_colon_kidney_conditions__c, " +
   //                                               "Thyroid_tumor_cancer_medical_conditions__c, Prostate_or_female_reprdct_conditions__c, Congenital_disease_or_other_condition__c, " +
   //                                               "cmm_Contact__r.cmm_Household_Role__c " +
   //                                               "from tmp_Disease_Record__c where cmm_Account__c = '" + strAccountId + "'";

   //             SForce.QueryResult qrTempDiseaseRecord = Sfdcbinding.query(strQueryForDiseaseRecord);

   //             if (qrTempDiseaseRecord.size > 0)
   //             {
   //                 foreach (SForce.tmp_Disease_Record__c record in qrTempDiseaseRecord.records)
   //                 {

   //                     lstDiseaseRecord.Add(new DiseaseRecord
   //                     {
   //                         AccountCreationStepCode = (int)record.cmm_Account_Creation_Step_Code__c,
   //                         AccountId = record.cmm_Account__c,
   //                         ContactId = record.cmm_Contact__c,
   //                         Name = record.cmm_Name__c,
   //                         HouseholdRole = record.cmm_Contact__r.cmm_Household_Role__c,
   //                         DiagnosedDisease = new Boolean[9] { (Boolean)record.Treated_in_last_12_months__c,
   //                                                             (Boolean)record.Diagnosed_with_Cardiovascular_issues__c,
   //                                                             (Boolean)record.Diagnosed_with_Allergy_Respiratory__c,
   //                                                             (Boolean)record.Arthritis_back_nervous_system__c,
   //                                                             (Boolean)record.Eyes_nose_ears_hands_feet_conditions__c,
   //                                                             (Boolean)record.Stomach_liver_colon_kidney_conditions__c,
   //                                                             (Boolean)record.Thyroid_tumor_cancer_medical_conditions__c,
   //                                                             (Boolean)record.Prostate_or_female_reprdct_conditions__c,
   //                                                             (Boolean)record.Congenital_disease_or_other_condition__c }
   //                     });
   //                 }
   //             }
   //         }

   //         // in the case of incomplete application, retrieve the number of family members from salesforce
   //         if (Session["NumberOfFamilyMembers"] != null)
   //         {
   //             nNumberOfFamilyMembers = (int)Session["NumberOfFamilyMembers"];
   //         }
   //         else
   //         {
   //             nNumberOfFamilyMembers = NumberOfFamilyMembers();
   //         }

   //         // in the case of incomplete application, retrieve lstMemberSDA from salesforce
   //         if ((List<MemberSmokingDrugAlcohol>)Session["MemberSmokingDrugAlcohol"] != null) lstMemberSDA = Session["MemberSmokingDrugAlcohol"] as List<MemberSmokingDrugAlcohol>;
   //         else
   //         {
   //             String strQueryForMemberSDA = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Name__c, cmm_Household_Role__c, " +
   //                                           "cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c " +
   //                                           "from tmp_SmokingDrugAlcohol__c where cmm_Account__r.Id = '" + strAccountId + "'";

   //             SForce.QueryResult qrMemberSDA = Sfdcbinding.query(strQueryForMemberSDA);

   //             if (qrMemberSDA.size > 0)
   //             {
   //                 foreach (SForce.tmp_SmokingDrugAlcohol__c sda in qrMemberSDA.records)
   //                 {
   //                     MemberSmokingDrugAlcohol memberSDA = new MemberSmokingDrugAlcohol();

   //                     memberSDA.AccountCreationStepCode = (int)sda.cmm_Account_Creation_Step_Code__c;
   //                     memberSDA.AccountId = sda.cmm_Account__c;
   //                     switch (sda.cmm_Household_Role__c)
   //                     {
   //                         case "Head of Household":
   //                             memberSDA.HouseholdRole = HouseholdRoles.Primary;
   //                             break;
   //                         case "Spouse":
   //                             memberSDA.HouseholdRole = HouseholdRoles.Spouse;
   //                             break;
   //                         case "Child":
   //                             memberSDA.HouseholdRole = HouseholdRoles.Child;
   //                             break;
   //                     }
   //                     memberSDA.ContactId = sda.cmm_Contact__c;
   //                     memberSDA.Name = sda.cmm_Name__c;
   //                     memberSDA.bCurrentSmoker = (sda.cmm_bCurrentSmoker__c == "Yes");
   //                     memberSDA.bFormerSmoker = (sda.cmm_bFormerSmoker__c == "Yes");
   //                     memberSDA.bCurrentDrug = (sda.cmm_bCurrentDrug__c == "Yes");
   //                     memberSDA.bFormerDrug = (sda.cmm_bFormerDrug__c == "Yes");
   //                     memberSDA.bAlcohol = (sda.cmm_bAlcohol__c == "Yes");

   //                     lstMemberSDA.Add(memberSDA);
   //                 }
   //             }
   //         }

			//// in the case of incomplete application, retrieve lstMedicalHistory from salesforce
			//if ((List<TreatmentInfo>)Session["TreatmentHistory"] != null) lstTreatmentHistory = Session["TreatmentHistory"] as List<TreatmentInfo>;
			//else
			//{
			//	String strQueryForTreatmentHistory = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Household_Role__c, " +
			//									     "cmm_Name__c, cmm_Treatment_Date__c, cmm_Treatment_Description__c, cmm_Physician_Info__c " +
			//									     "from tmp_Medical_History__c where cmm_Account__r.Id = '" + strAccountId + "'";

			//	SForce.QueryResult qrTmpTreatmentHistory = Sfdcbinding.query(strQueryForTreatmentHistory);

			//	if (qrTmpTreatmentHistory.size > 0)
			//	{
			//		foreach (SForce.tmp_Medical_History__c record in qrTmpTreatmentHistory.records)
			//		{
			//			TreatmentInfo treatment = new TreatmentInfo();

			//			treatment.AccountCreationStepCode = (int)record.cmm_Account_Creation_Step_Code__c;
			//			treatment.AccountId = record.cmm_Account__c;
			//			treatment.ContactId = record.cmm_Contact__c;
			//			treatment.Name = record.cmm_Name__c;
			//			treatment.HouseholdRole = record.cmm_Household_Role__c;

			//			//int nYear = record.cmm_Treatment_Date__c.Value.Year;
			//			//int nMonth = record.cmm_Treatment_Date__c.Value.Month;
			//			//int nDay = record.cmm_Treatment_Date__c.Value.Day;

			//			treatment.TreatmentDate = new DateTime(record.cmm_Treatment_Date__c.Value.Year,
			//												   record.cmm_Treatment_Date__c.Value.Month, 
			//												   record.cmm_Treatment_Date__c.Value.Day);


			//			treatment.TreatmentDescription = record.cmm_Treatment_Description__c;
			//			treatment.PhysicianInfo = record.cmm_Physician_Info__c;

			//			lstTreatmentHistory.Add(treatment);
			//		}
			//	}
			//}
		}

        protected void Page_Init(object sender, EventArgs e)
        {
            //if (Session["DiseaseRecord"] != null) lstDiseaseRecord = Session["DiseaseRecord"] as List<DiseaseRecord>;

            //if (Session["Email"] != null) strPrimaryEmail = Session["Email"] as String;

            //if (Session["NumberOfFamilyMembers"] != null) nNumberOfFamilyMembers = (int)Session["NumberOfFamilyMembers"];
            InitializedSfdcbinding();
        }

        protected void btnSubmitApplication_Click(object sender, EventArgs e)
        {
            //SForce.Applicant__c applicant = new SForce.Applicant__c();

            SForce.Application__c application = new SForce.Application__c();

            if (chkAgreementItem1.Checked &&
                chkAgreementItem2.Checked &&
                chkAgreementItem3.Checked &&
                chkAgreementItem4.Checked &&
                chkAgreementItem5.Checked &&
                chkAgreementItem6.Checked &&
                chkAgreementItem7.Checked &&
                chkAgreementItem8.Checked &&
                chkAgreementItem9.Checked &&
                chkAgreementItem10.Checked &&
                chkAgreementItem11.Checked &&
                chkAgreementItem12.Checked &&
                chkAgreementItem13.Checked &&
                chkAgreementItem14.Checked &&
                chkAgreementItem15.Checked &&
                chkAgreementItem16.Checked &&
                chkAgreementItem17.Checked &&
                chkAgreementItem18.Checked &&
                chkAgreementItem19.Checked &&
                chkConfirmation.Checked)
            {

                application.Abstain_from_tobacco_drugs_etc__c = true;
                application.Abstain_from_tobacco_drugs_etc__cSpecified = true;

                application.cmm_Account_In_good_standing__c = true;
                application.cmm_Account_In_good_standing__cSpecified = true;

                application.cmm_After_90_days_waiting_period__c = true;
                application.cmm_After_90_days_waiting_period__cSpecified = true;

                application.Biblical_living_and_church_attend__c = true;
                application.Biblical_living_and_church_attend__cSpecified = true;

                application.c4g_Application_Form_Received__c = true;
                application.c4g_Application_Form_Received__cSpecified = true;

                application.c4g_Payment_Authorization_Form_Received__c = true;
                application.c4g_Payment_Authorization_Form_Received__cSpecified = true;

                application.CMM_is_not_insurance__c = true;
                application.CMM_is_not_insurance__cSpecified = true;

                application.Desire_to_share_needs_as_in_Bible__c = true;
                application.Desire_to_share_needs_as_in_Bible__cSpecified = true;

                application.cmm_Eligibility_determined_by_guidelines__c = true;
                application.cmm_Eligibility_determined_by_guidelines__cSpecified = true;

                application.Have_read_and_agree__c = true;
                application.Have_read_and_agree__cSpecified = true;

                application.How_monthly_gift_will_be_used__c = true;
                application.How_monthly_gift_will_be_used__cSpecified = true;

                application.Medical_expense_over_10K_fee_up__c = true;
                application.Medical_expense_over_10K_fee_up__cSpecified = true;

                application.Member_responsible_for_bill_payment__c = true;
                application.Member_responsible_for_bill_payment__cSpecified = true;

                application.Monthly_fee_paid_by_10th_of_month__c = true;
                application.Monthly_fee_paid_by_10th_of_month__cSpecified = true;

                application.Must_notify_CMM_for_medical_needs__c = true;
                application.Must_notify_CMM_for_medical_needs__cSpecified = true;

                application.Must_submit_needs_processing_forms__c = true;
                application.Must_submit_needs_processing_forms__cSpecified = true;

                application.Needs_may_be_rejected_by_guidelines__c = true;
                application.Needs_may_be_rejected_by_guidelines__cSpecified = true;

                application.Needs_over_150K_shared_by_all__c = true;
                application.Needs_over_150K_shared_by_all__cSpecified = true;

                application.cmm_No_legal_contract_obligation__c = true;
                application.cmm_No_legal_contract_obligation__cSpecified = true;

                application.No_legal_right_to_money_wont_sue__c = true;
                application.No_legal_right_to_money_wont_sue__cSpecified = true;

                application.No_previous_bills_shared__c = true;
                application.No_previous_bills_shared__cSpecified = true;

                application.cmm_Portion_of_gift_used_to_support_CMM__c = true;
                application.cmm_Portion_of_gift_used_to_support_CMM__cSpecified = true;

                application.Register_as_self_payer_at_facility__c = true;
                application.Register_as_self_payer_at_facility__cSpecified = true;

                application.Will_not_use_funds_for_non_medical__c = true;
                application.Will_not_use_funds_for_non_medical__cSpecified = true;

                application.Will_request_self_pay_discount__c = true;
                application.Will_request_self_pay_discount__cSpecified = true;

                application.X11_Months_Shared_12th_Month_to_CMM__c = true;
                application.X11_Months_Shared_12th_Month_to_CMM__cSpecified = true;


                for (int i = 0; i < lstDiseaseRecord.Count; i++)
                {
                    if (lstDiseaseRecord[i].HouseholdRole == "Head of Household") application.Signature__c = lstDiseaseRecord[i].Name;
                }
                //application.Signature__c = lstDiseaseRecord[0].Name;


                //////////////////////////////////////////////////////////
                // End of questions no longer in use

                DateTime dtToday = DateTime.Today;

                application.Registration_Date__c = dtToday;
                application.Registration_Date__cSpecified = true;

                //String strEmail = (String)Session["Email"];
                String strQueryForPayingMemberId = "select Id from Contact where Email = '" + strPrimaryEmail + "'";
                SForce.QueryResult qrPayingMember = Sfdcbinding.query(strQueryForPayingMemberId);

                String strPayingMemberId = String.Empty;

                if (qrPayingMember.size > 0)
                {
                    SForce.Contact ctPayingMember = (SForce.Contact)qrPayingMember.records[0];
                    strPayingMemberId = (String)ctPayingMember.Id;
                }

                String strQueryForMembershipId = "select Id from Membership__c where Paying_Member__r.Id = '" + strPayingMemberId + "'";

                SForce.QueryResult qrMembershipId = Sfdcbinding.query(strQueryForMembershipId);

                String strMembershipId = String.Empty;

                if (qrMembershipId.size > 0)
                {
                    SForce.Membership__c membership = qrMembershipId.records[0] as SForce.Membership__c;

                    strMembershipId = membership.Id;
                }

                String strApplicationId = String.Empty;

                if (strMembershipId != String.Empty)
                {
                    application.c4g_Membership__c = strMembershipId;

                    SForce.SaveResult[] srApplication = Sfdcbinding.create(new SForce.sObject[] { application });

                    if (srApplication[0].success)
                    {
                        strApplicationId = srApplication[0].id;
                    }
                    else bApplicationErrorFlag = true;
                }

                List<SForce.Applicant__c> lstApplicant = new List<SForce.Applicant__c>();

                for (int nColumn = 0; nColumn < nNumberOfFamilyMembers; nColumn++)
                {
                    SForce.Applicant__c applicant = new SForce.Applicant__c();
                    applicant.Application__c = strApplicationId;
                    applicant.Contact__c = lstDiseaseRecord[nColumn].ContactId;

                    foreach (MemberSmokingDrugAlcohol memberInfo in lstMemberSDA)
                    {
                        if (applicant.Contact__c == memberInfo.ContactId)
                        {
                            applicant.Name = memberInfo.Name;
                            applicant.cmm_Currently_Smoking__c = memberInfo.bCurrentSmoker;
                            applicant.cmm_Currently_Smoking__cSpecified = true;
                            applicant.cmm_Former_Smoker__c = memberInfo.bFormerSmoker;
                            applicant.cmm_Former_Smoker__cSpecified = true;
                            applicant.cmm_Currently_Taking_Narcotic_Drug__c = memberInfo.bCurrentDrug;
                            applicant.cmm_Currently_Taking_Narcotic_Drug__cSpecified = true;
                            applicant.cmm_Formerly_Taking_Narcotic_Drug__c = memberInfo.bFormerDrug;
                            applicant.cmm_Formerly_Taking_Narcotic_Drug__cSpecified = true;
                            applicant.cmm_Drinking_Alcohol__c = memberInfo.bAlcohol;
                            applicant.cmm_Drinking_Alcohol__cSpecified = true;
                        }
                    }

                    for (int nRow = 0; nRow < 9; nRow++)
                    {
                        if (lstDiseaseRecord[nColumn].DiagnosedDisease[nRow] == true)
                        {
                            switch (nRow + 1)
                            {
                                case 1:
                                    applicant.Treated_in_last_12_months__c = true;
                                    applicant.Treated_in_last_12_months__cSpecified = true;
                                    break;
                                case 2:
                                    applicant.Diagnosed_with_Cardiovascular_issues__c = true;
                                    applicant.Diagnosed_with_Cardiovascular_issues__cSpecified = true;
                                    break;
                                case 3:
                                    applicant.Diagnosed_with_Allergy_Respiratory__c = true;
                                    applicant.Diagnosed_with_Allergy_Respiratory__cSpecified = true;
                                    break;
                                case 4:
                                    applicant.Arthritis_back_nervous_system_iss__c = true;
                                    applicant.Arthritis_back_nervous_system_iss__cSpecified = true;
                                    break;
                                case 5:
                                    applicant.Eyes_nose_ears_hands_feet_conditions__c = true;
                                    applicant.Eyes_nose_ears_hands_feet_conditions__cSpecified = true;
                                    break;
                                case 6:
                                    applicant.Stomach_liver_colon_kidney_conditions__c = true;
                                    applicant.Stomach_liver_colon_kidney_conditions__cSpecified = true;
                                    break;
                                case 7:
                                    applicant.Thyroid_tumor_cancer_medical_conditions__c = true;
                                    applicant.Thyroid_tumor_cancer_medical_conditions__cSpecified = true;
                                    break;
                                case 8:
                                    applicant.Prostate_or_female_reprdct_conditions__c = true;
                                    applicant.Prostate_or_female_reprdct_conditions__cSpecified = true;
                                    break;
                                case 9:
                                    applicant.Congenital_disease_or_other_condition__c = true;
                                    applicant.Congenital_disease_or_other_condition__cSpecified = true;
                                    break;
                            }

                        }
                        else
                        {
                            switch (nRow + 1)
                            {
                                case 1:
                                    applicant.Treated_in_last_12_months__c = false;
                                    applicant.Treated_in_last_12_months__cSpecified = true;
                                    break;
                                case 2:
                                    applicant.Diagnosed_with_Cardiovascular_issues__c = false;
                                    applicant.Diagnosed_with_Cardiovascular_issues__cSpecified = true;
                                    break;
                                case 3:
                                    applicant.Diagnosed_with_Allergy_Respiratory__c = false;
                                    applicant.Diagnosed_with_Allergy_Respiratory__cSpecified = true;
                                    break;
                                case 4:
                                    applicant.Arthritis_back_nervous_system_iss__c = false;
                                    applicant.Arthritis_back_nervous_system_iss__cSpecified = true;
                                    break;
                                case 5:
                                    applicant.Eyes_nose_ears_hands_feet_conditions__c = false;
                                    applicant.Eyes_nose_ears_hands_feet_conditions__cSpecified = true;
                                    break;
                                case 6:
                                    applicant.Stomach_liver_colon_kidney_conditions__c = false;
                                    applicant.Stomach_liver_colon_kidney_conditions__cSpecified = true;
                                    break;
                                case 7:
                                    applicant.Thyroid_tumor_cancer_medical_conditions__c = false;
                                    applicant.Thyroid_tumor_cancer_medical_conditions__cSpecified = true;
                                    break;
                                case 8:
                                    applicant.Prostate_or_female_reprdct_conditions__c = false;
                                    applicant.Prostate_or_female_reprdct_conditions__cSpecified = true;
                                    break;
                                case 9:
                                    applicant.Congenital_disease_or_other_condition__c = false;
                                    applicant.Congenital_disease_or_other_condition__cSpecified = true;
                                    break;
                            }
                        }
                    }

                    SForce.SaveResult[] srApplicant = Sfdcbinding.create(new SForce.sObject[] { applicant });

                    if (srApplicant[0].success)
                    {

                    }
                    else
                    {
                        //String errMessage = srApplicant[0].errors[0].message;

                        bApplicantErrorFlag = true;
                    }
                }

                if (bApplicantErrorFlag == false && lstTreatmentHistory.Count == 0)
                {
                    //Session["PreviousPage"] = "Agreement";
                    // delete all temporary record in temporary objects
                    //Response.Redirect("MainForm.aspx");
                }
                //else bApplicantErrorFlag = true;
            }

            //SForce.Medical_History__c[] medical_history = new SForce.Medical_History__c();
            
            foreach (TreatmentInfo info in lstTreatmentHistory)
            {
                SForce.Medical_History__c medical_history = new SForce.Medical_History__c();

                medical_history.Contact__c = info.ContactId;
                medical_history.Treatment_Date__c = info.TreatmentDate;
                medical_history.Treatment_Date__cSpecified = true;
                medical_history.Treatment_Details__c = info.TreatmentDescription;
                medical_history.Physician_Information__c = info.PhysicianInfo;

                SForce.SaveResult[] srMedicalHistory = Sfdcbinding.create(new SForce.sObject[] { medical_history });

                if (srMedicalHistory[0].success)
                {
                    // The medical history saved successfully
                }
                else
                {
                    // Error: the medical history is not saved
                    bMedicalHistoryErrorFlag = true;
                }

            }

            Boolean bMembershipUpdateErrorFlag = false;

            String strQueryForMembershipIdUpdate = "select Id from Membership__c where Email__c = '" + strPrimaryEmail + "' " +
                                                   "and Paying_Member__c = '" + strPrimaryId + "'";

            SForce.QueryResult qrMembershipUpdateId = Sfdcbinding.query(strQueryForMembershipIdUpdate);

            if (qrMembershipUpdateId.size > 0)
            {
                SForce.Membership__c membershipId = qrMembershipUpdateId.records[0] as SForce.Membership__c;

                String strMembershipId = membershipId.Id;
                SForce.Membership__c membershipUpdate = new SForce.Membership__c();
                membershipUpdate.Id = strMembershipId;
                membershipUpdate.c4g_Membership_Status__c = "Applied";

                DateTime dtToday = DateTime.Today;

                membershipUpdate.Registration_Date__c = dtToday;
                membershipUpdate.Registration_Date__cSpecified = true;

                DateTime dtTmpStartDate = new DateTime();
                if (dtToday.Day >= 26 && dtToday.Day <= 31) dtTmpStartDate = dtToday.AddMonths(2);
                if (dtToday.Day >= 1 && dtToday.Day <= 25) dtTmpStartDate = dtToday.AddMonths(1);

                DateTime dtStartDate = new DateTime(dtTmpStartDate.Year, dtTmpStartDate.Month, 1);
                membershipUpdate.c4g_Start_Date__c = dtStartDate;
                membershipUpdate.c4g_Start_Date__cSpecified = true;

                SForce.SaveResult[] updateResultsMembership = Sfdcbinding.update(new SForce.sObject[] { membershipUpdate });
                if (updateResultsMembership[0].success)
                {
                    // Membership update succeeded
                }
                else bMembershipUpdateErrorFlag = true;
            }

            if (bMedicalHistoryErrorFlag == false && bApplicationErrorFlag == false && bApplicantErrorFlag == false && bMembershipUpdateErrorFlag == false)
            {

                SForce.Account acctPrimary = new SForce.Account();
                acctPrimary.Id = strAccountId;
                acctPrimary.cmm_Account_Creation_Step_Code__c = "8";

                SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

                if (srAccount[0].success)
                {
                    Session["PreviousPage"] = "Agreement";
                    // delete all the temporary objects here
                    Response.Redirect("~/MainForm.aspx");
                }
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Session["PreviousPage"] = "Agreement";
            Response.Redirect("HealthHistory.aspx");
        }
    }
}