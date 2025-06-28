namespace OES
{
    partial class ExamForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) // Check if disposing is true and components is not null
            {
                components.Dispose(); // Dispose of the components to free up resources
            }
            base.Dispose(disposing); // Call the base Dispose method to ensure proper cleanup
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblQuestion = new System.Windows.Forms.Label();
            this.radioOptionA = new System.Windows.Forms.RadioButton();
            this.radioOptionB = new System.Windows.Forms.RadioButton();
            this.radioOptionC = new System.Windows.Forms.RadioButton();
            this.radioOptionD = new System.Windows.Forms.RadioButton();
            this.btnNext = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestion.Location = new System.Drawing.Point(32, 39);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(118, 29);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Question";
            // 
            // radioOptionA
            // 
            this.radioOptionA.AutoSize = true;
            this.radioOptionA.Location = new System.Drawing.Point(122, 168);
            this.radioOptionA.Name = "radioOptionA";
            this.radioOptionA.Size = new System.Drawing.Size(28, 20);
            this.radioOptionA.TabIndex = 1;
            this.radioOptionA.TabStop = true;
            this.radioOptionA.Text = "\r\n";
            this.radioOptionA.UseVisualStyleBackColor = true;
            // 
            // radioOptionB
            // 
            this.radioOptionB.AutoSize = true;
            this.radioOptionB.Location = new System.Drawing.Point(122, 232);
            this.radioOptionB.Name = "radioOptionB";
            this.radioOptionB.Size = new System.Drawing.Size(28, 20);
            this.radioOptionB.TabIndex = 2;
            this.radioOptionB.TabStop = true;
            this.radioOptionB.Text = "\r\n";
            this.radioOptionB.UseVisualStyleBackColor = true;
            // 
            // radioOptionC
            // 
            this.radioOptionC.AutoSize = true;
            this.radioOptionC.Location = new System.Drawing.Point(122, 285);
            this.radioOptionC.Name = "radioOptionC";
            this.radioOptionC.Size = new System.Drawing.Size(28, 20);
            this.radioOptionC.TabIndex = 3;
            this.radioOptionC.TabStop = true;
            this.radioOptionC.Text = "\r\n";
            this.radioOptionC.UseVisualStyleBackColor = true;
            this.radioOptionC.CheckedChanged += new System.EventHandler(this.radioOptionC_CheckedChanged);
            // 
            // radioOptionD
            // 
            this.radioOptionD.AutoSize = true;
            this.radioOptionD.Location = new System.Drawing.Point(122, 354);
            this.radioOptionD.Name = "radioOptionD";
            this.radioOptionD.Size = new System.Drawing.Size(28, 20);
            this.radioOptionD.TabIndex = 4;
            this.radioOptionD.TabStop = true;
            this.radioOptionD.Text = "\r\n";
            this.radioOptionD.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(651, 451);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(170, 56);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next\r\n";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ExamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1144, 519);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.radioOptionD);
            this.Controls.Add(this.radioOptionC);
            this.Controls.Add(this.radioOptionB);
            this.Controls.Add(this.radioOptionA);
            this.Controls.Add(this.lblQuestion);
            this.Name = "ExamForm";
            this.Text = "ExamForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion; // Label to display the question text
        private System.Windows.Forms.RadioButton radioOptionA;
        private System.Windows.Forms.RadioButton radioOptionB;
        private System.Windows.Forms.RadioButton radioOptionC;
        private System.Windows.Forms.RadioButton radioOptionD; // Radio buttons for the answer options
        private System.Windows.Forms.Button btnNext; // Button to go to the next question
    }
}