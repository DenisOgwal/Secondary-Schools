using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
namespace College_Management_System
{
    public partial class frmStore : Form
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmStore()
        {
            InitializeComponent();
        }
        private void frmStore_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.User.Text = label1.Text;
            frm.UserType.Text = label2.Text;
            frm.Show();*/
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Names.Items.Clear();
            Type.Items.Clear();
            dataGridViewX1.DataSource = null;
            Datefrom.Text = DateTime.Now.ToString();
            Dateto.Text = DateTime.Now.ToString();
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Type.Text == "Drug")
            {
                try
                {
                    Names.Enabled = true;
                    SqlConnection CN = new SqlConnection(cs.DBConn);
                    CN.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(DrugName) FROM DrugPurchase", CN);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    Names.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        Names.Items.Add(drow[0].ToString());
                    }
                    CN.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (Type.Text == "Equipment")
            {
                try
                {
                    Names.Enabled = true;
                    SqlConnection CN = new SqlConnection(cs.DBConn);
                    CN.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Equipmentname) FROM EquipmentPurchase", CN);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    Names.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        Names.Items.Add(drow[0].ToString());
                    }
                    CN.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Type.Text == "")
                {
                    MessageBox.Show("Please select Type Of Product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Type.Focus();
                    return;
                }
                if (Names.Text == "")
                {
                    MessageBox.Show("Please select Product Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Names.Focus();
                    return;
                }
                if (Type.Text == "Drug")
                {
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select RTRIM(PurchaseID)[Purchase ID], RTRIM(DrugName)[Drug Name], RTRIM(PurchaseDate)[Purchase Date], RTRIM(InvoiceNo)[InvoiceNo], RTRIM(Cures)[Cure For], RTRIM(MFGDate)[Manufacture date],RTRIM(EXPDate)[Expiry Date], RTRIM(BatchNo)[Batch No.], RTRIM(Manufacturer)[Manufacturer],RTRIM(Supplier)[Supplier],RTRIM(Quantity)[Quantity],RTRIM(Units)[Units], RTRIM(NoPerUnit)[No Per Unit],RTRIM(SmallUnit)[Small Unit], RTRIM(Indication)[Indication],RTRIM(Warning)[Warning], RTRIM(UnitCost)[Unit Cost], RTRIM(TotalCost)[Total Cost],RTRIM(Origin)[Country of Origin],RTRIM(StaffName)[Staff Name] from DrugPurchase where  PurchaseDate between @date1 and @date2 order by PurchaseDate", con);
                        cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " PurchaseDate").Value = Datefrom.Value.Date;
                        cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " PurchaseDate").Value = Dateto.Value.Date;
                        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                        DataSet myDataSet = new DataSet();
                        myDA.Fill(myDataSet, "DrugPurchase");
                        dataGridViewX1.DataSource = myDataSet.Tables["DrugPurchase"].DefaultView;
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (Type.Text == "Equipment")
                {
                    try
                    {
                        con = new SqlConnection(cs.DBConn);
                        con.Open();
                        cmd = new SqlCommand("select RTRIM(PurchaseID)[Purchase ID], RTRIM(Equipmentname)[Equipment Name], RTRIM(PurchaseDate)[Purchase Date], RTRIM(InvoiceNo)[InvoiceNo], RTRIM(Section)[Section],RTRIM(Model)[Model], RTRIM(MfgDate)[Manufacture Date], RTRIM(ExpDate)[Expiry Date],RTRIM(Country)[Origin],RTRIM(BatchNo)[Batch No.],RTRIM(Manufacturer)[Manufacturer], RTRIM(Supplier)[Supplier],RTRIM(Quantity)[Quantity], RTRIM(Units)[Units],RTRIM(NoPerUnit)[No Per Unit], RTRIM(SmallUnit)[Small Unit], RTRIM(Specifications)[Specifications], RTRIM(warnings)[warnings], RTRIM(Description)[Description], RTRIM(UnitCost)[Unit Cost], RTRIM(TotalCost)[Total Cost],RTRIM(StaffName)[Staff Name] from EquipmentPurchase where  EquipmentPurchase.PurchaseDate between @date1 and @date2 order by PurchaseDate", con);
                        cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " PurchaseDate").Value = Datefrom.Value.Date;
                        cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " PurchaseDate").Value = Dateto.Value.Date;
                        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                        DataSet myDataSet = new DataSet();
                        myDA.Fill(myDataSet, "EquipmentPurchase");
                        dataGridViewX1.DataSource = myDataSet.Tables["EquipmentPurchase"].DefaultView;
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (Type.Text == "Equipment")
            {
                try
                {
                    DataGridViewRow dr = dataGridViewX1.SelectedRows[0];
                    this.Hide();
                    frmEquipmentPurchase frm = new frmEquipmentPurchase();

                    frm.purchaseid.Text = dr.Cells[0].Value.ToString();
                    frm.equipmentname.Text = dr.Cells[1].Value.ToString();
                    //frm.purchasedate.Text = dr.Cells[2].Value.ToString();
                    //frm.section.Text = dr.Cells[4].Value.ToString();
                    frm.model.Text = dr.Cells[5].Value.ToString();
                    //frm.manufacturedate.Text = dr.Cells[5].Value.ToString();
                    //frm.expirydate.Text = dr.Cells[6].Value.ToString();
                    /*frm.origin.Text = dr.Cells[8].Value.ToString();
                    frm.batchno.Text = dr.Cells[9].Value.ToString();
                    frm.manufacturer.Text = dr.Cells[10].Value.ToString();
                    frm.supplier.Text = dr.Cells[11].Value.ToString();
                    frm.quantity.Text = dr.Cells[12].Value.ToString();
                    frm.units.Text = dr.Cells[13].Value.ToString();
                    frm.noperunit.Text = dr.Cells[14].Value.ToString();
                    frm.smallunit.Text = dr.Cells[15].Value.ToString();
                    frm.specifications.Text = dr.Cells[16].Value.ToString();
                    frm.warning.Text = dr.Cells[17].Value.ToString();
                    frm.description.Text = dr.Cells[18].Value.ToString();
                    frm.costperunit.Text = dr.Cells[19].Value.ToString();
                    frm.totalcost.Text = dr.Cells[20].Value.ToString();
                    frm.staffid.Text = dr.Cells[21].Value.ToString();
                    frm.staffname.Text = dr.Cells[22].Value.ToString();
                    frm.invoices.Text = dr.Cells[3].Value.ToString();*/
                    frm.label1.Text = label1.Text;
                    frm.label2.Text = label2.Text;
                    frm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.DataSource == null)
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
                xlApp.Columns[3].Cells.NumberFormat = "@";
                rowsTotal = dataGridViewX1.RowCount - 1;
                colsTotal = dataGridViewX1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridViewX1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridViewX1.Rows[I].Cells[j].Value;
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
    }
}
