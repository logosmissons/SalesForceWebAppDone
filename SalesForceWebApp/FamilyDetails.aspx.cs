using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using SForceApp = SalesForceWebApp;
using SForce = SalesForceWebApp.SForce;

namespace SalesForceWebApp
{
    public enum Gener { Male, Female }

    public class Spouse
    {
        public String ContactId { get; set; }
        public String SpouseName { get; set; }
        public String SpouseDateOfBirth { get; set; }
        public String SpouseSSN { get; set; }
        //public String strSpouseRegistrationDate { get; set; }
        public String SpouseGender { get; set; }
        //public String SpouseAction { get; set; }
    }

    public class Child
    {
        public String ContactId { get; set; }
        public String ChildName { get; set; }
        public String ChildDateOfBirth { get; set; }
        public String ChildSSN { get; set; }
        //public String strChildRegistrationDate { get; set; }
        public String ChildGender { get; set; }
        //public String ChildAction { get; set; }
    }



    public partial class FamilyDetails : System.Web.UI.Page
    {

        protected String strAccountId = String.Empty;
        protected String strAccountName = String.Empty;
        protected String strContactId = String.Empty;
        protected String strSpouseId = String.Empty;


        protected string strUserName = "harrispark@kcj777.com.uatsandbox";
        protected string strPasswd = "speed5of2light5";

        protected SForce.SforceService Sfdcbinding = null;
        protected SForce.LoginResult CurrentLoginResult = null;
        protected SForce.SaveResult[] saveResults = null;

        protected List<String> lstChildId = new List<string>();
        protected String strChildId = String.Empty;

        /// <summary>
        /// SQL statements
        /// </summary>
        protected string strQueryForSpouse = null;
        protected string strQueryForChildren = null;

        protected List<MemberSmokingDrugAlcohol> lstMemberSDA = new List<MemberSmokingDrugAlcohol>();

        protected void InitializedSfdcbinding()
        {
            Sfdcbinding = new SForce.SforceService();
            CurrentLoginResult = Sfdcbinding.login(strUserName, strPasswd);
            Sfdcbinding.Url = CurrentLoginResult.serverUrl;
            Sfdcbinding.SessionHeaderValue = new SForce.SessionHeader();
            Sfdcbinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;
        }

        protected void InitializeSQLQueryForSpouse()
        {
            strQueryForSpouse = "select Id, Name, FirstName, LastName, MiddleName, Birthdate, Social_Security_Number__c, cmm_Gender__c from Contact " +
                                "where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";
        }

        protected void InitializeSQLQueryForChildren()
        {
            strQueryForChildren = "select Id, Name, FirstName, LastName, MiddleName, Birthdate, Social_Security_Number__c, cmm_Gender__c from Contact " +
                                  "where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Child'";
        }

        protected void InitializeSQLStatements()
        {
            InitializeSQLQueryForSpouse();
            InitializeSQLQueryForChildren();
        }

        protected void Set_Spouse_current_smoking_drug()
        {
            if (hdnSpouseCurrentSmokerYes.Value == "red")
            {
                btnSpouseCurrentSmokerYes.BackColor = Color.Red;
                btnSpouseCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnSpouseCurrentSmokerNo.Value == "lightgrey")
            {
                btnSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                btnSpouseCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnSpouseNarcoticYes.Value == "red")
            {
                btnSpouseNarcoticYes.BackColor = Color.Red;
                btnSpouseNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnSpouseNarcoticYes.Value == "lightgrey")
            {
                btnSpouseNarcoticYes.BackColor = Color.LightGray;
                btnSpouseNarcoticYes.ForeColor = Color.Black;
            }
        }

        protected void Set_Spouse_smoking_drug_alcohol_buttons()
        {
            if (hdnSpouseCurrentSmokerYes.Value == "red")
            {
                btnSpouseCurrentSmokerYes.BackColor = Color.Red;
                btnSpouseCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnSpouseCurrentSmokerYes.Value == "lightgrey")
            {
                btnSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                btnSpouseCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnSpouseCurrentSmokerNo.Value == "blue")
            {
                btnSpouseCurrentSmokerNo.BackColor = Color.Blue;
                btnSpouseCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnSpouseCurrentSmokerNo.Value == "lightgrey")
            {
                btnSpouseCurrentSmokerNo.BackColor = Color.LightGray;
                btnSpouseCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnSpouseFormerSmokerYes.Value == "green")
            {
                btnSpouseFormerSmokerYes.BackColor = Color.Green;
                btnSpouseFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnSpouseFormerSmokerYes.Value == "lightgrey")
            {
                btnSpouseFormerSmokerYes.BackColor = Color.LightGray;
                btnSpouseFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnSpouseFormerSmokerNo.Value == "blue")
            {
                btnSpouseFormerSmokerNo.BackColor = Color.Blue;
                btnSpouseFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnSpouseFormerSmokerNo.Value == "lightgrey")
            {
                btnSpouseFormerSmokerNo.BackColor = Color.LightGray;
                btnSpouseFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnSpouseNarcoticYes.Value == "red")
            {
                btnSpouseNarcoticYes.BackColor = Color.Red;
                btnSpouseNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnSpouseNarcoticYes.Value == "lightgrey")
            {
                btnSpouseNarcoticYes.BackColor = Color.LightGray;
                btnSpouseNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnSpouseNarcoticNo.Value == "blue")
            {
                btnSpouseNarcoticNo.BackColor = Color.Blue;
                btnSpouseNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnSpouseNarcoticNo.Value == "lightgrey")
            {
                btnSpouseNarcoticNo.BackColor = Color.LightGray;
                btnSpouseNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnSpouseFormerNarcoticYes.Value == "green")
            {
                btnSpouseFormerNarcoticYes.BackColor = Color.Green;
                btnSpouseFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnSpouseFormerNarcoticYes.Value == "lightgrey")
            {
                btnSpouseFormerNarcoticYes.BackColor = Color.LightGray;
                btnSpouseFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnSpouseFormerNarcoticNo.Value == "blue")
            {
                btnSpouseFormerNarcoticNo.BackColor = Color.Blue;
                btnSpouseFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnSpouseFormerNarcoticNo.Value == "lightgrey")
            {
                btnSpouseFormerNarcoticNo.BackColor = Color.LightGray;
                btnSpouseFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnSpouseAlcoholYes.Value == "red")
            {
                btnSpouseAlcoholYes.BackColor = Color.Red;
                btnSpouseAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnSpouseAlcoholYes.Value == "lightgrey")
            {
                btnSpouseAlcoholYes.BackColor = Color.LightGray;
                btnSpouseAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnSpouseAlcoholNo.Value == "blue")
            {
                btnSpouseAlcoholNo.BackColor = Color.Blue;
                btnSpouseAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnSpouseAlcoholNo.Value == "lightgrey")
            {
                btnSpouseAlcoholNo.BackColor = Color.LightGray;
                btnSpouseAlcoholNo.ForeColor = Color.Black;
            }
        }

        protected void Set_Update_Spouse_smoking_drug_alcohol_buttons()
        {
            if (hdnEditSpouseCurrentSmokerYes.Value == "red")
            {
                btnEditSpouseCurrentSmokerYes.BackColor = Color.Red;
                btnEditSpouseCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseCurrentSmokerYes.Value == "lightgrey")
            {
                btnEditSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                btnEditSpouseCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseCurrentSmokerNo.Value == "blue")
            {
                btnEditSpouseCurrentSmokerNo.BackColor = Color.Blue;
                btnEditSpouseCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseCurrentSmokerNo.Value == "lightgrey")
            {
                btnEditSpouseCurrentSmokerNo.BackColor = Color.LightGray;
                btnEditSpouseCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerSmokerYes.Value == "green")
            {
                btnEditSpouseFormerSmokerYes.BackColor = Color.Green;
                btnEditSpouseFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerSmokerYes.Value == "lightgrey")
            {
                btnEditSpouseFormerSmokerYes.BackColor = Color.LightGray;
                btnEditSpouseFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerSmokerNo.Value == "blue")
            {
                btnEditSpouseFormerSmokerNo.BackColor = Color.Blue;
                btnEditSpouseFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerSmokerNo.Value == "lightgrey")
            {
                btnEditSpouseFormerSmokerNo.BackColor = Color.LightGray;
                btnEditSpouseFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseNarcoticYes.Value == "red")
            {
                btnEditSpouseNarcoticYes.BackColor = Color.Red;
                btnEditSpouseNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseNarcoticYes.Value == "lightgrey")
            {
                btnEditSpouseNarcoticYes.BackColor = Color.LightGray;
                btnEditSpouseNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseNarcoticNo.Value == "blue")
            {
                btnEditSpouseNarcoticNo.BackColor = Color.Blue;
                btnEditSpouseNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseNarcoticNo.Value == "lightgrey")
            {
                btnEditSpouseNarcoticNo.BackColor = Color.LightGray;
                btnEditSpouseNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerNarcoticYes.Value == "green")
            {
                btnEditSpouseFormerNarcoticYes.BackColor = Color.Green;
                btnEditSpouseFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerNarcoticYes.Value == "lightgrey")
            {
                btnEditSpouseFormerNarcoticYes.BackColor = Color.LightGray;
                btnEditSpouseFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerNarcoticNo.Value == "blue")
            {
                btnEditSpouseFormerNarcoticNo.BackColor = Color.Blue;
                btnEditSpouseFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerNarcoticNo.Value == "lightgrey")
            {
                btnEditSpouseFormerNarcoticNo.BackColor = Color.LightGray;
                btnEditSpouseFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseAlcoholYes.Value == "red")
            {
                btnEditSpouseAlcoholYes.BackColor = Color.Red;
                btnEditSpouseAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseAlcoholYes.Value == "lightgrey")
            {
                btnEditSpouseAlcoholYes.BackColor = Color.LightGray;
                btnEditSpouseAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseAlcoholNo.Value == "blue")
            {
                btnEditSpouseAlcoholNo.BackColor = Color.Blue;
                btnEditSpouseAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseAlcoholNo.Value == "lightgrey")
            {
                btnEditSpouseAlcoholNo.BackColor = Color.LightGray;
                btnEditSpouseAlcoholNo.ForeColor = Color.Black;
            }

        }

        protected void Set_Edit_Spouse_smoking_drug_alcohol_buttons()
        {
            if (hdnEditSpouseCurrentSmokerYes.Value == "red")
            {
                btnEditSpouseCurrentSmokerYes.BackColor = Color.Red;
                btnEditSpouseCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseCurrentSmokerYes.Value == "lightgrey")
            {
                btnEditSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                btnEditSpouseCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseCurrentSmokerNo.Value == "blue")
            {
                btnEditSpouseCurrentSmokerNo.BackColor = Color.Blue;
                btnEditSpouseCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseCurrentSmokerNo.Value == "lightgrey")
            {
                btnEditSpouseCurrentSmokerNo.BackColor = Color.LightGray;
                btnEditSpouseCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerSmokerYes.Value == "green")
            {
                btnEditSpouseFormerSmokerYes.BackColor = Color.Green;
                btnEditSpouseFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerSmokerYes.Value == "lightgrey")
            {
                btnEditSpouseFormerSmokerYes.BackColor = Color.LightGray;
                btnEditSpouseFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerSmokerNo.Value == "blue")
            {
                btnEditSpouseFormerSmokerNo.BackColor = Color.Blue;
                btnEditSpouseFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerSmokerNo.Value == "lightgrey")
            {
                btnEditSpouseFormerSmokerNo.BackColor = Color.LightGray;
                btnEditSpouseFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseNarcoticYes.Value == "red")
            {
                btnEditSpouseNarcoticYes.BackColor = Color.Red;
                btnEditSpouseNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseNarcoticYes.Value == "lightgrey")
            {
                btnEditSpouseNarcoticYes.BackColor = Color.LightGray;
                btnEditSpouseNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseNarcoticNo.Value == "blue")
            {
                btnEditSpouseNarcoticNo.BackColor = Color.Blue;
                btnEditSpouseNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseNarcoticNo.Value == "lightgrey")
            {
                btnEditSpouseNarcoticNo.BackColor = Color.LightGray;
                btnEditSpouseNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerNarcoticYes.Value == "green")
            {
                btnEditSpouseFormerNarcoticYes.BackColor = Color.Green;
                btnEditSpouseFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerNarcoticYes.Value == "lightgrey")
            {
                btnEditSpouseFormerNarcoticYes.BackColor = Color.LightGray;
                btnEditSpouseFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseFormerNarcoticNo.Value == "blue")
            {
                btnEditSpouseFormerNarcoticNo.BackColor = Color.Blue;
                btnEditSpouseFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseFormerNarcoticNo.Value == "lightgrey")
            {
                btnEditSpouseFormerNarcoticNo.BackColor = Color.LightGray;
                btnEditSpouseFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditSpouseAlcoholYes.Value == "red")
            {
                btnEditSpouseAlcoholYes.BackColor = Color.Red;
                btnEditSpouseAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnEditSpouseAlcoholYes.Value == "lightgrey")
            {
                btnEditSpouseAlcoholYes.BackColor = Color.LightGray;
                btnEditSpouseAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnEditSpouseAlcoholNo.Value == "blue")
            {
                btnEditSpouseAlcoholNo.BackColor = Color.Blue;
                btnEditSpouseAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnEditSpouseAlcoholNo.Value == "lightgrey")
            {
                btnEditSpouseAlcoholNo.BackColor = Color.LightGray;
                btnEditSpouseAlcoholNo.ForeColor = Color.Black;
            }
        }

        protected void Set_Spouse_smoking_drug_alcohol_hdn_fields()
        {
            if (btnSpouseCurrentSmokerYes.BackColor == Color.Red) hdnSpouseCurrentSmokerYes.Value = "red";
            else if (btnSpouseCurrentSmokerYes.BackColor == Color.LightGray) hdnSpouseCurrentSmokerYes.Value = "lightgrey";

            if (btnSpouseFormerSmokerYes.BackColor == Color.Green) hdnSpouseFormerSmokerYes.Value = "green";
            else if (btnSpouseFormerSmokerYes.BackColor == Color.LightGray) hdnSpouseFormerSmokerYes.Value = "lightgrey";

            if (btnSpouseNarcoticYes.BackColor == Color.Red) hdnSpouseNarcoticYes.Value = "red";
            else if (btnSpouseNarcoticYes.BackColor == Color.LightGray) hdnSpouseNarcoticYes.Value = "lightgray";

            if (btnSpouseFormerNarcoticYes.BackColor == Color.Green) hdnSpouseFormerNarcoticYes.Value = "green";
            else if (btnSpouseFormerNarcoticYes.BackColor == Color.LightGray) hdnSpouseFormerNarcoticYes.Value = "lightgrey";

            if (btnSpouseAlcoholYes.BackColor == Color.Red) hdnSpouseAlcoholYes.Value = "red";
            else if (btnSpouseAlcoholYes.BackColor == Color.LightGray) hdnSpouseAlcoholYes.Value = "lightgrey";

        }

        protected void Reset_Spouse_Smoking_Drug_Alcohol_Buttons()
        {
            btnSpouseCurrentSmokerYes.BackColor = Color.LightGray;
            btnSpouseCurrentSmokerYes.ForeColor = Color.Black;
            btnSpouseCurrentSmokerNo.BackColor = Color.Blue;
            btnSpouseCurrentSmokerNo.ForeColor = Color.White;
            hdnSpouseCurrentSmokerYes.Value = "lightgrey";
            hdnSpouseCurrentSmokerNo.Value = "blue";

            btnSpouseFormerSmokerYes.BackColor = Color.LightGray;
            btnSpouseFormerSmokerYes.ForeColor = Color.Black;
            btnSpouseFormerSmokerNo.BackColor = Color.Blue;
            btnSpouseFormerSmokerNo.ForeColor = Color.White;
            hdnSpouseFormerSmokerYes.Value = "lightgrey";
            hdnSpouseFormerSmokerNo.Value = "blue";

            btnSpouseNarcoticYes.BackColor = Color.LightGray;
            btnSpouseNarcoticYes.ForeColor = Color.Black;
            btnSpouseNarcoticNo.BackColor = Color.Blue;
            btnSpouseNarcoticNo.ForeColor = Color.White;
            hdnSpouseNarcoticYes.Value = "lightgrey";
            hdnSpouseNarcoticNo.Value = "blue";

            btnSpouseFormerNarcoticYes.BackColor = Color.LightGray;
            btnSpouseFormerNarcoticYes.ForeColor = Color.Black;
            btnSpouseFormerNarcoticNo.BackColor = Color.Blue;
            btnSpouseFormerNarcoticNo.ForeColor = Color.White;
            hdnSpouseFormerNarcoticYes.Value = "lightgrey";
            hdnSpouseFormerNarcoticNo.Value = "blue";

            btnSpouseAlcoholYes.BackColor = Color.LightGray;
            btnSpouseAlcoholYes.ForeColor = Color.Black;
            btnSpouseAlcoholNo.BackColor = Color.Blue;
            btnSpouseAlcoholNo.ForeColor = Color.White;
            hdnSpouseAlcoholYes.Value = "lightgrey";
            hdnSpouseAlcoholNo.Value = "blue";

        }

        protected void Reset_Child_Smoking_Drug_Alcohol_Buttons()
        {
            btnChildCurrentSmokerYes.BackColor = Color.LightGray;
            btnChildCurrentSmokerYes.ForeColor = Color.Black;
            btnChildCurrentSmokerNo.BackColor = Color.Blue;
            btnChildCurrentSmokerNo.ForeColor = Color.White;
            hdnChildCurrentSmokerYes.Value = "lightgrey";
            hdnChildCurrentSmokerNo.Value = "blue";

            btnChildFormerSmokerYes.BackColor = Color.LightGray;
            btnChildFormerSmokerYes.ForeColor = Color.Black;
            btnChildFormerSmokerNo.BackColor = Color.Blue;
            btnChildFormerSmokerNo.ForeColor = Color.White;
            hdnChildFormerSmokerYes.Value = "lightgrey";
            hdnChildFormerSmokerNo.Value = "blue";

            btnChildNarcoticYes.BackColor = Color.LightGray;
            btnChildNarcoticYes.ForeColor = Color.Black;
            btnChildNarcoticNo.BackColor = Color.Blue;
            btnChildNarcoticNo.ForeColor = Color.White;
            hdnChildNarcoticYes.Value = "lightgrey";
            hdnChildNarcoticNo.Value = "blue";

            btnChildFormerNarcoticYes.BackColor = Color.LightGray;
            btnChildFormerNarcoticYes.ForeColor = Color.Black;
            btnChildFormerNarcoticNo.BackColor = Color.Blue;
            btnChildFormerNarcoticNo.ForeColor = Color.White;
            hdnChildFormerNarcoticYes.Value = "lightgrey";
            hdnChildFormerNarcoticNo.Value = "blue";

            btnChildAlcoholYes.BackColor = Color.LightGray;
            btnChildAlcoholYes.ForeColor = Color.Black;
            btnChildAlcoholNo.BackColor = Color.Blue;
            btnChildAlcoholNo.ForeColor = Color.White;
            hdnChildAlcoholYes.Value = "lightgrey";
            hdnChildAlcoholNo.Value = "blue";

        }


        protected void Set_Child_smoking_drug_alcohol_buttons()
        {
            if (hdnChildCurrentSmokerYes.Value == "red")
            {
                btnChildCurrentSmokerYes.BackColor = Color.Red;
                btnChildCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnChildCurrentSmokerYes.Value == "lightgrey")
            {
                btnChildCurrentSmokerYes.BackColor = Color.LightGray;
                btnChildCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnChildCurrentSmokerNo.Value == "blue")
            {
                btnChildCurrentSmokerNo.BackColor = Color.Blue;
                btnChildCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnChildCurrentSmokerNo.Value == "lightgrey")
            {
                btnChildCurrentSmokerNo.BackColor = Color.LightGray;
                btnChildCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnChildFormerSmokerYes.Value == "green")
            {
                btnChildFormerSmokerYes.BackColor = Color.Green;
                btnChildFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnChildFormerSmokerYes.Value == "lightgrey")
            {
                btnChildFormerSmokerYes.BackColor = Color.LightGray;
                btnChildFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnChildFormerSmokerNo.Value == "blue")
            {
                btnChildFormerSmokerNo.BackColor = Color.Blue;
                btnChildFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnChildFormerSmokerNo.Value == "lightgrey")
            {
                btnChildFormerSmokerNo.BackColor = Color.LightGray;
                btnChildFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnChildNarcoticYes.Value == "red")
            {
                btnChildNarcoticYes.BackColor = Color.Red;
                btnChildNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnChildNarcoticYes.Value == "lightgrey")
            {
                btnChildNarcoticYes.BackColor = Color.LightGray;
                btnChildNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnChildNarcoticNo.Value == "blue")
            {
                btnChildNarcoticNo.BackColor = Color.Blue;
                btnChildNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnChildNarcoticNo.Value == "lightgrey")
            {
                btnChildNarcoticNo.BackColor = Color.LightGray;
                btnChildNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnChildFormerNarcoticYes.Value == "green")
            {
                btnChildFormerNarcoticYes.BackColor = Color.Green;
                btnChildFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnChildFormerNarcoticYes.Value == "lightgrey")
            {
                btnChildFormerNarcoticYes.BackColor = Color.LightGray;
                btnChildFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnChildFormerNarcoticNo.Value == "blue")
            {
                btnChildFormerNarcoticNo.BackColor = Color.Blue;
                btnChildFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnChildFormerNarcoticNo.Value == "lightgrey")
            {
                btnChildFormerNarcoticNo.BackColor = Color.LightGray;
                btnChildFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnChildAlcoholYes.Value == "red")
            {
                btnChildAlcoholYes.BackColor = Color.Red;
                btnChildAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnChildAlcoholYes.Value == "lightgrey")
            {
                btnChildAlcoholYes.BackColor = Color.LightGray;
                btnChildAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnChildAlcoholNo.Value == "blue")
            {
                btnChildAlcoholNo.BackColor = Color.Blue;
                btnChildAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnChildAlcoholNo.Value == "lightgrey")
            {
                btnChildAlcoholNo.BackColor = Color.LightGray;
                btnChildAlcoholNo.ForeColor = Color.Black;
            }

        }

        protected void Set_Edit_Child_smoking_drug_alcohol_buttons()
        {
            if (hdnEditChildCurrentSmokerYes.Value == "red")
            {
                btnEditChildCurrentSmokerYes.BackColor = Color.Red;
                btnEditChildCurrentSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditChildCurrentSmokerYes.Value == "lightgrey")
            {
                btnEditChildCurrentSmokerYes.BackColor = Color.LightGray;
                btnEditChildCurrentSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditChildCurrentSmokerNo.Value == "blue")
            {
                btnEditChildCurrentSmokerNo.BackColor = Color.Blue;
                btnEditChildCurrentSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditChildCurrentSmokerNo.Value == "lightgrey")
            {
                btnEditChildCurrentSmokerNo.BackColor = Color.LightGray;
                btnEditChildCurrentSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditChildFormerSmokerYes.Value == "green")
            {
                btnEditChildFormerSmokerYes.BackColor = Color.Green;
                btnEditChildFormerSmokerYes.ForeColor = Color.White;
            }
            else if (hdnEditChildFormerSmokerYes.Value == "lightgrey")
            {
                btnEditChildFormerSmokerYes.BackColor = Color.LightGray;
                btnEditChildFormerSmokerYes.ForeColor = Color.Black;
            }

            if (hdnEditChildFormerSmokerNo.Value == "blue")
            {
                btnEditChildFormerSmokerNo.BackColor = Color.Blue;
                btnEditChildFormerSmokerNo.ForeColor = Color.White;
            }
            else if (hdnEditChildFormerSmokerNo.Value == "lightgrey")
            {
                btnEditChildFormerSmokerNo.BackColor = Color.LightGray;
                btnEditChildFormerSmokerNo.ForeColor = Color.Black;
            }

            if (hdnEditChildNarcoticYes.Value == "red")
            {
                btnEditChildNarcoticYes.BackColor = Color.Red;
                btnEditChildNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditChildNarcoticYes.Value == "lightgrey")
            {
                btnEditChildNarcoticYes.BackColor = Color.LightGray;
                btnEditChildNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditChildNarcoticNo.Value == "blue")
            {
                btnEditChildNarcoticNo.BackColor = Color.Blue;
                btnEditChildNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditChildNarcoticNo.Value == "lightgrey")
            {
                btnEditChildNarcoticNo.BackColor = Color.LightGray;
                btnEditChildNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditChildFormerNarcoticYes.Value == "green")
            {
                btnEditChildFormerNarcoticYes.BackColor = Color.Green;
                btnEditChildFormerNarcoticYes.ForeColor = Color.White;
            }
            else if (hdnEditChildFormerNarcoticYes.Value == "lightgrey")
            {
                btnEditChildFormerNarcoticYes.BackColor = Color.LightGray;
                btnEditChildFormerNarcoticYes.ForeColor = Color.Black;
            }

            if (hdnEditChildFormerNarcoticNo.Value == "blue")
            {
                btnEditChildFormerNarcoticNo.BackColor = Color.Blue;
                btnEditChildFormerNarcoticNo.ForeColor = Color.White;
            }
            else if (hdnEditChildFormerNarcoticNo.Value == "lightgrey")
            {
                btnEditChildFormerNarcoticNo.BackColor = Color.LightGray;
                btnEditChildFormerNarcoticNo.ForeColor = Color.Black;
            }

            if (hdnEditChildAlcoholYes.Value == "red")
            {
                btnEditChildAlcoholYes.BackColor = Color.Red;
                btnEditChildAlcoholYes.ForeColor = Color.White;
            }
            else if (hdnEditChildAlcoholYes.Value == "lightgrey")
            {
                btnEditChildAlcoholYes.BackColor = Color.LightGray;
                btnEditChildAlcoholYes.ForeColor = Color.Black;
            }

            if (hdnEditChildAlcoholNo.Value == "blue")
            {
                btnEditChildAlcoholNo.BackColor = Color.Blue;
                btnEditChildAlcoholNo.ForeColor = Color.White;
            }
            else if (hdnEditChildAlcoholNo.Value == "lightgrey")
            {
                btnEditChildAlcoholNo.BackColor = Color.LightGray;
                btnEditChildAlcoholNo.ForeColor = Color.Black;
            }

        }

        protected void DisplaySpouse()
        {

            InitializedSfdcbinding();

            InitializeSQLQueryForSpouse();

            SForce.QueryResult qrSpouse = null;

            qrSpouse = Sfdcbinding.query(strQueryForSpouse);

            SForce.Contact ctSpouse = new SForce.Contact();

            var lstSpouse = new List<Spouse>();

            if (qrSpouse.size > 0)
            {
                ctSpouse = (SForce.Contact)qrSpouse.records[0];

                if (ctSpouse != null)
                {
                    Session["SpouseId"] = ctSpouse.Id;

                    DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;

                    String strSpouseGender = null;

                    if (ctSpouse.cmm_Gender__c.ToString() == "Male")
                    {
                        strSpouseGender = "Male";
                    }
                    else if (ctSpouse.cmm_Gender__c.ToString() == "Female")
                    {
                        strSpouseGender = "Female";
                    }

                    //var lstSpouse = new List<Spouse>();

                    lstSpouse.Add(new Spouse()
                    {
                        ContactId = ctSpouse.Id,
                        SpouseName = ctSpouse.LastName + ", " + ctSpouse.FirstName + " " + ctSpouse.MiddleName,
                        SpouseDateOfBirth = dtSpouseBirthdate.ToString("MM/dd/yyyy"),
                        SpouseSSN = ctSpouse.Social_Security_Number__c,
                        SpouseGender = strSpouseGender,

                        //SpouseAction = "Delete"
                    });

                    gvSpouse.DataSource = lstSpouse;
                    gvSpouse.DataBind();
                    upnlSpouse.Update();
                }
            }
            else
            {
                gvSpouse.DataSource = lstSpouse;
                gvSpouse.DataBind();
                upnlSpouse.Update();
            }
        }

        protected void DisplayChildren()
        {
            InitializedSfdcbinding();

            InitializeSQLQueryForChildren();

            SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildren);

            List<Child> lstChildren = new List<Child>();

            if (qrChild.size > 0)
            {

                foreach (SForce.Contact ctChild in qrChild.records)
                {
                    DateTime dtChildBirthdate = (DateTime)ctChild.Birthdate;
                    String strChildGender = null;
                    if (ctChild.cmm_Gender__c.ToString() == "Male") strChildGender = "Male";
                    else if (ctChild.cmm_Gender__c.ToString() == "Female") strChildGender = "Female";
                    lstChildren.Add(new Child()
                    {
                        ContactId = ctChild.Id,
                        ChildName = ctChild.LastName + ", " + ctChild.FirstName + " " + ctChild.MiddleName,
                        ChildDateOfBirth = dtChildBirthdate.ToString("MM/dd/yyyy"),
                        ChildSSN = ctChild.Social_Security_Number__c,
                        ChildGender = strChildGender
                    });
                }

                gvChildren.DataSource = lstChildren;
                gvChildren.DataBind();

                btnRemoveChildren.Enabled = true;
                upnlAddRemoveChildren.Update();
                upnlChildren.Update();

            }
            if (qrChild.size == 0)
            {
                gvChildren.DataSource = lstChildren;
                gvChildren.DataBind();
                btnRemoveChildren.Enabled = false;
                upnlAddRemoveChildren.Update();
                upnlChildren.Update();
            }


        }

        protected void RefreshSpouse()
        {
            var lstSpouse = new List<Spouse>();

            gvSpouse.DataSource = lstSpouse;
            gvSpouse.DataBind();
            upnlSpouse.Update();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //InitializeSQLQueryForSpouse();
            //InitializeSQLQueryForChildren();
            InitializedSfdcbinding();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //strAccountId = (String)Session["AccountId"];

            //InitializedSfdcbinding();

            //if (!IsPostBack)
            //{
            //    bIsCreateSpouse = false;
            //    bIsUpdateSpouse = false;
            //    bIsCreateChild = false;
            //    bIsUpdateChild = false;
            //}

            // 10/19/17 begin with retrieving primary member's ContactId from salesforce

            // if Session["AccountId"] and Session["ContactId"] are null, retrieve AccountId and ContactId from Salesforce

            if ((String)Session["AccountId"] != String.Empty) strAccountId = (String)Session["AccountId"];
            if ((String)Session["AccountName"] != String.Empty) strAccountName = (String)Session["AccountName"];
            //if (Session["ContactId"] != null) strContactId = (String)Session["ContactId"];

            String strPreviousPage = String.Empty;
            if ((String)Session["PreviousPage"] != String.Empty) strPreviousPage = (String)Session["PreviousPage"];

            InitializeSQLStatements();

            //Session["PreviousPage"] = "PersonalInfo";

            //if (strPreviousPage == "PersonalInfo")
            //{
            if (!IsPostBack)
            {
                //InitializeSQLQueryForSpouse();
                SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);

                if (qrSpouse.size == 1)
                {
                    SForce.Contact ctSpouse = qrSpouse.records[0] as SForce.Contact;

                    List<Spouse> lstSpouseInfo = new List<Spouse>();

                    Spouse spouseInfo = new Spouse();
                    spouseInfo.ContactId = ctSpouse.Id;
                    spouseInfo.SpouseName = ctSpouse.Name;
                    DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;
                    spouseInfo.SpouseDateOfBirth = dtSpouseBirthdate.ToString("MM/dd/yyyy");
                    spouseInfo.SpouseGender = ctSpouse.cmm_Gender__c;
                    spouseInfo.SpouseSSN = ctSpouse.Social_Security_Number__c;

                    Session["SpouseId"] = ctSpouse.Id;

                    lstSpouseInfo.Add(spouseInfo);
                    gvSpouse.DataSource = lstSpouseInfo;
                    gvSpouse.DataBind();

                    btnAddSpouse.Enabled = false;
                    btnRemoveSpouse.Enabled = true;
                    upnlSpouseAddRemove.Update();

                    //upnlSpouse.Update();

                }
                else if (qrSpouse.size == 0)
                {
                    List<Spouse> lstSpouseInfo = new List<Spouse>();

                    gvSpouse.DataSource = lstSpouseInfo;
                    gvSpouse.DataBind();
                }

                InitializeSQLQueryForChildren();
                SForce.QueryResult qrChildren = Sfdcbinding.query(strQueryForChildren);

                if (qrChildren.size > 0)
                {
                    List<Child> lstChildren = new List<Child>();
                    foreach (SForce.Contact ctChild in qrChildren.records)
                    {
                        Child childInfo = new Child();
                        childInfo.ContactId = ctChild.Id;
                        childInfo.ChildName = ctChild.Name;
                        DateTime dtChildBirthdate = (DateTime)ctChild.Birthdate;
                        childInfo.ChildDateOfBirth = dtChildBirthdate.ToString("MM/dd/yyyy");
                        childInfo.ChildGender = ctChild.cmm_Gender__c;
                        childInfo.ChildSSN = ctChild.Social_Security_Number__c;

                        lstChildren.Add(childInfo);
                    }
                    gvChildren.DataSource = lstChildren;
                    gvChildren.DataBind();
                    btnRemoveChildren.Enabled = true;
                    upnlAddRemoveChildren.Update();
                }
                else if (qrChildren.size == 0)
                {
                    List<Child> lstChildren = new List<Child>();

                    gvChildren.DataSource = lstChildren;
                    gvChildren.DataBind();
                }
                //}
                //else if (strPreviousPage == "PersonalDetails")
                //{
                //    if (!IsPostBack)
                //    {
                //        var lstSpouse = new List<Spouse>();

                //        gvSpouse.DataSource = lstSpouse;
                //        gvSpouse.DataBind();

                //        var lstChildren = new List<Child>();

                //        gvChildren.DataSource = lstChildren;
                //        gvChildren.DataBind();

                //    }
                //}

                // retrieve lstMemberSDA of primary member from salesforce in the case of incomplete application


                if ((List<MemberSmokingDrugAlcohol>)Session["MemberSmokingDrugAlcohol"] != null) lstMemberSDA = Session["MemberSmokingDrugAlcohol"] as List<MemberSmokingDrugAlcohol>;
                else
                {
                    String strQueryForMemberSDA = "select cmm_Account_Creation_Step_Code__c, cmm_Account__c, cmm_Contact__c, cmm_Household_Role__c, cmm_Name__c, " +
                                                  "cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c from tmp_SmokingDrugAlcohol__c " +
                                                  "where cmm_Account__c = '" + strAccountId + "'";

                    SForce.QueryResult qrMemberSDA = Sfdcbinding.query(strQueryForMemberSDA);

                    if (qrMemberSDA.size > 0)
                    {
                        foreach (SForce.tmp_SmokingDrugAlcohol__c sda in qrMemberSDA.records)
                        {
                            MemberSmokingDrugAlcohol memberSDA = new MemberSmokingDrugAlcohol();

                            memberSDA.AccountCreationStepCode = (int)sda.cmm_Account_Creation_Step_Code__c;
                            memberSDA.AccountId = strAccountId;
                            memberSDA.ContactId = sda.cmm_Contact__c;
                            memberSDA.Name = sda.cmm_Name__c;
                            if (sda.cmm_Household_Role__c == "Head of Household") memberSDA.HouseholdRole = HouseholdRoles.Primary;
                            else if (sda.cmm_Household_Role__c == "Spouse") memberSDA.HouseholdRole = HouseholdRoles.Spouse;
                            else if (sda.cmm_Household_Role__c == "Child") memberSDA.HouseholdRole = HouseholdRoles.Child;
                            memberSDA.bCurrentSmoker = (sda.cmm_bCurrentSmoker__c == "Yes");
                            memberSDA.bFormerSmoker = (sda.cmm_bFormerSmoker__c == "Yes");
                            memberSDA.bCurrentDrug = (sda.cmm_bCurrentDrug__c == "Yes");
                            memberSDA.bFormerDrug = (sda.cmm_bFormerDrug__c == "Yes");
                            memberSDA.bAlcohol = (sda.cmm_bAlcohol__c == "Yes");

                            lstMemberSDA.Add(memberSDA);
                        }
                    }
                }

                btnRemoveChildren.Enabled = false;

                gvChildren.AlternatingRowStyle.BackColor = System.Drawing.Color.LightGray;
            }
            //if (strPreviousPage == "PersonalDetails")
            //{
            //    if (!IsPostBack)
            //    {
            //        var lstSpouse = new List<Spouse>();

            //        gvSpouse.DataSource = lstSpouse;
            //        gvSpouse.DataBind();

            //        var lstChildren = new List<Child>();

            //        gvChildren.DataSource = lstChildren;
            //        gvChildren.DataBind();

            //    }
            //}
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/PersonalDetails.aspx");
            Session["PreviousPage"] = "FamilyDetails";
            Response.Redirect("PersonalDetails.aspx");
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            Set_Spouse_smoking_drug_alcohol_buttons();
            Set_Child_smoking_drug_alcohol_buttons();

            //if (rfvSpouseLastName.IsValid &&
            //    rfvSpouseFirstName.IsValid &&
            //    rfvSpouseDateOfBirth.IsValid &&
            //    rfvEditSpouseGender.IsValid &&
            //    rfvSpouseSSN.IsValid &&
            //    revSpouseSSN.IsValid &&
            //    rfvEditSpouseLastName.IsValid &&
            //    rfvEditSpouseFirstName.IsValid &&
            //    rfvEditSpouseDateOfBirth.IsValid &&
            //    rfvEditSpouseGender.IsValid &&
            //    rfvChildLastName.IsValid &&
            //    rfvChildFirstName.IsValid &&
            //    rfvChildDateOfBirth.IsValid &&
            //    rfvChildGender.IsValid &&
            //    rfvChildSSN.IsValid &&
            //    revChildSSN.IsValid &&
            //    rfvEditChildLastName.IsValid &&
            //    rfvEditChildFirstName.IsValid &&
            //    rfvEditChildDateOfBirth.IsValid &&
            //    rfvEditChildGender.IsValid
            //    )
            //{
            //    ;
            //}
            //if (IsValid && btnSpouseCurrentSmokerYes.BackColor == Color.LightGray && btnSpouseNarcoticYes.BackColor == Color.LightGray)
            //if (IsValid)
            //{




            SForce.Account acctPrimary = new SForce.Account();
            acctPrimary.Id = strAccountId;
            acctPrimary.cmm_Account_Creation_Step_Code__c = "4";

            SForce.SaveResult[] srAccount = Sfdcbinding.update(new SForce.sObject[] { acctPrimary });

            if (srAccount[0].success)
            {
                Session["PreviousPage"] = "FamilyDetails";
                Response.Redirect("~/MembershipDetails.aspx");
            }
        }

        protected void btnAddSpouse_Click(object sender, EventArgs e)
        {
            InitializeAddSpouseFields();
            //Set_Spouse_smoking_drug_alcohol_buttons();
            Session["IsCreateSpouse"] = true;
            Session["IsUpdateSpouse"] = false;
            mpeAddSpouse.Show();
        }

        protected void InitializeAddSpouseFields()
        {
            txtSpouseLastName.Text = String.Empty;
            txtSpouseFirstName.Text = String.Empty;
            txtSpouseMiddleName.Text = String.Empty;
            txtSpouseDateOfBirth.Text = String.Empty;
            rbListSpouseGender.ClearSelection();
            txtSpouseSSN.Text = String.Empty;
        }


        protected void btnAddChild_Click(object sender, EventArgs e)
        {
            //pnlAddChild.Visible = true;
            //InitializeAddChildFields();
            //Set_Child_smoking_drug_alcohol_buttons();
            Session["IsCreateChild"] = true;
            Session["IsUpdateChild"] = false;
            txtChildLastName.Text = "";
            txtChildFirstName.Text = "";
            txtChildMiddleName.Text = "";
            txtChildDateOfBirth.Text = "";
            rbListChildGender.SelectedIndex = -1;
            txtChildSSN.Text = "";
            Reset_Child_Smoking_Drug_Alcohol_Buttons();
            mpeAddChild.Show();
        }

        protected void InitializeAddChildFields()
        {
            txtChildLastName.Text = String.Empty;
            txtChildFirstName.Text = String.Empty;
            txtChildMiddleName.Text = String.Empty;
            txtChildDateOfBirth.Text = String.Empty;
            rbListChildGender.ClearSelection();
            txtChildSSN.Text = String.Empty;
        }

        protected void btnSpouseClose_Click(object sender, EventArgs e)
        {
            mpeAddSpouse.Hide();
            //LoadSpouseInfo();
            Set_Spouse_smoking_drug_alcohol_buttons();
            //Reset_Spouse_Smoking_Drug_Alcohol_Buttons();
        }

        protected void btnChildClose_Click(object sender, EventArgs e)
        {
            //pnlAddChild.Visible = false;
            mpeAddChild.Hide();
            Reset_Child_Smoking_Drug_Alcohol_Buttons();
        }

        protected void btnSpouseConfirm_Click(object sender, EventArgs e)
        {
            //Set_Spouse_smoking_drug_alcohol_hdn_fields();
            //Set_Spouse_smoking_drug_alcohol_buttons();

            Set_Spouse_smoking_drug_alcohol_buttons();
            //if (IsValid)
            if (rfvSpouseLastName.IsValid &&
                rfvSpouseFirstName.IsValid &&
                rfvSpouseDateOfBirth.IsValid &&
                rfvSpouseGender.IsValid &&
                rfvSpouseSSN.IsValid &&
                revSpouseSSN.IsValid &&
                btnSpouseCurrentSmokerYes.BackColor == Color.LightGray &&
                btnSpouseNarcoticYes.BackColor == Color.LightGray &&
                btnSpouseAlcoholYes.BackColor == Color.LightGray)
            {
                //lblResult.Text = "btnSpouseConfirm method";

                // 10/09/17 begin here
                // add spouse smoking, drug, alcohol info to lstMemberSDA


                SForce.Contact ctSpouse = new SForce.Contact();

                ctSpouse.cmm_Household__c = strAccountId;
                ctSpouse.cmm_Household_Role__c = "Spouse";
                ctSpouse.LastName = txtSpouseLastName.Text;
                ctSpouse.FirstName = txtSpouseFirstName.Text;
                if (txtSpouseMiddleName.Text != "") ctSpouse.MiddleName = txtSpouseMiddleName.Text;

                String strSpouseDateOfBirth = txtSpouseDateOfBirth.Text;

                String[] arraySpouseDateOfBirth = strSpouseDateOfBirth.Split('/');
                int nSpouseBirthMonth = Int32.Parse(arraySpouseDateOfBirth[0]);
                int nSpouseBirthDay = Int32.Parse(arraySpouseDateOfBirth[1]);
                int nSpouseBirthYear = Int32.Parse(arraySpouseDateOfBirth[2]);

                ctSpouse.Birthdate = new DateTime(nSpouseBirthYear, nSpouseBirthMonth, nSpouseBirthDay);
                ctSpouse.BirthdateSpecified = true;

                ctSpouse.cmm_Gender__c = rbListSpouseGender.SelectedItem.Text;
                ctSpouse.Social_Security_Number__c = txtSpouseSSN.Text;
                ctSpouse.cmm_Household_Role__c = "Spouse";
                ctSpouse.cmm_Gender__c = rbListSpouseGender.SelectedItem.Text;

                //////////////////////////////////////////////////
                // Initialize Sforce binding and creating spouse
                //InitializedSfdcbinding();

                //SForce.SaveResult[] saveResults = Sfdcbinding.create(SForce.SaveResult sObject[] { ctSpouse });

                //Boolean bIsCreateSpouse = false;

                //if ((Boolean)Session["IsCreateSpouse"] == true && (Boolean)Session["IsUpdateSpouse"] == false)
                //{

                SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { ctSpouse });

                if (saveResults[0].success)
                {
                    //lblResult.Text = "Spouse is added";
                    //pnlAddSpouse.CssClass = "pnlBackGroundHidden";
                    mpeAddSpouse.Hide();
                    DisplaySpouse();
                    InitializeAddSpouseFields();
                    btnAddSpouse.Enabled = false;
                    btnRemoveSpouse.Enabled = true;
                    upnlSpouseAddRemove.Update();
                    strSpouseId = saveResults[0].id;
                    Session["SpouseId"] = saveResults[0].id;

                }
                else
                {
                    lblResult.Text = saveResults[0].errors[0].message;
                }
                //}
                //if ((Boolean)Session["IsCreateSpouse"] == false && (Boolean)Session["IsUpdateSpouse"] == true)
                //{
                //    SForce.Contact ctSpouseUpdate = new SForce.Contact();

                //    ctSpouseUpdate.Id = strSpouseId;
                //    if ((String)Session["SpouseId"] != null) ctSpouseUpdate.Id = (String)Session["SpouseId"];

                //    //ctSpouseUpdate.cmm_Household__c = strAccountId;
                //    //ctSpouseUpdate.cmm_Household_Role__c = "Spouse";
                //    ctSpouseUpdate.LastName = txtSpouseLastName.Text;
                //    ctSpouseUpdate.FirstName = txtSpouseFirstName.Text;
                //    if (txtSpouseMiddleName.Text != "") ctSpouseUpdate.MiddleName = txtSpouseMiddleName.Text;

                //    String strSpouseDateOfBirthUpdate = txtSpouseDateOfBirth.Text;

                //    String[] arraySpouseDateOfBirthUpdate = strSpouseDateOfBirthUpdate.Split('/');
                //    int nSpouseBirthMonthUpdate = Int32.Parse(arraySpouseDateOfBirthUpdate[0]);
                //    int nSpouseBirthDayUpdate = Int32.Parse(arraySpouseDateOfBirthUpdate[1]);
                //    int nSpouseBirthYearUpdate = Int32.Parse(arraySpouseDateOfBirthUpdate[2]);

                //    ctSpouseUpdate.Birthdate = new DateTime(nSpouseBirthYearUpdate, nSpouseBirthMonthUpdate, nSpouseBirthDayUpdate);
                //    ctSpouseUpdate.BirthdateSpecified = true;

                //    ctSpouseUpdate.cmm_Gender__c = rbListSpouseGender.SelectedItem.Text;
                //    //ctSpouseUpdate.Social_Security_Number__c = txtSpouseSSN.Text;
                //    //ctSpouseUpdate.cmm_Household_Role__c = "Spouse";
                //    ctSpouseUpdate.cmm_Gender__c = rbListSpouseGender.SelectedItem.Text;

                //    SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctSpouseUpdate });

                //    if (updateResults[0].success)
                //    {
                //        // update succeeded
                //        mpeAddSpouse.Hide();
                //        DisplaySpouse();
                //        InitializeAddSpouseFields();
                //        btnAddSpouse.Enabled = false;
                //        btnRemoveSpouse.Enabled = true;
                //        upnlSpouseAddRemove.Update();
                //        //Set_Spouse_smoking_drug_alcohol_hdn_fields();
                //    }


                //}

                String strQueryForSpouseId = "select Id, Name from Contact where cmm_Household__c = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";
                SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouseId);

                if (qrSpouse.size > 0)
                {
                    SForce.Contact ctSpouseId = (SForce.Contact)qrSpouse.records[0];

                    MemberSmokingDrugAlcohol spouseSDA = new MemberSmokingDrugAlcohol();
                    spouseSDA.HouseholdRole = HouseholdRoles.Spouse;
                    spouseSDA.ContactId = ctSpouseId.Id;
                    spouseSDA.Name = ctSpouse.Name;
                    spouseSDA.bCurrentSmoker = false;
                    spouseSDA.bCurrentDrug = false;
                    spouseSDA.bAlcohol = false;
                    if (btnSpouseFormerSmokerYes.BackColor == Color.Green) spouseSDA.bFormerSmoker = true;
                    else if (btnSpouseFormerSmokerYes.BackColor == Color.LightGray) spouseSDA.bFormerSmoker = false;
                    if (btnSpouseFormerNarcoticYes.BackColor == Color.Green) spouseSDA.bFormerDrug = true;
                    else if (btnSpouseFormerNarcoticYes.BackColor == Color.LightGray) spouseSDA.bFormerDrug = false;
                    //if (btnSpouseAlcoholYes.BackColor == Color.Green) spouseSDA.bAlcohol = true;
                    //else if (btnSpouseAlcoholYes.BackColor == Color.LightGray) spouseSDA.bAlcohol = false;

                    lstMemberSDA.Add(spouseSDA);
                    Session["MemberSmokingDrugAlcohol"] = lstMemberSDA;

                    // Save lstMemberSDA for spouse to salesforce for incomplete application - done


                    //if (bIsCreateSpouse == true && bIsUpdateSpouse == false)
                    if ((Boolean)Session["IsCreateSpouse"] == true && (Boolean)Session["IsUpdateSpouse"] == false)
                    {
                        SForce.tmp_SmokingDrugAlcohol__c tmpSpouseSDA = new SForce.tmp_SmokingDrugAlcohol__c();
                        tmpSpouseSDA.cmm_Account_Creation_Step_Code__c = 4;
                        tmpSpouseSDA.cmm_Account_Creation_Step_Code__cSpecified = true;
                        tmpSpouseSDA.cmm_Account__c = strAccountId;
                        tmpSpouseSDA.cmm_Contact__c = ctSpouseId.Id;
                        tmpSpouseSDA.cmm_Name__c = ctSpouseId.Name;
                        tmpSpouseSDA.cmm_Household_Role__c = "Spouse";
                        tmpSpouseSDA.cmm_bCurrentSmoker__c = "No";
                        tmpSpouseSDA.cmm_bCurrentDrug__c = "No";
                        tmpSpouseSDA.cmm_bAlcohol__c = "No";
                        if (btnSpouseFormerSmokerYes.BackColor == Color.Green) tmpSpouseSDA.cmm_bFormerSmoker__c = "Yes";
                        else if (btnSpouseFormerSmokerYes.BackColor == Color.LightGray) tmpSpouseSDA.cmm_bFormerSmoker__c = "No";
                        if (btnSpouseFormerNarcoticYes.BackColor == Color.Green) tmpSpouseSDA.cmm_bFormerDrug__c = "Yes";
                        else if (btnSpouseFormerNarcoticYes.BackColor == Color.LightGray) tmpSpouseSDA.cmm_bFormerDrug__c = "No";
                        //if (btnSpouseAlcoholYes.BackColor == Color.Green) tmpSpouseSDA.cmm_bAlcohol__c = "Yes";
                        //else if (btnSpouseAlcoholYes.BackColor == Color.LightGray) tmpSpouseSDA.cmm_bAlcohol__c = "No";

                        SForce.SaveResult[] srTmpSpouseSDA = Sfdcbinding.create(new SForce.sObject[] { tmpSpouseSDA });

                        if (srTmpSpouseSDA[0].success)
                        {
                            // the temporary instance of salesforce object is created successfully
                        }
                    }
                    //if (bIsCreateSpouse == false && bIsUpdateSpouse == true)
                    //if ((String)Session["SpouseId"] != null) strSpouseId = (String)Session["SpouseId"];
                    //if ((Boolean)Session["IsCreateSpouse"] == false && (Boolean)Session["IsUpdateSpouse"] == true)
                    //{
                    //    String strQuerySpouseForSDA = "select Id from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
                    //                                  "and cmm_Contact__c = '" + strSpouseId + "' " +
                    //                                  "and cmm_Household_Role__c = 'Spouse'";

                    //    SForce.QueryResult qrSpouseSDA = Sfdcbinding.query(strQuerySpouseForSDA);

                    //    if (qrSpouseSDA.size > 0)
                    //    {
                    //        SForce.tmp_SmokingDrugAlcohol__c spouseSDAId = qrSpouseSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    //        SForce.tmp_SmokingDrugAlcohol__c spouseSDAUpdate = new SForce.tmp_SmokingDrugAlcohol__c();
                    //        spouseSDAUpdate.Id = spouseSDAId.Id;

                    //        spouseSDAUpdate.cmm_Name__c = ctSpouseId.Name;
                    //        spouseSDAUpdate.cmm_Household_Role__c = "Spouse";
                    //        spouseSDAUpdate.cmm_bCurrentSmoker__c = "No";
                    //        spouseSDAUpdate.cmm_bCurrentDrug__c = "No";
                    //        spouseSDAUpdate.cmm_bAlcohol__c = "No";
                    //        if (btnSpouseFormerSmokerYes.BackColor == Color.Green) spouseSDAUpdate.cmm_bFormerSmoker__c = "Yes";
                    //        else if (btnSpouseFormerSmokerYes.BackColor == Color.LightGray) spouseSDAUpdate.cmm_bFormerSmoker__c = "No";
                    //        if (btnSpouseFormerNarcoticYes.BackColor == Color.Green) spouseSDAUpdate.cmm_bFormerDrug__c = "Yes";
                    //        else if (btnSpouseFormerNarcoticYes.BackColor == Color.LightGray) spouseSDAUpdate.cmm_bFormerDrug__c = "No";
                    //        //if (btnSpouseAlcoholYes.BackColor == Color.Green) spouseSDAUpdate.cmm_bAlcohol__c = "Yes";
                    //        //else if (btnSpouseAlcoholYes.BackColor == Color.LightGray) spouseSDAUpdate.cmm_bAlcohol__c = "No";

                    //        SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { spouseSDAUpdate });

                    //        if (updateResults[0].success)
                    //        {
                    //            // spouse SDA info updated successfully
                    //        }
                    //    }
                    //}
                }
            }
            else if (btnSpouseCurrentSmokerYes.BackColor == Color.Red || btnSpouseNarcoticYes.BackColor == Color.Red || btnSpouseAlcoholYes.BackColor == Color.Red)
            {
                mpeSpouseSmokingDrug.Show();
            }
        }

        protected void btnChildConfirm_Click(object sender, EventArgs e)
        {
            rfvChildGender.Validate();
            Set_Child_smoking_drug_alcohol_buttons();

            if (rfvChildLastName.IsValid &&
                rfvChildFirstName.IsValid &&
                rfvChildDateOfBirth.IsValid &&
                rfvChildGender.IsValid &&
                rfvChildSSN.IsValid &&
                revChildSSN.IsValid &&
                btnChildCurrentSmokerYes.BackColor == Color.LightGray &&
                btnChildNarcoticYes.BackColor == Color.LightGray &&
                btnChildAlcoholYes.BackColor == Color.LightGray)
            {

                SForce.Contact ctChild = new SForce.Contact();

                ctChild.cmm_Household__c = strAccountId;
                ctChild.cmm_Household_Role__c = "Child";
                ctChild.LastName = txtChildLastName.Text;
                ctChild.FirstName = txtChildFirstName.Text;
                if (txtChildMiddleName.Text != "") ctChild.MiddleName = txtChildMiddleName.Text;

                String strChildDateOfBirth = txtChildDateOfBirth.Text;

                String[] arrayChildDateOfBirth = strChildDateOfBirth.Split('/');
                int nChildBirthMonth = Int32.Parse(arrayChildDateOfBirth[0]);
                int nChildBirthDay = Int32.Parse(arrayChildDateOfBirth[1]);
                int nChildBirthYear = Int32.Parse(arrayChildDateOfBirth[2]);

                ctChild.Birthdate = new DateTime(nChildBirthYear, nChildBirthMonth, nChildBirthDay);
                ctChild.BirthdateSpecified = true;

                ctChild.cmm_Gender__c = rbListChildGender.SelectedItem.Text;
                ctChild.Social_Security_Number__c = txtChildSSN.Text;

                InitializedSfdcbinding();

                //if (bIsCreateChild == true && bIsUpdateChild == false)
                //if ((Boolean)Session["IsCreateChild"] == true && (Boolean)Session["IsUpdateChild"] == false)
                //{

                SForce.SaveResult[] saveResults = Sfdcbinding.create(new SForce.sObject[] { ctChild });

                String strChildId = null;

                if (saveResults[0].success)
                {
                    lblResult.Text = "Child added";

                    //btnChildClose.Text = "Close";
                    mpeAddChild.Hide();
                    DisplayChildren();
                    InitializeAddChildFields();
                    btnRemoveChildren.Enabled = true;
                    upnlAddRemoveChildren.Update();
                    strChildId = saveResults[0].id;
                    lstChildId.Add(saveResults[0].id);
                    Session["ChildIds"] = lstChildId;

                }
                else
                {
                    lblResult.Text = saveResults[0].errors[0].message;
                }
                //}
                //if ((Boolean)Session["IsCreateChild"] == false && (Boolean)Session["IsUpdateChild"] == true)
                //{
                //ctChild.Id = strChildId;

                //SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctChild });

                //if (updateResults[0].success)
                //{
                //    lblResult.Text = "Child infomation updated";

                //    mpeAddChild.Hide();
                //    DisplayChildren();
                //    InitializeAddChildFields();
                //    btnRemoveChildren.Enabled = true;
                //    upnlAddRemoveChildren.Update();
                //}
                //}

                String strQueryForChildId = "select Name from Contact where cmm_Household__c = '" + strAccountId +
                                            "' and cmm_Household_Role__c = 'Child' and Id = '" + strChildId + "'";

                SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildId);

                if (qrChild.size > 0)
                {
                    SForce.Contact ctChildName = (SForce.Contact)qrChild.records[0];

                    MemberSmokingDrugAlcohol childSDA = new MemberSmokingDrugAlcohol();

                    childSDA.HouseholdRole = HouseholdRoles.Child;
                    childSDA.ContactId = strChildId;
                    childSDA.Name = ctChildName.Name;

                    childSDA.bCurrentSmoker = false;
                    childSDA.bCurrentDrug = false;
                    childSDA.bAlcohol = false;
                    if (btnChildFormerSmokerYes.BackColor == Color.Green) childSDA.bFormerSmoker = true;
                    else if (btnChildFormerSmokerYes.BackColor == Color.LightGray) childSDA.bFormerSmoker = false;
                    if (btnChildFormerNarcoticYes.BackColor == Color.Green) childSDA.bFormerDrug = true;
                    else if (btnChildFormerNarcoticNo.BackColor == Color.LightGray) childSDA.bFormerDrug = false;
                    //if (btnChildAlcoholYes.BackColor == Color.Green) childSDA.bAlcohol = true;
                    //else if (btnChildAlcoholYes.BackColor == Color.LightGray) childSDA.bAlcohol = false;

                    lstMemberSDA.Add(childSDA);
                    Session["MemberSmokingDrugAlcohol"] = lstMemberSDA;

                    // Save lstMemberSDA for child to salesforce for incomplete application - done

                    //if (bIsCreateChild == true && bIsUpdateChild == false)
                    //if ((Boolean)Session["IsCreateChild"] == true && (Boolean)Session["IsUpdateChild"] == false)
                    //{
                    SForce.tmp_SmokingDrugAlcohol__c tmpChildSDA = new SForce.tmp_SmokingDrugAlcohol__c();

                    tmpChildSDA.cmm_Account_Creation_Step_Code__c = 4;
                    tmpChildSDA.cmm_Account_Creation_Step_Code__cSpecified = true;
                    tmpChildSDA.cmm_Household_Role__c = "Child";
                    tmpChildSDA.cmm_Account__c = strAccountId;
                    tmpChildSDA.cmm_Contact__c = strChildId;
                    tmpChildSDA.cmm_Name__c = ctChildName.Name;
                    tmpChildSDA.cmm_bCurrentSmoker__c = "No";
                    tmpChildSDA.cmm_bCurrentDrug__c = "No";
                    tmpChildSDA.cmm_bAlcohol__c = "No";
                    if (btnChildFormerSmokerYes.BackColor == Color.Green) tmpChildSDA.cmm_bFormerSmoker__c = "Yes";
                    else if (btnChildFormerSmokerYes.BackColor == Color.LightGray) tmpChildSDA.cmm_bFormerSmoker__c = "No";
                    if (btnChildFormerNarcoticYes.BackColor == Color.Green) tmpChildSDA.cmm_bFormerDrug__c = "Yes";
                    else if (btnChildFormerNarcoticYes.BackColor == Color.LightGray) tmpChildSDA.cmm_bFormerDrug__c = "No";
                    //if (btnChildAlcoholYes.BackColor == Color.Green) tmpChildSDA.cmm_bAlcohol__c = "Yes";
                    //else if (btnChildAlcoholYes.BackColor == Color.LightGray) tmpChildSDA.cmm_bAlcohol__c = "No";

                    SForce.SaveResult[] srTmpChildSDA = Sfdcbinding.create(new SForce.sObject[] { tmpChildSDA });

                    if (srTmpChildSDA[0].success)
                    {
                        // The temporary childSDA record is created successfully
                    }
                    //}
                    //if (bIsCreateChild == false && bIsUpdateChild == true)
                    //if ((Boolean)Session["IsCreateChild"] == false && (Boolean)Session["IsUpdateChild"] == true)
                    //{
                    //    String strQueryChildForSDA = "select Id from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
                    //                                 "and cmm_Contact__c = '" + strChildId + "' " +
                    //                                 "and cmm_Household_Role__c = 'Child'";

                    //    SForce.QueryResult qrChildSDA = Sfdcbinding.query(strQueryChildForSDA);

                    //    if (qrChildSDA.size > 0)
                    //    {
                    //        SForce.tmp_SmokingDrugAlcohol__c childSDAId = qrChildSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    //        SForce.tmp_SmokingDrugAlcohol__c updateChildSDA = new SForce.tmp_SmokingDrugAlcohol__c();
                    //        updateChildSDA.Id = childSDAId.Id;

                    //        //updateChildSDA.cmm_Account_Creation_Step_Code__c = 4;
                    //        //updateChildSDA.cmm_Account_Creation_Step_Code__cSpecified = true;
                    //        //updateChildSDA.cmm_Household_Role__c = "Child";
                    //        //updateChildSDA.cmm_Account__c = strAccountId;
                    //        //updateChildSDA.cmm_Contact__c = strChildId;
                    //        updateChildSDA.cmm_Name__c = ctChildName.Name;
                    //        updateChildSDA.cmm_bCurrentSmoker__c = "No";
                    //        updateChildSDA.cmm_bCurrentDrug__c = "No";
                    //        if (btnChildFormerSmokerYes.BackColor == Color.Green) updateChildSDA.cmm_bFormerSmoker__c = "Yes";
                    //        else if (btnChildFormerSmokerYes.BackColor == Color.LightGray) updateChildSDA.cmm_bFormerSmoker__c = "No";
                    //        if (btnChildFormerNarcoticYes.BackColor == Color.Green) updateChildSDA.cmm_bFormerDrug__c = "Yes";
                    //        else if (btnChildFormerNarcoticYes.BackColor == Color.LightGray) updateChildSDA.cmm_bFormerDrug__c = "No";
                    //        if (btnChildAlcoholYes.BackColor == Color.Green) updateChildSDA.cmm_bAlcohol__c = "Yes";
                    //        else if (btnChildAlcoholYes.BackColor == Color.LightGray) updateChildSDA.cmm_bAlcohol__c = "No";

                    //        SForce.SaveResult[] updateChildSDAResults = Sfdcbinding.update(new SForce.sObject[] { updateChildSDA });

                    //        if (updateChildSDAResults[0].success)
                    //        {
                    //            // ChildSDA info updated successfully
                    //        }

                    //    }
                    //}
                }
            }
            else if (btnChildCurrentSmokerYes.BackColor == Color.Red || btnChildNarcoticYes.BackColor == Color.Red || btnChildAlcoholYes.BackColor == Color.Red)
            {
                mpeChildSmokingDrug.Show();
            }
        }

        protected void btnEditSpouse_Click(object sender, EventArgs e)
        {

            InitializeSQLQueryForSpouse();

            InitializedSfdcbinding();

            SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);


            if (qrSpouse.size > 0)
            {
                SForce.Contact ctSpouse = (SForce.Contact)qrSpouse.records[0];

                Session["SpouseId"] = ctSpouse.Id;
                txtEditSpouseLastName.Text = ctSpouse.LastName;
                txtEditSpouseFirstName.Text = ctSpouse.FirstName;
                if (ctSpouse.MiddleName != string.Empty) txtEditSpouseMiddleName.Text = ctSpouse.MiddleName;
                DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;
                txtEditSpouseDateOfBirth.Text = dtSpouseBirthdate.ToString("MM/dd/yyyy");

                if (ctSpouse.cmm_Gender__c == "Female") rbListEditSpouseGender.SelectedIndex = 1;
                if (ctSpouse.cmm_Gender__c == "Male") rbListEditSpouseGender.SelectedIndex = 0;

                txtEditSpouseSSN.Text = ctSpouse.Social_Security_Number__c;
                txtEditSpouseSSN.Enabled = false;

                pnlEditSpouse.Visible = true;
                mpeEditSpouse.Show();
            }
        }

        protected void btnCancelSpouse_Click(object sender, EventArgs e)
        {
            //pnlEditSpouse.Visible = false;
            mpeEditSpouse.Hide();
        }



        protected void btnUpdateSpouse_Click(object sender, EventArgs e)
        {

            Set_Edit_Spouse_smoking_drug_alcohol_buttons();

            if (rfvEditSpouseLastName.IsValid &&
                rfvEditSpouseFirstName.IsValid &&
                rfvEditSpouseDateOfBirth.IsValid &&
                rfvEditSpouseGender.IsValid &&
                btnEditSpouseCurrentSmokerYes.BackColor == Color.LightGray &&
                btnEditSpouseNarcoticYes.BackColor == Color.LightGray &&
                btnEditSpouseAlcoholYes.BackColor == Color.LightGray)
            {
                SForce.Contact ctUpdateSpouse = new SForce.Contact();

                String strSpouseId = (String)Session["SpouseId"];
                ctUpdateSpouse.Id = strSpouseId;
                ctUpdateSpouse.LastName = txtEditSpouseLastName.Text;
                ctUpdateSpouse.FirstName = txtEditSpouseFirstName.Text;
                if (txtEditSpouseMiddleName.Text == String.Empty)
                {
                    String[] strEmpty = { "MiddleName" };
                    ctUpdateSpouse.fieldsToNull = strEmpty;
                }
                else ctUpdateSpouse.MiddleName = txtEditSpouseMiddleName.Text;

                String[] arraySpouseBirthdate = txtEditSpouseDateOfBirth.Text.Split('/');

                int nSpouseBirthMonth = Int32.Parse(arraySpouseBirthdate[0]);
                int nSpouseBirthDay = Int32.Parse(arraySpouseBirthdate[1]);
                int nSpouseBirthYear = Int32.Parse(arraySpouseBirthdate[2]);

                ctUpdateSpouse.Birthdate = new DateTime(nSpouseBirthYear, nSpouseBirthMonth, nSpouseBirthDay);
                ctUpdateSpouse.BirthdateSpecified = true;
                ctUpdateSpouse.cmm_Gender__c = rbListEditSpouseGender.SelectedItem.Text;

                //InitializedSfdcbinding();

                SForce.SaveResult[] updateResults = Sfdcbinding.update(new SForce.sObject[] { ctUpdateSpouse });

                if (updateResults[0].success)
                {
                    lblUpdateResult.Text = "Spouse info updated";
                    //btnCancelSpouse.Text = "Close";
                    mpeEditSpouse.Hide();
                    DisplaySpouse();
                }
                else
                {
                    lblUpdateResult.Text = updateResults[0].errors[0].message;
                }

                //MemberSmokingDrugAlcohol updateSpouseSDA = new MemberSmokingDrugAlcohol();
                //updateSpouseSDA.HouseholdRole = HouseholdRoles.Spouse;
                //updateSpouseSDA.ContactId = strSpouseId;
                //updateSpouseSDA.Name = txtEditSpouseFirstName.Text + " " + txtEditSpouseLastName.Text;
                //updateSpouseSDA.bCurrentSmoker = false;
                //updateSpouseSDA.bCurrentDrug = false;
                //updateSpouseSDA.bAlcohol = false;

                //if (btnEditSpouseFormerSmokerYes.BackColor == Color.Green) updateSpouseSDA.bFormerSmoker = true;
                //else if (btnEditSpouseFormerSmokerYes.BackColor == Color.LightGray) updateSpouseSDA.bFormerSmoker = false;
                //if (btnEditSpouseFormerNarcoticYes.BackColor == Color.Green) updateSpouseSDA.bFormerDrug = true;
                //else if (btnEditSpouseFormerNarcoticYes.BackColor == Color.LightGray) updateSpouseSDA.bFormerDrug = false;

                //foreach (MemberSmokingDrugAlcohol sda in lstMemberSDA)
                //{
                //    if (sda.HouseholdRole == HouseholdRoles.Spouse)
                //    {
                //        lstMemberSDA.RemoveAt(lstMemberSDA.IndexOf(sda));
                //        lstMemberSDA.Insert(lstMemberSDA.IndexOf(sda), updateSpouseSDA);
                //    }
                //}
                //Session["MemberSmokingDrugAlcohol"] = lstMemberSDA;

                String strQueryForSpouseSDA = "select Id from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
                                              "and cmm_Contact__c = '" + strSpouseId + "' and cmm_Household_Role__c = 'Spouse'";

                SForce.QueryResult qrSpouseSDA = Sfdcbinding.query(strQueryForSpouseSDA);

                if (qrSpouseSDA.size > 0)
                {
                    SForce.tmp_SmokingDrugAlcohol__c spouseSDA = qrSpouseSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    SForce.tmp_SmokingDrugAlcohol__c updateTmpSpouseSDA = new SForce.tmp_SmokingDrugAlcohol__c();

                    updateTmpSpouseSDA.Id = spouseSDA.Id;
                    updateTmpSpouseSDA.cmm_Name__c = txtEditSpouseFirstName.Text + " " + txtEditSpouseLastName.Text;
                    updateTmpSpouseSDA.cmm_Household_Role__c = "Spouse";
                    updateTmpSpouseSDA.cmm_bCurrentSmoker__c = "No";
                    updateTmpSpouseSDA.cmm_bCurrentDrug__c = "No";
                    updateTmpSpouseSDA.cmm_bAlcohol__c = "No";
                    if (btnEditSpouseFormerSmokerYes.BackColor == Color.Green) updateTmpSpouseSDA.cmm_bFormerSmoker__c = "Yes";
                    else if (btnEditSpouseFormerSmokerYes.BackColor == Color.LightGray) updateTmpSpouseSDA.cmm_bFormerSmoker__c = "No";
                    if (btnEditSpouseFormerNarcoticYes.BackColor == Color.Green) updateTmpSpouseSDA.cmm_bFormerDrug__c = "Yes";
                    else if (btnEditSpouseFormerNarcoticYes.BackColor == Color.LightGray) updateTmpSpouseSDA.cmm_bFormerDrug__c = "No";

                    SForce.SaveResult[] updateResultsTmpSpouseSDA = Sfdcbinding.update(new SForce.sObject[] { updateTmpSpouseSDA });

                    if (updateResultsTmpSpouseSDA[0].success)
                    {
                        // update succeeded
                    }
                }
            }
            else
            {
                //mpeSpouseSmokingDrug.Show();
                mpeUpdateSpouseSmokingDrugDrinking.Show();

            }
        }

        protected void btnDeleteSpouse_Click(object sender, EventArgs e)
        {
            mpeDeleteSpouseConfirmation.Show();
        }

        protected void btnDeleteSpouseYes_Click(object sender, EventArgs e)
        {

            String strQueryForSpouseId = "select Id from Contact where cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";

            InitializedSfdcbinding();

            SForce.QueryResult qrSpouseId = Sfdcbinding.query(strQueryForSpouseId);

            if (qrSpouseId.size > 0)
            {
                SForce.Contact ctSpouseId = (SForce.Contact)qrSpouseId.records[0];

                String[] strSpouseId = new String[] { ctSpouseId.Id };

                SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(strSpouseId);
                SForce.DeleteResult deleteResult = deleteResults[0];

                if (deleteResult.success)
                {
                    RefreshSpouse();
                }
                else
                {
                }
            }
        }

        protected void btnDeleteSpouseNo_Click(object sender, EventArgs e)
        {
            mpeDeleteSpouseConfirmation.Hide();
        }

        protected void btnChildDelete_Click(object sender, EventArgs e)
        {
            Button btnDelete = (Button)sender;

            String strChildContactId = btnDelete.CommandArgument.ToString();

            String[] ChildContactId = new String[] { strChildContactId };

            InitializedSfdcbinding();

            SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(ChildContactId);
            SForce.DeleteResult deleteResut = deleteResults[0];

            if (deleteResut.success)
            {
                DisplayChildren();

                if (gvChildren.Rows.Count == 0)
                {
                    btnRemoveChildren.Enabled = false;
                    upnlAddRemoveChildren.Update();
                }
            }

        }

        protected void btnChildEdit_Click(object sender, EventArgs e)
        {

            Button btnEditChild = (Button)sender;

            strChildId = btnEditChild.CommandArgument.ToString();
            //String strChildContactId = btnEditChild.CommandArgument.ToString();
            Session["EditChildId"] = strChildId;

            LoadChildInfo();
            mpeEditChild.Show();
            //mpeEditChild.Show();

            //strQueryForChildren = "select Id, LastName, FirstName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c from Contact" +
            //                      " where Id = '" + strChildId + "'" +
            //                      " and cmm_Household__r.Id = '" + strAccountId + "'" +
            //                      " and cmm_Household_Role__c = 'Child'";

            //InitializedSfdcbinding();

            //SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildren);

            ////lblEditResult.Text += "Inside EditChild method";

            //if (qrChild.size > 0)
            //{
            //    SForce.Contact ctChildToEdit = qrChild.records[0] as SForce.Contact;

            //    lblAddChild.Text = "Edit Child Infomation";
            //    txtEditChildLastName.Text = ctChildToEdit.LastName;
            //    txtEditChildFirstName.Text = ctChildToEdit.FirstName;
            //    if (ctChildToEdit.MiddleName != String.Empty) txtEditChildMiddleName.Text = ctChildToEdit.MiddleName;
            //    DateTime dtChildBirthdate = (DateTime)ctChildToEdit.Birthdate;
            //    txtEditChildDateOfBirth.Text = dtChildBirthdate.ToString("MM/dd/yyyy");
            //    if (ctChildToEdit.cmm_Gender__c == "Male") rbListEditChildGender.SelectedIndex = 0;
            //    if (ctChildToEdit.cmm_Gender__c == "Female") rbListEditChildGender.SelectedIndex = 1;
            //    txtEditChildSSN.Text = ctChildToEdit.Social_Security_Number__c;
            //    txtEditChildSSN.ReadOnly = true;

            //    String strQueryChildForSDA = "select cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c from tmp_SmokingDrugAlcohol__c " +
            //                  "where cmm_Account__c = '" + strAccountId + "' and cmm_Contact__c = '" + ctChildToEdit.Id + "'";

            //    SForce.QueryResult qrChildSDA = Sfdcbinding.query(strQueryChildForSDA);

            //    if (qrChildSDA.size > 0)
            //    {
            //        SForce.tmp_SmokingDrugAlcohol__c childSDA = qrChildSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

            //        if (childSDA.cmm_bCurrentSmoker__c == "Yes")
            //        {
            //            btnEditChildCurrentSmokerYes.BackColor = Color.Red;
            //            btnEditChildCurrentSmokerYes.ForeColor = Color.White;
            //            btnEditChildCurrentSmokerNo.BackColor = Color.LightGray;
            //            btnEditChildCurrentSmokerNo.ForeColor = Color.Black;
            //        }
            //        if (childSDA.cmm_bCurrentSmoker__c == "No")
            //        {
            //            btnEditChildCurrentSmokerYes.BackColor = Color.LightGray;
            //            btnEditChildCurrentSmokerYes.ForeColor = Color.Black;
            //            btnEditChildCurrentSmokerNo.BackColor = Color.Blue;
            //            btnEditChildCurrentSmokerNo.ForeColor = Color.White;
            //        }
            //        if (childSDA.cmm_bFormerSmoker__c == "Yes")
            //        {
            //            btnEditChildFormerSmokerYes.BackColor = Color.Green;
            //            btnEditChildFormerSmokerYes.ForeColor = Color.White;
            //            btnEditChildFormerSmokerNo.BackColor = Color.LightGray;
            //            btnEditChildFormerSmokerNo.ForeColor = Color.Black;
            //        }
            //        if (childSDA.cmm_bFormerSmoker__c == "No")
            //        {
            //            btnEditChildFormerSmokerYes.BackColor = Color.LightGray;
            //            btnEditChildFormerSmokerYes.ForeColor = Color.Black;
            //            btnEditChildFormerSmokerNo.BackColor = Color.Blue;
            //            btnEditChildFormerSmokerNo.ForeColor = Color.White;
            //        }
            //        if (childSDA.cmm_bCurrentDrug__c == "Yes")
            //        {
            //            btnEditChildNarcoticYes.BackColor = Color.Red;
            //            btnEditChildNarcoticYes.ForeColor = Color.White;
            //            btnEditChildNarcoticNo.BackColor = Color.LightGray;
            //            btnEditChildNarcoticNo.ForeColor = Color.Black;
            //        }
            //        if (childSDA.cmm_bCurrentDrug__c == "No")
            //        {
            //            btnEditChildNarcoticYes.BackColor = Color.LightGray;
            //            btnEditChildNarcoticYes.ForeColor = Color.Black;
            //            btnEditChildNarcoticNo.BackColor = Color.Blue;
            //            btnEditChildNarcoticNo.ForeColor = Color.White;
            //        }
            //        if (childSDA.cmm_bFormerDrug__c == "Yes")
            //        {
            //            btnEditChildFormerNarcoticYes.BackColor = Color.Green;
            //            btnEditChildFormerNarcoticYes.ForeColor = Color.White;
            //            btnEditChildFormerNarcoticNo.BackColor = Color.LightGray;
            //            btnEditChildFormerNarcoticNo.ForeColor = Color.Black;
            //        }
            //        if (childSDA.cmm_bFormerDrug__c == "No")
            //        {
            //            btnEditChildFormerNarcoticYes.BackColor = Color.LightGray;
            //            btnEditChildFormerNarcoticYes.ForeColor = Color.Black;
            //            btnEditChildFormerNarcoticNo.BackColor = Color.Blue;
            //            btnEditChildFormerNarcoticNo.ForeColor = Color.White;
            //        }
            //        if (childSDA.cmm_bAlcohol__c == "Yes")
            //        {
            //            btnEditChildAlcoholYes.BackColor = Color.Green;
            //            btnEditChildAlcoholYes.ForeColor = Color.White;
            //            btnEditChildAlcoholNo.BackColor = Color.LightGray;
            //            btnEditChildAlcoholNo.ForeColor = Color.Black;
            //        }
            //        if (childSDA.cmm_bAlcohol__c == "No")
            //        {
            //            btnEditChildAlcoholYes.BackColor = Color.LightGray;
            //            btnEditChildAlcoholYes.ForeColor = Color.Black;
            //            btnEditChildAlcoholNo.BackColor = Color.Blue;
            //            btnEditChildAlcoholNo.ForeColor = Color.White;
            //        }

            //        //Session["IsCreateChild"] = false;
            //        //Session["IsUpdateChild"] = true;
            //        //mpeAddChild.Show();
            //        //Session["EditChildId"] = strChildId;
            //        mpeEditChild.Show();
            //    }


            //    //mpeAddChild.Show();

            //}


            //if (qrChild.size > 0)
            //{

            //    SForce.Contact ctChildToEdit = (SForce.Contact)qrChild.records[0];

            //    txtEditChildLastName.Text = ctChildToEdit.LastName;
            //    txtEditChildFirstName.Text = ctChildToEdit.FirstName;
            //    if (ctChildToEdit.MiddleName != String.Empty) txtEditChildMiddleName.Text = ctChildToEdit.MiddleName;

            //    DateTime dtEditChildDateOfBirth = (DateTime)ctChildToEdit.Birthdate;

            //    String strChildDateOfBirth = dtEditChildDateOfBirth.ToString("MM/dd/yyyy");

            //    String[] strArryChildDateOfBirth = strChildDateOfBirth.Split('/');

            //    int nChildBirthYear = Int32.Parse(strArryChildDateOfBirth[2]);
            //    int nChildBirthMonth = Int32.Parse(strArryChildDateOfBirth[0]);
            //    int nChildBirthDay = Int32.Parse(strArryChildDateOfBirth[1]);

            //    DateTime dtChildDatOfBirth = new DateTime(nChildBirthYear, nChildBirthMonth, nChildBirthDay);

            //    txtEditChildDateOfBirth.Text = dtEditChildDateOfBirth.ToString("MM/dd/yyy");

            //    if (ctChildToEdit.cmm_Gender__c == "Male") rbListEditChildGender.SelectedIndex = 0;
            //    if (ctChildToEdit.cmm_Gender__c == "Female") rbListEditChildGender.SelectedIndex = 1;

            //    txtEditChildSSN.Text = ctChildToEdit.Social_Security_Number__c;
            //    txtEditChildSSN.Enabled = false;

            //    mpeEditChild.Show();

            //}
        }

        protected void LoadChildInfoNoSDA()
        {
            String strChildId = String.Empty;
            if ((String)Session["EditChildId"] != null) strChildId = (String)Session["EditChildId"];

            strQueryForChildren = "select Id, LastName, FirstName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c from Contact" +
                                  " where Id = '" + strChildId + "'" +
                                  " and cmm_Household__r.Id = '" + strAccountId + "'" +
                                  " and cmm_Household_Role__c = 'Child'";

            InitializedSfdcbinding();

            SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildren);

            //lblEditResult.Text += "Inside EditChild method";

            if (qrChild.size > 0)
            {
                SForce.Contact ctChildToEdit = qrChild.records[0] as SForce.Contact;

                lblAddChild.Text = "Edit Child Infomation";
                txtEditChildLastName.Text = ctChildToEdit.LastName;
                txtEditChildFirstName.Text = ctChildToEdit.FirstName;
                if (ctChildToEdit.MiddleName != String.Empty) txtEditChildMiddleName.Text = ctChildToEdit.MiddleName;
                DateTime dtChildBirthdate = (DateTime)ctChildToEdit.Birthdate;
                txtEditChildDateOfBirth.Text = dtChildBirthdate.ToString("MM/dd/yyyy");
                if (ctChildToEdit.cmm_Gender__c == "Male") rbListEditChildGender.SelectedIndex = 0;
                if (ctChildToEdit.cmm_Gender__c == "Female") rbListEditChildGender.SelectedIndex = 1;
                txtEditChildSSN.Text = ctChildToEdit.Social_Security_Number__c;
                txtEditChildSSN.ReadOnly = true;

                mpeEditChild.Show();
            }
        }

        protected void LoadChildInfo()
        {

            //Session["EditChildId"] = strChildId;

            String strChildId = String.Empty;
            if ((String)Session["EditChildId"] != null) strChildId = (String)Session["EditChildId"];

            strQueryForChildren = "select Id, LastName, FirstName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c from Contact " +
                                  "where Id = '" + strChildId + "' " +
                                  "and cmm_Household__r.Id = '" + strAccountId + "' " +
                                  "and cmm_Household_Role__c = 'Child'";

            InitializedSfdcbinding();

            SForce.QueryResult qrChild = Sfdcbinding.query(strQueryForChildren);

            //lblEditResult.Text += "Inside EditChild method";

            if (qrChild.size > 0)
            {
                SForce.Contact ctChildToEdit = qrChild.records[0] as SForce.Contact;

                //lblAddChild.Text = "Edit Child Infomation";
                txtEditChildLastName.Text = ctChildToEdit.LastName;
                txtEditChildFirstName.Text = ctChildToEdit.FirstName;
                if (ctChildToEdit.MiddleName != String.Empty) txtEditChildMiddleName.Text = ctChildToEdit.MiddleName;
                DateTime dtChildBirthdate = (DateTime)ctChildToEdit.Birthdate;
                txtEditChildDateOfBirth.Text = dtChildBirthdate.ToString("MM/dd/yyyy");
                if (ctChildToEdit.cmm_Gender__c == "Male") rbListEditChildGender.SelectedIndex = 0;
                if (ctChildToEdit.cmm_Gender__c == "Female") rbListEditChildGender.SelectedIndex = 1;
                txtEditChildSSN.Text = ctChildToEdit.Social_Security_Number__c;
                txtEditChildSSN.ReadOnly = true;

                String strQueryChildForSDA = "select cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c from tmp_SmokingDrugAlcohol__c " +
                              "where cmm_Account__c = '" + strAccountId + "' and cmm_Contact__c = '" + ctChildToEdit.Id + "'";

                SForce.QueryResult qrChildSDA = Sfdcbinding.query(strQueryChildForSDA);

                if (qrChildSDA.size > 0)
                {
                    SForce.tmp_SmokingDrugAlcohol__c childSDA = qrChildSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    if (childSDA.cmm_bCurrentSmoker__c == "Yes")
                    {
                        btnEditChildCurrentSmokerYes.BackColor = Color.Red;
                        btnEditChildCurrentSmokerYes.ForeColor = Color.White;
                        hdnEditChildCurrentSmokerYes.Value = "red";
                        btnEditChildCurrentSmokerNo.BackColor = Color.LightGray;
                        btnEditChildCurrentSmokerNo.ForeColor = Color.Black;
                        hdnEditChildCurrentSmokerNo.Value = "lightgrey";
                    }
                    if (childSDA.cmm_bCurrentSmoker__c == "No")
                    {
                        btnEditChildCurrentSmokerYes.BackColor = Color.LightGray;
                        btnEditChildCurrentSmokerYes.ForeColor = Color.Black;
                        hdnEditChildCurrentSmokerYes.Value = "lightgrey";
                        btnEditChildCurrentSmokerNo.BackColor = Color.Blue;
                        btnEditChildCurrentSmokerNo.ForeColor = Color.White;
                        hdnEditChildCurrentSmokerNo.Value = "blue";
                    }
                    if (childSDA.cmm_bFormerSmoker__c == "Yes")
                    {
                        btnEditChildFormerSmokerYes.BackColor = Color.Green;
                        btnEditChildFormerSmokerYes.ForeColor = Color.White;
                        hdnEditChildFormerSmokerYes.Value = "green";
                        btnEditChildFormerSmokerNo.BackColor = Color.LightGray;
                        btnEditChildFormerSmokerNo.ForeColor = Color.Black;
                        hdnEditChildFormerSmokerNo.Value = "lightgrey";
                    }
                    if (childSDA.cmm_bFormerSmoker__c == "No")
                    {
                        btnEditChildFormerSmokerYes.BackColor = Color.LightGray;
                        btnEditChildFormerSmokerYes.ForeColor = Color.Black;
                        hdnEditChildFormerSmokerYes.Value = "lightgrey";
                        btnEditChildFormerSmokerNo.BackColor = Color.Blue;
                        btnEditChildFormerSmokerNo.ForeColor = Color.White;
                        hdnEditChildFormerSmokerNo.Value = "blue";
                    }
                    if (childSDA.cmm_bCurrentDrug__c == "Yes")
                    {
                        btnEditChildNarcoticYes.BackColor = Color.Red;
                        btnEditChildNarcoticYes.ForeColor = Color.White;
                        hdnEditChildNarcoticYes.Value = "red";
                        btnEditChildNarcoticNo.BackColor = Color.LightGray;
                        btnEditChildNarcoticNo.ForeColor = Color.Black;
                        hdnEditChildNarcoticNo.Value = "lightgrey";
                    }
                    if (childSDA.cmm_bCurrentDrug__c == "No")
                    {
                        btnEditChildNarcoticYes.BackColor = Color.LightGray;
                        btnEditChildNarcoticYes.ForeColor = Color.Black;
                        hdnEditChildNarcoticYes.Value = "lightgrey";
                        btnEditChildNarcoticNo.BackColor = Color.Blue;
                        btnEditChildNarcoticNo.ForeColor = Color.White;
                        hdnEditChildNarcoticNo.Value = "blue";
                    }
                    if (childSDA.cmm_bFormerDrug__c == "Yes")
                    {
                        btnEditChildFormerNarcoticYes.BackColor = Color.Green;
                        btnEditChildFormerNarcoticYes.ForeColor = Color.White;
                        hdnEditChildFormerNarcoticYes.Value = "green";
                        btnEditChildFormerNarcoticNo.BackColor = Color.LightGray;
                        btnEditChildFormerNarcoticNo.ForeColor = Color.Black;
                        hdnEditChildFormerNarcoticNo.Value = "lightgrey";
                    }
                    if (childSDA.cmm_bFormerDrug__c == "No")
                    {
                        btnEditChildFormerNarcoticYes.BackColor = Color.LightGray;
                        btnEditChildFormerNarcoticYes.ForeColor = Color.Black;
                        hdnEditChildFormerNarcoticYes.Value = "lightgrey";
                        btnEditChildFormerNarcoticNo.BackColor = Color.Blue;
                        btnEditChildFormerNarcoticNo.ForeColor = Color.White;
                        hdnEditChildFormerNarcoticNo.Value = "blue";
                    }
                    if (childSDA.cmm_bAlcohol__c == "Yes")
                    {
                        btnEditChildAlcoholYes.BackColor = Color.Red;
                        btnEditChildAlcoholYes.ForeColor = Color.White;
                        hdnEditChildAlcoholYes.Value = "red";
                        btnEditChildAlcoholNo.BackColor = Color.LightGray;
                        btnEditChildAlcoholNo.ForeColor = Color.Black;
                        hdnEditChildAlcoholNo.Value = "lightgrey";
                    }
                    if (childSDA.cmm_bAlcohol__c == "No")
                    {
                        btnEditChildAlcoholYes.BackColor = Color.LightGray;
                        btnEditChildAlcoholYes.ForeColor = Color.Black;
                        hdnEditChildAlcoholYes.Value = "lightgrey";
                        btnEditChildAlcoholNo.BackColor = Color.Blue;
                        btnEditChildAlcoholNo.ForeColor = Color.White;
                        hdnEditChildAlcoholNo.Value = "blue";
                    }

                    //Session["IsCreateChild"] = false;
                    //Session["IsUpdateChild"] = true;
                    //mpeAddChild.Show();
                    //Session["EditChildId"] = strChildId;

                    //txtEditChildLastName.Text = ctChildToEdit.LastName;
                    //txtEditChildFirstName.Text = ctChildToEdit.FirstName;
                    //if (ctChildToEdit.MiddleName != String.Empty) txtEditChildMiddleName.Text = ctChildToEdit.MiddleName;
                    //DateTime dtChildBirthdate = (DateTime)ctChildToEdit.Birthdate;
                    //txtEditChildDateOfBirth.Text = dtChildBirthdate.ToString("MM/dd/yyyy");
                    //if (ctChildToEdit.cmm_Gender__c == "Male") rbListEditChildGender.SelectedIndex = 0;
                    //if (ctChildToEdit.cmm_Gender__c == "Female") rbListEditChildGender.SelectedIndex = 1;
                    //txtEditChildSSN.Text = ctChildToEdit.Social_Security_Number__c;
                    //txtEditChildSSN.ReadOnly = true;

                    //Session["EditChildLastName"] = txtEditChildLastName.Text;
                    //Session["EditChildFirstName"] = txtEditChildFirstName.Text;
                    //Session["EditChildMiddleName"] = txtEditChildMiddleName.Text;
                    //Session["EditChildBirthdate"] = txtEditChildDateOfBirth.Text;
                    //Session["EditChildGender"] = rbListChildGender.SelectedValue;
                    //Session["EditChildSSN"] = txtEditChildSSN.Text;

                    //mpeEditChild.Show();
                }
            }
        }
        protected void btnEditChildCancel_Click(object sender, EventArgs e)
        {
            //if ((String)Session["EditChildLastName"] != String.Empty) txtEditChildLastName.Text = (String)Session["EditChildLastName"];
            //txtEditChildFirstName.Text = (String)Session["EditChildFirstName"];
            //txtEditChildMiddleName.Text = (String)Session["EditChildMiddleName"];
            //txtEditChildDateOfBirth.Text = (String)Session["EditChildBirthdate"];
            //rbListChildGender.SelectedValue = (String)Session["EditChildGender"];
            //txtEditChildSSN.Text = (String)Session["EditChildSSN"];


            //mpeEditChild.Hide();
        }

        protected void btnEditChildUpdateConfirm_Click(object sender, EventArgs e)
        {
            //String strUpdateChildId = (String)Session["ChildId"];
            Set_Edit_Child_smoking_drug_alcohol_buttons();

            if (rfvEditChildLastName.IsValid &&
                rfvEditChildFirstName.IsValid &&
                rfvEditChildDateOfBirth.IsValid &&
                rfvEditChildGender.IsValid &&
                btnEditChildCurrentSmokerYes.BackColor == Color.LightGray &&
                btnEditChildNarcoticYes.BackColor == Color.LightGray &&
                btnEditChildAlcoholYes.BackColor == Color.LightGray)
            {
                String strUpdateChildId = String.Empty;
                if ((String)Session["EditChildId"] != null) strUpdateChildId = (String)Session["EditChildId"];
                SForce.Contact ctUpdateChild = new SForce.Contact();

                ctUpdateChild.Id = strUpdateChildId;
                ctUpdateChild.LastName = txtEditChildLastName.Text;
                ctUpdateChild.FirstName = txtEditChildFirstName.Text;
                if (txtEditChildMiddleName.Text == String.Empty)
                {
                    String[] strEmpty = { "MiddleName" };
                    ctUpdateChild.fieldsToNull = strEmpty;
                }
                else ctUpdateChild.MiddleName = txtEditChildMiddleName.Text;

                String[] strArryUpdateChildrenBirthdate = txtEditChildDateOfBirth.Text.Split('/');

                int nUpdateChildBirthMonth = Int32.Parse(strArryUpdateChildrenBirthdate[0]);
                int nUpdateChildBirthDay = Int32.Parse(strArryUpdateChildrenBirthdate[1]);
                int nUpdateChildBirthYear = Int32.Parse(strArryUpdateChildrenBirthdate[2]);

                ctUpdateChild.Birthdate = new DateTime(nUpdateChildBirthYear, nUpdateChildBirthMonth, nUpdateChildBirthDay);
                ctUpdateChild.BirthdateSpecified = true;
                ctUpdateChild.cmm_Gender__c = rbListEditChildGender.SelectedItem.Text;

                //if (btnEditChildCurrentSmokerYes.BackColor == Color.LightGray && btnEditChildCurrentSmokerYes.ForeColor == Color.Black)

                InitializedSfdcbinding();

                SForce.SaveResult[] updateChildResults = Sfdcbinding.update(new SForce.sObject[] { ctUpdateChild });

                if (updateChildResults[0].success)
                {
                    //btnEditChildCancel.Text = "Close";
                    mpeEditChild.Hide();
                    DisplayChildren();

                    //mpeAddChild.Hide();
                    //DisplayChildren();
                    InitializeAddChildFields();
                    btnRemoveChildren.Enabled = true;
                    upnlAddRemoveChildren.Update();
                    strChildId = updateChildResults[0].id;
                    lstChildId.Add(updateChildResults[0].id);
                    Session["ChildIds"] = lstChildId;

                    /////////////////////////////////////////////////
                    //////////////////////////////////////////////////
                }

                //MemberSmokingDrugAlcohol updateChildSDA = new MemberSmokingDrugAlcohol();
            
                //updateChildSDA.HouseholdRole = HouseholdRoles.Child;
                //updateChildSDA.ContactId = strUpdateChildId;
                //updateChildSDA.Name = txtEditChildFirstName.Text + " " + txtEditChildLastName.Text;
                //updateChildSDA.bCurrentSmoker = false;
                //updateChildSDA.bCurrentDrug = false;
                //updateChildSDA.bAlcohol = false;

                //if (btnEditChildFormerSmokerYes.BackColor == Color.Green) updateChildSDA.bFormerSmoker = true;
                //else if (btnEditChildFormerSmokerYes.BackColor == Color.LightGray) updateChildSDA.bFormerSmoker = false;
                //if (btnEditChildFormerNarcoticYes.BackColor == Color.Green) updateChildSDA.bFormerDrug = true;
                //else if (btnEditChildFormerNarcoticYes.BackColor == Color.LightGray) updateChildSDA.bFormerDrug = false;

                //foreach (MemberSmokingDrugAlcohol sda in lstMemberSDA)
                //{
                //    if (sda.HouseholdRole == HouseholdRoles.Child && sda.ContactId == updateChildSDA.ContactId)
                //    {
                //        lstMemberSDA.RemoveAt(lstMemberSDA.IndexOf(sda));
                //        lstMemberSDA.Insert(lstMemberSDA.IndexOf(sda), updateChildSDA);
                //    }
                //}
                //Session["MemberSmokingDrugAlcohol"] = lstMemberSDA;

                String strQueryForChildSDA = "select Id, cmm_Name__c from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
                                 "and cmm_Household_Role__c = 'Child' and cmm_Contact__c = '" + strUpdateChildId + "'";

                SForce.QueryResult qrEditChildSDA = Sfdcbinding.query(strQueryForChildSDA);
                if (qrEditChildSDA.size > 0)
                {
                    //Set_Edit_Child_smoking_drug_alcohol_buttons();
                    SForce.tmp_SmokingDrugAlcohol__c tmpChildSDAId = qrEditChildSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    SForce.tmp_SmokingDrugAlcohol__c tmpUpdateChildSDA = new SForce.tmp_SmokingDrugAlcohol__c();
                    tmpUpdateChildSDA.Id = tmpChildSDAId.Id;

                    tmpUpdateChildSDA.cmm_bCurrentSmoker__c = "No";
                    tmpUpdateChildSDA.cmm_bCurrentDrug__c = "No";
                    tmpUpdateChildSDA.cmm_bAlcohol__c = "No";

                    // 11/06/17 begin here
                    if (btnEditChildFormerSmokerYes.BackColor == Color.LightGray && btnEditChildFormerSmokerNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bFormerSmoker__c = "No";
                    if (btnEditChildFormerSmokerYes.BackColor == Color.Green && btnEditChildFormerSmokerNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bFormerSmoker__c = "Yes";
                    if (btnEditChildFormerNarcoticYes.BackColor == Color.LightGray && btnEditChildFormerNarcoticNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bFormerDrug__c = "No";
                    if (btnEditChildFormerNarcoticYes.BackColor == Color.Green && btnEditChildFormerNarcoticNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bFormerDrug__c = "Yes";
                    //if (btnEditChildAlcoholYes.BackColor == Color.LightGray && btnEditChildAlcoholNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bAlcohol__c = "No";
                    //if (btnEditChildAlcoholYes.BackColor == Color.Green && btnEditChildAlcoholNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bAlcohol__c = "Yes";

                    SForce.SaveResult[] updateResultsChildSDA = Sfdcbinding.update(new SForce.sObject[] { tmpUpdateChildSDA });
                    if (updateResultsChildSDA[0].success)
                    {
                        // Child SDA update succeeded
                    }
                }
            }
            else
            {
                mpeEditChildSmokingDrugDrink.Show();
            }

            //if (btnEditChildCurrentSmokerYes.BackColor == Color.Red || btnEditChildNarcoticYes.BackColor == Color.Red || btnEditChildAlcoholYes.BackColor == Color.Red)
            //{
            //    //mpeChildSmokingDrug.Show();
            //    mpeEditChildSmokingDrugDrink.Show();
            //}
            //else
            //{

            //    String strUpdateChildId = String.Empty;
            //    if ((String)Session["EditChildId"] != null) strUpdateChildId = (String)Session["EditChildId"];
            //    SForce.Contact ctUpdateChild = new SForce.Contact();

            //    ctUpdateChild.Id = strUpdateChildId;
            //    ctUpdateChild.LastName = txtEditChildLastName.Text;
            //    ctUpdateChild.FirstName = txtEditChildFirstName.Text;
            //    if (txtEditChildMiddleName.Text == String.Empty)
            //    {
            //        String[] strEmpty = { "MiddleName" };
            //        ctUpdateChild.fieldsToNull = strEmpty;
            //    }
            //    else ctUpdateChild.MiddleName = txtEditChildMiddleName.Text;

            //    String[] strArryUpdateChildrenBirthdate = txtEditChildDateOfBirth.Text.Split('/');

            //    int nUpdateChildBirthMonth = Int32.Parse(strArryUpdateChildrenBirthdate[0]);
            //    int nUpdateChildBirthDay = Int32.Parse(strArryUpdateChildrenBirthdate[1]);
            //    int nUpdateChildBirthYear = Int32.Parse(strArryUpdateChildrenBirthdate[2]);

            //    ctUpdateChild.Birthdate = new DateTime(nUpdateChildBirthYear, nUpdateChildBirthMonth, nUpdateChildBirthDay);
            //    ctUpdateChild.BirthdateSpecified = true;
            //    ctUpdateChild.cmm_Gender__c = rbListEditChildGender.SelectedItem.Text;

            //    //if (btnEditChildCurrentSmokerYes.BackColor == Color.LightGray && btnEditChildCurrentSmokerYes.ForeColor == Color.Black)

            //    InitializedSfdcbinding();

            //    SForce.SaveResult[] updateChildResults = Sfdcbinding.update(new SForce.sObject[] { ctUpdateChild });

            //    if (updateChildResults[0].success)
            //    {
            //        //btnEditChildCancel.Text = "Close";
            //        mpeEditChild.Hide();
            //        DisplayChildren();

            //        //mpeAddChild.Hide();
            //        //DisplayChildren();
            //        InitializeAddChildFields();
            //        btnRemoveChildren.Enabled = true;
            //        upnlAddRemoveChildren.Update();
            //        strChildId = updateChildResults[0].id;
            //        lstChildId.Add(updateChildResults[0].id);
            //        Session["ChildIds"] = lstChildId;

            //        /////////////////////////////////////////////////
            //        //////////////////////////////////////////////////
            //    }

            //    String strQueryForChildSDA = "select Id, cmm_Name__c from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
            //                     "and cmm_Household_Role__c = 'Child' and cmm_Contact__c = '" + strUpdateChildId + "'";

            //    SForce.QueryResult qrEditChildSDA = Sfdcbinding.query(strQueryForChildSDA);
            //    if (qrEditChildSDA.size > 0)
            //    {
            //        //Set_Edit_Child_smoking_drug_alcohol_buttons();
            //        SForce.tmp_SmokingDrugAlcohol__c tmpChildSDAId = qrEditChildSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

            //        SForce.tmp_SmokingDrugAlcohol__c tmpUpdateChildSDA = new SForce.tmp_SmokingDrugAlcohol__c();
            //        tmpUpdateChildSDA.Id = tmpChildSDAId.Id;


            //        tmpUpdateChildSDA.cmm_bCurrentSmoker__c = "No";
            //        tmpUpdateChildSDA.cmm_bCurrentDrug__c = "No";


            //        // 11/06/17 begin here
            //        if (btnEditChildFormerSmokerYes.BackColor == Color.LightGray && btnEditChildFormerSmokerNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bFormerSmoker__c = "No";
            //        if (btnEditChildFormerSmokerYes.BackColor == Color.Green && btnEditChildFormerSmokerNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bFormerSmoker__c = "Yes";
            //        if (btnEditChildFormerNarcoticYes.BackColor == Color.LightGray && btnEditChildFormerNarcoticNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bFormerDrug__c = "No";
            //        if (btnEditChildFormerNarcoticYes.BackColor == Color.Green && btnEditChildFormerNarcoticNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bFormerDrug__c = "Yes";
            //        if (btnEditChildAlcoholYes.BackColor == Color.LightGray && btnEditChildAlcoholNo.BackColor == Color.Blue) tmpUpdateChildSDA.cmm_bAlcohol__c = "No";
            //        if (btnEditChildAlcoholYes.BackColor == Color.Green && btnEditChildAlcoholNo.BackColor == Color.LightGray) tmpUpdateChildSDA.cmm_bAlcohol__c = "Yes";

            //        SForce.SaveResult[] updateChildSDA = Sfdcbinding.update(new SForce.sObject[] { tmpUpdateChildSDA });
            //        if (updateChildSDA[0].success)
            //        {
            //            // Child SDA update succeeded
            //        }
            //    }
            //}
            //}
        }


        protected void LoadEditSpouseInfo()
        {
            String strSpouseId = String.Empty;

            if ((String)Session["SpouseId"] != null) strSpouseId = (String)Session["SpouseId"];

            SForce.QueryResult qrEditSpouseInfo = Sfdcbinding.query(strQueryForSpouse);

            if (qrEditSpouseInfo.size > 0)
            {
                SForce.Contact ctEditSpouseInfo = qrEditSpouseInfo.records[0] as SForce.Contact;

                txtEditSpouseLastName.Text = ctEditSpouseInfo.LastName;
                txtEditSpouseFirstName.Text = ctEditSpouseInfo.FirstName;
                if (ctEditSpouseInfo.MiddleName != String.Empty) txtEditSpouseMiddleName.Text = ctEditSpouseInfo.MiddleName;
                DateTime dtEditSpouseBirthdate = (DateTime)ctEditSpouseInfo.Birthdate;
                txtEditSpouseDateOfBirth.Text = dtEditSpouseBirthdate.ToString("MM/dd/yyyy");

                if (ctEditSpouseInfo.cmm_Gender__c == "Male") rbListEditSpouseGender.SelectedIndex = 0;
                if (ctEditSpouseInfo.cmm_Gender__c == "Female") rbListEditSpouseGender.SelectedIndex = 1;

                txtEditSpouseSSN.Text = ctEditSpouseInfo.Social_Security_Number__c;
                txtEditSpouseSSN.ReadOnly = true;
                revEditSpouseSSN.Enabled = false;

                String strQuerySpouseForSDA = "select cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c " +
                                              "from tmp_SmokingDrugAlcohol__c where cmm_Account__c = '" + strAccountId + "' " +
                                              "and cmm_Contact__c = '" + strSpouseId + "'";

                SForce.QueryResult qrEditSpouseSDA = Sfdcbinding.query(strQuerySpouseForSDA);

                if (qrEditSpouseSDA.size > 0)
                {
                    SForce.tmp_SmokingDrugAlcohol__c edtSpouseSDA = qrEditSpouseSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    if (edtSpouseSDA.cmm_bCurrentSmoker__c == "Yes")
                    {
                        btnEditSpouseCurrentSmokerYes.BackColor = Color.Red;
                        btnEditSpouseCurrentSmokerYes.ForeColor = Color.White;
                        hdnEditSpouseCurrentSmokerYes.Value = "red";
                        btnEditSpouseCurrentSmokerNo.BackColor = Color.LightGray;
                        btnEditSpouseCurrentSmokerNo.ForeColor = Color.Black;
                        hdnEditSpouseCurrentSmokerNo.Value = "lightgrey";
                    }
                    if (edtSpouseSDA.cmm_bCurrentSmoker__c == "No")
                    {
                        btnEditSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                        btnEditSpouseCurrentSmokerYes.ForeColor = Color.Black;
                        hdnEditSpouseCurrentSmokerYes.Value = "lightgrey";
                        btnEditSpouseCurrentSmokerNo.BackColor = Color.Blue;
                        btnEditSpouseCurrentSmokerNo.ForeColor = Color.White;
                        hdnEditSpouseCurrentSmokerNo.Value = "blue";
                    }
                    if (edtSpouseSDA.cmm_bFormerSmoker__c == "Yes")
                    {
                        btnEditSpouseFormerSmokerYes.BackColor = Color.Green;
                        btnEditSpouseFormerSmokerYes.ForeColor = Color.White;
                        hdnEditSpouseFormerSmokerYes.Value = "green";
                        btnEditSpouseFormerSmokerNo.BackColor = Color.LightGray;
                        btnEditSpouseFormerSmokerNo.ForeColor = Color.Black;
                        hdnEditSpouseFormerSmokerNo.Value = "lightgrey";
                    }
                    if (edtSpouseSDA.cmm_bFormerSmoker__c == "No")
                    {
                        btnEditSpouseFormerSmokerYes.BackColor = Color.LightGray;
                        btnEditSpouseFormerSmokerYes.ForeColor = Color.Black;
                        hdnEditSpouseFormerSmokerYes.Value = "lightgrey";
                        btnEditSpouseFormerSmokerNo.BackColor = Color.Blue;
                        btnEditSpouseFormerSmokerNo.ForeColor = Color.White;
                        hdnEditSpouseFormerSmokerNo.Value = "blue";
                    }
                    if (edtSpouseSDA.cmm_bCurrentDrug__c == "Yes")
                    {
                        btnEditSpouseNarcoticYes.BackColor = Color.Red;
                        btnEditSpouseNarcoticYes.ForeColor = Color.White;
                        hdnEditSpouseNarcoticYes.Value = "red";
                        btnEditSpouseNarcoticNo.BackColor = Color.LightGray;
                        btnEditSpouseNarcoticNo.ForeColor = Color.Black;
                        hdnEditSpouseNarcoticNo.Value = "lightgrey";
                    }
                    if (edtSpouseSDA.cmm_bCurrentDrug__c == "No")
                    {
                        btnEditSpouseNarcoticYes.BackColor = Color.LightGray;
                        btnEditSpouseNarcoticYes.ForeColor = Color.Black;
                        hdnEditSpouseNarcoticYes.Value = "lightgrey";
                        btnEditSpouseNarcoticNo.BackColor = Color.Blue;
                        btnEditSpouseNarcoticNo.ForeColor = Color.White;
                        hdnEditSpouseNarcoticNo.Value = "blue";
                    }
                    if (edtSpouseSDA.cmm_bFormerDrug__c == "Yes")
                    {
                        btnEditSpouseFormerNarcoticYes.BackColor = Color.Green;
                        btnEditSpouseFormerNarcoticYes.ForeColor = Color.White;
                        hdnEditSpouseFormerNarcoticYes.Value = "green";
                        btnEditSpouseFormerNarcoticNo.BackColor = Color.LightGray;
                        btnEditSpouseFormerNarcoticNo.ForeColor = Color.Black;
                        hdnEditSpouseFormerNarcoticNo.Value = "lightgrey";
                    }
                    if (edtSpouseSDA.cmm_bFormerDrug__c == "No")
                    {
                        btnEditSpouseFormerNarcoticYes.BackColor = Color.LightGray;
                        btnEditSpouseFormerNarcoticYes.ForeColor = Color.Black;
                        hdnEditSpouseFormerNarcoticYes.Value = "lightgrey";
                        btnEditSpouseFormerNarcoticNo.BackColor = Color.Blue;
                        btnEditSpouseFormerNarcoticNo.ForeColor = Color.White;
                        hdnEditSpouseFormerNarcoticNo.Value = "blue";
                    }
                    if (edtSpouseSDA.cmm_bAlcohol__c == "Yes")
                    {
                        btnEditSpouseAlcoholYes.BackColor = Color.Red;
                        btnEditSpouseAlcoholYes.ForeColor = Color.White;
                        hdnEditSpouseAlcoholYes.Value = "red";
                        btnEditSpouseAlcoholNo.BackColor = Color.LightGray;
                        btnEditSpouseAlcoholNo.ForeColor = Color.Black;
                        hdnEditSpouseAlcoholNo.Value = "lightgrey";
                    }
                    if (edtSpouseSDA.cmm_bAlcohol__c == "No")
                    {
                        btnEditSpouseAlcoholYes.BackColor = Color.LightGray;
                        btnEditSpouseAlcoholYes.ForeColor = Color.Black;
                        hdnEditSpouseAlcoholYes.Value = "lightgrey";
                        btnEditSpouseAlcoholNo.BackColor = Color.Blue;
                        btnEditSpouseAlcoholNo.ForeColor = Color.White;
                        hdnEditSpouseAlcoholNo.Value = "blue";
                    }

                    Session["IsCreateSpouse"] = false;
                    Session["IsUpdateSpouse"] = true;



                }


            }
        }


        protected void btnSpouseEdit_Click(object sender, EventArgs e)
        {
            strQueryForSpouse = "select Id, FirstName, LastName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c from Contact where cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";

            Button btnEditSpouse = (Button)sender;

            //String strSpouseId = btnEditSpouse.CommandArgument.ToString();

            Session["SpouseId"] = btnEditSpouse.CommandArgument.ToString();

            //LoadSpouseInfo();

            LoadEditSpouseInfo();

            mpeEditSpouse.Show();

            // call the function that sets every hdn field

            //mpeAddSpouse.Show();


            //InitializedSfdcbinding();

            //SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);

            //if (qrSpouse.size > 0)
            //{
            //    SForce.Contact ctSpouse = qrSpouse.records[0] as SForce.Contact;

            //    Session["SpouseId"] = ctSpouse.Id;
            //    txtSpouseLastName.Text = ctSpouse.LastName;
            //    txtSpouseFirstName.Text = ctSpouse.FirstName;
            //    if (ctSpouse.MiddleName != String.Empty) txtSpouseMiddleName.Text = ctSpouse.MiddleName;
            //    DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;
            //    txtSpouseDateOfBirth.Text = dtSpouseBirthdate.ToString("MM/dd/yyyy");

            //    if (ctSpouse.cmm_Gender__c == "Female") rbListSpouseGender.SelectedIndex = 1;
            //    if (ctSpouse.cmm_Gender__c == "Male") rbListSpouseGender.SelectedIndex = 0;

            //    txtSpouseSSN.Text = ctSpouse.Social_Security_Number__c;
            //    txtSpouseSSN.ReadOnly = true;
            //    revSpouseSSN.Enabled = false;

            //    lblAddSpouseTitle.Text = "Edit Spouse";
            //    //Set_Spouse_smoking_drug_alcohol_buttons();

            //    String strQuerySpouseForSDA = "select cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c from tmp_SmokingDrugAlcohol__c " +
            //                                  "where cmm_Account__c = '" + strAccountId + "' and cmm_Contact__c = '" + ctSpouse.Id + "'";

            //    SForce.QueryResult qrSpouseSDA = Sfdcbinding.query(strQuerySpouseForSDA);

            //    if (qrSpouseSDA.size > 0)
            //    {
            //        SForce.tmp_SmokingDrugAlcohol__c spouseSDA = qrSpouseSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

            //        if (spouseSDA.cmm_bCurrentSmoker__c == "Yes")
            //        {
            //            btnSpouseCurrentSmokerYes.BackColor = Color.Red;
            //            btnSpouseCurrentSmokerYes.ForeColor = Color.White;
            //            btnSpouseCurrentSmokerNo.BackColor = Color.LightGray;
            //            btnSpouseCurrentSmokerNo.ForeColor = Color.Black;
            //        }
            //        if (spouseSDA.cmm_bCurrentSmoker__c == "No")
            //        {
            //            btnSpouseCurrentSmokerYes.BackColor = Color.LightGray;
            //            btnSpouseCurrentSmokerYes.ForeColor = Color.Black;
            //            btnSpouseCurrentSmokerNo.BackColor = Color.Blue;
            //            btnSpouseCurrentSmokerNo.ForeColor = Color.White;
            //        }
            //        if (spouseSDA.cmm_bFormerSmoker__c == "Yes")
            //        {
            //            btnSpouseFormerSmokerYes.BackColor = Color.Green;
            //            btnSpouseFormerSmokerYes.ForeColor = Color.White;
            //            btnSpouseFormerSmokerNo.BackColor = Color.LightGray;
            //            btnSpouseFormerSmokerNo.ForeColor = Color.Black;
            //        }
            //        if (spouseSDA.cmm_bFormerSmoker__c == "No")
            //        {
            //            btnSpouseFormerSmokerYes.BackColor = Color.LightGray;
            //            btnSpouseFormerSmokerYes.ForeColor = Color.Black;
            //            btnSpouseFormerSmokerNo.BackColor = Color.Blue;
            //            btnSpouseFormerSmokerNo.ForeColor = Color.White;
            //        }
            //        if (spouseSDA.cmm_bCurrentDrug__c == "Yes")
            //        {
            //            btnSpouseNarcoticYes.BackColor = Color.Red;
            //            btnSpouseNarcoticYes.ForeColor = Color.White;
            //            btnSpouseNarcoticNo.BackColor = Color.LightGray;
            //            btnSpouseNarcoticNo.ForeColor = Color.Black;
            //        }
            //        if (spouseSDA.cmm_bCurrentDrug__c == "No")
            //        {
            //            btnSpouseNarcoticYes.BackColor = Color.LightGray;
            //            btnSpouseNarcoticYes.ForeColor = Color.Black;
            //            btnSpouseNarcoticNo.BackColor = Color.Blue;
            //            btnSpouseNarcoticNo.ForeColor = Color.White;
            //        }
            //        if (spouseSDA.cmm_bFormerDrug__c == "Yes")
            //        {
            //            btnSpouseFormerNarcoticYes.BackColor = Color.Green;
            //            btnSpouseFormerNarcoticYes.ForeColor = Color.White;
            //            btnSpouseFormerNarcoticNo.BackColor = Color.LightGray;
            //            btnSpouseFormerNarcoticNo.ForeColor = Color.Black;
            //        }
            //        if (spouseSDA.cmm_bFormerDrug__c == "No")
            //        {
            //            btnSpouseFormerNarcoticYes.BackColor = Color.LightGray;
            //            btnSpouseFormerNarcoticYes.ForeColor = Color.Black;
            //            btnSpouseFormerNarcoticNo.BackColor = Color.Blue;
            //            btnSpouseFormerNarcoticNo.ForeColor = Color.White;
            //        }
            //        if (spouseSDA.cmm_bAlcohol__c == "Yes")
            //        {
            //            btnSpouseAlcoholYes.BackColor = Color.Green;
            //            btnSpouseAlcoholYes.ForeColor = Color.White;
            //            btnSpouseAlcoholNo.BackColor = Color.LightGray;
            //            btnSpouseAlcoholNo.ForeColor = Color.Black;
            //        }
            //        if (spouseSDA.cmm_bAlcohol__c == "No")
            //        {
            //            btnSpouseAlcoholYes.BackColor = Color.LightGray;
            //            btnSpouseAlcoholYes.ForeColor = Color.Black;
            //            btnSpouseAlcoholNo.BackColor = Color.Blue;
            //            btnSpouseAlcoholNo.ForeColor = Color.White;
            //        }

            //        Session["IsCreateSpouse"] = false;
            //        Session["IsUpdateSpouse"] = true;
            //        mpeAddSpouse.Show();
            //    }



            //if (qrSpouse.size > 0)
            //{
            //    SForce.Contact ctSpouse = (SForce.Contact)qrSpouse.records[0];

            //    Session["SpouseId"] = strSpouseId;
            //    txtEditSpouseLastName.Text = ctSpouse.LastName;
            //    txtEditSpouseFirstName.Text = ctSpouse.FirstName;
            //    if (ctSpouse.MiddleName != string.Empty) txtEditSpouseMiddleName.Text = ctSpouse.MiddleName;

            //    DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;
            //    txtEditSpouseDateOfBirth.Text = dtSpouseBirthdate.ToString("MM/dd/yyyy");

            //    if (ctSpouse.cmm_Gender__c == "Female") rbListEditSpouseGender.SelectedIndex = 1;
            //    if (ctSpouse.cmm_Gender__c == "Male") rbListEditSpouseGender.SelectedIndex = 0;

            //    txtEditSpouseSSN.Text = ctSpouse.Social_Security_Number__c;
            //    txtEditSpouseSSN.Enabled = false;

            //    //pnlEditSpouse.CssClass = "pnlBackGround";
            //    pnlEditSpouse.Visible = true;
            //    mpeEditSpouse.Show();
            //}

            //}
        }

        protected void LoadSpouseInfo()
        {
            //strQueryForSpouse = "select Id, FirstName, LastName, MiddleName, Birthdate, cmm_Gender__c, Social_Security_Number__c from Contact where cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse'";

            //Button btnEditSpouse = (Button)sender;

            //String strSpouseId = btnEditSpouse.CommandArgument.ToString();
            String strSpouseId = String.Empty;

            if ((String)Session["SpouseId"] != null) strSpouseId = (String)Session["SpouseId"];

            InitializedSfdcbinding();

            SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);

            if (qrSpouse.size > 0)
            {
                SForce.Contact ctSpouse = qrSpouse.records[0] as SForce.Contact;

                Session["SpouseId"] = ctSpouse.Id;
                txtSpouseLastName.Text = ctSpouse.LastName;
                txtSpouseFirstName.Text = ctSpouse.FirstName;
                if (ctSpouse.MiddleName != String.Empty) txtSpouseMiddleName.Text = ctSpouse.MiddleName;
                DateTime dtSpouseBirthdate = (DateTime)ctSpouse.Birthdate;
                txtSpouseDateOfBirth.Text = dtSpouseBirthdate.ToString("MM/dd/yyyy");

                if (ctSpouse.cmm_Gender__c == "Female") rbListSpouseGender.SelectedIndex = 1;
                if (ctSpouse.cmm_Gender__c == "Male") rbListSpouseGender.SelectedIndex = 0;

                txtSpouseSSN.Text = ctSpouse.Social_Security_Number__c;
                txtSpouseSSN.ReadOnly = true;
                revSpouseSSN.Enabled = false;

                lblAddSpouseTitle.Text = "Edit Spouse";
                //Set_Spouse_smoking_drug_alcohol_buttons();

                String strQuerySpouseForSDA = "select cmm_bCurrentSmoker__c, cmm_bFormerSmoker__c, cmm_bCurrentDrug__c, cmm_bFormerDrug__c, cmm_bAlcohol__c from tmp_SmokingDrugAlcohol__c " +
                                              "where cmm_Account__c = '" + strAccountId + "' and cmm_Contact__c = '" + ctSpouse.Id + "'";

                SForce.QueryResult qrSpouseSDA = Sfdcbinding.query(strQuerySpouseForSDA);

                if (qrSpouseSDA.size > 0)
                {
                    SForce.tmp_SmokingDrugAlcohol__c spouseSDA = qrSpouseSDA.records[0] as SForce.tmp_SmokingDrugAlcohol__c;

                    if (spouseSDA.cmm_bCurrentSmoker__c == "Yes")
                    {
                        btnSpouseCurrentSmokerYes.BackColor = Color.Red;
                        btnSpouseCurrentSmokerYes.ForeColor = Color.White;
                        hdnSpouseCurrentSmokerYes.Value = "red";
                        btnSpouseCurrentSmokerNo.BackColor = Color.LightGray;
                        btnSpouseCurrentSmokerNo.ForeColor = Color.Black;
                        hdnSpouseCurrentSmokerNo.Value = "lightgrey";
                    }
                    if (spouseSDA.cmm_bCurrentSmoker__c == "No")
                    {
                        btnSpouseCurrentSmokerYes.BackColor = Color.LightGray;
                        btnSpouseCurrentSmokerYes.ForeColor = Color.Black;
                        hdnSpouseCurrentSmokerYes.Value = "lightgrey";
                        btnSpouseCurrentSmokerNo.BackColor = Color.Blue;
                        btnSpouseCurrentSmokerNo.ForeColor = Color.White;
                        hdnSpouseCurrentSmokerNo.Value = "blue";
                    }
                    if (spouseSDA.cmm_bFormerSmoker__c == "Yes")
                    {
                        btnSpouseFormerSmokerYes.BackColor = Color.Green;
                        btnSpouseFormerSmokerYes.ForeColor = Color.White;
                        hdnSpouseFormerSmokerYes.Value = "green";
                        btnSpouseFormerSmokerNo.BackColor = Color.LightGray;
                        btnSpouseFormerSmokerNo.ForeColor = Color.Black;
                        hdnSpouseFormerSmokerNo.Value = "lightgrey";
                    }
                    if (spouseSDA.cmm_bFormerSmoker__c == "No")
                    {
                        btnSpouseFormerSmokerYes.BackColor = Color.LightGray;
                        btnSpouseFormerSmokerYes.ForeColor = Color.Black;
                        hdnSpouseFormerSmokerYes.Value = "lightgrey";
                        btnSpouseFormerSmokerNo.BackColor = Color.Blue;
                        btnSpouseFormerSmokerNo.ForeColor = Color.White;
                        hdnSpouseFormerSmokerNo.Value = "blue";
                    }
                    if (spouseSDA.cmm_bCurrentDrug__c == "Yes")
                    {
                        btnSpouseNarcoticYes.BackColor = Color.Red;
                        btnSpouseNarcoticYes.ForeColor = Color.White;
                        hdnSpouseNarcoticYes.Value = "red";
                        btnSpouseNarcoticNo.BackColor = Color.LightGray;
                        btnSpouseNarcoticNo.ForeColor = Color.Black;
                        hdnSpouseNarcoticNo.Value = "lightgrey";
                    }
                    if (spouseSDA.cmm_bCurrentDrug__c == "No")
                    {
                        btnSpouseNarcoticYes.BackColor = Color.LightGray;
                        btnSpouseNarcoticYes.ForeColor = Color.Black;
                        hdnSpouseNarcoticYes.Value = "lightgrey";
                        btnSpouseNarcoticNo.BackColor = Color.Blue;
                        btnSpouseNarcoticNo.ForeColor = Color.White;
                        hdnSpouseNarcoticNo.Value = "blue";
                    }
                    if (spouseSDA.cmm_bFormerDrug__c == "Yes")
                    {
                        btnSpouseFormerNarcoticYes.BackColor = Color.Green;
                        btnSpouseFormerNarcoticYes.ForeColor = Color.White;
                        hdnSpouseFormerNarcoticYes.Value = "green";
                        btnSpouseFormerNarcoticNo.BackColor = Color.LightGray;
                        btnSpouseFormerNarcoticNo.ForeColor = Color.Black;
                        hdnSpouseFormerNarcoticNo.Value = "lightgrey";
                    }
                    if (spouseSDA.cmm_bFormerDrug__c == "No")
                    {
                        btnSpouseFormerNarcoticYes.BackColor = Color.LightGray;
                        btnSpouseFormerNarcoticYes.ForeColor = Color.Black;
                        hdnSpouseFormerNarcoticYes.Value = "lightgrey";
                        btnSpouseFormerNarcoticNo.BackColor = Color.Blue;
                        btnSpouseFormerNarcoticNo.ForeColor = Color.White;
                        hdnSpouseFormerNarcoticNo.Value = "blue";
                    }
                    if (spouseSDA.cmm_bAlcohol__c == "Yes")
                    {
                        btnSpouseAlcoholYes.BackColor = Color.Red;
                        btnSpouseAlcoholYes.ForeColor = Color.White;
                        hdnSpouseAlcoholYes.Value = "red";
                        btnSpouseAlcoholNo.BackColor = Color.LightGray;
                        btnSpouseAlcoholNo.ForeColor = Color.Black;
                        hdnSpouseAlcoholNo.Value = "lightgrey";
                    }
                    if (spouseSDA.cmm_bAlcohol__c == "No")
                    {
                        btnSpouseAlcoholYes.BackColor = Color.LightGray;
                        btnSpouseAlcoholYes.ForeColor = Color.Black;
                        hdnSpouseAlcoholYes.Value = "lightgrey";
                        btnSpouseAlcoholNo.BackColor = Color.Blue;
                        btnSpouseAlcoholNo.ForeColor = Color.White;
                        hdnSpouseAlcoholNo.Value = "blue";
                    }

                    Session["IsCreateSpouse"] = false;
                    Session["IsUpdateSpouse"] = true;
                    //mpeAddSpouse.Show();
                }

            }
        }



        protected void btnSpouseDelete_Click(object sender, EventArgs e)
        {

            Button btnDelete = (Button)sender;

            String strSpouseContactId = btnDelete.CommandArgument.ToString();

            String[] SpouseContactId = new String[] { strSpouseContactId };

            InitializedSfdcbinding();

            SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(SpouseContactId);
            SForce.DeleteResult deleteResult = deleteResults[0];

            if (deleteResult.success)
            {
                DisplaySpouse();
                btnAddSpouse.Enabled = true;
                btnRemoveSpouse.Enabled = false;
                upnlSpouseAddRemove.Update();
            }
        }

        protected void btnRemoveSpouse_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in gvSpouse.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkSelected = (CheckBox)row.Cells[0].FindControl("chkSelectSpouse");

                    if (chkSelected.Checked)
                    {
                        mpeRemoveSpouse.Show();

                        //String strName = ((Label)row.Cells[1].FindControl("lblName")).Text.Trim();

                        //String strSpouseLastName = strName.Substring(0, strName.IndexOf(',')).Trim();
                        //String[] strSpouseNames = strName.Split(' ');
                        //String strSpouseFirstName = strSpouseNames[1].Trim();

                        //String strDateOfBirth = ((Label)row.Cells[2].FindControl("lblDateOfBirth")).Text.Trim();
                        //String[] strSpouseDateOfBirths = strDateOfBirth.Split('/');

                        //int nSpouseBirthYear = Int32.Parse(strSpouseDateOfBirths[2].Trim());
                        //int nSpouseBirthMonth = Int32.Parse(strSpouseDateOfBirths[0].Trim());
                        //int nSpouseBirthDay = Int32.Parse(strSpouseDateOfBirths[1].Trim());

                        //DateTime dtSpouseDateOfBirth = new DateTime(nSpouseBirthYear, nSpouseBirthMonth, nSpouseBirthDay);
                        //String strSpouseBirthdate = dtSpouseDateOfBirth.ToString("yyyy-MM-dd");

                        //String strQueryForSpouse = "select Id from Contact where CMM_Household__r.Id = '" + strAccountId + "' and Household_Role__c = 'Spouse' " +
                        //                           "and LastName = '" + strSpouseLastName + "' " +
                        //                           "and FirstName = '" + strSpouseFirstName + "' " +
                        //                           "and Birthdate = " + strSpouseBirthdate;

                        //InitializedSfdcbinding();

                        //SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);

                        //if (qrSpouse.size > 0)
                        //{
                        //    SForce.Contact ctSpouse = (SForce.Contact)qrSpouse.records[0];

                        //    String[] strSpouseIds = new String[] { ctSpouse.Id };

                        //    SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(strSpouseIds);
                        //    SForce.DeleteResult deleteResult = deleteResults[0];

                        //    if (deleteResult.success)
                        //    {
                        //        DisplaySpouse();
                        //    }
                        //}
                    }
                    else
                    {
                        // Display error popup for not checking the check box for spouse
                        mpeSpouseUnchecked.Show();
                    }
                }
            }
        }

        protected void btnRemoveSpouseConfirm_Click(object sender, EventArgs e)
        {

            GridViewRow rowSpouseSelected = gvSpouse.Rows[0];

            String strName = ((Label)rowSpouseSelected.Cells[1].FindControl("lblName")).Text.Trim();

            String strSpouseLastName = strName.Substring(0, strName.IndexOf(',')).Trim();
            String[] strSpouseNames = strName.Split(' ');
            String strSpouseFirstName = strSpouseNames[1].Trim();

            String strDateOfBirth = ((Label)rowSpouseSelected.Cells[2].FindControl("lblDateOfBirth")).Text.Trim();
            String[] strSpouseDateOfBirths = strDateOfBirth.Split('/');

            int nSpouseBirthYear = Int32.Parse(strSpouseDateOfBirths[2].Trim());
            int nSpouseBirthMonth = Int32.Parse(strSpouseDateOfBirths[0].Trim());
            int nSpouseBirthDay = Int32.Parse(strSpouseDateOfBirths[1].Trim());

            DateTime dtSpouseDateOfBirth = new DateTime(nSpouseBirthYear, nSpouseBirthMonth, nSpouseBirthDay);
            String strSpouseBirthdate = dtSpouseDateOfBirth.ToString("yyyy-MM-dd");

            String strQueryForSpouse = "select Id from Contact where cmm_Household__r.Id = '" + strAccountId + "' and cmm_Household_Role__c = 'Spouse' " +
                                       "and LastName = '" + strSpouseLastName + "' " +
                                       "and FirstName = '" + strSpouseFirstName + "' " +
                                       "and Birthdate = " + strSpouseBirthdate;

            InitializedSfdcbinding();

            SForce.QueryResult qrSpouse = Sfdcbinding.query(strQueryForSpouse);

            if (qrSpouse.size > 0)
            {
                SForce.Contact ctSpouse = (SForce.Contact)qrSpouse.records[0];

                String[] strSpouseIds = new String[] { ctSpouse.Id };

                SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(strSpouseIds);
                SForce.DeleteResult deleteResult = deleteResults[0];

                if (deleteResult.success)
                {
                    DisplaySpouse();
                    btnAddSpouse.Enabled = true;
                    btnRemoveSpouse.Enabled = false;
                    upnlSpouseAddRemove.Update();
                }
            }
        }

        protected void btnRemoveChildren_Click(object sender, EventArgs e)
        {
            Boolean bAnyChildCheced = false;

            foreach (GridViewRow row in gvChildren.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelectedChild = (CheckBox)row.Cells[0].FindControl("chkSelectedChild");
                    if (chkSelectedChild.Checked) bAnyChildCheced = true;
                }
            }

            if (bAnyChildCheced) mpeRemoveChildren.Show();
            else
            {
                mpeChildrenUnselected.Show();
                //btnRemoveChildren.Enabled = true;
                //upnlAddRemoveChildren.Update();
            }
        }

        protected void btnRemoveChildrenConfirm_Click(object sender, EventArgs e)
        {
            List<String> lstChildIds = new List<string>();

            foreach (GridViewRow row in gvChildren.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelectedChild = (CheckBox)row.Cells[0].FindControl("chkSelectedChild");

                    if (chkSelectedChild.Checked)
                    {
                        Button btnChildToDelete = (Button)row.Cells[6].FindControl("btnChildDelete");

                        lstChildIds.Add(btnChildToDelete.CommandArgument.ToString());
                    }
                }
            }
            if (lstChildIds.Count > 0)
            {
                String[] ChildIds = new String[lstChildIds.Count];

                for (int i = 0; i < lstChildIds.Count; i++)
                {
                    ChildIds[i] = lstChildIds[i];
                }

                InitializedSfdcbinding();

                SForce.DeleteResult[] deleteResults = Sfdcbinding.delete(ChildIds);
                SForce.DeleteResult deleteResult = deleteResults[0];

                if (deleteResult.success)
                {
                    DisplayChildren();
                    if (gvChildren.Rows.Count == 0) btnRemoveChildren.Enabled = false;
                }

            }
        }

        protected void btnChildrenUncheckedOk_Click(object sender, EventArgs e)
        {
            btnRemoveChildren.Enabled = true;
            upnlAddRemoveChildren.Update();
        }

        protected void btnSpouseUncheckedOk_Click(object sender, EventArgs e)
        {
            btnRemoveSpouse.Enabled = true;
            upnlSpouseAddRemove.Update();
        }

        protected void btnSpouseOk_Click(object sender, EventArgs e)
        {
            mpeSpouseSmokingDrug.Hide();
            Set_Spouse_smoking_drug_alcohol_buttons();
            mpeAddSpouse.Show();
        }

        protected void btnChildOk_Click(object sender, EventArgs e)
        {
            mpeChildSmokingDrug.Hide();
            //Set_Edit_Child_smoking_drug_alcohol_buttons();
            Set_Child_smoking_drug_alcohol_buttons();
            mpeAddChild.Show();
            //LoadChildInfoNoSDA();
            
        }

        protected void btnUpdateSpouseOk_Click(object sender, EventArgs e)
        {
            mpeUpdateSpouseSmokingDrugDrinking.Hide();
            //String strTmpSpouseId = (String)Session["SpouseId"];
            //String strTmpSpouseLastName = txtEditSpouseLastName.Text;
            //String strTmpSpouseFirstName = txtEditSpouseFirstName.Text;
            //String strTmpSpouseMiddleName = txtEditSpouseMiddleName.Text;
            //String strTmpSpouseBirthdate = txtEditSpouseDateOfBirth.Text;
            //String strTmpSpouseGender = rbListEditSpouseGender.SelectedItem.Text;
            Set_Update_Spouse_smoking_drug_alcohol_buttons();
            mpeEditSpouse.Show();
        }

        protected void btnEditChildOk_Click(object sender, EventArgs e)
        {
            mpeEditChildSmokingDrugDrink.Hide();
            Set_Edit_Child_smoking_drug_alcohol_buttons();
            mpeEditChild.Show();
        }
    }
}