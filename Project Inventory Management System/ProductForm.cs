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
    public partial class ProductForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Sev Thorth\Documents\dbMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct", con);
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

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "EditCustomer")
            {
                ProductModuleForm formModule = new ProductModuleForm();
                formModule.lblProductID.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                formModule.txtProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                formModule.txtQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                formModule.txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                formModule.txtDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                formModule.comboCategory.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                formModule.btnSave.Enabled = false;
                formModule.btnUpdate.Enabled = true;
                formModule.ShowDialog();
            }
            else if (colName == "DeleteCustomer")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbProduct WHERE product_id LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadProduct();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductModuleForm productform = new ProductModuleForm();
            productform.btnSave.Enabled = true;
            productform.btnUpdate.Enabled = false;
            productform.ShowDialog();
            LoadProduct();
        }
    }
}
