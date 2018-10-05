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
    public partial class Home : Form
    {
        static string user;
        static Home home;
        SqlConnection sqlcon;
        public Home()
        {
            InitializeComponent();
        }
        public Home(string username)
        {
            InitializeComponent();
            user = username;
            label1.Text = "Welcome "+username+" !";
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30");
        }
        public static Home getInstance(string username)
        {
            if (home == null)
            {
                home = new Home(username);
            }
            return home;
        }
        public static string getUser()
        {
            return user;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            disp_scripts();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MessageBox.Show("You have been signed out!");
            Login login = new Login();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddScript addscript = AddScript.getInstance();
            addscript.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void disp_scripts()
        {
            sqlcon.Open();
            SqlCommand sqlcmd = sqlcon.CreateCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = "select * from [dbo].[script]";
            sqlcmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            sqlcon.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewInventory viewInventory = ViewInventory.getInstance();
            viewInventory.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyStock buystock = BuyStock.getInstance();
            buystock.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellStock sellstock = SellStock.getInstance();
            sellstock.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ViewProfit viewprofit = ViewProfit.getInstance();
            viewprofit.Show();
        }
    }
}
