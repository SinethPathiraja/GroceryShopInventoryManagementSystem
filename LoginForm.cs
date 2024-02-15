using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroceryShopInventoryManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        public static string Sellername = "";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ASUS\Documents\BIT\Semester 02\ITE 1942 - ICT Project\Activities,  Assignment\Project\Grocery Shop Inventory Management System\Database\Grocery Shop.mdf"";Integrated Security=True;Connect Timeout=30");
        private void Label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(uNameTB.Text == "" || passTB.Text == "")
            {
                MessageBox.Show("Enter the User Name and Password");
            }
            else
            {
                if (roleCB.SelectedIndex > -1)
                {
                    if (roleCB.SelectedItem.ToString() == "Admin")
                    {
                        if (uNameTB.Text == "Admin" && passTB.Text == "Admin")
                        {
                            ProductForm prod = new ProductForm();
                            prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are the Admin, Enter the correct ID and Password");
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Your in the Seller section");
                        con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from SellerTable where SellerName= '"+uNameTB.Text+"' and SellerPassword='"+passTB.Text+"'", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            Sellername = uNameTB.Text;
                            SellingForm sell = new SellingForm();
                            sell.Show();
                            this.Hide();
                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong User Name or Password");
                        }
                        con.Close();
                    }
                }              
                else
                {
                    MessageBox.Show("Select a Role");
                }

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            uNameTB.Text = "";
            passTB.Text = "";
        }

        private void roleCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
