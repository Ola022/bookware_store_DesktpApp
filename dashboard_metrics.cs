using System.Data.OleDb;
using System.Data;


namespace cosmesticClinic
{
    public class dashboard_metrics
    {
        private string conStr = connectionString.constr;
        public int GetTotalProducts()
        {
            int total = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = "SELECT COUNT(*) FROM tbl_Products";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                total = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return total;
        }

        public int GetLowStockCount()
        {
            int count = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = "SELECT COUNT(*) FROM tbl_Products WHERE quantity < 10";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                count = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return count;
        }

        public decimal GetTodaySales()
        {
            decimal total = 0;
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string today = DateTime.Now.ToString("yyyy-MM-dd");
                string query = $"SELECT SUM(total_amount) FROM tbl_Sales WHERE sale_date = #{today}#";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                var result = cmd.ExecuteScalar();
                total = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                con.Close();
            }
            return total;
        }

        public DataTable GetTopSellingProducts()
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = @"
            SELECT TOP 5 product_name, SUM(quantity_sold) AS total_sold 
            FROM tbl_Sales_Details 
            GROUP BY product_name 
            ORDER BY total_sold DESC";
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
                    string query = "SELECT product_name, quantity FROM tbl_products WHERE quantity <= 5";
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

        public DataTable GetRecentTransactions()
        {
            DataTable dt = new DataTable();
            using (OleDbConnection con = new OleDbConnection(conStr))
            {
                string query = @"
            SELECT TOP 5 product_name, quantity_sold, sale_time 
            FROM tbl_Sales 
            ORDER BY sale_time DESC";
                OleDbDataAdapter da = new OleDbDataAdapter(query, con);
                da.Fill(dt);
            }
            return dt;
        }



    }
}
