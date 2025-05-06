using System.Data.OleDb;
using System.Data;

namespace cosmesticClinic.Core_APP
{
    public partial class form_stock : Form
    {
        public form_stock()
        {
            InitializeComponent();
        }
        private string conStr = connectionString.constr;
        int UserID;

        private void form_stock_Load(object sender, EventArgs e)
        {
            getAllCategory();
            getAllStocks();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                getStockDetails();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_title.Text == "" || txt_description.Text == "" || txt_author.Text == "" || cmb_category.Text == ""
                     || numQty.Text == "" || num_costPrice.Text == "" || num_sellPrice.Text == "")
                {

                    MessageBox.Show("Please fill the field", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbConnection con = new OleDbConnection(conStr);
                    con.Open();
                    OleDbDataReader dr;
                    OleDbCommand cmd = new OleDbCommand();
                    //string currentTime = DateTime.Now.ToString("HH:mm:ss");
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    

                    cmd.CommandText = @"insert into tbl_stock ([book_title], [book_description], [category_id], [category_name], 
                                            [costprice], [sellingprice], [authors], [quantity], [last_update], [staff_update_id]) VALUES('"
                                            + txt_title.Text + "','" + txt_description.Text + "','" + cmb_category.SelectedValue + "'" +
                                            ",'" + cmb_category.Text + "','" + num_costPrice.Value + "','" + num_sellPrice.Value + "','" + txt_author.Text + "'" +
                                            ",'" + numQty.Value + "','" + currentDate + "','" + 2 + "')";
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
                    string updateQuery = @"UPDATE tbl_stock SET [book_title] = ?, [book_description] = ?, [category_id] = ?,
                                            [category_name] = ?, [costprice] = ?, [sellingprice] = ?, [authors] = ?, 
                                            [quantity] = ?, [last_update] = ?, [staff_update_id] = ?
                                            WHERE [ID] = ?"; // Or use ID if available

                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, con))
                    {                        
                        cmd.Parameters.AddWithValue("?", txt_title.Text);
                        cmd.Parameters.AddWithValue("?", txt_description.Text);
                        cmd.Parameters.AddWithValue("?", cmb_category.SelectedValue);
                        cmd.Parameters.AddWithValue("?", cmb_category.Text);
                        cmd.Parameters.AddWithValue("?", num_costPrice.Value);
                        cmd.Parameters.AddWithValue("?", num_sellPrice.Value);
                        cmd.Parameters.AddWithValue("?", txt_author.Text);
                        cmd.Parameters.AddWithValue("?", numQty.Value);
                        cmd.Parameters.AddWithValue("?", currentDate);
                        cmd.Parameters.AddWithValue("?", 2);
                        
                        cmd.Parameters.AddWithValue("?", UserID); // WHERE condition value

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("Updated successfully!", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    string deleteQuery = "DELETE FROM tbl_stock WHERE [ID] = ?"; // Or use [id] = ? if available

                    using (OleDbCommand cmd = new OleDbCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("?", UserID); // OR txt_id.Text

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            resetInput();
                            MessageBox.Show("Deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(txt_title.Text + " not category found ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (c is NumericUpDown)
                {
                    ((NumericUpDown)c).Text = String.Empty;
                }
            }
            txt_description.Text = "";
            getAllCategory();
            getAllStocks();
            UserID = 0;
        }
        void getAllStocks()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                string details = "SELECT (id) as [ID],(book_title) as [Book Title Name],(book_description) as [Description] ,(category_name) as [Category] " +
                    ",(costprice) as [Cost Price] ,(sellingprice) as [Selling Price] ,(authors) as [Authors] ,(quantity) as [Quantity] ," +
                    "(last_update) as [last update] from tbl_stock order by book_title asc  ";

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
        void getAllCategory()
        {

            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                OleDbDataAdapter da;
                DataTable dt;
                da = new OleDbDataAdapter("Select * From tbl_category order by category_name asc  ", con);
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

        void getStockDetails()
        {
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            try
            {
                UserID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                string query = "SELECT * FROM tbl_stock WHERE id = ?";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("?", UserID);

                OleDbDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    txt_title.Text = Convert.ToString(reader["book_title"]);
                    txt_description.Text = reader["book_description"].ToString();
                    cmb_category.SelectedValue = Convert.ToInt16(reader["category_id"]);
                    num_costPrice.Text = reader["costprice"].ToString();
                    num_sellPrice.Text = reader["sellingprice"].ToString();
                    txt_author.Text = reader["authors"].ToString();
                    numQty.Text = reader["quantity"].ToString();
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
