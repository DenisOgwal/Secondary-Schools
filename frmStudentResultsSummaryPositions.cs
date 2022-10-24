using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace College_Management_System
{
    public partial class frmStudentResultsSummaryPosition : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();



        public frmStudentResultsSummaryPosition()
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(class) FROM Results", CN);
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
        private void AutocompleteCourse2()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(class) FROM Results", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                class1.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    class1.Items.Add(drow[0].ToString());
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Session) FROM StudentRegistration", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Session.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Session.Items.Add(drow[0].ToString());
                }
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(class) FROM Positions", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                classes.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    classes.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteStudentName2()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(class) FROM Leveladvanced", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                aclasses.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    aclasses.Items.Add(drow[0].ToString());
                    aclassess.Items.Add(drow[0].ToString());
                    aclassesss.Items.Add(drow[0].ToString());
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
            AutocompleteStudentName();
            AutocompleteStudentName2();
            AutocompleteCourse2();
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
                string ct = "select distinct RTRIM(term) from Results where class= '" + Course.Text + "'";
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
                    MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
                    return;
                }
                if (Session.Text == "")
                {
                    MessageBox.Show("Please select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Session.Focus();
                    return;
                }
                if (osets.Text == "")
                {
                    MessageBox.Show("Please select set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    osets.Focus();
                    return;
                }
                if (Course.Text == "S.4")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(studentname)[Student Names], RTRIM(year)[Year], RTRIM(class)[Class],RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[SubjectCode],RTRIM(subjectname)[Subject Name],RTRIM(begginingofterm)[Ist Paper],RTRIM(midterm)[2nd Paper], RTRIM(endofterm)[3rd Paper], RTRIM(FourthPaper)[4th Paper], RTRIM(Average)[Average], RTRIM(Grade)[Grade], RTRIM(Aggregate)[Agregate] from Results where  class= '" + Course.Text + "'and term='" + Branch.Text + "'and year='" + Session.Text + "' and Sets='"+osets.Text+"' order by studentname ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "Results");
                    dataGridView1.DataSource = myDataSet.Tables["Results"].DefaultView;
                    con.Close();
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(studentname)[Student Names], RTRIM(year)[Year], RTRIM(class)[Class],RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[SubjectCode],RTRIM(subjectname)[Subject Name],RTRIM(begginingofterm)[BOT],RTRIM(midterm)[MOT], RTRIM(endofterm)[EOT], RTRIM(Average)[Average], RTRIM(Grade)[Grade], RTRIM(Aggregate)[Agregate] from Results where  class= '" + Course.Text + "'and term='" + Branch.Text + "'and year='" + Session.Text + "' and Sets='" + osets.Text + "'  order by studentname ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "Results");
                    dataGridView1.DataSource = myDataSet.Tables["Results"].DefaultView;
                    con.Close();
                }
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
                if(classes.Text=="S.4"){
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(Positions.term)[Term],RTRIM(stream)[Stream], RTRIM(total)[Total], RTRIM(Position)[Position],RTRIM(maxim)[Number Of Students],RTRIM(Totalagg)[Aggregates],RTRIM(Division)[Division],RTRIM(Sets)[Set] from Positions where  year='" + year.Text + "' and class='" + classes.Text + "' and Positions.term='" + term.Text + "' and Sets='" + sets.Text + "' ", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Positions");
                dataGridView2.DataSource = myDataSet.Tables["Positions"].DefaultView;
                con.Close();
                }else{
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number],  RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(Positions.term)[Term],RTRIM(stream)[Stream], RTRIM(total)[Total], RTRIM(Position)[Position],RTRIM(maxim)[Number Of Students],RTRIM(Totalagg)[Aggregates],RTRIM(Division)[Division],RTRIM(Sets)[Set] from Positions where  year='" + year.Text + "' and class='" + classes.Text + "' and Positions.term='" + term.Text + "'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Positions");
                dataGridView2.DataSource = myDataSet.Tables["Positions"].DefaultView;
                con.Close();
                }
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
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            Course.Text = "";
            Branch.Text = "";
            Session.Text = "";
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
                string ct = "select distinct RTRIM(year) from Results where term= '" + Branch.Text + "' and class= '" + Course.Text + "'";
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

     

        private void frmStudentRegistrationRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.UserType.Text = label5.Text;
            frm.User.Text = label8.Text;
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
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
        }

        private void classes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                term.Enabled = true;
                term.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(term) FROM Positions where class='"+classes.Text+"'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                term.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    term.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void term_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                year.Enabled = true;
                year.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(year) FROM Positions where class='" + classes.Text + "' and term='"+term.Text+"'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                year.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    year.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ayear.Enabled = true;
                ayear.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(year) FROM LeveladvancedFinale where class='" + aclasses.Text + "' and term='" + aterm.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ayear.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ayear.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
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

        private void button8_Click_1(object sender, EventArgs e)
        {
            dataGridView3.DataSource = null;
        }

        private void aclasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                aterm.Enabled = true;
                aterm.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(term) FROM LeveladvancedFinale where class='" + aclasses.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                aterm.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    aterm.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number],RTRIM(studentname)[Student Name],RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[Subject Code],RTRIM(subjectname)[Subject Name],RTRIM(Paper)[Paper],RTRIM(marks)[Marks],RTRIM(ExamName)[Exam Name] from Leveladvanced where  year='" + ayear.Text + "' and class='" + aclasses.Text + "' and term='" + aterm.Text + "' and Sets='" + asets.Text + "'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Leveladvanced");
                dataGridView3.DataSource = myDataSet.Tables["Leveladvanced"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            subjectcode1.Items.Clear();
            subjectcode1.Text = "";
            subjectcode1.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(subjectcode) from Results where term= '" + term1.Text + "' and class= '" + class1.Text + "' and year='"+year1.Text+"'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    subjectcode1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            year1.Items.Clear();
            year1.Text = "";
            year1.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(year) from Results where term= '" + term1.Text + "' and class= '" + class1.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    year1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void class1_SelectedIndexChanged(object sender, EventArgs e)
        {
            term1.Items.Clear();
            term1.Text = "";
            term1.Enabled = true;

            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(term) from Results where CLASS= '" + class1.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    term1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Session_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (class1.Text == "")
                {
                    MessageBox.Show("Please select class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    class1.Focus();
                    return;
                }
                if (term1.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    term1.Focus();
                    return;
                }
                if (year1.Text == "")
                {
                    MessageBox.Show("Please select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    year1.Focus();
                    return;
                }
                if (set1.Text == "")
                {
                    MessageBox.Show("Please select set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    set1.Focus();
                    return;
                }
                if (subjectcode1.Text == "")
                {
                    MessageBox.Show("Please select Subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    subjectcode1.Focus();
                    return;
                }
                if (class1.Text == "S.4")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(studentname)[Student Names], RTRIM(year)[Year], RTRIM(class)[Class],RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[SubjectCode],RTRIM(subjectname)[Subject Name],RTRIM(begginingofterm)[Ist Paper],RTRIM(midterm)[2nd Paper], RTRIM(endofterm)[3rd Paper], RTRIM(FourthPaper)[4th Paper], RTRIM(Average)[Average], RTRIM(Grade)[Grade], RTRIM(Aggregate)[Agregate] from Results where  class= '" + class1.Text + "'and term='" + term1.Text + "'and year='" + year1.Text + "' and Sets='" + set1.Text + "' and subjectcode='"+subjectcode1.Text+"' order by studentname ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "Results");
                    dataGridView4.DataSource = myDataSet.Tables["Results"].DefaultView;
                    con.Close();
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(studentname)[Student Names], RTRIM(year)[Year], RTRIM(class)[Class],RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[SubjectCode],RTRIM(subjectname)[Subject Name],RTRIM(begginingofterm)[BOT],RTRIM(midterm)[MOT], RTRIM(endofterm)[EOT], RTRIM(Average)[Average], RTRIM(Grade)[Grade], RTRIM(Aggregate)[Agregate] from Results where  class= '" + class1.Text + "'and term='" + term1.Text + "'and year='" + year1.Text + "'  and subjectcode='" + subjectcode1.Text + "'  order by studentname ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "Results");
                    dataGridView4.DataSource = myDataSet.Tables["Results"].DefaultView;
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records();
                    frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                }
            }
        }

        private void delete_records()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Results where year='" + Session.Text + "' and class='" + Course.Text + "' and term='" + Branch.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
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

        private void button10_Click(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (class1.Text == "S.4")
                    {
                        delete_records6();
                    }
                    else
                    {
                        delete_records3();
                    }
                    frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                }
            }
            else
            {
                MessageBox.Show("Your Not The Headmaster", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void delete_records3()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Results where year='" + year1.Text + "' and class='" + class1.Text + "' and term='" + term1.Text + "' and SubjectCode='" + subjectcode1.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        private void delete_records6()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Results where year='" + year1.Text + "' and class='" + class1.Text + "' and term='" + term1.Text + "' and SubjectCode='" + subjectcode1.Text + "' and Sets='"+set1.Text+"'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void button14_Click(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                delete_records4();
                frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                this.Hide();
                frm.label5.Text = label5.Text;
                frm.label8.Text = label8.Text;
                frm.Show();
            }
        }

        private void delete_records4()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                if (classes.Text == "S.4")
                {
                    string cq = "delete from Positions where year='" + year.Text + "' and class='" + classes.Text + "' and term='" + term.Text + "' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(cq);
                }
                else
                {
                    string cq = "delete from Positions where year='" + year.Text + "' and class='" + classes.Text + "' and term='" + term.Text + "'";
                    cmd = new SqlCommand(cq);
                }
                
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView4.DataSource = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView4.DataSource == null)
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
                rowsTotal = dataGridView4.RowCount - 1;
                colsTotal = dataGridView4.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView4.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView4.Rows[I].Cells[j].Value;
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

        private void dataGridView1_RowDividerDoubleClick(object sender, DataGridViewRowDividerDoubleClickEventArgs e)
        {
            
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow dr = dataGridView1.SelectedRows[0];
                        int RowsAffected = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cq = "delete from Results where year='" + dr.Cells[2].Value + "' and class='" + dr.Cells[3].Value + "' and term='" + dr.Cells[4].Value + "' and SubjectCode='" + dr.Cells[6].Value + "' and studentno='" + dr.Cells[0].Value + "'";
                        cmd = new SqlCommand(cq);
                        cmd.Connection = con;
                        RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                        this.Hide();
                        frm.label5.Text = label5.Text;
                        frm.label8.Text = label8.Text;
                        frm.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void dataGridView4_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        DataGridViewRow dr = dataGridView4.SelectedRows[0];
                        int RowsAffected = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cq = "delete from Results where year='" + dr.Cells[2].Value + "' and class='" + dr.Cells[3].Value + "' and term='" + dr.Cells[4].Value + "' and SubjectCode='" + dr.Cells[6].Value + "' and studentno='" + dr.Cells[0].Value + "'";
                        cmd = new SqlCommand(cq);
                        cmd.Connection = con;
                        RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else
                        {
                            MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                        frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                        this.Hide();
                        frm.label5.Text = label5.Text;
                        frm.label8.Text = label8.Text;
                        frm.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
      
        private void button15_Click(object sender, EventArgs e)
        {
          
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records7();
                    frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                }
            }
        }
        private void delete_records7()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq2 = "delete from LeveladvancedFinale where year='" + ayear.Text + "' and class='" + aclasses.Text + "' and term='" + aterm.Text + "' and Sets='" + asets.Text + "'";
                cmd = new SqlCommand(cq2);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Leveladvanced where year='" + ayear.Text + "' and class='" + aclasses.Text + "' and term='" + aterm.Text + "' and Sets='" + asets.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void button19_Click(object sender, EventArgs e)
        {
            dataGridView5.DataSource = null;
        }

        private void button17_Click(object sender, EventArgs e)
        {

            if (dataGridView5.DataSource == null)
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
                rowsTotal = dataGridView5.RowCount - 1;
                colsTotal = dataGridView5.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView5.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView5.Rows[I].Cells[j].Value;
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

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number],RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(term)[Term], RTRIM(stream)[Stream],RTRIM(subjectname)[Subject Name],RTRIM(Grade)[Grade],RTRIM(Points)[Points] from LeveladvancedGrade where  year='" + ayearss.Text + "' and class='" + aclassess.Text + "' and term='" + atermss.Text + "' and Sets='" + asetss.Text + "'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "LeveladvancedGrade");
                dataGridView5.DataSource = myDataSet.Tables["LeveladvancedGrade"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records8();
                    frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                }
            }
        }
        private void delete_records8()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from LeveladvancedGrade where year='" + ayearss.Text + "' and class='" + aclassess.Text + "' and term='" + atermss.Text + "' and Sets='" + asetss.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void aclassess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                atermss.Enabled = true;
                atermss.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(term) FROM LeveladvancedGrade where class='" + aclassess.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                atermss.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    atermss.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atermss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ayearss.Enabled = true;
                ayearss.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(year) FROM LeveladvancedGrade where class='" + aclassess.Text + "' and term='" + atermss.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ayearss.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ayearss.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ayear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            dataGridView6.DataSource = null;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridView6.DataSource == null)
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
                rowsTotal = dataGridView6.RowCount - 1;
                colsTotal = dataGridView6.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView6.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView6.Rows[I].Cells[j].Value;
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

        private void aclassesss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                atermsss.Enabled = true;
                atermsss.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(term) FROM LeveladvancedGrade where class='" + aclassesss.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                atermsss.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    atermsss.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void atermsss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ayearsss.Enabled = true;
                ayearsss.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(year) FROM LeveladvancedGrade where class='" + aclassesss.Text + "' and term='" + atermsss.Text + "'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ayearsss.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ayearsss.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void set1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ayearsss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                subjects.Enabled = true;
                subjects.Text = "";
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(subjectname) FROM Leveladvanced where class='" + aclassesss.Text + "' and term='" + atermsss.Text + "' and year='"+ayearsss.Text+"'", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                subjects.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    subjects.Items.Add(drow[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number],RTRIM(studentname)[Student Name],RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(term)[Term], RTRIM(stream)[Stream], RTRIM(subjectcode)[Subject Code],RTRIM(subjectname)[Subject Name],RTRIM(Paper)[Paper],RTRIM(marks)[Marks],RTRIM(ExamName)[Exam Name] from Leveladvanced where  year='" + ayearsss.Text + "' and class='" + aclassesss.Text + "' and term='" + atermsss.Text + "' and Sets='" + asetsss.Text + "' and subjectname='"+subjects.Text+"'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Leveladvanced");
                dataGridView6.DataSource = myDataSet.Tables["Leveladvanced"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (label5.Text.Trim() == "HEADMASTER")
            {
                if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    delete_records9();
                    frmStudentResultsSummaryPosition frm = new frmStudentResultsSummaryPosition();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                }
            }
        }
        private void delete_records9()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq2 = "delete from LeveladvancedFinale where year='" + ayearsss.Text + "' and class='" + aclassesss.Text + "' and term='" + atermsss.Text + "' and Sets='" + asetsss.Text + "' and subjectname='"+subjects.Text+"' ";
                cmd = new SqlCommand(cq2);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Leveladvanced where year='" + ayearsss.Text + "' and class='" + aclassesss.Text + "' and term='" + atermsss.Text + "' and Sets='" + asetsss.Text + "' and subjectname='" + subjects.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
