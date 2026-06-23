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

        private void LoadProducts()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var products = context.Products.ToList();

                foreach (var product in products)
                {
                    ProductCard card = new ProductCard();

                    card.lblProduct.Text = product.ProductName;

                    card.lblCategory.Text = product.Category.CategoryName;

                    card.lblBarcode.Text = product.Barcode;

                    card.lblPrice.Text = "£" + product.Price.ToString();

                    card.lblQuantity.Text = product.Quantity.ToString();

                    card.lblStatus.Text =
                        product.Quantity < 5
                        ? "Low Stock"
                        : "In Stock";

                    flowHome.Controls.Add(card);
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}
