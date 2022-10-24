using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
namespace College_Management_System
{
    public partial class frmBusFeePaymentHire : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        SqlDataAdapter adp;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        public frmBusFeePaymentHire()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            FeePaymentID.Text = "";
            StudentName.Text = "";
            Course.Text = "";
            Branch.Text = "";
            txtBusCharges.Text = "";
            txtSourceLocation.Text = "";
            ModeOfPayment.Text = "";
            PaymentDate.Text = DateTime.Today.ToString();
            PaymentModeDetails.Text = "";
            Fine.Text = "";
            TotalPaid.Text = "";
            DueFees.Text = "";
            btnSave.Enabled = true;
            Delete.Enabled = false;
            Update_record.Enabled = false;
            Print.Enabled = false;
            Year.Text = "";
            term.Text = "";
        }
        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModeOfPayment.Text == "")
                {
                    MessageBox.Show("Please select mode of payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ModeOfPayment.Focus();
                    return;
                }
                if (TotalPaid.Text == "")
                {
                    MessageBox.Show("Please enter total paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TotalPaid.Focus();
                    return;
                }
                if (StudentName.Text == "")
                {
                    MessageBox.Show("Please enter hirer name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StudentName.Focus();
                    return;
                }
                if (Course.Text == "")
                {
                    MessageBox.Show("Please enter Hirer Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (txtSourceLocation.Text == "")
                {
                    MessageBox.Show("Please enter hire duration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSourceLocation.Focus();
                    return;
                }
                if (txtBusCharges.Text == "")
                {
                    MessageBox.Show("Please enter charges", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBusCharges.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please enter Hirer Destination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                if (Year.Text == "")
                {
                    MessageBox.Show("Please enter year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Year.Focus();
                    return;
                }
                if (term.Text == "")
                {
                    MessageBox.Show("Please enter Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    term.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select HireTotalPaid,HirerName,Term,Year,HireDueFees from BusHireFeePayment where  HirerName= '" + StudentName.Text + "' and Destination= '" + Branch.Text + "' and Term='" + term.Text + "' and Year='" + Year.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update BusHireFeePayment set HireDueFees=@d9 where HirerName=@d10 and Destination=@d12 and Term=@d10 and Year=@d12 and Class=@d11 ";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = 0;
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteReader();
                    con.Close();
                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into BusHireFeePayment(HireFeePaymentID,HireBusCharges,HireDateOfPayment,HireModeOfPayment,HirePaymentModeDetails,HireTotalPaid,HireFine,HireDueFees,HirerName,HirerAddress,Destination,Duration,Term,Year,HireBusCharges1,Units) VALUES (@d1,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = 0;
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    con.Close();
                }
                else
                {
                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into BusHireFeePayment(HireFeePaymentID,HireBusCharges,HireDateOfPayment,HireModeOfPayment,HirePaymentModeDetails,HireTotalPaid,HireFine,HireDueFees,HirerName,HirerAddress,Destination,Duration,Term,Year,HireBusCharges1,Units) VALUES (@d1,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = Convert.ToInt32(txtBusCharges.Text);
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //printPreviewDialog1.Document = printDocument1;
                    //printPreviewDialog1.ShowDialog();
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        string monthss = DateTime.Today.Month.ToString();
        string days = DateTime.Today.Day.ToString();
        string yearss = DateTime.Today.Year.ToString();
        private void auto()
        {
            string years = yearss.Substring(2, 2);
            FeePaymentID.Text = "HF-" + years + monthss + days + GetUniqueKey(5);
        }

        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update BusHireFeePayment set HireBusCharges=@d3,HireDateOfPayment=@d4,HireModeOfPayment=@d5,HirePaymentModeDetails=@d6,HireTotalPaid=@d7,HireFine=@d8,HireDueFees=@d9,HirerName=@d10,HirerAddress=@d11,Duration=@d13,Destination=@d12,Term=@d14,Year=@d15,Units=@d17 where HireFeePaymentID=@d1";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                cmd.Parameters["@d3"].Value = Convert.ToInt32(txtBusCharges.Text);
                cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                cmd.Parameters["@d10"].Value = (StudentName.Text);
                cmd.Parameters["@d11"].Value = (Course.Text);
                cmd.Parameters["@d12"].Value = (Branch.Text);
                cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                cmd.Parameters["@d14"].Value = (term.Text);
                cmd.Parameters["@d15"].Value = (Year.Text);
                cmd.Parameters["@d17"].Value = (comboBox1.Text);
                if (Fine.Text == "")
                {
                    cmd.Parameters["@d8"].Value = 0;
                }
                else
                {
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                }
                cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
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
                string cq = "delete from BusHireFeePayment where HireFeePaymentID=@DELETE1;";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                cmd.Parameters["@DELETE1"].Value = FeePaymentID.Text;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Delete.Enabled = false;
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


        private void TotalPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select HireTotalPaid,HirerName,Term,Year,HireDueFees from BusHireFeePayment where  HirerName= '" + StudentName.Text + "' and Destination= '" + Branch.Text + "' and Term='" + term.Text + "' and Year='" + Year.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label7.Text = rdr["HireDueFees"].ToString();
                    int val4 = 0;
                    int val5 = 0;
                    int val6 = 0;
                    int.TryParse(label7.Text, out val4);
                    int.TryParse(Fine.Text, out val5);
                    int.TryParse(TotalPaid.Text, out val6);
                    int I = ((val4 + val5) - val6);
                    DueFees.Text = I.ToString();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                else
                {
                    int val1 = 0;
                    int val2 = 0;
                    int val3 = 0;
                    int.TryParse(txtBusCharges.Text, out val1);
                    int.TryParse(Fine.Text, out val2);
                    int.TryParse(TotalPaid.Text, out val3);
                    int I = ((val1 + val2) - val3);
                    DueFees.Text = I.ToString();
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

        private void TotalPaid_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            int.TryParse(txtBusCharges.Text, out val1);
            int.TryParse(Fine.Text, out val2);
            int.TryParse(TotalPaid.Text, out val3);
            if (val3 > val1 + val2)
            {
                MessageBox.Show("Total Paid can not be more than Transportation Fees + Fine", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TotalPaid.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBusFeePaymentRecordHire frm = new frmBusFeePaymentRecordHire();
            frm.label1.Text = label3.Text;
            frm.label2.Text = label4.Text;
            frm.ShowDialog();
        }
        public void company()
        {
            try
            {
                SqlDataReader rdr = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct6 = "select * from CompanyNames";
                cmd = new SqlCommand(ct6);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    companyname = rdr.GetString(1).Trim();
                    companyaddress = rdr.GetString(5).Trim();
                    companyslogan = rdr.GetString(2).Trim();
                    companycontact = rdr.GetString(4).Trim();
                    companyemail = rdr.GetString(3).Trim();
                }
                else
                {
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
        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModeOfPayment.Text == "")
                {
                    MessageBox.Show("Please select mode of payment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ModeOfPayment.Focus();
                    return;
                }
                if (TotalPaid.Text == "")
                {
                    MessageBox.Show("Please enter total paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TotalPaid.Focus();
                    return;
                }
                if (StudentName.Text == "")
                {
                    MessageBox.Show("Please enter hirer name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StudentName.Focus();
                    return;
                }
                if (Course.Text == "")
                {
                    MessageBox.Show("Please enter Hirer Address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (txtSourceLocation.Text == "")
                {
                    MessageBox.Show("Please enter hire duration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSourceLocation.Focus();
                    return;
                }
                if (txtBusCharges.Text == "")
                {
                    MessageBox.Show("Please enter charges", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBusCharges.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please enter Hirer Destination", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                if (Year.Text == "")
                {
                    MessageBox.Show("Please enter year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Year.Focus();
                    return;
                }
                if (term.Text == "")
                {
                    MessageBox.Show("Please enter Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    term.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select HireTotalPaid,HirerName,Term,Year,HireDueFees from BusHireFeePayment where  HirerName= '" + StudentName.Text + "' and Destination= '" + Branch.Text + "' and Term='" + term.Text + "' and Year='" + Year.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update BusHireFeePayment set HireDueFees=@d9 where HirerName=@d10 and Destination=@d12 and Term=@d10 and Year=@d12 and Class=@d11 ";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = 0;
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteReader();
                    con.Close();
                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into BusHireFeePayment(HireFeePaymentID,HireBusCharges,HireDateOfPayment,HireModeOfPayment,HirePaymentModeDetails,HireTotalPaid,HireFine,HireDueFees,HirerName,HirerAddress,Destination,Duration,Term,Year,HireBusCharges1,Units) VALUES (@d1,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = 0;
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    con.Close();
                }
                else
                {
                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into BusHireFeePayment(HireFeePaymentID,HireBusCharges,HireDateOfPayment,HireModeOfPayment,HirePaymentModeDetails,HireTotalPaid,HireFine,HireDueFees,HirerName,HirerAddress,Destination,Duration,Term,Year,HireBusCharges1,Units) VALUES (@d1,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "HireFeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "HireBusCharges"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "HireDateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "HireModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.VarChar, 200, "HirePaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "HireTotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "HireFine"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "HireDueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 50, "HirerName"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.VarChar, 100, "HireAddress"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 50, "Destination"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Duration"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "HireBusCharges1"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Units"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d3"].Value = Convert.ToInt32(txtBusCharges.Text);
                    cmd.Parameters["@d4"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d5"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d6"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(TotalPaid.Text);
                    cmd.Parameters["@d10"].Value = (StudentName.Text);
                    cmd.Parameters["@d11"].Value = (Course.Text);
                    cmd.Parameters["@d12"].Value = (Branch.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(txtSourceLocation.Text);
                    cmd.Parameters["@d14"].Value = (term.Text);
                    cmd.Parameters["@d15"].Value = (Year.Text);
                    cmd.Parameters["@d17"].Value = (comboBox1.Text);
                    cmd.Parameters["@d16"].Value = Convert.ToInt32(txtBusCharges.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d8"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d8"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //printPreviewDialog1.Document = printDocument1;
                    //printPreviewDialog1.ShowDialog();
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                company();
                rptReceiptBusHirepayment rpt = new rptReceiptBusHirepayment(); //The report you created.
                //MessageBox.Show("two", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                DataSet myDS = new DataSet(); //The DataSet you created.
                frmBusFeePaymentReceiptHire frm = new frmBusFeePaymentReceiptHire();
                /*frm.label1.Text = label1.Text;
                frm.label2.Text = label2.Text;*/
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusFeePayment";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusFeePayment");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("paymentid", FeePaymentID.Text);
                rpt.SetParameterValue("paidto", StudentName.Text);
                rpt.SetParameterValue("paid", TotalPaid.Text);
                rpt.SetParameterValue("payable", txtBusCharges.Text);
                rpt.SetParameterValue("due", DueFees.Text);
                rpt.SetParameterValue("receivedby", label4.Text);
                rpt.SetParameterValue("destination", Branch.Text);
                rpt.SetParameterValue("comanyname", companyname);
                rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                rpt.SetParameterValue("picpath", "logo.jpg");
                frm.crystalReportViewer1.ReportSource = rpt;
                myConnection.Close();
                frm.ShowDialog();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void frmBusFeePayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label3.Text;
            frm.User.Text = label4.Text;
            frm.Show();*/
        }
        private void txtSourceLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBusCharges_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSourceLocation_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT Charges FROM BusHire WHERE Units = '" + comboBox1.Text + "'";
                cmd = new SqlCommand(inquery3, con);
                label5.Text = cmd.ExecuteScalar().ToString();
                con.Close();
                int val1 = 0;
                int val2 = 0;
                int.TryParse(txtSourceLocation.Text, out val1);
                int.TryParse(label5.Text, out val2);
                int I = ((val1 * val2));
                txtBusCharges.Text = I.ToString();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("the number of days you have entered do no match the days allowed");
            }
        }

        private void frmBusFeePaymentHire_Load(object sender, EventArgs e)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                float x = 10;
                float y = 5;

                float width = 260.0F; // max width I found through trial and error
                float height = 0F;

                Font drawFontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
                Font drawFontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
                Font drawFontArial10italic = new Font("Arial", 10, FontStyle.Italic);
                Font drawFontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
                Font drawFontArial6Regular = new Font("Arial", 6, FontStyle.Regular);
                SolidBrush drawBrush = new SolidBrush(Color.Black);

                // Set format of string.
                StringFormat drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                StringFormat drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                StringFormat drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;

                // Draw string to screen.
                string text = "KIGUMBA TOWN COMMUNITY SECONDARY SCH";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Poised to Excel ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "P.o Box 85, Kigumba, Kiryandongo";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "TEL:0782587448/0702042617";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Payment ID: " + FeePaymentID.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Paid By: " +StudentName.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Date: " + DateTime.Now.ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;



                text = "Paid For:  Vehicle Hire";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Paid To: " + label4.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Total Ammount: UGX." + string.Format("{0:n0}", Convert.ToInt32(txtBusCharges.Text));
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Total Paid: UGX." + string.Format("{0:n0}", Convert.ToInt32(TotalPaid.Text));
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Due Payment: UGX." +string.Format("{0:n0}", Convert.ToInt32( DueFees.Text));
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "*****THANK YOU*****";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Powered by: +256 787045644";
                e.Graphics.DrawString(text, drawFontArial6Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial6Regular).Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBusFeePaymentHire_Load_1(object sender, EventArgs e)
        {
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label4.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { Delete.Enabled = true; } else { Delete.Enabled = false; }
                    if (pricess == "Yes") { Update_record.Enabled = true; } else { Update_record.Enabled = false; }
                }
                if (label4.Text == "ADMIN")
                {
                    Delete.Enabled = true;
                    Update_record.Enabled = true;
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
    }
}