using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grocerseeker_LKSN_2024
{
    public partial class ProfileForm : Form
    {
        
        public string phones {  get; set; }
        public string Names { get; set; }
        string Cust;
        string Vend;


        public ProfileForm()
        {
            InitializeComponent();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";


            string email = txtEmail.Text;




            if (!Regex.IsMatch(email, pattern))
            {

                lblcaution.Text = "Format email tidak valid!";
            }

            else
            {

                lblcaution.Text = "";
            }
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            string cust = string.Empty;
            string vendors = string.Empty;

            //Connection.select("select * from users where  '"+phones+"'");
            //MessageBox.Show(phones);


            SqlConnection conn = Connection.GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE phone_number = '"+phones+"'", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                txtPhone.Text = phones;
                txtEmail.Text = rd["email"].ToString();
                cust = rd["cust_active"].ToString();
                vendors = rd["vendor_active"].ToString();


            }
            
            if (vendors == "1" && cust == "1")
            {
                

                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
               checkBox1.Checked = true;
                checkBox2.Checked = true;

                if (checkBox1.Checked && checkBox2.Checked)
                {
                    if (rd.Read())
                    {
                        txtCustName.Text = rd["cust_name"].ToString();
                        txtCustAddress.Text = rd["cust_address"].ToString();
                        txtCustLatitude.Text = rd["cust_latitude"].ToString();
                        txtCustLogitude.Text = rd["cust_longitude"].ToString();

                        txtVendorName.Text = rd["vendor_name"].ToString();
                        txtVendorAddress.Text = rd["vendor_address"].ToString();
                        txtVendorLatitude.Text = rd["vendor_latitude"].ToString();
                        txtVendorLongitude.Text = rd["vendor_longitude"].ToString();
                    }
                }
            }
            else if (vendors == "1")
            {
                
                 checkBox2.Checked = true ;
                checkBox2.Enabled = true;

               
                if (checkBox2.Checked)
                {
                    if (rd.Read())
                    {
                        txtVendorName.Text = rd["vendor_name"].ToString();
                        txtVendorAddress.Text = rd["vendor_address"].ToString();
                        txtVendorLatitude.Text = rd["vendor_latitude"].ToString();
                        txtVendorLongitude.Text = rd["vendor_longitude"].ToString();
                    }
                }
            }
            else if(cust== "1")
            {
                checkBox1.Checked = true;
                checkBox1.Enabled = true;
              
                
                if (checkBox1.Checked)
                {
                    if (rd.Read())
                    {
                        txtCustName.Text = rd["cust_name"].ToString();
                        txtCustAddress.Text = rd["cust_address"].ToString();
                        txtCustLatitude.Text = rd["cust_latitude"].ToString();
                        txtCustLogitude.Text = rd["cust_longitude"].ToString();
                    }
                }
            }
            conn.Close();
            rd.Close();
          //  MessageBox.Show(vendors, cust);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtPhone.Enabled = true;
            txtEmail.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            button1.Enabled = false;
            
            groupBox1.Enabled = true;
            txtCustName.Enabled = true;
            txtCustAddress.Enabled = true;
            txtCustLatitude.Enabled = true;
            txtCustLogitude.Enabled = true;

             
                txtVendorAddress.Enabled = true;
                txtVendorLatitude.Enabled = true;
                txtVendorLongitude.Enabled = true;
                txtVendorName.Enabled = true;
            




            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
            groupBox2.Visible = true;

            }
            else
            {
                groupBox2.Visible =false;
            }

            
            string cust = string.Empty;
            string vendors = string.Empty;

            SqlConnection conn = Connection.GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE phone_number = '" + phones + "'", conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                txtPhone.Text = phones;
                txtEmail.Text = rd["email"].ToString();
                cust = rd["cust_active"].ToString();
                vendors = rd["vendor_active"].ToString();
                //-------------------
                txtVendorName.Text = rd["vendor_name"].ToString();
                txtVendorAddress.Text = rd["vendor_address"].ToString();
                txtVendorLatitude.Text = rd["vendor_latitude"].ToString();
                txtVendorLongitude.Text = rd["vendor_longitude"].ToString();


            }

            if (vendors == "1" && cust == "1")
            {

                if (rd.Read())
                {
                    txtCustName.Text = rd["cust_name"].ToString();
                    txtCustAddress.Text = rd["cust_address"].ToString(); 
                    txtCustLatitude.Text = rd["cust_latitude"].ToString();
                    txtCustLogitude.Text = rd["cust_longitude"].ToString();

                    txtVendorName.Text = rd["vendor_name"].ToString();
                    txtVendorAddress.Text = rd["vendor_address"].ToString();
                    txtVendorLatitude.Text = rd["vendor_latitude"].ToString();
                    txtVendorLongitude.Text = rd["vendor_longitude"].ToString();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
            groupBox1.Visible = true;
              

            }
            else
            {
                groupBox1.Visible = false;
            }
            string cust = string.Empty;
            string vendors = string.Empty;

            SqlConnection conn = Connection.GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE phone_number = '" + phones + "'", conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                txtPhone.Text = phones;
                txtEmail.Text = rd["email"].ToString();
                cust = rd["cust_active"].ToString();
                vendors = rd["vendor_active"].ToString();
                //-------------------
                    txtCustName.Text = rd["cust_name"].ToString();
                    txtCustAddress.Text = rd["cust_address"].ToString();
                    txtCustLatitude.Text = rd["cust_latitude"].ToString();
                    txtCustLogitude.Text = rd["cust_longitude"].ToString();
                

            }

            if (vendors == "1" && cust == "1")
            {

                if (rd.Read())
                {
                    txtCustName.Text = rd["cust_name"].ToString();
                    txtCustAddress.Text = rd["cust_address"].ToString();
                    txtCustLatitude.Text = rd["cust_latitude"].ToString();
                    txtCustLogitude.Text = rd["cust_longitude"].ToString();

                    txtVendorName.Text = rd["vendor_name"].ToString();
                    txtVendorAddress.Text = rd["vendor_address"].ToString();
                    txtVendorLatitude.Text = rd["vendor_latitude"].ToString();
                    txtVendorLongitude.Text = rd["vendor_longitude"].ToString();
                }
            }
            





        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cust = "0";
            Vend = "0";
            if (checkBox1.Checked && checkBox2.Checked)
            {
                Cust = "1";
                Vend = "1";
            }
            else if (checkBox1.Checked)
            {
                Cust = "1";
            }
            else if (checkBox2.Checked)
            {
                Vend = "1";
            }

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE users SET phone_number = @phone, email = @Email, cust_active = @Cust, vendor_active = @Vend, cust_name = @CustName, cust_address = @CustAddress, cust_latitude = @CustLatitude, cust_longitude = @CustLongitude, vendor_name = @VendorName, vendor_address = @VendorAddress, vendor_latitude = @VendorLatitude, vendor_longitude = @VendorLongitude WHERE phone_number = '"+phones+"'",
                    conn);

                cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Cust", Cust);
                cmd.Parameters.AddWithValue("@Vend", Vend);
                cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);
                cmd.Parameters.AddWithValue("@CustAddress", txtCustAddress.Text);
                cmd.Parameters.AddWithValue("@CustLatitude", txtCustLatitude.Text);
                cmd.Parameters.AddWithValue("@CustLongitude", txtCustLogitude.Text);
                cmd.Parameters.AddWithValue("@VendorName", txtVendorName.Text);
                cmd.Parameters.AddWithValue("@VendorAddress", txtVendorAddress.Text);
                cmd.Parameters.AddWithValue("@VendorLatitude", txtVendorLatitude.Text);
                cmd.Parameters.AddWithValue("@VendorLongitude", txtVendorLongitude.Text);
                cmd.Parameters.AddWithValue("@OriginalPhone", phones);

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Berhasil Melakukan update, Silahkan Melakukan Login Kembali", "UPDATE BERHASIL!!!");
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click OK TO COUNTINUE", "DISCARDING . . .");
            MessageBox.Show("Please Log In Again", "Error Occured");
            Form1 Form1 = new Form1();
            Form1.Show();
            
            //MainForm form = new MainForm();
            //form.Show();
            this.Close();
          
        }
    }
}
