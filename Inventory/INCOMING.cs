using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Inventory
{
    public partial class INCOMING : Form
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd;
        SqlCommand cmdLog;
        public INCOMING(String user)
        {
            InitializeComponent();
            lblUser.Text = user;
            txtBarCode.Select();
            DateTime now = DateTime.Now;
            txtBarCode.Focus();
        }
        void Reset()
        {
            txtBarCode.Text = "";
            txtDes.Text = "";
            txtPartNo.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            pbProduct.Image = null;
            txtLocation.Text = "";
            txtStatus.Text = "";
            txtMOQ.Text = "";
            txtType.Text = "";
            txtRemarks.Text = "";
            lblMsg.Text = "";
            QtyChg.Text = "";
            lbval2.Text = "";
            txtBarCode.Focus();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {

            Reset();
            return;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            cmd = new SqlCommand("SELECT * FROM FACTBL_STK WHERE BarCode = '" + txtBarCode.Text + "'", con);
            cmdLog = new SqlCommand("SELECT * FROM FACTBL_LOG", con);
            object res = cmd.ExecuteScalar();
            con.Close();
            if (res != null && txtQty.Text != "" && txtMOQ.Text != "")

            {
                con.Open();
                txtStatus.Text = "☺ OK";
                int valMQO = int.Parse(txtMOQ.Text);
                int val = int.Parse(txtQty.Text);
                int val1 = int.Parse(QtyChg.Text);
                int val2 = val + val1;
                if (val2 < valMQO)
                {
                    txtStatus.Text = "$BUY";

                }
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update FACTBL_STK set QTY ='" + lbval2.Text + "',DES='" + txtDes.Text + "',PRICE='" + txtPrice.Text + "',LOCATION='" + txtLocation.Text + "',STATUS='" + txtStatus.Text + "',MOQ='" + txtMOQ.Text + "',PartNo='" + txtPartNo.Text + "',PType='" + txtType.Text + "',Remarks='" + txtRemarks.Text + "' where Barcode ='" + txtBarCode.Text + "'";

                cmd.ExecuteNonQuery();
                lblMsg.Text = "Product Updated successfully !" + "  Stock Status : " + txtStatus.Text;
                LogEntry();

                //clear
                txtBarCode.Text = "";
                txtDes.Text = "";
                txtPartNo.Text = "";
                txtPrice.Text = "";
                txtQty.Text = "";
                pbProduct.Image = null;
                txtLocation.Text = "";
                txtStatus.Text = "";
                txtMOQ.Text = "";
                QtyChg.Text = "";
                lbval2.Text = "";
                txtType.Text = "";
                txtRemarks.Text = "";

                con.Close();
            }

            else if (string.IsNullOrEmpty(txtDes.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtLocation.Text) || string.IsNullOrEmpty(txtStatus.Text) || pbProduct.Image == null)
            {
                MessageBox.Show("In-complete Input!", "INFORMATION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Product not Exist !", "Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBarCode.Text = "";
                txtDes.Text = "";
                txtPartNo.Text = "";
                txtPrice.Text = "";
                txtQty.Text = "";
                pbProduct.Image = null;
                txtLocation.Text = "";
                txtStatus.Text = "";
                txtMOQ.Text = "";
                txtType.Text = "";
                txtRemarks.Text = "";
            }
            txtBarCode.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void QtyChg_TextChanged(object sender, EventArgs e)
        {

            if (QtyChg.Text != "" && QtyChg.Text.Contains("-") == false && txtQty.Text != "")
            {
                int val = int.Parse(txtQty.Text);
                int val1 = int.Parse(QtyChg.Text);
                int val2 = val + val1;
                lbval2.Text = val2.ToString();
            }

        }

        private void QtyChg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public void LogEntry()
        {
            SqlConnection con = new SqlConnection(strcon);
            String CmdType;
            CmdType = "CHECK-IN";
            string saveLog = "INSERT into FACTBL_LOG (Barcode, Des, NewQty, OldQty, Entrytype, UDate, UName) " + " VALUES ('" + txtBarCode.Text + "','" + txtDes.Text + "','" + lbval2.Text + "','" + txtQty.Text + "','" + CmdType + "', '" + DateTime.Now + "','" + lblUser.Text + "');";
            cmd = new SqlCommand(saveLog, con);
            cmd.ExecuteNonQuery();
        }
    }
}
