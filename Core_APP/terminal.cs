using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace cosmesticClinic.Core_APP
{
    public partial class terminal : Form
    {
        public terminal()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        int user_id = Login.user_id;
        string user_role = Login.user_Role;
        string user_fullname = Login.fullname;
        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Are Sure you want Exist", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void terminal_Load(object sender, EventArgs e)
        {
            //getStaffDetails();
            lbl_role.Text = user_role;
            lbl_fullname.Text = user_fullname;

            getAllStocks();
            getAllCategory();
            getInvoiceNo();

            cmb_mode.SelectedIndex = 0;
        }

        void getStaffDetailss()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            try
            {                
                con.Open();
                OleDbDataReader read;

                //string details = "SELECT * from tbl_User_login WHERE ID ='" + 2 + "'";
                string details = "SELECT * from tbl_User_login WHERE ID = 3 ";
                OleDbCommand cmd = new OleDbCommand(details, con);

                read = cmd.ExecuteReader();

                if (read.Read() == true)
                {
                    lbl_role.Text = read["userrole"].ToString();
                    lbl_fullname.Text = read["fullname"].ToString();
                    con.Close();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("User not Found ", "invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        void getAllCategory()
        {

            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                OleDbDataAdapter da;
                DataTable dt;
                da = new OleDbDataAdapter("Select * From tbl_category order by category_name asc ", con);
                dt = new DataTable();
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr["category_name"] = "-- Select Category  --";
                dt.Rows.InsertAt(dr, 0);
                cmb_category.ValueMember = "id";
                cmb_category.DisplayMember = "category_name";
                cmb_category.DataSource = dt;                
                con.Close();
            }

            catch (System.Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        void getAllStocks()
        {

            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                OleDbDataAdapter da;
                DataTable dt;
                da = new OleDbDataAdapter("Select * From tbl_stock order by book_title asc ", con);
                dt = new DataTable();
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr["book_title"] = "-- Select Book  --";
                dt.Rows.InsertAt(dr, 0);
                cmb_stock.ValueMember = "id";
                cmb_stock.DisplayMember = "book_title";
                cmb_stock.DataSource = dt;
               

                con.Close();
            }

            catch (System.Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void getStockbyCategory()
        {
            if (cmb_category.SelectedText != "-- Select Category  --")
            {
                OleDbConnection con = new OleDbConnection(conStr);
                con.Open();
                try
                {
                    OleDbDataAdapter da;
                    DataTable dt;
                    da = new OleDbDataAdapter("Select * From tbl_stock WHERE category_id=" 
                        + Convert.ToInt32(cmb_category.SelectedValue) + " order by book_title asc ", con);
                    dt = new DataTable();
                    da.Fill(dt);

                    DataRow dr = dt.NewRow();
                    dr["book_title"] = "-- Select Book  --";
                    dt.Rows.InsertAt(dr, 0);
                    cmb_stock.ValueMember = "id";
                    cmb_stock.DisplayMember = "book_title";
                    cmb_stock.DataSource = dt;

                    con.Close();
                }

                catch (System.Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
                con.Close();

                
            }
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_category.Text != "-- Select Category  --")
            {
                getStockbyCategory();
            }
            else
            {
                getAllStocks();
            }
        }

        private void cmb_stock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_stock.Text != "-- Select Book  --")
            {
                getStockDetails();
            }
            else
            {
                clearItem();
                //lblAvailableQty.Text = "0";
            }
        }
        void clearItem()
        {
            foreach (var c in pan_market.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
            lbl_bookTitles.Text = cmb_stock.Text;
            txt_description.Text = "";
            num_qty.Text = "0";
            lbl_unitPrice.Text = "0.00";
            lbl_subTotalPrice.Text = "0.00";
            
        }

        
        double selectedItemCP;
        int selectedItemCategoryID;
        void getStockDetails()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            try
            {
                con.Open();
                OleDbDataReader read;

                string scan = "SELECT * from tbl_stock WHERE id=" + cmb_stock.SelectedValue + " ";                
                OleDbCommand cmd = new OleDbCommand(scan, con);

                read = cmd.ExecuteReader();

                if (read.Read() == true)
                {
                    //bookID = Convert.ToInt32(read["book_title"]);
                    
                    txt_author.Text = Convert.ToString(read["authors"]);                   
                    txt_description.Text = Convert.ToString(read["book_description"]);
                    selectedItemCategoryID = Convert.ToInt16(read["category_id"]);
                    txt_category.Text = Convert.ToString(read["category_name"]);
                    lbl_unitPrice.Text = read["sellingprice"].ToString();
                    lbl_bookTitles.Text = cmb_stock.Text;

                    selectedItemCP = Convert.ToDouble(read["costprice"]);
                    num_qty.Text = "1";
                    lbl_subTotalPrice.Text = subTotal(lbl_unitPrice.Text);
                    con.Close();
                    

                    con.Close();
                }
                else
                {
                    con.Close();
                    MessageBox.Show(" Inalid Input, Select from Drop Down ", "Terminal Input Invalid ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private string subTotal(string price)
        {
            return (Convert.ToDouble(price) * Convert.ToDouble(num_qty.Text)).ToString();
        }

        private void num_qty_KeyUp(object sender, KeyEventArgs e)
        {
            calculateSubTotal();
        }
        private void num_qty_ValueChanged(object sender, EventArgs e)
        {
            calculateSubTotal();
        }
        void calculateSubTotal()
        {
            try
            {
                if (num_qty.Text == "" || num_qty.Text == "0")
                {
                    lbl_subTotalPrice.Text = "0.00";
                }
                else
                {

                    if (lbl_unitPrice.Text != String.Empty)
                    {
                        lbl_subTotalPrice.Text = subTotal(lbl_unitPrice.Text);
                    }
                    // numQtyy.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Input");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (num_qty.Text == string.Empty || num_qty.Text == "0" || Convert.ToInt32(num_qty.Text) <= 0)
                    MessageBox.Show("Add Quantity", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                else
                {
                    if (cmb_stock.Text != "-- Select Book  --")
                    {
                        try
                        {

                            bool _isProductExist = false;

                            if (dgv_cart.Rows.Count > 0)
                            {
                                for (int i = 0; i < dgv_cart.Rows.Count; i++)
                                {
                                    int id = Convert.ToInt32(dgv_cart.Rows[i].Cells[7].Value);
                                    int stockID = Convert.ToInt32(cmb_stock.SelectedValue);

                                    if (stockID == id)
                                    {
                                        _isProductExist = true;
                                        MessageBox.Show("Product Already Exist in Cart!, You Edit Instead", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        break;
                                    }
                                    else
                                    {
                                        _isProductExist = false;
                                    }
                                }
                            }

                            if (_isProductExist == false)
                            {
                                OleDbConnection con = new OleDbConnection(conStr);
                                con.Open();
                                OleDbDataReader read;

                                string scan = "SELECT * from tbl_stock WHERE id=" + cmb_stock.SelectedValue + " ";
                                OleDbCommand cmd = new OleDbCommand(scan, con);
                                read = cmd.ExecuteReader();

                                if (read.Read() == true)
                                {
                                    int qty = Convert.ToInt32(read["quantity"]);
                                    if (num_qty.Value > qty)
                                    {
                                        MessageBox.Show("Out of Stock!, " + qty + " remain!!!", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    }
                                    else
                                    {
                                        if (num_qty.Value >= 0 && lbl_unitPrice.Text != "0.00")
                                        {
                                            calculateSubTotal();
                                            dgv_cart.Rows.Add(dgv_cart.Rows.Count + 1, cmb_stock.Text, txt_author.Text, num_qty.Value,
                                                lbl_unitPrice.Text, lbl_subTotalPrice.Text, txt_description.Text, cmb_stock.SelectedValue,
                                                selectedItemCP, txt_category.Text, selectedItemCategoryID);

                                            clearItem();
                                            iRefreshItem();
                                            getAllCategory();
                                            getAllStocks();
                                        }
                                    }
                                }
                                else
                                {
                                    con.Close();
                                    MessageBox.Show(" No record found", "Invalid ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                con.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Invalid Argument Passed('), Error processing Request ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Product Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument Passed('), " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clearItem();
            getAllCategory();
            getAllStocks();
        }


        //double totalAmount;
        //double totalQty;
        private void iRefreshItem()
        {
            try
            {
                if (dgv_cart.Rows.Count > 0)
                {
                    double totalAmount = 0;
                    double totalQty = 0;
                    int j = 0;
                    for (int i = 0; i < dgv_cart.Rows.Count; i++)
                    {
                        j++;
                        totalAmount += Convert.ToInt32(dgv_cart.Rows[i].Cells[5].Value);
                        totalQty += Convert.ToInt32(dgv_cart.Rows[i].Cells[3].Value);
                        dgv_cart.Rows[i].Cells[0].Value = j;
                    }
                    if (num_discount.Value > 0 || num_discount.Text != String.Empty)
                    {
                        double discount = Convert.ToDouble(num_discount.Value);
                        totalAmount = totalAmount - discount;
                    }

                    lbl_totalPrice.Text = totalAmount.ToString();
                    lbl_totalQty.Text = totalQty.ToString();
                    // numQtyy.Text = "0";
                    //numQtyy.Value = 0;
                    // viewCategory();
                    // getStock();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument Passed(')", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_totalQty_Click(object sender, EventArgs e)
        {            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        
        public static string salesInvoice;
        public void getInvoiceNo()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            OleDbCommand cmd;
            try
            {
                con.Open();
                OleDbDataReader read;
                
                string scan = "SELECT MAX(invoice_number) from tbl_transaction ";
                cmd = new OleDbCommand(scan, con);

                var maxid = cmd.ExecuteScalar() as string;
                if (maxid == null)
                {
                    lbl_invoiceNum.Text = "E-00000001";
                    salesInvoice = lbl_invoiceNum.Text;
                }
                else
                {
                    int interval = int.Parse(maxid.Substring(2, 8));
                    interval++;
                    salesInvoice = String.Format("E-{0:00000000}", interval);
                    lbl_invoiceNum.Text = salesInvoice;
                }
                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument Passed(') or Check Your Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool checkStockQty;
        void reduceStockQuantity(int id, int quantity)
        {
            try
            {
                checkStockQty = false;
                int qty = 0;
                OleDbConnection con = new OleDbConnection(conStr);
                con.Open();
                OleDbDataReader read;

                string scan = "SELECT * from tbl_stock WHERE id=" + id + " ";

                OleDbCommand cmd = new OleDbCommand(scan, con);
                read = cmd.ExecuteReader();

                if (read.Read() == true)
                {
                    qty = Convert.ToInt32(read["quantity"]);
                    if (qty >= quantity)
                    {
                        qty = qty - quantity;
                        con.Close();

                        int reader;
                        con.Open();
                        string stmt = "UPDATE tbl_stock SET quantity=" + qty + " WHERE id=" + id + "";
                        OleDbCommand com = new OleDbCommand(stmt, con);
                        reader = com.ExecuteNonQuery();
                        checkStockQty = true;
                        con.Close();
                    }
                    else if (qty < quantity)
                    {
                        checkStockQty = false;
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument Passed(') or Check Your Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {

            DialogResult dialog = new DialogResult();
            dialog = MessageBox.Show("Are Sure you want continue?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {

                for (int i = 0; i < dgv_cart.Rows.Count; i++)
                {
                    try
                    {
                        // ############################### CHECK STOCK DATA ########################
                        int subQty = Convert.ToInt32(dgv_cart.Rows[i].Cells[3].Value);
                        int id = Convert.ToInt32(dgv_cart.Rows[i].Cells[7].Value);
                        reduceStockQuantity(id, subQty);
                        if (checkStockQty == true)
                        {
                            // #################################### REFRESH THE INVOICE NUMBER  #########################
                            getInvoiceNo();
                            // #################################### DATA TO SAVED  #########################
                            // 0sn 1book_title 2author  3qty 4unitprice 5subtotal 6description 7bookid 8costprice 9categoryName 10categoryid


                            int book_id = Convert.ToInt16(dgv_cart.Rows[i].Cells[7].Value);
                            string book_title = Convert.ToString(dgv_cart.Rows[i].Cells[1].Value);
                            string author = Convert.ToString(dgv_cart.Rows[i].Cells[2].Value);
                            double costprice = Convert.ToDouble(dgv_cart.Rows[i].Cells[8].Value);
                            double price_per_quantity = Convert.ToDouble(dgv_cart.Rows[i].Cells[4].Value);
                            double quantity_sold = Convert.ToDouble(dgv_cart.Rows[i].Cells[3].Value);
                            double subtotal_amount = Convert.ToDouble(dgv_cart.Rows[i].Cells[5].Value);
                            int categoryid = Convert.ToInt16(dgv_cart.Rows[i].Cells[10].Value);
                            string category_name = Convert.ToString(dgv_cart.Rows[i].Cells[9].Value);

                            int staff_id = 2;
                            string staff_name = lbl_fullname.Text;
                            double discount = Convert.ToDouble(num_discount.Value);
                            string invoiceNo = salesInvoice;
                            string mode = cmb_mode.Text;
                            string trans_time = DateTime.Now.ToString("hh: mm :ss tt");
                            string trans_date = DateTime.Now.ToString("yyyy-MM-dd");

                            double trans_total_amt = Convert.ToDouble(lbl_totalPrice.Text);
                            double trans_total_qty = Convert.ToDouble(lbl_totalQty.Text);


                            //  ################################# START THE CONNECTION  ###################
                            OleDbConnection con = new OleDbConnection(conStr);
                            OleDbCommand cmd;
                            OleDbDataReader dr;
                            con.Open();
                            string details = "INSERT INTO tbl_sales(book_id, book_title, author, costprice, price_per_quantity, quantity_sold,  subtotal_amount, " +
                                "discount, invoice_number, staff_id, staff_name, payment_mode, trans_date, trans_time, trans_total_amt, trans_total_qty, categoryid, category_name)" +
                                "VALUES(" + book_id + ",'" + book_title + "','" + author + "'," + costprice + "," + price_per_quantity + "," + quantity_sold + "," + subtotal_amount +
                                "," + discount + ",'" + invoiceNo + "'," + user_id + ",'" + user_fullname + "', '" + mode + "','" + trans_date + "','" + trans_time + "'," +
                                trans_total_amt + "," + trans_total_qty + "," + categoryid + ",'" + category_name + "')";
                            cmd = new OleDbCommand(details, con);
                            dr = cmd.ExecuteReader();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Invalid Argument Passed(') or Check Your Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                addTransaction();
            }
        }

        public void addTransaction()
        {
            try
            {
                //  ################################# START THE CONNECTION  ###################
                OleDbConnection con = new OleDbConnection(conStr);
                OleDbCommand cmd;
                OleDbDataReader dr;
                con.Open();


            double trans_total_amt = Convert.ToDouble(lbl_totalPrice.Text);
            double trans_total_qty = Convert.ToDouble(lbl_totalQty.Text);
            var date = DateTime.Now.ToString("yyyy-MM-dd");
                string time = DateTime.Now.ToString("hh: mm :ss tt");

                string details = "INSERT INTO tbl_transaction(invoice_number, total_item, total_amount, staff_id, staff_name, payment_mode, trans_date, trans_time)" +
                    "VALUES('" + salesInvoice+ "'," + trans_total_qty + "," + trans_total_amt + "," + user_id + ",'" + lbl_fullname.Text + "','" +
                    cmb_mode.Text + "','" + date + "','" + time + "')";

                cmd = new OleDbCommand(details, con);
                dr = cmd.ExecuteReader();

                con.Close();
                createTransactionReport();
        }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument Passed(') or Check Your Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        public void createTransactionReport()
        {
            try
            {
                OleDbDataReader dr;
                OleDbConnection reportConnect = new OleDbConnection(conStr);
                OleDbCommand com;
                reportConnect.Open();
                string time = DateTime.Now.ToString("hh: mm :ss tt");
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                string action = "Make Transaction";
                string invoice = salesInvoice;
                //string menu = "transaction";

                string details = "INSERT INTO tbl_report(staff_name, staff_id, action_perform, date_created, time_created, invoice_number, total_amt_purchased, total_qty_purchased)" +
                    "VALUES('" + lbl_fullname.Text + "'," + user_id + ",'" + action + "','" + date + "','" + time + "','" +
                    lbl_invoiceNum.Text + "','" + lbl_totalPrice.Text + "','" + lbl_totalQty.Text + "')";

                com = new OleDbCommand(details, reportConnect);
                dr = com.ExecuteReader();
                reportConnect.Close();
                dr.Close();
                if (invoice != null)
                {
                    invoice printInvoice = new invoice(invoice);
                    printInvoice.ShowDialog();

                    MessageBox.Show("Operation Sucess", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                //if (printInvoice.ShowDialog() == DialogResult.)
                //{
                resetAll();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument, transaction report", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
}


        private void resetAll()
        {
            clearItem();
            //  btnAdd.Enabled = false;
            int numRows = dgv_cart.Rows.Count;
            for (int i = 0; i < numRows; i++)
            {
                try
                {
                    int max = dgv_cart.Rows.Count - 1;
                    dgv_cart.Rows.Remove(dgv_cart.Rows[max]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable Delete" + ex, "Error Cancelling", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            getInvoiceNo();

            num_discount.Value = 0;
            lbl_totalPrice.Text = "0";
            lbl_totalQty.Text = "0.00";
            cmb_mode.SelectedIndex = 0;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (dgv_cart.Rows.Count > 0)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show(" Cart is not Empty, Do you wish to Continue?!", "Cancel  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    resetAll();
                }
            }
            else
            {
                resetAll();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string prdt = "";
                if (dgv_cart.Rows.Count > 0)
                {
                    prdt = dgv_cart.SelectedRows[0].Cells[1].Value.ToString();
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("Remove " + prdt + "!", "Remove ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (dgv_cart.SelectedRows.Count > 0)
                        {
                            foreach (DataGridViewRow items in this.dgv_cart.SelectedRows)
                            {
                                dgv_cart.Rows.RemoveAt(items.Index);
                            }
                            iRefreshItem();
                        }
                        iRefreshItem();
                    }
                }
                else
                {
                    MessageBox.Show("Select Item", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Argument " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       
    }
}

