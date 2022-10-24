﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmClientDetails2 : Form
    {
          DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmClientDetails2()
        {
            InitializeComponent();
        }
        public void loadpay()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Admission_no)[Admission Number],RTRIM(Student_name)[Student Names] from StudentRegistration order by ID DESC", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView1.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmClientDetails_Load(object sender, EventArgs e)
        {
            loadpay();
            dataGridView1.Select();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (clientnames.Text == "")
            {
                MessageBox.Show("Please Enter Client Names", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clientnames.Focus();
                return;
            }
            this.Hide();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTRIM(Admission_no)[Admission Number],RTRIM(Student_name)[Student Names] from Student where ScholarNo like '" + textBoxX1.Text + "%' OR Student_name Like '" + textBoxX1.Text + "%'  order by ID DESC", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "StudentRegistration");
                dataGridView1.DataSource = myDataSet.Tables["StudentRegistration"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try{
            DataGridViewRow dr = dataGridView1.CurrentRow;
            clientnames.Text=dr.Cells[0].Value.ToString();
            Accountnames.Text = dr.Cells[1].Value.ToString();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Select();
                e.Handled = true;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dataGridView1.CurrentRow.Selected = true;
                e.Handled = true;
                try
                {
                    DataGridViewRow dr = dataGridView1.CurrentRow;
                    clientnames.Text = dr.Cells[0].Value.ToString();
                    Accountnames.Text = dr.Cells[1].Value.ToString();
                    buttonX1.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                if (e.KeyCode == Keys.Up)
                {
                    DataGridViewRow dr7 = dataGridView1.CurrentRow;
                    if (dr7.Index.Equals(0))
                    {
                        textBoxX1.Select();
                        textBoxX1.Focus();
                        e.Handled = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
