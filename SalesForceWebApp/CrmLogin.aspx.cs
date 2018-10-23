using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Security.Cryptography;

namespace SalesForceWebApp
{
    public partial class CrmLogin : System.Web.UI.Page
    {
        protected string strUserId;
        protected string strPasswd;
        protected string strConnectionString;

        protected MySqlConnection conn = null;
        protected MySqlDataAdapter da = null;
        protected MySqlCommand comm = null; 


        protected DataSet ds = null;
        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayData();
        }

        protected void DisplayData()
        {
            strUserId = "Hj_P007";
            strPasswd = "speed009";

            strConnectionString = @"Data Source=10.1.10.7; Port=3306; Database=cmmworld_admin; User ID=Hj_P007; Password='speed009'";

            conn = new MySqlConnection(strConnectionString);

            conn.Open();

            da = new MySqlDataAdapter("select username, email, password from user limit 30", conn);

            //da = new MySqlDataAdapter("select id, bank_owner from member where id >= 10000 order by id asc limit 30", conn);

            ds = new DataSet();

            da.Fill(ds);

            gvLogin.DataSource = ds;
            gvLogin.DataBind();

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userEmail = txtEmail.Text;

            MySqlConnection conn = new MySqlConnection();

            string strConn = "Data Source=localhost; port=3306; Database=cmmcrmuserprofile; Uid=harris; Pwd=speed5of2light5";
            conn.ConnectionString = strConn;

            MySqlCommand cmdPasswordForUser = new MySqlCommand();

            cmdPasswordForUser.CommandType = CommandType.Text;
            cmdPasswordForUser.CommandText = "select password from user_profile where email = '" + userEmail + "'";
            cmdPasswordForUser.Connection = conn;

            conn.Open();

            String pwFromDb = (String) cmdPasswordForUser.ExecuteScalar();

            conn.Close();

            String pwFromUser = PasswordHashing(txtPassword.Text);

            if (String.Equals(pwFromDb, pwFromUser))
            {
                Response.Redirect("CrmRegister.aspx");
            }
            else
            {
                lblLoginError.Text = "Error: your password is incorrect!";
            }
        }

        private string PasswordHashing(string strInput)
        {
            byte[] hashedPassword;

            using (SHA512 sha512Hash = new SHA512Managed())
            {
                hashedPassword = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(strInput));
            }

            StringBuilder password = new StringBuilder();

            for (int i = 0; i < hashedPassword.Length; i++)
            {
                password.Append(hashedPassword.ElementAt(i));
            }

            return password.ToString();

        }
    }
}