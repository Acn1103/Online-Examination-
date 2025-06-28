using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OES;
using System.Diagnostics;

namespace OES
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (ValidateStudent(email, password)) // Check if the student exists in the database
            {
                MessageBox.Show("Login successful!"); // Show a success message
                ExamForm examForm = new ExamForm(); // Create an instance of the ExamForm
                examForm.Show(); // Show the ExamForm
                this.Hide(); // Hide the LoginForm
            }
            else
            {
                MessageBox.Show("Invalid email or password.");
            }
        }

        private bool ValidateStudent(string email, string password)
        {
            string connectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True"; // Connection string to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT StudentID FROM Students WHERE Email = @Email AND Password = @Password"; // SQL query to check if the student exists with the provided email and password
                using (SqlCommand command = new SqlCommand(query, connection)) // Create a new SqlCommand object with the query and the connection
                {
                    command.Parameters.AddWithValue("@Email", email); // Add parameters to the command to prevent SQL injection
                    command.Parameters.AddWithValue("@Password", password); // Add parameters to the command to prevent SQL injection
                    object result = command.ExecuteScalar(); // Execute the command and get the result
                    return result != null; // If the result is not null, it means the student exists
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm(); // Create an instance of the RegistrationForm
            registrationForm.Show(); // Show the RegistrationForm
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            AddQuestionsForm addQuestionForm = new AddQuestionsForm(); // Create an instance of the AddQuestionsForm
            addQuestionForm.Show(); // Show the AddQuestionsForm
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseManagementForm databaseManagementForm = new DatabaseManagementForm(); // Create an instance of the DatabaseManagementForm
            databaseManagementForm.Show(); // Show the DatabaseManagementForm
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}


           