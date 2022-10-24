using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmInternalMarksEntry : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string grade = null;
        int aggregate = 0;
        int Average = 0;
        int endofterm = 0;
        int begginingofterm = 0;
        int midterm = 0;
        string division = "7";
        public frmInternalMarksEntry()
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
            if (cmbBranch.Text == "A Level")
            {
                sets.Text = "";
                sets.Enabled = true;
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
                string ct = "select distinct RTRIM(Section) from Student,batch where Student.Session=Batch.session and Student.Course = '" + cmbCourse.Text + "' and Student.Branch= '" + cmbBranch.Text + "' and Semester='" + cmbSemester.Text + "'";
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
            try
            {
                cmbSubjectCode.Items.Clear();
                cmbSubjectCode.Text = "";
                cmbSubjectCode.Enabled = true;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                    string ct = "select distinct RTRIM(SubjectCode) from SubjectGrade where Class = '" + cmbCourse.Text + "' and Term='"+cmbSemester.Text+"' ";
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
                    Paper.Items.Clear();
                    Paper.Text = "";
                    Paper.Enabled = true;
                    cmbExamName.Enabled = false;
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string ct2 = "select distinct RTRIM(Paper) from SubjectInfo where CourseName = '" + cmbCourse.Text + "' and SubjectCode ='" + cmbSubjectCode.Text + "' and Semester='" + cmbSemester.Text + "'";
                        cmd = new SqlCommand(ct2);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        Paper.Items.Clear();
                        while (rdr.Read())
                        {
                            Paper.Items.Add(rdr[0]);
                        }
                        con.Close();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (cmbBranch.Text == "A Level")
                {
                    string ct = "SELECT SubjectNameA FROM SubjectInfoA WHERE SubjectCodeA = '" + cmbSubjectCode.Text + "'";
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
                    if (cmbBranch.Text == "A Level")
                    {
                        Paper.Items.Clear();
                        Paper.Text = "";
                        Paper.Enabled = true;
                        cmbExamName.Enabled = false;
                       // cmbExamName.Enabled = true;
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string ct2 = "select distinct RTRIM(Paper) from SubjectInfoA where Class = '" + cmbCourse.Text + "' and SubjectCodeA ='" + cmbSubjectCode.Text + "' and Term='" + cmbSemester.Text + "'";
                            cmd = new SqlCommand(ct2);
                            cmd.Connection = con;
                            rdr = cmd.ExecuteReader();
                            Paper.Items.Clear();
                            while (rdr.Read())
                            {
                                Paper.Items.Add(rdr[0]);
                            }
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
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
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
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
                con.Close();
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
            Paper.Text = "";
            dtpExamDate.Text = System.DateTime.Today.ToString();
            cmbBranch.Enabled = false;
            cmbCourse.Enabled = false;
            cmbSemester.Enabled = false;
            cmbSubjectCode.Enabled = false;
            cmbSection.Enabled = false;
            sets.Text = "";
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
        public void reset2() {
            cmbExamName.Text = "";
            Paper.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
             try
            {
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
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
                    if (cmbExamName.Text == "Mid Term")
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
                                    string ct = "select studentno,subjectcode from Results where studentno='" + row.Cells[0].Value + "' and SubjectCode='" + cmbSubjectCode.Text + "' and term='"+cmbSemester.Text+"' and year='"+cmbSession.Text+"' and class='"+cmbCourse.Text+"'";
                                    cmd = new SqlCommand(ct);
                                    cmd.Connection = con;
                                    rdr = cmd.ExecuteReader();
                                    if (rdr.Read())
                                    {
                                        MessageBox.Show("Record Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        if ((rdr != null))
                                        {
                                            rdr.Close();
                                        }
                                        //return;
                                    }
                                    con.Close();
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "insert into Results(studentno,year,class,term,stream,level,subjectcode,subjectname,midterm,ExamDate,enrollmentnumber,studentname,Initials) VALUES (@d12,@d1,@d2,@d4,@d5,@d3,@d6,@d7,@d8,@d9,@d10,@d11,@d13)";
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
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "midterm"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                        // Prepare command for repeated execution
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
                        //btnSave.Enabled = false;
                        
                        reset2();
                    }
                   /* if (cmbExamName.Text == "Mid term")
                    {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                string cb = "update Results set year=@d1,class=@d2,Initials=@d13,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,midterm=@d8 where studentno=@d12 and subjectcode=@d6 ";
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
                                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "midterm"));
                                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                            
                                // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                if ((row.Cells[3].Value) != null)
                                {
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
                                    cmd.Parameters["@d13"].Value = staffname.Text;
                                    cmd.ExecuteNonQuery();
                                    
                                }
                            }
                        }
                        MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ///btnSave.Enabled = false;
                        con.Close();
                        reset2();
                    }*/
                    if (cmbExamName.Text == "End of Term")
                    {
                        resultscompute();
                        MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = false;
                        con.Close();
                    }
                }
                if (cmbCourse.Text == "S.4")
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
                    if (Paper.Text == "")
                    {
                        MessageBox.Show("Please select paper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Paper.Focus();
                        return;
                    }
                    if (sets.Text == "")
                    {
                        MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sets.Focus();
                        return;
                    }
                    if (Paper.Text == "Paper 1")
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
                                    /*con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string ct = "select studentno,subjectcode from Results where studentno='" + row.Cells[0].Value + "' and SubjectCode='" + cmbSubjectCode.Text + "' and Sets='" +sets.Text+ "'";
                                    cmd = new SqlCommand(ct);
                                    cmd.Connection = con;
                                    rdr = cmd.ExecuteReader();
                                    if (rdr.Read())
                                    {
                                        MessageBox.Show("Record Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        if ((rdr != null))
                                        {
                                            rdr.Close();
                                        }
                                        return;
                                    }*/
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string cb = "insert into Results(studentno,year,class,term,stream,level,subjectcode,subjectname,begginingofterm,ExamDate,enrollmentnumber,studentname,Initials,Sets) VALUES (@d12,@d1,@d2,@d4,@d5,@d3,@d6,@d7,@d8,@d9,@d10,@d11,@d13,@d14)";
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
                                    cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "begginingofterm"));
                                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                    cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));
                                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 10, "Sets"));
                                    // Prepare command for repeated execution
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
                                                cmd.Parameters["@d14"].Value = sets.Text;
                                                cmd.ExecuteNonQuery();
                                }
                            }
                        }
                       
                        //btnSave.Enabled = false;
                        resultscompute5();
                        MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                    if (Paper.Text == "Paper 2")
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
                        string cb = "update Results set year=@d1,class=@d2,Initials=@d13,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,midterm=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and Sets='"+sets.Text+"'";
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
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "midterm"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));

                        // Prepare command for repeated execution
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
                                    cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                    cmd.Parameters["@d13"].Value = staffname.Text;
                                    cmd.ExecuteNonQuery();

                                }
                            }
                        }
                       
                        ///btnSave.Enabled = false;
                        resultscompute6();
                        MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                       
                    }
                    if (Paper.Text == "Paper 1" || Paper.Text == "Paper 2" || Paper.Text == "")
                    {
                    }else{
            int papersno = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
            cmd = new SqlCommand(inquery6, con);
            papersno = Convert.ToInt32(cmd.ExecuteScalar());
          
             if (papersno > 3)
            {                           
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT endofterm FROM Results WHERE year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "' and Sets='" + sets.Text + "'";
                            cmd = new SqlCommand(inquery3, con);
                            object rdrs = cmd.ExecuteScalar();
                            if ( rdrs !=DBNull.Value)
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
                                             string cb = "update Results set year=@d1,class=@d2,Initials=@d13,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,FourthPaper=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
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
                                             cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "FourthPaper"));
                                             cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                             cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                                             cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));

                                             // Prepare command for repeated execution
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
                                             cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                             cmd.Parameters["@d13"].Value = staffname.Text;
                                             cmd.ExecuteNonQuery();
                                             con.Close();
                                        }
                                    }
                                }
                                resultscompute8();
                            }
                            else {
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
                                            string cb = "update Results set year=@d1,class=@d2,Initials=@d13,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,endofterm=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
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
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));

                                            // Prepare command for repeated execution
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
                                            cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                            cmd.Parameters["@d13"].Value = staffname.Text;
                                            cmd.ExecuteNonQuery();
                                            ///btnSave.Enabled = false;
                                        }
                                    }
                                }
                            }
             }
                            if (papersno == 3)
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
                                            string cb = "update Results set year=@d1,class=@d2,Initials=@d13,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,endofterm=@d8 where studentno=@d12 and subjectname=@d7 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
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
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "Initials"));

                                            // Prepare command for repeated execution
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
                                            cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                            cmd.Parameters["@d13"].Value = staffname.Text;
                                            cmd.ExecuteNonQuery();
                                            ///btnSave.Enabled = false;
                                        }
                                    }
                                }
                                resultscompute7();

                            }    
                            MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         con.Close();
                        }
                        
                    reset2();
                }
                if (cmbBranch.Text == "A Level")
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
                    if (Paper.Text == "")
                    {
                        MessageBox.Show("Please select Paper ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Paper.Focus();
                        return;
                    }
                    if (sets.Text == "")
                    {
                        MessageBox.Show("Please select Sets", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sets.Focus();
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
                                if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                                {
                                    row.Cells[3].Value = DBNull.Value;
                                }
                                else
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string cb = "insert into Leveladvanced(studentno,year,class,term,stream,level,subjectcode,subjectname,Paper,marks,ExamDate,enrollmentnumber,studentname,ExamName,Sets) VALUES (@d12,@d1,@d2,@d4,@d5,@d3,@d6,@d7,@d16,@d14,@d9,@d10,@d11,@d15,@d17)";
                                    cmd = new SqlCommand(cb);
                                    cmd.Connection = con;
                                    // Add Parameters to Command Parameters collection
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                    cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 20, "marks"));
                                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 40, "ExamName"));
                                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 40, "Paper"));
                                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Sets"));

                                    // Prepare command for repeated execution
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
                                    cmd.Parameters["@d14"].Value = row.Cells[3].Value;
                                    cmd.Parameters["@d15"].Value = "End Of Term";
                                    cmd.Parameters["@d16"].Value = Paper.Text;
                                    cmd.Parameters["@d17"].Value = sets.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // btnSave.Enabled = false;
                    //con.Close();
                    ALevelFinale();
                    reset2();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int paperfour = 0;
        public void resultscompute()
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
                        label14.Text = "";
                        label13.Text = "";
                       /* try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT begginingofterm FROM Results WHERE studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectcode = '" + cmbSubjectCode.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                            cmd = new SqlCommand(inquery3, con);
                            label3.Text = cmd.ExecuteScalar().ToString();
                            begginingofterm = int.Parse(label3.Text);
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }*/
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT midterm FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectcode = '" + cmbSubjectCode.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                            cmd = new SqlCommand(inquery3);
                            cmd.Connection = con;
                            var resultss = cmd.ExecuteScalar();
                            //rdr = cmd.ExecuteReader();
                            if (resultss == DBNull.Value)
                            { 
                                //label4.Text = cmd.ExecuteScalar().ToString();
                                midterm = 0;
                            }
                            else
                            {
                                midterm = Convert.ToInt32(resultss); 
                            }
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        try
                        {
                            if ((row.Cells[3].Value) == null || (row.Cells[3].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                            {
                                row.Cells[3].Value = DBNull.Value;
                                endofterm = 0;
                            }
                            else
                            {
                                endofterm = Convert.ToInt32(row.Cells[3].Value.ToString());
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        Average = (midterm + endofterm) / 2;

                
                        try
                        {
                            grade = "";
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" +Average+ "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                            cmd = new SqlCommand(inquery10, con);
                            grade = cmd.ExecuteScalar().ToString();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (grade == "F9")
                        {
                            aggregate = 9;
                        }
                        else if (grade == "P8")
                        {
                            aggregate = 8;
                        }
                        else if (grade == "P7")
                        {
                            aggregate = 7;
                        }
                        else if (grade == "C6")
                        {
                            aggregate = 6;
                        }
                        else if (grade == "C5")
                        {
                            aggregate = 5;
                        }
                        else if (grade == "C4")
                        {
                            aggregate = 4;
                        }
                        else if (grade == "C3")
                        {
                            aggregate = 3;
                        }
                        else if (grade == "D2")
                        {
                            aggregate = 2;
                        }
                        else if (grade == "D1")
                        {
                            aggregate = 1;
                        }
                        else
                        {
                            grade = "";
                            aggregate = 0;
                            MessageBox.Show("marks can not be less than 0 or more than 100");
                        }
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb = "update Results set stream=@d5,subjectname=@d7,endofterm=@d8,ExamDate=@d9,grade=@d13,Average=@d14,aggregate=@d15 where studentno=@d12 and subjectcode=@d6 and year=@d1 and class=@d2 and term=@d4 ";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        // Add Parameters to Command Parameters collection
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                        cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                        cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "aggregate"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));

                        // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted
                        cmd.Parameters["@d1"].Value = cmbSession.Text;
                        cmd.Parameters["@d2"].Value = cmbCourse.Text;
                        cmd.Parameters["@d3"].Value = cmbBranch.Text;
                        cmd.Parameters["@d4"].Value = cmbSemester.Text;
                        cmd.Parameters["@d5"].Value = cmbSection.Text;
                        cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                        cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                        cmd.Parameters["@d13"].Value = grade;
                        cmd.Parameters["@d14"].Value = Average;
                        cmd.Parameters["@d15"].Value = aggregate;
                        cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                        cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                        cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                        cmd.Parameters["@d17"].Value = staffname.Text;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            reset2();
        }

        public void resultscompute5()
        {
             int papersno = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
                        cmd = new SqlCommand(inquery6, con);
                        papersno = Convert.ToInt32(cmd.ExecuteScalar());
                        //MessageBox.Show(papersno.ToString(),"Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        if (papersno == 1)
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
                                        label14.Text = "";
                                        label13.Text = "";
                                       
                                       /* try
                                        {

                                            begginingofterm = Convert.ToInt32(row.Cells[3].Value.ToString());
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }*/
                                        if (row.Cells[3].Value == DBNull.Value)
                                        {
                                            Average = 0;
                                        }
                                        else
                                        {
                                            Average = Convert.ToInt32(row.Cells[3].Value.ToString());
                                        }
                                        try
                                        {
                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" +cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                                            cmd = new SqlCommand(inquery10, con);
                                            grade = cmd.ExecuteScalar().ToString();
                                            con.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        if (grade == "F9")
                                        {
                                            aggregate = 9;
                                        }
                                        else if (grade == "P8")
                                        {
                                            aggregate = 8;
                                        }
                                        else if (grade == "P7")
                                        {
                                            aggregate = 7;
                                        }
                                        else if (grade == "C6")
                                        {
                                            aggregate = 6;
                                        }
                                        else if (grade == "C5")
                                        {
                                            aggregate = 5;
                                        }
                                        else if (grade == "C4")
                                        {
                                            aggregate = 4;
                                        }
                                        else if (grade == "C3")
                                        {
                                            aggregate = 3;
                                        }
                                        else if (grade == "D2")
                                        {
                                            aggregate = 2;
                                        }
                                        else if (grade == "D1")
                                        {
                                            aggregate = 1;
                                        }
                                        else
                                        {
                                            MessageBox.Show("marks can not be less than 0 or more than 100");
                                        }
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string cb = "update Results set Initials=@d17,level=@d3,term=@d4,stream=@d5,ExamDate=@d9,grade=@d13,Average=@d14,aggregate=@d15 where studentno=@d12 and SubjectName=@d7 and year=@d1 and class=@d2 and term=@d4 and Sets=@d19 ";
                                        cmd = new SqlCommand(cb);
                                        cmd.Connection = con;
                                        // Add Parameters to Command Parameters collection
                                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                                        cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                                        cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "aggregate"));
                                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));
                                        cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Sets"));

                                        // Prepare command for repeated execution
                                        cmd.Prepare();
                                        // Data to be inserted
                                        cmd.Parameters["@d1"].Value = cmbSession.Text;
                                        cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                        cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                        cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                        cmd.Parameters["@d5"].Value = cmbSection.Text;
                                        cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                        cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                        cmd.Parameters["@d13"].Value = grade;
                                        cmd.Parameters["@d14"].Value = Average;
                                        cmd.Parameters["@d15"].Value = aggregate;
                                        cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                        cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                        cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                                        cmd.Parameters["@d17"].Value = staffname.Text;
                                        cmd.Parameters["@d19"].Value = sets.Text;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                }
                            }
                            reset2();
                        }

        }



        public void resultscompute6()
        {
            int papersno = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
            cmd = new SqlCommand(inquery6, con);
            papersno = Convert.ToInt32(cmd.ExecuteScalar());
         
             if (papersno == 2)
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
                            label14.Text = "";
                            label13.Text = "";
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT begginingofterm FROM Results WHERE studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "' and Sets='"+sets.Text+"'";
                                cmd = new SqlCommand(inquery3, con);
                                var resultss = cmd.ExecuteScalar();
                            //rdr = cmd.ExecuteReader();
                                if (resultss == DBNull.Value)
                                {
                                    begginingofterm = 0;
                                   
                                }
                                else {
                                    begginingofterm = Convert.ToInt32(resultss);
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            try
                            {
                                if (row.Cells[3].Value==DBNull.Value)
                                {
                                    midterm = 0;
                                }else{

                                midterm = Convert.ToInt32(row.Cells[3].Value.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            if (cmbCourse.Text == "S.4" && txtSubjectName.Text.Trim().Equals("english", StringComparison.InvariantCultureIgnoreCase))
                            {
                                Average = (begginingofterm + midterm); 
                            }
                            else
                            {
                                Average = (begginingofterm + midterm) / 2;
                            }

                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                                cmd = new SqlCommand(inquery10, con);
                                grade = cmd.ExecuteScalar().ToString();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            if (grade == "F9")
                            {
                                aggregate = 9;
                            }
                            else if (grade == "P8")
                            {
                                aggregate = 8;
                            }
                            else if (grade == "P7")
                            {
                                aggregate = 7;
                            }
                            else if (grade == "C6")
                            {
                                aggregate = 6;
                            }
                            else if (grade == "C5")
                            {
                                aggregate = 5;
                            }
                            else if (grade == "C4")
                            {
                                aggregate = 4;
                            }
                            else if (grade == "C3")
                            {
                                aggregate = 3;
                            }
                            else if (grade == "D2")
                            {
                                aggregate = 2;
                            }
                            else if (grade == "D1")
                            {
                                aggregate = 1;
                            }
                            else
                            {
                                MessageBox.Show("marks can not be less than 0 or more than 100");
                            }
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,grade=@d13,Average=@d14,aggregate=@d15 where studentno=@d12 and SubjectName=@d7 and year=@d1 and class=@d2 and term=@d4 and Sets=@d19 ";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "aggregate"));
                            cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Sets"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = cmbSession.Text;
                            cmd.Parameters["@d2"].Value = cmbCourse.Text;
                            cmd.Parameters["@d3"].Value = cmbBranch.Text;
                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                            cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                            cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d13"].Value = grade;
                            cmd.Parameters["@d14"].Value = Average;
                            cmd.Parameters["@d15"].Value = aggregate;
                            cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                            cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                            cmd.Parameters["@d17"].Value = staffname.Text;
                            cmd.Parameters["@d19"].Value = sets.Text;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                reset2();
            }
        }
        public void resultscompute7()
        {
            int papersno = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
            cmd = new SqlCommand(inquery6, con);
            papersno = Convert.ToInt32(cmd.ExecuteScalar());
           
             if (papersno == 3)
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
                            label14.Text = "";
                            label13.Text = "";
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT begginingofterm FROM Results WHERE studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                 var resultss = cmd.ExecuteScalar();
                            //rdr = cmd.ExecuteReader();
                                 if (resultss == DBNull.Value)
                                 {
                                     begginingofterm =0;
                                    
                                 }
                                 else {
                                     begginingofterm = Convert.ToInt32(resultss);
                                 }
                                 con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT midterm FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                var resultss = cmd.ExecuteScalar();
                                //rdr = cmd.ExecuteReader();
                                if (resultss == DBNull.Value)
                                {
                                    midterm = 0;

                                }
                                else
                                {
                                    midterm = Convert.ToInt32(resultss);
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                if (row.Cells[3].Value == DBNull.Value)
                                {
                                    endofterm = 0;
                                }
                                else
                                {
                                    endofterm = Convert.ToInt32(row.Cells[3].Value.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            Average = (begginingofterm + midterm + endofterm) / 3;


                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                                cmd = new SqlCommand(inquery10, con);
                                grade = cmd.ExecuteScalar().ToString();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            if (grade == "F9")
                            {
                                aggregate = 9;
                            }
                            else if (grade == "P8")
                            {
                                aggregate = 8;
                            }
                            else if (grade == "P7")
                            {
                                aggregate = 7;
                            }
                            else if (grade == "C6")
                            {
                                aggregate = 6;
                            }
                            else if (grade == "C5")
                            {
                                aggregate = 5;
                            }
                            else if (grade == "C4")
                            {
                                aggregate = 4;
                            }
                            else if (grade == "C3")
                            {
                                aggregate = 3;
                            }
                            else if (grade == "D2")
                            {
                                aggregate = 2;
                            }
                            else if (grade == "D1")
                            {
                                aggregate = 1;
                            }
                            else
                            {
                                MessageBox.Show("marks can not be less than 0 or more than 100");
                            }
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,subjectname=@d7,endofterm=@d8,ExamDate=@d9,grade=@d13,Average=@d14,aggregate=@d15 where studentno=@d12 and SubjectName=@d7 and year=@d1 and class=@d2 and term=@d4 and Sets=@d19 ";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "aggregate"));
                            cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Sets"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = cmbSession.Text;
                            cmd.Parameters["@d2"].Value = cmbCourse.Text;
                            cmd.Parameters["@d3"].Value = cmbBranch.Text;
                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                            cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                            cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d13"].Value = grade;
                            cmd.Parameters["@d14"].Value = Average;
                            cmd.Parameters["@d15"].Value = aggregate;
                            cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                            cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                            cmd.Parameters["@d17"].Value = staffname.Text;
                            cmd.Parameters["@d19"].Value = sets.Text;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                reset2();
            }
        }

        public void resultscompute8()
        {
            int papersno = 0;
            con = new SqlConnection(cs.DBConn);
            con.Open();
            String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
            cmd = new SqlCommand(inquery6, con);
            papersno = Convert.ToInt32(cmd.ExecuteScalar());
           
             if (papersno >= 4)
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
                            label14.Text = "";
                            label13.Text = "";
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT begginingofterm FROM Results WHERE studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "' and Sets='"+sets.Text+"'";
                                cmd = new SqlCommand(inquery3, con);
                                var resultss = cmd.ExecuteScalar();
                                //rdr = cmd.ExecuteReader();
                                if (resultss == DBNull.Value)
                                {
                                    begginingofterm = 0;

                                }
                                else
                                {
                                    begginingofterm = Convert.ToInt32(resultss);
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT midterm FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "' and Sets='" + sets.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                var resultss = cmd.ExecuteScalar();
                                //rdr = cmd.ExecuteReader();
                                if (resultss == DBNull.Value)
                                {
                                    midterm = 0;

                                }
                                else
                                {
                                    midterm = Convert.ToInt32(resultss);
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT endofterm FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and stream = '" + cmbSection.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "' and Sets='" + sets.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                var resultss = cmd.ExecuteScalar();
                                //rdr = cmd.ExecuteReader();
                                if (resultss == DBNull.Value)
                                {
                                    endofterm = 0;

                                }
                                else
                                {
                                    endofterm = Convert.ToInt32(resultss);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            try
                            {
                                if (row.Cells[3].Value==DBNull.Value)
                                {
                                    paperfour = 0;
                                }else{
                                paperfour = Convert.ToInt32(row.Cells[3].Value.ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            Average = (begginingofterm + midterm + endofterm + paperfour) / 4;


                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                                cmd = new SqlCommand(inquery10, con);
                                grade = cmd.ExecuteScalar().ToString();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            if (grade == "F9")
                            {
                                aggregate = 9;
                            }
                            else if (grade == "P8")
                            {
                                aggregate = 8;
                            }
                            else if (grade == "P7")
                            {
                                aggregate = 7;
                            }
                            else if (grade == "C6")
                            {
                                aggregate = 6;
                            }
                            else if (grade == "C5")
                            {
                                aggregate = 5;
                            }
                            else if (grade == "C4")
                            {
                                aggregate = 4;
                            }
                            else if (grade == "C3")
                            {
                                aggregate = 3;
                            }
                            else if (grade == "D2")
                            {
                                aggregate = 2;
                            }
                            else if (grade == "D1")
                            {
                                aggregate = 1;
                            }
                            else
                            {
                                MessageBox.Show("marks can not be less than 0 or more than 100");
                            }
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,grade=@d13,Average=@d14,aggregate=@d15 where studentno=@d12 and SubjectName=@d7 and year=@d1 and class=@d2 and term=@d4 and Sets=@d19 ";
                            cmd = new SqlCommand(cb);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "year"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "level"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "aggregate"));
                            cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));
                            cmd.Parameters.Add(new SqlParameter("@d19", System.Data.SqlDbType.NChar, 10, "Sets"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = cmbSession.Text;
                            cmd.Parameters["@d2"].Value = cmbCourse.Text;
                            cmd.Parameters["@d3"].Value = cmbBranch.Text;
                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                            cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                            cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d13"].Value = grade;
                            cmd.Parameters["@d14"].Value = Average;
                            cmd.Parameters["@d15"].Value = aggregate;
                            cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                            cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                            cmd.Parameters["@d17"].Value = staffname.Text;
                            cmd.Parameters["@d19"].Value = sets.Text;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                reset2();
            }
        }
        public void resultscompute2()
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
                        label14.Text = "";
                        label13.Text = "";    
                        Average = Convert.ToInt32(row.Cells[3].Value.ToString());
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                            cmd = new SqlCommand(inquery10, con);
                            grade = cmd.ExecuteScalar().ToString();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (grade == "F9")
                        {
                            aggregate = 9;
                        }
                        else if (grade == "P8")
                        {
                            aggregate = 8;
                        }
                        else if (grade == "P7")
                        {
                            aggregate = 7;
                        }
                        else if (grade == "C6")
                        {
                            aggregate = 6;
                        }
                        else if (grade == "C5")
                        {
                            aggregate = 5;
                        }
                        else if (grade == "C4")
                        {
                            aggregate = 4;
                        }
                        else if (grade == "C3")
                        {
                            aggregate = 3;
                        }
                        else if (grade == "D2")
                        {
                            aggregate = 2;
                        }
                        else if (grade == "D1")
                        {
                            aggregate = 1;
                        }
                        else
                        {
                            MessageBox.Show("marks can not be less than 0 or more than 100");
                        }
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,ExamDate=@d9,Paper1=@d15 where studentno=@d12 and subjectname=@d7 and year=@d1 and class=@d2 and term=@d4 and Sets='"+sets.Text+"' ";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        // Add Parameters to Command Parameters collection
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                        cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                        cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "Paper1"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));

                        // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted
                        cmd.Parameters["@d1"].Value = cmbSession.Text;
                        cmd.Parameters["@d2"].Value = cmbCourse.Text;
                        cmd.Parameters["@d3"].Value = cmbBranch.Text;
                        cmd.Parameters["@d4"].Value = cmbSemester.Text;
                        cmd.Parameters["@d5"].Value = cmbSection.Text;
                        cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                        cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                        cmd.Parameters["@d13"].Value = grade;
                        cmd.Parameters["@d14"].Value = Average;
                        cmd.Parameters["@d15"].Value = aggregate;
                        cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                        cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                        cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                        cmd.Parameters["@d17"].Value = staffname.Text;
                        cmd.ExecuteNonQuery();

                        int papersno = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "' ";
                        cmd = new SqlCommand(inquery6, con);
                        papersno = Convert.ToInt32(cmd.ExecuteScalar());
                        if (papersno == 1)
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb2 = "update Results set PaperFinal=@d3 where studentno=@d2 and subjectname=@d1 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' ";
                            cmd = new SqlCommand(cb2);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "subjectname"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "PaperFinal"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d2"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d3"].Value = aggregate;
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }
            reset2();
        }

        public void resultscompute3()
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
                        label14.Text = "";
                        label13.Text = "";
                        Average = Convert.ToInt32(row.Cells[3].Value.ToString());
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and  SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                            cmd = new SqlCommand(inquery10, con);
                            grade = cmd.ExecuteScalar().ToString();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (grade == "F9")
                        {
                            aggregate = 9;
                        }
                        else if (grade == "P8")
                        {
                            aggregate = 8;
                        }
                        else if (grade == "P7")
                        {
                            aggregate = 7;
                        }
                        else if (grade == "C6")
                        {
                            aggregate = 6;
                        }
                        else if (grade == "C5")
                        {
                            aggregate = 5;
                        }
                        else if (grade == "C4")
                        {
                            aggregate = 4;
                        }
                        else if (grade == "C3")
                        {
                            aggregate = 3;
                        }
                        else if (grade == "D2")
                        {
                            aggregate = 2;
                        }
                        else if (grade == "D1")
                        {
                            aggregate = 1;
                        }
                        else
                        {
                            MessageBox.Show("marks can not be less than 0 or more than 100");
                        }
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,Paper2=@d15 where studentno=@d12 and subjectname=@d7 and year=@d1 and class=@d2 and term=@d4 ";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        // Add Parameters to Command Parameters collection
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                        cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                        cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "Paper2"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));

                        // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted
                        cmd.Parameters["@d1"].Value = cmbSession.Text;
                        cmd.Parameters["@d2"].Value = cmbCourse.Text;
                        cmd.Parameters["@d3"].Value = cmbBranch.Text;
                        cmd.Parameters["@d4"].Value = cmbSemester.Text;
                        cmd.Parameters["@d5"].Value = cmbSection.Text;
                        cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                        cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                        cmd.Parameters["@d13"].Value = grade;
                        cmd.Parameters["@d14"].Value = Average;
                        cmd.Parameters["@d15"].Value = aggregate;
                        cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                        cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                        cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                        cmd.Parameters["@d17"].Value = staffname.Text;
                        cmd.ExecuteNonQuery();
                        int papersno = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
                        cmd = new SqlCommand(inquery6, con);
                        papersno = Convert.ToInt32(cmd.ExecuteScalar());
                        if (papersno == 2)
                        {
                            double paper1result = 0;
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT Paper1 FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "'  and subjectname = '" + txtSubjectName.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                paper1result = Convert.ToDouble(cmd.ExecuteScalar());
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                           
                            double adds = (aggregate + paper1result) / 2;
                            int averageaggregate = (int)Math.Round(adds);
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb2 = "update Results set PaperFinal=@d3 where studentno=@d2 and subjectname=@d1 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' ";
                            cmd = new SqlCommand(cb2);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "subjectname"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "PaperFinal"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d2"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d3"].Value = averageaggregate;
                            cmd.ExecuteNonQuery();
                        }

                    }
                }
            }
            reset2();
        }
        public void resultscompute4()
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
                        label14.Text = "";
                        label13.Text = "";
                        Average = Convert.ToInt32(row.Cells[3].Value.ToString());
                        try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + Average + "' and MaxMark >='" + Average + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                            cmd = new SqlCommand(inquery10, con);
                            grade = cmd.ExecuteScalar().ToString();
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (grade == "F9")
                        {
                            aggregate = 9;
                        }
                        else if (grade == "P8")
                        {
                            aggregate = 8;
                        }
                        else if (grade == "P7")
                        {
                            aggregate = 7;
                        }
                        else if (grade == "C6")
                        {
                            aggregate = 6;
                        }
                        else if (grade == "C5")
                        {
                            aggregate = 5;
                        }
                        else if (grade == "C4")
                        {
                            aggregate = 4;
                        }
                        else if (grade == "C3")
                        {
                            aggregate = 3;
                        }
                        else if (grade == "D2")
                        {
                            aggregate = 2;
                        }
                        else if (grade == "D1")
                        {
                            aggregate = 1;
                        }
                        else
                        {
                            MessageBox.Show("marks can not be less than 0 or more than 100");
                        }
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        string cb = "update Results set year=@d1,class=@d2,Initials=@d17,level=@d3,term=@d4,stream=@d5,subjectname=@d7,ExamDate=@d9,Paper3=@d15 where studentno=@d12 and subjectname=@d7 and year=@d1 and class=@d2 and term=@d4 ";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        // Add Parameters to Command Parameters collection
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "Session"));
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "Course"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "Branch"));
                        cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Semester"));
                        cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Section"));
                        cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                        cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 40, "SubjectName"));
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
                        cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                        cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 15, "studentno"));
                        cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.NChar, 10, "grade"));
                        cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 10, "Average"));
                        cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.Int, 10, "Paper3"));
                        cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Initials"));

                        // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted
                        cmd.Parameters["@d1"].Value = cmbSession.Text;
                        cmd.Parameters["@d2"].Value = cmbCourse.Text;
                        cmd.Parameters["@d3"].Value = cmbBranch.Text;
                        cmd.Parameters["@d4"].Value = cmbSemester.Text;
                        cmd.Parameters["@d5"].Value = cmbSection.Text;
                        cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                        cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                        cmd.Parameters["@d13"].Value = grade;
                        cmd.Parameters["@d14"].Value = Average;
                        cmd.Parameters["@d15"].Value = aggregate;
                        cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                        cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                        cmd.Parameters["@d8"].Value = row.Cells[3].Value;
                        cmd.Parameters["@d17"].Value = staffname.Text;
                        cmd.ExecuteNonQuery();

                        int papersno = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery6 = "SELECT COUNT(SubjectCode) FROM SubjectInfo WHERE SubjectName='" + txtSubjectName.Text + "' and CourseName='" + cmbCourse.Text + "'";
                        cmd = new SqlCommand(inquery6, con);
                        papersno = Convert.ToInt32(cmd.ExecuteScalar());
                        if (papersno > 2)
                        {
                            double paper1result = 0;
                            double paper2result = 0;
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery3 = "SELECT Paper1,Paper2 FROM Results WHERE  studentno='" + row.Cells[0].Value + "' and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' and level = '" + cmbBranch.Text + "' and subjectname = '" + txtSubjectName.Text + "'";
                                cmd = new SqlCommand(inquery3, con);
                                rdr=cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                    paper1result = Convert.ToDouble(rdr.GetInt32(0));
                                    paper2result = Convert.ToDouble(rdr.GetInt32(1));
                                }
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            double adds=(aggregate + paper1result + paper2result) / 3;
                            int averageaggregate = (int)Math.Round(adds);
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb2 = "update Results set PaperFinal=@d3 where studentno=@d2 and subjectname=@d1 and year = '" + cmbSession.Text + "' and class = '" + cmbCourse.Text + "' and term = '" + cmbSemester.Text + "' ";
                            cmd = new SqlCommand(cb2);
                            cmd.Connection = con;
                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "subjectname"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 15, "studentno"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.Int, 10, "PaperFinal"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            cmd.Parameters["@d1"].Value = txtSubjectName.Text;
                            cmd.Parameters["@d2"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d3"].Value = averageaggregate;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            reset2();
        }
        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBranch.Text == "O Level")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    if (cmbExamName.Text == "beginning of term")
                    {
                        string cb = "update Results set stream=@d5,subjectname=@d7,ExamDate=@d9,begginingofterm=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
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
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "begginingofterm"));
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

                    if (cmbExamName.Text == "Mid Term")
                    {
                        string cb = "update Results set stream=@d5,subjectname=@d7,ExamDate=@d9,midterm=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
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
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "midterm"));
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

                    if (cmbExamName.Text == "End of Term")
                    {
                        string cb = "update Results set stream=@d5,subjectname=@d7,ExamDate=@d9,endofterm=@d8 where studentno=@d12 and subjectcode=@d6 and  year=@d1 and class=@d2 and level=@d3 and term=@d4 ";
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
                        cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 20, "endofterm"));
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
                        resultscompute();
                        reset2();
                    }
                    con.Close();
                    MessageBox.Show("Successfully updated", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Update_record.Enabled = false;
                }
                if (cmbBranch.Text == "A Level")
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
                                else
                                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb = "update Leveladvanced set stream=@d5,level=@d3,subjectname=@d7,ExamDate=@d9,enrollmentnumber=@d10,studentname=@d11, marks=@d14  where ExamName=@d15 and studentno=@d12 and Paper=@d16 and subjectcode=@d6 and Sets=@d17 and  year=@d1 and class=@d2 and level=@d3 and term=@d4";
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
                    cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                    cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.NChar, 20, "studentno"));
                    cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.Int, 20, "marks"));
                    cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 20, "enrollmentnumber"));
                    cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 40, "studentname"));
                    cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 40, "ExamName"));
                    cmd.Parameters.Add(new SqlParameter("@d16", System.Data.SqlDbType.NChar, 40, "Paper"));
                    cmd.Parameters.Add(new SqlParameter("@d17", System.Data.SqlDbType.NChar, 10, "Sets"));
                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                   
                                    cmd.Parameters["@d1"].Value = cmbSession.Text;
                                    cmd.Parameters["@d2"].Value = cmbCourse.Text;
                                    cmd.Parameters["@d3"].Value = cmbBranch.Text;
                                    cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                    cmd.Parameters["@d5"].Value = cmbSection.Text;
                                    cmd.Parameters["@d6"].Value = cmbSubjectCode.Text;
                                    cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                    cmd.Parameters["@d15"].Value = cmbExamName.Text;
                                    cmd.Parameters["@d9"].Value = dtpExamDate.Text;
                                    cmd.Parameters["@d12"].Value = row.Cells[0].Value;
                                    cmd.Parameters["@d10"].Value = row.Cells[1].Value;
                                    cmd.Parameters["@d11"].Value = row.Cells[2].Value;
                                    cmd.Parameters["@d14"].Value = row.Cells[3].Value;
                                    cmd.Parameters["@d16"].Value = Paper.Text;
                                    cmd.Parameters["@d17"].Value = sets.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    MessageBox.Show("Successfully Updated", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // btnSave.Enabled = false;
                    con.Close();
                    ALevelFinale();
                    reset2();
                }
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
                string cq = "delete from Results where year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and level='" + cmbBranch.Text + "' and term='" + cmbSemester.Text + "' and stream='" + cmbSection.Text + "' and SubjectCode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "'";
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

        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            if (cmbBranch.Text == "A Level")
            {
                Paper.Items.Clear();
                Paper.Text = "";
                Paper.Enabled = true;
                cmbExamName.Enabled = true;
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string ct = "select distinct RTRIM(Paper) from SubjectInfoA where Class = '" + cmbCourse.Text + "' and SubjectCodeA ='" + cmbSubjectCode.Text+ "'";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    Paper.Items.Clear();
                    while (rdr.Read())
                    {
                        Paper.Items.Add(rdr[0]);
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        int pupiltotal=0;
        int maxi=0;
        int Totalagg=0;
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
                                String inquery3 = "SELECT COUNT( DISTINCT studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and stream='"+cmbSection.Text+"' ";
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
                                cmd.Parameters["@d24"].Value = counter;
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
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void pupiltotals1()
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
                                String inquery3 = "SELECT SUM(Average) FROM Results WHERE  studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "' ";
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
                                String inquery3 = "SELECT SUM(aggregate) FROM Results WHERE  studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='"+sets.Text+"' ";
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
                                String inquery3 = "SELECT COUNT( DISTINCT studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'  and Sets='" + sets.Text + "' ";
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
                            string cb = "insert into Positions(studentno,year,class,term,total,position,maxim,Totalagg,Sets) VALUES (@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27)";
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
                            cmd.Parameters.Add(new SqlParameter("@d27", System.Data.SqlDbType.NChar, 10, "Sets"));
                            cmd.Prepare();
                            cmd.Parameters["@d19"].Value = row.Cells[0].Value;
                            cmd.Parameters["@d20"].Value = cmbSession.Text;
                            cmd.Parameters["@d21"].Value = cmbCourse.Text;
                            cmd.Parameters["@d22"].Value = cmbSemester.Text;
                            cmd.Parameters["@d23"].Value = pupiltotal;
                            cmd.Parameters["@d24"].Value = counter;
                            cmd.Parameters["@d25"].Value = maxi;
                            cmd.Parameters["@d26"].Value = Totalagg;
                            cmd.Parameters["@d27"].Value = sets.Text;
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
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        int[] myNum =new int[500];
        int[] maxim0 = new int[500];
        string[] studentno= new string[500];
        int counter=0;
        public void posit()
        {
            if (cmbBranch.Text == "A Level")
            {
                MessageBox.Show("Please positions are assigned to O Level students only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            try
            {
                try
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT studentno,total,maxim FROM Positions WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Stream='"+cmbSection.Text+"' ORDER BY total DESC";
                            cmd = new SqlCommand(inquery3, con);
                            rdr = cmd.ExecuteReader();        
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            dt.Load(rdr);
                            da.Fill(dt);                          
                        foreach (DataRow dr in dt.Rows)
                        {
                            studentno[counter] = dr.Field<string>(0);
                            myNum[counter] = dr.Field<int>(1);
                            maxim0[counter] = dr.Field<int>(2);
                            counter++;
                        }
                            con.Close();
                        }
                            catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        try{
                        for (int i = 0; i < myNum.Length; i++)
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery4 = "Update Positions set position=@d1 WHERE studentno ='" + studentno[i] + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                            cmd = new SqlCommand(inquery4, con);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.Int, 10, "position"));
                            cmd.Prepare();
                            cmd.Parameters["@d1"].Value = (i-(maxim0[i]-1));
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        }
                            catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void posit1()
        {
            if (cmbBranch.Text == "A Level")
            {
                MessageBox.Show("Please positions are assigned to O Level students only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            try
            {
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT studentno,total,maxim FROM Positions WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "' ORDER BY total DESC";
                    cmd = new SqlCommand(inquery3, con);
                    rdr = cmd.ExecuteReader();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.Load(rdr);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        studentno[counter] = dr.Field<string>(0);
                        myNum[counter] = dr.Field<int>(1);
                        maxim0[counter] = dr.Field<int>(2);
                        counter++;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    for (int i = 0; i < myNum.Length; i++)
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery4 = "Update Positions set position=@d1 WHERE studentno ='" + studentno[i] + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                        cmd = new SqlCommand(inquery4, con);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.Int, 10, "position"));
                        cmd.Prepare();
                        cmd.Parameters["@d1"].Value = (i - (maxim0[i] - 1));
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbBranch.Text == "A Level")
            {
                MessageBox.Show("Please that is for O Level Classes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (MessageBox.Show("Have all the Results been Input?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.3" || cmbCourse.Text == "S.2")
                {
                pupiltotals();
                }else if( cmbCourse.Text=="S.4"){
                pupiltotals1();
                }
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
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.3" || cmbCourse.Text == "S.2")
                {
                    try { 
                    posit();
                    Olevelaggregate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (cmbCourse.Text == "S.4")
                {
                    try { 
                    posit1();
                    Olevelaggregate1();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (cmbBranch.Text == "O Level")
                {

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
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.3" || cmbCourse.Text == "S.2")
                {
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='D1'";
                        cmd = new SqlCommand(inquery3, con);
                        label32.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='D2'";
                        cmd = new SqlCommand(inquery3, con);
                        label33.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C3'";
                        cmd = new SqlCommand(inquery3, con);
                        label34.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C4'";
                        cmd = new SqlCommand(inquery3, con);
                        label35.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C5'";
                        cmd = new SqlCommand(inquery3, con);
                        label36.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='C6'";
                        cmd = new SqlCommand(inquery3, con);
                        label37.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='P7'";
                        cmd = new SqlCommand(inquery3, con);
                        label38.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='P8'";
                        cmd = new SqlCommand(inquery3, con);
                        label39.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and grade='F9'";
                        cmd = new SqlCommand(inquery3, con);
                        label40.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    panel2.Visible = true;
                }
                if (cmbCourse.Text == "S.4")
                {
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='D1'";
                        cmd = new SqlCommand(inquery3, con);
                        label32.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='D2'";
                        cmd = new SqlCommand(inquery3, con);
                        label33.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='C3'";
                        cmd = new SqlCommand(inquery3, con);
                        label34.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='C4'";
                        cmd = new SqlCommand(inquery3, con);
                        label35.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='C5'";
                        cmd = new SqlCommand(inquery3, con);
                        label36.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='C6'";
                        cmd = new SqlCommand(inquery3, con);
                        label37.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='P7'";
                        cmd = new SqlCommand(inquery3, con);
                        label38.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='P8'";
                        cmd = new SqlCommand(inquery3, con);
                        label39.Text = cmd.ExecuteScalar().ToString();
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
                        String inquery3 = "SELECT COUNT(studentno) FROM Results WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Sets='" + sets.Text + "' and grade='F9'";
                        cmd = new SqlCommand(inquery3, con);
                        label40.Text = cmd.ExecuteScalar().ToString();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    panel2.Visible = true;
                }
            }
            if (cmbBranch.Text == "A Level")
            {
                try
                {
                    label60.Text = cmbCourse.Text + " Class";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    label61.Text = txtSubjectName.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                try
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='A' and Sets='"+sets.Text+"'";
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='B' and Sets='" + sets.Text + "'";
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='C' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    label55.Text = cmd.ExecuteScalar().ToString();
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='D' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    label56.Text = cmd.ExecuteScalar().ToString();
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='E' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    label57.Text = cmd.ExecuteScalar().ToString();
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='O' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    label58.Text = cmd.ExecuteScalar().ToString();
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
                    String inquery3 = "SELECT COUNT(studentno) FROM LevelAdvancedGrade WHERE  year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "'and stream='" + cmbSection.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and Grade='F' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery3, con);
                    label59.Text = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
                panel3.Visible = true;
            }
        }
        public void ALevelFinale()
        {
          
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if ((row.Cells[3].Value) != null)
                        {
                            label14.Text = "";
                            label13.Text = "";
                            
                            try
                            {
                                endofterm = Convert.ToInt32(row.Cells[3].Value.ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                           
                            try
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                String inquery10 = "SELECT Grade FROM SubjectGrade WHERE MinMark<='" + endofterm + "' and MaxMark >='" + endofterm + "' and Class = '" + cmbCourse.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' ORDER BY GradeID DESC";
                                cmd = new SqlCommand(inquery10, con);
                                grade = cmd.ExecuteScalar().ToString();
                                con.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            if (grade == "F9")
                            {
                                aggregate = 9;
                            }
                            else if (grade == "P8")
                            {
                                aggregate = 8;
                            }
                            else if (grade == "P7")
                            {
                                aggregate = 7;
                            }
                            else if (grade == "C6")
                            {
                                aggregate = 6;
                            }
                            else if (grade == "C5")
                            {
                                aggregate = 5;
                            }
                            else if (grade == "C4")
                            {
                                aggregate = 4;
                            }
                            else if (grade == "C3")
                            {
                                aggregate = 3;
                            }
                            else if (grade == "D2")
                            {
                                aggregate = 2;
                            }
                            else if (grade == "D1")
                            {
                                aggregate = 1;
                            }
                            else
                            {
                                MessageBox.Show("marks can not be less than 0 or more than 100");
                            }
                           
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string ct = "select studentno,year,class,term,stream,level,subjectcode,subjectname,Paper,Average,Aggregate from LeveladvancedFinale where studentno='" + row.Cells[0].Value + "' and subjectcode='" + cmbSubjectCode.Text + "' and Paper='" + Paper.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(ct);
                                        cmd.Connection = con;
                                        rdr = cmd.ExecuteReader();
                                        if (rdr.Read())
                                        {

                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            string cb = "update LeveladvancedFinale set year=@d2,class=@d3,term=@d4,stream=@d5,level=@d6,subjectname=@d8,Average =@d10,Aggregate=@d12,Initials=@d14 where studentno =@d1 and subjectcode=@d7 and Paper=@d9 and Sets='" + sets.Text + "'";
                                            cmd = new SqlCommand(cb);
                                            cmd.Connection = con;
                                            // Add Parameters to Command Parameters collection
                                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "year"));
                                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "class"));
                                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "level"));
                                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 40, "Paper"));
                                            cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 20, "Average"));
                                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Aggregate"));
                                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 20, "Initials"));
                                            // Prepare command for repeated execution
                                            cmd.Prepare();
                                            // Data to be inserted
                                            cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                            cmd.Parameters["@d2"].Value = cmbSession.Text;
                                            cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                                            cmd.Parameters["@d6"].Value = cmbBranch.Text;
                                            cmd.Parameters["@d7"].Value = cmbSubjectCode.Text;
                                            cmd.Parameters["@d8"].Value = txtSubjectName.Text;
                                            cmd.Parameters["@d9"].Value = Paper.Text;
                                            cmd.Parameters["@d10"].Value =endofterm;
                                            cmd.Parameters["@d12"].Value = aggregate;
                                            cmd.Parameters["@d14"].Value = staffname.Text;
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            if ((rdr != null))
                                            {
                                                rdr.Close();
                                            }
                                            return;
                                        }
                                        else
                                        {

                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            string cb = "insert into LeveladvancedFinale(studentno,year,class,term,stream,level,subjectcode,subjectname,Paper,Average,Aggregate,Initials,Sets) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d12,@d14,@d15)";
                                            cmd = new SqlCommand(cb);
                                            cmd.Connection = con;
                                            // Add Parameters to Command Parameters collection
                                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "year"));
                                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "class"));
                                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "level"));
                                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 40, "Paper"));
                                            cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.Int, 20, "Average"));
                                            cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "Aggregate"));
                                            cmd.Parameters.Add(new SqlParameter("@d14", System.Data.SqlDbType.NChar, 20, "Initials"));
                                            cmd.Parameters.Add(new SqlParameter("@d15", System.Data.SqlDbType.NChar, 10, "Sets"));
                                            // Prepare command for repeated execution
                                            cmd.Prepare();
                                            // Data to be inserted
                                            cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                            cmd.Parameters["@d2"].Value = cmbSession.Text;
                                            cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                                            cmd.Parameters["@d6"].Value = cmbBranch.Text;
                                            cmd.Parameters["@d7"].Value = cmbSubjectCode.Text;
                                            cmd.Parameters["@d8"].Value = txtSubjectName.Text;
                                            cmd.Parameters["@d9"].Value = Paper.Text;
                                            cmd.Parameters["@d10"].Value = endofterm;
                                            cmd.Parameters["@d12"].Value = aggregate;
                                            cmd.Parameters["@d14"].Value = staffname.Text;
                                            cmd.Parameters["@d15"].Value = sets.Text;
                                            cmd.ExecuteNonQuery();
                                            //MessageBox.Show("success","Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            con.Close();
                                        }
                        }
                    }
                }
            
        }
       
        public void ALevelFinaleGrade()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT Photo FROM StudentRegistration WHERE ScholarNo like '%" + row.Cells[0].Value+ "%'";
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            MemoryStream stream = new MemoryStream();
                            byte[] image = (byte[])rdr["Photo"];
                            stream.Write(image, 0, image.Length);
                            Bitmap bitmap = new Bitmap(stream);
                            pictureBox1.Image = bitmap;
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
                    int[] mypapers = new int[200];
                    string[] mypapers2 = new string[200];
                    string ALevelGrade="X";
                    int AlevelPoints=0;
                    int countpapers = 0;
                    int studentcountpapers=0;
                    int papersum = 0;
                        try
                        {
                            label33.Text = "";
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery3 = "SELECT COUNT(subjectcode) FROM LeveladvancedFinale WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='"+sets.Text+"'";
                            cmd = new SqlCommand(inquery3, con);
                            label33.Text = cmd.ExecuteScalar().ToString();
                            studentcountpapers = Convert.ToInt32(label33.Text);
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        try { 
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery4 = "SELECT aggregate,subjectname FROM LeveladvancedFinale WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and subjectname='" + txtSubjectName.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='" + sets.Text + "'";
                            cmd = new SqlCommand(inquery4, con);
                            rdr = cmd.ExecuteReader();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            dt.Load(rdr);
                            rdr.Close();
                            da.Fill(dt);
                           
                            if (studentcountpapers == 1)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    mypapers[countpapers] = dr.Field<int>(0);
                                    countpapers++;
                                    if (mypapers[0] == 1)
                                    {
                                        ALevelGrade = "D1";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 2)
                                    {
                                        ALevelGrade = "D2";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 3)
                                    {
                                        ALevelGrade = "C3";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 4)
                                    {
                                        ALevelGrade = "C4";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 5)
                                    {
                                        ALevelGrade = "C5";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 6)
                                    {
                                        ALevelGrade = "C6";
                                        AlevelPoints = 1;
                                    }
                                    else if (mypapers[0] == 7)
                                    {
                                        ALevelGrade = "P7";
                                        AlevelPoints = 0;
                                    }
                                    else if (mypapers[0] == 8)
                                    {
                                        ALevelGrade = "P8";
                                        AlevelPoints = 0;
                                    }
                                    else
                                    {
                                        ALevelGrade = "F9";
                                        AlevelPoints = 0;
                                    }
                                }
                                con.Close();
                            }
                            if (studentcountpapers == 2)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    mypapers[countpapers] = dr.Field<int>(0);
                                     mypapers2[countpapers] = dr.Field<string>(1);
                                    countpapers++;
                                    papersum = mypapers[0] + mypapers[1];
                                    int averagepapersum = papersum / 2;
                                    if(mypapers2[0].Trim().Equals("SUB ICT",StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (averagepapersum==1) {
                                            ALevelGrade = "D1";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 2)
                                        {
                                            ALevelGrade = "D2";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 3)
                                        {
                                            ALevelGrade = "C3";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 4)
                                        {
                                            ALevelGrade = "C4";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 5)
                                        {
                                            ALevelGrade = "C5";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 6)
                                        {
                                            ALevelGrade = "C6";
                                            AlevelPoints = 1;
                                        }
                                        else if (averagepapersum == 7)
                                        {
                                            ALevelGrade = "P7";
                                            AlevelPoints = 0;
                                        }
                                        else if (averagepapersum == 8)
                                        {
                                            ALevelGrade = "P7";
                                            AlevelPoints = 0;
                                        }
                                        else if (averagepapersum == 9)
                                        {
                                            ALevelGrade = "F9";
                                            AlevelPoints = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (mypapers[0] <= 2 && mypapers[1] <= 2)
                                        {

                                            ALevelGrade = "A";
                                            AlevelPoints = 6;
                                        }
                                        else if (mypapers[0] <= 3 && mypapers[1] <= 3)
                                        {
                                            ALevelGrade = "B";
                                            AlevelPoints = 5;
                                        }
                                        else if (mypapers[0] <= 4 && mypapers[1] <= 4)
                                        {
                                            ALevelGrade = "C";
                                            AlevelPoints = 4;
                                        }
                                        else if (mypapers[0] <= 5 && mypapers[1] <= 5)
                                        {
                                            ALevelGrade = "D";
                                            AlevelPoints = 3;
                                        }
                                        else if ((mypapers[0] <= 6 && mypapers[1] <= 6) || (mypapers[0] <= 8 && mypapers[1] <= 8 && papersum <= 12))
                                        {
                                            ALevelGrade = "E";
                                            AlevelPoints = 2;
                                        }
                                        else if ((((mypapers[0] >= 7 && mypapers[0] <= 8) && (mypapers[1] >= 7 && mypapers[1] <= 8)) || papersum <= 16) || ((mypapers[0] <= 6 && mypapers[1] == 9) || (mypapers[0] == 9 && mypapers[1] <= 6)))
                                        {
                                            ALevelGrade = "O";
                                            AlevelPoints = 1;
                                        }
                                        else if ((mypapers[0] >= 8 && mypapers[1] == 9) || (mypapers[0] == 9 && mypapers[1] >= 8))
                                        {
                                            ALevelGrade = "F";
                                            AlevelPoints = 0;
                                        }
                                        else
                                        {
                                            ALevelGrade = "X";
                                            AlevelPoints = 0;
                                        }
                                    }
                                }
                                con.Close();
                            }
                            if (studentcountpapers == 3)
                            {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        mypapers[countpapers] = dr.Field<int>(0);
                                        countpapers++;
                                            papersum = mypapers[0] + mypapers[1] + mypapers[2];
                                           if ((mypapers[0] <= 3 && mypapers[1] <= 2 && mypapers[2] <= 2) || (mypapers[0] <= 2 && mypapers[1] <= 3 && mypapers[2] <= 2) || (mypapers[0] <= 2 && mypapers[1] <= 2 && mypapers[2] <= 3))
                                            {
                                                ALevelGrade = "A";
                                                AlevelPoints = 6;
                                            }
                                            else if ((mypapers[0] <= 4 && mypapers[1] <= 3 && mypapers[2] <= 3) || (mypapers[0] <= 3 && mypapers[1] <= 4 && mypapers[2] <= 3) || (mypapers[0] <= 3 && mypapers[1] <= 3 && mypapers[2] <= 4))
                                            {
                                                ALevelGrade = "B";
                                                AlevelPoints = 5;
                                            }
                                            else if ((mypapers[0] <= 5 && mypapers[1] <= 4 && mypapers[2] <= 4) || (mypapers[0] <= 4 && mypapers[1] <= 5 && mypapers[2] <= 4) || (mypapers[0] <= 4 && mypapers[1] <= 4 && mypapers[2] <= 5))
                                            {
                                                ALevelGrade = "C";
                                                AlevelPoints = 4;
                                            }
                                            else if ((mypapers[0] <= 6 && mypapers[1] <= 5 && mypapers[2] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 6 && mypapers[2] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 5 && mypapers[2] <= 6))
                                            {
                                                ALevelGrade = "D";
                                                AlevelPoints = 3;
                                            }
                                           else if ((mypapers[0] <= 7 && mypapers[1] <= 6 && mypapers[2] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 7 && mypapers[2] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 6 && mypapers[2] <= 7) || (mypapers[0] <= 8 && mypapers[1] <= 5 && mypapers[2] <= 6) || (mypapers[0] <= 8 && mypapers[1] <= 6 && mypapers[2] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 8 && mypapers[2] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 8 && mypapers[2] <= 5) || (mypapers[0] <= 6 && mypapers[1] <= 5 && mypapers[2] <= 8) || (mypapers[0] <= 5 && mypapers[1] <= 6 && mypapers[2] <= 8))
                                            {
                                                ALevelGrade = "E";
                                                AlevelPoints = 2;
                                            }
                                            else if ((mypapers[0] <= 7 && mypapers[1] <= 7 && mypapers[2] <= 7) || (mypapers[0] <= 8 && mypapers[1] <= 8 && mypapers[2] <= 8) || (mypapers[0] <= 8 && mypapers[1] <= 8 && mypapers[2] <= 9) || (mypapers[0] <= 8 && mypapers[1] <= 9 && mypapers[2] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 8 && mypapers[2] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 9 && mypapers[2] <= 7) || (mypapers[0] <= 9 && mypapers[1] <= 7 && mypapers[2] <= 9) || (mypapers[0] <= 7 && mypapers[1] <= 9 && mypapers[2] <= 9))
                                            {
                                                ALevelGrade = "O";
                                                AlevelPoints = 1;
                                            }
                                            else if ((mypapers[0] <= 9 && mypapers[1] <= 9 && mypapers[2] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 9 && mypapers[2] <= 9))
                                            {
                                                ALevelGrade = "F";
                                                AlevelPoints = 0;
                                            }
                                            else
                                            {
                                                ALevelGrade = "X";
                                                AlevelPoints = 0;
                                            }
                                    }
                                con.Close();
                            }
                            if (studentcountpapers == 4)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    mypapers[countpapers] = dr.Field<int>(0);
                                    countpapers++;
                                    papersum = mypapers[0] + mypapers[1] + mypapers[2] + mypapers[3];
                                    if ((mypapers[0] <= 3 && mypapers[1] <= 2 && mypapers[2] <= 2 && mypapers[3] <= 2) || (mypapers[0] <= 2 && mypapers[1] <= 3 && mypapers[2] <= 2 && mypapers[3] <= 2) || (mypapers[0] <= 2 && mypapers[1] <= 2 && mypapers[2] <= 3 && mypapers[3] <= 2) || (mypapers[0] <= 2 && mypapers[1] <= 2 && mypapers[2] <= 2 && mypapers[3] <= 3))
                                    {
                                        ALevelGrade = "A";
                                        AlevelPoints = 6;
                                    }
                                    else if ((mypapers[0] <= 4 && mypapers[1] <= 3 && mypapers[2] <= 3 && mypapers[3] <= 3) || (mypapers[0] <= 3 && mypapers[1] <= 4 && mypapers[2] <= 3 && mypapers[3] <= 3) || (mypapers[0] <= 3 && mypapers[1] <= 3 && mypapers[2] <= 4 && mypapers[3] <= 3) || (mypapers[0] <= 3 && mypapers[1] <= 3 && mypapers[2] <= 3 && mypapers[3] <= 4))
                                    {
                                        ALevelGrade = "B";
                                        AlevelPoints = 5;
                                    }
                                    else if ((mypapers[0] <= 5 && mypapers[1] <= 4 && mypapers[2] <= 4 && mypapers[3] <= 4) || (mypapers[0] <= 4 && mypapers[1] <= 5 && mypapers[2] <= 4 && mypapers[3] <= 4) || (mypapers[0] <= 4 && mypapers[1] <= 4 && mypapers[2] <= 5 && mypapers[3] <= 4) || (mypapers[0] <= 4 && mypapers[1] <= 4 && mypapers[2] <= 4 && mypapers[3] <= 5))
                                    {
                                        ALevelGrade = "C";
                                        AlevelPoints = 4;
                                    }
                                    else if ((mypapers[0] <= 6 && mypapers[1] <= 5 && mypapers[2] <= 5 && mypapers[3] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 6 && mypapers[2] <= 5 && mypapers[3] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 5 && mypapers[2] <= 6 && mypapers[3] <= 5) || (mypapers[0] <= 5 && mypapers[1] <= 5 && mypapers[2] <= 5 && mypapers[3] <= 6))
                                    {
                                        ALevelGrade = "D";
                                        AlevelPoints = 3;
                                    }
                                    else if ((mypapers[0] <= 7 && mypapers[1] <= 6 && mypapers[2] <= 6 && mypapers[3] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 7 && mypapers[2] <= 6 && mypapers[3] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 6 && mypapers[2] <= 7 && mypapers[3] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 6 && mypapers[2] <= 6 && mypapers[3] <= 7) || (mypapers[0] <= 6 && mypapers[1] <= 6 && mypapers[2] <= 5 && mypapers[3] <= 8) || (mypapers[0] <= 6 && mypapers[1] <= 5 && mypapers[2] <= 6 && mypapers[3] <= 8) || (mypapers[0] <= 5 && mypapers[1] <= 6 && mypapers[2] <= 6 && mypapers[3] <= 8) || (mypapers[0] <= 6 && mypapers[1] <= 6 && mypapers[2] <= 8 && mypapers[3] <= 5) || (mypapers[0] <= 6 && mypapers[1] <= 5 && mypapers[2] <= 8 && mypapers[3] <= 6) || (mypapers[0] <= 5 && mypapers[1] <= 6 && mypapers[2] <= 8 && mypapers[3] <= 6) || (mypapers[0] <= 5 && mypapers[1] <= 8 && mypapers[2] <= 6 && mypapers[3] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 8 && mypapers[2] <= 5 && mypapers[3] <= 6) || (mypapers[0] <= 6 && mypapers[1] <= 8 && mypapers[2] <= 6 && mypapers[3] <= 5) || (mypapers[0] <= 8 && mypapers[1] <= 5 && mypapers[2] <= 6 && mypapers[3] <= 6) || (mypapers[0] <= 8 && mypapers[1] <= 6 && mypapers[2] <= 5 && mypapers[3] <= 6) || (mypapers[0] <= 8 && mypapers[1] <= 6 && mypapers[2] <= 6 && mypapers[3] <= 5))
                                    {
                                        ALevelGrade = "E";
                                        AlevelPoints = 2;
                                    }
                                    else if ((mypapers[0] <= 7 && mypapers[1] <= 7 && mypapers[2] <= 7 && mypapers[3] <= 7) || (mypapers[0] <= 8 && mypapers[1] == 8 && mypapers[2] <= 8 && mypapers[3] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 8 && mypapers[2] <= 8 && mypapers[3] <= 8) || (mypapers[0] <= 8 && mypapers[1] <= 9 && mypapers[2] <= 8 && mypapers[3] <= 8) || (mypapers[0] <= 8 && mypapers[1] <= 8 && mypapers[2] <= 9 && mypapers[3] <= 8) || (mypapers[0] <= 8 && mypapers[1] <= 8 && mypapers[2] <= 8 && mypapers[3] <= 9) || (mypapers[0] <= 9 && mypapers[1] <= 9 && mypapers[2] <= 7 && mypapers[3] <= 7) || (mypapers[0] <= 9 && mypapers[1] <= 7 && mypapers[2] <= 9 && mypapers[3] <= 7) || (mypapers[0] <= 9 && mypapers[1] <= 7 && mypapers[2] <= 7 && mypapers[3] <= 9) || (mypapers[0] <= 7 && mypapers[1] <= 9 && mypapers[2] <= 9 && mypapers[3] <= 7) || (mypapers[0] <= 7 && mypapers[1] <= 9 && mypapers[2] <= 7 && mypapers[3] <= 9) || (mypapers[0] <= 7 && mypapers[1] <= 7 && mypapers[2] <= 9 && mypapers[3] <= 9))
                                    {
                                        ALevelGrade = "O";
                                        AlevelPoints = 1;
                                    }
                                    else if ((mypapers[0] <= 9 && mypapers[1] <= 9 && mypapers[2] <= 8 && mypapers[3] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 8 && mypapers[2] <= 9 && mypapers[3] <= 8) || (mypapers[0] <= 9 && mypapers[1] <= 8 && mypapers[2] <= 8 && mypapers[3] <= 9) || (mypapers[0] <= 8 && mypapers[1] <= 9 && mypapers[2] <= 9 && mypapers[3] <= 8) || (mypapers[0] <= 8 && mypapers[1] <= 9 && mypapers[2] <= 8 && mypapers[3] <= 9) || (mypapers[0] <= 8 && mypapers[1] <= 8 && mypapers[2] <= 9 && mypapers[3] <= 9))
                                    {
                                        ALevelGrade = "F";
                                        AlevelPoints = 0;
                                    }
                                    else
                                    {
                                        ALevelGrade = "X";
                                        AlevelPoints = 0;
                                    }
                                }
                                con.Close();
                            }
                                      con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        string ct = "select studentno,year,class,term,stream,level,subjectname,Grade,Points from LeveladvancedGrade where studentno='" + row.Cells[0].Value + "' and subjectname='" + txtSubjectName.Text + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(ct);
                                        cmd.Connection = con;
                                        rdr = cmd.ExecuteReader();
                                        if (rdr.Read())
                                        {
                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            string cb = "update LeveladvancedGrade set studentno=@d1,year=@d2,class=@d3,term=@d4,stream=@d5,level=@d6,subjectname=@d7,Grade=@d8,Points=@d9,Photo=@d10 where studentno='" + row.Cells[0].Value + "' and subjectname='" + txtSubjectName.Text + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                            cmd = new SqlCommand(cb);
                                            cmd.Connection = con;
                                            // Add Parameters to Command Parameters collection
                                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "year"));
                                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "class"));
                                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "level"));
                                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 20, "Grade"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "Points"));
                                            // Prepare command for repeated execution
                                            cmd.Prepare();
                                            // Data to be inserted
                                            cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                            cmd.Parameters["@d2"].Value = cmbSession.Text;
                                            cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                                            cmd.Parameters["@d6"].Value = cmbBranch.Text;
                                            cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                            cmd.Parameters["@d8"].Value = ALevelGrade;
                                            cmd.Parameters["@d9"].Value = AlevelPoints;
                                            MemoryStream ms = new MemoryStream();
                                            Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                                            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            byte[] data = ms.GetBuffer();
                                            SqlParameter p = new SqlParameter("@d10", SqlDbType.Image);
                                            p.Value = data;
                                            cmd.Parameters.Add(p);
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            rdr.Dispose();
                                            da.Dispose();
                                            dt.Clear();
                                        }
                                        else
                                        {
                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            string cb = "insert into LeveladvancedGrade(studentno,year,class,term,stream,level,subjectname,Grade,Points,Photo,Sets) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11)";
                                            cmd = new SqlCommand(cb);
                                            cmd.Connection = con;
                                            // Add Parameters to Command Parameters collection
                                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "studentno"));
                                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "year"));
                                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 30, "class"));
                                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "term"));
                                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "stream"));
                                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "level"));
                                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.VarChar, 250, "SubjectName"));
                                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.NChar, 20, "Grade"));
                                            cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.Int, 10, "Points"));
                                            cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.NChar, 10, "Sets"));
                                            // Prepare command for repeated execution
                                            cmd.Prepare();
                                            // Data to be inserted
                                            cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                            cmd.Parameters["@d2"].Value = cmbSession.Text;
                                            cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                            cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                            cmd.Parameters["@d5"].Value = cmbSection.Text;
                                            cmd.Parameters["@d6"].Value = cmbBranch.Text;
                                            cmd.Parameters["@d7"].Value = txtSubjectName.Text;
                                            cmd.Parameters["@d8"].Value = ALevelGrade;
                                            cmd.Parameters["@d9"].Value = AlevelPoints;
                                            cmd.Parameters["@d11"].Value = sets.Text;
                                            MemoryStream ms = new MemoryStream();
                                            Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                                            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            byte[] data = ms.GetBuffer();
                                            SqlParameter p = new SqlParameter("@d10", SqlDbType.Image);
                                            p.Value = data;
                                            cmd.Parameters.Add(p);
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            rdr.Dispose();
                                            da.Dispose();
                                            dt.Clear();
                                        }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
            }
            MessageBox.Show("success", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (sets.Text == "")
            {
                MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sets.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select Subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            if (MessageBox.Show("Have all the Results been Input?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ALevelFinaleGrade();
            }      
        }
        int english = 0;
        public void Olevelaggregate()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    int countpapersagg =0;
                    int studentcountpapers1 = 0;
                    string [] studentmyaggregates = new string[150];
                    int[] myaggregates = new int[150];
                    int aggsum = 0;
                    try { 
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery6 = "SELECT COUNT(subjectname) FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" +row.Cells[0].Value+ "'";
                        cmd = new SqlCommand(inquery6, con);
                        label33.Text = cmd.ExecuteScalar().ToString();
                        studentcountpapers1 = Convert.ToInt32(label33.Text);
                        con.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Count is the problem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                        if (studentcountpapers1 >=8 )
                        {
                            try { 
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery7 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' order by aggregate ASC";
                            cmd = new SqlCommand(inquery7, con);
                            rdr = cmd.ExecuteReader();
                            SqlDataAdapter sa1 = new SqlDataAdapter(cmd);
                            DataTable dts1 = new DataTable();
                            dts1.Load(rdr);
                            sa1.Fill(dts1);
                            rdr.Close();
                            foreach (DataRow dr1 in dts1.Rows)
                            {
                                myaggregates[countpapersagg] = dr1.Field<int>(0);
                               countpapersagg ++;
                            }
                            aggsum = myaggregates[0] + myaggregates[1] + myaggregates[2] + myaggregates[3] + myaggregates[4] + myaggregates[5] + myaggregates[6] + myaggregates[7];
                            con.Close();
                            dts1.Dispose();
                            }
                                
                            catch (Exception)
                            {
                                MessageBox.Show("aggregate is a problem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            try { 
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery8 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' and subjectname like'%ENGLISH%'";
                             cmd = new SqlCommand(inquery8, con);
                            label19.Text = cmd.ExecuteScalar().ToString(); 
                            english = Convert.ToInt32(label19.Text);
                            con.Close();
                            int[] counters1 =new int[150];
                            int[] counters2= new int[150];
                            int[] counters7 = new int[150];
                            int[] counters3 = new int[150];
                            int[] counters4 = new int[150];
                            int[] counters5 = new int[150];
                            int[] counters6 = new int[150];
                            int[] myaggregates2 = new int[150];
                            int countpapersagg2 = 0;
                            int r1=0;
                            int r2 = 0;
                            int r3 = 0;
                            int r4 = 0;
                            int r5 = 0;
                            int r6 = 0;
                           
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery9 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' order by aggregate ASC";
                            cmd = new SqlCommand(inquery9, con);
                            rdr = cmd.ExecuteReader();
                            SqlDataAdapter sa2 = new SqlDataAdapter(cmd);
                            DataTable dts2 = new DataTable();
                            dts2.Load(rdr);
                            sa2.Fill(dts2);
                            rdr.Close();
                            foreach (DataRow dr2 in dts2.Rows)
                            {
                                myaggregates2[countpapersagg2] = dr2.Field<int>(0);
                                countpapersagg2++;
                            }
                            for(int y=0; y < myaggregates2.Length;y++){
                                //division 2
                                if (myaggregates2[y] <= 6)
                                {
                                    for (int k = 0; k <y; k++)
                                    {
                                        counters1[k] = myaggregates2[y];
                                    }
                                    do{
                                        r1++;
                                    }while (r1 <=counters1.Length);
                                }

                                //division 3
                                if (myaggregates2[y] <= 8)
                                {
                                    for (int k = 0; k < y; k++)
                                    {
                                        counters7[k] = myaggregates2[y];
                                    }
                                        do
                                        {
                                            for (int x = 0; x < counters7.Length; x++)
                                            {
                                                if (counters7[x] <= 6)
                                                {
                                                    counters2[x] = counters7[x];
                                                }
                                            }
                                            do
                                            {
                                                r3++;
                                            } while (r3 <= counters2.Length);
                                            r2++;
                                        } while (r2 <= counters7.Length);
                                }
                                //division 4
                                if (myaggregates2[y] <= 6)
                                {
                                    for (int k = 0; k <y; k++)
                                    {
                                        counters3[k] = myaggregates2[y];
                                    }
                                        do
                                        {
                                            r4++;
                                        } while (r4 <= counters3.Length);
                                    }
                                    if (myaggregates2[y] <= 7)
                                    {
                                        for (int k = 0; k <= y; k++)
                                        {
                                            counters4[k] = myaggregates2[y];
                                        }
                                        do
                                        {
                                            r5++;
                                        } while (r5 <= counters4.Length);
                                    }
                                    if (myaggregates2[y] <= 8)
                                    {
                                        for (int k = 0; k <y; k++)
                                        {
                                            counters5[k] = myaggregates2[y];
                                        }
                                        do
                                        {
                                            r6++;
                                        } while (r6 <= counters5.Length);
                                    }
                            }
                            if (english <= 6 && aggsum <= 32 && r1 >= 7)
                            {
                                division = "1";
                            }
                            else if (english > 6 && aggsum <= 32 && r1 >= 6 && r6 >= 8)
                            {
                                division = "2";
                            }
                            else if (english <= 8 && aggsum <= 45 && r1 >= 6 && r6 >= 8)
                            {
                                division = "2";
                            }
                            else if (english > 8 && aggsum <= 45 && r1 >= 6 && r6 >= 8)
                            {
                                division = "3";
                            }
                            else if ((r2 >= 8 && r3 >= 3 && aggsum <= 58) || (r2 >= 7 && r3 >= 4 && aggsum <= 58) || (aggsum <= 58 && r3 >= 5))
                            {
                                division = "3";
                            }
                            else if ((r4 >= 1 && aggsum < 70) || (r5 >= 2 && aggsum < 70) || (r6 >= 3 && aggsum < 70))
                            {
                                division = "4";
                            }
                            else if (aggsum > 70)
                            {
                                division = "9";
                            }
                            else
                            {
                                division = "7";
                            }
                           
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery4 = "Update Positions set Totalagg=@d2,Division=@d3 WHERE studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                            cmd = new SqlCommand(inquery4, con);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.Int, 10, "Totalagg"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Division"));
                            cmd.Prepare();
                            cmd.Parameters["@d2"].Value = aggsum;
                            cmd.Parameters["@d3"].Value = division;   
                            cmd.ExecuteNonQuery();
                            con.Close();
                            dts2.Clear();
                            rdr.Dispose();
                            sa2.Dispose();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("English is the problem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            try { 
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            String inquery4 = "Update Positions set Totalagg=@d2,Division=@d3 WHERE studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                            cmd = new SqlCommand(inquery4, con);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.Int, 10, "Totalagg"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Division"));
                            cmd.Prepare();
                            cmd.Parameters["@d2"].Value = 0;
                            cmd.Parameters["@d3"].Value = division;                         
                            cmd.ExecuteNonQuery();
                            con.Close();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Positions is the problem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        public void Olevelaggregate1()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    int countpapersagg = 0;
                    int studentcountpapers1 = 0;
                    string[] studentmyaggregates = new string[200];
                    int[] myaggregates = new int[200];
                    int aggsum = 0;

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    String inquery6 = "SELECT COUNT(subjectname) FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='" + sets.Text + "'";
                    cmd = new SqlCommand(inquery6, con);
                    label33.Text = cmd.ExecuteScalar().ToString();
                    studentcountpapers1 = Convert.ToInt32(label33.Text);
                    con.Close();

                    if (studentcountpapers1 >= 8)
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery7 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='"+sets.Text+"' order by aggregate ASC";
                        cmd = new SqlCommand(inquery7, con);
                        rdr = cmd.ExecuteReader();
                        SqlDataAdapter sa1 = new SqlDataAdapter(cmd);
                        DataTable dts1 = new DataTable();
                        dts1.Load(rdr);
                        sa1.Fill(dts1);
                        rdr.Close();
                        foreach (DataRow dr1 in dts1.Rows)
                        {
                            myaggregates[countpapersagg] = dr1.Field<int>(0);
                            countpapersagg++;
                        }
                        aggsum = myaggregates[0] + myaggregates[1] + myaggregates[2] + myaggregates[3] + myaggregates[4] + myaggregates[5] + myaggregates[6] + myaggregates[7];
                        con.Close();

                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery8 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='"+sets.Text+"' and subjectname like'%ENGLISH%'";
                        cmd = new SqlCommand(inquery8, con);
                        label19.Text = cmd.ExecuteScalar().ToString();
                        english = Convert.ToInt32(label19.Text);
                        con.Close();
                        int[] counters1 = new int[200];
                        int[] counters2 = new int[200];
                        int[] counters7 = new int[200];
                        int[] counters3 = new int[200];
                        int[] counters4 = new int[200];
                        int[] counters5 = new int[200];
                        int[] counters6 = new int[200];
                        int[] myaggregates2 = new int[200];
                        int countpapersagg2 = 0;
                        int r1 = 0;
                        int r2 = 0;
                        int r3 = 0;
                        int r4 = 0;
                        int r5 = 0;
                        int r6 = 0;
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery9 = "SELECT aggregate FROM Results WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and studentno='" + row.Cells[0].Value + "' and Sets='"+sets.Text+"' order by aggregate ASC";
                        cmd = new SqlCommand(inquery7, con);
                        rdr = cmd.ExecuteReader();
                        //SqlDataAdapter sa2 = new SqlDataAdapter(cmd);
                        DataTable dts2 = new DataTable();
                        dts2.Load(rdr);
                        //sa2.Fill(dts2);
                        rdr.Close();
                        foreach (DataRow dr2 in dts2.Rows)
                        {
                            myaggregates2[countpapersagg2] = dr2.Field<int>(0);
                            countpapersagg2++;
                        }
                        for (int y = 0; y < myaggregates2.Length; y++)
                        {
                            //division 2
                            if (myaggregates2[y] <= 6)
                            {
                                for (int k = 0; k < y; k++)
                                {
                                    counters1[k] = myaggregates2[y];
                                }
                                do
                                {
                                    r1++;
                                } while (r1 <= counters1.Length);
                            }

                            //division 3
                            if (myaggregates2[y] <= 8)
                            {
                                for (int k = 0; k < y; k++)
                                {
                                    counters7[k] = myaggregates2[y];
                                }
                                do
                                {
                                    for (int x = 0; x < counters7.Length; x++)
                                    {
                                        if (counters7[x] <= 6)
                                        {
                                            counters2[x] = counters7[x];
                                        }
                                    }
                                    do
                                    {
                                        r3++;
                                    } while (r3 <= counters2.Length);
                                    r2++;
                                } while (r2 <= counters7.Length);
                            }
                            //division 4
                            if (myaggregates2[y] <= 6)
                            {
                                for (int k = 0; k < y; k++)
                                {
                                    counters3[k] = myaggregates2[y];
                                }
                                do
                                {
                                    r4++;
                                } while (r4 <= counters3.Length);
                            }
                            if (myaggregates2[y] <= 7)
                            {
                                for (int k = 0; k <= y; k++)
                                {
                                    counters4[k] = myaggregates2[y];
                                }
                                do
                                {
                                    r5++;
                                } while (r5 <= counters4.Length);
                            }
                            if (myaggregates2[y] <= 8)
                            {
                                for (int k = 0; k < y; k++)
                                {
                                    counters5[k] = myaggregates2[y];
                                }
                                do
                                {
                                    r6++;
                                } while (r6 <= counters5.Length);
                            }
                        }
                        if (english <= 6 && aggsum <= 32 && r1>=7)
                        {
                            division = "1";
                        }
                        else if (english > 6 && aggsum <= 32 && r1 >= 6 && r6>=8)
                        {
                            division = "2";
                        }
                        else if (english <= 8 && aggsum <= 45 && r1 >= 6 && r6 >= 8)
                        {
                            division = "2";
                        }
                        else if (english > 8 && aggsum <= 45 && r1 >= 6 && r6 >= 8)
                        {
                            division = "3";
                        }
                        else if ((r2 >= 8 && r3 >= 3 && aggsum <= 58) || (r2 >= 7 && r3 >= 4 && aggsum <= 58) || (aggsum <= 58 && r3 >= 5))
                        {
                            division = "3";
                        }
                        else if ((r4 >= 1 && aggsum < 70) || (r5 >= 2 && aggsum < 70) || (r6 >= 3 && aggsum < 70))
                        {
                            division = "4";
                        }
                        else if (aggsum > 70)
                        {
                            division = "9";
                        }
                        else
                        {
                            division = "7";
                        }
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery4 = "Update Positions set Totalagg=@d2,Division=@d3 WHERE studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='"+sets.Text+"'";
                        cmd = new SqlCommand(inquery4, con);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.Int, 10, "Totalagg"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Division"));
                        cmd.Prepare();
                        cmd.Parameters["@d2"].Value = aggsum;
                        cmd.Parameters["@d3"].Value = division;
                        cmd.ExecuteNonQuery();
                        con.Close();
                        dts1.Clear();
                        rdr.Dispose();
                        sa1.Dispose();
                    }
                    else
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        String inquery4 = "Update Positions set Totalagg=@d2,Division=@d3 WHERE studentno ='" + row.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='"+sets.Text+"'";
                        cmd = new SqlCommand(inquery4, con);
                        cmd.Connection = con;
                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.Int, 10, "Totalagg"));
                        cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Division"));
                        cmd.Prepare();
                        cmd.Parameters["@d2"].Value = 0;
                        cmd.Parameters["@d3"].Value = division;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (label5.Text == "HEADMASTER" || label5.Text == "Teacher")
                {
                    Update_record.Enabled = true;
                    Delete.Enabled = true;
                }
                else
                {
                    Update_record.Enabled = false;
                }
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
                
                if (cmbBranch.Text=="O Level")
                {
                    if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
                    {
                        if (cmbExamName.Text == "")
                        {
                            MessageBox.Show("Please select exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cmbExamName.Focus();
                            return;
                        }
                        if (cmbExamName.Text == "beginning of term")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(begginingofterm)[Marks Obtained]from Results where  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                            cmd = new SqlCommand(sql, con);
                            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            dataGridView1.Rows.Clear();
                            while (rdr.Read() == true)
                            {
                                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                            }
                            con.Close();
                        }
                        if (cmbExamName.Text == "Mid Term")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(midterm)[Marks Obtained]from Results where Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
                            cmd = new SqlCommand(sql, con);
                            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            dataGridView1.Rows.Clear();
                            while (rdr.Read() == true)
                            {
                                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                            }
                            con.Close();
                        }
                        if (cmbExamName.Text == "End of Term")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(endofterm)[Marks Obtained]from Results where  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' order by studentname";
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
                    else if (cmbCourse.Text == "S.4")
                    {
                        if (Paper.Text == "")
                        {
                            MessageBox.Show("Please select Paper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Paper.Focus();
                            return;
                        }
                        if (sets.Text == "")
                        {
                            MessageBox.Show("Please select Sets", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sets.Focus();
                            return;
                        }
                        if (Paper.Text == "Paper 1")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(begginingofterm)[Marks Obtained]from Results where  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectname='" + txtSubjectName.Text + "' and Sets='" + sets.Text + "' order by studentname";
                            cmd = new SqlCommand(sql, con);
                            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            dataGridView1.Rows.Clear();
                            while (rdr.Read() == true)
                            {
                                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                            }
                            con.Close();
                        }
                        if (Paper.Text == "Paper 2")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(midterm)[Marks Obtained]from Results where Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectname='" + txtSubjectName.Text + "' and Sets='" + sets.Text + "' order by studentname";
                            cmd = new SqlCommand(sql, con);
                            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            dataGridView1.Rows.Clear();
                            while (rdr.Read() == true)
                            {
                                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                            }
                            con.Close();
                        }
                        if (Paper.Text == "Paper 3")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(endofterm)[Marks Obtained]from Results where  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectname='" + txtSubjectName.Text + "' and Sets='" + sets.Text + "' order by studentname";
                            cmd = new SqlCommand(sql, con);
                            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                            dataGridView1.Rows.Clear();
                            while (rdr.Read() == true)
                            {
                                dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3]);
                            }
                            con.Close();
                        }
                        if (Paper.Text == "Paper 4")
                        {
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(FourthPaper)[Marks Obtained]from Results where  Results.class= '" + cmbCourse.Text + "'and Results.level='" + cmbBranch.Text + "'and Results.year='" + cmbSession.Text + "' and Results.term= '" + cmbSemester.Text + "' and Results.stream= '" + cmbSection.Text + "' and subjectname='" + txtSubjectName.Text + "' and Sets='" + sets.Text + "' order by studentname";
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
                }
                if (cmbBranch.Text == "A Level")
                {
                    if (Paper.Text == "")
                    {
                        MessageBox.Show("Please select Paper", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Paper.Focus();
                        return;
                    }

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string sql = "select RTrim(studentno)[ScholarNo],Rtrim(enrollmentnumber)[Enrollment No.], RTRIM(studentname)[Student Name], RTRIM(marks)[Marks Obtained]from Leveladvanced where  class= '" + cmbCourse.Text + "'and level='" + cmbBranch.Text + "'and year='" + cmbSession.Text + "' and term= '" + cmbSemester.Text + "' and stream= '" + cmbSection.Text + "' and subjectcode='" + cmbSubjectCode.Text + "' and Paper='" + Paper.Text + "' and ExamName='" + cmbExamName.Text + "' and Sets='"+sets.Text+"' order by studentname";
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
                if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
                {
                    if (cmbExamName.Text == "")
                    {
                        MessageBox.Show("Please select exam name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbExamName.Focus();
                        return;
                    }
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

                    /* rowsTotal = dataGridView1.RowCount - 1;
                     colsTotal = dataGridView1.Columns.Count - 1;
                     var _with1 = excelWorksheet;
                     _with1.Cells.Select();
                     _with1.Cells.Delete();
                     _with1.Cells[1, 1].Value = "Year:";
                     _with1.Cells[1, 2].Value = cmbSession.Text;
                     _with1.Cells[1, 3].Value = "Class:";
                     _with1.Cells[1, 4].Value = cmbCourse.Text;
                     _with1.Cells[2, 1].Value = "Term:";
                     _with1.Cells[2, 2].Value = cmbSemester.Text;
                     _with1.Cells[2, 3].Value = "Stream:";
                     _with1.Cells[2, 4].Value = cmbSection.Text;
                     _with1.Cells[3, 1].Value = "Subject Code:";
                     _with1.Cells[3, 2].Value = cmbSubjectCode.Text;
                     _with1.Cells[3, 3].Value = "Subject Name:";
                     _with1.Cells[3, 4].Value = txtSubjectName.Text;
                     _with1.Cells[4, 2].Value = "Exam Name:";
                     _with1.Cells[4, 3].Value = cmbExamName.Text;
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
                    /*_with1.Rows["2:2"].Font.FontStyle = "Bold";
                    _with1.Rows["2:2"].Font.Size = 12;
                    _with1.Rows["3:3"].Font.FontStyle = "Bold";
                    _with1.Rows["3:3"].Font.Size = 12;
                    _with1.Rows["4:4"].Font.FontStyle = "Bold";
                    _with1.Rows["4:4"].Font.Size = 12;
                    _with1.Rows["5:5"].Font.FontStyle = "Bold";
                    _with1.Rows["5:5"].Font.Size = 12;
                    _with1.Cells.Columns.AutoFit();
                    // _with1.Cells.Columns.Justify();
                    _with1.Cells.Select();
                    _with1.Cells.EntireColumn.AutoFit();
                    _with1.Cells[1, 1].Select();*/
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

        private void Paper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
                {
                    sets.Text = "";
                    sets.Enabled = false;
                }
                if (cmbCourse.Text == "S.4")
                {
                    sets.Text = "";
                    sets.Enabled = true;
                }
        }
        }
    }

