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

namespace GroceryShopInventoryManagementSystem
{
    public partial class CategoryForm : Form
    {
        public CategoryForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ASUS\Documents\BIT\Semester 02\ITE 1942 - ICT Project\Activities,  Assignment\Project\Grocery Shop Inventory Management System\Database\Grocery Shop.mdf"";Integrated Security=True;Connect Timeout=30");
        private void button5_Click(object sender, EventArgs e)
        {
            try 
            {
                con.Open();
                string query = "insert into CategoryTable values(" + catIdTB.Text + ",'" + catNameTB.Text + "','" + catDescrTB.Text + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Added Successfully");
                con.Close();
                populate();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void populate()
        {
            con.Open();
            string query = "Select * from CategoryTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            catDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void catDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            catIdTB.Text = catDGV.SelectedRows[0].Cells[0].Value.ToString();
            catNameTB.Text = catDGV.SelectedRows[0].Cells[1].Value.ToString();
            catDescrTB.Text = catDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (catIdTB.Text == "")
                {
                    MessageBox.Show("Select The Category to Delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from CategoryTable where CatId=" + catIdTB.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfull");
                    con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (catIdTB.Text == "" || catNameTB.Text == "" || catDescrTB.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                { 
                    con.Open();
                    string query = "update CategoryTable set CatName='" + catNameTB.Text + "',CatDescri='" + catDescrTB.Text + "' where CatId=" + catIdTB.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category SuccessfullY Updated");
                    con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductForm prod = new ProductForm();
            prod.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellerForm seller = new SellerForm();
            seller.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}
