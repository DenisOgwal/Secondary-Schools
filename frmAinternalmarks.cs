using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;

namespace College_Management_System
{
    public partial class frmAinternalmarks : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        string BarPrinter = null;
        string KitchenPrinter = null;
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;
        public frmAinternalmarks()
        {
            InitializeComponent();
        }

        public void AutocompleSession()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(year) from Leveladvanced ";
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

        private void frmAinternalmarks_Load(object sender, EventArgs e)
        {
            Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            AutocompleSession();
            cmbSession.Enabled = true;
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCourse.Items.Clear();
            cmbCourse.Text = "";
            cmbCourse.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(class) from Leveladvanced where year = '" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(level) from Leveladvanced where class = '" + cmbCourse.Text + "' and year='" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(term) from Leveladvanced where class = '" + cmbCourse.Text + "' and level = '" + cmbBranch.Text + "' and year='" + cmbSession.Text + "'";
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
            cmbSection.Items.Clear();
            cmbSection.Text = "";
            cmbSection.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(stream) from Leveladvanced where class = '" + cmbCourse.Text + "' and level= '" + cmbBranch.Text + "' and term='" + cmbSemester.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbSection.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {


            prints.Text = "";
            prints.Enabled = true;

        }
        private void Reset()
        {
            cmbCourse.Text = "";
            cmbBranch.Text = "";
            cmbBranch.Enabled = false;
            cmbSemester.Text = "";
            cmbSemester.Enabled = false;
            cmbSession.Text = "";
            cmbSession.Enabled = true;
            cmbCourse.Enabled = false;
            cmbSection.Text = "";
            cmbSection.Enabled = false;
            cmbstudentno.Enabled = false;
            crystalReportViewer1.ReportSource = null;
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
                    BarPrinter = "no printer";
                    KitchenPrinter = "no printer";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbCourse.Text = "";
            cmbBranch.Text = "";
            cmbBranch.Enabled = false;
            cmbSemester.Text = "";
            cmbSemester.Enabled = false;
            cmbSession.Text = "";
            cmbSession.Enabled = true;
            cmbCourse.Enabled = false;
            cmbSection.Text = "";
            cmbSection.Enabled = false;
            cmbstudentno.Text = "";
            cmbstudentno.Enabled = false;
            crystalReportViewer1.ReportSource = null;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select BeginsOn,EndsOn from NextTerm  order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label11.Text = rdr.GetString(0);
                    label12.Text = rdr.GetString(1);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (prints.Text == "By Student No.")
            {
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
                if (cmbBranch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }

                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                if (cmbstudentno.Text == "")
                {
                    MessageBox.Show("Please select student number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbstudentno.Focus();
                    return;
                }
                if (sets.Text == "")
                {
                    MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sets.Focus();
                    return;
                }
                company();
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    ALevelReport rpt = new ALevelReport();
                    //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet();
                    //Alevelresults myDS = new Alevelresults();
                    //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select Grade,Points,leveladvancedGrade.subjectname,LeveladvancedFinale.Average,LeveladvancedFinale.Paper,LeveladvancedFinale.Aggregate,LeveladvancedFinale.subjectcode,leveladvancedGrade.Photo,Initials,Student.Student_name,Enrollment_No,ScholarNo,Session,Course,Branch,Student.Term from Student,LeveladvancedFinale,LeveladvancedGrade where Student.ScholarNo=LeveladvancedFinale.studentno and LeveladvancedGrade.studentno=Student.ScholarNo and Student.Course=LeveladvancedFinale.class and LeveladvancedFinale.class=LeveladvancedGrade.class and LeveladvancedFinale.term =LeveladvancedGrade.term and LeveladvancedGrade.year =LeveladvancedFinale.year and LeveladvancedGrade.subjectname=LeveladvancedFinale.Subjectname and  Student.ScholarNo = '" + cmbstudentno.Text + "' and LeveladvancedGrade.class= '" + cmbCourse.Text + "'and LeveladvancedGrade.year='" + cmbSession.Text + "'  and LeveladvancedGrade.term = '" + cmbSemester.Text + "' and LeveladvancedFinale.class= '" + cmbCourse.Text + "'and LeveladvancedFinale.year='" + cmbSession.Text + "'  and LeveladvancedFinale.term = '" + cmbSemester.Text + "' and LeveladvancedFinale.Sets = '" + sets.Text + "'";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Student");
                    myDA.Fill(myDS, "LeveladvancedGrade");
                    myDA.Fill(myDS, "LeveladvancedFinale");
                    //MessageBox.Show(myDS.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /*
                                    myConnection = new SqlConnection(cs.DBConn);
                                    MyCommand.Connection = myConnection;
                                    MyCommand.CommandText = "select LeveladvancedFinale.Class,leveladvancedGrade.Grade,leveladvancedGrade.Points,leveladvancedGrade.subjectname,LeveladvancedFinale.Average,LeveladvancedFinale.Paper,LeveladvancedFinale.Aggregate,LeveladvancedFinale.subjectcode,leveladvancedGrade.Photo,LeveladvancedFinale.year,LeveladvancedFinale.term,LeveladvancedFinale.level,LeveladvancedFinale.stream,Initials,Student_name,Enrollment_No,ScholarNo from Student,LeveladvancedFinale,LeveladvancedGrade where Student.ScholarNo=LeveladvancedFinale.studentno and LeveladvancedGrade.studentno=Student.ScholarNo and Student.Course=LeveladvancedFinale.class and LeveladvancedFinale.term =LeveladvancedGrade.term and LeveladvancedGrade.year =LeveladvancedFinale.year and  Student.ScholarNo = '" + cmbstudentno.Text + "' and LeveladvancedGrade.class= '" + cmbCourse.Text + "'and LeveladvancedGrade.year='" + cmbSession.Text + "'  and LeveladvancedGrade.term = '" + cmbSemester.Text + "' and LeveladvancedGrade.studentno='" + cmbstudentno.Text + "'";
                                    MyCommand.CommandType = CommandType.Text;
                                    myDA.SelectCommand = MyCommand;
                                    myDA.Fill(myDS, "Student");
                                    myDA.Fill(myDS, "LeveladvancedGrade");
                                    myDA.Fill(myDS, "LeveladvancedFinale");*/
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("set", sets.Text);
                    rpt.SetParameterValue("beginson", label11.Text);
                    rpt.SetParameterValue("endson", label12.Text);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    rpt.SetParameterValue("picpath", "logo.jpg");
                    crystalReportViewer1.ReportSource = rpt;
                    myConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

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
                if (cmbBranch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }

                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select Distinct rtrim(studentno) from LeveladvancedGrade where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read() == true)
                    {
                        String studentno = rdr[0].ToString();
                        try
                        {
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            ALevelReport rpt = new ALevelReport();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            DataSet myDS = new DataSet();
                            //The DataSet you created.
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select LeveladvancedFinale.class,Leveladvanced.studentno,Grade,Points,Leveladvanced.subjectname,Average,Leveladvanced.Paper,Aggregate,Leveladvanced.subjectcode,Studentname,ExamName,marks,Photo,Leveladvanced.year,Leveladvanced.term,Leveladvanced.level,Leveladvanced.stream,enrollmentnumber,Initials from Leveladvanced,LeveladvancedFinale,LeveladvancedGrade where Leveladvanced.studentno=LeveladvancedFinale.studentno and LeveladvancedFinale.studentno=LeveladvancedGrade.studentno and LeveladvancedFinale.Paper=Leveladvanced.Paper and LeveladvancedFinale.class= '" + cmbCourse.Text + "'and LeveladvancedFinale.level='" + cmbBranch.Text + "' and LeveladvancedFinale.year='" + cmbSession.Text + "' and LeveladvancedFinale.term = '" + cmbSemester.Text + "' and Leveladvanced.studentno = '" + studentno + "' and LeveladvancedFinale.Sets = '" + sets.Text + "'";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "Leveladvanced");
                            myDA.Fill(myDS, "LeveladvancedFinale");
                            myDA.Fill(myDS, "LeveladvancedGrade");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("set", sets.Text);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                            rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            //rpt.SetParameterValue("beginson", label11.Text);
                            //rpt.SetParameterValue("endson", label12.Text);
                            crystalReportViewer1.ReportSource = rpt;
                            rpt.Refresh();
                            rpt.PrintToPrinter(1, true, 1, 2);
                            myConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmAinternalmarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.UserType.Text = label6.Text;
            frm.User.Text = label7.Text;
            frm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void prints_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void prints_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (prints.Text == "All")
            {
                cmbstudentno.Items.Clear();
                cmbstudentno.Text = "";
                cmbstudentno.Enabled = false;
            }
            else if (prints.Text == "By Student No.")
            {
                cmbstudentno.Items.Clear();
                cmbstudentno.Text = "";
                cmbstudentno.Enabled = true;
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select distinct RTRIM(studentno) from Leveladvanced where stream = '" + cmbSection.Text + "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        cmbstudentno.Items.Add(rdr[0]);
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select BeginsOn,EndsOn from NextTerm  order by ID Desc";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    label11.Text = rdr.GetString(0);
                    label12.Text = rdr.GetString(1);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (prints.Text == "By Student No.")
            {
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
                if (cmbBranch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }

                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                if (cmbstudentno.Text == "")
                {
                    MessageBox.Show("Please select student number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbstudentno.Focus();
                    return;
                }
                if (sets.Text == "")
                {
                    MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    sets.Focus();
                    return;
                }
                company();
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    ALevelReport rpt = new ALevelReport();
                    //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet();
                    //Alevelresults myDS = new Alevelresults();
                    //The DataSet you created.
                    myConnection = new SqlConnection(cs.DBConn);
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select Grade,Points,leveladvancedGrade.subjectname,LeveladvancedFinale.Average,LeveladvancedFinale.Paper,LeveladvancedFinale.Aggregate,LeveladvancedFinale.subjectcode,leveladvancedGrade.Photo,Initials,Student.Student_name,Enrollment_No,ScholarNo,Session,Course,Branch,Student.Term from Student,LeveladvancedFinale,LeveladvancedGrade where Student.ScholarNo=LeveladvancedFinale.studentno and LeveladvancedGrade.studentno=Student.ScholarNo and Student.Course=LeveladvancedFinale.class and LeveladvancedFinale.class=LeveladvancedGrade.class and LeveladvancedFinale.term =LeveladvancedGrade.term and LeveladvancedGrade.year =LeveladvancedFinale.year and LeveladvancedGrade.subjectname=LeveladvancedFinale.Subjectname and  Student.ScholarNo = '" + cmbstudentno.Text + "' and LeveladvancedGrade.class= '" + cmbCourse.Text + "'and LeveladvancedGrade.year='" + cmbSession.Text + "'  and LeveladvancedGrade.term = '" + cmbSemester.Text + "' and LeveladvancedFinale.class= '" + cmbCourse.Text + "'and LeveladvancedFinale.year='" + cmbSession.Text + "'  and LeveladvancedFinale.term = '" + cmbSemester.Text + "' and LeveladvancedFinale.Sets = '" + sets.Text + "'";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "Student");
                    myDA.Fill(myDS, "LeveladvancedGrade");
                    myDA.Fill(myDS, "LeveladvancedFinale");
                    //MessageBox.Show(myDS.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /*
                                    myConnection = new SqlConnection(cs.DBConn);
                                    MyCommand.Connection = myConnection;
                                    MyCommand.CommandText = "select LeveladvancedFinale.Class,leveladvancedGrade.Grade,leveladvancedGrade.Points,leveladvancedGrade.subjectname,LeveladvancedFinale.Average,LeveladvancedFinale.Paper,LeveladvancedFinale.Aggregate,LeveladvancedFinale.subjectcode,leveladvancedGrade.Photo,LeveladvancedFinale.year,LeveladvancedFinale.term,LeveladvancedFinale.level,LeveladvancedFinale.stream,Initials,Student_name,Enrollment_No,ScholarNo from Student,LeveladvancedFinale,LeveladvancedGrade where Student.ScholarNo=LeveladvancedFinale.studentno and LeveladvancedGrade.studentno=Student.ScholarNo and Student.Course=LeveladvancedFinale.class and LeveladvancedFinale.term =LeveladvancedGrade.term and LeveladvancedGrade.year =LeveladvancedFinale.year and  Student.ScholarNo = '" + cmbstudentno.Text + "' and LeveladvancedGrade.class= '" + cmbCourse.Text + "'and LeveladvancedGrade.year='" + cmbSession.Text + "'  and LeveladvancedGrade.term = '" + cmbSemester.Text + "' and LeveladvancedGrade.studentno='" + cmbstudentno.Text + "'";
                                    MyCommand.CommandType = CommandType.Text;
                                    myDA.SelectCommand = MyCommand;
                                    myDA.Fill(myDS, "Student");
                                    myDA.Fill(myDS, "LeveladvancedGrade");
                                    myDA.Fill(myDS, "LeveladvancedFinale");*/
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("set", sets.Text);
                    rpt.SetParameterValue("beginson", label11.Text);
                    rpt.SetParameterValue("endson", label12.Text);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    rpt.SetParameterValue("picpath", "logo.jpg");
                    crystalReportViewer1.ReportSource = rpt;
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    string userName = Environment.UserName;
                    String studentno = cmbstudentno.Text;
                    //MessageBox.Show(studentno,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    String[] breakApart = studentno.Split('/');
                    string finalstring = breakApart[0] + "" + breakApart[1];
                    String filePath3 = @"\Users\" + userName + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "\\" + finalstring + ".pdf";

                    CrDiskFileDestinationOptions.DiskFileName = filePath3;
                    CrExportOptions = rpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    rpt.Export();
                    rpt.Close();
                    rpt.Dispose();
                    crystalReportViewer1.ReportSource = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    string userNames = Environment.UserName;
                    String filePath4 = @"\Users\" + userNames + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "";
                    MessageBox.Show("Successful exported to PDF\n check Directory " + filePath4, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

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
                if (cmbBranch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }

                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select Distinct rtrim(studentno) from LeveladvancedGrade where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read() == true)
                    {
                        String studentno = rdr[0].ToString();
                        try
                        {
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            ALevelReport rpt = new ALevelReport();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            DataSet myDS = new DataSet();
                            //The DataSet you created.
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select LeveladvancedFinale.class,Leveladvanced.studentno,Grade,Points,Leveladvanced.subjectname,Average,Leveladvanced.Paper,Aggregate,Leveladvanced.subjectcode,Studentname,ExamName,marks,Photo,Leveladvanced.year,Leveladvanced.term,Leveladvanced.level,Leveladvanced.stream,enrollmentnumber,Initials from Leveladvanced,LeveladvancedFinale,LeveladvancedGrade where Leveladvanced.studentno=LeveladvancedFinale.studentno and LeveladvancedFinale.studentno=LeveladvancedGrade.studentno and LeveladvancedFinale.Paper=Leveladvanced.Paper and LeveladvancedFinale.class= '" + cmbCourse.Text + "'and LeveladvancedFinale.level='" + cmbBranch.Text + "' and LeveladvancedFinale.year='" + cmbSession.Text + "' and LeveladvancedFinale.term = '" + cmbSemester.Text + "' and Leveladvanced.studentno = '" + studentno + "' and LeveladvancedFinale.Sets = '" + sets.Text + "'";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "Leveladvanced");
                            myDA.Fill(myDS, "LeveladvancedFinale");
                            myDA.Fill(myDS, "LeveladvancedGrade");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("set", sets.Text);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                            rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            //rpt.SetParameterValue("beginson", label11.Text);
                            //rpt.SetParameterValue("endson", label12.Text);
                            crystalReportViewer1.ReportSource = rpt;
                            ExportOptions CrExportOptions;
                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                            string userName = Environment.UserName;
                            //MessageBox.Show(studentno,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            String[] breakApart = studentno.Split('/');
                            string finalstring = breakApart[0] + "" + breakApart[1];
                            String filePath3 = @"\Users\" + userName + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "\\" + finalstring + ".pdf";

                            CrDiskFileDestinationOptions.DiskFileName = filePath3;
                            CrExportOptions = rpt.ExportOptions;
                            {
                                CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                                CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                                CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                                CrExportOptions.FormatOptions = CrFormatTypeOptions;
                            }
                            rpt.Export();
                            rpt.Close();
                            rpt.Dispose();
                            crystalReportViewer1.ReportSource = null;
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    con.Close();
                    string userNames = Environment.UserName;
                    String filePath4 = @"\Users\" + userNames + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "";
                    MessageBox.Show("Successful exported to PDF\n check Directory " + filePath4, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbstudentno_Click(object sender, EventArgs e)
        {
           /* frmClientDetails frm = new frmClientDetails();
            frm.ShowDialog();
            cmbstudentno.Text = frm.clientnames.Text;*/
        }

        private void cmbstudentno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
