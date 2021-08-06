using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace tt
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Connection string to connect Oracle Database
        /*string oradb = "Data Source = (DESCRIPTION = "+
            "(ADDRESS = (PROTOCOL = TCP)(HOST = desktop-i31h2ff)(PORT = 1521))"+
            "(CONNECT_DATA ="+
            "(SERVER = DEDICATED)"+
            "(SERVICE_NAME = XE)));" +
            "User Id = tt; password=tt;";*/

        string oradb = ConfigurationManager.ConnectionStrings["myConnection"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Call method on page load
            OracleConnection con = new OracleConnection(oradb);
            if (!IsPostBack)
            {
                fillData();
                
               
            }
        }

        private void fillData()
        {
            
            string qry = "SELECT * FROM THONGKE";
            OracleConnection con = new OracleConnection(oradb);
            con.Open();
            OracleDataAdapter da = new OracleDataAdapter(qry, con);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            gvttvt.DataSource = ds;
            gvttvt.DataBind();
            con.Close();
            con.Dispose();
        }
    }
}