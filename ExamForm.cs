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
    public partial class ExamForm: Form
    {
        private int score = 0; // Variable to keep track of the score
        private int currentQuestionIndex = 0; // Index to keep track of the current question
        private SqlConnection connection; // SqlConnection to connect to the database
        private SqlDataReader reader; // SqlDataReader to read the questions from the database
        public ExamForm()
        {
            InitializeComponent(); // Initialize the form components
            connection = new SqlConnection("Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True");
            LoadQuestion(); // Load the first question when the form is initialized
        }
        private void LoadQuestion()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Questions", connection); // SQL command to select all questions
            reader = command.ExecuteReader(); // Execute the command and get a SqlDataReader to read the results

            if (reader.Read())
            {
                lblQuestion.Text = reader["QuestionText"].ToString(); // Load the first question
                radioOptionA.Text = reader["OptionA"].ToString();
                radioOptionB.Text = reader["OptionC"].ToString();
                radioOptionC.Text = reader["OptionD"].ToString(); // Load the options for the question
            }
            else
            {
                MessageBox.Show("No question found!");
                this.Close();
            }
        }

        private void radioOptionC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (reader["CorrectAnswer"].ToString()=="A" && radioOptionA.Checked || // Check if the selected answer matches the correct answer
                reader["CorrectAnswer"].ToString()=="B" && radioOptionB.Checked ||
                reader["CorrectAnswer"].ToString()=="C" && radioOptionC.Checked ||
                reader["CorrectAnswer"].ToString()=="D" && radioOptionD.Checked)

                {
                   score++;
                }
            if(reader.Read())
            {
                lblQuestion.Text = reader["QuestionText"].ToString(); // Load the next question
                radioOptionA.Text = reader["OptionA"].ToString();
                radioOptionB.Text = reader["OptionB"].ToString();
                radioOptionC.Text = reader["OptionC"].ToString();
                radioOptionD.Text = reader["OptionD"].ToString();
            }
            else
            {
                reader.Close(); // Close the reader when there are no more questions
                connection.Close(); // Close the connection
                MessageBox.Show("Exam finished! Your score is: " + score); // Show the score
                SaveResult(); // Save the result to the database
                this.Close(); // Close the exam form
            }

        }
        private void SaveResult()
        {
            string connectionString = "Data Source=SHERENI\\SQLEXPRESS;Initial Catalog=OnlineExamDB;Integrated Security=True"; // Connection string to the database
            using (SqlConnection connection = new SqlConnection(connectionString)) // Create a new connection object
            {
                connection.Open();
                string query = "INSERT INTO Results (StudentID, Score, ExamDate) VALUES (@StudentID, @Score, @ExamDate)";
                using (SqlCommand command = new SqlCommand(query, connection)) // Create a command object with the query and connection
                {
                    command.Parameters.AddWithValue("@StudentID", 1); // Replace with actual StudentID
                    command.Parameters.AddWithValue("@Score", score);
                    command.Parameters.AddWithValue("@ExamDate", DateTime.Now); // Current date and time
                    int rowsAffected = command.ExecuteNonQuery(); // Execute the query
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Result saved successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to save result.");
                    }
                }
            }
        }
    }
}
