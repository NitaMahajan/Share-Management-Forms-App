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
    public partial class SellStock : Form
    {
        private static SellStock sellstock;
        SqlConnection sqlcon;
        private SellStock()
        {
            InitializeComponent();
            sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DELL\Documents\StockDB.mdf;Integrated Security=True;Connect Timeout=30;");
        }

        public static SellStock getInstance() {
            if(sellstock == null)
            {
                sellstock = new SellStock();
            }
            return sellstock;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //first we need to retrieve details of the share from the inventory
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Script ID cant be blank!");
            }
            else if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Price cant be blank!");
            }
            else if (textBox3.Text.Equals(""))
            {
                MessageBox.Show("Number of shares cant be blank!");
            }
            else
            {
                sqlcon.Open();
                //First we need to retrieve details and see if we can actually sell
                string check_query = "select * from [dbo].[inventory] where scriptID='" + textBox1.Text.Trim() + "'";
                SqlDataAdapter sda = new SqlDataAdapter(check_query, sqlcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == textBox1.Text.Trim())
                {
                    //record present, now check for number of shares
                    if ((int)dt.Rows[0][2] >= Convert.ToInt32(textBox3.Text.Trim()))
                    {
                        //transaction ok
                        int newShares = (int)dt.Rows[0][2] - Convert.ToInt32(textBox3.Text.Trim());
                        double newTotalCost = newShares * (double)dt.Rows[0][1];

                        //Update inventory
                        SqlCommand ucmd = sqlcon.CreateCommand();
                        ucmd.CommandType = CommandType.Text;
                        ucmd.CommandText = "update [dbo].[inventory] set noShares='" + newShares + "', totalCost='" + newTotalCost + "' where ScriptID='" + textBox1.Text.Trim() + "'";
                        ucmd.ExecuteNonQuery();
                        MessageBox.Show("Sold and updated script!");


                        double profitPerShare = Convert.ToDouble(textBox2.Text) - (double)dt.Rows[0][1];
                        double totalProfit = (profitPerShare * (int)dt.Rows[0][2]);
                        //update proft table



                        //

                        SqlCommand sqlcmd = new SqlCommand("addprofit", sqlcon);
                        sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlcmd.Parameters.AddWithValue("@ScriptID", Convert.ToInt32(textBox1.Text.Trim()));
                        sqlcmd.Parameters.AddWithValue("@price", profitPerShare);
                        sqlcmd.Parameters.AddWithValue("@noShares", Convert.ToDouble(textBox3.Text.Trim()));
                        sqlcmd.Parameters.AddWithValue("@totalCost", totalProfit);

                        sqlcmd.ExecuteNonQuery();
                        MessageBox.Show("Profit updated successfully!!");
                        textBox1.Text = textBox2.Text = ""; //clear
                        textBox3.Text = "";
                        this.Hide();
                        //Home home = new Home(textBox1.Text.Trim());
                        Home home = Home.getInstance(Home.getUser());
                        home.Show();

                        //
                        ViewInventory.getInstance().disp_inventory();
                        ViewProfit.getInstance().disp_profit();
                    }
                    else
                    {
                        MessageBox.Show("You dont have shares of the script!");
                        textBox1.Text = textBox2.Text = textBox3.Text = ""; //clear
                    }
                    sqlcon.Close();

                }
                else
                {
                    MessageBox.Show("You dont have shares of the script!");
                    textBox1.Text = textBox2.Text = textBox3.Text = ""; //clear
                }
                

            }
        }
    }
}
