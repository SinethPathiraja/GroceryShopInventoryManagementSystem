using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroceryShopInventoryManagementSystem
{
    public partial class SellerForm : Form
    {
        public SellerForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ASUS\Documents\BIT\Semester 02\ITE 1942 - ICT Project\Activities,  Assignment\Project\Grocery Shop Inventory Management System\Database\Grocery Shop.mdf"";Integrated Security=True;Connect Timeout=30");
        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SellerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            sellerIdTB.Text = sellerDGV.SelectedRows[0].Cells[0].Value.ToString();
            sellerNameTB.Text = sellerDGV.SelectedRows[0].Cells[1].Value.ToString();
            sellerAgeTB.Text = sellerDGV.SelectedRows[0].Cells[2].Value.ToString();
            sellerPhoneTB.Text = sellerDGV.SelectedRows[0].Cells[3].Value.ToString();
            sellerPassTB.Text = sellerDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void populate()
        {
            con.Open();
            string query = "Select * from SellerTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            sellerDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellerIdTB.Text == "")
                {
                    MessageBox.Show("Select The Seller to Delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from SellerTable where SellerId=" + sellerIdTB.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller Deleted Successfull");
                    con.Close();
                    populate();
                    sellerIdTB.Text = "";
                    sellerNameTB.Text = "";
                    sellerAgeTB.Text = "";
                    sellerPhoneTB.Text = "";
                    sellerPassTB.Text = "";                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "insert into SellerTable values(" + sellerIdTB.Text + ",'" + sellerNameTB.Text + "','" + sellerAgeTB.Text + "','" + sellerPhoneTB.Text + "','" + sellerPassTB.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller Added Successfully");
                con.Close();
                populate();
                sellerIdTB.Text = "";
                sellerNameTB.Text = "";
                sellerAgeTB.Text = "";
                sellerPhoneTB.Text = "";
                sellerPassTB.Text = "";               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellerIdTB.Text == "" || sellerNameTB.Text == "" || sellerAgeTB.Text == "" || sellerPhoneTB.Text == "" || sellerPassTB.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    con.Open();
                    string query = "update SellerTable set SellerName='" + sellerNameTB.Text + "',SellerAge='" + sellerAgeTB.Text + "',SellerPhoneNo='" + sellerPhoneTB.Text + "',SellerPassword='" + sellerPassTB + "' where SellerId=" + sellerIdTB.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller SuccessfullY Updated");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}
