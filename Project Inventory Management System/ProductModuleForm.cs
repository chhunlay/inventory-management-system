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

namespace Project_Inventory_Management_System
{
    public partial class ProductModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Sev Thorth\Documents\dbMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            comboCategory.Items.Clear();
            cm = new SqlCommand("SELECT categoryname FROM tbCategory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                comboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtProductName.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this Product?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbProduct SET product_name=@product_name,product_qty = @product_qty,product_price=@product_price,product_description = @product_description,product_category = @product_category WHERE product_id Like '" + lblProductID.Text + "'", con);
                    cm.Parameters.AddWithValue("@product_name", txtProductName.Text);
                    cm.Parameters.AddWithValue("@product_qty", txtQuantity.Text);
                    cm.Parameters.AddWithValue("@product_price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@product_description", txtDescription.Text);
                    cm.Parameters.AddWithValue("@product_category", comboCategory.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully updated.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this Product?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbProduct (product_name,product_qty,product_price,product_description,product_category)VALUES(@product_name,@product_qty,@product_price,@product_description,@product_category)", con);
                    cm.Parameters.AddWithValue("@product_name", txtProductName.Text);
                    cm.Parameters.AddWithValue("@product_qty", txtQuantity.Text);
                    cm.Parameters.AddWithValue("@product_price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@product_description", txtDescription.Text);
                    cm.Parameters.AddWithValue("@product_category", comboCategory.Text);

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
