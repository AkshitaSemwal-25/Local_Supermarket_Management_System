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
    public partial class Items : UserControl
    {
        public Items()
        {
            InitializeComponent();
        }

        private int selectedProductId = 0;

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                Product product = new Product();

                product.ProductName = txtProductName.Text;

                product.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);

                product.SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue);

                product.Barcode = txtBarcode.Text;

                product.Price = Convert.ToDecimal(txtPrice.Text);

                product.Quantity = Convert.ToInt32(txtQuantity.Text);

                product.RestockDate = dtpRestockDate.Value;

                context.Products.Add(product);

                context.SaveChanges();
            }

            LoadProducts();

            MessageBox.Show("Product Added Successfully");
        }

        private void Items_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
            LoadSuppliers();
        }

        private void LoadProducts()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var products = context.Products
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Category = p.Category.CategoryName,
                    Supplier = p.Supplier.SupplierName,
                    p.Barcode,
                    p.Price,
                    p.Quantity,
                    p.RestockDate
                })
                .ToList();

                dgvProducts.DataSource = products;
            }
        }
        private void LoadCategories()
        {
            using(var context = new LocalSupermarketsEntities())
            {
                cmbCategory.DataSource = context.Categories.ToList();

                cmbCategory.DisplayMember = "CategoryName";

                cmbCategory.ValueMember = "CategoryId";

            }
        }

        private void LoadSuppliers()
        {
            using (var context = new LocalSupermarketsEntities())
            {
                cmbSupplier.DataSource = context.Suppliers.ToList();

                cmbSupplier.DisplayMember = "SupplierName";

                cmbSupplier.ValueMember = "SupplierId";
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

                selectedProductId = Convert.ToInt32(row.Cells["ProductId"].Value);

                txtProductName.Text = row.Cells["ProductName"].Value.ToString();

                txtBarcode.Text = row.Cells["Barcode"].Value.ToString();

                txtPrice.Text = row.Cells["Price"].Value.ToString();

                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();

                cmbCategory.Text = row.Cells["Category"].Value.ToString();

                cmbSupplier.Text = row.Cells["Supplier"].Value.ToString();

                dtpRestockDate.Value = Convert.ToDateTime(row.Cells["RestockDate"].Value);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                Product product = context.Products.FirstOrDefault( p => p.ProductId == selectedProductId);

                if (product != null)
                {
                    product.ProductName = txtProductName.Text;

                    product.CategoryId = Convert.ToInt32(cmbCategory.SelectedValue);

                    product.SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue);

                    product.Barcode = txtBarcode.Text;

                    product.Price = Convert.ToDecimal(txtPrice.Text);

                    product.Quantity = Convert.ToInt32(txtQuantity.Text);

                    product.RestockDate = dtpRestockDate.Value;

                    context.SaveChanges();
                }
            }

            LoadProducts();

            MessageBox.Show("Product Updated Successfully");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product to delete.");
                return;
            }

            DialogResult result = MessageBox.Show(
            "Are you sure you want to delete this product?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var context = new LocalSupermarketsEntities())
                {
                    Product product = context.Products
                    .FirstOrDefault(p => p.ProductId == selectedProductId);

                    if (product != null)
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }
                }

                LoadProducts();

                ClearFields();

                selectedProductId = 0;

                MessageBox.Show("Product deleted successfully.");
            }
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtBarcode.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();

            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;

            dtpRestockDate.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
