using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grocerseeker_LKSN_2024
{
    internal class Connection
    {
        public static SqlConnection GetConn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=PULUNG\\SQLEXPRESS;Initial Catalog=grocerseekesr33;Integrated Security=True;Encrypt=False";


            return conn;
        }

        public static SqlConnection conn = new SqlConnection("Data Source=PULUNG\\SQLEXPRESS;Initial Catalog=grocerseekesr33;Integrated Security=True;Encrypt=False");
        public static SqlCommand cmd = new SqlCommand("", conn);


        public static void query(String query)
        {
            conn.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static Object select(String query)
        {
            conn.Open();
            DataTable dt = new DataTable();
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            return dt;
        }


    }
}
 