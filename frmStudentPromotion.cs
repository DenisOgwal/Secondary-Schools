using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmStudentPromotion : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();



        public frmStudentPromotion()
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Course) FROM Student", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());

                }
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Session) FROM Student", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Session.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Session.Items.Add(drow[0].ToString());
                    promoteyear.Items.Add(drow[0].ToString());
                    studentpromoyear.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AutocompleteStudentName3()
        {

            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Student_Name) FROM Student Where Course='" + studenpromoclass.Text + "' and Session='" + studentpromoyear.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                StudentName.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    StudentName.Items.Add(drow[0].ToString());
                }
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
                string ct = "select distinct RTRIM(Section) from Student where course= '" + Course.Text + "'";
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
                cmd = new SqlCommand("select RTRIM(ScholarNo)[Student No],RTRIM(Student_name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level],RTRIM(Session)[Year],RTRIM(Term)[Term],RTRIM(Types)[Student Type], RTRIM(Section)[Stream], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board]  from Student where  Course= '" + Course.Text + "'and Section='" + Branch.Text + "'and Session='" + Session.Text + "' and Term ='3rd' order by Student_name", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Student");
                dataGridView1.DataSource = myDataSet.Tables["Student"].DefaultView;
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
            if (fromclass.Text == "")
            {
                MessageBox.Show("Please select Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fromclass.Focus();
                return;
            }
            if (toclass.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toclass.Focus();
                return;
            }
            if (promoteyear.Text == "")
            {
                MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                promoteyear.Focus();
                return;
            }
            if (streams.Text == "")
            {
                MessageBox.Show("Please select Stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                streams.Focus();
                return;
            }
            string terms = null;
            if (toclass.Text == "2nd")
            {
                terms = "1st";
            }
            else if (toclass.Text == "3rd")
            {
                terms = "2nd";
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(ScholarNo)[Student No],RTRIM(Student_name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level],RTRIM(Session)[Year],RTRIM(Term)[Term],RTRIM(Types)[Student Type], RTRIM(Section)[Stream], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board]  from Student Where Course='" + fromclass.Text + "' and Term='" + terms + "' and Session='" + promoteyear.Text + "' and Section='" + streams.Text + "'  order by Student_name", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Student");
                dataGridView2.DataSource = myDataSet.Tables["Student"].DefaultView;
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
            if (studentpromoyear.Text == "")
            {
                MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                studentpromoyear.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(ScholarNo)[Student No],RTRIM(Student_name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level],RTRIM(Session)[Year],RTRIM(Term)[Term],RTRIM(Types)[Student Type], RTRIM(Section)[Stream], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board] from Student where  Student_Name= '" + StudentName.Text + "' and Term='3rd' and Session='" + studentpromoyear.Text + "' and Course='" + studenpromoclass.Text + "' order by Student_name", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Student");
                dataGridView3.DataSource = myDataSet.Tables["Student"].DefaultView;
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
                string ct = "select distinct RTRIM(Session) from Student where Section= '" + Branch.Text + "' and Course= '" + Course.Text + "'";
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
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            dataGridView1.Rows.Remove(dr);
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView2.SelectedRows[0];
            dataGridView2.Rows.Remove(dr);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (studentpromoyear.Text == "")
            {
                MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                studentpromoyear.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(ScholarNo)[Student No],RTRIM(Student_name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level],RTRIM(Session)[Year],RTRIM(Term)[Term],RTRIM(Types)[Student Type], RTRIM(Section)[Stream], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board] from Student where  ScholarNo like'" + textBox1.Text + "%' and Term='3rd' and Session='" + studentpromoyear.Text + "' and Course='" + studenpromoclass.Text + "' order by Student_name", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Student");
                dataGridView3.DataSource = myDataSet.Tables["Student"].DefaultView;
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
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.User.Text = label5.Text;
            frm.UserType.Text = label8.Text;
            frm.Show();
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
                dataGridView1.DataSource = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(ScholarNo)[Student No],RTRIM(Student_name)[Student Name], RTRIM(Admission_No)[Admission No.], RTRIM(DateOfAdmission)[Date Of Admission], RTRIM(Fathers_Name)[Father's Name],RTRIM(Mother_name)[Mother's Name], RTRIM(Gender)[Gender], RTRIM(DOB)[DOB],RTRIM(Category)[Category],RTRIM(Religion)[Religion], RTRIM(Address)[Address], RTRIM(Contact_No)[Contact No.], RTRIM(Email)[Email], RTRIM(Course)[Class], RTRIM(Branch)[Level],RTRIM(Session)[Year],RTRIM(Term)[Term],RTRIM(Types)[Student Type], RTRIM(Section)[Stream], RTRIM(Submitted_Documents)[Documents Submitted],RTRIM(Nationality)[Nationality],RTRIM(GuardianName)[GuardianName],RTRIM(GuardianAddress)[Guardian Address],RTRIM(GuardianContactNo)[Guardian Contact No.], RTRIM(High_School_name)[P.7 School], RTRIM(HS_Year_of_passing)[Year Of Passing], RTRIM(HS_Percentage)[Percentage], RTRIM(HS_Board)[Board], RTRIM(Higher_secondary_Name)[O'Level], RTRIM(H_year_of_passing)[Year Of Passing], RTRIM(H_percentage)[ Percentage], RTRIM(H_board)[Board]  from Student where  Course= 'S.7' order by Student_name", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Student");
                dataGridView1.DataSource = myDataSet.Tables["Student"].DefaultView;
            }
            catch (Exception Ex)
            {

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
            try
            {
                string yearsss = (Convert.ToInt32(Session.Text) + 1).ToString();
                string classes = null;
                if (Course.Text == "S.1") { classes = "S.2"; }
                else if (Course.Text == "S.2") { classes = "S.3"; }
                else if (Course.Text == "S.3") { classes = "S.4"; }
                else if (Course.Text == "S.4") { classes = "S.5"; }
                else if (Course.Text == "S.5") { classes = "S.6"; }
                else if (Course.Text == "S.6")
                {
                    MessageBox.Show("Can not promote S.6", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                        {
                        }
                        else
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string ct = "select ScholarNo from Student where Session='" + yearsss + "' and ScholarNo=@find and Term='1st' and Course='" + classes + "'";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "ScholarNo"));
                            cmd.Parameters["@find"].Value = row.Cells[0].Value.ToString();
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                con.Close();
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "insert into Student(ScholarNo,Student_name,Admission_No,DateOfAdmission,Fathers_Name,Mother_name,Gender,DOB,Category,Religion,Address,Contact_No,Email,Course,Branch,Session,Term,Types,Section,Submitted_Documents,Nationality,GuardianName,GuardianContactNo,GuardianAddress,High_School_name,HS_Year_of_passing,HS_Percentage,HS_Board,Higher_secondary_Name,H_year_of_passing,H_percentage,H_board) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27,@d28,@d29,@d30,@d31,@d32)";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Student_name"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "Admission_No"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 20, "DateOfAdmission"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 30, "Fathers_Name"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Mother_name"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "Gender"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 15, "DOB"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 15, "Category"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 30, "Religion"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 50, "Address"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 10, "Contact_No"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 30, "Email"));
                                cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 30, "Course"));
                                cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Branch"));
                                cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Session"));
                                cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Term"));
                                cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "Types"));
                                cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Section"));
                                cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.VarChar, 250, "Submitted_Documents"));
                                cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.NChar, 20, "Nationality"));
                                cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.NChar, 30, "GuardianName"));
                                cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "GuardianContactNo"));
                                cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.NChar, 50, "GuardianAddress"));
                                cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 30, "High_School_name"));
                                cmd.Parameters.Add(new SqlParameter("@d26", System.Data.SqlDbType.NChar, 10, "HS_Year_of_passing"));
                                cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 10, "HS_Percentage"));
                                cmd.Parameters.Add(new SqlParameter("@d28", System.Data.SqlDbType.NChar, 30, "HS_Board"));
                                cmd.Parameters.Add(new SqlParameter("@d29", System.Data.SqlDbType.NChar, 30, "Higher_secondary_Name"));
                                cmd.Parameters.Add(new SqlParameter("@d30", System.Data.SqlDbType.NChar, 10, "H_year_of_passing"));
                                cmd.Parameters.Add(new SqlParameter("@d31", System.Data.SqlDbType.NChar, 10, "H_percentage"));
                                cmd.Parameters.Add(new SqlParameter("@d32", System.Data.SqlDbType.NChar, 30, "H_board"));


                                cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d2"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d3"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d4"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d5"].Value = row.Cells[4].Value;
                                cmd.Parameters["@d6"].Value = row.Cells[5].Value;
                                cmd.Parameters["@d7"].Value = row.Cells[6].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[7].Value;
                                cmd.Parameters["@d9"].Value = row.Cells[8].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[9].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[10].Value;
                                cmd.Parameters["@d12"].Value = row.Cells[11].Value;
                                cmd.Parameters["@d13"].Value = row.Cells[12].Value;
                                cmd.Parameters["@d14"].Value = classes;
                                cmd.Parameters["@d15"].Value = row.Cells[14].Value;
                                cmd.Parameters["@d16"].Value = yearsss;
                                cmd.Parameters["@d17"].Value = "1st";
                                cmd.Parameters["@d18"].Value = row.Cells[17].Value;
                                cmd.Parameters["@d19"].Value = row.Cells[18].Value;
                                cmd.Parameters["@d20"].Value = row.Cells[19].Value;
                                cmd.Parameters["@d21"].Value = row.Cells[20].Value;
                                cmd.Parameters["@d22"].Value = row.Cells[21].Value;
                                cmd.Parameters["@d23"].Value = row.Cells[22].Value;
                                cmd.Parameters["@d24"].Value = row.Cells[23].Value;
                                cmd.Parameters["@d25"].Value = row.Cells[24].Value;
                                cmd.Parameters["@d26"].Value = row.Cells[25].Value;
                                cmd.Parameters["@d27"].Value = row.Cells[26].Value;
                                cmd.Parameters["@d28"].Value = row.Cells[27].Value;
                                cmd.Parameters["@d29"].Value = row.Cells[28].Value;
                                cmd.Parameters["@d30"].Value = row.Cells[29].Value;
                                cmd.Parameters["@d31"].Value = row.Cells[30].Value;
                                cmd.Parameters["@d32"].Value = row.Cells[31].Value;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                    }
                }

                MessageBox.Show("Successfully Resgistered", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if (fromclass.Text == "")
            {
                MessageBox.Show("Please select Class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fromclass.Focus();
                return;
            }
            if (toclass.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fromclass.Focus();
                return;
            }
            if (promoteyear.Text == "")
            {
                MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Course.Focus();
                return;
            }

            try
            {

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                        {
                        }
                        else
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string ct = "select ScholarNo from Student where Session='" + promoteyear.Text + "' and ScholarNo=@find and Term='" + toclass.Text + "' and Course='" + fromclass.Text + "'";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "ScholarNo"));
                            cmd.Parameters["@find"].Value = row.Cells[0].Value.ToString();
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {

                                con.Close();
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "insert into Student(ScholarNo,Student_name,Admission_No,DateOfAdmission,Fathers_Name,Mother_name,Gender,DOB,Category,Religion,Address,Contact_No,Email,Course,Branch,Session,Term,Types,Section,Submitted_Documents,Nationality,GuardianName,GuardianContactNo,GuardianAddress,High_School_name,HS_Year_of_passing,HS_Percentage,HS_Board,Higher_secondary_Name,H_year_of_passing,H_percentage,H_board) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27,@d28,@d29,@d30,@d31,@d32)";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Student_name"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "Admission_No"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 20, "DateOfAdmission"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 30, "Fathers_Name"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Mother_name"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "Gender"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 15, "DOB"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 15, "Category"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 30, "Religion"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 50, "Address"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 10, "Contact_No"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 30, "Email"));
                                cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 30, "Course"));
                                cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Branch"));
                                cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Session"));
                                cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Term"));
                                cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "Types"));
                                cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Section"));
                                cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.VarChar, 250, "Submitted_Documents"));
                                cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.NChar, 20, "Nationality"));
                                cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.NChar, 30, "GuardianName"));
                                cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "GuardianContactNo"));
                                cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.NChar, 50, "GuardianAddress"));
                                cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 30, "High_School_name"));
                                cmd.Parameters.Add(new SqlParameter("@d26", System.Data.SqlDbType.NChar, 10, "HS_Year_of_passing"));
                                cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 10, "HS_Percentage"));
                                cmd.Parameters.Add(new SqlParameter("@d28", System.Data.SqlDbType.NChar, 30, "HS_Board"));
                                cmd.Parameters.Add(new SqlParameter("@d29", System.Data.SqlDbType.NChar, 30, "Higher_secondary_Name"));
                                cmd.Parameters.Add(new SqlParameter("@d30", System.Data.SqlDbType.NChar, 10, "H_year_of_passing"));
                                cmd.Parameters.Add(new SqlParameter("@d31", System.Data.SqlDbType.NChar, 10, "H_percentage"));
                                cmd.Parameters.Add(new SqlParameter("@d32", System.Data.SqlDbType.NChar, 30, "H_board"));


                                cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d2"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d3"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d4"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d5"].Value = row.Cells[4].Value;
                                cmd.Parameters["@d6"].Value = row.Cells[5].Value;
                                cmd.Parameters["@d7"].Value = row.Cells[6].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[7].Value;
                                cmd.Parameters["@d9"].Value = row.Cells[8].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[9].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[10].Value;
                                cmd.Parameters["@d12"].Value = row.Cells[11].Value;
                                cmd.Parameters["@d13"].Value = row.Cells[12].Value;
                                cmd.Parameters["@d14"].Value = fromclass.Text;
                                cmd.Parameters["@d15"].Value = row.Cells[14].Value;
                                cmd.Parameters["@d16"].Value = promoteyear.Text;
                                cmd.Parameters["@d17"].Value = toclass.Text;
                                cmd.Parameters["@d18"].Value = row.Cells[17].Value;
                                cmd.Parameters["@d19"].Value = row.Cells[18].Value;
                                cmd.Parameters["@d20"].Value = row.Cells[19].Value;
                                cmd.Parameters["@d21"].Value = row.Cells[20].Value;
                                cmd.Parameters["@d22"].Value = row.Cells[21].Value;
                                cmd.Parameters["@d23"].Value = row.Cells[22].Value;
                                cmd.Parameters["@d24"].Value = row.Cells[23].Value;
                                cmd.Parameters["@d25"].Value = row.Cells[24].Value;
                                cmd.Parameters["@d26"].Value = row.Cells[25].Value;
                                cmd.Parameters["@d27"].Value = row.Cells[26].Value;
                                cmd.Parameters["@d28"].Value = row.Cells[27].Value;
                                cmd.Parameters["@d29"].Value = row.Cells[28].Value;
                                cmd.Parameters["@d30"].Value = row.Cells[29].Value;
                                cmd.Parameters["@d31"].Value = row.Cells[30].Value;
                                cmd.Parameters["@d32"].Value = row.Cells[31].Value;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                        }
                    }
                }

                MessageBox.Show("Successfully Resgistered", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fromclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            streams.Items.Clear();
            streams.Text = "";
            streams.Enabled = true;

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Section) from Student where course= '" + fromclass.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    streams.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void studenpromoclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (studenpromoclass.Text == "S.1")
            {
                studentpromoclass2.Enabled = true;
                studentpromoclass2.Text = "S.2";
                studentpromoclass2.Items.Add("S.1");
            }
            if (studenpromoclass.Text == "S.2")
            {
                studentpromoclass2.Enabled = true;
                studentpromoclass2.Text = "S.3";
                studentpromoclass2.Items.Add("S.2");
            }
            if (studenpromoclass.Text == "S.3")
            {
                studentpromoclass2.Enabled = true;
                studentpromoclass2.Text = "S.4";
                studentpromoclass2.Items.Add("S.3");
            }

            if (studenpromoclass.Text == "S.4")
            {
                studentpromoclass2.Enabled = true;
                studentpromoclass2.Text = "S.4";
                studentpromoclass2.Items.Add("S.5");
            }
            if (studenpromoclass.Text == "S.5")
            {
                studentpromoclass2.Enabled = true;
                studentpromoclass2.Text = "S.6";
                studentpromoclass2.Items.Add("S.5");
            }
            if (studenpromoclass.Text == "S.6")
            {
                studentpromoclass2.Text = "S.6";
            }
            AutocompleteStudentName3();
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView3.SelectedRows[0];
            dataGridView3.Rows.Remove(dr);

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                string yearss = (Convert.ToInt32(studentpromoyear.Text) + 1).ToString();
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                        {
                        }
                        else
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string ct = "select ScholarNo from Student where Session='" + yearss + "' and ScholarNo=@find and Term='1st' and Course='" + studentpromoclass2.Text + "'";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "ScholarNo"));
                            cmd.Parameters["@find"].Value = row.Cells[0].Value.ToString();
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                MessageBox.Show("Registration. Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                AdmissionNo = "";
                                if ((rdr != null))
                                {
                                    rdr.Close();
                                }
                                return;
                            }

                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "insert into Student(ScholarNo,Student_name,Admission_No,DateOfAdmission,Fathers_Name,Mother_name,Gender,DOB,Category,Religion,Address,Contact_No,Email,Course,Branch,Session,Term,Types,Section,Submitted_Documents,Nationality,GuardianName,GuardianContactNo,GuardianAddress,High_School_name,HS_Year_of_passing,HS_Percentage,HS_Board,Higher_secondary_Name,H_year_of_passing,H_percentage,H_board) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27,@d28,@d29,@d30,@d31,@d32)";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Student_name"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 15, "Admission_No"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 20, "DateOfAdmission"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 30, "Fathers_Name"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Mother_name"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 10, "Gender"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 15, "DOB"));
                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 15, "Category"));
                            cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 30, "Religion"));
                            cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 50, "Address"));
                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 10, "Contact_No"));
                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 30, "Email"));
                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 30, "Course"));
                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 30, "Branch"));
                            cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 10, "Session"));
                            cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d18", System.Data.SqlDbType.NChar, 20, "Types"));
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Section"));
                            cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.VarChar, 250, "Submitted_Documents"));
                            cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.NChar, 20, "Nationality"));
                            cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.NChar, 30, "GuardianName"));
                            cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.NChar, 10, "GuardianContactNo"));
                            cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.NChar, 50, "GuardianAddress"));
                            cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.NChar, 30, "High_School_name"));
                            cmd.Parameters.Add(new SqlParameter("@d26", System.Data.SqlDbType.NChar, 10, "HS_Year_of_passing"));
                            cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 10, "HS_Percentage"));
                            cmd.Parameters.Add(new SqlParameter("@d28", System.Data.SqlDbType.NChar, 30, "HS_Board"));
                            cmd.Parameters.Add(new SqlParameter("@d29", System.Data.SqlDbType.NChar, 30, "Higher_secondary_Name"));
                            cmd.Parameters.Add(new SqlParameter("@d30", System.Data.SqlDbType.NChar, 10, "H_year_of_passing"));
                            cmd.Parameters.Add(new SqlParameter("@d31", System.Data.SqlDbType.NChar, 10, "H_percentage"));
                            cmd.Parameters.Add(new SqlParameter("@d32", System.Data.SqlDbType.NChar, 30, "H_board"));

                            auto();
                            string checkdate = DateTime.Today.ToShortDateString();
                            DateTime dtc1 = DateTime.Parse(checkdate);
                            string converteddatesc = dtc1.ToString("dd/MMM/yyyy");
                            cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d2"].Value = row.Cells[1].Value;
                            cmd.Parameters["@d3"].Value = row.Cells[2].Value;
                            cmd.Parameters["@d4"].Value = row.Cells[3].Value;
                            cmd.Parameters["@d5"].Value = row.Cells[4].Value;
                            cmd.Parameters["@d6"].Value = row.Cells[5].Value;
                            cmd.Parameters["@d7"].Value = row.Cells[6].Value;
                            cmd.Parameters["@d8"].Value = row.Cells[7].Value;
                            cmd.Parameters["@d9"].Value = row.Cells[8].Value;
                            cmd.Parameters["@d10"].Value = row.Cells[9].Value;
                            cmd.Parameters["@d11"].Value = row.Cells[10].Value;
                            cmd.Parameters["@d12"].Value = row.Cells[11].Value;
                            cmd.Parameters["@d13"].Value = row.Cells[12].Value;
                            cmd.Parameters["@d14"].Value = studentpromoclass2.Text;
                            cmd.Parameters["@d15"].Value = row.Cells[14].Value;
                            cmd.Parameters["@d16"].Value = yearss;
                            cmd.Parameters["@d17"].Value = "1st";
                            cmd.Parameters["@d18"].Value = row.Cells[17].Value;
                            cmd.Parameters["@d19"].Value = row.Cells[18].Value;
                            cmd.Parameters["@d20"].Value = row.Cells[19].Value;
                            cmd.Parameters["@d21"].Value = row.Cells[20].Value;
                            cmd.Parameters["@d22"].Value = row.Cells[21].Value;
                            cmd.Parameters["@d23"].Value = row.Cells[22].Value;
                            cmd.Parameters["@d24"].Value = row.Cells[23].Value;
                            cmd.Parameters["@d25"].Value = row.Cells[24].Value;
                            cmd.Parameters["@d26"].Value = row.Cells[25].Value;
                            cmd.Parameters["@d27"].Value = row.Cells[26].Value;
                            cmd.Parameters["@d28"].Value = row.Cells[27].Value;
                            cmd.Parameters["@d29"].Value = row.Cells[28].Value;
                            cmd.Parameters["@d30"].Value = row.Cells[29].Value;
                            cmd.Parameters["@d31"].Value = row.Cells[30].Value;
                            cmd.Parameters["@d32"].Value = row.Cells[31].Value;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                MessageBox.Show("Successfully Resgistered", "Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
