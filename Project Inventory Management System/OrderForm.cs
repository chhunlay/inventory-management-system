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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Sev Thorth\Documents\dbMS.mdf"";Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }
        public void LoadOrder()
        {
            int i = 0;
            dgvorder.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbOrder", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvorder.Rows.Add(i, dr[0].ToString(), Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void lblQTY_Click(object sender, EventArgs e)
        {

        }

        private void dgvorder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvorder.Columns[e.ColumnIndex].Name;
             if (colName == "Edit")
             {
                /*   OrderModuleForm formModule = new OrderModuleForm();
                  formModule.lblOrderID.Text = dgvorder.Rows[e.RowIndex].Cells[1].Value.ToString();
                  formModule.orderDate.Text = dgvorder.Rows[e.RowIndex].Cells[2].Value.ToString();
                  formModule.txtProductID.Text = dgvorder.Rows[e.RowIndex].Cells[3].Value.ToString();
                  formModule.txtProductName.Text = dgvorder.Rows[e.RowIndex].Cells[4].Value.ToString();
                  formModule.txtCustomerID.Text = dgvorder.Rows[e.RowIndex].Cells[5].Value.ToString();
                  formModule.txtCustomerName.Text = dgvorder.Rows[e.RowIndex].Cells[6].Value.ToString();
                  formModule.UDQTY.Value =Convert.ToInt32(dgvorder.Rows[e.RowIndex].Cells[7].Value.ToString());
                  formModule.txtPrice.Text = dgvorder.Rows[e.RowIndex].Cells[8].Value.ToString();

                  formModule.btnSave.Enabled = false;
                  formModule.btnUpdate.Enabled = true;
                  formModule.ShowDialog();*/
            }
            else if (colName == "Delete")
             {
                 if (MessageBox.Show("Are you sure you want to delete this Product?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                     con.Open();
                     cm = new SqlCommand("DELETE FROM tbOrder WHERE orderid LIKE '" + dgvorder.Rows[e.RowIndex].Cells[1].Value.ToString() + "' ", con);
                     cm.ExecuteNonQuery();
                     con.Close();
                     MessageBox.Show("Record has been successfully deleted!");
                 }
             }
             LoadOrder();
         }
           
            private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm orderModuleForm = new OrderModuleForm();
            orderModuleForm.btnSave.Enabled = true;
            orderModuleForm.btnUpdate.Enabled = false;
            orderModuleForm.ShowDialog();
            LoadOrder();
        }
    }
}
