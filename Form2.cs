using System.Data.OleDb;
using System.Data;

namespace cosmesticClinic
{
    public partial class Form2 : Form
    {

        private Button currentButton;        

        public Form2()
        {
            InitializeComponent();
            //this.ControlBox = false;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        
        int user_id = Login.user_id;
        string user_role = Login.user_Role;
        string user_fullname = Login.fullname;
        private string conStr = connectionString.constr;
        private void Form2_Load(object sender, EventArgs e)
        {
            Reset();
            loadMetricsMethod();

        }

        private Form activateForm;

        private void loadMetricsMethod()
        {
            lblTotalProducts.Text = GetTotalProducts().ToString();
            lblTodaySales.Text = "₦" + GetTodaySales().ToString("N2");
            lblTotalUsers.Text = GetTotalUsers().ToString();
            LoadLowStockProducts(dataGridViewLowStock);
            dataGridRecentSales.DataSource = GetRecentTransactions();
        }
        public DataTable GetRecentTransactions()
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = @"
            SELECT TOP 15 (invoice_number) as [invoice_number], (book_title) as [Book Title], (quantity_sold) as [Quantity], 
            (payment_mode) as [Payment Mode], (trans_date) as [Date], (trans_time) as [Time]
            FROM tbl_sales 
            ORDER BY invoice_number DESC";
                OleDbDataAdapter da = new OleDbDataAdapter(query, con);
                da.Fill(dt);
            }
            return dt;
        }

        public void LoadLowStockProducts(DataGridView dgv)
        {
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                try
                {
                    con.Open();
                    string query = "SELECT (book_title) as [Book Title], (quantity) as [Quantity] FROM tbl_stock WHERE quantity <= 20";
                    OleDbDataAdapter da = new OleDbDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading low stock: " + ex.Message);
                }
            }
        }

        public int GetTotalUsers()
        {
            int total = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM tbl_User_login";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    total = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching total users: " + ex.Message);
                }
            }
            return total;
        }

        public decimal GetTodaySales()
        {
            decimal total = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                try
                {
                    con.Open();
                    string query = "SELECT SUM(total_amount) FROM tbl_transaction WHERE trans_date = ?";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    cmd.Parameters.AddWithValue("?", DateTime.Now.ToString("yyyy-MM-dd"));
                    //cmd.Parameters.AddWithValue("?", DateTime.Now.Date);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                        total = Convert.ToDecimal(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching today's sales: " + ex.Message);
                }
            }
            return total;
        }

        public int GetTotalProducts()
        {
            int total = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = "SELECT COUNT(*) FROM tbl_stock";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                total = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return total;
        }

        private void ActivateButton(Object btnSender, Color color)
        {
            if (btnSender != null)
            {
                disableButton();
                //Color color = selectThemeColor();
                currentButton = (Button)btnSender;
                currentButton.BackColor = color;
                currentButton.ForeColor = Color.White;
                currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // panelTitleBar.BackColor = color;

                //  ThemeColor.primaryColor = color;
                // ThemeColor.secondaryColor = Color.Gainsboro;
            }
        }
        private void openChildForm(Form childForm, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            ActivateButton(btnSender, Color.Teal);
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            //panelDashboard.Visible = false; 51, 51, 76
        }
        private void disableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    //previousBtn.BackColor = Color.FromArgb(31, 16, 82); 31, 16, 82
                    previousBtn.BackColor = Color.Indigo; //.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }

        }

        private void Reset()
        {
            disableButton();
            currentButton = null;
            btn_dashboard.BackColor = Color.Teal;
            lblPanelName.Text = "DASHBOARD";
            // panel_dashboard.Visible = true;

        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            if (activateForm != null)
                activateForm.Close();
            Reset();
            loadMetricsMethod();
        }
        private void btn_staff_Click(object sender, EventArgs e)
        {
            openChildForm(new Core_APP.form_staff(), sender);
            lblPanelName.Text = "STAFF";
        }

        private void btn_category_Click(object sender, EventArgs e)
        {
            lblPanelName.Text = "CATEGORY";
            openChildForm(new Core_APP.form_category(), sender);
        }

        private void btn_stock_Click(object sender, EventArgs e)
        {
            openChildForm(new Core_APP.form_stock(), sender);
            lblPanelName.Text = "STOCK";
        }

        private void btn_report_Click(object sender, EventArgs e)
        {
            //openChildForm(new Core_APP.form_report(), sender);
            //lblPanelName.Text = "REPORT";
        }

        private void btn_transaction_Click(object sender, EventArgs e)
        {
            openChildForm(new Core_APP.form_transaction(), sender);
            lblPanelName.Text = "TRANSACTION";
        }
        private void btn_sales_Click(object sender, EventArgs e)
        {
            openChildForm(new Core_APP.form_sales(), sender);
            lblPanelName.Text = "SALES";
        }
        private void btn_report_Click_1(object sender, EventArgs e)
        {
            openChildForm(new Core_APP.form_report(), sender);
            lblPanelName.Text = "REPORT";
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnPatientBilling_Click(object sender, EventArgs e)
        {
            Core_APP.terminal terminal = new Core_APP.terminal();
            terminal.Show();

            this.Hide();
            
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
            
            
        }

       
    }
}
