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
            InitializeComponent();
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
            string questionText = txtQuestion.Text.Trim();
            string optionA = txtOptionA.Text.Trim();
            string optionB = txtOptionB.Text.Trim();
            string optionC = txtOptionC.Text.Trim();
            string optionD = txtOptionD.Text.Trim();
            string correctAnswer = cmbCorrectAnswer.SelectedItem != null ? cmbCorrectAnswer.SelectedItem.ToString() : "";

            if (string.IsNullOrWhiteSpace(questionText) ||
                string.IsNullOrWhiteSpace(optionA) ||
                string.IsNullOrWhiteSpace(optionB) ||
                string.IsNullOrWhiteSpace(optionC) ||
                string.IsNullOrWhiteSpace(optionD) ||
                string.IsNullOrWhiteSpace(correctAnswer))
            {
                MessageBox.Show("Please fill in all question fields and select the correct answer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AddQuestion(questionText, optionA, optionB, optionC, optionD, correctAnswer))
            {
                MessageBox.Show("Question added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
            else
            {
                MessageBox.Show("Failed to add question.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AddQuestion(string questionText, string optionA, string optionB, string optionC, string optionD, string correctAnswer)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Questions (QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) " +
                                   "VALUES (@QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@QuestionText", questionText);
                        command.Parameters.AddWithValue("@OptionA", optionA);
                        command.Parameters.AddWithValue("@OptionB", optionB);
                        command.Parameters.AddWithValue("@OptionC", optionC);
                        command.Parameters.AddWithValue("@OptionD", optionD);
                        command.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding question: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearForm()
        {
            txtQuestion.Clear();
            txtOptionA.Clear();
            txtOptionB.Clear();
            txtOptionC.Clear();
            txtOptionD.Clear();
            cmbCorrectAnswer.SelectedIndex = -1;
        }
    }
}
