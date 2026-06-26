using System;
using System.Linq;
using System.Windows.Forms;

namespace Local_Supermarket_Management_System
{
    public partial class Stock : UserControl
    {
        public Stock()
        {
            InitializeComponent();
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            LoadProductNames();
            LoadStock();
        }

        private void LoadStock()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var stock = context.Products
                .OrderBy(p => p.Quantity)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Category = p.Category.CategoryName,
                    p.Quantity,
                    Status = p.Quantity < 5 ? "Low Stock" : "In Stock",
                    p.RestockDate
                })
                .ToList();

                dgvRestock.DataSource = stock;
            }
        }

        private void LoadProductNames()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                cmbProduct.DataSource = context.Products.ToList();

                cmbProduct.DisplayMember = "ProductName";

                cmbProduct.ValueMember = "ProductId";
            }

            cmbProduct.SelectedIndex = -1;

            txtCurrentStock.Clear();

            nudQuantity.Value = 0;
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

                    if (product.RestockDate.HasValue)
                        dtpRestockDate.Value = product.RestockDate.Value;
                    else
                        dtpRestockDate.Value = DateTime.Now;
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

                Product product = context.Products
                .FirstOrDefault(p => p.ProductId == productId);

                if (product != null)
                {
                    product.Quantity += Convert.ToInt32(nudQuantity.Value);

                    product.RestockDate = dtpRestockDate.Value;

                    context.SaveChanges();

                    MessageBox.Show("Stock updated successfully.");
                }
            }

            LoadStock();

            cmbProduct_SelectedIndexChanged(null, null);

            nudQuantity.Value = 0;
        }

        private void dgvRestock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}