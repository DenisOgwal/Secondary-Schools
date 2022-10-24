using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmAnalysis : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        //int pupiltotal = 0;
        public frmAnalysis()
        {
            InitializeComponent();
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCourse.Items.Clear();
                cmbCourse.Text = "";
                cmbCourse.Enabled = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Class) from GradingSystem where Grading='Old'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbCourse.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            cmbBranch.Text = "";
            cmbBranch.Enabled = true;
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(branch) from Student where course = '" + cmbCourse.Text + "' and session='" + cmbSession.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbBranch.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSemester.Items.Clear();
            cmbSemester.Text = "";
            cmbSemester.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Semester) from batch where Course = '" + cmbCourse.Text + "' and session='" + cmbSession.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbSemester.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSubjectCode.Items.Clear();
                cmbSubjectCode.Text = "";
                cmbSubjectCode.Enabled = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(SubjectCode) from SubjectInfo where CourseName = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "' and semester= '" + cmbSemester.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbSubjectCode.Items.Add(rdr[0]);
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

       
        private void cmbSubjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "SELECT SubjectName FROM SubjectInfo WHERE SubjectCode = '" + cmbSubjectCode.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.Read())
                {
                    txtSubjectName.Text = rdr.GetString(0).Trim();
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

        public void AutocompleSession()
        {
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
                    cmbSession.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmInternalMarksEntry_Load(object sender, EventArgs e)
        {
            AutocompleSession();
        }

        
        private void Reset()
        {
            cmbBranch.Text = "";
            cmbCourse.Text = "";
            cmbSession.Text = "";
            cmbSemester.Text = "";
            cmbSubjectCode.Text = "";
            txtSubjectName.Text = "";
            cmbExamName.Text = "";
            cmbBranch.Enabled = false;
            cmbCourse.Enabled = false;
            cmbSemester.Enabled = false;
            cmbSubjectCode.Enabled = false;
            cmbSession.Focus();
        }


        private void frmInternalMarksEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label5.Text;
            frm.User.Text = label6.Text;
            frm.Show();
        }
        String D1marks;
        String D2marks;
        String C3marks;
        String C4marks;
        String C5marks;
        String C6marks;
        String C7marks;
        String P7marks;
        String P8marks;
        String F9marks;

        String D1marksp;
        String D2marksp;
        String C3marksp;
        String C4marksp;
        String C5marksp;
        String C6marksp;
        String C7marksp;
        String P7marksp;
        String P8marksp;
        String F9marksp;

        String subjectname;
        String forclass;
        String Year;
        String Term;
        String subjectcode;
        string totals;
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

        private void button1_Click(object sender, EventArgs e)
        {
            company();
            if (cmbSession.Text == "")
            {
                MessageBox.Show("Please select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSession.Focus();
                return;
            }
            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
           
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select Subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            Year = cmbSession.Text;
            Term = cmbSemester.Text;
            subjectcode = cmbSubjectCode.Text;
            try
            {
                forclass = cmbCourse.Text + " Class";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                subjectname = txtSubjectName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbBranch.Text != "A Level")
            {

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totals = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='D1'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marksi = Convert.ToDouble(D1marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double D1markspi = Math.Round(((D1marksi / totalsi) * 100), 1);
                        D1marksp = D1markspi.ToString();
                    }
                    else
                    {
                        D1marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='D2'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marksi = Convert.ToDouble(D2marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double D2markspi = Math.Round(((D2marksi / totalsi) * 100), 1);
                        D2marksp = D2markspi.ToString();
                    }
                    else
                    {
                        D2marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C3'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marksi = Convert.ToDouble(C3marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C3markspi = Math.Round(((C3marksi / totalsi) * 100), 1);
                        C3marksp = C3markspi.ToString();
                    }
                    else
                    {
                        C3marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C4'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marksi = Convert.ToDouble(C4marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C4markspi = Math.Round(((C4marksi / totalsi) * 100), 1);
                        C4marksp = C4markspi.ToString();
                    }
                    else
                    {
                        C4marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C5'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marksi = Convert.ToDouble(C5marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C5markspi = Math.Round(((C5marksi / totalsi) * 100), 1);
                        C5marksp = C5markspi.ToString();
                    }
                    else
                    {
                        C5marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C6'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marksi = Convert.ToDouble(C6marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C6markspi = Math.Round(((C6marksi / totalsi) * 100), 1);
                        C6marksp = C6markspi.ToString();
                    }
                    else
                    {
                        C6marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='P7'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marksi = Convert.ToDouble(P7marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double P7markspi = Math.Round(((P7marksi / totalsi) * 100), 1);
                        P7marksp = P7markspi.ToString();
                    }
                    else
                    {
                        P7marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='P8'";
                    cmd = new SqlCommand(inquery3, con);
                    P8marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P8marksi = Convert.ToDouble(P8marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double P8markspi = Math.Round(((P8marksi / totalsi) * 100), 1);
                        P8marksp = P8markspi.ToString();
                    }
                    else
                    {
                        P8marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='F9'";
                    cmd = new SqlCommand(inquery3, con);
                    F9marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double F9marksi = Convert.ToDouble(F9marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double F9markspi = Math.Round(((F9marksi / totalsi) * 100), 1);
                        F9marksp = F9markspi.ToString();
                    }
                    else
                    {
                        F9marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rptclassmarkssummary rpt = new rptclassmarkssummary(); //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet(); //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    myConnection.Open();
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select  * from Purchases";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Purchases");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("year", Year);
                    rpt.SetParameterValue("subjectcode", subjectcode);
                    rpt.SetParameterValue("term", Term);
                    rpt.SetParameterValue("subjectname", subjectname);
                    rpt.SetParameterValue("class", forclass);
                    rpt.SetParameterValue("D1marks", D1marks);
                    rpt.SetParameterValue("D2marks", D2marks);
                    rpt.SetParameterValue("C3marks", C3marks);
                    rpt.SetParameterValue("C4marks", C4marks);
                    rpt.SetParameterValue("C5marks", C5marks);
                    rpt.SetParameterValue("C6marks", C6marks);
                    rpt.SetParameterValue("P7marks", P7marks);
                    rpt.SetParameterValue("P8marks", P8marks);
                    rpt.SetParameterValue("F9marks", F9marks);
                    rpt.SetParameterValue("D1marksp", D1marksp);
                    rpt.SetParameterValue("D2marksp", D2marksp);
                    rpt.SetParameterValue("C3marksp", C3marksp);
                    rpt.SetParameterValue("C4marksp", C4marksp);
                    rpt.SetParameterValue("C5marksp", C5marksp);
                    rpt.SetParameterValue("C6marksp", C6marksp);
                    rpt.SetParameterValue("P7marksp", P7marksp);
                    rpt.SetParameterValue("P8marksp", P8marksp);
                    rpt.SetParameterValue("F9marksp", F9marksp);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                     rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    //rpt.SetParameterValue("picpath", "logo.jpg");
                    crystalReportViewer1.ReportSource = rpt;
                    //crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totals = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='A'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marksi = Convert.ToDouble(D1marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double D1markspi = Math.Round(((D1marksi / totalsi) * 100), 1);
                        D1marksp = D1markspi.ToString();
                    }
                    else
                    {
                        D1marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='B'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marksi = Convert.ToDouble(D2marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double D2markspi = Math.Round(((D2marksi / totalsi) * 100), 1);
                        D2marksp = D2markspi.ToString();
                    }
                    else
                    {
                        D2marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='C'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marksi = Convert.ToDouble(C3marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C3markspi = Math.Round(((C3marksi / totalsi) * 100), 1);
                        C3marksp = C3markspi.ToString();
                    }
                    else
                    {
                        C3marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='D'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marksi = Convert.ToDouble(C4marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C4markspi = Math.Round(((C4marksi / totalsi) * 100), 1);
                        C4marksp = C4markspi.ToString();
                    }
                    else
                    {
                        C4marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='E'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marksi = Convert.ToDouble(C5marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C5markspi = Math.Round(((C5marksi / totalsi) * 100), 1);
                        C5marksp = C5markspi.ToString();
                    }
                    else
                    {
                        C5marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='O'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marksi = Convert.ToDouble(C6marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double C6markspi = Math.Round(((C6marksi / totalsi) * 100), 1);
                        C6marksp = C6markspi.ToString();
                    }
                    else
                    {
                        C6marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LeveladvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and grade='F'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marksi = Convert.ToDouble(P7marks);
                    Double totalsi = Convert.ToDouble(totals);
                    if (totalsi != 0)
                    {
                        Double P7markspi = Math.Round(((P7marksi / totalsi) * 100), 1);
                        P7marksp = P7markspi.ToString();
                    }
                    else
                    {
                        P7marksp = "N/A";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rptclassmarkssummaryA rpt = new rptclassmarkssummaryA(); //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    myConnection.Open();
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select  * from Purchases";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Purchases");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("year", Year);
                    rpt.SetParameterValue("subjectcode", subjectcode);
                    rpt.SetParameterValue("term", Term);
                    rpt.SetParameterValue("subjectname", subjectname);
                    rpt.SetParameterValue("class", forclass);
                    rpt.SetParameterValue("D1marks", D1marks);
                    rpt.SetParameterValue("D2marks", D2marks);
                    rpt.SetParameterValue("C3marks", C3marks);
                    rpt.SetParameterValue("C4marks", C4marks);
                    rpt.SetParameterValue("C5marks", C5marks);
                    rpt.SetParameterValue("C6marks", C6marks);
                    rpt.SetParameterValue("P7marks", P7marks);
                    rpt.SetParameterValue("D1marksp", D1marksp);
                    rpt.SetParameterValue("D2marksp", D2marksp);
                    rpt.SetParameterValue("C3marksp", C3marksp);
                    rpt.SetParameterValue("C4marksp", C4marksp);
                    rpt.SetParameterValue("C5marksp", C5marksp);
                    rpt.SetParameterValue("C6marksp", C6marksp);
                    rpt.SetParameterValue("P7marksp", P7marksp);
                    crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        string D1marks7;
        string D2marks7;
        string C3marks7;
        string C4marks7;
        string C5marks7;
        string C6marks7;
        string P7marks7;
        string P8marks7;
        string F9marks7;

        string D1marks6;
        string D2marks6;
        string C3marks6;
        string C4marks6;
        string C5marks6;
        string C6marks6;
        string P7marks6;
        string P8marks6;
        string F9marks6;

        string D1marks5;
        string D2marks5;
        string C3marks5;
        string C4marks5;
        string C5marks5;
        string C6marks5;
        string P7marks5;
        string P8marks5;
        string F9marks5;

        string D1marksS4;
        string D2marksS4;
        string C3marksS4;
        string C4marksS4;
        string C5marksS4;
        string C6marksS4;
        string P7marksS4;
        string P8marksS4;
        string F9marksS4;

        string D1marks7p;
        string D2marks7p;
        string C3marks7p;
        string C4marks7p;
        string C5marks7p;
        string C6marks7p;
        string P7marks7p;
        string P8marks7p;
        string F9marks7p;

        string D1marks6p;
        string D2marks6p;
        string C3marks6p;
        string C4marks6p;
        string C5marks6p;
        string C6marks6p;
        string P7marks6p;
        string P8marks6p;
        string F9marks6p;

        string D1marks5p;
        string D2marks5p;
        string C3marks5p;
        string C4marks5p;
        string C5marks5p;
        string C6marks5p;
        string P7marks5p;
        string P8marks5p;
        string F9marks5p;

        string D1marksS4p="";
        string D2marksS4p="";
        string C3marksS4p="";
        string C4marksS4p="";
        string C5marksS4p="";
        string C6marksS4p="";
        string P7marksS4p="";
        string P8marksS4p="";
        string F9marksS4p="";

        string totalp5="";
        string totalp6="";
        string totalp7="";
        string totalS4="";

        private void button2_Click(object sender, EventArgs e)
        {
            company();
            if (cmbSession.Text == "")
            {
                MessageBox.Show("Please select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSession.Focus();
                return;
            }
            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            Year = cmbSession.Text;
            Term = cmbSemester.Text;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "'";
                cmd = new SqlCommand(inquery3, con);
                totalp5 = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "'  and grade='D1'";
                cmd = new SqlCommand(inquery3, con);
                D1marks5 = cmd.ExecuteScalar().ToString();
                con.Close();
                Double D1marks5i = Convert.ToDouble(D1marks5);
                Double totalp5i = Convert.ToDouble(totalp5);
                if (totalp5i != 0)
                {
                    Double D1marks5pi =Math.Round(((D1marks5i / totalp5i) * 100),1);
                    D1marks5p = D1marks5pi.ToString();
                }
                else {
                    D1marks5p = "N/A";
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cmbBranch.Text != "A Level")
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='D2'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marks5i = Convert.ToDouble(D2marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double D2marks5pi = Math.Round(((D2marks5i / totalp5i) * 100), 1);
                        D2marks5p = D2marks5pi.ToString();
                    }
                    else
                    {
                        D2marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='C3'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marks5i = Convert.ToDouble(C3marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double C3marks5pi = Math.Round(((C3marks5i / totalp5i) * 100), 1);
                        C3marks5p = C3marks5pi.ToString();
                    }
                    else
                    {
                        C3marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='C4'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marks5i = Convert.ToDouble(C4marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double C4marks5pi = Math.Round(((C4marks5i / totalp5i) * 100), 1);
                        C4marks5p = C4marks5pi.ToString();
                    }
                    else
                    {
                        C4marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='C5'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marks5i = Convert.ToDouble(C5marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double C5marks5pi = Math.Round(((C5marks5i / totalp5i) * 100), 1);
                        C5marks5p = C5marks5pi.ToString();
                    }
                    else
                    {
                        C5marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "'  and grade='C6'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marks5i = Convert.ToDouble(C6marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double C6marks5pi = Math.Round(((C6marks5i / totalp5i) * 100), 1);
                        C6marks5p = C6marks5pi.ToString();
                    }
                    else
                    {
                        C6marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='P7'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marks5i = Convert.ToDouble(P7marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double P7marks5pi = Math.Round(((P7marks5i / totalp5i) * 100), 1);
                        P7marks5p = P7marks5pi.ToString();
                    }
                    else
                    {
                        P7marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "'  and grade='P8'";
                    cmd = new SqlCommand(inquery3, con);
                    P8marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P8marks5i = Convert.ToDouble(P8marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double P8marks5pi = Math.Round(((P8marks5i / totalp5i) * 100), 1);
                        P8marks5p = P8marks5pi.ToString();
                    }
                    else
                    {
                        P8marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.3' and term='" + cmbSemester.Text + "' and grade='F9'";
                    cmd = new SqlCommand(inquery3, con);
                    F9marks5 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double F9marks5i = Convert.ToDouble(F9marks5);
                    Double totalp5i = Convert.ToDouble(totalp5);
                    if (totalp5i != 0)
                    {
                        Double F9marks5pi = Math.Round(((F9marks5i / totalp5i) * 100), 1);
                        F9marks5p = F9marks5pi.ToString();
                    }
                    else
                    {
                        F9marks5p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //end of P.5


                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totalp6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "'  and grade='D1'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marks6i = Convert.ToDouble(D1marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);

                    if (totalp6i != 0)
                    {
                        Double D1marks6pi = Math.Round(((D1marks6i / totalp6i) * 100), 1);
                        D1marks6p = D1marks6pi.ToString();
                    }
                    else
                    {
                        D1marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='D2'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marks6i = Convert.ToDouble(D2marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double D2marks6pi = Math.Round(((D2marks6i / totalp6i) * 100), 1);
                        D2marks6p = D2marks6pi.ToString();
                    }
                    else
                    {
                        D2marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='C3'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marks6i = Convert.ToDouble(C3marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C3marks6pi = Math.Round(((C3marks6i / totalp6i) * 100), 1);
                        C3marks6p = C3marks6pi.ToString();
                    }
                    else
                    {
                        C3marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='C4'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marks6i = Convert.ToDouble(C4marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C4marks6pi = Math.Round(((C4marks6i / totalp6i) * 100), 1);
                        C4marks6p = C4marks6pi.ToString();
                    }
                    else
                    {
                        C4marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='C5'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marks6i = Convert.ToDouble(C5marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C5marks6pi = Math.Round(((C5marks6i / totalp6i) * 100), 1);
                        C5marks6p = C5marks6pi.ToString();
                    }
                    else
                    {
                        C5marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "'  and grade='C6'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marks6i = Convert.ToDouble(C6marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C6marks6pi = Math.Round(((C6marks6i / totalp6i) * 100), 1);
                        C6marks6p = C6marks6pi.ToString();
                    }
                    else
                    {
                        C6marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='P7'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marks6i = Convert.ToDouble(P7marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double P7marks6pi = Math.Round(((P7marks6i / totalp6i) * 100), 1);
                        P7marks6p = P7marks6pi.ToString();
                    }
                    else
                    {
                        P7marks6p = "N/A";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "'  and grade='P8'";
                    cmd = new SqlCommand(inquery3, con);
                    P8marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P8marks6i = Convert.ToDouble(P8marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double P8marks6pi = Math.Round(((P8marks6i / totalp6i) * 100), 1);
                        P8marks6p = P8marks6pi.ToString();
                    }
                    else
                    {
                        P8marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.2' and term='" + cmbSemester.Text + "' and grade='F9'";
                    cmd = new SqlCommand(inquery3, con);
                    F9marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double F9marks6i = Convert.ToDouble(F9marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double F9marks6pi = Math.Round(((F9marks6i / totalp6i) * 100), 1);
                        F9marks6p = F9marks6pi.ToString();
                    }
                    else
                    {
                        F9marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //End of P.6


                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totalp7 = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "'  and grade='D1'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marks7i = Convert.ToDouble(D1marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double D1marks7pi = Math.Round(((D1marks7i / totalp7i) * 100), 1);
                        D1marks7p = D1marks7pi.ToString();
                    }
                    else
                    {
                        D1marks7p = "N/A";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='D2'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marks7i = Convert.ToDouble(D2marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double D2marks7pi = Math.Round(((D2marks7i / totalp7i) * 100), 1);
                        D2marks7p = D2marks7pi.ToString();
                    }
                    else
                    {
                        D2marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='C3'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marks7i = Convert.ToDouble(C3marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C3marks7pi = Math.Round(((C3marks7i / totalp7i) * 100), 1);
                        C3marks7p = C3marks7pi.ToString();
                    }
                    else
                    {
                        C3marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='C4'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marks7i = Convert.ToDouble(C4marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C4marks7pi = Math.Round(((C4marks7i / totalp7i) * 100), 1);
                        C4marks7p = C4marks7pi.ToString();
                    }
                    else
                    {
                        C4marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='C5'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marks7i = Convert.ToDouble(C5marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C5marks7pi = Math.Round(((C5marks7i / totalp7i) * 100), 1);
                        C5marks7p = C5marks7pi.ToString();
                    }
                    else
                    {
                        C5marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "'  and grade='C6'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marks7i = Convert.ToDouble(C6marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C6marks7pi = Math.Round(((C6marks7i / totalp7i) * 100), 1);
                        C6marks7p = C6marks7pi.ToString();
                    }
                    else
                    {
                        C6marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='P7'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marks7i = Convert.ToDouble(P7marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double P7marks7pi = Math.Round(((P7marks7i / totalp7i) * 100), 1);
                        P7marks7p = P7marks7pi.ToString();
                    }
                    else
                    {
                        P7marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "' and grade='P8'";
                    cmd = new SqlCommand(inquery3, con);
                    P8marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P8marks7i = Convert.ToDouble(P8marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double P8marks7pi = Math.Round(((P8marks7i / totalp7i) * 100), 1);
                        P8marks7p = P8marks7pi.ToString();
                    }
                    else
                    {
                        P8marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.1' and term='" + cmbSemester.Text + "'  and grade='F9'";
                    cmd = new SqlCommand(inquery3, con);
                    F9marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double F9marks7i = Convert.ToDouble(F9marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double F9marks7pi = Math.Round(((F9marks7i / totalp7i) * 100), 1);
                        F9marks7p = F9marks7pi.ToString();
                    }
                    else
                    {
                        F9marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //S.4 

                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totalS4 = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "'  and grade='D1'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marksS4i = Convert.ToDouble(D1marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double D1marksS4pi = Math.Round(((D1marksS4i / totalS4i) * 100), 1);
                        D1marksS4p = D1marksS4pi.ToString();
                    }
                    else
                    {
                        D1marksS4p = "N/A";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='D2'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marksS4i = Convert.ToDouble(D2marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double D2marksS4pi = Math.Round(((D2marksS4i / totalS4i) * 100), 1);
                        D2marksS4p = D2marksS4pi.ToString();
                    }
                    else
                    {
                        D2marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='C3'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marksS4i = Convert.ToDouble(C3marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double C3marksS4pi = Math.Round(((C3marksS4i / totalS4i) * 100), 1);
                        C3marksS4p = C3marksS4pi.ToString();
                    }
                    else
                    {
                        C3marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='C4'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marksS4i = Convert.ToDouble(C4marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double C4marksS4pi = Math.Round(((C4marksS4i / totalS4i) * 100), 1);
                        C4marksS4p = C4marksS4pi.ToString();
                    }
                    else
                    {
                        C4marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='C5'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marksS4i = Convert.ToDouble(C5marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double C5marksS4pi = Math.Round(((C5marksS4i / totalS4i) * 100), 1);
                        C5marksS4p = C5marksS4pi.ToString();
                    }
                    else
                    {
                        C5marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "'  and grade='C6'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marksS4i = Convert.ToDouble(C6marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double C6marksS4pi = Math.Round(((C6marksS4i / totalS4i) * 100), 1);
                        C6marksS4p = C6marksS4pi.ToString();
                    }
                    else
                    {
                        C6marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='P7'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marksS4i = Convert.ToDouble(P7marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double P7marksS4pi = Math.Round(((P7marksS4i / totalS4i) * 100), 1);
                        P7marksS4p = P7marksS4pi.ToString();
                    }
                    else
                    {
                        P7marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "'  and grade='P8'";
                    cmd = new SqlCommand(inquery3, con);
                    P8marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P8marksS4i = Convert.ToDouble(P8marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double P8marksS4pi = Math.Round(((P8marksS4i / totalS4i) * 100), 1);
                        P8marksS4p = P8marksS4pi.ToString();
                    }
                    else
                    {
                        P8marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.4' and term='" + cmbSemester.Text + "' and grade='F9'";
                    cmd = new SqlCommand(inquery3, con);
                    F9marksS4 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double F9marksS4i = Convert.ToDouble(F9marksS4);
                    Double totalS4i = Convert.ToDouble(totalS4);
                    if (totalS4i != 0)
                    {
                        Double F9marksS4pi = Math.Round(((F9marksS4i / totalS4i) * 100), 1);
                        F9marksS4p = F9marksS4pi.ToString();
                    }
                    else
                    {
                        F9marksS4p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rpttermmarkssummary rpt = new rpttermmarkssummary(); //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet(); //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    myConnection.Open();
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select  * from Purchases";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Purchases");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("year", Year);
                    rpt.SetParameterValue("term", Term);
                    rpt.SetParameterValue("D1marks5", D1marks5);
                    rpt.SetParameterValue("D2marks5", D2marks5);
                    rpt.SetParameterValue("C3marks5", C3marks5);
                    rpt.SetParameterValue("C4marks5", C4marks5);
                    rpt.SetParameterValue("C5marks5", C5marks5);
                    rpt.SetParameterValue("C6marks5", C6marks5);
                    rpt.SetParameterValue("P7marks5", P7marks5);
                    rpt.SetParameterValue("P8marks5", P8marks5);
                    rpt.SetParameterValue("F9marks5", F9marks5);
                    rpt.SetParameterValue("D1marks6", D1marks6);
                    rpt.SetParameterValue("D2marks6", D2marks6);
                    rpt.SetParameterValue("C3marks6", C3marks6);
                    rpt.SetParameterValue("C4marks6", C4marks6);
                    rpt.SetParameterValue("C5marks6", C5marks6);
                    rpt.SetParameterValue("C6marks6", C6marks6);
                    rpt.SetParameterValue("P7marks6", P7marks6);
                    rpt.SetParameterValue("P8marks6", P8marks6);
                    rpt.SetParameterValue("F9marks6", F9marks6);
                    rpt.SetParameterValue("D1marks7", D1marks7);
                    rpt.SetParameterValue("D2marks7", D2marks7);
                    rpt.SetParameterValue("C3marks7", C3marks7);
                    rpt.SetParameterValue("C4marks7", C4marks7);
                    rpt.SetParameterValue("C5marks7", C5marks7);
                    rpt.SetParameterValue("C6marks7", C6marks7);
                    rpt.SetParameterValue("P7marks7", P7marks7);
                    rpt.SetParameterValue("P8marks7", P8marks7);
                    rpt.SetParameterValue("F9marks7", F9marks7);
                    rpt.SetParameterValue("D1marksS4", D1marksS4);
                    rpt.SetParameterValue("D2marksS4", D2marksS4);
                    rpt.SetParameterValue("C3marksS4", C3marksS4);
                    rpt.SetParameterValue("C4marksS4", C4marksS4);
                    rpt.SetParameterValue("C5marksS4", C5marksS4);
                    rpt.SetParameterValue("C6marksS4", C6marksS4);
                    rpt.SetParameterValue("P7marksS4", P7marksS4);
                    rpt.SetParameterValue("P8marksS4", P8marksS4);
                    rpt.SetParameterValue("F9marksS4", F9marksS4);

                    rpt.SetParameterValue("D1marks5p", D1marks5p);
                    rpt.SetParameterValue("D2marks5p", D2marks5p);
                    rpt.SetParameterValue("C3marks5p", C3marks5p);
                    rpt.SetParameterValue("C4marks5p", C4marks5p);
                    rpt.SetParameterValue("C5marks5p", C5marks5p);
                    rpt.SetParameterValue("C6marks5p", C6marks5p);
                    rpt.SetParameterValue("P7marks5p", P7marks5p);
                    rpt.SetParameterValue("P8marks5p", P8marks5p);
                    rpt.SetParameterValue("F9marks5p", F9marks5p);
                    rpt.SetParameterValue("D1marks6p", D1marks6p);
                    rpt.SetParameterValue("D2marks6p", D2marks6p);
                    rpt.SetParameterValue("C3marks6p", C3marks6p);
                    rpt.SetParameterValue("C4marks6p", C4marks6p);
                    rpt.SetParameterValue("C5marks6p", C5marks6p);
                    rpt.SetParameterValue("C6marks6p", C6marks6p);
                    rpt.SetParameterValue("P7marks6p", P7marks6p);
                    rpt.SetParameterValue("P8marks6p", P8marks6p);
                    rpt.SetParameterValue("F9marks6p", F9marks6p);
                    rpt.SetParameterValue("D1marks7p", D1marks7p);
                    rpt.SetParameterValue("D2marks7p", D2marks7p);
                    rpt.SetParameterValue("C3marks7p", C3marks7p);
                    rpt.SetParameterValue("C4marks7p", C4marks7p);
                    rpt.SetParameterValue("C5marks7p", C5marks7p);
                    rpt.SetParameterValue("C6marks7p", C6marks7p);
                    rpt.SetParameterValue("P7marks7p", P7marks7p);
                    rpt.SetParameterValue("P8marks7p", P8marks7p);
                    rpt.SetParameterValue("F9marks7p", F9marks7p);
                    rpt.SetParameterValue("D1marksS4p", D1marksS4p);
                    rpt.SetParameterValue("D2marksS4p", D2marksS4p);
                    rpt.SetParameterValue("C3marksS4p", C3marksS4p);
                    rpt.SetParameterValue("C4marksS4p", C4marksS4p);
                    rpt.SetParameterValue("C5marksS4p", C5marksS4p);
                    rpt.SetParameterValue("C6marksS4p", C6marksS4p);
                    rpt.SetParameterValue("P7marksS4p", P7marksS4p);
                    rpt.SetParameterValue("P8marksS4p", P8marksS4p);
                    rpt.SetParameterValue("F9marksS4p", F9marksS4p);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    //rpt.SetParameterValue("picpath", "logo.jpg");
                    crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totalp6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "'  and Grade='A'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marks6i = Convert.ToDouble(D1marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);

                    if (totalp6i != 0)
                    {
                        Double D1marks6pi = Math.Round(((D1marks6i / totalp6i) * 100), 1);
                        D1marks6p = D1marks6pi.ToString();
                    }
                    else
                    {
                        D1marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "' and Grade='B'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marks6i = Convert.ToDouble(D2marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double D2marks6pi = Math.Round(((D2marks6i / totalp6i) * 100), 1);
                        D2marks6p = D2marks6pi.ToString();
                    }
                    else
                    {
                        D2marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "' and Grade='C'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marks6i = Convert.ToDouble(C3marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C3marks6pi = Math.Round(((C3marks6i / totalp6i) * 100), 1);
                        C3marks6p = C3marks6pi.ToString();
                    }
                    else
                    {
                        C3marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "' and Grade='D'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marks6i = Convert.ToDouble(C4marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C4marks6pi = Math.Round(((C4marks6i / totalp6i) * 100), 1);
                        C4marks6p = C4marks6pi.ToString();
                    }
                    else
                    {
                        C4marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "' and Grade='E'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marks6i = Convert.ToDouble(C5marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C5marks6pi = Math.Round(((C5marks6i / totalp6i) * 100), 1);
                        C5marks6p = C5marks6pi.ToString();
                    }
                    else
                    {
                        C5marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "'  and Grade='O'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marks6i = Convert.ToDouble(C6marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double C6marks6pi = Math.Round(((C6marks6i / totalp6i) * 100), 1);
                        C6marks6p = C6marks6pi.ToString();
                    }
                    else
                    {
                        C6marks6p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.6' and term='" + cmbSemester.Text + "' and Grade='F'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks6 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marks6i = Convert.ToDouble(P7marks6);
                    Double totalp6i = Convert.ToDouble(totalp6);
                    if (totalp6i != 0)
                    {
                        Double P7marks6pi = Math.Round(((P7marks6i / totalp6i) * 100), 1);
                        P7marks6p = P7marks6pi.ToString();
                    }
                    else
                    {
                        P7marks6p = "N/A";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
                //End of P.6


                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    totalp7 = cmd.ExecuteScalar().ToString();
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "'  and Grade='A'";
                    cmd = new SqlCommand(inquery3, con);
                    D1marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D1marks7i = Convert.ToDouble(D1marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double D1marks7pi = Math.Round(((D1marks7i / totalp7i) * 100), 1);
                        D1marks7p = D1marks7pi.ToString();
                    }
                    else
                    {
                        D1marks7p = "N/A";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "' and Grade='B'";
                    cmd = new SqlCommand(inquery3, con);
                    D2marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double D2marks7i = Convert.ToDouble(D2marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double D2marks7pi = Math.Round(((D2marks7i / totalp7i) * 100), 1);
                        D2marks7p = D2marks7pi.ToString();
                    }
                    else
                    {
                        D2marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "' and Grade='C'";
                    cmd = new SqlCommand(inquery3, con);
                    C3marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C3marks7i = Convert.ToDouble(C3marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C3marks7pi = Math.Round(((C3marks7i / totalp7i) * 100), 1);
                        C3marks7p = C3marks7pi.ToString();
                    }
                    else
                    {
                        C3marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "' and Grade='D'";
                    cmd = new SqlCommand(inquery3, con);
                    C4marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C4marks7i = Convert.ToDouble(C4marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C4marks7pi = Math.Round(((C4marks7i / totalp7i) * 100), 1);
                        C4marks7p = C4marks7pi.ToString();
                    }
                    else
                    {
                        C4marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "' and Grade='E'";
                    cmd = new SqlCommand(inquery3, con);
                    C5marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C5marks7i = Convert.ToDouble(C5marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C5marks7pi = Math.Round(((C5marks7i / totalp7i) * 100), 1);
                        C5marks7p = C5marks7pi.ToString();
                    }
                    else
                    {
                        C5marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "'  and Grade='O'";
                    cmd = new SqlCommand(inquery3, con);
                    C6marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double C6marks7i = Convert.ToDouble(C6marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double C6marks7pi = Math.Round(((C6marks7i / totalp7i) * 100), 1);
                        C6marks7p = C6marks7pi.ToString();
                    }
                    else
                    {
                        C6marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='S.5' and term='" + cmbSemester.Text + "' and Grade='F'";
                    cmd = new SqlCommand(inquery3, con);
                    P7marks7 = cmd.ExecuteScalar().ToString();
                    con.Close();
                    Double P7marks7i = Convert.ToDouble(P7marks7);
                    Double totalp7i = Convert.ToDouble(totalp7);
                    if (totalp7i != 0)
                    {
                        Double P7marks7pi = Math.Round(((P7marks7i / totalp7i) * 100), 1);
                        P7marks7p = P7marks7pi.ToString();
                    }
                    else
                    {
                        P7marks7p = "N/A";
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rpttermmarkssummaryA rpt = new rpttermmarkssummaryA(); //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    myConnection.Open();
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select  * from Purchases";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Purchases");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("year", Year);
                    rpt.SetParameterValue("term", Term);
                    rpt.SetParameterValue("D1marks6", D1marks6);
                    rpt.SetParameterValue("D2marks6", D2marks6);
                    rpt.SetParameterValue("C3marks6", C3marks6);
                    rpt.SetParameterValue("C4marks6", C4marks6);
                    rpt.SetParameterValue("C5marks6", C5marks6);
                    rpt.SetParameterValue("C6marks6", C6marks6);
                    rpt.SetParameterValue("P7marks6", P7marks6);
                    rpt.SetParameterValue("P8marks6", P8marks6);
                    rpt.SetParameterValue("F9marks6", F9marks6);
                    rpt.SetParameterValue("D1marks7", D1marks7);
                    rpt.SetParameterValue("D2marks7", D2marks7);
                    rpt.SetParameterValue("C3marks7", C3marks7);
                    rpt.SetParameterValue("C4marks7", C4marks7);
                    rpt.SetParameterValue("C5marks7", C5marks7);
                    rpt.SetParameterValue("C6marks7", C6marks7);
                    rpt.SetParameterValue("P7marks7", P7marks7);
                    rpt.SetParameterValue("P8marks7", P8marks7);
                    rpt.SetParameterValue("F9marks7", F9marks7);
                                      
                    rpt.SetParameterValue("D1marks6p", D1marks6p);
                    rpt.SetParameterValue("D2marks6p", D2marks6p);
                    rpt.SetParameterValue("C3marks6p", C3marks6p);
                    rpt.SetParameterValue("C4marks6p", C4marks6p);
                    rpt.SetParameterValue("C5marks6p", C5marks6p);
                    rpt.SetParameterValue("C6marks6p", C6marks6p);
                    rpt.SetParameterValue("P7marks6p", P7marks6p);
                    rpt.SetParameterValue("P8marks6p", P8marks6p);
                    rpt.SetParameterValue("F9marks6p", F9marks6p);
                    rpt.SetParameterValue("D1marks7p", D1marks7p);
                    rpt.SetParameterValue("D2marks7p", D2marks7p);
                    rpt.SetParameterValue("C3marks7p", C3marks7p);
                    rpt.SetParameterValue("C4marks7p", C4marks7p);
                    rpt.SetParameterValue("C5marks7p", C5marks7p);
                    rpt.SetParameterValue("C6marks7p", C6marks7p);
                    rpt.SetParameterValue("P7marks7p", P7marks7p);
                    rpt.SetParameterValue("P8marks7p", P8marks7p);
                    rpt.SetParameterValue("F9marks7p", F9marks7p);                  
                    crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
            
            }

        }

        private void Grades_Click(object sender, EventArgs e)
        {
            company();
            if (cmbSession.Text == "")
            {
                MessageBox.Show("Please select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSession.Focus();
                return;
            }
            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            Year = cmbSession.Text;
            Term = cmbSemester.Text;
            try
            {
                forclass = cmbCourse.Text + " Class";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                cmd = new SqlCommand(inquery3, con);
                totals = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Division='1'";
                cmd = new SqlCommand(inquery3, con);
                D1marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double D1marksi = Convert.ToDouble(D1marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double D1markspi = Math.Round(((D1marksi / totalsi) * 100), 1);
                    D1marksp = D1markspi.ToString();
                }
                else
                {
                    D1marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Division='2'";
                cmd = new SqlCommand(inquery3, con);
                D2marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double D2marksi = Convert.ToDouble(D2marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double D2markspi = Math.Round(((D2marksi / totalsi) * 100), 1);
                    D2marksp = D2markspi.ToString();
                }
                else
                {
                    D2marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'and Division='3'";
                cmd = new SqlCommand(inquery3, con);
                C3marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double C3marksi = Convert.ToDouble(C3marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double C3markspi = Math.Round(((C3marksi / totalsi) * 100), 1);
                    C3marksp = C3markspi.ToString();
                }
                else
                {
                    C3marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and term='" + cmbSemester.Text + "'and Division='4'";
                cmd = new SqlCommand(inquery3, con);
                C4marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double C4marksi = Convert.ToDouble(C4marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double C4markspi = Math.Round(((C4marksi / totalsi) * 100), 1);
                    C4marksp = C4markspi.ToString();
                }
                else
                {
                    C4marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'and Division='7'";
                cmd = new SqlCommand(inquery3, con);
                C5marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double C5marksi = Convert.ToDouble(C5marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double C5markspi = Math.Round(((C5marksi / totalsi) * 100), 1);
                    C5marksp = C5markspi.ToString();
                }
                else
                {
                    C5marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM Positions WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'and Division='9'";
                cmd = new SqlCommand(inquery3, con);
                C7marks = cmd.ExecuteScalar().ToString();
                con.Close();
                Double C7marksi = Convert.ToDouble(C7marks);
                Double totalsi = Convert.ToDouble(totals);
                if (totalsi != 0)
                {
                    Double C7markspi = Math.Round(((C7marksi / totalsi) * 100), 1);
                    C7marksp = C7markspi.ToString();
                }
                else
                {
                    C7marksp = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptclassgradessummary rpt = new rptclassgradessummary(); //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                cms_purchases myDS = new cms_purchases(); //The DataSet you created.
                myConnection = new SqlConnection(cs.DBConn);
                myConnection.Open();
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select  * from Purchases";
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Purchases");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("year", Year);
                rpt.SetParameterValue("term", Term);
                rpt.SetParameterValue("class", forclass);
                rpt.SetParameterValue("D1marks", D1marks);
                rpt.SetParameterValue("D2marks", D2marks);
                rpt.SetParameterValue("C3marks", C3marks);
                rpt.SetParameterValue("C4marks", C4marks);
                rpt.SetParameterValue("C5marks", C5marks);
                rpt.SetParameterValue("C7marks", C7marks);
                rpt.SetParameterValue("D1marksp", D1marksp);
                rpt.SetParameterValue("D2marksp", D2marksp);
                rpt.SetParameterValue("C3marksp", C3marksp);
                rpt.SetParameterValue("C4marksp", C4marksp);
                rpt.SetParameterValue("C5marksp", C5marksp);
                rpt.SetParameterValue("C7marksp", C7marksp);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer1.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }
       
       
    }
}

