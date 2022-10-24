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
    public partial class frmSubjectGradeRecord : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

       

        public frmSubjectGradeRecord()
        {
            InitializeComponent();
        }

        private void frmSubjectInfoRecord_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            
        }
        private void AutocompleteCourse()
        {

            try
            {


                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Class) FROM SubjectGrade", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
                    dtable = ds.Tables[0];
                    Course.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());

                }
           
        }
        
       
        private void Course_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            year.Items.Clear();
            year.Text = "";
            year.Enabled = true;

            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();

                    string ct = "select distinct RTRIM(Year) from SubjectGrade where Class= '" + Course.Text + "'";
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

        private void Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Semester.Text = "";
            Semester.Enabled = true;
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Term) FROM SubjectGrade", CN);     
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Semester.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Semester.Items.Add(drow[0].ToString());
                }
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
                if (year.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    year.Focus();
                    return;
                }
                if (Semester.Text == "")
                {
                    MessageBox.Show("Please select term", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Semester.Focus();
                    return;
                }
               
                var _with1 = listView1;
                _with1.Clear();
                _with1.Columns.Add("Subject Code", 100, HorizontalAlignment.Left);
                _with1.Columns.Add("Year", 100, HorizontalAlignment.Center);
                _with1.Columns.Add("Class", 100, HorizontalAlignment.Center);
                _with1.Columns.Add("Term", 100, HorizontalAlignment.Center);
                _with1.Columns.Add("Grade", 100, HorizontalAlignment.Center);
                _with1.Columns.Add("Min Mark", 60, HorizontalAlignment.Center);
                _with1.Columns.Add("Max Mark", 60, HorizontalAlignment.Center);
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = new SqlCommand("select RTrim(SubjectCode)[Subject Code], RTRIM(Year)[Year], RTRIM(Class)[Class], RTRIM(Term)[Term], RTRIM(Grade)[Grade], RTRIM(MinMark)[Min Mark], RTRIM(MaxMark)[Max Mark] from SubjectGrade where  Class= '" + Course.Text + "'and Year='" + year.Text + "'and Term='" + Semester.Text + "'", con);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var item = new ListViewItem();
                    item.Text = rdr[0].ToString();
                    item.SubItems.Add(rdr[1].ToString());
                    item.SubItems.Add(rdr[2].ToString());
                    item.SubItems.Add(rdr[3].ToString());
                    item.SubItems.Add(rdr[4].ToString());
                    item.SubItems.Add(rdr[5].ToString());
                    item.SubItems.Add(rdr[6].ToString());
                    listView1.Items.Add(item);
                }
                con.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            year.Text = "";
            Semester.Text = "";
            listView1.Items.Clear();
            year.Enabled = false;
            Semester.Enabled = false;
            Course.Focus();
        }

        private void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
    }
}
