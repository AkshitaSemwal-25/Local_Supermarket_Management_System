using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Local_Supermarket_Management_System
{
    public partial class Suppliers : UserControl
    {
        public Suppliers()
        {
            InitializeComponent();
        }

        private void Suppliers_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
        }

        public void LoadSuppliers()
        {
            using(var context = new LocalSupermarketsEntities())
            {
                dgvSuppliers.DataSource = context.Suppliers.ToList();
            }
        }

        private int selectedSupplierId = 0;

        private void dgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];

                selectedSupplierId = Convert.ToInt32(row.Cells["SupplierId"].Value);

                txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();

                txtPhone.Text = row.Cells["Phone"].Value.ToString();

                txtEmail.Text =  row.Cells["Email"].Value.ToString();

                txtAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using(var context = new LocalSupermarketsEntities())
{
                Supplier supplier = new Supplier();

                supplier.SupplierName = txtSupplierName.Text;

                supplier.Phone = txtPhone.Text;

                supplier.Email = txtEmail.Text;

                supplier.Address = txtAddress.Text;

                context.Suppliers.Add(supplier);

                context.SaveChanges();
            }

            LoadSuppliers();

            ClearFields();

            MessageBox.Show("Supplier added successfully.");
        }

        private void ClearFields()
        {
            txtSupplierName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId == 0)
            {
                MessageBox.Show("Please select a supplier first.");
                return;
            }

            using (var context = new LocalSupermarketsEntities())
            {
                Supplier supplier = context.Suppliers
                .FirstOrDefault(s => s.SupplierId == selectedSupplierId);

                if (supplier != null)
                {
                    supplier.SupplierName = txtSupplierName.Text;
                    supplier.Phone = txtPhone.Text;
                    supplier.Email = txtEmail.Text;
                    supplier.Address = txtAddress.Text;

                    context.SaveChanges();
                }
            }

            LoadSuppliers();

            ClearFields();

            selectedSupplierId = 0;

            MessageBox.Show("Supplier updated successfully.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedSupplierId == 0)
            {
                MessageBox.Show("Please select a supplier first.");
                return;
            }

            DialogResult result = MessageBox.Show(
            "Are you sure you want to delete this supplier?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (var context = new LocalSupermarketsEntities())
                {
                    Supplier supplier = context.Suppliers
                    .FirstOrDefault(s => s.SupplierId == selectedSupplierId);

                    if (supplier != null)
                    {
                        context.Suppliers.Remove(supplier);
                        context.SaveChanges();
                    }
                }

                LoadSuppliers();

                ClearFields();

                selectedSupplierId = 0;

                MessageBox.Show("Supplier deleted successfully.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
