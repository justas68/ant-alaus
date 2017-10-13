namespace Alus
{
    partial class Feedback
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.email_textBox = new System.Windows.Forms.TextBox();
            this.feedback_textBox = new System.Windows.Forms.TextBox();
            this.feedback_comboBox = new System.Windows.Forms.ComboBox();
            this.send_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your email adress";
            // 
            // email_textBox
            // 
            this.email_textBox.Location = new System.Drawing.Point(12, 44);
            this.email_textBox.Name = "email_textBox";
            this.email_textBox.Size = new System.Drawing.Size(203, 22);
            this.email_textBox.TabIndex = 1;
            
            // 
            // feedback_textBox
            // 
            this.feedback_textBox.Location = new System.Drawing.Point(12, 95);
            this.feedback_textBox.Multiline = true;
            this.feedback_textBox.Name = "feedback_textBox";
            this.feedback_textBox.Size = new System.Drawing.Size(564, 206);
            this.feedback_textBox.TabIndex = 2;
            
            // 
            // feedback_comboBox
            // 
            this.feedback_comboBox.FormattingEnabled = true;
            this.feedback_comboBox.Items.AddRange(new object[] {
            "General feedback",
            "Suggestions",
            "Bugs",
            "Complaints",
            "Other"});
            this.feedback_comboBox.Location = new System.Drawing.Point(388, 42);
            this.feedback_comboBox.Name = "feedback_comboBox";
            this.feedback_comboBox.Size = new System.Drawing.Size(188, 24);
            this.feedback_comboBox.TabIndex = 3;
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(442, 346);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(133, 35);
            this.send_button.TabIndex = 4;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.send_button_Click);
            // 
            // Feedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 393);
            this.Controls.Add(this.send_button);
            this.Controls.Add(this.feedback_comboBox);
            this.Controls.Add(this.feedback_textBox);
            this.Controls.Add(this.email_textBox);
            this.Controls.Add(this.label1);
            this.Name = "Feedback";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox email_textBox;
        private System.Windows.Forms.TextBox feedback_textBox;
        private System.Windows.Forms.ComboBox feedback_comboBox;
        private System.Windows.Forms.Button send_button;
    }
}