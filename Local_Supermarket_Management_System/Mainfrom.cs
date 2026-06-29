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
    public partial class Mainfrom : Form
    {
        public Mainfrom()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadControl(UserControl uc)
        {
            panelContainer.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(uc);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Home home = new Home();
            LoadControl(home);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Items item = new Items();
            LoadControl(item);
        }

        private void Mainfrom_Load(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Home home = new Home();
            LoadControl(home);
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
             Suppliers supplier = new Suppliers();
            LoadControl(supplier);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Search search = new Search();
            LoadControl(search);
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Stock stock = new Stock();
            LoadControl(stock);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Sales sale = new Sales();
            LoadControl(sale);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            panelContainer.Controls.Clear();
            Reports report = new Reports();
            LoadControl(report);

        }
    }
}
