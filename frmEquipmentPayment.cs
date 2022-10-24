using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmEquipmentPayment : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public String str;
        SqlDataReader rdr = null;


        public frmEquipmentPayment()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtTransactionID.Text = "";
            txtdes.Text = "";
            txtamt.Text = "";
            dtp.Text = DateTime.Today.ToString();
            rbcredit.Checked = false;
            rbdebit.Checked = false;
            btnSave.Enabled = true;
            Delete.Enabled = false;
            Update_record.Enabled = false;
            Year.Text = "";
            term.Text = "";
            paidfor.Text = "";

        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void AutocompleteTerm()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Semester) FROM Batch", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                term.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    term.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Autocomleteids()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT (purchaseid) FROM EquipmentPurchase order by ID DESC", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                paidfor.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    paidfor.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmOtherTransaction_Load(object sender, EventArgs e)
        {
            rbcredit.Checked = false;
            rbdebit.Checked = false;
            AutocompleteTerm();
            Autocomleteids();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtamt.Text == "")
            {
                MessageBox.Show("Please enter amount ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtamt.Focus();
                return;
            }
            if (Year.Text == "")
            {
                MessageBox.Show("Please enter Year ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Year.Focus();
                return;
            }
            if (term.Text == "")
            {
                MessageBox.Show("Please enter term ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                term.Focus();
                return;
            }

            try
            {
                if (rbdebit.Checked)
                {
                    str = rbdebit.Text;
                }
                if (rbcredit.Checked)
                {
                    str = rbcredit.Text;
                }
                if (rbcredit.Checked || rbdebit.Checked)
                {

                }
                else
                {
                    MessageBox.Show("Select transation type option please", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbcredit.Checked = true;
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Amount from EquipmentPayment  where  Reason= '" + paidfor.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb2 = "update EquipmentPayment  set DuePayment=@d8 where Reason=@d7";
                    cmd = new SqlCommand(cb2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "TransactionType"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Amount"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 100, "Reason"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "DuePayment"));
                    cmd.Parameters["@d1"].Value = str;
                    cmd.Parameters["@d2"].Value = dtp.Text;
                    cmd.Parameters["@d3"].Value = txtamt.Text;
                    cmd.Parameters["@d4"].Value = txtdes.Text;
                    cmd.Parameters["@d5"].Value = term.Text;
                    cmd.Parameters["@d6"].Value = Year.Text;
                    cmd.Parameters["@d7"].Value = paidfor.Text;
                    cmd.Parameters["@d8"].Value = 0;
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Update_record.Enabled = false;
                    con.Close();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into EquipmentPayment (TransactionType,Date,Amount,Description,Term,Year,Reason,DuePayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "TransactionType"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Amount"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 100, "Reason"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "DuePayment"));
                    cmd.Parameters["@d1"].Value = str;
                    cmd.Parameters["@d2"].Value = dtp.Text;
                    cmd.Parameters["@d3"].Value = txtamt.Text;
                    cmd.Parameters["@d4"].Value = txtdes.Text;
                    cmd.Parameters["@d5"].Value = term.Text;
                    cmd.Parameters["@d6"].Value = Year.Text;
                    cmd.Parameters["@d7"].Value = paidfor.Text;
                    cmd.Parameters["@d8"].Value = duepayments.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    con.Close();
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into EquipmentPayment (TransactionType,Date,Amount,Description,Term,Year,Reason,DuePayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "TransactionType"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Amount"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 100, "Reason"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "DuePayment"));
                    cmd.Parameters["@d1"].Value = str;
                    cmd.Parameters["@d2"].Value = dtp.Text;
                    cmd.Parameters["@d3"].Value = txtamt.Text;
                    cmd.Parameters["@d4"].Value = txtdes.Text;
                    cmd.Parameters["@d5"].Value = term.Text;
                    cmd.Parameters["@d6"].Value = Year.Text;
                    cmd.Parameters["@d7"].Value = paidfor.Text;
                    cmd.Parameters["@d8"].Value = duepayments.Text;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from EquipmentPayment  where TransactionID=@DELETE1;";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.Int, 10, "TransactionID"));
                cmd.Parameters["@DELETE1"].Value = Convert.ToInt32(txtTransactionID.Text);
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbdebit.Checked)
                {
                    str = rbdebit.Text;
                }
                if (rbcredit.Checked)
                {
                    str = rbcredit.Text;
                }
                if (rbcredit.Checked || rbdebit.Checked)
                {

                }
                else
                {
                    MessageBox.Show("Select transation type option please", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbcredit.Checked = true;
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update EquipmentPayment set TransactionType=@d1,Date=@d2,Amount=@d3,Description=@d4,Term=@d5,Year=@d6 where TransactionType=@d1  and Date=@d2 and Year=@d6 and Term=@d5";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "TransactionType"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Date"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Amount"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 200, "Description"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 10, "Term"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 10, "Year"));
                cmd.Parameters["@d1"].Value = str;
                cmd.Parameters["@d2"].Value = dtp.Text;
                cmd.Parameters["@d3"].Value = txtamt.Text;
                cmd.Parameters["@d4"].Value = txtdes.Text;
                cmd.Parameters["@d5"].Value = term.Text;
                cmd.Parameters["@d6"].Value = Year.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Update_record.Enabled = false;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmequipmentpaymentrecord frm = new frmequipmentpaymentrecord();
            frm.label1.Text = label4.Text;
            frm.Show();
        }

        private void txtamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void paidfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalCost FROM EquipmentPurchase WHERE PurchaseID = '" + paidfor.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label7.Text = rdr.GetInt32(0).ToString();
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select DuePayment from EquipmentPayment  where  Reason= '" + paidfor.Text + "' order by TransactionID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label8.Text = rdr["DuePayment"].ToString();
                    int val4 = 0;
                    //int val5 = 0;
                    int val6 = 0;
                    int.TryParse(label8.Text, out val4);
                    int.TryParse(txtamt.Text, out val6);
                    int I = ((val4) - val6);
                    duepayments.Text = I.ToString();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                else
                {
                    int val1 = 0;
                    //int val2 = 0;
                    int val3 = 0;
                    int.TryParse(label7.Text, out val1);
                    int.TryParse(txtamt.Text, out val3);
                    int I = ((val1) - val3);
                    duepayments.Text = I.ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}