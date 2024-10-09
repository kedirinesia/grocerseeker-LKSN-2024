using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocerseeker_LKSN_2024
{
    public partial class Transaction : Form
    {

        public string Role { get; set; }
        public string Phone { get; set; }

        string id;

        public Transaction()
        {
            InitializeComponent();
        }
        void dgvCustHistory()
        {
            DataTable dt = (DataTable)Connection.select("SELECT products.product_name, \r\n       users.vendor_name, \r\n       transactions.quantity, \r\n       products.price_per_unit, \r\n       transactions.total_price, \r\n       transactions.delivery_cost, \r\n       transactions.status\r\nFROM products\r\nINNER JOIN categories ON products.category_id = categories.id\r\nINNER JOIN transactions ON products.id = transactions.product_id\r\nINNER JOIN users ON products.vendor_id = users.id \r\n\r\nWHERE transactions.status IN ('Abort', 'Success') and transactions.customer_id = '"+id+"'");

         
                dgvHistory.DataSource = dt;
             
                
            
        }


        void dgvCustPending()
        {
            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                SqlCommand cmdPending = new SqlCommand("SELECT products.product_name, users.vendor_name, transactions.quantity, products.price_per_unit, transactions.total_price, transactions.delivery_cost, transactions.status FROM products INNER JOIN categories ON products.category_id = categories.id INNER JOIN transactions ON products.id = transactions.product_id INNER JOIN users ON products.vendor_id = users.id WHERE transactions.status = 'Pending' and transactions.customer_id = '" + id+"'   " , conn);

                SqlDataAdapter daPending = new SqlDataAdapter(cmdPending);
                DataTable dtPending = new DataTable();
                daPending.Fill(dtPending);
                dgvPending.DataSource = dtPending;
            }

        }

        private void Transaction_Load(object sender, EventArgs e)
        {




            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("select id from users where phone_number = '" + Phone + "'", conn);
                SqlDataReader rd = cmdd.ExecuteReader();
                while (rd.Read())
                {
                    id = rd["id"].ToString();
                   // MessageBox.Show(id);
                }
            }

            //using (SqlConnection conn = Connection.GetConn())
            //{
            //    conn.Open();
            //    SqlCommand cmdPending = new SqlCommand("select * from transactions", conn);

            //    SqlDataAdapter daPending = new SqlDataAdapter(cmdPending);
            //    DataTable dtPending = new DataTable();
            //    daPending.Fill(dtPending);
            //    dgvPending.DataSource = dtPending;
                //MessageBox.Show(Role);
                if (Role == "Customer")
                {
                dgvCustHistory();
                dgvCustPending();
            }
                else if (Role == "Vendor")
                {

                }
                else
                {
                    MessageBox.Show("ERROR ROLE NOT FOUND");
                }
            }
        }
    }
//}
