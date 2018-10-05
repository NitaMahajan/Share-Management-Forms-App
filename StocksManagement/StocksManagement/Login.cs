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
    public partial class Login : Form
    {
        SqlConnection sqlcon;
        public Login()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Username cant be empty!");
            }
            else if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Password cant be empty!");
            }
            else
            {
                //SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
                sqlcon.Open();
                string login_query = "select count(*) from [dbo].[user] where username='" + textBox1.Text.Trim() + "' and password='" + textBox2.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(login_query,sqlcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    //Home home = new Home(textBox1.Text.Trim());
                    Home home = Home.getInstance(textBox1.Text.Trim());
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Invalid credentials!");
                    textBox1.Text = textBox2.Text = ""; //clear
                }
                sqlcon.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Username cant be empty!");
            }
            else if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Password cant be empty!");
            }
            else
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("adduser",sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@username",textBox1.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@password",textBox2.Text.Trim());
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("User registration successful");
                textBox1.Text = textBox2.Text = ""; //clear
                this.Hide();
                //Home home = new Home(textBox1.Text.Trim());
                Home home = Home.getInstance(textBox1.Text.Trim());
                home.Show();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
