﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmBalanceForwardRecord : Form
    {
        ConnectionString cs = new ConnectionString();

        public frmBalanceForwardRecord()
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
            dynamic SelectQry = "SELECT RTRIM(BalanceForwardID)[Drawings ID],RTRIM(DateRegistered)[Date],RTRIM(Price)[Ammount],RTRIM(Description)[Description],RTRIM(Term)[Term],RTRIM(Year)[Year] FROM BalanceForward order by ID DESC ";
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
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmBalanceForward frm = new frmBalanceForward();
            frm.label21.Text = label2.Text;
            frm.ShowDialog();
            frm.purchaseid.Text = dr.Cells[0].Value.ToString();
            frm.Purchasedate.Text = dr.Cells[1].Value.ToString();
            frm.Price.Text = dr.Cells[2].Value.ToString();
            frm.description.Text = dr.Cells[3].Value.ToString();
          
        }

        private void frmEventRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmEvent frm = new frmEvent();
            frm.label6.Text = label1.Text;
            frm.label8.Text = label2.Text;
            frm.Show();*/
        }
    }
}
