using System.Data.OleDb;

namespace cosmesticClinic
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
        }
        private string conStr = connectionString.constr;
        public static int user_id;
        public static string user_name;        
        public static string user_Role; 
        public static string fullname;

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection(conStr);
            try
            {
                if (txtUser.Text.Equals(string.Empty) || txtPass.Text.Equals(string.Empty))
                {

                    MessageBox.Show("Enter your login Details, USERNAME & PASSWORD", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    con.Open();
                    OleDbDataReader read;

                    string details = "SELECT * from tbl_User_login WHERE username='" + txtUser.Text + "' and password='" + txtPass.Text + "'";
                    OleDbCommand cmd = new OleDbCommand(details, con);

                    read = cmd.ExecuteReader();

                    if (read.Read() == true)
                    {
                        user_id = Convert.ToInt32(read["id"]);
                        user_name = Convert.ToString(read["username"]);                        
                        fullname = Convert.ToString(read["fullname"]);
                        user_Role = Convert.ToString(read["userrole"]);

                        con.Close();
                        this.Hide();
                        if (user_Role == "Admin")
                        {
                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                        }
                        else
                        {
                            Core_APP.terminal terminal = new Core_APP.terminal();
                            terminal.ShowDialog();
                        }
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("User not Found ", "invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPass.Text = "admin";
            txtUser.Text = "admin";
            //txtUser.Text = "user"; 
            //txtPass.Text = "user";
        }
    }
}
