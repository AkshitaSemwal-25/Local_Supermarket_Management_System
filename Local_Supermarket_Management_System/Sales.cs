using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Local_Supermarket_Management_System
{
    public partial class Sales : UserControl
    {
        public Sales()
        {
            InitializeComponent();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadSales();
        }

        private void LoadProducts()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                cmbProduct.DataSource = context.Products.ToList();

                cmbProduct.DisplayMember = "ProductName";

                cmbProduct.ValueMember = "ProductId";
            }

            cmbProduct.SelectedIndex = -1;

            txtCurrentStock.Clear();

            nudQuantitySold.Value = 0;
        }

        private void LoadSales()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var sales = context.Sales
                .Select(s => new
                {
                    s.SaleId,
                    Product = s.Product.ProductName,
                    s.QuantitySold,
                    s.SaleDate
                })
                .ToList();

                dgvSales.DataSource = sales;
            }
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null)
                return;

            int productId;

            if (!int.TryParse(cmbProduct.SelectedValue.ToString(), out productId))
                return;

            using (var context = new LocalSupermarketsEntities())
            {
                Product product = context.Products
                .FirstOrDefault(p => p.ProductId == productId);

                if (product != null)
                {
                    txtCurrentStock.Text = product.Quantity.ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            using (var context = new LocalSupermarketsEntities())
            {
                int productId = Convert.ToInt32(cmbProduct.SelectedValue);

                Product product = context.Products.FirstOrDefault(p => p.ProductId == productId);

                if (product == null)
                    return;

                int quantitySold = Convert.ToInt32(nudQuantitySold.Value);

                if (quantitySold <= 0)
                {
                    MessageBox.Show("Enter a valid quantity.");
                    return;
                }

                if (quantitySold > product.Quantity)
                {
                    MessageBox.Show("Not enough stock available.");
                    return;
                }

                product.Quantity -= quantitySold;

                Sale sale = new Sale();

                sale.ProductId = productId;
                sale.QuantitySold = quantitySold;
                sale.SaleDate = dtpSaleDate.Value;

                context.Sales.Add(sale);

                context.SaveChanges();

                MessageBox.Show("Sale completed successfully.");
            }

            LoadSales();

            LoadProducts();

            cmbProduct_SelectedIndexChanged(null, null);

            nudQuantitySold.Value = 0;
        }
    }
}
