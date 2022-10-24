﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class FrmSickStudentDischargeRecord : Form
    {
        ConnectionString cs = new ConnectionString();

        public FrmSickStudentDischargeRecord()
        {
            InitializeComponent();
        }

        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(cs.DBConn);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        public DataView GetData()
        {
            dynamic SelectQry = "SELECT RTRIM(ID)[ID],RTRIM(ScholarNo)[Student No.],RTRIM(Year)[Year],RTRIM(Class)[Class],RTRIM(Term)[Term],RTRIM(BedNo)[Bed Name],RTRIM(JoiningDate)[Admission Date],RTRIM(DischargeDate)[Discharge Date],(DischargeNotes)[Discharge Notes] FROM SickBayAdmissions where Discharged='Yes' order by ID Desc";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }
        private void frmEventRecord_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetData();
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            FrmSickStudentAdmission frm = new FrmSickStudentAdmission();
            frm.label6.Text = label1.Text;
            frm.ShowDialog();
            frm.txtEventID.Text = dr.Cells[0].Value.ToString();
            frm.txtEventName.Text = dr.Cells[1].Value.ToString();
            frm.dtpStartingDate.Text = dr.Cells[2].Value.ToString();
            frm.dtpStartingTime.Text = dr.Cells[3].Value.ToString();
            frm.dtpEndingDate.Text = dr.Cells[4].Value.ToString();
            frm.dtpEndingTime.Text = dr.Cells[5].Value.ToString();
            frm.txtManagedBy.Text = dr.Cells[6].Value.ToString();
            frm.txtActivities.Text = dr.Cells[7].Value.ToString();
            frm.btnDelete.Enabled = true;
            frm.btnUpdate_record.Enabled = true;
            frm.txtEventName.Focus();
            frm.btnSave.Enabled = false;*/
        }
    }
}
