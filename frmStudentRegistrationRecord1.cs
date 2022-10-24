using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmStudentRegistrationRecord1 : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

       

        public frmStudentRegistrationRecord1()
        {
            InitializeComponent();
        }
        private void AutocompleteCourse()
        {

            try
            {
                
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Course) FROM StudentRegistration", CN);
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
        private void AutocompleteSession()
        {

            try
            {
               
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Session) FROM StudentRegistration", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Session.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Session.Items.Add(drow[0].ToString());

                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteStudentName()
        {

            try
            {               
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Student_Name) FROM StudentRegistration", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                StudentName.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    StudentName.Items.Add(drow[0].ToString());

                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmRegistrationRecord_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            AutocompleteSession();
            //AutocompleteStudentName();
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
                string ct = "select distinct RTRIM(branch) from StudentRegistration where course= '" + Course.Text + "'";
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

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Course.Text == "")
                {
                    MessageBox.Show("Please select course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select branch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                if (Session.Text == "")
                {
                    MessageBox.Show("Please select session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Session.Focus();
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Percentage],RTRIM(PG_University)[Institution]  from studentRegistration where  Course= '" + Course.Text + "'and branch='" + Branch.Text + "'and Session='" + Session.Text + "'order by DateOfAdmission", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView1.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Course.Text = "";
            Branch.Text = "";
            Session.Text = "";
            Branch.Enabled = false;
            Session.Enabled = false;
            Course.Focus();
        }

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                xlApp.Columns[3].Cells.NumberFormat = "@";
                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Percentage],RTRIM(PG_University)[Institution]  from studentRegistration where  DateOfAdmission between @date1 and @date2 order by DateOfAdmission", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " DateOfAdmission").Value = DateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " DateOfAdmission").Value = DateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView2.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            DateFrom.Text = DateTime.Today.ToString();
            DateTo.Text = DateTime.Today.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                xlApp.Columns[3].Cells.NumberFormat = "@";
                rowsTotal = dataGridView2.RowCount - 1;
                colsTotal = dataGridView2.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView2.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView2.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void StudentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Percentage],RTRIM(PG_University)[Institution] from studentRegistration where  Student_Name= '" + StudentName.Text + "'order by DateOfAdmission", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView3.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView3.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                xlApp.Columns[3].Cells.NumberFormat = "@";
                rowsTotal = dataGridView3.RowCount - 1;
                colsTotal = dataGridView3.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView3.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView3.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            Course.Text = "";
            Branch.Text = "";
            Session.Text = "";
            DateFrom.Text = DateTime.Today.ToString();
            DateTo.Text = DateTime.Today.ToString();
            StudentName.Text = "";
         
            Branch.Enabled = false;
            Session.Enabled = false;
        }

        private void Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Items.Clear();
            Session.Text = "";
            Session.Enabled = true;

            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();


                string ct = "select distinct RTRIM(Session) from StudentRegistration where Branch= '" + Branch.Text + "' and Course= '" + Course.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Session.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmStudentRegistration frm = new frmStudentRegistration();
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;   
                frm.StudentName.Text = dr.Cells[0].Value.ToString();
                frm.AdmissionNo.Text = dr.Cells[1].Value.ToString();
                frm.DateOfAdmission.Text = dr.Cells[2].Value.ToString();
                frm.FatherName.Text = dr.Cells[3].Value.ToString();
                frm.MotherName.Text = dr.Cells[4].Value.ToString();
                frm.Gender.Text = dr.Cells[5].Value.ToString();
                frm.DOB.Text = dr.Cells[6].Value.ToString();
                frm.Category.Text = dr.Cells[7].Value.ToString();
                frm.Religion.Text = dr.Cells[8].Value.ToString();
                frm.Session.Text = dr.Cells[9].Value.ToString();
                frm.Address.Text = dr.Cells[10].Value.ToString();
                frm.ContactNo.Text = dr.Cells[11].Value.ToString();
                frm.Email.Text = dr.Cells[12].Value.ToString();
                frm.Course.Text = dr.Cells[13].Value.ToString();
                frm.Branch.Text = dr.Cells[14].Value.ToString();
                frm.DocumentSubmitted.Text = dr.Cells[15].Value.ToString();
                frm.Nationality.Text = dr.Cells[16].Value.ToString();
                frm.GuardianName.Text = dr.Cells[17].Value.ToString();
                frm.GuardianAddress.Text = dr.Cells[18].Value.ToString();
                frm.GuardianContactNo.Text = dr.Cells[19].Value.ToString();
                frm.HS.Text = dr.Cells[20].Value.ToString();
                frm.HSYOP.Text = dr.Cells[21].Value.ToString();
                frm.HSPercentage.Text = dr.Cells[22].Value.ToString();
                frm.HSBoard.Text = dr.Cells[23].Value.ToString();
                frm.HSS.Text = dr.Cells[24].Value.ToString();
                frm.HSSYOP.Text = dr.Cells[25].Value.ToString();
                frm.HSSPercentage.Text = dr.Cells[26].Value.ToString();
                frm.HSSBoard.Text = dr.Cells[27].Value.ToString();
                frm.PG.Text = dr.Cells[28].Value.ToString();
                frm.PGYOP.Text = dr.Cells[29].Value.ToString();
                frm.PGpercentage.Text = dr.Cells[30].Value.ToString();
                frm.PGUniy.Text = dr.Cells[31].Value.ToString();
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;
                frm.Branch.Enabled = false;
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView3.SelectedRows[0];
                this.Hide();
                frmStudentRegistration frm = new frmStudentRegistration();
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;   
                frm.StudentName.Text = dr.Cells[0].Value.ToString();
                frm.AdmissionNo.Text = dr.Cells[1].Value.ToString();
                frm.DateOfAdmission.Text = dr.Cells[2].Value.ToString();
                frm.FatherName.Text = dr.Cells[3].Value.ToString();
                frm.MotherName.Text = dr.Cells[4].Value.ToString();
                frm.Gender.Text = dr.Cells[5].Value.ToString();
                frm.DOB.Text = dr.Cells[6].Value.ToString();
                frm.Category.Text = dr.Cells[7].Value.ToString();
                frm.Religion.Text = dr.Cells[8].Value.ToString();
                frm.Session.Text = dr.Cells[9].Value.ToString();
                frm.Address.Text = dr.Cells[10].Value.ToString();
                frm.ContactNo.Text = dr.Cells[11].Value.ToString();
                frm.Email.Text = dr.Cells[12].Value.ToString();
                frm.Course.Text = dr.Cells[13].Value.ToString();
                frm.Branch.Text = dr.Cells[14].Value.ToString();
                frm.DocumentSubmitted.Text = dr.Cells[15].Value.ToString();
                frm.Nationality.Text = dr.Cells[16].Value.ToString();
                frm.GuardianName.Text = dr.Cells[17].Value.ToString();
                frm.GuardianAddress.Text = dr.Cells[18].Value.ToString();
                frm.GuardianContactNo.Text = dr.Cells[19].Value.ToString();
                frm.HS.Text = dr.Cells[20].Value.ToString();
                frm.HSYOP.Text = dr.Cells[21].Value.ToString();
                frm.HSPercentage.Text = dr.Cells[22].Value.ToString();
                frm.HSBoard.Text = dr.Cells[23].Value.ToString();
                frm.HSS.Text = dr.Cells[24].Value.ToString();
                frm.HSSYOP.Text = dr.Cells[25].Value.ToString();
                frm.HSSPercentage.Text = dr.Cells[26].Value.ToString();
                frm.HSSBoard.Text = dr.Cells[27].Value.ToString();
                frm.PG.Text = dr.Cells[28].Value.ToString();
                frm.PGYOP.Text = dr.Cells[29].Value.ToString();
                frm.PGpercentage.Text = dr.Cells[30].Value.ToString();
                frm.PGUniy.Text = dr.Cells[31].Value.ToString();
                frm.Branch.Enabled = false;
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {             
                DataGridViewRow dr = dataGridView2.SelectedRows[0];
                this.Hide();
                frmStudentRegistration frm = new frmStudentRegistration();
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;   
                frm.StudentName.Text = dr.Cells[0].Value.ToString();
                frm.AdmissionNo.Text = dr.Cells[1].Value.ToString();
                frm.DateOfAdmission.Text = dr.Cells[2].Value.ToString();
                frm.FatherName.Text = dr.Cells[3].Value.ToString();
                frm.MotherName.Text = dr.Cells[4].Value.ToString();
                frm.Gender.Text = dr.Cells[5].Value.ToString();
                frm.DOB.Text = dr.Cells[6].Value.ToString();
                frm.Category.Text = dr.Cells[7].Value.ToString();
                frm.Religion.Text = dr.Cells[8].Value.ToString();
                frm.Session.Text = dr.Cells[9].Value.ToString();
                frm.Address.Text = dr.Cells[10].Value.ToString();
                frm.ContactNo.Text = dr.Cells[11].Value.ToString();
                frm.Email.Text = dr.Cells[12].Value.ToString();
                frm.Course.Text = dr.Cells[13].Value.ToString();
                frm.Branch.Text = dr.Cells[14].Value.ToString();
                frm.DocumentSubmitted.Text = dr.Cells[15].Value.ToString();
                frm.Nationality.Text = dr.Cells[16].Value.ToString();
                frm.GuardianName.Text = dr.Cells[17].Value.ToString();
                frm.GuardianAddress.Text = dr.Cells[18].Value.ToString();
                frm.GuardianContactNo.Text = dr.Cells[19].Value.ToString();
                frm.HS.Text = dr.Cells[20].Value.ToString();
                frm.HSYOP.Text = dr.Cells[21].Value.ToString();
                frm.HSPercentage.Text = dr.Cells[22].Value.ToString();
                frm.HSBoard.Text = dr.Cells[23].Value.ToString();
                frm.HSS.Text = dr.Cells[24].Value.ToString();
                frm.HSSYOP.Text = dr.Cells[25].Value.ToString();
                frm.HSSPercentage.Text = dr.Cells[26].Value.ToString();
                frm.HSSBoard.Text = dr.Cells[27].Value.ToString();   
                frm.PG.Text = dr.Cells[28].Value.ToString();
                frm.PGYOP.Text = dr.Cells[29].Value.ToString();
                frm.PGpercentage.Text = dr.Cells[30].Value.ToString();
                frm.PGUniy.Text = dr.Cells[31].Value.ToString();
                frm.Branch.Enabled = false;
                frm.label1.Text = label5.Text;
                frm.label8.Text = label8.Text;
                frm.ShowDialog();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Percentage],RTRIM(PG_University)[Institution] from studentRegistration where  Student_Name like'" + StudentName.Text + "%' order by DateOfAdmission", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView3.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            StudentName.Text = "";
            dataGridView3.DataSource = null;
        }

        private void frmStudentRegistrationRecord1_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmStudentRegistration frm = new frmStudentRegistration();
            this.Hide();
            frm.label1.Text = label5.Text;
            frm.label8.Text = label8.Text;
            frm.ShowDialog();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource=null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Grade], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[Grade], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Grade],RTRIM(PG_University)[Institution]  from studentRegistration where  Course= 'S.7' order by DateOfAdmission", con);

                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView1.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
            }catch(Exception Ex){

                MessageBox.Show(Ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (dataGridView1.RowCount < 1)
                {
                    MessageBox.Show("Sorry we only want to eport hearders, click on ngenerate first..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int rowsTotal = 0;
                int colsTotal = 0;
                int I = 0;
                int j = 0;
                int iC = 0;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                Excel.Application xlApp = new Excel.Application();
                try
                {
                    Excel.Workbook excelBook = xlApp.Workbooks.Add();
                    Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                    excelWorksheet.get_Range("A1", "A5").Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    xlApp.Visible = true;
                    rowsTotal = dataGridView1.RowCount - 1;
                    colsTotal = dataGridView1.Columns.Count - 1;
                    var _with1 = excelWorksheet;
                    _with1.Cells.Select();
                    _with1.Cells.Delete();
                    for (iC = 0; iC <= colsTotal; iC++)
                    {
                        _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                    }
                    for (I = 0; I <= rowsTotal - 1; I++)
                    {
                        for (j = 0; j <= colsTotal; j++)
                        {
                            _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                        }
                    }
                    _with1.Rows["1:1"].Font.FontStyle = "Bold";
                    _with1.Rows["1:1"].Font.Size = 12;
                    _with1.Cells.Columns.AutoFit();
                    _with1.Cells.Select();
                    _with1.Cells.EntireColumn.AutoFit();
                    _with1.Cells[1, 1].Select();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    //RELEASE ALLOACTED RESOURCES
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                    xlApp = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XLS files (*.xls, *.xlt)|*.xls;*.xlt|XLSX files (*.xlsx, *.xlsm, *.xltx, *.xltm)|*.xlsx;*.xlsm;*.xltx;*.xltm|ODS files (*.ods, *.ots)|*.ods;*.ots|CSV files (*.csv, *.tsv)|*.csv;*.tsv|HTML files (*.html, *.htm)|*.html;*.htm";
                openFileDialog.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.Data.OleDb.OleDbConnection MyConnection;
                    //System.Data.DataSet DtSet;
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();
                    System.Data.OleDb.OleDbDataAdapter MyCommand;
                    MyConnection = new System.Data.OleDb.OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + openFileDialog.FileName + "';Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';");
                    MyConnection.Open();
                    MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from  [Sheet1$]", MyConnection);
                    DataTable data = new DataTable();
                    MyCommand.Fill(data);
                    dataGridView1.DataSource = data;
                    dataGridView1.Columns[0].Width = 120;
                    dataGridView1.Columns[1].Width = 120;
                    dataGridView1.Columns[2].Width = 250;
                    dataGridView1.Columns[3].Width = 120;
                    MyConnection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        string AdmissionNo = null;
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
            AdmissionNo = "A-" + years + monthss + days + GetUniqueKey(7);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            
            try
            {

                
                           /* con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string ct = "select Admission_No from studentRegistration where Admission_No=@find";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "AdmissionNo"));
                            cmd.Parameters["@find"].Value = AdmissionNo;
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                MessageBox.Show("Admission No. Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                AdmissionNo = "";
                                if ((rdr != null))
                                {
                                    rdr.Close();
                                }
                                return;
                            }*/

                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "insert into studentRegistration(Student_name,Admission_No,DateOfAdmission,Fathers_Name,Gender,DOB,Address,Session,Contact_No,Email,Course,Branch,Submitted_Documents,Nationality,GuardianName,GuardianContactNo,GuardianAddress,High_School_name,HS_Year_of_passing,HS_Percentage,HS_Board,Higher_secondary_Name,H_year_of_passing,H_percentage,H_board,Post_graduation,PG_year_of_passing,PG_percentage,PG_university,mother_name,religion,category) VALUES (@d2,@d3,@d4,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27,@d32,@d33,@d34,@d35,@d36,@d37,@d38)";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Student_name"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "Admission_No"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 20, "DateOfAdmission"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Fathers_Name"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "Gender"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 15, "DOB"));
                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 50, "Address"));
                            cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 10, "Session"));
                            cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 10, "Contact_No"));
                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 30, "Email"));
                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 30, "Course"));
                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 30, "Branch"));
                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.VarChar, 250, "Submitted_Documents"));
                            cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 20, "Nationality"));
                            cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 30, "GuardianName"));
                            cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 10, "GuardianContactNo"));
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 50, "GuardianAddress"));
                            cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.NChar, 30, "High_School_name"));
                            cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.NChar, 10, "HS_Year_of_passing"));
                            cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.NChar, 10, "HS_Percentage"));
                            cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 30, "HS_Board"));
                            cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.NChar, 30, "Higher_secondary_Name"));
                            cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 10, "H_year_of_passing"));
                            cmd.Parameters.Add(new SqlParameter("@d26", System.Data.SqlDbType.NChar, 10, "H_percentage"));
                            cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 30, "H_board"));
                            cmd.Parameters.Add(new SqlParameter("@d32", System.Data.SqlDbType.NChar, 30, "Post_graduation"));
                            cmd.Parameters.Add(new SqlParameter("@d33", System.Data.SqlDbType.NChar, 10, "PG_year_of_passing"));
                            cmd.Parameters.Add(new SqlParameter("@d34", System.Data.SqlDbType.NChar, 10, "PG_percentage"));
                            cmd.Parameters.Add(new SqlParameter("@d35", System.Data.SqlDbType.NChar, 30, "PG_university"));
                            cmd.Parameters.Add(new SqlParameter("@d36", System.Data.SqlDbType.NChar, 30, "mother_name"));
                            cmd.Parameters.Add(new SqlParameter("@d37", System.Data.SqlDbType.NChar, 30, "religion"));
                            cmd.Parameters.Add(new SqlParameter("@d38", System.Data.SqlDbType.NChar, 15, "category"));
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                                    {
                                    }
                                    else
                                    {
                                        auto();
                                        string checkdate = DateTime.Today.ToShortDateString();
                                        DateTime dtc1 = DateTime.Parse(checkdate);
                                        string converteddatesc = dtc1.ToString("dd/MMM/yyyy");

                            cmd.Parameters["@d2"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d3"].Value = AdmissionNo;
                            cmd.Parameters["@d4"].Value = converteddatesc;
                            cmd.Parameters["@d6"].Value = row.Cells[1].Value;
                            cmd.Parameters["@d7"].Value = row.Cells[3].Value;
                            cmd.Parameters["@d8"].Value = row.Cells[4].Value;
                            cmd.Parameters["@d9"].Value = row.Cells[8].Value;
                            cmd.Parameters["@d10"].Value = row.Cells[7].Value;
                            cmd.Parameters["@d11"].Value = row.Cells[9].Value;
                            cmd.Parameters["@d12"].Value = row.Cells[10].Value;
                            cmd.Parameters["@d13"].Value = row.Cells[11].Value;
                            cmd.Parameters["@d14"].Value = row.Cells[12].Value;
                            cmd.Parameters["@d15"].Value = row.Cells[13].Value;
                            cmd.Parameters["@d16"].Value = row.Cells[14].Value;
                            cmd.Parameters["@d17"].Value = row.Cells[15].Value;
                            cmd.Parameters["@d18"].Value = row.Cells[17].Value;
                            cmd.Parameters["@d19"].Value = row.Cells[16].Value;
                            cmd.Parameters["@d20"].Value = row.Cells[18].Value;
                            cmd.Parameters["@d21"].Value = row.Cells[19].Value;
                            cmd.Parameters["@d22"].Value = row.Cells[20].Value;
                            cmd.Parameters["@d23"].Value = row.Cells[21].Value;
                            cmd.Parameters["@d24"].Value = row.Cells[22].Value;
                            cmd.Parameters["@d25"].Value = row.Cells[23].Value;
                            cmd.Parameters["@d26"].Value = row.Cells[24].Value;
                            cmd.Parameters["@d27"].Value = row.Cells[25].Value;
                            cmd.Parameters["@d32"].Value = row.Cells[26].Value;
                            cmd.Parameters["@d33"].Value = row.Cells[27].Value;
                            cmd.Parameters["@d34"].Value = row.Cells[28].Value;
                            cmd.Parameters["@d35"].Value = row.Cells[28].Value;
                            cmd.Parameters["@d36"].Value = row.Cells[2].Value;
                            cmd.Parameters["@d37"].Value = row.Cells[6].Value;
                            cmd.Parameters["@d38"].Value = row.Cells[5].Value;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Successfully Resgistered", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StudentName_Click(object sender, EventArgs e)
        {
            frmClientDetails2 frm = new frmClientDetails2();
            frm.ShowDialog();
            StudentName.Text = frm.Accountnames.Text;
        }

        private void StudentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Student_Name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion],RTRIM(Session)[Year], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board],RTRIM(Post_Graduation)[Other],RTRIM(PG_Year_Of_Passing)[Year Of Passing],RTRIM(PG_percentage)[Percentage],RTRIM(PG_University)[Institution] from studentRegistration where  Student_Name= '" + StudentName.Text + "'order by DateOfAdmission", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView3.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
