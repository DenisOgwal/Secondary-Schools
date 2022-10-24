using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace College_Management_System
{
    public partial class FrmGradingSystem :DevComponents.DotNetBar.Office2007Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public FrmGradingSystem()
        {
            InitializeComponent();
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
        private void FrmPatientClear_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aPaymentsDataset.GradingSystem' table. You can move, or remove it, as needed.
            this.gradingSystemTableAdapter.Fill(this.aPaymentsDataset.GradingSystem);
            AutocompleSession();
          
        }
        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.Equals(true))
                    {
                        row.Selected = true;

                       // selectedrowtotal();
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.IsCurrentCellDirty)
                {
                    dataGridView3.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmGradingSystem frm = new FrmGradingSystem();
            frm.label6.Text = label6.Text;
            frm.ShowDialog();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        string oldsystem = null;
        public void checkboxes()
        {
            if (checkBox1.Checked == true) { oldsystem = "New"; } else { oldsystem = "Old"; }
            if (checkBox2.Checked == true) { oldsystem = "Old"; } else { oldsystem = "New"; }
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            checkboxes();
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "update GradingSystem set Grading=@d2,Year=@d3 where Class=@d1";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 10, "Class"));
                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 20, "Grading"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Year"));

                cmd.Parameters["@d1"].Value = label1.Text;
                cmd.Parameters["@d2"].Value = oldsystem;
                cmd.Parameters["@d3"].Value = cmbSession.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView3.CurrentRow;
                label1.Text = dr.Cells[0].Value.ToString();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM GradingSystem where Class='" + label1.Text + "' ";
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    oldsystem = rdr["Grading"].ToString().Trim();
                    if (oldsystem == "Old")
                    {
                        checkBox2.Checked = true;
                    }
                    else
                    {
                        checkBox2.Checked = false;
                    }
                    if (oldsystem == "New")
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
           
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
        }
    }
}
