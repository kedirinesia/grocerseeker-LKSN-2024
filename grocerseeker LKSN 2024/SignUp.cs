using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace grocerseeker_LKSN_2024
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string custlatitude =  txtCustLatitude.Text;
            string custlongitude =  txtCustLongtitude.Text;


            string Vendorlatitude = txtVendorLatitude.Text;
            string Vendolongitude =  txtVendorLongitude.Text;

            // if (!float.TryParse(txtCustLatitude.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float latitude) ||
            //!float.TryParse(txtCustLongtitude.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float longitude) ||
            //!float.TryParse(txtVendorLatitude.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float Vlatitude) ||
            //!float.TryParse(txtVendorLongtitude.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float Vlongitude))
            // {
            //     MessageBox.Show("Please enter valid numeric values for latitude and longitude.", "Input Error");
            //     return;  
            // }



              int customer;
            int vendor;

            if (checkBox1.Checked && checkBox2.Checked)
            {

                if (string.IsNullOrEmpty(txtCustAddress.Text) || string.IsNullOrEmpty(txtCustName.Text)
                    || string.IsNullOrEmpty(txtCustLatitude.Text) || string.IsNullOrEmpty(txtCustLongtitude.Text)
                    || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtpass.Text)
                    || string.IsNullOrEmpty(txtconfirm.Text) || string.IsNullOrEmpty(txtvendorName.Text) || string.IsNullOrEmpty(txtVendorAddress.Text) 
                    || string.IsNullOrEmpty(txtVendorLatitude.Text) || string.IsNullOrEmpty(txtVendorLongitude.Text)
                    
                    
                                                                                        )
                {
                    MessageBox.Show("harap isi semua textbox");
                    return;
                }

           //     MessageBox.Show("2");
                customer = 1;
                vendor = 1;
                Connection.query("INSERT INTO users (phone_number, email, password, vendor_active, vendor_name, vendor_address, vendor_latitude, vendor_longitude, cust_active, cust_name, cust_address, cust_latitude, cust_longitude) VALUES ('"
     + txtPhone.Text + "', '"
     + txtEmail.Text + "', '"
     + txtpass.Text + "', 1, '"
     + txtvendorName.Text + "', '"
     + txtVendorAddress.Text + "', '"
     + Vendorlatitude + "', '"
     + Vendolongitude + "', 1, '"
     + txtCustName.Text + "', '"
     + txtCustAddress.Text + "', '"
     + custlatitude + "', '"
     + custlongitude + "');");



                //  Connection.query("insert into users (phone_number, email, password,) values ('" + txtPhone.Text + "', '" + txtEmail.Text + "', '" + txtpass.Text + "', " + customer + " , '" + txtCustName.Text + "' , '" + txtCustAddress.Text + "', '" + custlatitude + "', '" + custlongitude + "')\r\n");
                MessageBox.Show("You have successfully registered", "Congratulations");
                MessageBox.Show("Silahkan Melakukan Login !", "Notification");


                this.Hide();
                return;
            }

            if (checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(txtCustAddress.Text) || string.IsNullOrEmpty(txtCustName.Text)
                    || string.IsNullOrEmpty(txtCustLatitude.Text) || string.IsNullOrEmpty(txtCustLongtitude.Text)
                    || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtpass.Text)
                    || string.IsNullOrEmpty(txtconfirm.Text))
                {
                    MessageBox.Show("harap isi semua textbox");
                    return;
                }
                customer = 1;
                Connection.query("insert into users (phone_number, email, password, cust_active, cust_name, cust_address, cust_latitude, cust_longitude) values ('"+txtPhone.Text+"', '"+txtEmail.Text+"', '"+txtpass.Text+"', "+customer+" , '"+txtCustName.Text+"' , '"+txtCustAddress.Text+"', '"+custlatitude+"', '"+custlongitude+"')\r\n");
                MessageBox.Show("You have successfully registered", "Congratulations");
                MessageBox.Show("Silahkan Melakukan Login !", "Notification");
               

                this.Hide();
                return;
            }
              if (checkBox2.Checked)
            {

                if ( string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtpass.Text)
                   || string.IsNullOrEmpty(txtconfirm.Text) || string.IsNullOrEmpty(txtvendorName.Text) || string.IsNullOrEmpty(txtVendorAddress.Text)
                    || string.IsNullOrEmpty(txtVendorLatitude.Text) || string.IsNullOrEmpty(txtVendorLongitude.Text))
                {
                    MessageBox.Show("harap isi semua textbox");
                    return;
                }

                vendor = 1;
                
                Connection.query("insert into users (phone_number, email, password, vendor_active, vendor_name, vendor_address, vendor_latitude, vendor_longitude) values ('" + txtPhone.Text + "', '" + txtEmail.Text + "', '" + txtpass.Text + "', " + vendor + " , '" + txtvendorName.Text + "' , '" + txtVendorAddress.Text + "', '" + Vendorlatitude + "', '" + Vendolongitude + "')\r\n");
                MessageBox.Show("You have successfully registered", "Congratulations");
                MessageBox.Show("Silahkan Melakukan Login !", "Notification");


                this.Hide();
                return;
            }




            






        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPhone.Text) &&
     !string.IsNullOrEmpty(txtEmail.Text) &&
     !string.IsNullOrEmpty(txtpass.Text) &&
     !string.IsNullOrEmpty(txtconfirm.Text) &&
     (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }

            string input = txtpass.Text;

            if(txtpass.Text == "" && txtconfirm.Text == "")
            {
                lblcaution.Text = "                   Caution : Password Cannot be null";
                return;
            }

            if (input.Length < 8)
            {
                label17.Text = "password must be at least 8 char long";
                return;
            }
            else
            {
                label17.Text = "";
            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
        }

        private void txtconfirm_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPhone.Text) &&
     !string.IsNullOrEmpty(txtEmail.Text) &&
     !string.IsNullOrEmpty(txtpass.Text) &&
     !string.IsNullOrEmpty(txtconfirm.Text) &&
     (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }


            string inputt = txtconfirm.Text;
            int min = 8;

            if (inputt.Length < 8)
            {
                label17.Text = "password must be at least 8 char long";
                return;
            }
            else
            {
                label17.Text = "";
            }



            if (txtpass.Text != txtconfirm.Text)
            {
                lblcaution.Text = "Password Confirmation doesnt match with the inputed password";
            }
            else
            {
                // MessageBox.Show("berhasil");
                lblcaution.Text = "";
            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text) &&
    !string.IsNullOrEmpty(txtEmail.Text) &&
    !string.IsNullOrEmpty(txtpass.Text) &&
    !string.IsNullOrEmpty(txtconfirm.Text) &&
    (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }

            int min = 10;
            if (txtPhone.Text.Length < min)
            {
                lblangka.Text ="nomor minimal memiliki 10 digit";
            }
            else
            {
                lblangka.Text = "";
            }

            if (txtPhone.Text.Length == 15)
            {

                MessageBox.Show("Batas maksimal angka adalah 15");

            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
            SqlConnection conn = Connection.GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("select phone_number from users where phone_number = '"+txtPhone.Text+"'", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                lblcaution2.Visible = true;
               
            }
            else
            {
                lblcaution2.Visible = false;
               
            }

            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text) &&
     !string.IsNullOrEmpty(txtEmail.Text) &&
     !string.IsNullOrEmpty(txtpass.Text) &&
     !string.IsNullOrEmpty(txtconfirm.Text) &&
     (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }

            if (checkBox1.Checked)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPhone.Text) &&
    !string.IsNullOrEmpty(txtEmail.Text) &&
    !string.IsNullOrEmpty(txtpass.Text) &&
    !string.IsNullOrEmpty(txtconfirm.Text) &&
    (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }

            if (checkBox2.Checked)
            {
                groupBox2.Visible = true;
            }
            else
            {
                groupBox2.Visible = false;
            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true  || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPhone.Text) &&
    !string.IsNullOrEmpty(txtEmail.Text) &&
    !string.IsNullOrEmpty(txtpass.Text) &&
    !string.IsNullOrEmpty(txtconfirm.Text) &&
    (checkBox1.Checked || checkBox2.Checked))
            {
                button1.Enabled = true;
                button1.Text = "Register";
            }
            else
            {
                button1.Enabled = false;
                button1.Text = "Register (disabled)";
            }

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";


            string email = txtEmail.Text;


            

             if (!Regex.IsMatch(email, pattern))
            {
              
                label18.Text = "Format email tidak valid!";
            }

            else
            {
               
                label18.Text = "";
            }
            //if (lblcaution.Visible == true || lblcaution2.Visible == true || lblangka.Visible == true || label17.Visible == true  || label18.Visible == true)
            //{
            //    button1.Enabled = false;
            //    button1.Text = "Register (disabled)";
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    button1.Text = "Register";
            //}
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
           
        }

        private void lblcaution_Click(object sender, EventArgs e)
        {
          
        }
    }
}
