using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using System.IO;
namespace College_Management_System
{
    public partial class frmInternalMarksReportNew : Form
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
        public frmInternalMarksReportNew()
        {
            InitializeComponent();
        }
        public void AutocompleSession()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(year) from ResultsNew ";
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
        private void frmInternalMarksReport_Load(object sender, EventArgs e)
        {
            Left = Top = 0;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            AutocompleSession();
            cmbSession.Enabled = true;
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
                string ct = "select distinct RTRIM(level) from ResultsNew where class = '" + cmbCourse.Text + "' and year='" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(term) from ResultsNew where class = '" + cmbCourse.Text + "' and level = '" + cmbBranch.Text + "' and year='" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(stream) from ResultsNew where class = '" + cmbCourse.Text + "' and level= '" + cmbBranch.Text + "' and term='" + cmbSemester.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbSection.Items.Add(rdr[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string ct = "select distinct RTRIM(Class) from GradingSystem where Grading='New'";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
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
                else
                {
                    label11.Text = "NULL";
                    label12.Text = "NULL";
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                string userName = Environment.UserName;
                String filePath3 = @"\Users\" + userName + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "";
                DirectoryInfo di3 = Directory.CreateDirectory(filePath3);
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
                company();
                try
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rptStudent_reportO4 rpt = new rptStudent_reportO4();
                    //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet();
                    //The DataSet you created    
                    myConnection = new SqlConnection(cs.DBConn);
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select * from ResultsNew,StudentRegistration where StudentRegistration.ScholarNo=ResultsNew.studentno and class= '" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and year='" + cmbSession.Text + "' and term='" + cmbSemester.Text + "' and studentno = '" + cmbstudentno.Text + "' order by subjectname ASC  ";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "ResultsNew");
                    myDA.Fill(myDS, "StudentRegistration");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("beginson", label11.Text);
                    rpt.SetParameterValue("endson", label12.Text);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    //rpt.SetParameterValue("companyslogan", companyslogan);
                    rpt.SetParameterValue("companyaddress", companyaddress);
                    rpt.SetParameterValue("picpath", "logo.jpg");
                    crystalReportViewer1.ReportSource = rpt;
                    //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, cmbstudentno.Text.ToString());

                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                    string userName = Environment.UserName;
                    String studentno = cmbstudentno.Text;
                    //MessageBox.Show(studentno,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    String[] breakApart = studentno.Split('/');
                    string finalstring = breakApart[0];// + "" + breakApart[1];
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
                    string sql = "select Distinct rtrim(studentno) from ResultsNew where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read() == true)
                    {
                        String studentno = rdr[0].ToString();
                        try
                        {
                            company();
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            rptStudent_reportO4 rpt = new rptStudent_reportO4();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            DataSet myDS = new DataSet();
                            //The DataSet you created    
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select * from ResultsNew,StudentRegistration where StudentRegistration.ScholarNo=ResultsNew.studentno and class= '" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and year='" + cmbSession.Text + "' and term='" + cmbSemester.Text + "' and studentno = '" + studentno + "' order by subjectname ASC  ";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "ResultsNew");
                            myDA.Fill(myDS, "StudentRegistration");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("beginson", label11.Text);
                            rpt.SetParameterValue("endson", label12.Text);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                            //rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            crystalReportViewer1.ReportSource = rpt;
                            ExportOptions CrExportOptions;
                            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                            string userName = Environment.UserName;
                            //MessageBox.Show(studentno,"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            String[] breakApart = studentno.Split('/');
                            string finalstring = breakApart[0];// + "" + breakApart[1];
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
                try
                {
                    company();
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    rptStudent_reportO4 rpt = new rptStudent_reportO4();
                    //The report you created.
                    SqlConnection myConnection = default(SqlConnection);
                    SqlCommand MyCommand = new SqlCommand();
                    SqlDataAdapter myDA = new SqlDataAdapter();
                    DataSet myDS = new DataSet();
                    //The DataSet you created    
                    myConnection = new SqlConnection(cs.DBConn);
                    MyCommand.Connection = myConnection;
                    MyCommand.CommandText = "select * from ResultsNew,StudentRegistration where StudentRegistration.ScholarNo=ResultsNew.studentno and class= '" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and year='" + cmbSession.Text + "' and term='" + cmbSemester.Text + "' and studentno = '" + cmbstudentno.Text + "'  order by subjectname ASC";
                    MyCommand.CommandType = CommandType.Text;
                    myDA.SelectCommand = MyCommand;
                    myDA.Fill(myDS, "ResultsNew");
                    myDA.Fill(myDS, "StudentRegistration");
                    rpt.SetDataSource(myDS);
                    rpt.SetParameterValue("beginson", label11.Text);
                    rpt.SetParameterValue("endson", label12.Text);
                    rpt.SetParameterValue("companyname", companyname);
                    //rpt.SetParameterValue("companyemail", companyemail);
                    rpt.SetParameterValue("companycontact", companycontact);
                    //rpt.SetParameterValue("companyslogan", companyslogan);
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
                    string sql = "select Distinct rtrim(studentno) from ResultsNew where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read() == true)
                    {
                        String studentno = rdr[0].ToString();
                        try
                        {
                            company();
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            rptStudent_reportO4 rpt = new rptStudent_reportO4();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            DataSet myDS = new DataSet();
                            //The DataSet you created    
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select * from ResultsNew,StudentRegistration where StudentRegistration.ScholarNo=ResultsNew.studentno and class= '" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and year='" + cmbSession.Text + "' and term='" + cmbSemester.Text + "' and studentno = '" + studentno + "' order by subjectname ASC  ";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "ResultsNew");
                            myDA.Fill(myDS, "StudentRegistration");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("beginson", label11.Text);
                            rpt.SetParameterValue("endson", label12.Text);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                            //rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            crystalReportViewer1.ReportSource = rpt;
                            //rpt.Refresh();
                            rpt.PrintToPrinter(1, true, 1, 2);
                            myConnection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
        }

        private void frmInternalMarksReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.UserType.Text = label6.Text;
            frm.User.Text = label7.Text;
            frm.Show();
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            prints.Text = "";
            prints.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void cmbstudentno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void prints_SelectedIndexChanged(object sender, EventArgs e)
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
                    string ct = "select distinct RTRIM(studentno) from ResultsNew where stream = '" + cmbSection.Text + "'";
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
            else
            {
                cmbstudentno.Items.Clear();
                cmbstudentno.Text = "";
                cmbstudentno.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            cmbstudentno.Enabled = true;
            crystalReportViewer1.ReportSource = null;
        }
    }
}
