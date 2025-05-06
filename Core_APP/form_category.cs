using System.Data.OleDb;
using System.Data;

namespace cosmesticClinic.Core_APP
{
    public partial class form_category : Form
    {
        public form_category()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        int UserID;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_category.Text == "" || txt_description.Text == "")
                {

                    MessageBox.Show("Please fill the field", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }                
                else
                {
                    OleDbConnection con = new OleDbConnection(conStr);
                    con.Open();
                    OleDbDataReader dr;
                    OleDbCommand cmd = new OleDbCommand();

                    cmd.CommandText = @"insert into tbl_category ([category_name], [description]) VALUES('" + txt_category.Text + "','" + txt_description.Text + "')";
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
     

        private void form_category_Load(object sender, EventArgs e)
        {
            getAllCategories();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    string updateQuery = @"UPDATE tbl_category SET [category_name] = ?, [description] = ? WHERE [ID] = ?"; // Or use ID if available

                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("?", txt_category.Text);
                        cmd.Parameters.AddWithValue("?", txt_description.Text);
                        cmd.Parameters.AddWithValue("?", UserID); // WHERE condition value

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("Category updated successfully!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No record found to update. Please select the category below", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string deleteQuery = "DELETE FROM tbl_category WHERE [ID] = ?"; // Or use [id] = ? if available

                    using (OleDbCommand cmd = new OleDbCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("?", UserID); // OR txt_id.Text

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("Category deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(txt_category.Text + " not category found ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                getCategoryDetails();
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
            txt_description.Text = "";
            txt_category.Text = "";
            getAllCategories();
            UserID = 0;
        }
        public void getAllCategories()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                string details = "SELECT (id) as [ID],(category_name) as [Category Name],(description) as [Description] from tbl_category  order by category_name asc ";

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

        void getCategoryDetails()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                UserID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                string query = "SELECT * FROM tbl_category WHERE id = ?";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("?", UserID);

                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txt_category.Text = Convert.ToString(reader["category_name"]);                                        
                    txt_description.Text = reader["description"].ToString();
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
    }
}
