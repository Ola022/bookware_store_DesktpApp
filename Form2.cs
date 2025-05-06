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

        private void Form2_Load(object sender, EventArgs e)
        {
            Reset();
        }
        private Form activateForm;
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
