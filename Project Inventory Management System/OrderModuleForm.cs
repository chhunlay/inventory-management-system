using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Project_Inventory_Management_System
{
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Sev Thorth\Documents\dbMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int product_qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT customerid, customername FROM tbCustomer WHERE CONCAT(customerid,customername) LIKE '%" + txtSearchCustomer.Text + "%'  ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT (product_id,product_name,product_qty,product_price,product_description,product_category) LIKE '%" + txtSearchProduct.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();
        }


        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }
        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
       
        private void UDQTY_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt16(UDQTY.Value) > product_qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UDQTY.Value = UDQTY.Value - 1;
                return;
            }

            int total = Convert.ToInt32(txtPrice.Text) * Convert.ToInt32(UDQTY.Value); 
            txtTotal.Text = total.ToString();
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerID.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCustomerName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            product_qty = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerID.Text == "")
                {
                    MessageBox.Show("Please select customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtProductID.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(orderdate,product_id,product_name,customerid,customername,product_qty,product_price,total)VALUES(@orderdate,@product_id,@product_name,@customerid,@customername,@product_qty,@product_price,@total)", con);
                    cm.Parameters.AddWithValue("@orderdate", orderDate.Value);
                    cm.Parameters.AddWithValue("@product_id", Convert.ToInt32(txtProductID.Text));
                    cm.Parameters.AddWithValue("@product_name", txtProductName.Text);
                    cm.Parameters.AddWithValue("@customerid", Convert.ToInt32(txtCustomerID.Text));
                    cm.Parameters.AddWithValue("@customername", txtCustomerName.Text);
                    cm.Parameters.AddWithValue("@product_qty", Convert.ToInt32(UDQTY.Value));
                    cm.Parameters.AddWithValue("@product_price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully saved.");

                    cm = new SqlCommand("UPDATE tbProduct SET product_qty=(product_qty-@product_qty) WHERE product_id LIKE '" + txtProductID.Text + "'", con);
                    cm.Parameters.AddWithValue("@product_qty", Convert.ToInt16(UDQTY.Value));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
        }
        public void Clear()
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();

            txtProductID.Clear();
            txtProductName.Clear();

            txtPrice.Clear();
            txtTotal.Clear();
            UDQTY.Value = 0;
            orderDate.Value = DateTime.Now;
        }

        public SqlDataReader GetDr()
        {
            return dr;
        }
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
