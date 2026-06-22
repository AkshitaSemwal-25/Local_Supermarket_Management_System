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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string password = txtPassword.Text;

            string connString = "Server=localhost;Database=LocalSupermarkets;Trusted_Connection=True;TrustServerCertificate=True;";

            // Validations 
            if (name == string.Empty && password == string.Empty)
            {
                MessageBox.Show("Please fill in all fields.", "Information Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();

                        //check if the name already exists in the database
                        string checkName = "SELECT COUNT(*) FROM USERS WHERE Name = @Name";
                        using (SqlCommand cmd = new SqlCommand(checkName, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            int count = (int)cmd.ExecuteScalar();
                            if (count > 0)
                            {
                                MessageBox.Show("Login successful", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();

                                Mainform home = new Mainform();
                                home.Show();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),"Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Signup signup = new Signup();
            signup.Show();
            this.Hide();
        }
    }
}
