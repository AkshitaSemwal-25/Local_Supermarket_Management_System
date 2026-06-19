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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
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
                    using (SqlConnection conn = new SqlConnection(connString))
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
                                MessageBox.Show("Account already exists", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        string insertQueries = "INSERT INTO USERS (Name,Password) VALUES (@Name,@Password)";
                        using (SqlCommand cmd = new SqlCommand(insertQueries, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Password", password);
                            int rowsAffected = cmd.ExecuteNonQuery();


                            // Check if the insertion was succesful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Account Created successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //close the current form cmd.Parameters.AddWithValue("@Name", name);and open the login form
                                this.Close();
                                Login login = new Login();
                                login.Show();

                            }
                        }
                    }  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
        }
    }
}
