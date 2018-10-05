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
    public partial class ViewProfit : Form
    {
        private static ViewProfit viewprofit;
        SqlConnection sqlcon;
        private ViewProfit()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        public static ViewProfit getInstance()
        {
            if (viewprofit == null)
            {
                viewprofit = new ViewProfit();
            }
            return viewprofit;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = Home.getInstance(Home.getUser());
            home.Show();
        }

        private void ViewProfit_Load(object sender, EventArgs e)
        {
            this.disp_profit();
        }

        public void disp_profit()
        {
            sqlcon.Open();
            SqlCommand sqlcmd = sqlcon.CreateCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select * from [dbo].[profit]";
            sqlcmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            sqlcon.Close();

        }
    }
}
