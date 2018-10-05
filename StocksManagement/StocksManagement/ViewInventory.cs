using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StocksManagement
{
    public partial class ViewInventory : Form
    {
        SqlConnection sqlcon;
        private static ViewInventory inventory;
        public ViewInventory()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
            
        }
        public static ViewInventory getInstance() {
            if (inventory == null)
            {
                inventory = new ViewInventory();
            }
            
            return inventory;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = Home.getInstance(Home.getUser());
            home.Show();
        }

        public void disp_inventory()
        {
            sqlcon.Open();
            SqlCommand sqlcmd = sqlcon.CreateCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select * from [dbo].[inventory]";
            sqlcmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            sqlcon.Close();

        }

        private void ViewInventory_Load(object sender, EventArgs e)
        {
            this.disp_inventory();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
