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
    public partial class DatabaseManagementForm : Form
    {
        private string connectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True"; // Connection string to the database
        public DatabaseManagementForm()
        {
            InitializeComponent(); // Initialize the form components
            LoadStudents(); // Load the students into the DataGridView
            LoadQuestions(); // Load the questions into the DataGridView
            LoadResults(); // Load the results into the DataGridView
        }

        private void LoadStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
            {
                connection.Open();
                string query = "SELECT StudentID, FullName, Email, Password  FROM Students"; // SQL query to select all students
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection); // Create a SqlDataAdapter to execute the query and fill the DataTable
                DataTable dataTable = new DataTable(); // Create a DataTable to hold the results
                adapter.Fill(dataTable); // Fill the DataTable with the results of the query
                dataGridViewStudents.DataSource = dataTable; // Set the DataGridView's data source to the DataTable
            }
        }

        private void LoadQuestions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
            {
                connection.Open();
                string query = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions"; // SQL query to select all questions
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection); // Create a SqlDataAdapter to execute the query and fill the DataTable
                DataTable dataTable = new DataTable(); // Create a DataTable to hold the results
                adapter.Fill(dataTable); // Fill the DataTable with the results of the query
                dataGridViewQuestions.DataSource = dataTable; // Set the DataGridView's data source to the DataTable
            }
        }

        private void LoadResults()
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
            {
                connection.Open(); // Open the connection to the database
                string query = "SELECT Results.ResultID, Students.FullName, Results.Score, Results.ExamDate " +
                               "FROM Results INNER JOIN Students ON Results.StudentID = Students.StudentID"; // SQL query to select results with student names
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection); // Create a SqlDataAdapter to execute the query and fill the DataTable
                DataTable dataTable = new DataTable(); // Create a DataTable to hold the results
                adapter.Fill(dataTable); // Fill the DataTable with the results of the query
                dataGridViewResults.DataSource = dataTable; // Set the DataGridView's data source to the DataTable
            }
        }


        private void Students_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Results_Click(object sender, EventArgs e)
        {

        }

        private void DatabaseManagementForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewQuestions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0) // Check if any row is selected in the DataGridView
            {
                int questionID = Convert.ToInt32(dataGridViewQuestions.SelectedRows[5].Cells["QuestionID"].Value); // Get the QuestionID of the selected row

                using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
                {
                    connection.Open();
                    string query = "DELETE FROM Questions WHERE QuestionID = @QuestionID"; // SQL query to delete the question with the specified QuestionID
                    using (SqlCommand command = new SqlCommand(query, connection)) // Create a SqlCommand to execute the query
                    {
                        command.Parameters.AddWithValue("@QuestionID", questionID); // Add the QuestionID as a parameter to the command to prevent SQL injection
                        int rowsAffected = command.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                        if (rowsAffected > 0) // Check if any rows were affected (i.e., the question was deleted successfully)
                        {
                            MessageBox.Show("Question deleted successfully!");
                            LoadQuestions(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete question.");
                        }
                    }
                }
            }

            else
            {
                MessageBox.Show("Please select a question to delete.");
            }
        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeleteQuestion_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0) // Check if any row is selected in the DataGridView
            {
                int questionID = Convert.ToInt32(dataGridViewQuestions.SelectedRows[0].Cells["QuestionID"].Value); // Get the QuestionID of the selected row

                using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
                {
                    connection.Open(); // Open the connection to the database
                    string query = "DELETE FROM Questions WHERE QuestionID = @QuestionID"; // SQL query to delete the question with the specified QuestionID
                    using (SqlCommand command = new SqlCommand(query, connection)) // Create a SqlCommand to execute the query
                    {
                        command.Parameters.AddWithValue("@QuestionID", questionID); // Add the QuestionID as a parameter to the command to prevent SQL injection
                        int rowsAffected = command.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Question deleted successfully!");
                            LoadQuestions(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete question.");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a question to delete.");
            }
        }
    }
}
