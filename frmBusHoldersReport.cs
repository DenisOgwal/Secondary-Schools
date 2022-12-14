using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmBusHoldersReport : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        string companyname = null;
        string companyemail = null;
        string companyaddress = null;
        string companycontact = null;
        string companyslogan = null;

        public frmBusHoldersReport()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Branch.Text = "";
            Branch.Enabled = false;
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
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
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Course.Text == "")
                {
                    MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                 TransportationFacility_DBDataSet myDS = new  TransportationFacility_DBDataSet();
                //The DataSet you created.

                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and StartingDate between @date1 and @date2 and Course= '" + Course.Text + "'and branch='" + Branch.Text + "' order by BusHolders.Student_name ";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "StartingDate").Value = Date_from.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "StartingDate").Value = Date_to.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
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

        private void ScholarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void cmbStudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                 TransportationFacility_DBDataSet  myDS = new  TransportationFacility_DBDataSet ();
                //The DataSet you created.
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and Student.Student_name= '" + cmbStudentName.Text + "' order by BusHolders.Student_name ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer3.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                 TransportationFacility_DBDataSet  myDS = new  TransportationFacility_DBDataSet ();
                //The DataSet you created.

                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and Student.Student_name like '" + txtStudentName.Text + "%' order by BusHolders.Student_name ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer3.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            cmbStudentName.Text = "";
            txtStudentName.Text = "";
            crystalReportViewer3.ReportSource = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ScholarNo.Text = "";
            crystalReportViewer2.ReportSource = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                 TransportationFacility_DBDataSet  myDS = new  TransportationFacility_DBDataSet ();
                //The DataSet you created.
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and StartingDate between @date1 and @date2 order by BusHolders.Student_name ";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, "StartingDate").Value = dateTimePicker1.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, "StartingDate").Value = dateTimePicker2.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer4.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            crystalReportViewer4.ReportSource = null;
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
        }

        private void cmbSourceLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                TransportationFacility_DBDataSet myDS = new TransportationFacility_DBDataSet();
                //The DataSet you created.
                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and SourceLocation= '" + cmbSourceLocation.Text + "' order by BusHolders.Student_name ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer5.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSourceLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                 TransportationFacility_DBDataSet  myDS = new  TransportationFacility_DBDataSet ();
                //The DataSet you created.

                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and SourceLocation like '" + txtSourceLocation.Text + "%' order by BusHolders.Student_name ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                crystalReportViewer5.ReportSource = rpt;
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtSourceLocation.Text = "";
            cmbSourceLocation.Text = "";
            crystalReportViewer5.ReportSource = null;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            txtSourceLocation.Text = "";
            cmbSourceLocation.Text = "";
            crystalReportViewer5.ReportSource = null;
            crystalReportViewer4.ReportSource = null;
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
            cmbStudentName.Text = "";
            txtStudentName.Text = "";
            crystalReportViewer3.ReportSource = null;
            ScholarNo.Text = "";
            crystalReportViewer2.ReportSource = null;
            Course.Text = "";
            Branch.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            crystalReportViewer1.ReportSource = null;
        }

        private void frmBusHoldersReport_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
           // AutocompleteScholarNo();
            AutocompleteStudentname();
            AutocompleteSourceLocation();
        }
        private void AutocompleteCourse()
        {

            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Course) FROM Student,BusHolders where Student.ScholarNo=BusHolders.ScholarNo", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteStudentname()
        {

            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(BusHolders.Student_name) FROM Student,BusHolders where Student.ScholarNo=BusHolders.ScholarNo", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbStudentName.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    cmbStudentName.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteSourceLocation()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SourceLocation) FROM BusHolders ", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbSourceLocation.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    cmbSourceLocation.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AutocompleteScholarNo()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(ScholarNo) FROM BusHolders", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ScholarNo.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ScholarNo.Items.Add(drow[0].ToString());
                }
                CN.Close();
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

        private void frmBusHoldersReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            frm.UserType.Text = label5.Text;
            frm.User.Text = label13.Text;
            frm.Show();
        }

        private void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch.Items.Clear();
            Branch.Text = "";
            Branch.Enabled = true;

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(branch) from Student where course= '" + Course.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Branch.Items.Add(rdr[0]);
                }
                con.Close();
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
                company();
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptBusHolders rpt = new rptBusHolders();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                TransportationFacility_DBDataSet myDS = new TransportationFacility_DBDataSet();
                //The DataSet you created.

                myConnection = new SqlConnection(cs.DBConn);
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from BusHolders,Student where Student.ScholarNo=BusHolders.ScholarNo and Student.ScholarNo= '" + ScholarNo.Text + "' order by BusHolders.Student_name ";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "BusHolders");
                myDA.Fill(myDS, "Student");
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("companyname", companyname);
                //rpt.SetParameterValue("companyemail", companyemail);
                rpt.SetParameterValue("companycontact", companycontact);
                rpt.SetParameterValue("companyslogan", companyslogan);
                rpt.SetParameterValue("companyaddress", companyaddress);
                //rpt.SetParameterValue("picpath", "logo.jpg");
                myConnection.Close();
                crystalReportViewer2.ReportSource = rpt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
