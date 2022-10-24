using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmNextTerm : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();


        public frmNextTerm()
        {
            InitializeComponent();
        }
        public void Reset()
        {
            begins.Text = DateTime.Today.ToString();
            ends.Text = DateTime.Today.ToString();
        }
       
        private void frmSection_Load(object sender, EventArgs e)
        {
            AutocompleteTerm();  
        }
        private void AutocompleteTerm()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SemesterName) FROM Semester", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                term.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    term.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Year.Text == "")
            {
                MessageBox.Show("Please enter Year ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Year.Focus();
                return;
            }
            if (term.Text == "")
            {
                MessageBox.Show("Please enter term ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                term.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into NextTerm(BeginsOn,EndsOn,Year,Term) VALUES (@d1,@d2,@d3,@d4)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 20, "BeginsOn"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 20, "EndsOn"));
                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 10, "Term"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 10, "Year"));
                cmd.Parameters["@d1"].Value = begins.Text;
                cmd.Parameters["@d2"].Value = ends.Text;
                cmd.Parameters["@d3"].Value = Year.Text;
                cmd.Parameters["@d4"].Value = term.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                Properties.Settings.Default["s1start"] = "Yes";
                Properties.Settings.Default.Save();
                Properties.Settings.Default["s2start"] = "Yes";
                Properties.Settings.Default.Save();
                Properties.Settings.Default["s3start"] = "Yes";
                Properties.Settings.Default.Save();
                Properties.Settings.Default["s5start"] = "Yes";
                Properties.Settings.Default.Save();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Reset();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
        }
       
        private void btnGetData_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSectionRecord frm = new frmSectionRecord();
            frm.label1.Text = label1.Text;
            frm.Show();
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSectionRecord frm = new frmSectionRecord();
            frm.label1.Text = label1.Text;
            frm.Show();
        }

     
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
