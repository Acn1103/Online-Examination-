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
    public partial class AddQuestionsForm: Form
    {
        public AddQuestionsForm()
        {
            InitializeComponent(); // Initialize the form components
        }

        private void Add_Questions_Form_Load(object sender, EventArgs e)
        {

        }

        private void txtOptionC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtOptionD_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {


            string questionText = txtQuestion.Text; // Get the question text from the textbox
            string optionA = txtOptionA.Text;
            string optionB = txtOptionB.Text;
            string optionC = txtOptionC.Text;
            string optionD = txtOptionD.Text; // Get the options from the textboxes
            string correctAnswer = cmbCorrectAnswer.SelectedItem.ToString(); // Get the selected correct answer from the combobox

            if (AddQuestion(questionText, optionA, optionB, optionC, optionD, correctAnswer)) // Call the method to add the question to the database
            {
                MessageBox.Show("Question added successfully!"); // Show a success message
                ClearForm(); // Clear the form fields after adding the question
            }
            else
            {
                MessageBox.Show("Failed to add question."); // Show a failure message if the question could not be added
            }

        }
        private bool AddQuestion(string questionText, string optionA, string optionB, string optionC, string optionD, string correctAnswer) // Method to add a question to the database
        {
            string connectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True"; // Connection string to the database
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new SqlConnection object
            {
                connection.Open(); // Open the connection to the database
                string query = "INSERT INTO Questions (QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) " +
                               "VALUES (@QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)"; // SQL query to insert a new question into the Questions table
                using (SqlCommand command = new SqlCommand(query, connection)) // Create a new SqlCommand object with the query and the connection
                {
                    command.Parameters.AddWithValue("@QuestionText", questionText); // Add parameters to the command to prevent SQL injection
                    command.Parameters.AddWithValue("@OptionA", optionA);
                    command.Parameters.AddWithValue("@OptionB", optionB);
                    command.Parameters.AddWithValue("@OptionC", optionC);
                    command.Parameters.AddWithValue("@OptionD", optionD); // Add the options as parameters
                    command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer); // Add the correct answer as a parameter
                    int rowsAffected = command.ExecuteNonQuery(); // Execute the command and get the number of rows affected
                    return rowsAffected > 0; // Return true if at least one row was affected, indicating that the question was added successfully
                }
            }
        }
        private void ClearForm()
        {
            txtQuestion.Clear(); // Clear the question textbox
            txtOptionA.Clear();
            txtOptionB.Clear();
            txtOptionC.Clear();
            txtOptionD.Clear(); // Clear the option textboxes
            cmbCorrectAnswer.SelectedIndex = -1; // Clear the selected item in the combobox
        }
    }
}
