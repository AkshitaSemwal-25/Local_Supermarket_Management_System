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
    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var context = new LocalSupermarketsEntities())
            {
                var query = context.Products
                                        .Include("Category")
                                        .Include("Supplier")
                                        .AsQueryable();

                string searchText = txtSearch.Text.Trim();

                switch (cmbSearchBy.Text)
                {
                    case "Product Name":
                        query = query.Where(p => p.ProductName.Contains(searchText));
                        break;

                    case "Category":
                        query = query.Where(p => p.Category.CategoryName.Contains(searchText));
                        break;

                    case "Supplier":
                        query = query.Where(p => p.Supplier.SupplierName.Contains(searchText));
                        break;

                    case "Barcode":
                        query = query.Where(p => p.Barcode.Contains(searchText));
                        break;
                }

                dgvSearch.DataSource = query
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
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbSearchBy.SelectedIndex = -1;
        }
    }
}
