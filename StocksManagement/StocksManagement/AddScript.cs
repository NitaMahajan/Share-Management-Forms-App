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
    public partial class AddScript : Form
    {
        private static AddScript addscript;
        SqlConnection sqlcon;
        public AddScript()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        public static AddScript getInstance()
        {
            if(addscript == null)
            {
                addscript = new AddScript();
            }
            return addscript;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = Home.getInstance(Home.getUser());
            home.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Name cant be blank!");
            }
            else
            {
                sqlcon.Open();
                SqlCommand sqlcmd = new SqlCommand("addscript",sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Parameters.AddWithValue("@scriptName",textBox1.Text.Trim());
                sqlcmd.Parameters.AddWithValue("@scriptComment", textBox2.Text.Trim());
                sqlcmd.ExecuteNonQuery();
                MessageBox.Show("Added script!");
                textBox1.Text = textBox2.Text = ""; //clear
                this.Hide();
                Home home = Home.getInstance(Home.getUser());
                home.Show();
                home.disp_scripts();
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
