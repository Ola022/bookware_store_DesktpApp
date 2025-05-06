using System.Data.OleDb;
using System.Data;

namespace cosmesticClinic.Core_APP
{
    public partial class form_staff : Form
    {
        public form_staff()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        private void form_staff_Load(object sender, EventArgs e)
        {
            getAllStaff();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txt_fullname_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_confirm.Text == "" || txt_email.Text == "" || txt_fullname.Text == "" || txt_password.Text == ""
                     || txt_phone.Text == "" || cmb_role.Text == "" || txt_username.Text == "")
                {
                 
                        MessageBox.Show("Please fill the field", "", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                }
                else if(txt_confirm.Text != txt_password.Text)
                    {
                        MessageBox.Show("Password not match", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                else
                {
                    OleDbConnection con = new OleDbConnection(conStr);
                    con.Open();
                    OleDbDataReader dr;
                    OleDbCommand cmd = new OleDbCommand();
                    //var date = dtpDate.Value.ToString("yyyy-MM-dd");
                    //var time = dtpTime.Value.ToString("hh :mm");

                    cmd.CommandText = @"insert into tbl_User_login ([username], [password], [userrole], [fullname], [email], [phone_number]) VALUES('"
                        + txt_username.Text + "','" + txt_password.Text + "','" + cmb_role.Text + "','" + txt_fullname.Text + "','" + txt_email.Text + "','" + txt_phone.Text + "')";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Operation Sucessfull!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    resetInput();          
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation Not Sucessfull! " + ex.Message, "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void resetInput()
        {
            foreach (var c in panel2.Controls)
            {
                if (c is ComboBox)
                {
                    ((ComboBox)c).Text = String.Empty;
                }
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
            txt_password.Text = "";
            txt_confirm.Text = "";
            UserID = 0;
            getAllStaff();
        }
        public void getAllStaff()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {

                string details = "SELECT (id) as [ID],(username) as [Username],(fullname) as [Fullname], (phone_number) as [Phone Number], (userrole) as [User Role] from tbl_User_login ";

                OleDbCommand cmd = new OleDbCommand(details, con);
                //OleDbCommand cmd = new OleDbCommand("select * from tbl_users", con);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        int UserID;
        void getStaffDetails()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                UserID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                
                string query = "SELECT * FROM tbl_User_login WHERE id = ?";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("?", UserID);
                
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_fullname.Text = Convert.ToString(reader["fullname"]);
                    txt_password.Text = "****";
                    txt_confirm.Text = "****";                    
                    txt_email.Text = Convert.ToString(reader["email"]);                                        
                    txt_phone.Text = reader["phone_number"].ToString();
                    cmb_role.Text = reader["userrole"].ToString();
                    txt_username.Text = reader["username"].ToString();
                }
                else
                {
                    MessageBox.Show("Invalid Input, Select from Drop Down", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                con.Close();
            }

            catch (System.Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
            con.Close();

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                getStaffDetails();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    string updateQuery = @"UPDATE tbl_User_login 
                        SET [userrole] = ?, 
                            [fullname] = ?, 
                            [email] = ?, 
                            [phone_number] = ? 
                        WHERE [ID] = ?"; // Or use ID if available

                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, con))
                    {                        
                        cmd.Parameters.AddWithValue("?", cmb_role.Text);
                        cmd.Parameters.AddWithValue("?", txt_fullname.Text);
                        cmd.Parameters.AddWithValue("?", txt_email.Text);
                        cmd.Parameters.AddWithValue("?", txt_phone.Text);
                        cmd.Parameters.AddWithValue("?", UserID); // WHERE condition value

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("User information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found to update./n /b Please select the user info below", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    string deleteQuery = "DELETE FROM tbl_User_login WHERE [ID] = ?"; // Or use [id] = ? if available

                    using (OleDbCommand cmd = new OleDbCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("?", UserID); // OR txt_id.Text

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("User deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No user found with that "+txt_fullname.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
