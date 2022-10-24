using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmInternalMarksEntryNew : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string grade = null;
        public frmInternalMarksEntryNew()
        {
            InitializeComponent();
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
            cmbSection.Items.Clear();
            cmbSection.Text = "";
            cmbSection.Enabled = true;
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Section) from Student,Batch where Student.Session=Batch.Session and Student.Course = '" + cmbCourse.Text + "' and Student.Branch= '" + cmbBranch.Text + "' and Semester='" + cmbSemester.Text + "'";
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

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSubjectCode.Items.Clear();
                cmbSubjectCode.Text = "";
                cmbSubjectCode.Enabled = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(SubjectCode) from SubjectGrade where Class = '" + cmbCourse.Text + "' and Term='" + cmbSemester.Text + "' ";
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
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
                {
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
                if (cmbCourse.Text == "S.4")
                {
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

                    cmbExamName.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbSession.Text == "")
            {
                MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select Stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string sql = "select rtrim(ScholarNo),rtrim(Admission_No),rtrim(student_name) from Student,batch where Student.Course=batch.Course and Student.Session=batch.Session and Student.Term=batch.Semester  and Student.Course = '" + cmbCourse.Text + "' and Student.Branch= '" + cmbBranch.Text + "' and Batch.semester= '" + cmbSemester.Text + "' and Student.Session= '" + cmbSession.Text + "' and section='" + cmbSection.Text + "' order by student_name,ScholarNo";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2]);
                }
                con.Close();
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
                string ct = "select distinct RTRIM(Session) from Batch ";
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
            /* Left = Top = 0;
             this.Width = Screen.PrimaryScreen.WorkingArea.Width;
             this.Height = Screen.PrimaryScreen.WorkingArea.Height;
             this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;*/
            AutocompleSession();
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label6.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { Delete.Enabled = true; } else { Delete.Enabled = false; }
                    if (pricess == "Yes") { Update_record.Enabled = true; } else { Update_record.Enabled = false; }
                }
                if (label6.Text == "ADMIN")
                {
                    Delete.Enabled = true;
                    Update_record.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void Reset()
        {
            frmInternalMarksEntry frm = new frmInternalMarksEntry();
            this.Hide();
            frm.label5.Text = label5.Text;
            frm.label6.Text = label6.Text;
            frm.Show();
            cmbBranch.Text = "";
            cmbCourse.Text = "";
            cmbSection.Text = "";
            cmbSession.Text = "";
            cmbSemester.Text = "";
            cmbSubjectCode.Text = "";
            txtSubjectName.Text = "";
            cmbExamName.Text = "";
            dtpExamDate.Text = System.DateTime.Today.ToString();
            cmbBranch.Enabled = false;
            cmbCourse.Enabled = false;
            cmbSemester.Enabled = false;
            cmbSubjectCode.Enabled = false;
            cmbSection.Enabled = false;
            // dataGridView1.Rows.Clear();
            btnSave.Enabled = true;
            Delete.Enabled = false;
            Update_record.Enabled = true;
            cmbSession.Focus();
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }
        public void reset2()
        {
            cmbExamName.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }
                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                if (cmbSubjectCode.Text == "")
                {
                    MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSubjectCode.Focus();
                    return;
                }
                if (cmbExamName.Text == "")
                {
                    MessageBox.Show("Please select exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbExamName.Focus();
                    return;
                }
                if (staffname.Text == "")
                {
                    MessageBox.Show("Please select Staff Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    staffname.Focus();
                    return;
                }

                if (cmbExamName.Text == "A1")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }

                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "insert into ResultsNew(studentno,year,class,term,stream,level,subjectcode,subjectname,A1,ExamDate,enrollmentnumber,studentname,Initials) VALUES (@d12,@d1,@d2,@d4,@d5,@d3,@d6,@d7,@d8,@d9,@d10,@d11,@d13)";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                // Add Parameters to Command Parameters collection
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A1"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbExamName.Text == "A2")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set A2=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "subjectname"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A2"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbExamName.Text == "A3")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set A3=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "subjectname"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A3"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbExamName.Text == "A4")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set A4=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "subjectname"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A4"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbExamName.Text == "A5")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set A5=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "subjectname"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A5"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbExamName.Text == "A6")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set A6=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "subjectname"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A6"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                cmd.Prepare();
                                // Data to be inserted
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.Parameters["@d13"].Value = staffname.Text;
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                resultcompute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                if (cmbExamName.Text == "A1")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A1=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A1"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                if (cmbExamName.Text == "A2")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A2=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A2"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                if (cmbExamName.Text == "A3")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A3=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A3"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                if (cmbExamName.Text == "A4")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A4=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A4"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                if (cmbExamName.Text == "A5")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A5=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A5"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                if (cmbExamName.Text == "A6")
                {
                    string cb = "update ResultsNew set stream=@d5,subjectname=@d7,ExamDate=@d9,A6=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "A6"));
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                cmd.ExecuteNonQuery();

                            }
                        }
                    }
                    reset2();
                }
                con.Close();
                    MessageBox.Show("Successfully updated", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Update_record.Enabled = false;
                resultcompute();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInternalMarksEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label5.Text;
            frm.User.Text = label6.Text;
            frm.Show();*/
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete the records?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }

        private void delete_records()
        {
            try
            {
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from ResultsNew where year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and term='" + cmbSemester.Text + "' and stream='" + cmbSection.Text + "' and SubjectCode='" + cmbSubjectCode.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Delete.Enabled = false;
                    Reset();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Delete.Enabled = false;
                    Reset();
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

        int pupiltotal = 0;
        int maxi = 0;
        int Totalagg = 0;
        public void pupiltotals()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {

                        label9.Text = "";
                        label15.Text = "";
                        label19.Text = "";
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT SUM(Average) FROM Results WHERE  studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' ";
                            cmd = new SqlCommand(inquery3, con);
                            label9.Text = cmd.ExecuteScalar().ToString();
                            pupiltotal = int.Parse(label9.Text);
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
                            String inquery3 = "SELECT SUM(aggregate) FROM Results WHERE  studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                            cmd = new SqlCommand(inquery3, con);
                            label19.Text = cmd.ExecuteScalar().ToString();
                            Totalagg = int.Parse(label19.Text);
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
                            String inquery3 = "SELECT COUNT( DISTINCT studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and stream='" + cmbSection.Text + "' ";
                            cmd = new SqlCommand(inquery3, con);
                            label15.Text = cmd.ExecuteScalar().ToString();
                            maxi = int.Parse(label15.Text);
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
                            string cb = "insert into Positions(studentno,year,class,term,total,position,maxim,Totalagg,Stream) VALUES (@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27)";
                            cmd = new SqlCommand(cb, con);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d20", System.Data.SqlDbType.NChar, 30, "year"));
                            cmd.Parameters.Add(new SqlParameter("@d21", System.Data.SqlDbType.NChar, 30, "class"));
                            cmd.Parameters.Add(new SqlParameter("@d22", System.Data.SqlDbType.NChar, 10, "term"));
                            cmd.Parameters.Add(new SqlParameter("@d23", System.Data.SqlDbType.Int, 30, "total"));
                            cmd.Parameters.Add(new SqlParameter("@d24", System.Data.SqlDbType.Int, 10, "position"));
                            cmd.Parameters.Add(new SqlParameter("@d25", System.Data.SqlDbType.Int, 10, "maxim"));
                            cmd.Parameters.Add(new SqlParameter("@d26", System.Data.SqlDbType.Int, 10, "Totalagg"));
                            cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 30, "Totalagg"));
                            cmd.Prepare();
                            cmd.Parameters["@d19"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d20"].Value = cmbSession.Text;
                            cmd.Parameters["@d21"].Value = cmbCourse.Text;
                            cmd.Parameters["@d22"].Value = cmbSemester.Text;
                            cmd.Parameters["@d23"].Value = pupiltotal;
                            cmd.Parameters["@d24"].Value = "";
                            cmd.Parameters["@d25"].Value = maxi;
                            cmd.Parameters["@d26"].Value = Totalagg;
                            cmd.Parameters["@d27"].Value = cmbSection.Text;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select Stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }
            if (MessageBox.Show("Have You Done Total Addition?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {


            }
        }
        double A1, A2, A3, A4, A5, A6 = 0;
        int resultpresence1, resultpresence2,resultpresence3, resultpresence4, resultpresence5, resultpresence6 = 0;
        double resultsaverage = 0; int identifiers = 0;
        public void resultcompute()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if ((row.Cells[0].Value) == null || (row.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[0].Value.ToString()))
                    {
                    }
                    else
                    {
                        if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                        {
                            row.Cells[3].Value = DBNull.Value;
                        }
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT A1,A2,A3,A4,A5,A6 FROM ResultsNew WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectcode = '" + cmbSubjectCode.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                            cmd = new SqlCommand(inquery3);
                            cmd.Connection = con;
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                if (rdr["A1"].ToString() == "") { A1 = 0; resultpresence1 = 0; } else { A1 = Convert.ToDouble(rdr["A1"]); resultpresence1 = 1; }
                                if (rdr["A2"].ToString() == "") { A2 = 0; resultpresence2 = 0; } else { A2 = Convert.ToDouble(rdr["A2"]); resultpresence2 = 1; }
                                if (rdr["A3"].ToString() == "") { A3 = 0; resultpresence3 = 0; } else { A3 = Convert.ToDouble(rdr["A3"]); resultpresence3 = 1; }
                                if (rdr["A4"].ToString() == "") { A4 = 0; resultpresence4 = 0; } else { A4 = Convert.ToDouble(rdr["A4"]); resultpresence4 = 1; }
                                if (rdr["A5"].ToString() == "") { A5 = 0; resultpresence5 = 0; } else { A5 = Convert.ToDouble(rdr["A5"]); resultpresence5 = 1; }
                                if (rdr["A6"].ToString() == "") { A6 = 0; resultpresence6 = 0; } else { A6 = Convert.ToDouble(rdr["A6"]); resultpresence6 = 1; }

                                resultsaverage = (A1 + A2 + A3 + A4 + A5 + A6) / (resultpresence1 + resultpresence2 + resultpresence3 + resultpresence4 + resultpresence5 + resultpresence6);
                                if (resultsaverage >= 0.9 && resultsaverage <= 1.49) { identifiers = 1; }
                                if (resultsaverage >= 1.5 && resultsaverage <= 2.49) { identifiers = 2; }
                                if (resultsaverage >= 2.5 && resultsaverage <= 3.0) { identifiers = 3; }
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update ResultsNew set Average=@d8,Identifier=@d9 where studentno=@d12 and subjectcode=@d6 and term=@d4 and class=@d2 ";
                                cmd = new SqlCommand(cb);
                                cmd.Connection = con;
                                // Add Parameters to Command Parameters collection
                                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "level"));
                                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Float, 10, "Average"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "Identifier"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));

                                cmd.Prepare();
                                cmd.Parameters["@d1"].Value = cmbSession.Text;
                                cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                cmd.Parameters["@d5"].Value = cmbSection.Text;
                                cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                cmd.Parameters["@d9"].Value = identifiers;
                                cmd.Parameters["@d8"].Value = resultsaverage;
                                cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                resultsaverage = 0;
                            }
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
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
            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select Stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select Subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            try
            {
                label41.Text = cmbCourse.Text + " Class";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                label42.Text = txtSubjectName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT COUNT(studentno) FROM ResultsNew WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Identifier='3'";
                cmd = new SqlCommand(inquery3, con);
                label53.Text = cmd.ExecuteScalar().ToString();
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
                String inquery3 = "SELECT COUNT(studentno) FROM ResultsNew WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Identifier='2'";
                cmd = new SqlCommand(inquery3, con);
                label54.Text = cmd.ExecuteScalar().ToString();
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
                String inquery3 = "SELECT COUNT(studentno) FROM ResultsNew WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Identifier='1'";
                cmd = new SqlCommand(inquery3, con);
                label55.Text = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cmbBranch.Text == "O Level")
            {
                MessageBox.Show("Please this is for A Level Grading", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
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
            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select Stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select Subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSession.Text == "")
                {
                    MessageBox.Show("Please select Year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (cmbSubjectCode.Text == "")
                {
                    MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSubjectCode.Focus();
                    return;
                }

                if (cmbExamName.Text == "")
                {
                    MessageBox.Show("Please select exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbExamName.Focus();
                    return;
                }
                if (cmbExamName.Text == "A1")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A1)[Marks Obtained] from ResultsNew where  ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
                if (cmbExamName.Text == "A2")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A2)[Marks Obtained] from ResultsNew where ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
                if (cmbExamName.Text == "A3")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A3)[Marks Obtained] from ResultsNew where  ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
                if (cmbExamName.Text == "A4")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A4)[Marks Obtained] from ResultsNew where ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
                if (cmbExamName.Text == "A5")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A5)[Marks Obtained] from ResultsNew where  ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
                if (cmbExamName.Text == "A6")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(A6)[Marks Obtained] from ResultsNew where  ResultsNew.class= '" + cmbCourse.Text + "'and ResultsNew.level='" + cmbBranch.Text + "'and ResultsNew.year='" + cmbSession.Text + "' and ResultsNew.term= '" + cmbSemester.Text + "' and ResultsNew.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                    cmd = new SqlCommand(sql, con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dataGridView1.Rows.Clear();
                    while (rdr.Read() == true)
                    {
                        dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (cmbSemester.Text == "")
                {
                    MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSemester.Focus();
                    return;
                }

                if (cmbSection.Text == "")
                {
                    MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSection.Focus();
                    return;
                }
                if (cmbSubjectCode.Text == "")
                {
                    MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSubjectCode.Focus();
                    return;
                }


                if (dataGridView1.RowCount == 1)
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
                MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }

            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select stream", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
            {
                if (cmbExamName.Text == "")
                {
                    MessageBox.Show("Please select exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbExamName.Focus();
                    return;
                }
            }
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

        private void staffID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "SELECT Initials FROM Employee WHERE StaffID = '" + staffID.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (rdr.Read())
                {
                    staffname.Text = rdr.GetString(0).Trim();
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
        string numberphone = null;
        string messages = null;
        string names = null;
        string classpositions = null;
        string classtotals = null;
        string classtotalagg = null;
        string classnumber = null;
        private void button8_Click(object sender, EventArgs e)
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

            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            try
            {
                if (cmbBranch.Text == "O Level")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {

                        if (!row.IsNewRow)
                        {
                            if ((row.Cells[1].Value) != null)
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT total,position,Totalagg,maxim FROM Positions where studentno='" + row.Cells[0].Value + "'";
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                    classtotals = rdr.GetString(0).Trim();
                                    classpositions = rdr.GetString(1).Trim();
                                    classtotalagg = rdr.GetString(2).Trim();
                                    classnumber = rdr.GetString(3).Trim();
                                }
                                con.Close();
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT Contact_No,Student_name FROM Student where ScholarNo='" + row.Cells[0].Value + "'";
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                    numberphone = rdr.GetString(0).Trim();
                                    names = rdr.GetString(1).Trim();
                                }
                                if ((rdr != null))
                                {
                                    rdr.Close();
                                }
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }
                                /*
                                WebClient client = new WebClient();
                                string numbers = "+256" + numberphone;
                                messages = names + " Has a total aggregate of " + classtotalagg + " and the class position is " + classpositions + " out of " + classnumber + " Students with a total of " + classtotals + " in " + cmbCourse.Text + " class of the year " + cmbSession.Text + " and Term " + cmbSemester.Text;
                                string baseURL = "http://geniussmsgroup.com/api/http/messagesService/get?username=Denis16&password=jesus@lord1&senderid=Geniussms&message=" + messages + "&numbers=" + numbers;
                                client.OpenRead(baseURL);*/
                                // MessageBox.Show("Successfully sent message");
                            }
                        }
                    }
                    MessageBox.Show("Messages Successfully Sent", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void cmbExamName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

