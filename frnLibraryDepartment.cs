using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace College_Management_System
{
    public partial class frnLibraryDepartment : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frnLibraryDepartment()
        {
            InitializeComponent();
        }

        private void btnNewRecord_Click(object sender, EventArgs e)
        {
            txtDepartmentID.Text = "";
            txtDepartmentName.Text = "";
            txtDepartmentName.Focus();
            btnDelete.Enabled = true;
            btnUpdate_record.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartmentName.Text == "")
            {
                MessageBox.Show("Please enter department name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepartmentName.Focus();
                return;
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select AcademicDepartments from AcademicUnits where  AcademicDepartments= '" + txtDepartmentName.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Department Name Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDepartmentName.Text = "";
                    txtDepartmentName.Focus();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into AcademicUnits(AcademicDepartments) VALUES (@d2)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "AcademicDepartments"));
                cmd.Parameters["@d2"].Value = txtDepartmentName.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                Autocomplete();
                con.Close();
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
                SqlCommand cmd = new SqlCommand("SELECT  distinct AcademicDepartments FROM AcademicUnits", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "AcademicUnits");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["AcademicDepartments"].ToString());
                }
                txtDepartmentName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDepartmentName.AutoCompleteCustomSource = col;
                txtDepartmentName.AutoCompleteMode = AutoCompleteMode.Suggest;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frnLibraryDepartment_Load(object sender, EventArgs e)
        {
            Autocomplete();
            try
            {
                string prices = null;
                string pricess = null;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM UserAccess where UserName='" + label3.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    prices = rdr["Deletes"].ToString().Trim();
                    pricess = rdr["Updates"].ToString().Trim();
                    if (prices == "Yes") { btnDelete.Enabled = true; } else { btnDelete.Enabled = false; }
                    if (pricess == "Yes") { btnUpdate_record.Enabled = true; } else { btnUpdate_record.Enabled = false; }
                }
                if (label3.Text == "ADMIN")
                {
                    btnDelete.Enabled = true;
                    btnUpdate_record.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtDepartmentName.Text == "")
            {
                MessageBox.Show("Please enter department Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepartmentID.Focus();
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
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select Department from Employee where Department=@find";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "Department"));
                cmd.Parameters["@find"].Value = txtDepartmentName.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Unable to delete..Already in use", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDepartmentID.Text = "";
                    txtDepartmentName.Text = "";
                    txtDepartmentName.Focus();
                    btnDelete.Enabled = false;
                    btnUpdate_record.Enabled = false;
                    Autocomplete();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                int RowsAffected = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from AcademicUnits where AcademicDepartments=@DELETE1;";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 30, "AcademicDepartments"));
                cmd.Parameters["@DELETE1"].Value = txtDepartmentName.Text;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepartmentID.Text = "";
                    txtDepartmentName.Text = "";
                    txtDepartmentID.Focus();
                    btnDelete.Enabled = false;
                    btnUpdate_record.Enabled = false;
                    Autocomplete();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepartmentID.Text = "";
                    txtDepartmentName.Text = "";
                    txtDepartmentID.Focus();
                    btnDelete.Enabled = false;
                    btnUpdate_record.Enabled = false;
                    Autocomplete();
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

        private void btnUpdate_record_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update AcademicUnits set AcademicDepartments=@d2 where AcademicDepartments=@d1";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 30, "AcademicDepartments"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 30, "AcademicDepartments"));
                cmd.Parameters["@d1"].Value = txtDepartmentName.Text;
                cmd.Parameters["@d2"].Value = txtDepartmentName.Text;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate_record.Enabled = false;
                Autocomplete();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frnLibraryDepartmentRecord frm = new frnLibraryDepartmentRecord();
            frm.label1.Text = label1.Text;
            frm.ShowDialog();
        }
    }
}

