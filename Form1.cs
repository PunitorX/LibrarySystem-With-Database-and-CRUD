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

namespace LibrarySystem
{
    public partial class Form1 : Form
    {
        //The connection string to connect the winForm application to the Database 'LibraryBooks' and then the table 'Books'.
        //Database is on Microsoft SQL Server
        string connectionString = @"Data Source=PUNITOR\SQLEXPRESS;Initial Catalog=LibraryBooks;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Adds strings to be selected on the 'comboGenre' combobox
            comboGenre.Text = "Please Select An Item";
            comboGenre.Items.Add("Comedy");
            comboGenre.Items.Add("Horror");
            comboGenre.Items.Add("Fiction");
            comboGenre.Items.Add("Fantasy");
            comboGenre.Items.Add("Mystery");
            comboGenre.Items.Add("Thriller");
            comboGenre.Items.Add("Historical");
            comboGenre.Items.Add("Romance");
            comboGenre.Items.Add("Western");
            comboGenre.Items.Add("Sci-Fi");
            comboGenre.Items.Add("Dystopian");
        }
        
        //Loads the data from the database.
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                //...
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Books", con);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }
        
        //Adds data
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Books values(@Book, @First_Name, @Last_Name, @Publisher, @Genre, @Release_Year)", con);
                cmd.Parameters.AddWithValue("@Book", txtBook.Text);
                cmd.Parameters.AddWithValue("@First_Name", txtFirst.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtLast.Text);
                cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                cmd.Parameters.AddWithValue("@Genre", comboGenre.SelectedItem);
                cmd.Parameters.AddWithValue("@Release_Year", double.Parse(txtRelease.Text));
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully Added");

                Clear();
            }
        }

        //Clears data from the textboxes
        public void Clear()
        {
            txtBook.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtPublisher.Text = "";
            txtRelease.Text = "";
            comboGenre.Text = "Please Select An Item";
        }

        //Deletes data from the database
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE Books WHERE Book=@Book", con);
                cmd.Parameters.AddWithValue("@Book", txtBook.Text);
                cmd.ExecuteNonQuery();
                con.Close();    
                MessageBox.Show("Successfully Deleted");
            }
        }

        //Updates data in the database
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Books set First_Name=@First_Name, Last_Name=@Last_Name, Publisher = @Publisher, Genre = @Genre, Release_Year = @Release_Year WHERE Book = @Book", con);
                cmd.Parameters.AddWithValue("@Book", txtBook.Text);
                cmd.Parameters.AddWithValue("@First_Name", txtFirst.Text);
                cmd.Parameters.AddWithValue("@Last_Name", txtLast.Text);
                cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                cmd.Parameters.AddWithValue("@Genre", comboGenre.SelectedItem);
                cmd.Parameters.AddWithValue("@Release_Year", double.Parse(txtRelease.Text));
                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Successfully Updated");

                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBook.Text = "";
            txtFirst.Text = "";
            txtLast.Text = "";
            txtPublisher.Text = "";
            txtRelease.Text = "";
            comboGenre.Text = "Please Select An Item";
        }
    }
}
