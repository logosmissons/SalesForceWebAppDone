using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;


namespace SalesForceWebApp
{
    public partial class CrmRegister : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {

                MySqlConnection conn = new MySqlConnection();

                string strConn = "Data Source=localhost; port=3306; Database=cmmcrmuserprofile; Uid=harris; Pwd=speed5of2light5";
                conn.ConnectionString = strConn;

                MySqlCommand cmdGetLastId = new MySqlCommand();

                cmdGetLastId.CommandType = System.Data.CommandType.Text;
                cmdGetLastId.CommandText = "select max(id) from user_profile";
                cmdGetLastId.Connection = conn;

                //MySqlCommand cmdGetRecordCount = new MySqlCommand();
                //cmdGetRecordCount.CommandType = System.Data.CommandType.Text;
                //cmdGetRecordCount.CommandText = "select count(id) from user_profile";
                //cmdGetRecordCount.Connection = conn;

                MySqlCommand cmdInsertNewMember = new MySqlCommand();
                cmdInsertNewMember.Connection = conn;
                cmdInsertNewMember.CommandText = "insert into user_profile (id, email, user_first_name, user_last_name, password, IsActive) " +
                                                 "values (@Id, @Email, @First_Name, @Last_Name, @Password, @IsActive)";
                int nNewId = 1;
                conn.Open();

                var result = cmdGetLastId.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    nNewId = Convert.ToInt32(result);
                    nNewId++;
                }
                //MySqlDataReader rdr = cmdGetLastId.ExecuteReader();
                //MySqlDataReader rdrRecordCount = cmdGetRecordCount.ExecuteReader();
                //rdr.Read();

                //if (rdrRecordCount.HasRows)
                //{
                //    MySqlDataReader rdrLastId = cmdGetLastId.ExecuteReader();

                //    nNewId = rdrLastId.GetInt32(0);
                //    nNewId++;
                //}

                //if (rdr.Read())
                //{
                //    if (rdr.Read())
                //    {
                //        nNewId = Int32.Parse(rdr.GetValue(0).ToString());
                //        nNewId++;

                //    }
                //}

                //if (nNewId > 0) nNewId++;
                //if (rdr.Read() == true)
                //{
                //    nNewId = Int32.Parse(rdr.GetValue(0).ToString());
                //    nNewId++;
                //}
                conn.Close();

                MySqlParameter pmUserId = new MySqlParameter();
                pmUserId.DbType = System.Data.DbType.Int32;
                pmUserId.ParameterName = "@Id";
                pmUserId.Value = nNewId;
                cmdInsertNewMember.Parameters.Add(pmUserId);

                MySqlParameter pmEmail = new MySqlParameter();
                pmEmail.DbType = System.Data.DbType.String;
                pmEmail.ParameterName = "@Email";
                pmEmail.Value = txtUserEmail.Text;
                cmdInsertNewMember.Parameters.Add(pmEmail);

                MySqlParameter pmFirstName = new MySqlParameter();
                pmFirstName.DbType = System.Data.DbType.String;
                pmFirstName.ParameterName = "@First_Name";
                pmFirstName.Value = txtUserFirstName.Text;
                cmdInsertNewMember.Parameters.Add(pmFirstName);

                MySqlParameter pmLastName = new MySqlParameter();
                pmLastName.DbType = System.Data.DbType.String;
                pmLastName.ParameterName = "@Last_Name";
                pmLastName.Value = txtUserLastName.Text;
                cmdInsertNewMember.Parameters.Add(pmLastName);

                MySqlParameter pmPassword = new MySqlParameter();
                pmPassword.DbType = System.Data.DbType.String;
                pmPassword.ParameterName = "@Password";
                pmPassword.Value = CmmCrmPasswordHashing(txtUserPassword.Text);
                cmdInsertNewMember.Parameters.Add(pmPassword);
                

                MySqlParameter pmIsActive = new MySqlParameter();
                pmIsActive.DbType = System.Data.DbType.Boolean;
                pmIsActive.ParameterName = "@IsActive";
                pmIsActive.Value = true;
                cmdInsertNewMember.Parameters.Add(pmIsActive);

                conn.Open();
                cmdInsertNewMember.ExecuteNonQuery();
                conn.Close();

            }
      
        }

        protected void cvPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int nPasswordLength = source.ToString().Length;

            if (nPasswordLength >= 6) args.IsValid = true;
            else args.IsValid = false;
        }

        private string CmmCrmPasswordHashing (string strInput)
        {
            byte[] hashedPassword;

            using (SHA512 sha512Hash = new SHA512Managed())
            {

                //hashedPassword = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(strInput));

                hashedPassword = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(strInput));
            }

            //return hashedPassword.ToString();

            StringBuilder password = new StringBuilder();

            for (int i = 0; i < hashedPassword.Length; i++)
            {


                //String.Concat(hashedPassword.ElementAt(i));
                password.Append(hashedPassword.ElementAt(i));
            }

            return password.ToString();

        }

        //private byte[] GetByteArray (string input)
        //{
        //    byte[] output = new byte[input.Length];

        //    for (int i = 0; i < input.Length; i++)
        //    {
        //        output[i] = (byte) input.ElementAt(i);
        //    }

        //    return output;
        //}
    }
}