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
        public class QuestionItem
        {
            public int QuestionID { get; set; }
            public string QuestionText { get; set; }
            public string OptionA { get; set; }
            public string OptionB { get; set; }
            public string OptionC { get; set; }
            public string OptionD { get; set; }
            public string CorrectAnswer { get; set; }
        }

        private int score = 0;
        private int currentQuestionIndex = 0;
        private int currentStudentId = 1;
        private List<QuestionItem> questions = new List<QuestionItem>();

        public ExamForm() : this(1)
        {
        }

        public ExamForm(int studentId)
        {
            InitializeComponent();
            currentStudentId = studentId;
            LoadAllQuestions();
            DisplayCurrentQuestion();
        }

        private void LoadAllQuestions()
        {
            questions.Clear();
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            questions.Add(new QuestionItem
                            {
                                QuestionID = Convert.ToInt32(reader["QuestionID"]),
                                QuestionText = reader["QuestionText"]?.ToString() ?? "",
                                OptionA = reader["OptionA"]?.ToString() ?? "",
                                OptionB = reader["OptionB"]?.ToString() ?? "",
                                OptionC = reader["OptionC"]?.ToString() ?? "",
                                OptionD = reader["OptionD"]?.ToString() ?? "",
                                CorrectAnswer = reader["CorrectAnswer"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayCurrentQuestion()
        {
            if (questions.Count == 0)
            {
                MessageBox.Show("No questions found!", "Exam Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            if (currentQuestionIndex < questions.Count)
            {
                var q = questions[currentQuestionIndex];
                lblQuestion.Text = string.Format("Q{0}: {1}", currentQuestionIndex + 1, q.QuestionText);
                radioOptionA.Text = q.OptionA;
                radioOptionB.Text = q.OptionB;
                radioOptionC.Text = q.OptionC;
                radioOptionD.Text = q.OptionD;

                radioOptionA.Checked = false;
                radioOptionB.Checked = false;
                radioOptionC.Checked = false;
                radioOptionD.Checked = false;
            }
        }

        private void radioOptionC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count)
            {
                var q = questions[currentQuestionIndex];
                string selectedAnswer = "";
                if (radioOptionA.Checked) selectedAnswer = "A";
                else if (radioOptionB.Checked) selectedAnswer = "B";
                else if (radioOptionC.Checked) selectedAnswer = "C";
                else if (radioOptionD.Checked) selectedAnswer = "D";

                if (string.Equals(q.CorrectAnswer.Trim(), selectedAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    score++;
                }

                currentQuestionIndex++;

                if (currentQuestionIndex < questions.Count)
                {
                    DisplayCurrentQuestion();
                }
                else
                {
                    MessageBox.Show("Exam finished! Your score is: " + score + " / " + questions.Count, "Exam Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SaveResult();
                    this.Close();
                }
            }
        }

        private void SaveResult()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Results (StudentID, Score, ExamDate) VALUES (@StudentID, @Score, @ExamDate)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", currentStudentId);
                        command.Parameters.AddWithValue("@Score", score);
                        command.Parameters.AddWithValue("@ExamDate", DateTime.Now);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Result saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save result.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving exam result: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
