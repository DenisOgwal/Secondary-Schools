using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class frmScholarshipAssign : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        ConnectionString cs = new ConnectionString();
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        SqlDataAdapter adp;
        DataSet ds1 = new DataSet();
        public frmScholarshipAssign()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmScholarshipAssign_Load(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Distinct RTRIM(ScholarshipName) FROM Scholarship";
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    scholarshipname.Items.Add(rdr[0].ToString());
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
        int percentages = 0;
        int ammounts = 0;
        int scholarship = 0;
        private void scholarshipname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
            con = new SqlConnection(cs.DBConn);
            con.Open();
            cmd = new SqlCommand("select TotalFees from FeesDetails where Course='" + studentclass.Text + "' and Semester='" + studentterm.Text + "'", con);
            rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select Amount from Scholarship where ScholarshipName='" + scholarshipname.Text.Trim() + "'", con);
                percentages = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select TotalFees from FeesDetails where Course='" + studentclass.Text + "' and Semester='" + studentterm.Text + "' order by ID Desc", con);
                ammounts = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                if ((rdr != null))
                {
                    rdr.Close();
                }

            }
            else
            {
                MessageBox.Show("Fees not yet set for that class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            percentage.Text = percentages.ToString();
            double divs = percentages * 0.01;
            scholarship = Convert.ToInt32(divs * ammounts);
            amountpayable.Text = (ammounts - scholarship).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void percentage_TextChanged(object sender, EventArgs e)
        {
            try { 
            double divs =Convert.ToInt32(percentage.Text) * 0.01;
            scholarship = Convert.ToInt32(divs * ammounts);
            amountpayable.Text = (ammounts - scholarship).ToString();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select * from ScholarshipStudents where  Class= '" + studentclass.Text + "' and Term= '" + studentterm.Text + "' and Year='" +studentyear.Text + "' and ScholarNo='" + studentno.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    MessageBox.Show("Bursary Already Assigned ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "insert into ScholarshipStudents(Student_name,ScholarNo,Percentage,PaymentDate,Term,Class,Year,AmountPayable) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 50, "Student_name"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "Percentage"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 20, "PaymentDate"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Term"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 10, "Class"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "Year"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "AmountPayable"));

                    cmd.Parameters["@d1"].Value = studentname.Text;
                    cmd.Parameters["@d2"].Value = studentno.Text;
                    cmd.Parameters["@d3"].Value = percentage.Text;
                    cmd.Parameters["@d4"].Value = PaymentDate.Text;
                    cmd.Parameters["@d5"].Value = studentterm.Text;
                    cmd.Parameters["@d6"].Value = studentclass.Text;
                    cmd.Parameters["@d7"].Value = studentyear.Text;
                    cmd.Parameters["@d8"].Value = amountpayable.Text;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    int totalfeesbalance = 0;
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct2 = "select TotalDue from FeesBalance where ScholarNo=@find";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                    cmd.Parameters["@find"].Value = studentno.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        totalfeesbalance = (Convert.ToInt32(rdr["TotalDue"].ToString()) - Convert.ToInt32(ammounts)) + Convert.ToInt32(amountpayable.Text);
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb1 = "UPDATE FeesBalance SET Student_name=@d25,Semester=@d9,Year=@d23,DateOfPayment=@d17,DueFees=@d22,TotalDue=@d24 where ScholarNo=@d2";
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
                        cmd.Parameters["@d1"].Value = "N/A";
                        cmd.Parameters["@d2"].Value = studentno.Text; ;
                        cmd.Parameters["@d7"].Value = studentclass.Text;
                        cmd.Parameters["@d8"].Value = "N/A";
                        cmd.Parameters["@d9"].Value = studentterm.Text;
                        cmd.Parameters["@d10"].Value = 0;
                        cmd.Parameters["@d16"].Value = 0;
                        cmd.Parameters["@d17"].Value = PaymentDate.Text;
                        cmd.Parameters["@d22"].Value = amountpayable.Text;
                        cmd.Parameters["@d23"].Value = studentyear.Text;
                        cmd.Parameters["@d24"].Value = totalfeesbalance;
                        cmd.Parameters["@d25"].Value = studentname.Text;
                        cmd.ExecuteNonQuery();
                        delete_records2();
                        con.Close();
                         MessageBox.Show("Successfully Saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void delete_records2()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from FeePayment where  ScholarNo='" + studentno.Text + "' and FDCourse='" + studentclass.Text + "' and Semester='" +studentterm.Text + "' and Year='" + studentyear.Text + "'";
                cmd = new SqlCommand(cq, con);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {

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
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void studentno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Student_Name,Course,Branch,Session,Term FROM Student WHERE ScholarNo = '" + studentno.Text + "'";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    studentname.Text = rdr.GetString(0).Trim();
                    studentclass.Text = rdr.GetString(1).Trim();
                    studentterm.Text = rdr.GetString(4).Trim();
                    studentyear.Text = rdr.GetString(3).Trim();
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

        private void studentno_Click(object sender, EventArgs e)
        {
            frmClientDetails frm = new frmClientDetails();
            frm.ShowDialog();
            studentno.Text = frm.clientnames.Text;
        }
    }
}
