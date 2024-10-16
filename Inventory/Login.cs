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
using System.Configuration;

namespace Inventory
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void clearcomp()
        {
            guna2TextBox2.Clear();
            guna2TextBox3.Clear();
            guna2TextBox2.Focus();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Facility].[dbo].[FACTBL_Login] where UserName='" + guna2TextBox2.Text + "' and Password='" + guna2TextBox3.Text + "'", connectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                //  MainMenu main = new MainMenu(dt.Rows[0][0].ToString());
                //    main.Show();

                Menu ss = new Menu(guna2TextBox2.Text);
                ss.Show();
                clearcomp();

            }
            else
            {
                MessageBox.Show("Invalid User Name or Password..!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearcomp();
            }
        }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            button2_Click_1(this, new EventArgs());
        }

        private void guna2TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click_1(this, new EventArgs());
            }
        }
    }
}
