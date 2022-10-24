using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
namespace College_Management_System
{
    public partial class frmInternalMarksReport : Form
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
        public frmInternalMarksReport()
        {
            InitializeComponent();
        }
          public void AutocompleSession()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(year) from Results ";
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
                string ct = "select distinct RTRIM(level) from Results where class = '" + cmbCourse.Text + "' and year='" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(term) from Results where class = '" + cmbCourse.Text + "' and level = '" + cmbBranch.Text + "' and year='" + cmbSession.Text + "'";
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
                string ct = "select distinct RTRIM(stream) from Results where class = '" + cmbCourse.Text + "' and level= '" + cmbBranch.Text + "' and term='" + cmbSemester.Text + "'";
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
            if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2")
            {
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
                        rptStudent_reportO rpt = new rptStudent_reportO();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where  Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
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
                        string finalstring = breakApart[0]; //+ "" + breakApart[1];
                        String filePath3 = @"\Users\" + userName + "\\documents\\Dither Technologies\\Report Cards\\" + cmbSession.Text + "\\" + cmbCourse.Text.ToString().Trim() + "\\" + cmbSection.Text.ToString().Trim() + "\\" + cmbSemester.Text + "\\"+finalstring+".pdf";

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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "' and Stream='"+cmbSection.Text+"'";
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
                                rptStudent_reportO rpt = new rptStudent_reportO();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where  Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
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
                                string finalstring = breakApart[0]; //+ "" + breakApart[1];
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
            if (cmbCourse.Text == "S.3")
            {
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
                        rptStudent_reportO3 rpt = new rptStudent_reportO3();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where  Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
                        rpt.SetDataSource(myDS);
                        rpt.SetParameterValue("beginson", label11.Text);
                        rpt.SetParameterValue("endson", label12.Text);
                        rpt.SetParameterValue("companyname", companyname);
                        //rpt.SetParameterValue("companyemail", companyemail);
                        rpt.SetParameterValue("companycontact", companycontact);
                        //rpt.SetParameterValue("companyslogan", companyslogan);
                        rpt.SetParameterValue("companyaddress", companyaddress);
                        rpt.SetParameterValue("picpath", "logo.jpg");
                        //frm.crystalReportViewer1.ReportSource = rpt;
                        crystalReportViewer1.ReportSource = rpt;
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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "' and Stream='"+cmbSection.Text+"'";
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
                                rptStudent_reportO3 rpt = new rptStudent_reportO3();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
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
                                string finalstring = breakApart[0]; //+ "" + breakApart[1];
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

            else if (cmbCourse.Text == "S.4")
            {
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
                    try
                    {
                        company();
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        rptStudent_reportO2 rpt = new rptStudent_reportO2();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where  Results.Class=Positions.class and Results.Sets=Positions.Sets and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and Results.Sets = '" + sets.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
                        rpt.SetDataSource(myDS);
                        rpt.SetParameterValue("set", sets.Text);
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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
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
                                rptStudent_reportO2 rpt = new rptStudent_reportO2();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' and Positions.Sets='" + sets.Text + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
                                rpt.SetDataSource(myDS);
                                rpt.SetParameterValue("set", sets.Text);
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
            if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2")
            {
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
                        rptStudent_reportO rpt = new rptStudent_reportO();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
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
                                rptStudent_reportO rpt = new rptStudent_reportO();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
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
            } if (cmbCourse.Text == "S.3")
            {
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
                        rptStudent_reportO3 rpt = new rptStudent_reportO3();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where StudentRegistration.ScholarNo=Positions.studentno and Results.Class=Positions.class and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "'";
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
                                rptStudent_reportO3 rpt = new rptStudent_reportO3();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
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
                                //rpt.Refresh();
                                rpt.PrintToPrinter(1, true, 1, 2);
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
            
            else if(cmbCourse.Text=="S.4"){
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
                    try
                    {
                        company();
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        rptStudent_reportO2 rpt = new rptStudent_reportO2();
                        //The report you created.
                        SqlConnection myConnection = default(SqlConnection);
                        SqlCommand MyCommand = new SqlCommand();
                        SqlDataAdapter myDA = new SqlDataAdapter();
                        DataSet myDS = new DataSet();
                        //The DataSet you created    
                        myConnection = new SqlConnection(cs.DBConn);
                        MyCommand.Connection = myConnection;
                        MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where  Results.Class=Positions.class and Results.Sets=Positions.Sets and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "' and Results.level='" + cmbBranch.Text + "' and Results.year='" + cmbSession.Text + "' and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and Results.Sets = '" + sets.Text + "' and StudentRegistration.ScholarNo = '" + cmbstudentno.Text + "' order by aggregate ASC  ";
                        MyCommand.CommandType = CommandType.Text;
                        myDA.SelectCommand = MyCommand;
                        myDA.Fill(myDS, "Results");
                        myDA.Fill(myDS, "StudentRegistration");
                        myDA.Fill(myDS, "Positions");
                        rpt.SetDataSource(myDS);
                        rpt.SetParameterValue("set", sets.Text);
                        rpt.SetParameterValue("beginson", label11.Text);
                        rpt.SetParameterValue("endson", label12.Text);
                        rpt.SetParameterValue("companyname", companyname);
                        //rpt.SetParameterValue("companyemail", companyemail);
                        rpt.SetParameterValue("companycontact", companycontact);
                        //rpt.SetParameterValue("companyslogan", companyslogan);
                        rpt.SetParameterValue("companyaddress", companyaddress);
                        rpt.SetParameterValue("picpath", "logo.jpg");
                        crystalReportViewer1.ReportSource = rpt;
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
                        string sql = "select Distinct rtrim(studentno) from Positions where class = '" + cmbCourse.Text + "' and term= '" + cmbSemester.Text + "' and year= '" + cmbSession.Text + "' and Sets='"+sets.Text+"'";
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
                                rptStudent_reportO2 rpt = new rptStudent_reportO2();
                                //The report you created.
                                SqlConnection myConnection = default(SqlConnection);
                                SqlCommand MyCommand = new SqlCommand();
                                SqlDataAdapter myDA = new SqlDataAdapter();
                                DataSet myDS = new DataSet();
                                //The DataSet you created    
                                myConnection = new SqlConnection(cs.DBConn);
                                MyCommand.Connection = myConnection;
                                MyCommand.CommandText = "select * from StudentRegistration,Results,Positions where Results.Class=Positions.class and StudentRegistration.ScholarNo=Results.studentno and Results.studentno=Positions.studentno and  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "'and Positions.term='" + cmbSemester.Text + "' and Positions.year='" + cmbSession.Text + "' and Results.term = '" + cmbSemester.Text + "' and StudentRegistration.ScholarNo = '" + studentno + "' and Positions.Sets='" + sets.Text + "' order by aggregate ASC  ";
                                MyCommand.CommandType = CommandType.Text;
                                myDA.SelectCommand = MyCommand;
                                myDA.Fill(myDS, "Results");
                                myDA.Fill(myDS, "StudentRegistration");
                                myDA.Fill(myDS, "Positions");
                                rpt.SetDataSource(myDS);
                                rpt.SetParameterValue("set", sets.Text);
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
                                //rpt.Refresh();
                                rpt.PrintToPrinter(1, true, 1, 2);
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
                    string ct = "select distinct RTRIM(studentno) from Results where stream = '" + cmbSection.Text + "'";
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
