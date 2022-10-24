using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmSubjectGrading : Form
    {
        SqlDataReader rdr = null;
        SqlDataReader rdr2 = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        ConnectionString cs = new ConnectionString();
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
       

        public frmSubjectGrading()
        {
            InitializeComponent();
        }
        private void Reset()
        {

            SubjectCode.Text = "";
            SubjectName.Text = "";
            cmbCourse.Text = "";
            grade.Text = "";
            year.Text = "";
            minmark.Text = "";
            maxmark.Text = "";
            Semester.Text = "";
            btnSave.Enabled = true;
            Delete.Enabled = false;
            Update_record.Enabled = false;
            SubjectCode.Focus();
            grade.Enabled = false;
            Semester.Enabled = false;
        }
        private void reset2() {
            minmark.Text = "";
            maxmark.Text = "";
            grade.Text = "";
        }
        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void PopulateCourseID()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(CourseName) from  course";
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
        private void Populateyear()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Session) from  Student";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    year.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmSubjectInfo_Load(object sender, EventArgs e)
        {
            PopulateCourseID();
            Autocomplete();
            Populateyear();
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label1.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { Delete.Enabled = true; } else { Delete.Enabled = false; }
                    if (pricess == "Yes") { Update_record.Enabled = true; } else { Update_record.Enabled = false; }
                }
                if (label1.Text == "ADMIN")
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
        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();


                SqlCommand cmd = new SqlCommand("SELECT subjectcode FROM subjectinfo", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "subjectinfo");


                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["SubjectCode"].ToString());

                }
                SubjectCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                SubjectCode.AutoCompleteCustomSource = col;
                SubjectCode.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CourseID_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*cmbBranch.Items.Clear();
            cmbBranch.Text = "";
            cmbBranch.Enabled = true;
            cmbBranch.Focus();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(Branchname) from course where coursename = '" + cmbCourse.Text + "'";
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
            }*/
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SubjectCode.Text == "")
            {
                MessageBox.Show("Please enter subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SubjectCode.Focus();
                return;
            }
            if (year.Text == "")
            {
                MessageBox.Show("Please Select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                year.Focus();
                return;
            }
            if (SubjectName.Text == "")
            {
                MessageBox.Show("Please enter subject name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SubjectName.Focus();
                return;
            }
            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select class name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (grade.Text == "")
            {
                MessageBox.Show("Please Enter Grade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                grade.Focus();
                return;
            }
            if (Semester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Semester.Focus();
                return;
            }
            if (minmark.Text == "")
            {
                MessageBox.Show("Please Enter minimum mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                minmark.Focus();
                return;
            } if (maxmark.Text == "")
            {
                MessageBox.Show("Please Enter Maximum mark", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                maxmark.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select SubjectCode from SubjectGrade where SubjectCode=@find and Year='"+SubjectCode.Text+"'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 20, "subjectcode"));
                cmd.Parameters["@find"].Value = SubjectCode.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Subject Code Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   SubjectCode.Text = "";
                   SubjectCode.Focus();


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into SubjectGrade(SubjectCode,Class,Term,Year,Grade,MinMark,MaxMark) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Class"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Year"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 2, "Grade"));
                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
                cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                cmd.Parameters["@d2"].Value = cmbCourse.Text.Trim();
                cmd.Parameters["@d4"].Value = Semester.Text.Trim();
                cmd.Parameters["@d5"].Value = year.Text.Trim();
                cmd.Parameters["@d6"].Value = grade.Text.Trim();
                cmd.Parameters["@d7"].Value = minmark.Text.Trim();
                cmd.Parameters["@d8"].Value = maxmark.Text.Trim();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //btnSave.Enabled = true;
                Autocomplete();
                //Reset();
                reset2();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
           
        }

        private void SubjectCode_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Update_record_Click(object sender, EventArgs e)
        {
            try{
              con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "update SubjectGrade set MinMark=@d7,MaxMark=@d8,Class=@d4,Term=@d6 where subjectcode=@d1 and Grade=@d5 "; 

                cmd = new SqlCommand(cb);

                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Class"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 2, "grade"));
                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 10, "Term"));
                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
              
                cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                cmd.Parameters["@d4"].Value = cmbCourse.Text.Trim();
                cmd.Parameters["@d5"].Value = grade.Text.Trim();
                cmd.Parameters["@d6"].Value = Semester.Text.Trim();
                cmd.Parameters["@d7"].Value = minmark.Text.Trim();
                cmd.Parameters["@d8"].Value = maxmark.Text.Trim();
           
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Update_record.Enabled = false;
                reset2();
                Autocomplete();
                con.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (SubjectCode.Text == "")
            {
                MessageBox.Show("Please enter subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SubjectCode.Focus();
                return;
            }
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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
                string cq = "delete from SubjectGrade where SubjectCode=@DELETE1 and Year='" + year.Text + "' and Grade='" + grade.Text + "';";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                cmd.Parameters["@DELETE1"].Value = SubjectCode.Text;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                    Autocomplete();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

             private void ViewRecord_Click(object sender, EventArgs e)
             {
                 this.Hide();
                 frmSubjectGradeRecord frm = new frmSubjectGradeRecord();
                 frm.ShowDialog();
                 
                 
             }

             private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
             {

        }

        private void Semester_SelectedIndexChanged(object sender, EventArgs e)
             {
                 grade.Enabled = true;
                 grade.Focus();
             }

             private void SubjectName_TextChanged(object sender, EventArgs e)
             {
                 grade.Enabled = true;
                 minmark.Enabled = true;
                 maxmark.Enabled = true;
                 year.Focus();
             }

             private void SubjectCode_SelectedIndexChanged(object sender, EventArgs e)
             {
                 SubjectCode.Text = SubjectCode.Text.TrimEnd();
                 if (label1.Text == "HEADMASTER")
                 {
                     Delete.Enabled = true;
                     Update_record.Enabled = true;
                 }
                 else
                 {
                     Delete.Enabled = false;
                     Update_record.Enabled = false;
                 }
                 try
                 {
                     if (cmbCourse.Text == "S.5" || cmbCourse.Text == "S.6")
                     {
                         con = new SqlConnection(cs.DBConn);
                         con.Open();
                         cmd = con.CreateCommand();
                         cmd.CommandText = "SELECT * FROM subjectinfoA WHERE SubjectCodeA= '" + SubjectCode.Text.Trim() + "'";
                         rdr = cmd.ExecuteReader();
                         if (rdr.Read())
                         {
                             SubjectName.Text = (String)rdr["SubjectNameA"];
                             //cmbCourse.Text = (String)rdr["CourseName"];
                             Semester.Text = (String)rdr["Term"];
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
                     else
                     {
                         con = new SqlConnection(cs.DBConn);
                         con.Open();
                         cmd = con.CreateCommand();
                         cmd.CommandText = "SELECT * FROM subjectinfo WHERE SubjectCode = '" + SubjectCode.Text.Trim() + "'";
                         rdr = cmd.ExecuteReader();

                         if (rdr.Read())
                         {

                             SubjectName.Text = (String)rdr["SubjectName"];
                             //cmbCourse.Text = (String)rdr["CourseName"];
                             Semester.Text = (String)rdr["Semester"];

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

                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }

             private void button1_Click(object sender, EventArgs e)
             {
                 if (SubjectCode.Text == "")
                 {
                     MessageBox.Show("Please enter subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     SubjectCode.Focus();
                     return;
                 }
                 if (year.Text == "")
                 {
                     MessageBox.Show("Please Select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     year.Focus();
                     return;
                 }
                 if (SubjectName.Text == "")
                 {
                     MessageBox.Show("Please enter subject name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     SubjectName.Focus();
                     return;
                 }
                 if (cmbCourse.Text == "")
                 {
                     MessageBox.Show("Please select class name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     cmbCourse.Focus();
                     return;
                 }
                if (cmbBranch.Text == "")
                {
                    MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbBranch.Focus();
                    return;
                }
                if (Semester.Text == "")
                 {
                     MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     Semester.Focus();
                     return;
                 }
                 try
                 {
                     con = new SqlConnection(cs.DBConn);
                     con.Open();
                     cmd = con.CreateCommand();
                     cmd.CommandText = "SELECT Distinct(Grade) FROM subjectGrade";
                     rdr = cmd.ExecuteReader();

                     while (rdr.Read())
                     {
                         con = new SqlConnection(cs.DBConn);
                         con.Open();
                         cmd = con.CreateCommand();
                         string grading = rdr.GetString(0).Trim();
                         cmd.CommandText = "SELECT MinMark,MaxMark FROM subjectGrade WHERE Grade='" + grading + "' and Class='" + cmbCourse.Text + "' and Term='" + Semester.Text + "' and Year='" + year.Text + "'";
                         rdr2 = cmd.ExecuteReader();
                         if (rdr2.Read())
                         {
                             int minmar = rdr2.GetInt32(0);
                             int maxmar = rdr2.GetInt32(1);
                             con = new SqlConnection(cs.DBConn);
                             con.Open();
                             string cb = "insert into SubjectGrade(SubjectCode,Class,Term,Year,Grade,MinMark,MaxMark) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8)";
                             cmd = new SqlCommand(cb);
                             cmd.Connection = con;
                             cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                             cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Class"));
                             cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                             cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Year"));
                             cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 2, "Grade"));
                             cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                             cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
                             cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                             cmd.Parameters["@d2"].Value = cmbCourse.Text.Trim();
                             cmd.Parameters["@d4"].Value = Semester.Text.Trim();
                             cmd.Parameters["@d5"].Value = year.Text.Trim();
                             cmd.Parameters["@d6"].Value = grading;
                             cmd.Parameters["@d7"].Value = minmar;
                             cmd.Parameters["@d8"].Value = maxmar;
                             cmd.ExecuteNonQuery();
                         }
                     }
                     MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }

        private void cmbBranch_TextChanged(object sender, EventArgs e)
        {
           // cmbCourse.Text = cmbCourse.Text.Trim();
            Semester.Items.Clear();
            Semester.Enabled = true;
            SubjectCode.Focus();
            SubjectCode.Enabled = true;
            if (cmbCourse.Text == "All")
            {
                Semester.Items.Clear();
                Semester.Enabled = true;
                SubjectCode.Focus();
                SubjectCode.Enabled = true;
                try
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    if (cmbBranch.Text == "A Level")
                    {
                        string ct = "select distinct RTRIM(SubjectCodeA) from  SubjectInfoA ";
                        cmd = new SqlCommand(ct);
                    }
                    else if (cmbBranch.Text == "O Level")
                    {
                        string ct = "select distinct RTRIM(SubjectCode) from  SubjectInfo";
                        cmd = new SqlCommand(ct);
                    }
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    SubjectCode.Items.Clear();
                    while (rdr.Read())
                    {
                        SubjectCode.Items.Add(rdr[0]);
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
                try
                {

                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    if (cmbBranch.Text == "A Level")
                    {
                        string ct = "select distinct RTRIM(SubjectCodeA) from  SubjectInfoA WHERE Class='" + cmbCourse.Text + "'";
                        cmd = new SqlCommand(ct);
                    }
                    else if (cmbBranch.Text == "O Level")
                    {
                        string ct = "select distinct RTRIM(SubjectCode) from  SubjectInfo WHERE CourseName='" + cmbCourse.Text + "'";
                        cmd = new SqlCommand(ct);
                    }
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    SubjectCode.Items.Clear();
                    while (rdr.Read())
                    {
                        SubjectCode.Items.Add(rdr[0]);
                    }
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SubjectCode.Text == "")
            {
                MessageBox.Show("Please enter subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SubjectCode.Focus();
                return;
            }
            if (year.Text == "")
            {
                MessageBox.Show("Please Select year", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                year.Focus();
                return;
            }
            if (SubjectName.Text == "")
            {
                MessageBox.Show("Please enter subject name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SubjectName.Focus();
                return;
            }
            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select class name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (cmbBranch.Text == "")
            {
                MessageBox.Show("Please select Level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            if (Semester.Text == "")
            {
                MessageBox.Show("Please select Term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Semester.Focus();
                return;
            }
            try
            {
                if (cmbCourse.Text == "All")
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Distinct(Grade) FROM subjectGrade";
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {

                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = con.CreateCommand();
                        string grading = rdr.GetString(0).Trim();
                        cmd.CommandText = "SELECT MinMark,MaxMark FROM subjectGrade WHERE Grade='" + grading + "' and Term='" + Semester.Text + "' and Year='" + year.Text + "'";
                        rdr2 = cmd.ExecuteReader();
                        if (rdr2.Read())
                        {
                           
                            int minmar = rdr2.GetInt32(0);
                            int maxmar = rdr2.GetInt32(1);
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb1 = "insert into SubjectGrade(SubjectCode,Class,Term,Year,Grade,MinMark,MaxMark) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8)";
                            cmd = new SqlCommand(cb1);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Year"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 2, "Grade"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
                            cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                            cmd.Parameters["@d2"].Value = "S.2";
                            cmd.Parameters["@d4"].Value = Semester.Text.Trim();
                            cmd.Parameters["@d5"].Value = year.Text.Trim();
                            cmd.Parameters["@d6"].Value = grading;
                            cmd.Parameters["@d7"].Value = minmar;
                            cmd.Parameters["@d8"].Value = maxmar;
                            cmd.ExecuteNonQuery();
                            con.Close();

                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb2 = "insert into SubjectGrade(SubjectCode,Class,Term,Year,Grade,MinMark,MaxMark) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8)";
                            cmd = new SqlCommand(cb2);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Year"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 2, "Grade"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
                            cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                            cmd.Parameters["@d2"].Value = "S.3";
                            cmd.Parameters["@d4"].Value = Semester.Text.Trim();
                            cmd.Parameters["@d5"].Value = year.Text.Trim();
                            cmd.Parameters["@d6"].Value = grading;
                            cmd.Parameters["@d7"].Value = minmar;
                            cmd.Parameters["@d8"].Value = maxmar;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            con = new SqlConnection(cs.DBConn);
                            con.Open();
                            string cb4 = "insert into SubjectGrade(SubjectCode,Class,Term,Year,Grade,MinMark,MaxMark) VALUES (@d1,@d2,@d4,@d5,@d6,@d7,@d8)";
                            cmd = new SqlCommand(cb4);
                            cmd.Connection = con;
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 10, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 10, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Year"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 2, "Grade"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 10, "MinMark"));
                            cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.Int, 10, "MaxMark"));
                            cmd.Parameters["@d1"].Value = SubjectCode.Text.Trim();
                            cmd.Parameters["@d2"].Value = "S.4";
                            cmd.Parameters["@d4"].Value = Semester.Text.Trim();
                            cmd.Parameters["@d5"].Value = year.Text.Trim();
                            cmd.Parameters["@d6"].Value = grading;
                            cmd.Parameters["@d7"].Value = minmar;
                            cmd.Parameters["@d8"].Value = maxmar;
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please Select All for Class", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBranch_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
    }

