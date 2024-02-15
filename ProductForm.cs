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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ASUS\Documents\BIT\Semester 02\ITE 1942 - ICT Project\Activities,  Assignment\Project\Grocery Shop Inventory Management System\Database\Grocery Shop.mdf"";Integrated Security=True;Connect Timeout=30");
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    private void fillcombo()
        {
            //This method will bind the ComboBox with Database
            con.Open();
            SqlCommand cmd = new SqlCommand("select CatName from CategoryTable", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CatName", typeof(string));
            dt.Load(rdr);
            catCB.ValueMember = "CatName";
            catCB.DataSource = dt;
            searchCB.ValueMember = "CatName";
            searchCB.DataSource = dt;
            con.Close();
        }

    private void populate()
        {
            con.Open();
            string query = "select * from ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            fillcombo();
            populate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm cat = new CategoryForm();
            cat.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "insert into ProductTable values(" + prodIdTB.Text + ",'" + prodNameTB.Text + "','" + prodQtyTB.Text + "','" + prodPriceTB.Text + "','" + catCB.SelectedValue.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Added Successfully");
                con.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prodDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodIdTB.Text = prodDGV.SelectedRows[0].Cells[0].Value.ToString();
            prodNameTB.Text = prodDGV.SelectedRows[0].Cells[1].Value.ToString();
            prodQtyTB.Text = prodDGV.SelectedRows[0].Cells[2].Value.ToString();
            prodPriceTB.Text = prodDGV.SelectedRows[0].Cells[3].Value.ToString();
            catCB.SelectedValue = prodDGV.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodIdTB.Text == "" || prodNameTB.Text == "" || prodQtyTB.Text == "" || prodPriceTB.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    con.Open();
                    string query = "update ProductTable set ProdName='" + prodNameTB.Text + "',ProdQty='" + prodQtyTB.Text + "',ProdPrice='" + prodPriceTB.Text + "',ProdCat='" + catCB.SelectedValue.ToString() + "' where ProdId=" + prodIdTB.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product SuccessfullY Updated");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (prodIdTB.Text == "")
                {
                    MessageBox.Show("Select The Product to Delete");
                }
                else
                {
                    con.Open();
                    string query = "delete from ProductTable where ProdId=" + prodIdTB.Text + "";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted Successfull");
                    con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
            string query = "select * from ProductTable where ProdCat='" + searchCB.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            SellerForm seller = new SellerForm();
            seller.Show();
        }
    }
}
