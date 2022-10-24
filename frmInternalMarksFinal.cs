using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmInternalMarksFinal : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        SqlDataReader rdr2 = null;
        SqlDataReader rdr3 = null;
        SqlConnection con = null;
        SqlConnection con2 = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();

        public frmInternalMarksFinal()
        {
            InitializeComponent();
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

        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbSubjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {

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

            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string sql = "select rtrim(ScholarNo),rtrim(Enrollment_no),rtrim(student_name) from Student,batch where Student.Course=batch.Course and Student.Session=batch.Session and Student.Term=batch.Semester  and Student.Course = '" + cmbCourse.Text + "' and Student.Branch= '" + cmbBranch.Text + "' and Batch.semester= '" + cmbSemester.Text + "' and Student.Session= '" + cmbSession.Text + "' order by student_name,ScholarNo";
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
            AutocompleSession();
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label2.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (pricess == "Yes") { Update_record.Enabled = true; } else { Update_record.Enabled = false; }
                }
                if (label2.Text == "ADMIN")
                {
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
            frm.Show();
            cmbBranch.Text = "";
            cmbCourse.Text = "";
            cmbSession.Text = "";
            cmbSemester.Text = "";
            cmbBranch.Enabled = false;
            cmbCourse.Enabled = false;
            cmbSemester.Enabled = false;
            btnSave.Enabled = true;
            Update_record.Enabled = true;
            cmbSession.Focus();
        }

        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
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
                if (cmbCourse.Text == "S.4" && cmbBranch.Text == "O Level")
                {
                    if (sets.Text == "")
                    {
                        MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sets.Focus();
                        return;
                    }
                    btnSave.Enabled = false;
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
                                string ct = "select * from FinalResults where STUDENTNO='" + row.Cells[0].Value + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                cmd = new SqlCommand(ct);
                                cmd.Connection = con;
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                }
                                else
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string cb = "insert into FinalResults(STUDENTNO,NAMES,CLASS,TERM,YEAR,Sets) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                                    cmd = new SqlCommand(cb);
                                    cmd.Connection = con;
                                    // Add Parameters to Command Parameters collection
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "STUDENTNO"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "NAMES"));
                                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "CLASS"));
                                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "TERM"));
                                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "YEAR"));
                                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 10, "Sets"));
                                    // Prepare command for repeated execution
                                    cmd.Prepare();
                                    // Data to be inserted

                                    cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                    cmd.Parameters["@d2"].Value = row.Cells[2].Value;
                                    cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                    cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                    cmd.Parameters["@d5"].Value = cmbSession.Text;
                                    cmd.Parameters["@d6"].Value = sets.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    con.Close();

                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT subjectname,aggregate FROM Results where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                cmd.Connection = con;
                                rdr2 = cmd.ExecuteReader();
                                while (rdr2.Read())
                                {
                                    string subjectnan = rdr2.GetString(0);
                                    if (subjectnan.Trim().Equals("physics", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set PHY=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "PHY"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("maths", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("MATHEMATICS", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set MAT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "MAT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("geography", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set GEO=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "GEO"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("english", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ENG=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ENG"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("biology", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set BIO=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "BIO"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("chemistry", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set CHE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "CHE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("history", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set HIS=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "HIS"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("commerce", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set COM=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "COM"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("ips", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("integrated practical skills", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set IPS=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "IPS"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("entrepreneurship", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("ENTREPRENEURSHIP EDUCATION", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ENT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ENT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("agriculture", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set AGR=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "AGR"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("christian religious education", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("CRE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set CRE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "CRE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("islamic religious education", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("IRE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set IRE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "IRE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("computer studies", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ICT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ICT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    /* if ((rdr2 == null))
                                     {
                                         rdr2.Close();
                                     }*/

                                    //con.Close();
                                }
                            }
                        }
                    }
                    con.Close();
                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT Totalagg,Division FROM Positions where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                cmd.Connection = con;
                                rdr3 = cmd.ExecuteReader();
                                while (rdr3.Read())
                                {
                                    string divi = rdr3.GetString(1);
                                    string totalagg = rdr3.GetInt32(0).ToString();
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    String inquery4 = "Update FinalResults set AGGREGATES=@d1,GRADE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                    cmd = new SqlCommand(inquery4, con);
                                    cmd.Connection = con;
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "AGGREGATES"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "GRADE"));
                                    cmd.Prepare();
                                    cmd.Parameters["@d1"].Value = totalagg;
                                    cmd.Parameters["@d2"].Value = divi;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    // rdr2.Close();


                                }
                            }
                        }
                    }
                    con.Close();
                    btnSave.Enabled = true; ;
                    MessageBox.Show("Successful", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbCourse.Text == "S.1" || cmbCourse.Text == "S.2" || cmbCourse.Text == "S.3")
                {
                    btnSave.Enabled = false;
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
                                string ct = "select * from FinalResults where STUDENTNO='" + row.Cells[0].Value + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                cmd = new SqlCommand(ct);
                                cmd.Connection = con;
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                }
                                else
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string cb = "insert into FinalResults(STUDENTNO,NAMES,CLASS,TERM,YEAR) VALUES (@d1,@d2,@d3,@d4,@d5)";
                                    cmd = new SqlCommand(cb);
                                    cmd.Connection = con;
                                    // Add Parameters to Command Parameters collection
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "STUDENTNO"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "NAMES"));
                                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "CLASS"));
                                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "TERM"));
                                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "YEAR"));
                                    // Prepare command for repeated execution
                                    cmd.Prepare();
                                    // Data to be inserted

                                    cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                    cmd.Parameters["@d2"].Value = row.Cells[2].Value;
                                    cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                    cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                    cmd.Parameters["@d5"].Value = cmbSession.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    con.Close();

                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT subjectname,aggregate FROM Results where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "'";
                                cmd.Connection = con;
                                rdr2 = cmd.ExecuteReader();
                                while (rdr2.Read())
                                {
                                    string subjectnan = rdr2.GetString(0);
                                    if (subjectnan.Trim().Equals("physics", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set PHY=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "PHY"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("maths", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("MATHEMATICS", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set MAT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "MAT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("geography", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set GEO=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "GEO"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("english", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ENG=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ENG"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("biology", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set BIO=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "BIO"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("chemistry", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set CHE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "CHE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("history", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set HIS=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "HIS"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("commerce", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set COM=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "COM"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("ips", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("integrated practical skills", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set IPS=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "IPS"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("entrepreneurship", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ENT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ENT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("agriculture", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set AGR=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "AGR"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("christian religious education", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("CRE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set CRE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "CRE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("islamic religious education", StringComparison.InvariantCultureIgnoreCase) || subjectnan.Trim().Equals("IRE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set IRE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "IRE"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("computer studies", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update FinalResults set ICT=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "ICT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetInt32(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    /* if ((rdr2 == null))
                                     {
                                         rdr2.Close();
                                     }*/

                                    //con.Close();
                                }
                            }
                        }
                    }
                    con.Close();
                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT Totalagg,Division FROM Positions where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "'";
                                cmd.Connection = con;
                                rdr3 = cmd.ExecuteReader();
                                while (rdr3.Read())
                                {
                                    string divi = rdr3.GetString(1);
                                    string totalagg = rdr3.GetInt32(0).ToString();
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    String inquery4 = "Update FinalResults set AGGREGATES=@d1,GRADE=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "'";
                                    cmd = new SqlCommand(inquery4, con);
                                    cmd.Connection = con;
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "AGGREGATES"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "GRADE"));
                                    cmd.Prepare();
                                    cmd.Parameters["@d1"].Value = totalagg;
                                    cmd.Parameters["@d2"].Value = divi;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    // rdr2.Close();


                                }
                            }
                        }
                    }
                    con.Close();
                    btnSave.Enabled = true; ;
                    MessageBox.Show("Successful", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (cmbBranch.Text == "A Level")
                {
                    if (sets.Text == "")
                    {
                        MessageBox.Show("Please select Set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sets.Focus();
                        return;
                    }
                    btnSave.Enabled = false;
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
                                string ct = "select * from LeveladvancedCollection where STUDENTNO='" + row.Cells[0].Value + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                cmd = new SqlCommand(ct);
                                cmd.Connection = con;
                                rdr = cmd.ExecuteReader();
                                if (rdr.Read())
                                {
                                }
                                else
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    string cb = "insert into LeveladvancedCollection(STUDENTNO,NAMES,CLASS,TERM,YEAR,Sets) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                                    cmd = new SqlCommand(cb);
                                    cmd.Connection = con;
                                    // Add Parameters to Command Parameters collection
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "STUDENTNO"));
                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 50, "NAMES"));
                                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "CLASS"));
                                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "TERM"));
                                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "YEAR"));
                                    cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 10, "Sets"));
                                    // Prepare command for repeated execution
                                    cmd.Prepare();
                                    // Data to be inserted

                                    cmd.Parameters["@d1"].Value = row.Cells[0].Value;
                                    cmd.Parameters["@d2"].Value = row.Cells[2].Value;
                                    cmd.Parameters["@d3"].Value = cmbCourse.Text;
                                    cmd.Parameters["@d4"].Value = cmbSemester.Text;
                                    cmd.Parameters["@d5"].Value = cmbSession.Text;
                                    cmd.Parameters["@d6"].Value = sets.Text;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }
                    con.Close();

                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT subjectname,Grade FROM LeveladvancedGrade where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                cmd.Connection = con;
                                rdr2 = cmd.ExecuteReader();
                                while (rdr2.Read())
                                {
                                    string subjectnan = rdr2.GetString(0);
                                    string res = rdr2.GetString(1);
                                    if (subjectnan.Trim().Equals("SUB ICT", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        string finalstring = "SICT- " + rdr2.GetString(1);
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update LeveladvancedCollection set Compulsory=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Compulsory"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = finalstring;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("general paper", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update LeveladvancedCollection set GeneralPaper=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "GeneralPaper"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = rdr2.GetString(1);
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals("Sub Maths", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        string finalstring = "SMAT- " + rdr2.GetString(1);
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        String inquery4 = "Update LeveladvancedCollection set Compulsory=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(inquery4, con);
                                        cmd.Connection = con;
                                        cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "MAT"));
                                        cmd.Prepare();
                                        cmd.Parameters["@d2"].Value = finalstring;
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                        // rdr2.Close();
                                    }
                                    else if (subjectnan.Trim().Equals(" ", StringComparison.InvariantCultureIgnoreCase))
                                    {

                                    }
                                    else
                                    {
                                        con = new SqlConnection(cs.DBConn);
                                        con.Open();
                                        cmd = con.CreateCommand();
                                        String fetchprinciple = "SELECT Principle1 FROM LeveladvancedCollection where STUDENTNO='" + row2.Cells[0].Value + "' and TERM='" + cmbSemester.Text + "' and CLASS='" + cmbCourse.Text + "' and YEAR='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                        cmd = new SqlCommand(fetchprinciple, con);
                                        object rdrs = cmd.ExecuteScalar();
                                        if (rdrs == DBNull.Value)
                                        {
                                            string truncstring = subjectnan.Substring(0, 3);
                                            string finalstring = truncstring + "- " + res;
                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            String inquery4 = "Update LeveladvancedCollection set Principle1=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                            cmd = new SqlCommand(inquery4, con);
                                            cmd.Connection = con;
                                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Principle1"));
                                            cmd.Prepare();
                                            cmd.Parameters["@d2"].Value = finalstring;
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            // rdr2.Close();
                                        }
                                        else
                                        {
                                            con = new SqlConnection(cs.DBConn);
                                            con.Open();
                                            cmd = con.CreateCommand();
                                            String fetchprinciple2 = "SELECT Principle2 FROM LeveladvancedCollection where STUDENTNO='" + row2.Cells[0].Value + "' and TERM='" + cmbSemester.Text + "' and CLASS='" + cmbCourse.Text + "' and YEAR='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                            cmd = new SqlCommand(fetchprinciple2, con);
                                            object rdrs3 = cmd.ExecuteScalar();
                                            if (rdrs3 == DBNull.Value)
                                            {
                                                string truncstring = subjectnan.Substring(0, 3);
                                                string finalstring = truncstring + "- " + res;
                                                con = new SqlConnection(cs.DBConn);
                                                con.Open();
                                                String inquery4 = "Update LeveladvancedCollection set Principle2=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                                cmd = new SqlCommand(inquery4, con);
                                                cmd.Connection = con;
                                                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Principle12"));
                                                cmd.Prepare();
                                                cmd.Parameters["@d2"].Value = finalstring;
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                                // rdr2.Close();
                                            }
                                            else
                                            {
                                                con = new SqlConnection(cs.DBConn);
                                                con.Open();
                                                cmd = con.CreateCommand();
                                                String fetchprinciple3 = "SELECT Principle3 FROM LeveladvancedCollection where STUDENTNO='" + row2.Cells[0].Value + "' and TERM='" + cmbSemester.Text + "' and CLASS='" + cmbCourse.Text + "' and YEAR='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                                cmd = new SqlCommand(fetchprinciple3, con);
                                                object rdrs4 = cmd.ExecuteScalar();
                                                if (rdrs4 == DBNull.Value)
                                                {
                                                    string truncstring = subjectnan.Substring(0, 3);
                                                    string finalstring = truncstring + "-" + res;
                                                    con = new SqlConnection(cs.DBConn);
                                                    con.Open();
                                                    String inquery4 = "Update LeveladvancedCollection set Principle3=@d2 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                                    cmd = new SqlCommand(inquery4, con);
                                                    cmd.Connection = con;
                                                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Principle3"));
                                                    cmd.Prepare();
                                                    cmd.Parameters["@d2"].Value = finalstring;
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                    // rdr2.Close();
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    con.Close();
                    foreach (DataGridViewRow row2 in dataGridView1.Rows)
                    {
                        if (!row2.IsNewRow)
                        {
                            if ((row2.Cells[0].Value) == null || (row2.Cells[0].Value) == DBNull.Value || String.IsNullOrWhiteSpace(row2.Cells[0].Value.ToString()))
                            {
                            }
                            else
                            {
                                con = new SqlConnection(cs.DBConn);
                                con.Open();
                                cmd = con.CreateCommand();
                                cmd.CommandText = "SELECT Points FROM LeveladvancedGrade where studentno='" + row2.Cells[0].Value + "' and term='" + cmbSemester.Text + "' and class='" + cmbCourse.Text + "' and year='" + cmbSession.Text + "' and Sets='" + sets.Text + "'";
                                cmd.Connection = con;
                                rdr3 = cmd.ExecuteReader();
                                if (rdr3.Read())
                                {
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    String inquery3 = "SELECT SUM(Points) FROM LeveladvancedGrade WHERE  studentno ='" + row2.Cells[0].Value + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' and Sets='" + sets.Text + "' ";
                                    cmd = new SqlCommand(inquery3, con);
                                    string totalagg = cmd.ExecuteScalar().ToString();
                                    con.Close();
                                    con = new SqlConnection(cs.DBConn);
                                    con.Open();
                                    String inquery4 = "Update LeveladvancedCollection set Points=@d1 WHERE STUDENTNO ='" + row2.Cells[0].Value + "' and YEAR='" + cmbSession.Text + "' and CLASS='" + cmbCourse.Text + "' and TERM='" + cmbSemester.Text + "' and Sets='" + sets.Text + "'";
                                    cmd = new SqlCommand(inquery4, con);
                                    cmd.Connection = con;
                                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "Points"));
                                    cmd.Prepare();
                                    cmd.Parameters["@d1"].Value = totalagg;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    // rdr2.Close();
                                }
                            }
                        }
                    }
                    con.Close();
                    btnSave.Enabled = true; ;
                    MessageBox.Show("Successful", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




        private void Update_record_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmInternalMarksEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label1.Text;
            frm.User.Text = label2.Text;
            frm.Show();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
        }


        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {


        }

        int pupiltotal = 0;
        int maxi = 0;
        int Totalagg = 0;


        int[] myNum = new int[500];
        int[] maxim0 = new int[500];
        string[] studentno = new string[500];
        int counter = 0;
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
                    String inquery3 = "SELECT studentno,total,maxim FROM Positions WHERE year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "' ORDER BY total DESC";
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
                        con2 = new SqlConnection(cs.DBConn);
                        con2.Open();
                        String inquery4 = "Update Positions set position=@d1 WHERE studentno ='" + studentno[i] + "' and year='" + cmbSession.Text + "' and class='" + cmbCourse.Text + "' and term='" + cmbSemester.Text + "'";
                        cmd = new SqlCommand(inquery4, con2);
                        cmd.Connection = con2;
                        cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.Int, 10, "position"));
                        cmd.Prepare();
                        cmd.Parameters["@d1"].Value = (i - (maxim0[i] - 1));
                        cmd.ExecuteNonQuery();
                        con2.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Successfully saved", "Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        }

        private void button3_Click(object sender, EventArgs e)
        {

            frmInternalMarksReportFinal frm = new frmInternalMarksReportFinal();
            this.Hide();
            frm.label6.Text = label1.Text;
            frm.label7.Text = label2.Text;
            frm.Show();

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

        }
        int english = 0;

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

        }


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

        }

        private void cmbExamName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmInternalMarksReportFinalA frm = new frmInternalMarksReportFinalA();
            frm.label6.Text = label1.Text;
            frm.label7.Text = label2.Text;
            frm.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


        }
    }
}

