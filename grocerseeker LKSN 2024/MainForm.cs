using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocerseeker_LKSN_2024
{
    public partial class MainForm : Form
    {
        public string Role {  get; set; }
        public string Names { get; set; }
        public string phones { get; set; }
        //private string phones = null;

        //public hp(string angka)
        //{
        //    phones = angka;
        //}
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

         // MessageBox.Show(phones);

         label2.Text = "Login As " + Role;
         //  MessageBox.Show("Login As " + Role);


            lblname.Text = "Welcome ( " + Names + " )";
            //SqlConnection conn = Connection.GetConn();
            //conn.Open();

            //if(Role == "Customer")
            //{

            //    SqlCommand cmdd = new SqlCommand("select cust_name from users where");


            //}else if(Role == "Vendor")
            //{

            //}


            //SqlCommand cmd = new SqlCommand("select ");
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            ProfileForm frm = new ProfileForm();
            frm.phones = phones;
            frm.Show();

            //=--------------------

           this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {             
            ProductForm frms = new ProductForm();
            frms.phones = phones;
            //MessageBox.Show("Phones value: " + phones);
            if (Role == "Customer")
            {
                ProductForm frm = new ProductForm();
                frm.phones = phones;
                frm.Show();
                frm.panelCustomer.Visible = true;
                frm.dgvCustomer.Visible = true;
                frm.panel1.Visible = false;
               
            }
            else if (Role == "Vendor") {

                ProductForm frm = new ProductForm();
                frm.phones = phones;
                frm.Show();
                frm.panelvendor.Visible = true;
                frm.panel1.Visible = true;
                frm.dgvVendor.Visible = true;
               

            }

          
          //  this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Transaction transaction = new Transaction();
           // MessageBox.Show(Role);
            transaction.Role = Role;    
            transaction.Phone = phones;
            transaction.Show();
        }
    }
}
