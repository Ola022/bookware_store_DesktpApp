//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Data.OleDb;

namespace cosmesticClinic.Core_APP
{
    public partial class form_sales : Form
    {
        public form_sales()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            getAllSales();
        }
        private void form_sales_Load(object sender, EventArgs e)
        {
            getAllSales();
        }
        public void getAllSales()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                string details = "SELECT (id) as [ID],(invoice_number) as [invoice_number], (book_title) as [Book Title],(author) as [Author]" +
                    ",(quantity_sold) as [Quantity],(subtotal_amount) as [Total Amount],(staff_name) as [Staff],(payment_mode) as [Payment Mode]," +
                    "(trans_date) as [Date], (trans_time) as [Time] from tbl_sales  order by invoice_number desc ";

                OleDbCommand cmd = new OleDbCommand(details, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                DataSet myDS = new DataSet();
                da.Fill(myDS, "inventory");
                dataGridView1.DataSource = myDS.Tables["inventory"].DefaultView;
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Columns[0].Visible = false;
                }
                else
                {
                    MessageBox.Show("No Record found ", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Invalid  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
                    XcelApp.Application.Workbooks.Add(Type.Missing);
                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    XcelApp.Columns.AutoFit();
                    XcelApp.Visible = true;
                }
                else
                {
                    MessageBox.Show("No Data Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Make sure Microsoft Office is Install", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}
