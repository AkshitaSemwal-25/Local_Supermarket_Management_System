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
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void btnLowStock_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var report = context.Products
                .Where(p => p.Quantity < 5)
                .Select(p => new
                {
                    p.ProductName,
                    Category = p.Category.CategoryName,
                    p.Quantity,
                    p.RestockDate
                })
                .ToList();

                dgvReports.DataSource = report;
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var report = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    Category = p.Category.CategoryName,
                    Supplier = p.Supplier.SupplierName,
                    p.Price,
                    p.Quantity
                })
                .ToList();

                dgvReports.DataSource = report;
            }
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var report = context.Sales
                .Select(s => new
                {
                    Product = s.Product.ProductName,
                    s.QuantitySold,
                    s.SaleDate
                })
                .ToList();

                dgvReports.DataSource = report;
            }
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                dgvReports.DataSource = context.Suppliers
                .Select(s => new
                {
                    s.SupplierName,
                    s.Phone,
                    s.Email,
                    s.Address
                })
                .ToList();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvReports.DataSource = null;
        }
    }
}
