using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
namespace College_Management_System
{
    public partial class frmFeesPayment : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        ConnectionString cs = new ConnectionString();
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        SqlDataAdapter adp;
        DataTable dt = new DataTable();
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        public frmFeesPayment()
        {
            InitializeComponent();
        }

        private void FeesPayment_Load(object sender, EventArgs e)
        {
            //label21.Width = this.Width;
            //AutocompleScholarNo();
            dataGridView1.DataSource = GetData();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(session) from Student ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Year.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label24.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { Delete.Enabled = true; } else { Delete.Enabled = false; }
                    if (pricess == "Yes") { Update_record.Enabled = true; } else { Update_record.Enabled = false; }
                }
                if (label24.Text == "ADMIN")
                {
                    Delete.Enabled = true;
                    Update_record.Enabled = true;
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AutocompleFeeID()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct (FeeID) from FeesDetails where Year='" + Year.Text + "' and Course='" + Course.Text + "' ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    FeeID.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from FeePayment where FeePaymentID=@DELETE1;";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 20, "FeepaymentID"));
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

        private void AutocompleScholarNo()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(ScholarNo) from FeePayment ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ScholarNo.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FeeID_SelectedIndexChanged(object sender, EventArgs e)
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
                Term.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Term.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (Year.Text == "")
                {
                    MessageBox.Show("Please Enter Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Year.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM feesdetails WHERE FeeID = '" + FeeID.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    FDCourse.Text = rdr.GetString(2).Trim();
                    FDBranch.Text = rdr.GetString(3).Trim();
                    Term.Text = rdr.GetString(4).Trim();
                    if (Term.Text == "1st")
                    {
                        label11.Visible = true;
                        CautionMoney.Visible = true;
                    }
                    else
                    {
                        label11.Visible = false;
                        CautionMoney.Visible = false;
                    }
                    TutionFees.Text = rdr.GetString(5).ToString();
                    LibraryFees.Text = rdr.GetString(6).ToString();
                    UDFees.Text = rdr.GetString(7).ToString();
                    USFees.Text = rdr.GetString(8).ToString();
                    CautionMoney.Text = rdr.GetString(9).ToString();
                    OtherFees.Text = rdr.GetString(10).ToString();
                    TotalFees.Text = rdr.GetString(11).ToString();
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

        private void ScholarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Student_Name,Course,Branch FROM student WHERE ScholarNo = '" + ScholarNo.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    StudentName.Text = rdr.GetString(0).Trim();
                    Course.Text = rdr.GetString(1).Trim();
                    Branch.Text = rdr.GetString(2).Trim();
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

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            FeePaymentID.Text = "";
            FeeID.Text = "";
            Course.Text = "";
            Branch.Text = "";
            Term.Text = "";
            TutionFees.Text = "";
            LibraryFees.Text = "";
            UDFees.Text = "";
            USFees.Text = "";
            CautionMoney.Text = "";
            OtherFees.Text = "";
            TotalFees.Text = "";
            FDBranch.Text = "";
            FDCourse.Text = "";
            ScholarNo.Text = "";
            StudentName.Text = "";
            ModeOfPayment.Text = "";
            PaymentModeDetails.Text = "";
            TotalPaid.Text = "";
            Fine.Text = "";
            Year.Text = "";
            DueFees.Text = "";
            PaymentDate.Text = DateTime.Today.ToString();
            Delete.Enabled = false;
            Update_record.Enabled = false;
            btnSave.Enabled = true;
            Print.Enabled = false;
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
            FeePaymentID.Text = "F-" + years + monthss + days + GetUniqueKey(5);
        }
        string numberphone = null;
        string messages = null;
        public void sendmessage()
        {

            try
            {
                 using (var client2 = new WebClient())
                 using (client2.OpenRead("http://client3.google.com/generate_204"))
                 {
                     con = new SqlConnection(cs.DBConn);
                     con.Open();
                     cmd = con.CreateCommand();
                     cmd.CommandText = "SELECT distinct RTRIM(Contact_No) FROM Student where ScholarNo='" + ScholarNo.Text + "'";
                     rdr = cmd.ExecuteReader();
                     if (rdr.Read())
                     {
                         numberphone = rdr.GetString(0).Trim();
                     }
                     if ((rdr != null))
                     {
                         rdr.Close();
                     }
                     if (con.State == ConnectionState.Open)
                     {
                         con.Close();
                     }
                     string usernamess = Properties.Settings.Default.smsusername;
                     string passwordss = Properties.Settings.Default.smspassword;
                     WebClient client = new WebClient();
                     string numbers = "+256" + numberphone;
                     messages = TotalPaid.Text + " Has been paid for clearing School Fees for " + StudentName.Text + " and a balance of " + DueFees.Text + " is left for this " + Term.Text + " Term of year" + Year.Text;
                     string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messages + "&numbers=" + numbers;
                     client.OpenRead(baseURL);
                     MessageBox.Show("Successfully sent message");
                 }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not send message because your computer is not connected to internet");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeePaymentID.Text == "")
                {
                    MessageBox.Show("Please Enter Fees Payment ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FeePaymentID.Focus();
                    return;
                }
                if (ScholarNo.Text == "")
                {
                    MessageBox.Show("Please select Scholar No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ScholarNo.Focus();
                    return;
                }
                if (FeeID.Text == "")
                {
                    MessageBox.Show("Please select Fee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FeeID.Focus();
                    return;
                }
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select FeePaymentID,Year,ScholarNo,FDCourse,FDBranch,Semester,DueFees from FeePayment where  FDCourse= '" + FDCourse.Text + "' and ScholarNo= '" + ScholarNo.Text + "' and Semester='" + Term.Text + "' and Year='" + Year.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update feepayment set DueFees=@d22 where ScholarNo=@d2 and Semester=@d9 and Year=@d23 and FDCourse=@d7 ";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(TutionFees.Text);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(LibraryFees.Text);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(UDFees.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(USFees.Text);
                    cmd.Parameters["@d23"].Value = Convert.ToInt32(Year.Text);
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(TotalFees.Text);
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(CautionMoney.Text);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(OtherFees.Text);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(0);
                    cmd.ExecuteReader();
                    con.Close();


                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into feepayment(FeePaymentID,ScholarNo,FeeID,FDCourse,FDBranch,Semester,TutionFees,LibraryFees,UniversityDevelopmentFees,UniversityStudentWelfareFees,CautionMoney,OtherFees,TotalFees,DateOfPayment,ModeOfPayment,PaymentModeDetails,TotalPaid,Fine,DueFees,Year,TotalFees1,Student_name) VALUES (@d1,@d2,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d23"].Value = Year.Text;
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d25"].Value = StudentName.Text;
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(0);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    int totalfeesbalance = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select TotalDue from FeesBalance where ScholarNo=@find";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters["@find"].Value = ScholarNo.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        totalfeesbalance = Convert.ToInt32(rdr["TotalDue"].ToString()) - Convert.ToInt32(TotalPaid.Text);
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb1 = "UPDATE FeesBalance SET Student_name=@d25,FDCourse=@d7,FDBranch=@d8,Semester=@d9,TutionFees=@d10,TotalFees=@d16,Year=@d23,DateOfPayment=@d17,DueFees=@d22,TotalDue=@d24 where ScholarNo=@d2";
                        cmd = new SqlCommand(cb1);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                        cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                        cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                        cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "Year"));
                        cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalDue"));
                        cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                        cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                        cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                        cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                        cmd.Parameters["@d8"].Value = Branch.Text.Trim();
                        cmd.Parameters["@d9"].Value = Term.Text.Trim();
                        cmd.Parameters["@d10"].Value = TotalFees.Text;
                        cmd.Parameters["@d16"].Value = TotalFees.Text;
                        cmd.Parameters["@d17"].Value = PaymentDate.Text;
                        cmd.Parameters["@d22"].Value = DueFees.Text;
                        cmd.Parameters["@d23"].Value = Year.Text;
                        cmd.Parameters["@d24"].Value = totalfeesbalance;
                        cmd.Parameters["@d25"].Value = StudentName.Text;
                        MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    string smsallow = Properties.Settings.Default.smsallow;
                    if (smsallow == "Yes")
                    {
                        sendmessage();
                    }
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                }
                else
                {
                    //auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into feepayment(FeePaymentID,ScholarNo,FeeID,FDCourse,FDBranch,Semester,TutionFees,LibraryFees,UniversityDevelopmentFees,UniversityStudentWelfareFees,CautionMoney,OtherFees,TotalFees,DateOfPayment,ModeOfPayment,PaymentModeDetails,TotalPaid,Fine,DueFees,Year,TotalFees1,Student_name) VALUES (@d1,@d2,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d23"].Value = Year.Text;
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d25"].Value = StudentName.Text;
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(CautionMoney.Text);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(OtherFees.Text);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    int totalfeesbalance = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select TotalDue from FeesBalance where ScholarNo=@find";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters["@find"].Value = ScholarNo.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        totalfeesbalance = Convert.ToInt32(rdr["TotalDue"].ToString()) - Convert.ToInt32(TotalPaid.Text);
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb1 = "UPDATE FeesBalance SET Student_name=@d25,FDCourse=@d7,FDBranch=@d8,Semester=@d9,TutionFees=@d10,TotalFees=@d16,Year=@d23,DateOfPayment=@d17,DueFees=@d22,TotalDue=@d24 where ScholarNo=@d2";
                        cmd = new SqlCommand(cb1);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                        cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                        cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                        cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "Year"));
                        cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalDue"));
                        cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                        cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                        cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                        cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                        cmd.Parameters["@d8"].Value = Branch.Text.Trim();
                        cmd.Parameters["@d9"].Value = Term.Text.Trim();
                        cmd.Parameters["@d10"].Value = TotalFees.Text;
                        cmd.Parameters["@d16"].Value = TotalFees.Text;
                        cmd.Parameters["@d17"].Value = PaymentDate.Text;
                        cmd.Parameters["@d22"].Value = DueFees.Text;
                        cmd.Parameters["@d23"].Value = Year.Text;
                        cmd.Parameters["@d24"].Value = totalfeesbalance;
                        cmd.Parameters["@d25"].Value = StudentName.Text;
                        MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    string smsallow = Properties.Settings.Default.smsallow;
                    if (smsallow == "Yes")
                    {
                        sendmessage();
                    }
                    //MessageBox.Show("Successfully saved", "Fees Payment Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //sendmessage();
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

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }

        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update feepayment set ScholarNo=@d2,Year=@d23,FeeID=@d6,FDCourse=@d7,FDBranch=@d8,Semester=@d9,TutionFees=@d10,LibraryFees=@d11,UniversityDevelopmentFees=@d12,UniversityStudentWelfareFees=@d13,CautionMoney=@d14,OtherFees=@d15,TotalFees=@d16,DateOfPayment=@d17,ModeOfPayment=@d18,PaymentModeDetails=@d19,TotalPaid=@d20,Fine=@d21,DueFees=@d22 where FeePaymentID=@d1";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                cmd.Parameters["@d9"].Value = Term.Text.Trim();
                cmd.Parameters["@d10"].Value = Convert.ToInt32(TutionFees.Text);
                cmd.Parameters["@d11"].Value = Convert.ToInt32(LibraryFees.Text);
                cmd.Parameters["@d12"].Value = Convert.ToInt32(UDFees.Text);
                cmd.Parameters["@d13"].Value = Convert.ToInt32(USFees.Text);
                cmd.Parameters["@d23"].Value = Convert.ToInt32(Year.Text);
                if (Term.Text == "1st")
                {
                    cmd.Parameters["@d14"].Value = Convert.ToInt32(CautionMoney.Text);
                }
                else
                {
                    cmd.Parameters["@d14"].Value = 0;
                }
                cmd.Parameters["@d15"].Value = Convert.ToInt32(OtherFees.Text);
                cmd.Parameters["@d16"].Value = 0;
                cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                if (Fine.Text == "")
                {
                    cmd.Parameters["@d21"].Value = 0;
                }
                else
                {
                    cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                }
                cmd.Parameters["@d22"].Value = Convert.ToInt32(DueFees.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Fees Payment Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Update_record.Enabled = false;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FeesPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmMainMenu form2 = new frmMainMenu();
            form2.User.Text = label24.Text;
            form2.UserType.Text = label23.Text;
            form2.Show();*/
        }

        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(cs.DBConn);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        public DataView GetData()
        {
            dynamic SelectQry = "SELECT RTRIM(FeeID)[Fee ID],RTRIM(Year)[Year],RTRIM(Course)[Class], RTRIM(Semester)[Term], RTRIM(Branch)[Section] from FeesDetails order by course,semester ";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }

        private void Print_Click(object sender, EventArgs e)
        {
           /* try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                frmFeePaymentReceipt frm = new frmFeePaymentReceipt();
                rptFeePaymentReceipt rpt = new rptFeePaymentReceipt();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                CMS_DBDataSet1 myDS = new CMS_DBDataSet1();
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from feepayment,Student where student.Scholarno=FeePayment.ScholarNo and FeePaymentID= '" + FeePaymentID.Text + "'";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "FeePayment");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
            try
            {
                if (ScholarNo.Text == "")
                {
                    MessageBox.Show("Please select Scholar No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ScholarNo.Focus();
                    return;
                }
                printPreviewDialog1.Document = printDocument2;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmFeePaymentRecord frm = new frmFeePaymentRecord();
            this.Hide();
            frm.dataGridView1.DataSource = null;
            frm.Course.Text = "";
            frm.Branch.Text = "";
            frm.Date_from.Text = DateTime.Today.ToString();
            frm.Date_to.Text = DateTime.Today.ToString();
            frm.ScholarNo.Text = "";
            frm.dataGridView2.DataSource = null;
            frm.dataGridView3.DataSource = null;
            frm.dataGridView4.DataSource = null;
            frm.dataGridView5.DataSource = null;
            frm.PaymentDateFrom.Text = DateTime.Today.ToString();
            frm.PaymentDateTo.Text = DateTime.Today.ToString();
            frm.DateFrom.Text = DateTime.Today.ToString();
            frm.DateTo.Text = DateTime.Today.ToString();
            frm.dateTimePicker1.Text = DateTime.Today.ToString();
            frm.dateTimePicker2.Text = DateTime.Today.ToString();
            Branch.Enabled = false;
            frm.label13.Text = label23.Text;
            frm.label14.Text = label24.Text;
            frm.ShowDialog();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                FeeID.Text = dr.Cells[0].Value.ToString();
                Year.Text = dr.Cells[1].Value.ToString();
                Term.Text = dr.Cells[3].Value.ToString();
                FDCourse.Text = dr.Cells[2].Value.ToString();
                FDBranch.Text = dr.Cells[4].Value.ToString();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TutionFees,LibraryFees,UniversityDevelopmentFees,UniversityStudentWelfareFees,CautionMoney,OtherFees,TotalFees from FeesDetails WHERE Year='" + Year.Text + "' and  FeeID = '" + dr.Cells[0].Value.ToString() + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    TutionFees.Text = (String)rdr["TutionFees"];
                    OtherFees.Text = (String)rdr["OtherFees"];
                    USFees.Text = (String)rdr["UniversityStudentWelfareFees"];
                    LibraryFees.Text = (String)rdr["LibraryFees"];
                    CautionMoney.Text = (String)rdr["CautionMoney"];
                    UDFees.Text = (String)rdr["UniversityDevelopmentFees"];
                    TotalFees.Text = (String)rdr["TotalFees"];
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void Fine_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void TotalPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void TotalPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ScholarNo,FDCourse,FDBranch,Semester,DueFees,Year from FeePayment where  FDCourse= '" + FDCourse.Text + "' and ScholarNo= '" + ScholarNo.Text + "' and Semester='" + Term.Text + "' and Year='" + Year.Text + "' order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label27.Text = rdr["DueFees"].ToString();
                    int val4 = 0;
                    int val5 = 0;
                    int val6 = 0;
                    int.TryParse(label27.Text, out val4);
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
                    int.TryParse(TotalFees.Text, out val1);
                    int.TryParse(Fine.Text, out val2);
                    int.TryParse(TotalPaid.Text, out val3);
                    int I = ((val1 + val2) - val3);
                    DueFees.Text = I.ToString();
                }
                con.Close();
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
            int.TryParse(TotalFees.Text, out val1);
            int.TryParse(Fine.Text, out val2);
            int.TryParse(TotalPaid.Text, out val3);
            if (val3 > val1 + val2)
            {
                MessageBox.Show("Total Paid can not be more than Total Fees + Fine", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TotalFees.Text = "";
                TotalFees.Focus();
            }
        }
        private void Year_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);
        }

        private void Year_TextChanged(object sender, EventArgs e)
        {
            FeeID.Items.Clear();
            AutocompleFeeID();
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            company();
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
                string text = companyname;
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companyslogan;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companyaddress;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companycontact;
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

                text = "Date: " + DateTime.Now.ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Student: "+ScholarNo.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Name: " + StudentName.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Paid For:  School Fees";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Paid To: " + label24.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Total Ammount: UGX." + string.Format("{0:n0}", Convert.ToInt32(TutionFees.Text));
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Total Paid: UGX." + string.Format("{0:n0}", Convert.ToInt32(TotalPaid.Text));
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Due Payment: UGX." + string.Format("{0:n0}", Convert.ToInt32(DueFees.Text));
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

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            company();
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
                string text = companyname;
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companyslogan;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companyaddress;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = companycontact;
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Student No: " + ScholarNo.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = "Student Name: " + StudentName.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = "Date: " + DateTime.Now.ToString();
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                 con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM FeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" +FeePaymentID.Text+ "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "School Fees: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Registration Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Registration Fee: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Development Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Development Fee: " +string.Format("{0:n0}", Convert.ToInt32( rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='UNSA Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "UNSA Fee: " +string.Format("{0:n0}", Convert.ToInt32( rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Sesemat Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Sesemat Fee: " +string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='UNEB Reg Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "UNEB Registration Fee: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Ream'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Ream: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Sports Fee'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Sports Fee: " +string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Uniform'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Uniform: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Identity Card & Photos'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Identity Card & Photos: " +string.Format("{0:n0}", Convert.ToInt32( rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Tools'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Tools: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Hostel'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Hostel: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT TotalPaid FROM OtherFeePayment WHERE ScholarNo = '" + ScholarNo.Text + "' and FeePaymentID='" + FeePaymentID.Text + "' and FeeName='Others'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    text = "Others: " + string.Format("{0:n0}", Convert.ToInt32(rdr.GetInt32(0)));
                    e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;
                }

                text = "--------------------------------------------";
                e.Graphics.DrawString(text, drawFontArial12Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial12Bold).Height;
                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;
                text = "Paid To: " + label24.Text;
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;



                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "*****THANK YOU*****";
                e.Graphics.DrawString(text, drawFontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Bold).Height;

                text = " ";
                e.Graphics.DrawString(text, drawFontArial10Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial10Regular).Height;

                text = "Powered by: +256 787045644 Dither Technologies LTD";
                e.Graphics.DrawString(text, drawFontArial6Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += e.Graphics.MeasureString(text, drawFontArial6Regular).Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                   // BarPrinter = "no printer";
                   // KitchenPrinter = "no printer";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (FeePaymentID.Text == "")
                {
                    MessageBox.Show("Please Enter Fees Payment ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FeePaymentID.Focus();
                    return;
                }
                if (ScholarNo.Text == "")
                {
                    MessageBox.Show("Please select Scholar No.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ScholarNo.Focus();
                    return;
                }
                if (FeeID.Text == "")
                {
                    MessageBox.Show("Please select Fee ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    FeeID.Focus();
                    return;
                }
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select FeePaymentID,Year,ScholarNo,FDCourse,FDBranch,Semester,DueFees from FeePayment where  FDCourse= '" + FDCourse.Text + "' and ScholarNo= '" + ScholarNo.Text + "' and Semester='" + Term.Text + "' and Year='" + Year.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb3 = "update feepayment set DueFees=@d22 where ScholarNo=@d2 and Semester=@d9 and Year=@d23 and FDCourse=@d7 ";
                    cmd = new SqlCommand(cb3);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(TutionFees.Text);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(LibraryFees.Text);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(UDFees.Text);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(USFees.Text);
                    cmd.Parameters["@d23"].Value = Convert.ToInt32(Year.Text);
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(TotalFees.Text);
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(CautionMoney.Text);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(OtherFees.Text);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(0);
                    cmd.ExecuteReader();
                    con.Close();


                    auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into feepayment(FeePaymentID,ScholarNo,FeeID,FDCourse,FDBranch,Semester,TutionFees,LibraryFees,UniversityDevelopmentFees,UniversityStudentWelfareFees,CautionMoney,OtherFees,TotalFees,DateOfPayment,ModeOfPayment,PaymentModeDetails,TotalPaid,Fine,DueFees,Year,TotalFees1,Student_name) VALUES (@d1,@d2,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d23"].Value = Year.Text;
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d25"].Value = StudentName.Text;
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(0);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    int totalfeesbalance = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select TotalDue from FeesBalance where ScholarNo=@find";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters["@find"].Value = ScholarNo.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        totalfeesbalance = Convert.ToInt32(rdr["TotalDue"].ToString()) - Convert.ToInt32(TotalPaid.Text);
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb1 = "UPDATE FeesBalance SET Student_name=@d25,FDCourse=@d7,FDBranch=@d8,Semester=@d9,TutionFees=@d10,TotalFees=@d16,Year=@d23,DateOfPayment=@d17,DueFees=@d22,TotalDue=@d24 where ScholarNo=@d2";
                        cmd = new SqlCommand(cb1);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                        cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                        cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                        cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "Year"));
                        cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalDue"));
                        cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                        cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                        cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                        cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                        cmd.Parameters["@d8"].Value = Branch.Text.Trim();
                        cmd.Parameters["@d9"].Value = Term.Text.Trim();
                        cmd.Parameters["@d10"].Value = TotalFees.Text;
                        cmd.Parameters["@d16"].Value = TotalFees.Text;
                        cmd.Parameters["@d17"].Value = PaymentDate.Text;
                        cmd.Parameters["@d22"].Value = DueFees.Text;
                        cmd.Parameters["@d23"].Value = Year.Text;
                        cmd.Parameters["@d24"].Value = totalfeesbalance;
                        cmd.Parameters["@d25"].Value = StudentName.Text;
                        MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    string smsallow = Properties.Settings.Default.smsallow;
                    if (smsallow == "Yes")
                    {
                        sendmessage();
                    }
                    //MessageBox.Show("Successfully saved", "Fees Payment Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //sendmessage();
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    //con.Close();
                }
                else
                {
                    //auto();
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into feepayment(FeePaymentID,ScholarNo,FeeID,FDCourse,FDBranch,Semester,TutionFees,LibraryFees,UniversityDevelopmentFees,UniversityStudentWelfareFees,CautionMoney,OtherFees,TotalFees,DateOfPayment,ModeOfPayment,PaymentModeDetails,TotalPaid,Fine,DueFees,Year,TotalFees1,Student_name) VALUES (@d1,@d2,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "FeeId"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "LibraryFees"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "UniversityDevelopmentFees"));
                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 15, "UniversityStudentWelfareFees"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "CautionMoney"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "OtherFees"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "ModeOfPayment"));
                    cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.VarChar, 200, "PaymentModeDetails"));
                    cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.Int, 10, "TotalPaid"));
                    cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.Int, 10, "Fine"));
                    cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                    cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalFees1"));
                    cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                    cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                    cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                    cmd.Parameters["@d6"].Value = FeeID.Text.Trim();
                    cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                    cmd.Parameters["@d8"].Value = FDBranch.Text.Trim();
                    cmd.Parameters["@d9"].Value = Term.Text.Trim();
                    cmd.Parameters["@d10"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d11"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d12"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d13"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d23"].Value = Year.Text;
                    cmd.Parameters["@d24"].Value = Convert.ToInt32(0);
                    cmd.Parameters["@d25"].Value = StudentName.Text;
                    if (Term.Text == "1st")
                    {
                        cmd.Parameters["@d14"].Value = Convert.ToInt32(CautionMoney.Text);
                    }
                    else
                    {
                        cmd.Parameters["@d14"].Value = 0;
                    }
                    cmd.Parameters["@d15"].Value = Convert.ToInt32(OtherFees.Text);
                    cmd.Parameters["@d16"].Value = 0;
                    cmd.Parameters["@d17"].Value = (PaymentDate.Text);
                    cmd.Parameters["@d18"].Value = (ModeOfPayment.Text);
                    cmd.Parameters["@d19"].Value = (PaymentModeDetails.Text);
                    cmd.Parameters["@d20"].Value = Convert.ToInt32(TotalPaid.Text);
                    if (Fine.Text == "")
                    {
                        cmd.Parameters["@d21"].Value = 0;
                    }
                    else
                    {
                        cmd.Parameters["@d21"].Value = Convert.ToInt32(Fine.Text);
                    }
                    cmd.Parameters["@d22"].Value = Convert.ToInt32(DueFees.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    int totalfeesbalance = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select TotalDue from FeesBalance where ScholarNo=@find";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters["@find"].Value = ScholarNo.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        totalfeesbalance = Convert.ToInt32(rdr["TotalDue"].ToString()) - Convert.ToInt32(TotalPaid.Text);
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb1 = "UPDATE FeesBalance SET Student_name=@d25,FDCourse=@d7,FDBranch=@d8,Semester=@d9,TutionFees=@d10,TotalFees=@d16,Year=@d23,DateOfPayment=@d17,DueFees=@d22,TotalDue=@d24 where ScholarNo=@d2";
                        cmd = new SqlCommand(cb1);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "FeePaymentID"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "FDCourse"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 30, "FDBranch"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 10, "TutionFees"));
                        cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.Int, 10, "TotalFees"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "DateOfPayment"));
                        cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.Int, 10, "DueFees"));
                        cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "Year"));
                        cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "TotalDue"));
                        cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 50, "Student_name"));
                        cmd.Parameters["@d1"].Value = FeePaymentID.Text.Trim();
                        cmd.Parameters["@d2"].Value = ScholarNo.Text.Trim();
                        cmd.Parameters["@d7"].Value = FDCourse.Text.Trim();
                        cmd.Parameters["@d8"].Value = Branch.Text.Trim();
                        cmd.Parameters["@d9"].Value = Term.Text.Trim();
                        cmd.Parameters["@d10"].Value = TotalFees.Text;
                        cmd.Parameters["@d16"].Value = TotalFees.Text;
                        cmd.Parameters["@d17"].Value = PaymentDate.Text;
                        cmd.Parameters["@d22"].Value = DueFees.Text;
                        cmd.Parameters["@d23"].Value = Year.Text;
                        cmd.Parameters["@d24"].Value = totalfeesbalance;
                        cmd.Parameters["@d25"].Value = StudentName.Text;
                        MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    string smsallow = Properties.Settings.Default.smsallow;
                    if (smsallow == "Yes")
                    {
                        sendmessage();
                    }
                    //MessageBox.Show("Successfully saved", "Fees Payment Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //sendmessage();
                    //printPreviewDialog1.Document = printDocument1;
                    //printPreviewDialog1.ShowDialog();
                    btnSave.Enabled = false;
                    Print.Enabled = true;
                    //con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                company();
                rptReceiptFeespayment rpt = new rptReceiptFeespayment(); //The report you created.
                //MessageBox.Show("two", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                DataSet myDS = new DataSet(); //The DataSet you created.
                frmFeePaymentReceipt frm = new frmFeePaymentReceipt();
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
                rpt.SetParameterValue("paidto", ScholarNo.Text);
                rpt.SetParameterValue("paid", TotalPaid.Text);
                rpt.SetParameterValue("payable", TotalFees.Text);
                rpt.SetParameterValue("due", DueFees.Text);
                rpt.SetParameterValue("receivedby", label24.Text);
                rpt.SetParameterValue("destination", FDCourse.Text);
                rpt.SetParameterValue("term", Term.Text);
                rpt.SetParameterValue("year", Year.Text);
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

        private void ScholarNo_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new frmClientDetails();
            frm.ShowDialog();
            ScholarNo.Text = frm.clientnames.Text;
        }

        private void ScholarNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Student_Name,Course,Branch FROM student WHERE ScholarNo = '" + ScholarNo.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    StudentName.Text = rdr.GetString(0).Trim();
                    Course.Text = rdr.GetString(1).Trim();
                    Branch.Text = rdr.GetString(2).Trim();
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
    }
}

