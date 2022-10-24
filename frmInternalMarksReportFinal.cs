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
namespace College_Management_System
{
    public partial class frmInternalMarksReportFinal : Form
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
        public frmInternalMarksReportFinal()
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGetData_Click(object sender, EventArgs e)
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
                    company();
                    try
                    {
                        if (cmbCourse.Text == "S.4")
                        {
                            if (sets.Text == "")
                            {
                                MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                sets.Focus();
                                return;
                            }
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            OlevelResultsFinal rpt = new OlevelResultsFinal();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            finalresultdataset myDS = new finalresultdataset();
                            //The DataSet you created    
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select * from FinalResults where  CLASS= '" + cmbCourse.Text + "' and YEAR='" + cmbSession.Text + "'and TERM='" + cmbSemester.Text + "' and Sets='"+sets.Text+"' order by NAMES ASC  ";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "FinalResults");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                           // rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            crystalReportViewer1.ReportSource = rpt;
                        }
                        else
                        {
                            Cursor = Cursors.WaitCursor;
                            timer1.Enabled = true;
                            OlevelResultsFinal rpt = new OlevelResultsFinal();
                            //The report you created.
                            SqlConnection myConnection = default(SqlConnection);
                            SqlCommand MyCommand = new SqlCommand();
                            SqlDataAdapter myDA = new SqlDataAdapter();
                            finalresultdataset myDS = new finalresultdataset();
                            //The DataSet you created    
                            myConnection = new SqlConnection(cs.DBConn);
                            MyCommand.Connection = myConnection;
                            MyCommand.CommandText = "select * from FinalResults where  CLASS= '" + cmbCourse.Text + "' and YEAR='" + cmbSession.Text + "'and TERM='" + cmbSemester.Text + "' order by NAMES ASC  ";
                            MyCommand.CommandType = CommandType.Text;
                            myDA.SelectCommand = MyCommand;
                            myDA.Fill(myDS, "FinalResults");
                            rpt.SetDataSource(myDS);
                            rpt.SetParameterValue("companyname", companyname);
                            //rpt.SetParameterValue("companyemail", companyemail);
                            rpt.SetParameterValue("companycontact", companycontact);
                            //rpt.SetParameterValue("companyslogan", companyslogan);
                            rpt.SetParameterValue("companyaddress", companyaddress);
                            rpt.SetParameterValue("picpath", "logo.jpg");
                            crystalReportViewer1.ReportSource = rpt;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
        }    
    }
}
