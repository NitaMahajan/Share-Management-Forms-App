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
    public partial class BuyStock : Form
    {
        SqlConnection sqlcon;
        private static BuyStock buystock;
        public BuyStock()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
        }
        public static BuyStock getInstance() {
            if (buystock == null)
            {
                buystock = new BuyStock();
            }
            return buystock;
        }

        private void BuyStock_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = Home.getInstance(Home.getUser());
            home.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Script Number cant be blank!");
            }
            else if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the price!");
            }
            else if (textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the number of shares!");
            }
            else
            {
                sqlcon.Open();


                //

                string check_query = "select count(*) from [dbo].[inventory] where scriptID='" + textBox1.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(check_query, sqlcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    //Present
                    
                    string retrieve_details = "select * from [dbo].[inventory] where scriptID='" + textBox1.Text.Trim() + "'";
                    SqlDataAdapter sda2 = new SqlDataAdapter(retrieve_details, sqlcon);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    double price = (double)dt2.Rows[0][1];
                    int numberofshares = (int)dt2.Rows[0][2];
                    double totalCost = price * numberofshares;

                    double newtotalcost = totalCost + ((Convert.ToDouble(textBox2.Text.Trim())) * (Convert.ToInt32(textBox3.Text.Trim())));
                    int newShares = numberofshares + (Convert.ToInt32(textBox3.Text.Trim()));
                    double newprice = (double)(newtotalcost / newShares);

                    SqlCommand ucmd = sqlcon.CreateCommand();
                    ucmd.CommandType = CommandType.Text;
                    ucmd.CommandText = "update [dbo].[inventory] set price='"+newprice+"', noShares='"+ newShares + "', totalCost='"+newtotalcost+"' where ScriptID='"+ textBox1.Text.Trim()+"'";
                    ucmd.ExecuteNonQuery();
                    MessageBox.Show("Bought and updated script!");
                    

                }
                else
                {
                    //Not present
                    SqlCommand sqlcmd = new SqlCommand("buyscript", sqlcon);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@scriptID", textBox1.Text.Trim());
                    sqlcmd.Parameters.AddWithValue("@price", Convert.ToDouble(textBox2.Text.Trim()));
                    sqlcmd.Parameters.AddWithValue("@noShares", Convert.ToInt32(textBox3.Text.Trim()));
                    sqlcmd.Parameters.AddWithValue("@totalCost", ((Convert.ToDouble(textBox2.Text.Trim())) * (Convert.ToInt32(textBox3.Text.Trim()))));

                    sqlcmd.ExecuteNonQuery();
                    MessageBox.Show("Bought script!");
                    
                    
                }

                ViewInventory.getInstance().disp_inventory();
     
                textBox1.Text = textBox2.Text = textBox3.Text = ""; //clear
                this.Hide();
                Home home = Home.getInstance(Home.getUser());
                home.Show();
                sqlcon.Close();
                //



                //first check if the script is present, if yes then update accordingly, else just insert







            }
        }
    }
}
