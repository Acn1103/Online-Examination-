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
        public DatabaseManagementForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadQuestions();
            LoadResults();
        }

        private void LoadStudents()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT StudentID, FullName, Email FROM Students";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewStudents.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuestions()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT QuestionID, QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer FROM Questions";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewQuestions.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadResults()
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT Results.ResultID, Students.FullName, Results.Score, Results.ExamDate " +
                                   "FROM Results INNER JOIN Students ON Results.StudentID = Students.StudentID";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridViewResults.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading results data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelectedQuestion()
        {
            if (dataGridViewQuestions.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewQuestions.SelectedRows[0];
                if (selectedRow.Cells["QuestionID"].Value != null &&
                    int.TryParse(selectedRow.Cells["QuestionID"].Value.ToString(), out int questionID))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this question?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection connection = DatabaseHelper.GetConnection())
                            {
                                connection.Open();
                                string query = "DELETE FROM Questions WHERE QuestionID = @QuestionID";
                                using (SqlCommand command = new SqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@QuestionID", questionID);
                                    int rowsAffected = command.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Question deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        LoadQuestions();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Failed to delete question.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting question: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid question selection.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please select a question row to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            DeleteSelectedQuestion();
        }

        private void btnDeleteQuestion_Click_1(object sender, EventArgs e)
        {
            DeleteSelectedQuestion();
        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
