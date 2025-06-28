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

namespace OES
{
    public partial class RegistrationForm: Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text; // Get the full name from the textbox
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text; // Get the password and confirm password from the textboxes

            if (password != confirmPassword) // Check if the password and confirm password match
            {
                MessageBox.Show("Passwords do not match!"); // Show an error message if they do not match
                return;
            }

            if (RegisterStudent(fullName, email, password)) // Call the method to register the student
            {
                MessageBox.Show("Registration successful!"); // Show a success message if the registration was successful
                this.Close(); // Close the registration form
            }
            else
            {
                MessageBox.Show("Registration failed. Email may already exist."); // Show an error message if the registration failed
            }
        }
        private bool RegisterStudent(string fullName, string email, string password)
        {
            string connectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True"; // Connection string to the database
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
            {
                connection.Open();
                string query = "INSERT INTO Students (FullName, Email, Password) VALUES (@FullName, @Email, @Password)"; // SQL query to insert a new student into the Students table
                using (SqlCommand command = new SqlCommand(query, connection)) // Create a new SqlCommand object with the query and the connection
                {
                    command.Parameters.AddWithValue("@FullName", fullName); // Add parameters to the command to prevent SQL injection
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password); // Add the password parameter

                    int rowsAffected = command.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                    return rowsAffected > 0; //     Return true if at least one row was affected, indicating that the student was registered successfully
                }
            }
        }

    }
}
