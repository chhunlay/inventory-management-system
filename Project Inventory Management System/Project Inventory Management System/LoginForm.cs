using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Inventory_Management_System
{
    public partial class LoginForm : Form
    {
        private readonly object get;

        public LoginForm()
        {
            InitializeComponent();
        }
        private void Btn_login(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == false)
                txt_password.UseSystemPasswordChar = true;
            else
                txt_password.UseSystemPasswordChar = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application!", "Inventory System Mangement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            txt_password.Clear();
            txt_username.Clear();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
