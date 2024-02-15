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
using System.Text.RegularExpressions;

namespace GroceryShopInventoryManagementSystem
{
    public partial class SellingForm : Form
    {
        public SellingForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\ASUS\Documents\BIT\Semester 02\ITE 1942 - ICT Project\Activities,  Assignment\Project\Grocery Shop Inventory Management System\Database\Grocery Shop.mdf"";Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            con.Open();
            string query = "select ProdName,ProdPrice from ProductTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodDGV1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void populatebills()
        {
            con.Open();
            string query = "select * from BillTable";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            billsDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populatebills();
            fillcombo();
            sellerNameLbl.Text = LoginForm.Sellername;
            dateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }

        private void prodDGV1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            prodNameTB.Text = prodDGV1.SelectedRows[0].Cells[0].Value.ToString();
            prodPriceTB.Text = prodDGV1.SelectedRows[0].Cells[1].Value.ToString();
        }

        int Grdtotal = 0, n = 0;

        private void button5_Click(object sender, EventArgs e)
        {

            if (billIdTB.Text == "")
            {
                MessageBox.Show("Missing Bill Id");
            }
            else
            {
                try
                {
                    con.Open();
                    string query = "insert into BillTable values(" + billIdTB.Text + ",'" + sellerNameLbl.Text + "','" + dateLbl.Text + "','" + amtLbl.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order Added Successfully");
                    con.Close();
                    populatebills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void billsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Nihal Stores", new Font("Century Schoolbook", 25, FontStyle.Bold), Brushes.Black, new Point(230));
            e.Graphics.DrawString("Bill ID:" + billsDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century Schoolbook", 20, FontStyle.Bold), Brushes.Black, new Point(100, 70));
            e.Graphics.DrawString("Seller Name:" + billsDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century Schoolbook", 20, FontStyle.Bold), Brushes.Black, new Point(100, 100));
            e.Graphics.DrawString("Date:" + billsDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century Schoolbook", 20, FontStyle.Bold), Brushes.Black, new Point(100, 130));
            e.Graphics.DrawString("Total Amount:" + billsDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century Schoolbook", 20, FontStyle.Bold), Brushes.Black, new Point(100, 160));
            e.Graphics.DrawString("Sineth", new Font("Century Schoolbook", 20, FontStyle.Italic), Brushes.Black, new Point(270,230));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void searchCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con.Open();
            string query = "select ProdName,ProdQty from ProductTable where ProdCat='" + searchCB.SelectedValue.ToString();
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodDGV1.DataSource = ds.Tables[0];
            con.Close();
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
            //catCB.ValueMember = "CatName";
            //catCB.DataSource = dt;
            searchCB.ValueMember = "CatName";
            searchCB.DataSource = dt;
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm login = new LoginForm();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {         

            if (prodNameTB.Text == "" || prodQtyTB.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(prodPriceTB.Text) * Convert.ToInt32(prodQtyTB.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(orderDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = prodNameTB.Text;
                newRow.Cells[2].Value = prodPriceTB.Text;
                newRow.Cells[3].Value = prodQtyTB.Text;
                newRow.Cells[4].Value = Convert.ToInt32(prodPriceTB.Text) * Convert.ToInt32(prodQtyTB.Text);
                orderDGV.Rows.Add(newRow);
                n++;
                Grdtotal = Grdtotal + total;
                amtLbl.Text = ""+Grdtotal;
            }
        }
    }
}
