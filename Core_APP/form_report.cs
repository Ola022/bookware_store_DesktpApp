using OfficeOpenXml;
using System.Data;
using System.Data.OleDb;

namespace cosmesticClinic.Core_APP
{
    public partial class form_report : Form
    {
        public form_report()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        private void form_report_Load(object sender, EventArgs e)
        {
            getAllReport();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            getAllReport();
        }
        public void getAllReport()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                string details = "SELECT (id) as [ID],(action_perform) as [Report], (staff_name) as [Staff], (invoice_number) as [Invoice Number]" +
                    ",(total_amt_purchased) as [Total Amount], (date_created) as [Date Create],(time_created) as [Time] from tbl_report  order by invoice_number desc ";

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

            //try
            //{
            //    if (dataGridView1.Rows.Count > 0)
            //    {
            //        Microsoft.Office.Interop.Excel._Application XcelApp = new Microsoft.Office.Interop.Excel.Application();
            //        XcelApp.Application.Workbooks.Add(Type.Missing);
            //        for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            //        {
            //            XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            //        }
            //        for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //        {
            //            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //            {
            //                XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
            //            }
            //        }
            //        XcelApp.Columns.AutoFit();
            //        XcelApp.Visible = true;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No Data Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Make sure Microsoft Office is Install", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }


        private void ExportToExcel(DataGridView dgv)
        {
            // Required for EPPlus v8+
            

            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Export");

                // Add column headers
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgv.Columns[i].HeaderText;
                }

                // Add rows
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = dgv.Rows[row].Cells[col].Value?.ToString();
                    }
                }

                // Save file
                using (SaveFileDialog saveFile = new SaveFileDialog()
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = "Export.xlsx"
                })
                {
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFile.FileName, package.GetAsByteArray());
                        MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

    }
}
