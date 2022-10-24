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
    public partial class frmEXpenses : DevComponents.DotNetBar.Office2007Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        public frmEXpenses()
        {
            InitializeComponent();
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
            expenseid.Text = "EX-" + years + monthss + days + GetUniqueKey(5);
        }
        public void dataload()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(ExpenseID)[Expense ID], RTRIM(CashierID)[Cashier Name],RTRIM(Year)[Year], RTRIM(Months)[Months], RTRIM(Date)[Date],RTRIM(Expense)[Paid For],RTRIM(Cost)[Cost],RTRIM(TotalPaid)[Total Paid], RTRIM(Duepayment)[Due Payment],RTRIM(Description)[Description], RTRIM(Payee)[Names of Payee],RTRIM(Telephone)[Telephone No. ], RTRIM(Expenses.Email)[Email Address], RTRIM(Expenses.Address)[ Address], RTRIM(Paid)[Payment] from Expenses  order by Expenses.ID DESC", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Expenses");
                dataGridViewX1.DataSource = myDataSet.Tables["Expenses"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmEXpenses_Load(object sender, EventArgs e)
        {
            /* Left = Top = 0;
             this.Width = Screen.PrimaryScreen.WorkingArea.Width;
             this.Height = Screen.PrimaryScreen.WorkingArea.Height;*/
            dataload();
            auto();
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label1.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { buttonX3.Enabled = true; } else { buttonX3.Enabled = false; }
                    if (pricess == "Yes") { buttonX4.Enabled = true; } else { buttonX4.Enabled = false; }
                }
                if (label1.Text == "ADMIN")
                {
                    buttonX3.Enabled = true;
                    buttonX4.Enabled = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            expenseid.Text = "";
            year.Text = DateTime.Today.ToString();
            months.Text = DateTime.Today.ToString();
            expensedate.Text = DateTime.Today.ToString();
            description.Text = "";
            cost.Text = null;
            totalpaid.Text = null;
            duepayment.Text = null;
            names.Text = "";
            address.Text = "";
            tel.Text = null;
            email.Text = "";
            service.Text = "";
            Paid.Text = "";
            buttonX5.Enabled = true;
        }
        private void buttonX6_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void email_Validating(object sender, CancelEventArgs e)
        {
            if (email.Text == "" || email.Text == "N/A" || email.Text == "n/a")
            {
            }
            else
            {
                System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
                if (email.Text.Length > 0)
                {
                    if (!rEMail.IsMatch(email.Text))
                    {
                        MessageBox.Show("invalid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        email.SelectAll();
                        e.Cancel = true;
                    }
                }
            }
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
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonX5_Click(object sender, EventArgs e)
        {

            if (cost.Text == "")
            {
                MessageBox.Show("Please enter Cost", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cost.Focus();
                return;
            }
            if (totalpaid.Text == "")
            {
                MessageBox.Show("Please enter total paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                totalpaid.Focus();
                return;
            }
            if (year.Text == "")
            {
                MessageBox.Show("Please enter Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                year.Focus();
                return;
            }
            if (duepayment.Text == "")
            {
                MessageBox.Show("Please enter Duepayment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                duepayment.Focus();
                return;
            }
            if (Paid.Text == "")
            {
                MessageBox.Show("Please Select Paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Paid.Focus();
                return;
            }
            if (duepayment.Text != "0")
            {
                //MessageBox.Show("Pay this fee in not more than two installments for better accounts", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string paymentmode = null;
            if (radioButton1.Checked == true)
            {
                paymentmode = "Cash";
            }
            else if (radioButton3.Checked == true)
            {
                paymentmode = "Bank";

            }
            else if (radioButton2.Checked == true)
            {
                paymentmode = "Mobile Money";
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Duepayment from Expenses where ExpenseID='" + expenseid.Text + "'order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (expenseid.Text == "")
                    {
                        MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        expenseid.Focus();
                        return;
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update Expenses set Duepayment=@d9 where ExpenseID=@d1";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text.Trim();
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(cost.Text);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(tel.Text);
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into Expenses(ExpenseID,CashierID,Year,Months,Date,Expense,Cost,TotalPaid,Duepayment,Description,Payee,Telephone,Email,Address,Comment,Paid,ModeOfpayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,'" + paymentmode + "')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Comment"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Paid"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text;
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(duepayment.Text);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = tel.Text;
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.Parameters["@d15"].Value = "Pending Approval";
                    cmd.Parameters["@d16"].Value = Paid.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully saved", "Expense Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    auto();
                    if (expenseid.Text == "")
                    {
                        MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        expenseid.Focus();
                        return;
                    }

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into Expenses(ExpenseID,CashierID,Year,Months,Date,Expense,Cost,TotalPaid,Duepayment,Description,Payee,Telephone,Email,Address,Comment,Paid,ModeOfPayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,'" + paymentmode + "')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Comment"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Paid"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text;
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(cost.Text);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(duepayment.Text);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = tel.Text;
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.Parameters["@d15"].Value = "Pending Approval";
                    cmd.Parameters["@d16"].Value = Paid.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully saved", "Expense Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //printPreviewDialog1.Document = printDocument1;
                //printPreviewDialog1.ShowDialog();
                //this.Hide();
                /*Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptExpensesReceipt rpt = new rptExpensesReceipt(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                DataSet1 myDS = new DataSet1(); //The DataSet you created.
                Receipts frm = new Receipts();
                //frm.label1.Text = label7.Text;
                //frm.label2.Text = label12.Text;
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from Expenses";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Expenses");
                rpt.SetDataSource(myDS);
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Show();*/
                buttonX5.Enabled = false;
                dataload();
                dataGridViewX1.Refresh();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void managername_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (expenseid.Text == "")
            {
                MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                expenseid.Focus();
                return;
            }
            try
            {
                int RowsAffected = 0;

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete  from  Expenses where ExpenseID=@DELETE1 and Comment !='Approved'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                cmd.Parameters["@DELETE1"].Value = expenseid.Text;
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

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (expenseid.Text == "")
            {
                MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                expenseid.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ExpenseID from Expenses where ExpenseID=@find and Comment='Approved'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                cmd.Parameters["@find"].Value = expenseid.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("Unable to Update..Once Approved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reset();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update Expenses set Year=@d3,Months=@d4,Date=@d5,Expense=@d6,Cost=@d7,TotalPaid=@d8,Duepayment=@d9,Description=@d10,Payee=@d11,Telephone=@d12,Email=@d13,Address=@d14 where ExpenseID=@d1 and Comment!='Approved'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "CashierID"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                cmd.Parameters["@d2"].Value = label1.Text.Trim();
                cmd.Parameters["@d3"].Value = year.Text.Trim();
                cmd.Parameters["@d4"].Value = months.Text.Trim();
                cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                cmd.Parameters["@d6"].Value = service.Text.Trim();
                cmd.Parameters["@d7"].Value = Convert.ToInt32(cost.Text);
                cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                cmd.Parameters["@d9"].Value = Convert.ToInt32(duepayment.Text);
                cmd.Parameters["@d10"].Value = description.Text;
                cmd.Parameters["@d11"].Value = names.Text.Trim();
                cmd.Parameters["@d12"].Value = Convert.ToInt32(tel.Text);
                cmd.Parameters["@d13"].Value = email.Text;
                cmd.Parameters["@d14"].Value = address.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated", "Expense Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void totalpaid_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Duepayment from Expenses where ExpenseID='" + expenseid.Text + "'order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label3.Text = rdr["Duepayment"].ToString();
                    int val4 = 0;
                    int val6 = 0;
                    int.TryParse(label3.Text, out val4);
                    int.TryParse(totalpaid.Text, out val6);
                    int I = (val4 - val6);
                    duepayment.Text = I.ToString();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                else
                {
                    int val1 = 0;
                    int val3 = 0;
                    int.TryParse(cost.Text, out val1);
                    int.TryParse(totalpaid.Text, out val3);
                    int I = (val1 - val3);
                    duepayment.Text = I.ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridViewX1.SelectedRows[0];
                expenseid.Text = dr.Cells[0].Value.ToString();
                expensedate.Text = dr.Cells[4].Value.ToString();
                service.Text = dr.Cells[5].Value.ToString();
                cost.Text = dr.Cells[6].Value.ToString();
                totalpaid.Text = dr.Cells[7].Value.ToString();
                duepayment.Text = dr.Cells[8].Value.ToString();
                description.Text = dr.Cells[9].Value.ToString();
                names.Text = dr.Cells[10].Value.ToString();
                tel.Text = dr.Cells[11].Value.ToString();
                email.Text = dr.Cells[12].Value.ToString();
                address.Text = dr.Cells[13].Value.ToString();
                buttonX4.Enabled = true;
                buttonX3.Enabled = true;
                label1.Text = label1.Text;
                label2.Text = label2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cashierid_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmEXpenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* this.Hide();
             frmMainMenu frm = new frmMainMenu();
             frm.User.Text = label1.Text;
             frm.UserType.Text = label2.Text;
             frm.outlet.Text = outlet.Text;
             frm.Show();*/
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            frmExpensesRecord frm = new frmExpensesRecord();
            frm.label1.Text = label1.Text;
            frm.label2.Text = label2.Text;
            frm.ShowDialog();
        }

        private void managerid_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (cost.Text == "")
            {
                MessageBox.Show("Please enter Cost", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cost.Focus();
                return;
            }
            if (totalpaid.Text == "")
            {
                MessageBox.Show("Please enter total paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                totalpaid.Focus();
                return;
            }
            if (year.Text == "")
            {
                MessageBox.Show("Please enter Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                year.Focus();
                return;
            }
            if (duepayment.Text == "")
            {
                MessageBox.Show("Please enter Duepayment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                duepayment.Focus();
                return;
            }
            if (Paid.Text == "")
            {
                MessageBox.Show("Please Select Paid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Paid.Focus();
                return;
            }
            if (duepayment.Text != "0")
            {
                //MessageBox.Show("Pay this fee in not more than two installments for better accounts", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string paymentmode = null;
            if (radioButton1.Checked == true)
            {
                paymentmode = "Cash";
            }
            else if (radioButton3.Checked == true)
            {
                paymentmode = "Bank";

            }
            else if (radioButton2.Checked == true)
            {
                paymentmode = "Mobile Money";
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Duepayment from Expenses where ExpenseID='" + expenseid.Text + "'order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    if (expenseid.Text == "")
                    {
                        MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        expenseid.Focus();
                        return;
                    }
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update Expenses set Duepayment=@d9 where ExpenseID=@d1";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text.Trim();
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(cost.Text);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(tel.Text);
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into Expenses(ExpenseID,CashierID,Year,Months,Date,Expense,Cost,TotalPaid,Duepayment,Description,Payee,Telephone,Email,Address,Comment,Paid,ModeOfpayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,'" + paymentmode + "')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Comment"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Paid"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text;
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(duepayment.Text);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = tel.Text;
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.Parameters["@d15"].Value = "Pending Approval";
                    cmd.Parameters["@d16"].Value = Paid.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully saved", "Expense Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    auto();
                    if (expenseid.Text == "")
                    {
                        MessageBox.Show("Please enter Member ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        expenseid.Focus();
                        return;
                    }

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into Expenses(ExpenseID,CashierID,Year,Months,Date,Expense,Cost,TotalPaid,Duepayment,Description,Payee,Telephone,Email,Address,Comment,Paid,ModeOfPayment) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,'" + paymentmode + "')";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ExpenseID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "CashierID"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Months"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 20, "Date"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 40, "Expense"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 15, "Cost"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 15, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 15, "Duepayment"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 200, "Description"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 60, "Payee"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Telephone"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 50, "Email"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 60, "Address"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Comment"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Paid"));
                    cmd.Parameters["@d1"].Value = expenseid.Text.Trim();
                    cmd.Parameters["@d2"].Value = label1.Text;
                    cmd.Parameters["@d3"].Value = year.Text.Trim();
                    cmd.Parameters["@d4"].Value = months.Text.Trim();
                    cmd.Parameters["@d5"].Value = expensedate.Text.Trim();
                    cmd.Parameters["@d6"].Value = service.Text.Trim();
                    cmd.Parameters["@d7"].Value = Convert.ToInt32(cost.Text);
                    cmd.Parameters["@d8"].Value = Convert.ToInt32(totalpaid.Text);
                    cmd.Parameters["@d9"].Value = Convert.ToInt32(duepayment.Text);
                    cmd.Parameters["@d10"].Value = description.Text;
                    cmd.Parameters["@d11"].Value = names.Text.Trim();
                    cmd.Parameters["@d12"].Value = tel.Text;
                    cmd.Parameters["@d13"].Value = email.Text;
                    cmd.Parameters["@d14"].Value = address.Text;
                    cmd.Parameters["@d15"].Value = "Pending Approval";
                    cmd.Parameters["@d16"].Value = Paid.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully saved", "Expense Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                try
                {
                    company();
                    //this.Hide();

                    frmReceiptexpenses rpt = new frmReceiptexpenses(); //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet(); //The DataSet you created.
                    FrmExpensesSlip frm = new FrmExpensesSlip();
                    frm.label1.Text = label1.Text;
                    frm.label2.Text = label2.Text;
                    myConnection = new SqlConnection(cs.DBConn);
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select * from Expenses";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Expenses");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("paymentid", expenseid.Text);
                    rpt.SetParameterValue("paidto", names.Text);
                    rpt.SetParameterValue("paid", totalpaid.Text);
                    rpt.SetParameterValue("payable", cost.Text);
                    rpt.SetParameterValue("due", duepayment.Text);
                    rpt.SetParameterValue("receivedby", label1.Text);
                    rpt.SetParameterValue("comanyname", companyname);
                    rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    rpt.SetParameterValue("picpath", "logo.jpg");
                    frm.crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                    frm.ShowDialog();
                    //BarPrinter = Properties.Settings.Default.frontendprinter;
                    //frm.crystalReportViewer1.PrintReport();
                    //rpt.PrintOptions.PrinterName = BarPrinter;
                    //rpt.PrintToPrinter(1, true, 1, 1);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                buttonX5.Enabled = false;
                dataload();
                dataGridViewX1.Refresh();
                con.Close();
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
                company();
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
                string text = companyname;
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companyslogan;
                e.Graphics.DrawString(text, drawFontArial10italic, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10italic).Height;




                text = companyaddress;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companycontact;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Payment ID: " + expenseid.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Date: " + DateTime.Now.ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;



                text = "Paid For:  Expense";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Paid To: " + names.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Total Ammount: UGX." + cost.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Total Paid: UGX." + totalpaid.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Due Payment: UGX." + duepayment.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Issued By: " + label1.Text;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "THANK YOU, COME AGAIN";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Powered by: " + "www.essentialsystems.atwebpages.com";
                e.Graphics.DrawString(text, drawFontArial6Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial6Regular).Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
