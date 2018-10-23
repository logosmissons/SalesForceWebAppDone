using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;

namespace SalesForceWebApp
{
    public partial class CrmShowUserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = "Data Source=localhost; port=3306; Database=cmmcrmuserprofile; Uid=harris; Pwd=speed5of2light5";

            MySqlDataAdapter da = new MySqlDataAdapter("select id, email, user_first_name, user_last_name, password, IsActive from user_profile", conn);

            DataSet ds = new DataSet();

            da.Fill(ds);

            gvUserProfile.DataSource = ds;

            gvUserProfile.DataBind();

        }
    }
}