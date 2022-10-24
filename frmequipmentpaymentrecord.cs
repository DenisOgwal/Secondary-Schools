using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmequipmentpaymentrecord : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmequipmentpaymentrecord()
        {
            InitializeComponent();
        }

        private void frmTransactionRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmEquipmentPayment frm = new frmEquipmentPayment();
            frm.Show();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmEquipmentPayment frm = new frmEquipmentPayment();
            // or simply use column name instead of index
            //dr.Cells["id"].Value.ToString();

            frm.txtTransactionID.Text = dr.Cells[0].Value.ToString();
            frm.dtp.Text = dr.Cells[2].Value.ToString();
            if (dr.Cells[1].Value.ToString() == "Debit")
            {
                frm.rbdebit.Checked = true;
            }
            else
            {
                frm.rbcredit.Checked = true;
            }
            frm.txtamt.Text = dr.Cells[3].Value.ToString();
            frm.txtdes.Text = dr.Cells[4].Value.ToString();
            frm.paidfor.Text = dr.Cells[5].Value.ToString();
            frm.Year.Text = dr.Cells[6].Value.ToString();
            frm.term.Text = dr.Cells[7].Value.ToString();
            frm.Delete.Enabled = true;
            frm.Update_record.Enabled = true;
            frm.btnSave.Enabled = false;
            frm.label4.Text = label1.Text;
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(TransactionID)[Transaction ID],RTRIM(TransactionType)[Transaction Type],RTRIM(Date)[Transaction Date],RTRIM(Amount)[Amount],RTRIM(Description)[Description] ,RTRIM(Reason)[Purchase ID],RTRIM(Year)[Year],RTRIM(Term)[Term]FROM EquipmentPayment where date between @date1 and @date2  order by TransactionID Desc", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " Date").Value = DateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " Date").Value = DateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "EquipmentPayment");
                dataGridView1.DataSource = myDataSet.Tables["EquipmentPayment"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateFrom.Text = System.DateTime.Today.ToString();
            DateTo.Text = System.DateTime.Today.ToString();
            dataGridView1.DataSource = null;
        }

        private void frmTransactionRecord1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }
    }

}

