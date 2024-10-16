using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class Menu : Form
    {
        public Menu(String user)
        {
            InitializeComponent();
            lblUser.Text = user;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void lblInput_Click(object sender, EventArgs e)
        {

            INCOMING ss = new INCOMING(lblUser.Text);
            ss.Show();
        }

        private void lblMaster_Click(object sender, EventArgs e)
        {
            STKQuery ss = new STKQuery(lblUser.Text);
            ss.Show();
        }

        private void lblOutput_Click(object sender, EventArgs e)
        {
            Outgoing ss = new Outgoing(lblUser.Text);
            ss.Show();
        }

        private void lblQuery_Click(object sender, EventArgs e)
        {
            STK_REG ss = new STK_REG(lblUser.Text);
            ss.Show();
        }
    }
}
