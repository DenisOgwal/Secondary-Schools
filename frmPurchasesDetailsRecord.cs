using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmPurchasesDetailsRecord : Form
    {
        //SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();

        public frmPurchasesDetailsRecord()
        {
            InitializeComponent();
        }
        private void AutocompleteCourse()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Product) FROM Purchases", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                Course.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    Course.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AutocompleteScholarNo()
        {
            try
            {
                SqlConnection CN = new SqlConnection(cs.DBConn);
                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Purchaseid) FROM Purchases", CN);
                ds = new DataSet("ds");
                adp.Fill(ds);
                dtable = ds.Tables[0];
                ScholarNo.Items.Clear();
                foreach (DataRow drow in dtable.Rows)
                {
                    ScholarNo.Items.Add(drow[0].ToString());
                }
                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ExportExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView2.RowCount - 1;
                colsTotal = dataGridView2.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView2.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView2.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView3.DataSource == null)
            {
                MessageBox.Show("Sorry nothing to export into excel sheet..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();
            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;
                rowsTotal = dataGridView3.RowCount - 1;
                colsTotal = dataGridView3.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView3.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView3.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;
                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Course.Text = "";
            Date_from.Text = DateTime.Today.ToString();
            Date_to.Text = DateTime.Today.ToString();
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            PaymentDateFrom.Text = DateTime.Today.ToString();
            PaymentDateTo.Text = DateTime.Today.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Course.Text = "";
            Date_from.Text = System.DateTime.Today.ToString();
            Date_to.Text = System.DateTime.Today.ToString();
            dataGridView1.DataSource = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ScholarNo.Text = "";
            dataGridView2.DataSource = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            PaymentDateFrom.Text = System.DateTime.Today.ToString();
            PaymentDateTo.Text = System.DateTime.Today.ToString();
            dataGridView3.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (Course.Text == "")
                {
                    MessageBox.Show("Please select Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Course.Focus();
                    return;
                }

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(purchaseid)[Purchase ID], RTRIM(year)[Session],RTRIM(term)[Semester],RTRIM(employeeid)[Staff ID],RTRIM(product)[Product],RTRIM(quantity)[Quantity],RTRIM(units)[Units],RTRIM(quality)[Quality],RTRIM(unitcost)[Unit Cost],RTRIM(department)[Department],RTRIM(supplier)[Supplier],RTRIM(manufacturer)[Manufacturer],RTRIM(standards)[Standards],RTRIM(totalcost)[Total Cost],RTRIM(manufacturedate)[Manufacture Date],RTRIM(purchasedate)[Date of Purchase],RTRIM(expirydate)[Date of Expiry]  from Purchases where product= '" + Course.Text + "' and purchasedate between @date1 and @date2 order by purchasedate", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "purchasedate").Value = Date_from.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "purchasedate").Value = Date_to.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Purchases");
                dataGridView1.DataSource = myDataSet.Tables["Purchases"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScholarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(purchaseid)[Purchase ID], RTRIM(year)[Session],RTRIM(term)[Semester],RTRIM(employeeid)[Staff ID],RTRIM(product)[Product],RTRIM(quantity)[Quantity],RTRIM(units)[Units],RTRIM(quality)[Quality],RTRIM(unitcost)[Unit Cost],RTRIM(department)[Department],RTRIM(supplier)[Supplier],RTRIM(manufacturer)[Manufacturer],RTRIM(standards)[Standards],RTRIM(totalcost)[Total Cost],RTRIM(manufacturedate)[Manufacture Date],RTRIM(purchasedate)[Date of Purchase],RTRIM(expirydate)[Date of Expiry]  from Purchases where purchaseid= '" + ScholarNo.Text + "' order by purchasedate", con);
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Purchases");
                dataGridView2.DataSource = myDataSet.Tables["Purchases"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView2.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView2.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView3.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView3.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
        }

        private void frmBusFeePaymentRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmPurchaseDetails frm = new frmPurchaseDetails();
            frm.label13.Text = label13.Text;
            frm.label21.Text = label14.Text;
            frm.Show();*/
        }

        private void frmBusFeePaymentRecord_Load(object sender, EventArgs e)
        {
            AutocompleteCourse();
            AutocompleteScholarNo();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("select RTrim(purchaseid)[Purchase ID], RTRIM(year)[Session],RTRIM(term)[Semester],RTRIM(employeeid)[Staff ID],RTRIM(product)[Product],RTRIM(quantity)[Quantity],RTRIM(units)[Units],RTRIM(quality)[Quality],RTRIM(unitcost)[Unit Cost],RTRIM(department)[Department],RTRIM(supplier)[Supplier],RTRIM(manufacturer)[Manufacturer],RTRIM(standards)[Standards],RTRIM(totalcost)[Total Cost],RTRIM(manufacturedate)[Manufacture Date],RTRIM(purchasedate)[Date of Purchase],RTRIM(expirydate)[Date of Expiry]  from Purchases where purchasedate between @date1 and @date2 order by purchasedate", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, "purchasedate").Value = PaymentDateFrom.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, "purchasedate").Value = PaymentDateTo.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Purchases");
                dataGridView3.DataSource = myDataSet.Tables["Purchases"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            this.Hide();
            frmPurchaseDetails frm = new frmPurchaseDetails();
            frm.purchaseid.Text = dr.Cells[0].Value.ToString();
            frm.year.Text = dr.Cells[1].Value.ToString();
            frm.term.Text = dr.Cells[2].Value.ToString();
            frm.employeeid.Text = dr.Cells[3].Value.ToString();
            frm.product.Text = dr.Cells[4].Value.ToString();
            frm.Quantity.Text = dr.Cells[5].Value.ToString();
            frm.units.Text = dr.Cells[6].Value.ToString();
            frm.Quality.Text = dr.Cells[7].Value.ToString();
            frm.unitcost.Text = dr.Cells[8].Value.ToString();
            frm.department.Text = dr.Cells[9].Value.ToString();
            frm.supplier.Text = dr.Cells[10].Value.ToString();
            frm.manufacturer.Text = dr.Cells[11].Value.ToString();
            frm.standards.Text = dr.Cells[12].Value.ToString();
            frm.total.Text = dr.Cells[13].Value.ToString();
            frm.manufacturedate.Text = dr.Cells[14].Value.ToString();
            frm.Purchasedate.Text = dr.Cells[15].Value.ToString();
            frm.expirydate.Text = dr.Cells[16].Value.ToString();

            frm.label13.Text = label13.Text;
            frm.label21.Text = label14.Text;
            frm.ShowDialog();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView2.SelectedRows[0];
            this.Hide();
            frmPurchaseDetails frm = new frmPurchaseDetails();

            frm.purchaseid.Text = dr.Cells[0].Value.ToString();
            frm.year.Text = dr.Cells[1].Value.ToString();
            frm.term.Text = dr.Cells[2].Value.ToString();
            frm.employeeid.Text = dr.Cells[3].Value.ToString();
            frm.product.Text = dr.Cells[4].Value.ToString();
            frm.Quantity.Text = dr.Cells[5].Value.ToString();
            frm.units.Text = dr.Cells[6].Value.ToString();
            frm.Quality.Text = dr.Cells[7].Value.ToString();
            frm.unitcost.Text = dr.Cells[8].Value.ToString();
            frm.department.Text = dr.Cells[9].Value.ToString();
            frm.supplier.Text = dr.Cells[10].Value.ToString();
            frm.manufacturer.Text = dr.Cells[11].Value.ToString();
            frm.standards.Text = dr.Cells[12].Value.ToString();
            frm.total.Text = dr.Cells[13].Value.ToString();
            frm.manufacturedate.Text = dr.Cells[14].Value.ToString();
            frm.Purchasedate.Text = dr.Cells[15].Value.ToString();
            frm.expirydate.Text = dr.Cells[16].Value.ToString();

            frm.label13.Text = label13.Text;
            frm.label21.Text = label14.Text;
            frm.ShowDialog();
        }

        private void dataGridView3_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow dr = dataGridView3.SelectedRows[0];
            this.Hide();
            frmPurchaseDetails frm = new frmPurchaseDetails();

            frm.purchaseid.Text = dr.Cells[0].Value.ToString();
            frm.year.Text = dr.Cells[1].Value.ToString();
            frm.term.Text = dr.Cells[2].Value.ToString();
            frm.employeeid.Text = dr.Cells[3].Value.ToString();
            frm.product.Text = dr.Cells[4].Value.ToString();
            frm.Quantity.Text = dr.Cells[5].Value.ToString();
            frm.units.Text = dr.Cells[6].Value.ToString();
            frm.Quality.Text = dr.Cells[7].Value.ToString();
            frm.unitcost.Text = dr.Cells[8].Value.ToString();
            frm.department.Text = dr.Cells[9].Value.ToString();
            frm.supplier.Text = dr.Cells[10].Value.ToString();
            frm.manufacturer.Text = dr.Cells[11].Value.ToString();
            frm.standards.Text = dr.Cells[12].Value.ToString();
            frm.total.Text = dr.Cells[13].Value.ToString();
            frm.manufacturedate.Text = dr.Cells[14].Value.ToString();
            frm.Purchasedate.Text = dr.Cells[15].Value.ToString();
            frm.expirydate.Text = dr.Cells[16].Value.ToString();

            frm.label13.Text = label13.Text;
            frm.label21.Text = label14.Text;
            frm.ShowDialog();
        }
    }
}



