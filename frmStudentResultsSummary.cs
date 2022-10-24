using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;
namespace College_Management_System
{
    public partial class frmStudentResultsSummary : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();



        public frmStudentResultsSummary()
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(CLASS) FROM FinalResults", CN);
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Class) from GradingSystem where Grading='Old'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    classes.Items.Add(rdr[0]);
                }
                con.Close();
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(CLASS) FROM LeveladvancedCollection", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                aclasses.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    aclasses.Items.Add(drow[0].ToString());
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
                string ct = "select distinct RTRIM(TERM) from FinalResults where CLASS= '" + Course.Text + "'";
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
                if (Course.Text == "S.4")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(STUDENTNO)[Student Number], RTRIM(NAMES)[Student Names], RTRIM(YEAR)[Year], RTRIM(CLASS)[Class],RTRIM(TERM)[Term], RTRIM(ENG)[English], RTRIM(MAT)[Maths],RTRIM(PHY)[Physics],RTRIM(BIO)[Biology],RTRIM(CHE)[Chemistry], RTRIM(HIS)[History], RTRIM(GEO)[Geography], RTRIM(COM)[Commerce], RTRIM(AGR)[Agriculture], RTRIM(CRE)[CRE], RTRIM(IRE)[IRE],RTRIM(IPS)[IPS],RTRIM(ENT)[Entrepreneurship],RTRIM(ICT)[Computer Studies],RTRIM(AGGREGATES)[Aggregates], RTRIM(GRADE)[Grade] , RTRIM(Sent)[Sent] from FinalResults where  CLASS= '" + Course.Text + "'and TERM='" + Branch.Text + "'and YEAR='" + Session.Text + "' and Sets='" + osets.Text + "' order by NAMES ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "FinalResults");
                    dataGridView1.DataSource = myDataSet.Tables["FinalResults"].DefaultView;
                    con.Close();
                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = new SqlCommand("select RTRIM(STUDENTNO)[Student Number], RTRIM(NAMES)[Student Names], RTRIM(YEAR)[Year], RTRIM(CLASS)[Class],RTRIM(TERM)[Term], RTRIM(ENG)[English], RTRIM(MAT)[Maths],RTRIM(PHY)[Physics],RTRIM(BIO)[Biology],RTRIM(CHE)[Chemistry], RTRIM(HIS)[History], RTRIM(GEO)[Geography], RTRIM(COM)[Commerce], RTRIM(AGR)[Agriculture], RTRIM(CRE)[CRE], RTRIM(IRE)[IRE],RTRIM(IPS)[IPS],RTRIM(ENT)[Entrepreneurship],RTRIM(ICT)[Computer Studies],RTRIM(AGGREGATES)[Aggregates], RTRIM(GRADE)[Grade], RTRIM(Sent)[Sent]  from FinalResults where  CLASS= '" + Course.Text + "'and TERM='" + Branch.Text + "'and YEAR='" + Session.Text + "'  and Sets='" + osets.Text + "' order by NAMES ASC", con);
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();
                    myDA.Fill(myDataSet, "FinalResults");
                    dataGridView1.DataSource = myDataSet.Tables["FinalResults"].DefaultView;
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
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(Student.Student_name)[Student Name], RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(Positions.term)[Term],RTRIM(Section)[Stream], RTRIM(total)[Total], RTRIM(Position)[Position],RTRIM(maxim)[Number Of Students],RTRIM(Totalagg)[Aggregates],RTRIM(Division)[Division] from Student,Positions where  year='" + year.Text + "' and class='" + classes.Text + "' and Positions.term='" + term.Text + "' and Sets='"+sets.Text+"' and Positions.studentno=Student.ScholarNo", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Positions");
                myDA.Fill(myDataSet, "Student");
                dataGridView2.DataSource = myDataSet.Tables["Positions"].DefaultView;
                dataGridView2.DataSource = myDataSet.Tables["Student"].DefaultView;
                con.Close();
                }else{
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(studentno)[Student Number], RTRIM(Student.Student_name)[Student Name], RTRIM(year)[Year], RTRIM(class)[Class], RTRIM(Positions.term)[Term],RTRIM(Section)[Stream], RTRIM(total)[Total], RTRIM(Position)[Position],RTRIM(maxim)[Number Of Students],RTRIM(Totalagg)[Aggregates],RTRIM(Division)[Division] from Student,Positions where  year='" + year.Text + "' and class='" + classes.Text + "' and Positions.term='" + term.Text + "' and Positions.studentno=Student.ScholarNo", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Positions");
                myDA.Fill(myDataSet, "Student");
                dataGridView2.DataSource = myDataSet.Tables["Positions"].DefaultView;
                dataGridView2.DataSource = myDataSet.Tables["Student"].DefaultView;
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
                string ct = "select distinct RTRIM(YEAR) from FinalResults where TERM= '" + Branch.Text + "' and CLASS= '" + Course.Text + "'";
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(YEAR) FROM LeveladvancedCollection where CLASS='" + aclasses.Text + "' and TERM='" + aterm.Text + "'", CN);
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(TERM) FROM LeveladvancedCollection where CLASS='" + aclasses.Text + "'", CN);
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
                cmd = new SqlCommand("select RTRIM(STUDENTNO)[Student Number], RTRIM(NAMES)[Student Name], RTRIM(YEAR)[Year], RTRIM(CLASS)[Class], RTRIM(TERM)[Term], RTRIM(GeneralPaper)[General Paper], RTRIM(Compulsory)[SUB ICT/SUB MATHS],RTRIM(Principle1)[Principle 1],RTRIM(Principle2)[Principle 2],RTRIM(Principle3)[Principle 3],RTRIM(Points)[Points] from LeveladvancedCollection where  YEAR='" + ayear.Text + "' and CLASS='" + aclasses.Text + "' and TERM='" + aterm.Text + "' and Sets='"+asets.Text+"'", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "LeveladvancedCollection");
                dataGridView3.DataSource = myDataSet.Tables["LeveladvancedCollection"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string numberphone = null;
        string messages = null;
        string names = null;
        string eng, mat, phy, geo, bio, che, his, com, ips, ent, agr, cre, ire, ict, aggregates, grade,sents, setn = null;
        private void button15_Click(object sender, EventArgs e)
        {
            string smsallow = Properties.Settings.Default.smsallow;
            if (smsallow == "Yes")
            {

                try
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[1].Value) != null)
                            {

                                eng = row.Cells[5].Value.ToString().Trim();
                                mat = row.Cells[6].Value.ToString().Trim();
                                phy = row.Cells[7].Value.ToString().Trim();

                                bio = row.Cells[8].Value.ToString().Trim();
                                che = row.Cells[9].Value.ToString().Trim();
                                his = row.Cells[10].Value.ToString().Trim();
                                geo = row.Cells[11].Value.ToString().Trim();

                                com = row.Cells[12].Value.ToString().Trim();
                                agr = row.Cells[13].Value.ToString().Trim();
                                cre = row.Cells[14].Value.ToString().Trim();
                                ire = row.Cells[15].Value.ToString().Trim();
                                ips = row.Cells[16].Value.ToString().Trim();
                                ent = row.Cells[17].Value.ToString().Trim();
                                ict = row.Cells[18].Value.ToString().Trim();

                                aggregates = row.Cells[19].Value.ToString().Trim();
                                grade = row.Cells[20].Value.ToString().Trim();
                                sents = row.Cells[21].Value.ToString().Trim();
                                setn = osets.Text;
                                string messages2 = " ";
                                if (com == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", COM " + com;
                                }
                                if (ips == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", IPS " + ips;
                                }

                                if (ent == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", ENT " + ent;
                                }

                                if (agr == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", AGR " + agr;
                                }

                                if (cre == "") { }
                                else
                                {
                                    messages2 = messages2 + ", CRE " + cre;
                                }

                                if (ire == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", IRE " + ire;
                                }

                                if (ict == "")
                                { }
                                else
                                {
                                    messages2 = messages2 + ", ICT " + ict;
                                }

                                if (sents == "No" || sents == "")
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    cmd = con.CreateCommand();
                                    cmd.CommandText = "SELECT Contact_No,Student_name FROM Student where ScholarNo='" + row.Cells[0].Value + "'";
                                    rdr = cmd.ExecuteReader();
                                    if (rdr.Read())
                                    {
                                        numberphone = rdr["Contact_No"].ToString().Trim();
                                        names = rdr["Student_name"].ToString().Trim();
                                    }
                                    if ((rdr != null))
                                    {
                                        rdr.Close();
                                    }
                                    if (con.State == ConnectionState.Open)
                                    {
                                        con.Close();
                                    }
                                    string numbers = null;
                                    int stringcount = numberphone.Length;
                                    if (stringcount == 10)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numbers = numberphone;
                                        WebClient client = new WebClient();
                                        messages = "Dear Parent/Guardian here is " + names + " " + Course.Text + " Performance for " + Branch.Text + " Term " + Session.Text + " " + osets.Text + ". Total Agg " + aggregates + ", Div " + grade + ", MAT " + mat + ", ENG " + eng + ", PHY " + phy + ", BIO " + bio + ", CHE " + che + ", GEO " + geo + ", HIS " + his + "" + messages2 + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messages + "&numbers=" + numbers;
                                        client.OpenRead(baseURL);
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string cb = "update FinalResults set Sent='Yes' where STUDENTNO=@d1 and YEAR='" + Session.Text + "' and  CLASS='" + Course.Text + "' and TERM='" + Branch.Text + "' and Sets='" + osets.Text + "'";
                                        cmd = new SqlCommand(cb);
                                        cmd.Connection = con;
                                        // Add Parameters to Command Parameters collection
                                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, " STUDENTNO"));
                                        cmd.Prepare();
                                        // Data to be inserted
                                        cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else if (stringcount < 10 && stringcount == 9)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numbers = "+256" + numberphone;
                                        WebClient client = new WebClient();
                                        messages = "Dear Parent/Guardian here is " + names + " " + Course.Text + " Performance for " + Branch.Text + " Term " + Session.Text + " " + osets.Text + ". Total Agg " + aggregates + ", Div " + grade + ", MAT " + mat + ", ENG " + eng + ", PHY " + phy + ", BIO " + bio + ", CHE " + che + ", GEO " + geo + ", HIS " + his + "" + messages2 + " \nKIGUMBA TOWN SEED SS";
                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messages + "&numbers=" + numbers;
                                        client.OpenRead(baseURL);
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string cb = "update FinalResults set Sent='Yes' where STUDENTNO=@d1 and YEAR='" + Session.Text + "' and  CLASS='" + Course.Text + "' and TERM='" + Branch.Text + "' and Sets='" + osets.Text + "'";
                                        cmd = new SqlCommand(cb);
                                        cmd.Connection = con;
                                        // Add Parameters to Command Parameters collection
                                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, " STUDENTNO"));
                                        cmd.Prepare();
                                        // Data to be inserted
                                        cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else if (stringcount > 10 && stringcount == 12)
                                    {
                                        string usernamess = Properties.Settings.Default.smsusername;
                                        string passwordss = Properties.Settings.Default.smspassword;
                                        numbers = numberphone;
                                        WebClient client = new WebClient();
                                        messages = "Dear Parent/Guardian here is " + names + " " + Course.Text + " Performance for " + Branch.Text + " Term " + Session.Text + " " + osets.Text + ". Total Agg " + aggregates + ", Div " + grade + ", MAT " + mat + ", ENG " + eng + ", PHY " + phy + ", BIO " + bio + ", CHE " + che + ", GEO " + geo + ", HIS " + his + "" + messages2 + " \nKIGUMBA TOWN SEED SS";

                                        string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=" + usernamess + "&password=" + passwordss + "&senderid=Geniussms&message=" + messages + "&numbers=" + numbers;
                                        client.OpenRead(baseURL);
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string cb = "update FinalResults set Sent='Yes' where STUDENTNO=@d1 and YEAR='" + Session.Text + "' and  CLASS='" + Course.Text + "' and TERM='" + Branch.Text + "' and Sets='" + osets.Text + "'";
                                        cmd = new SqlCommand(cb);
                                        cmd.Connection = con;
                                        // Add Parameters to Command Parameters collection
                                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, " STUDENTNO"));
                                        cmd.Prepare();
                                        // Data to be inserted
                                        cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    else
                                    {

                                    }
                                }

                            }
                        }
                    }
                    MessageBox.Show("Done", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    frmStudentResultsSummary frm = new frmStudentResultsSummary();
                    this.Hide();
                    frm.label5.Text = label5.Text;
                    frm.label8.Text = label8.Text;
                    frm.Show();
                    return;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.CurrentRow;
            dataGridView1.Rows.Remove(dr);
        } 
    }
}
