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

namespace Local_Supermarket_Management_System
{
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void flowHome_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadProducts()
        {
            flowHome.Controls.Clear();

            string connString = "Server=localhost;Database=LocalSupermarkets;Trusted_Connection=True;TrustServerCertificate=True;";

            using(SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                string query = "SELECT Id, Title, Category, Barcode, Price, Quantity, Status FROM PRODUCTS";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductCard card = new ProductCard();
                            card.ProductId = Convert.ToInt32(reader["Id"]);
                            card.Product = reader["Title"].ToString();
                            card.Category = reader["Category"].ToString();
                            card.Barcode = reader["Barcode"].ToString();
                            card.Price = reader["Price"].ToString();
                            card.Stock = reader["Stock"].ToString();
                            card.Status = reader["Status"].ToString();

                            flowHome.Controls.Add(card);
                        }
                    }
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
