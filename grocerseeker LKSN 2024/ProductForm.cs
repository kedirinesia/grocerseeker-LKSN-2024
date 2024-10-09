using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace grocerseeker_LKSN_2024
{
    public partial class ProductForm : Form
    {
        int status;
        public string phones { get; set; }

        string nameNow;
        private const double EarthRadius = 6371;
        string VendorName;
        string ProductName;
        string categoryName;
        string unitType;
        int categoryID;
        int vendorID;
        string Productname;
        string CategoryName;
        string unit_type;
        float price_per_unit;
        float unit_stock;
        string CustName;

        float unitStock;
        float PricePerUnit;
        string mode;



        public ProductForm()
        {
            InitializeComponent();
        }
        void dgvCustomers()
        {
            MainForm mainForm = new MainForm();
            DataTable dt = new DataTable();
            dt = (DataTable)Connection.select("SELECT \r\n    users.vendor_name, \r\n    products.product_name, \r\n    categories.name AS Category_name, \r\n    products.unit_type, \r\n    products.price_per_unit, \r\n    products.unit_stock \r\nFROM \r\n    products \r\nINNER JOIN \r\n    users ON products.vendor_id = users.id \r\nINNER JOIN \r\n    categories ON products.category_id = categories.id \r\nWHERE \r\n    users.phone_number <> '" + phones + "' and products.unit_stock > 0");
            dgvCustomer.DataSource = dt;

        }

        public void dgvVendors()
        {

            DataTable dt = new DataTable();
            dt = (DataTable)Connection.select("SELECT \r\n   \r\n    products.product_name, \r\n\tcategories.name AS Category_Name,\r\n\t \r\n     \r\n    products.unit_type, \r\n    products.price_per_unit, \r\n    products.unit_stock \r\nFROM \r\n    products \r\nINNER JOIN \r\n    users ON products.vendor_id = users.id \r\nINNER JOIN \r\n    categories ON products.category_id = categories.id \r\nWHERE \r\n    users.phone_number = '" + phones + "'\r\n");
            dgvVendor.DataSource = dt;

        }

        private void ProductForm_Load(object sender, EventArgs e)
        {



            nameNow = txtVendorName.Text;
            txtCustUnitStock.DecimalPlaces = 2;  // Atur jumlah tempat desimal
            txtCustUnitStock.Minimum = 0;         // Atur nilai minimum
            txtCustUnitStock.Maximum = 100;       // Atur nilai maksimum
            txtCustUnitStock.Increment = 0.01m;   // Atur increment untuk penambahan

            // DataSet Item = (DataSet) Connection.select(" select name from categories");
            SqlConnection conn = Connection.GetConn();
            conn.Open();
            string query = "select name from categories";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            cmbVendorCategory.Items.Clear();

            while (rd.Read())
            {
                cmbVendorCategory.Items.Add(rd["name"].ToString());
                //MessageBox.Show(rd["name"].ToString());
            }
            rd.Close();





            MainForm mainForm = new MainForm();
            //  MessageBox.Show(phones);
            dgvCustomers();
            dgvVendors();



        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.transactionsTableAdapter.FillBy(this.grocerseekerDataSet.transactions);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvCustomer.Rows[e.RowIndex];

            VendorName = selectedRow.Cells["vendor_name"].Value.ToString();
            string ProductName = selectedRow.Cells["product_name"].Value.ToString();
            string CategoryName = selectedRow.Cells["Category_name"].Value.ToString();
            string UnitType = selectedRow.Cells["unit_type"].Value.ToString();
            PricePerUnit = float.Parse(selectedRow.Cells["price_per_unit"].Value.ToString());
            float unitStock = float.Parse(selectedRow.Cells["unit_stock"].Value.ToString());


            //---------VENDOR------------------//
            DataTable Vendordata = (DataTable)Connection.select("SELECT users.vendor_longitude, users.vendor_latitude FROM products INNER JOIN users ON products.vendor_id = users.id WHERE products.product_name= '" + ProductName + "'  and users.vendor_name = '" + VendorName + "'");
            // MessageBox.Show(VendorName);

            double vendorLat = Convert.ToDouble(Vendordata.Rows[0]["vendor_latitude"]) / 1000.0;
            double vendorLon = Convert.ToDouble(Vendordata.Rows[0]["vendor_longitude"]) / 1000.0;
            //---------!VENDOR------------------//
            //---------CUST----------//
            DataTable customerData = (DataTable)Connection.select($"SELECT cust_latitude, cust_longitude FROM users WHERE phone_number = '{phones}'");
            double customerLat = Convert.ToDouble(customerData.Rows[0]["cust_latitude"]) / 1000.0;
            double customerLon = Convert.ToDouble(customerData.Rows[0]["cust_longitude"]) / 1000.0;



            txtCustName.Text = ProductName;
            txtCustPrice.Value = (decimal)PricePerUnit;
            cmbCustCategory.Text = CategoryName;
            txtCustUnitStock.Value = (decimal)unitStock;

            if (UnitType == "measurable")
            {
                radioCustMeasurable.Checked = true;
            }
            else if (UnitType == "countable")
            {
                radioCustCountable.Checked = true;
            }

            //----------------------------------------------------
            if (txtCustQuantity.Value > txtCustUnitStock.Value)
            {
                lblperingatan.Text = "cannot calculate when quantity higher then stock";
                lblperingatan.Visible = true;
                lbltotal.Text = "ERROR QUANTITY";
                return;
            }
            else
            {
                lblperingatan.Visible = false;
            }

            float quantiti = (float)txtCustQuantity.Value;
            float totalHarga = PricePerUnit * quantiti;
            lbltotal.Text = totalHarga.ToString();


            double distance = Haversine(vendorLat, vendorLon, customerLat, customerLon);
            double shippingCost = CalculateShippingCost(distance);


            //MessageBox.Show($"Vendor Latitude: {vendorLat}, Vendor Longitude: {vendorLon}");
            //MessageBox.Show($"Customer Latitude: {customerLat}, Customer Longitude: {customerLon}");
            lblDelivery.Text = shippingCost.ToString("C", new CultureInfo("id-ID"));
        }


        private void txtCustQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (txtCustQuantity.Value > txtCustUnitStock.Value)
            {

                lblperingatan.Text = "cannot calculate when quantity higher then stock";

                lblperingatan.Visible = true;

                lbltotal.Text = "ERROR QUANTITY";
                lbltotal.ForeColor = Color.Red;
                return;




            }
            else
            {
                lbltotal.ForeColor = Color.Black;
                lblperingatan.Visible = false;
            }


            float quantiti = (float)txtCustQuantity.Value;
            float totalHarga = PricePerUnit * quantiti;
            lbltotal.Text = totalHarga.ToString();


            //---------VENDOR------------------//


            //DataTable Vendordata = (DataTable)Connection.select("SELECT       users.vendor_longitude, users.vendor_latitude\r\nFROM            products INNER JOIN\r\n                         users ON products.vendor_id = users.id\r\n\t\t\t\t\t\t where products.product_name= '" + ProductName + "'  and users.vendor_name = '" + VendorName + "'");
            //double vendorLat = Convert.ToDouble(Vendordata.Rows[0]["vendor_latitude"]);
            //double vendorLon = Convert.ToDouble(Vendordata.Rows[0]["vendor_longitude"]);


            //---------CUST----------//
            //DataTable customerData = (DataTable)Connection.select($"SELECT cust_latitude, cust_longitude FROM users WHERE phone_number = '{phones}'");
            //double customerLat = Convert.ToDouble(customerData.Rows[0]["cust_latitude"]);
            //double customerLon = Convert.ToDouble(customerData.Rows[0]["cust_longitude"]);
            ////------------------------------------
            //double distance = Haversine(vendorLat, vendorLon, customerLat, customerLon);
            //double shippingCost = CalculateShippingCost(distance);


            //lblDelivery.Text = shippingCost.ToString();

        }

        private void dgvVendor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtCustQuantity_TabIndexChanged(object sender, EventArgs e)
        {
            if (txtCustQuantity.Value > txtCustUnitStock.Value)
            {

                lblperingatan.Text = "cannot calculate when quantity higher then stock";

                lblperingatan.Visible = true;



            }
            else
            {
                lblperingatan.Visible = false;
            }
        }

        private void txtCustQuantity_Leave(object sender, EventArgs e)
        {
            if (txtCustQuantity.Value > txtCustUnitStock.Value)
            {

                lblperingatan.Text = "cannot calculate when quantity higher then stock";

                lblperingatan.Visible = true;



            }
            else
            {
                lblperingatan.Visible = false;
            }
        }

        private void txtCustQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show("GUNAKAN TOMBOL UP AND DOWN!!!", "PERINGATAN!!!!");
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panelvendor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvVendor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = false;
            txtVendorName.Enabled = false;
            cmbVendorCategory.Enabled = false;
            cmbVendorStatus.Enabled = false; tctVendorUnit.Enabled = false;
            txtVendorPrice.Enabled = false;





            groupBox4.Enabled = true;
            btnCancel.Enabled = true;
            btnAddItem.Enabled = false;
            btnEditItem.Enabled = true;
            btnDeleteItem.Enabled = true;
            //  groupBox4.Enabled = false;


            if (e.RowIndex < 0) return;


            DataGridViewRow selectedRow = dgvVendor.Rows[e.RowIndex];


            Productname = selectedRow.Cells["product_name"].Value?.ToString() ?? string.Empty;
            CategoryName = selectedRow.Cells["category_name"].Value?.ToString() ?? string.Empty;
            unit_type = selectedRow.Cells["unit_type"].Value?.ToString() ?? string.Empty;

            nameNow = Productname;
            // MessageBox.Show(nameNow);
            price_per_unit = float.TryParse(selectedRow.Cells["price_per_unit"].Value?.ToString(), out var price) ? price : 0;
            unit_stock = float.TryParse(selectedRow.Cells["unit_stock"].Value?.ToString(), out var stock) ? stock : 0;

            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                // SqlCommand cmd = new SqlCommand("select is_active from products where product_name = '" + ProductName + "'", conn);
                SqlCommand cmd = new SqlCommand("select is_active from products where product_name = @Productname", conn);
                cmd.Parameters.AddWithValue("@Productname", Productname);


                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    // MessageBox.Show("Status: " + status.ToString());

                    status = int.Parse(rd["is_active"].ToString());
                    // MessageBox.Show("Status: " + status.ToString());

                    if (status == 1)
                    {
                        cmbVendorStatus.Enabled = true;
                        cmbVendorStatus.Text = "active";
                        cmbVendorStatus.Enabled = false;
                        cmbVendorStatus.Refresh();
                    }
                    else if (status == 0)
                    {
                        cmbVendorStatus.Enabled = true;
                        cmbVendorStatus.Text = "inactive";
                        cmbVendorStatus.Enabled = false;
                        cmbVendorStatus.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("error");
                    }
                }
            }
            // cmbCustCategory.Text = "inactive";
            // MessageBox.Show(status.ToString());

            txtVendorName.Text = Productname;


            cmbVendorCategory.Text = CategoryName;


            if (unit_type == "measurable")
            {
                radioVendorMeasurable.Checked = true;
            }
            else if (unit_type == "countable")
            {
                radioVendorCountable.Checked = true;
            }

            txtVendorPrice.Value = (decimal)price_per_unit;
            tctVendorUnit.Value = (decimal)unit_stock;


            //MessageBox.Show($"Unit Type: {unit_type}");
            //MessageBox.Show($"Category Name: {CategoryName}");
        }






        public double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadius * c;
        }

        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        public double CalculateShippingCost(double distance)
        {



            return (distance / 100) * 15000;


        }

        private void btnCustClear_Click(object sender, EventArgs e)
        {
            txtCustUnitStock.Value = 0;
            txtCustPrice.Value = 0;
            txtCustName.Text = "";
            cmbCustCategory.Text = "";
            PricePerUnit = 0;
            unitStock = 0;
            txtCustQuantity.Value = 0;
            lblDelivery.Text = "0";
            MessageBox.Show("Clearing....", "Press OK to Continue");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Canceling......", "Press OK To Continue");
            groupBox4.Enabled = false;
            cmbVendorStatus.Text = "";
            cmbVendorCategory.Text = "";
            btnEditItem.Enabled = false;
            btnDeleteItem.Enabled = false;
            btnAddItem.Enabled = true;
            txtVendorName.Text = "";
            cmbVendorCategory.Items.Clear();
            radioVendorCountable.Checked = false;
            txtVendorPrice.Value = 0;
            tctVendorUnit.Value = 0;
            radioVendorCountable.Checked = false;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

            btnSave.Enabled = true;
            txtVendorName.Enabled = true;
            cmbVendorCategory.Enabled = true;
            cmbVendorStatus.Enabled = true; tctVendorUnit.Enabled = true;
            txtVendorPrice.Enabled = true;

            SqlConnection conn = Connection.GetConn();
            conn.Open();
            string query = "select name from categories";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            cmbVendorCategory.Items.Clear();

            while (rd.Read())
            {
                cmbVendorCategory.Items.Add(rd["name"].ToString());
                //MessageBox.Show(rd["name"].ToString());
            }
            rd.Close();
            txtVendorName.Focus();
            groupBox4.Enabled = true;
            mode = "1";
            //MessageBox.Show(mode);
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            txtVendorName.Enabled = true;
            cmbVendorCategory.Enabled = true;
            cmbVendorStatus.Enabled = true; tctVendorUnit.Enabled = true;
            txtVendorPrice.Enabled = true;
            mode = "2";
            //MessageBox.Show(mode);
            groupBox4.Enabled = true;
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {




            mode = "3";
            DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {



                SqlConnection conn = Connection.GetConn();
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete from products where product_name = '" + txtVendorName.Text + "'", conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product deleted successfully.", "DELETED!");
                dgvVendors();
            }
            else
            {
                MessageBox.Show("Delete operation cancelled.");
            }



            // MessageBox.Show(mode);
        }



        void VendorID()
        {
            SqlConnection conn = Connection.GetConn();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT products.vendor_id\r\nFROM products\r\nJOIN users ON products.vendor_id = users.id; ", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {



                vendorID = int.Parse(rd["vendor_id"].ToString());



            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtVendorName.Text) || string.IsNullOrEmpty(txtVendorPrice.Text) || string.IsNullOrEmpty(cmbVendorCategory.Text) || string.IsNullOrEmpty(cmbVendorStatus.Text))
            {
                MessageBox.Show("DATA CANNOT BE NULL, FILL ALL FORM !!!", "ERROR, CLICK OK TO CONTINUE!");
                return;
            }
            if (tctVendorUnit.Value < 1)
            {
                MessageBox.Show("Need At least 1 Items On Stock");
                return;
            }

            if (radioVendorCountable.Checked)
            {
                unitType = "countable";
            }
            else if (radioVendorMeasurable.Checked)
            {
                unitType = "measurable";
            }

            VendorID();

            if (mode == "1")
            {
                // Insert function
                Connection.query("INSERT INTO products (vendor_id, product_name, category_id, unit_type, price_per_unit, unit_stock, is_active) VALUES (" + vendorID + ", '" + txtVendorName.Text + "', " + categoryID + ", '" + unitType + "', " + txtVendorPrice.Value + ", " + tctVendorUnit.Value + ", " + Convert.ToInt32(cmbVendorStatus.Text) + ");");
                MessageBox.Show("Adding Data", "Click OK TO CONTINUE");
                MessageBox.Show("Data Added Successfully");
                dgvVendors();
            }
            else if (mode == "2")
            {
                // Update function
                string query = "UPDATE products SET product_name = @productName, unit_type = @unitType, price_per_unit = @pricePerUnit, unit_stock = @unitStock, is_active = @isActive WHERE product_name = @nameNow";

                try
                {
                    using (SqlConnection connection = Connection.GetConn())
                    {
                        connection.Open();
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@productName", txtVendorName.Text);
                            command.Parameters.AddWithValue("@unitType", unitType);
                            command.Parameters.AddWithValue("@pricePerUnit", txtVendorPrice.Value);
                            command.Parameters.AddWithValue("@unitStock", tctVendorUnit.Value);
                            command.Parameters.AddWithValue("@isActive", Convert.ToInt32(cmbVendorStatus.Text));
                            command.Parameters.AddWithValue("@nameNow", nameNow);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Data Updated Successfully");
                                dgvVendors();
                            }
                            else
                            {
                                MessageBox.Show("No rows were updated.");
                                MessageBox.Show($"Name Now: {nameNow}");

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }


            }
            else if (mode == "3")
            {

            }
            else
            {
                MessageBox.Show("ERROR OCCURRED, PLEASE RE LOG IN");
            }
        }


        private void cmbVendorCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendorCategory.Text == "production")
            {
                categoryID = 1;
            }
            else if (cmbVendorCategory.Text == "dairy")
            {
                categoryID = 2;
            }
            else if (cmbVendorCategory.Text == "meat")
            {
                categoryID = 3;
            }
            else if (cmbVendorCategory.Text == "bakery")
            {
                categoryID = 4;
            }
            else if (cmbVendorCategory.Text == "pantry")
            {
                categoryID = 5;
            }
            else if (cmbVendorCategory.Text == "beverages")
            {
                categoryID = 6;
            }
            else if (cmbVendorCategory.Text == "frozen foods")
            {
                categoryID = 7;
            }
            else if (cmbVendorCategory.Text == "household goods")
            {
                categoryID = 8;
            }
            else if (cmbVendorCategory.Text == "personal care")
            {
                categoryID = 9;
            }
            else if (cmbVendorCategory.Text == "baby products")
            {
                categoryID = 10;
            }
            else if (cmbVendorCategory.Text == "collection")
            {
                categoryID = 11;
            }
            // MessageBox.Show(categoryID.ToString());
        }

        private void radioVendorCountable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioVendorCountable.Checked)
            {
                unitType = "countable";
            }
            else if (radioVendorMeasurable.Checked)
            {
                unitType = "measurable";
            }
        }

        private void radioVendorMeasurable_CheckedChanged(object sender, EventArgs e)
        {
            if (radioVendorCountable.Checked)
            {
                unitType = "countable";
            }
            else if (radioVendorMeasurable.Checked)
            {
                unitType = "measurable";
            }
        }

        private void BtnBuyItem_Click(object sender, EventArgs e)
        {
        }

        private void BtnBuyItem_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show(VendorName);
            //NOTE------------
            //            INSERT KE TRANSACTIONS
            //            product_name = textbox di Product Area
            //            Vendor_ name = DGV di Product area
            //            Quantity = Numeric di Product Area
            //            price_per_unit = Numeric di product Area
            //            total_ price = label di Product Area
            //            delivery_ cost = label di Product Area
            //            status = diisi otomatis "Pending"


            //variabel untuk mempermudah

            //table Products
            string productName = txtCustName.Text;
            decimal pricePerUnit = txtCustPrice.Value;
            //table users
            string name = VendorName;
            //string customerName;
            //table Transactions
            decimal Quantity = txtCustQuantity.Value;
            decimal totalPrice = int.Parse(lbltotal.Text);
            decimal deliveryCost = decimal.Parse(lblDelivery.Text);
           
            string status = "pending";

            SqlConnection connn = Connection.GetConn();
            
                SqlCommand cmdd = new SqlCommand("select cust_name from users where phone_number = '"+phones+"'",connn);
                SqlDataReader rd = cmdd.ExecuteReader();
                while (rd.Read())
            {
                  
                    CustName = rd["cust_name"].ToString();
               

            }


            MessageBox.Show(CustName);



            using (SqlConnection conn = Connection.GetConn())
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {

                        int vendorId = InsertVendorIfNotExists(name, conn, transaction);


                        int productId = InsertProduct(productName, pricePerUnit, vendorId, conn, transaction);


                        using (SqlCommand cmdTransaction = new SqlCommand("INSERT INTO transactions (vendor_id, product_id, quantity, total_price, delivery_cost, status) VALUES (@VendorId, @ProductId, @Quantity, @TotalPrice, @DeliveryCost, @Status)", conn, transaction))
                        {
                            cmdTransaction.Parameters.AddWithValue("@VendorId", vendorId);
                            cmdTransaction.Parameters.AddWithValue("@ProductId", productId);
                            cmdTransaction.Parameters.AddWithValue("@Quantity", Quantity);
                            cmdTransaction.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            cmdTransaction.Parameters.AddWithValue("@DeliveryCost", deliveryCost);
                            cmdTransaction.Parameters.AddWithValue("@Status", status);
                            cmdTransaction.Parameters.AddWithValue("@CustomerName", CustName);
                            cmdTransaction.ExecuteNonQuery();
                        }


                        transaction.Commit();
                        MessageBox.Show("Transaction inserted successfully.");
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }

      
        private int InsertVendorIfNotExists(string vendorName, SqlConnection conn, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM users WHERE vendor_name = @VendorName) BEGIN INSERT INTO users (vendor_name) VALUES (@VendorName); SELECT SCOPE_IDENTITY(); END ELSE BEGIN SELECT id FROM users WHERE vendor_name = @VendorName; END", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@VendorName", vendorName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        
        private int InsertProduct(string productName, decimal pricePerUnit, int vendorId, SqlConnection conn, SqlTransaction transaction)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO products (product_name, price_per_unit, vendor_id) OUTPUT INSERTED.id VALUES (@ProductName, @PricePerUnit, @VendorId)", conn, transaction))
            {
                cmd.Parameters.AddWithValue("@ProductName", productName);
                cmd.Parameters.AddWithValue("@PricePerUnit", pricePerUnit);
                cmd.Parameters.AddWithValue("@VendorId", vendorId);
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}


    