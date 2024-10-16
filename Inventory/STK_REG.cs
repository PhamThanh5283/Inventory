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
    public partial class STK_REG : Form
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd;
        public STK_REG(String user)
        {
            InitializeComponent();
            lblUser.Text = user;
            btnAdd.Text = "ADD NEW";
            lblAction.Text = "NEW-ENTRY";
            btnBrowse.Visible = true;
            btnDLT.Visible = false;
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
            btnAdd.Text = "ADD NEW";
            btnBrowse.Visible = true;
            btnDLT.Visible = false;
            lblAction.Text = "NEW-ENTRY";

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Picture |*.png; *.jpg; *.gif; *.bmp";
            if (op.ShowDialog() == DialogResult.OK)
                pbProduct.Image = Image.FromFile(op.FileName);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            cmd = new SqlCommand("SELECT * FROM FACTBL_STK WHERE BarCode = '" + txtBarCode.Text + "'", con);

            object res = cmd.ExecuteScalar();
            con.Close();
            if (res != null)

            {
                con.Open();


                //---------------------------------------
                //   MemoryStream ms = new MemoryStream();
                //   pbProduct.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //  byte[] Pic_arr = new byte[ms.Length];
                //   ms.Position = 0;
                //   ms.Read(Pic_arr, 0, Pic_arr.Length);

                //---------------------------------------
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update FACTBL_STK set QTY ='" + txtQty.Text + "',DES='" + txtDes.Text + "',PRICE='" + txtPrice.Text + "',LOCATION='" + txtLocation.Text + "',STATUS='" + txtStatus.Text + "',MOQ='" + txtMOQ.Text + "',PartNo='" + txtPartNo.Text + "',PType='" + txtType.Text + "',Remarks='" + txtRemarks.Text + "' where Barcode ='" + txtBarCode.Text + "'";
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Product Exist, Updated successfully !", "Updated !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMsg.Text = "Product Updated successfully !";
                con.Close();
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
                txtType.Text = "";
                txtRemarks.Text = "";

            }


            // else if (string.IsNullOrEmpty(txtDes.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtLocation.Text) || string.IsNullOrEmpty(txtStatus.Text) || string.IsNullOrEmpty(txtMOQ.Text))
            else if (string.IsNullOrEmpty(txtDes.Text) || string.IsNullOrEmpty(txtQty.Text) || string.IsNullOrEmpty(txtLocation.Text) || string.IsNullOrEmpty(txtStatus.Text) || string.IsNullOrEmpty(txtMOQ.Text) || pbProduct.Image == null)

            {
                MessageBox.Show("In-complete Input!", "INFORMATION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                cmd = new SqlCommand("INSERT INTO FACTBL_STK Values (@Barcode,@Des, @Price, @Qty, @Picture, @Location, @Status, @MOQ, @PartNo, @PType, @Remarks)", con);

                SqlParameter[] param = new SqlParameter[11];

                MemoryStream ms = new MemoryStream();
                pbProduct.Image.Save(ms, pbProduct.Image.RawFormat);
                byte[] image = ms.ToArray();

                param[0] = new SqlParameter("@Barcode", SqlDbType.VarChar, 35);
                param[0].Value = txtBarCode.Text;

                param[1] = new SqlParameter("@Des", SqlDbType.NVarChar);
                param[1].Value = txtDes.Text;

                param[2] = new SqlParameter("@Price", SqlDbType.VarChar, 50);
                param[2].Value = txtPrice.Text;

                param[3] = new SqlParameter("@Qty", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(txtQty.Text);

                param[4] = new SqlParameter("@Picture", SqlDbType.Image);
                param[4].Value = image;

                param[5] = new SqlParameter("@Location", SqlDbType.NVarChar);
                param[5].Value = txtLocation.Text;

                param[6] = new SqlParameter("@Status", SqlDbType.NVarChar);
                param[6].Value = txtStatus.Text;

                param[7] = new SqlParameter("@MOQ", SqlDbType.NVarChar);
                param[7].Value = txtMOQ.Text;

                param[8] = new SqlParameter("@PartNo", SqlDbType.NVarChar);
                param[8].Value = txtPartNo.Text;

                param[9] = new SqlParameter("@PType", SqlDbType.NVarChar);
                param[9].Value = txtType.Text;

                param[10] = new SqlParameter("@Remarks", SqlDbType.NVarChar);
                param[10].Value = txtRemarks.Text;


                con.Open();
                cmd.Parameters.AddRange(param);
                cmd.ExecuteNonQuery();
                con.Close();
                // MessageBox.Show("Product added successfully !", "Added !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMsg.Text = "Product Added successfully !";
                LogEntry();
                txtBarCode.Text = "";
                txtDes.Text = "";
                txtPartNo.Text = "";
                txtPrice.Text = "";
                txtQty.Text = "";
                txtMOQ.Text = "";
                pbProduct.Image = null;
                txtLocation.Text = "";
                txtStatus.Text = "";
                txtType.Text = "";
                txtRemarks.Text = "";
            }
            txtBarCode.Focus();
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (e.KeyCode == Keys.Enter)
            {
                lblMsg.Text = "";
                con.Open();
                String selectQuery = "SELECT * FROM FACTBL_STK WHERE BarCode = '" + txtBarCode.Text + "'";
                cmd = new SqlCommand(selectQuery, con);
                SqlDataReader DataRead = cmd.ExecuteReader();
                DataRead.Read();


                if (DataRead.HasRows)
                {
                    btnAdd.Text = "UPDATE";
                    lblAction.Text = "UPDATE";
                    btnBrowse.Visible = false;
                    btnDLT.Visible = true;
                    txtDes.Text = DataRead[1].ToString();
                    txtPrice.Text = DataRead[2].ToString();
                    txtQty.Text = DataRead[3].ToString();
                    txtLocation.Text = DataRead[5].ToString();
                    txtStatus.Text = DataRead[6].ToString();
                    txtMOQ.Text = DataRead[7].ToString();
                    txtPartNo.Text = DataRead[8].ToString();
                    txtType.Text = DataRead[9].ToString();
                    txtRemarks.Text = DataRead[10].ToString();
                    txtQtyChg.Text = DataRead[3].ToString();
                    byte[] images = ((byte[])DataRead[4]);
                    if (images == null)

                    {
                        pbProduct.Image = null;
                    }

                    else
                    {

                        MemoryStream mstream = new MemoryStream(images);

                        pbProduct.Image = Image.FromStream(mstream);
                    }
                }
                else
                {

                    MessageBox.Show("This Item Not Exist !");
                }
                con.Close();
                DataRead.Close();
            }
        }



        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        public void LogEntry()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string saveLog = "INSERT into FACTBL_LOG (Barcode, Des, NewQty, OldQty, Entrytype, UDate, UName) " + " VALUES ('" + txtBarCode.Text + "','" + txtDes.Text + "','" + txtQty.Text + "','" + txtQtyChg.Text + "','" + lblAction.Text + "', '" + DateTime.Now + "','" + lblUser.Text + "');";
            cmd = new SqlCommand(saveLog, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnDLT_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(strcon);
            if (MessageBox.Show("Proceed Delete ?", "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    lblAction.Text = "DELETE";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlCommand sqlCmd = new SqlCommand("stkDeletion", con);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@Barcode", txtBarCode.Text);
                    sqlCmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Deleted successfully");

                    LogEntry();
                    Reset();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }

                con.Close();

            }
            else
                MessageBox.Show("Data Not Deleted, Record not found !", "Delete Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
