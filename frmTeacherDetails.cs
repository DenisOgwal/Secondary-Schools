using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace College_Management_System
{
    public partial class frmTeacherDetails : Form
    {
        ConnectionString cs = new ConnectionString();
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        public frmTeacherDetails()
        {
            InitializeComponent();
        }

        private void frmTeacherDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmStudentAcess frm = new frmStudentAcess();
            frm.Show();*/
        }

        private void frmTeacherDetails_Load(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(StaffName) from Employee where  Designation like '%Teacher%'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   teachername.Items.Add(rdr[0]);
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String inquery3 = "SELECT position FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                cmd = new SqlCommand(inquery3, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    label7.Text = rdr.GetString(0);
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
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
                String inquery3 = "SELECT subject1 FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                cmd = new SqlCommand(inquery3, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    label6.Text = rdr.GetString(0);
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
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
                String inquery3 = "SELECT subject2 FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                cmd = new SqlCommand(inquery3, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    label8.Text = rdr.GetString(0);
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
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
                String inquery3 = "SELECT classes FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                cmd = new SqlCommand(inquery3, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    label9.Text = rdr.GetString(0);
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
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
                String inquery3 = "SELECT officeno FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                cmd = new SqlCommand(inquery3, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    label10.Text = rdr.GetString(0);
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
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
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Picture FROM Employee WHERE StaffName like '%" + teachername.Text + "%'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    MemoryStream stream = new MemoryStream();
                    byte[] image = (byte[])rdr["Picture"];
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
            
        }  
       
    }
}
