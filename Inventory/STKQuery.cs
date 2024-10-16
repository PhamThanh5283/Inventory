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
    public partial class STKQuery : Form
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //SqlConnection con = new SqlConnection(strcon);
        SqlCommand sqlcmdSTG;
        public STKQuery(String user)
        {
            InitializeComponent();
            lblUser.Text = user;
        }
        public void disp_data()
        {
            SqlConnection STGsqlcon = new SqlConnection(strcon);
            STGsqlcon.Open();
            SqlCommand cmd = STGsqlcon.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * FROM FACTBL_STK";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            STGdataGridView.DataSource = dt;
            STGsqlcon.Close();

        }
        void Reset()
        {
            comboBox3.Text = comboBox4.Text = comboBox5.Text = comboBox6.Text = comboBox7.Text = comboBox8.Text = comboBox7.Text = comboBox1.Text = txtSearch.Text = ""; cbPicture.Image = null;
            btnSave.Text = "Save"; btnDelete.Enabled = false; btnDelete.Visible = false;

        }


        void FillDataGridViewSTG()
        {
            SqlConnection STGsqlcon = new SqlConnection(strcon);
            if (STGsqlcon.State == ConnectionState.Closed)
                STGsqlcon.Open();
            SqlDataAdapter sqlDaSTG = new SqlDataAdapter("STGViewOrSearch", STGsqlcon);
            sqlDaSTG.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDaSTG.SelectCommand.Parameters.AddWithValue("@SEARCHNAME", txtSearch.Text.Trim());
            DataTable dtblSTG = new DataTable();
            sqlDaSTG.Fill(dtblSTG);
            STGdataGridView.DataSource = dtblSTG;
            STGdataGridView.Columns[0].Visible = false;
            STGsqlcon.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection STGsqlcon = new SqlConnection(strcon);
            if (MessageBox.Show("Proceed Delete ?", "Delete Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (STGsqlcon.State == ConnectionState.Closed)
                        STGsqlcon.Open();
                    SqlCommand sqlcmdSTG = new SqlCommand("STGDeletion", STGsqlcon);
                    sqlcmdSTG.CommandType = CommandType.StoredProcedure;
                    sqlcmdSTG.Parameters.AddWithValue("@BARCODE", comboBox3.Text);
                    sqlcmdSTG.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully");
                    Reset();
                    FillDataGridViewSTG();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error Message");
                }


            }
            else
                MessageBox.Show("Row Not Deleted", "Delete Row", MessageBoxButtons.OK, MessageBoxIcon.Information);
            disp_data();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection STGsqlcon = new SqlConnection(strcon);
            STGsqlcon.Open();
            sqlcmdSTG = new SqlCommand("SELECT * FROM FACTBL_STK WHERE BarCode = '" + comboBox3.Text + "'", STGsqlcon);

            object res = sqlcmdSTG.ExecuteScalar();
            STGsqlcon.Close();
            if (res != null)

            {


                STGsqlcon.Open();
                SqlCommand cmd = STGsqlcon.CreateCommand();
                cmd.CommandType = CommandType.Text;


                cmd.CommandText = "update FACTBL_STK set QTY ='" + comboBox6.Text + "',DES='" + comboBox4.Text + "',PRICE='" + comboBox5.Text + "',LOCATION='" + comboBox7.Text + "',STATUS='" + comboBox8.Text + "' where Barcode ='" + comboBox3.Text + "'";

                cmd.ExecuteNonQuery();
                STGsqlcon.Close();
                MessageBox.Show("Product Exist, Updated successfully !", "Updated !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //clear

                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                comboBox6.Text = "";
                cbPicture.Image = null;
                comboBox7.Text = "";
                comboBox8.Text = "";
                disp_data();


            }


            else if (string.IsNullOrEmpty(comboBox4.Text) || string.IsNullOrEmpty(comboBox5.Text) || string.IsNullOrEmpty(comboBox6.Text) || string.IsNullOrEmpty(comboBox7.Text) || string.IsNullOrEmpty(comboBox8.Text) || cbPicture.Image == null)
            {
                MessageBox.Show("In-complete Input!", "INFORMATION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                sqlcmdSTG = new SqlCommand("INSERT INTO FACTBL_STK Values (@Barcode,@Des, @Price, @Qty, @Picture, @Location, @Status)", STGsqlcon);

                SqlParameter[] param = new SqlParameter[6];

                MemoryStream ms = new MemoryStream();
                cbPicture.Image.Save(ms, cbPicture.Image.RawFormat);
                byte[] image = ms.ToArray();

                param[0] = new SqlParameter("@Barcode", SqlDbType.VarChar, 35);
                param[0].Value = comboBox3.Text;

                param[1] = new SqlParameter("@Des", SqlDbType.NVarChar);
                param[1].Value = comboBox4.Text;

                param[2] = new SqlParameter("@Price", SqlDbType.VarChar, 50);
                param[2].Value = comboBox5.Text;

                param[3] = new SqlParameter("@Qty", SqlDbType.Int);
                param[3].Value = Convert.ToInt32(comboBox6.Text);

                param[4] = new SqlParameter("@Picture", SqlDbType.Image);
                param[4].Value = image;

                param[5] = new SqlParameter("@Location", SqlDbType.NVarChar);
                param[5].Value = comboBox7.Text;

                param[6] = new SqlParameter("@Status", SqlDbType.NVarChar);
                param[6].Value = comboBox8.Text;


                STGsqlcon.Open();
                sqlcmdSTG.Parameters.AddRange(param);
                sqlcmdSTG.ExecuteNonQuery();
                STGsqlcon.Close();
                MessageBox.Show("Product added successfully !", "Added !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox5.Text = "";
                comboBox6.Text = "";
                cbPicture.Image = null;
                comboBox7.Text = "";
                comboBox8.Text = "";

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            disp_data();

            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            cbPicture.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox8.Text = "";
            cbPicture.Image = null;
            txtSearch.Text = "";
            comboBox1.Text = "";
            btnSave.Text = "Save";
            btnDelete.Visible = false;
            btnDelete.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void STGdataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (STGdataGridView.CurrentRow.Index != -1)
            {

                comboBox3.Text = STGdataGridView.CurrentRow.Cells[0].Value.ToString();
                comboBox4.Text = STGdataGridView.CurrentRow.Cells[1].Value.ToString();
                comboBox5.Text = STGdataGridView.CurrentRow.Cells[2].Value.ToString();
                comboBox6.Text = STGdataGridView.CurrentRow.Cells[3].Value.ToString();

                MemoryStream ms = new MemoryStream((byte[])STGdataGridView.CurrentRow.Cells[4].Value);
                cbPicture.Image = Image.FromStream(ms);

                comboBox7.Text = STGdataGridView.CurrentRow.Cells[5].Value.ToString();
                comboBox8.Text = STGdataGridView.CurrentRow.Cells[6].Value.ToString();




                btnSave.Text = "Update";
                btnDelete.Visible = true;
                btnDelete.Enabled = true;

            }
        }
        private void STGdataGridView_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            MessageBox.Show("Error happened " + anError.Context.ToString());

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[anError.RowIndex].ErrorText = "an error";
                view.Rows[anError.RowIndex].Cells[anError.ColumnIndex].ErrorText = "an error";

                anError.ThrowException = false;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SqlConnection STGsqlcon = new SqlConnection(strcon);
            //BY BARCODE
            if (comboBox1.Text == "BARCODE")
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * FROM FACTBL_STK WHERE BARCODE LIKE '%" + txtSearch.Text + "%'", STGsqlcon);
                DataTable data = new DataTable();
                sda.Fill(data);
                STGdataGridView.DataSource = data;
            }
            //BY DESCRIPTION
            if (comboBox1.Text == "DESCRIPTION")
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * FROM FACTBL_STK WHERE DES LIKE '%" + txtSearch.Text + "%'", STGsqlcon);
                DataTable data = new DataTable();
                sda.Fill(data);
                STGdataGridView.DataSource = data;
            }
        }

        private void STKQuery_Load(object sender, EventArgs e)
        {

            //this.tbL_PRDTableAdapter1.Fill(this.dB_BARCODEDataSet1.TBL_PRD);
            btnDelete.Visible = false;
            disp_data();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Picture |*.png; *.jpg; *.gif; *.bmp";
            if (op.ShowDialog() == DialogResult.OK)
                cbPicture.Image = Image.FromFile(op.FileName);
        }
    }
}
