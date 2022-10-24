using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmSubjectInfoRecord : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();       
        public frmSubjectInfoRecord()
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
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(CourseName) FROM SubjectInfo", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {               
                    SqlConnection CN = new SqlConnection(cs.DBConn);
                    CN.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(class) FROM SubjectInfoA", CN);
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
            
            Branch.Items.Clear();
            Branch.Text = "";
            Branch.Enabled = true;

            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();

                if (Course.Text == "S.1" || Course.Text == "S.2" || Course.Text == "S.3" || Course.Text == "S.4")
                {
                    string ct = "select distinct RTRIM(Branch) from SubjectInfo where CourseName= '" + Course.Text + "'";
                    cmd = new SqlCommand(ct);
                }
                if (Course.Text == "S.5" || Course.Text == "S.6" )
                {
                    string ct = "select distinct RTRIM(Level) from SubjectInfoA where Class= '" + Course.Text + "'";
                    cmd = new SqlCommand(ct);
                }
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

        private void Branch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Semester.Text = "";
            Semester.Enabled = true;
            try
            {


                SqlConnection CN = new SqlConnection(cs.DBConn);

                CN.Open();
                if (Branch.Text == "O Level")
                {
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Semester) FROM SubjectInfo", CN);
                }
                if (Branch.Text == "A Level")
                {
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(term) FROM SubjectInfoA", CN);
                }
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
                if (Branch.Text == "")
                {
                    MessageBox.Show("Please select level", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Branch.Focus();
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
                _with1.Columns.Add("Subject Name", 300, HorizontalAlignment.Center);
                _with1.Columns.Add("Paper", 200, HorizontalAlignment.Left);
                
              
                con = new SqlConnection(cs.DBConn);

                con.Open();

                if (Branch.Text == "O Level")
                {
                    cmd = new SqlCommand("select RTrim(SubjectCode)[Subject Code], RTRIM(SubjectName)[Subject Name], RTRIM(Paper)[Paper] from subjectinfo where  CourseName= '" + Course.Text + "'and branch='" + Branch.Text + "'and Semester='" + Semester.Text + "'", con);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var item = new ListViewItem();
                        item.Text = rdr[0].ToString();
                        item.SubItems.Add(rdr[1].ToString());
                        item.SubItems.Add(rdr[2].ToString());
                        listView1.Items.Add(item);
                    }
                }

                if (Branch.Text == "A Level")
                {
                    cmd = new SqlCommand("select RTrim(SubjectCodeA)[Subject Code], RTRIM(SubjectNameA)[Subject Name],RTRIM(Paper)[Paper] from subjectinfoA where  class= '" + Course.Text + "'and level='" + Branch.Text + "'and term='" + Semester.Text + "'", con);

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var item = new ListViewItem();
                        item.Text = rdr[0].ToString();
                        item.SubItems.Add(rdr[1].ToString());
                        item.SubItems.Add(rdr[2].ToString());
                        listView1.Items.Add(item);
                    }
                    con.Close();
                }

              
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
            Branch.Text = "";
            Semester.Text = "";
            listView1.Items.Clear();
            Branch.Enabled = false;
            Semester.Enabled = false;
            Course.Focus();
        }

        private void Semester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
    }
}
