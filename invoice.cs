using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Printing;

namespace cosmesticClinic
{
    public partial class invoice : Form
    {
        string invoiceNumber;
        Bitmap memoryImage;

        public invoice(string _invoiceNumber)
        {
            this.invoiceNumber = _invoiceNumber;
            InitializeComponent();

            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private string conStr = connectionString.constr;

        private void invoice_Load(object sender, EventArgs e)
        {
            LoadTransRecord();
            dgv_sales.DataSource = LoadSalesRecord();
        }

        public DataTable LoadSalesRecord()
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = @"
            SELECT (book_title) as [Title], (quantity_sold) as [Qty], 
            (price_per_quantity) as [Unit Price], (subtotal_amount) as [Total#]
            FROM tbl_sales WHERE invoice_number= '" + invoiceNumber+ "' ";
                OleDbDataAdapter da = new OleDbDataAdapter(query, con);
                da.Fill(dt);
            }
            return dt; 
        }
        public void LoadTransRecord()
        {
            try
            {
                OleDbConnection con = new OleDbConnection(conStr);
                OleDbDataReader read;
                con.Open();

                string details = "SELECT total_item, total_amount, trans_date from tbl_transaction WHERE invoice_number='" + invoiceNumber + "'";
                OleDbCommand cmd = new OleDbCommand(details, con);
                read = cmd.ExecuteReader();

                if (read.Read() == true)
                {
                    lblItem.Text = Convert.ToString(read["total_item"]);
                    lblInvoiceNo.Text = this.invoiceNumber.ToString();
                    lblAmt.Text = Convert.ToString(read["total_amount"]);
                    lblDate.Text = Convert.ToString(read["trans_date"]);                    
                    con.Close();

                }
                else
                {
                    con.Close();
                    MessageBox.Show("No Record with" + invoiceNumber, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid  " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void label4_Click(object sender, EventArgs e)
        {
            //PrintDialog myPrintDialog = new PrintDialog();
            //Bitmap memoryImage = new Bitmap(panel1.Width, panel1.Height);
            //panel1.DrawToBitmap(memoryImage, panel1.ClientRectangle);
            //if (myPrintDialog.ShowDialog() == DialogResult.OK)
            //{
            //    System.Drawing.Printing.PrinterSettings values;
            //    values = myPrintDialog.PrinterSettings;
            //    myPrintDialog.Document = printDocument1;
            //    //printDocument1.PrintController = new StandardPrintController();
            //    printDocument1.Print();
            //}
            //printDocument1.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            //CapturePanel(); // convert panel to image

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            // Force small size
            PaperSize receiptSize = new PaperSize("Receipt", 280, 800); // Width = 80mm; Height can be adjusted
            printDoc.DefaultPageSettings.PaperSize = receiptSize;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
                this.Close();
            }
        }
        private Font receiptFont = new Font("Consolas", 10);
        private int currentY = 0; // To track vertical spacing
        private int leftMargin = 10;
        private int pageWidth = 280; // typical POS width in pixels

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //e.Graphics.DrawImage(memoryImage, 0, 0);
            Graphics g = e.Graphics;
            Font headerFont = new Font("Consolas", 12, FontStyle.Bold);
            Font bodyFont = new Font("Consolas", 9);
            int left = 10;
            int right = e.MarginBounds.Right;
            int width = e.MarginBounds.Width;
            int currentY = 10;

            // Center text helper
            string centerText(string text, Font font)
            {
                SizeF size = g.MeasureString(text, font);
                float x = left + (width - size.Width) / 2;
                g.DrawString(text, font, Brushes.Black, x, currentY);
                currentY += (int)size.Height + 2;
                return text;
            }

            // HEADER
            centerText("Book Store", headerFont);
            centerText("Department of Computer Science", bodyFont);            
            centerText("FCI LAUTECH Ogbomoso", bodyFont);            
            centerText("Tel: +234 812 345 6789", bodyFont);

            currentY += 10;
            g.DrawLine(Pens.Black, left, currentY, right, currentY);
            currentY += 5;

            // Invoice and Date
            string invoiceNo = lblInvoiceNo.Text; // You can auto-generate this
            string date = lblDate.Text;

            g.DrawString("Invoice: " + invoiceNo, bodyFont, Brushes.Black, left, currentY);
            g.DrawString("Date: " + date, bodyFont, Brushes.Black, right - 160, currentY);
            currentY += 20;

            g.DrawLine(Pens.Black, left, currentY, right, currentY);
            currentY += 5;

            g.DrawLine(Pens.Black, leftMargin, currentY, pageWidth, currentY);
            currentY += 10;
            g.DrawLine(Pens.Black, leftMargin, currentY, pageWidth, currentY);
            currentY += 10;

            // TABLE HEADER
            //g.DrawString("Item         Qty  Price  Total", receiptFont, Brushes.Black, leftMargin, currentY);
            //currentY += 20;

            // Loop through items in dgv_sales
            foreach (DataGridViewRow row in dgv_sales.Rows)
            {
                if (row.IsNewRow) continue;

                string title = row.Cells[0].Value.ToString();
                string qty = row.Cells[1].Value.ToString();
                string unitPrice = row.Cells[2].Value.ToString();
                string total = row.Cells[3].Value.ToString();

                g.DrawString("Item", receiptFont, Brushes.Black, leftMargin + 5, currentY);
                currentY += 15;
                g.DrawString(title, receiptFont, Brushes.Black, leftMargin + 5, currentY);
                currentY += 15;

                string qtyLine = $"Qty: {qty}  Price: {unitPrice}  Total: {total}";
                g.DrawString(qtyLine, receiptFont, Brushes.Black, leftMargin + 5, currentY);
                currentY += 15;
                g.DrawLine(Pens.Black, leftMargin, currentY, pageWidth, currentY);
                currentY += 10;

            }

            // DIVIDER
            g.DrawLine(Pens.Black, leftMargin, currentY, pageWidth, currentY);
            currentY += 10;
            
            // TOTALS
            g.DrawString("Total Items:", bodyFont, Brushes.Black, left + 140, currentY);
            g.DrawString(lblItem.Text, bodyFont, Brushes.Black, left + 240, currentY);
            currentY += 20;

            g.DrawString("Total Amount:", bodyFont, Brushes.Black, left + 140, currentY);
            g.DrawString("₦" + lblAmt.Text, bodyFont, Brushes.Black, left + 240, currentY);
            currentY += 30;

            // FOOTER
            centerText("Thank you for your purchase!", bodyFont);
            
        }
        private void CapturePanel()
        {
            memoryImage = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(memoryImage, new Rectangle(0, 0, this.Width, this.Height));
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
