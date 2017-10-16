namespace Alus
{
    partial class FeedbackForm
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
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.feedbackTextBox = new System.Windows.Forms.TextBox();
            this.feedbackComboBox = new System.Windows.Forms.ComboBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.suggestionExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(8, 9);
            this.emailLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(198, 25);
            this.emailLabel.TabIndex = 0;
            this.emailLabel.Text = "Your email address";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(13, 39);
            this.emailTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(300, 31);
            this.emailTextBox.TabIndex = 1;
            // 
            // feedbackTextBox
            // 
            this.feedbackTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.feedbackTextBox.Location = new System.Drawing.Point(13, 80);
            this.feedbackTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.feedbackTextBox.Multiline = true;
            this.feedbackTextBox.Name = "feedbackTextBox";
            this.feedbackTextBox.Size = new System.Drawing.Size(856, 483);
            this.feedbackTextBox.TabIndex = 2;
            // 
            // feedbackComboBox
            // 
            this.feedbackComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.feedbackComboBox.FormattingEnabled = true;
            this.feedbackComboBox.Items.AddRange(new object[] {
            "General feedback",
            "Suggestions",
            "Bugs",
            "Complaints",
            "Other"});
            this.feedbackComboBox.Location = new System.Drawing.Point(569, 37);
            this.feedbackComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.feedbackComboBox.Name = "feedbackComboBox";
            this.feedbackComboBox.Size = new System.Drawing.Size(300, 33);
            this.feedbackComboBox.TabIndex = 3;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(669, 573);
            this.sendButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(200, 44);
            this.sendButton.TabIndex = 4;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.send_button_Click);
            // 
            // suggestionExitButton
            // 
            this.suggestionExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.suggestionExitButton.Location = new System.Drawing.Point(13, 573);
            this.suggestionExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.suggestionExitButton.Name = "suggestionExitButton";
            this.suggestionExitButton.Size = new System.Drawing.Size(200, 44);
            this.suggestionExitButton.TabIndex = 5;
            this.suggestionExitButton.Text = "Exit";
            this.suggestionExitButton.UseVisualStyleBackColor = true;
            this.suggestionExitButton.Click += new System.EventHandler(this.suggestionExitButton_Click);
            // 
            // FeedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 631);
            this.Controls.Add(this.suggestionExitButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.feedbackComboBox);
            this.Controls.Add(this.feedbackTextBox);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.emailLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FeedbackForm";
            this.Text = "Feedback";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox feedbackTextBox;
        private System.Windows.Forms.ComboBox feedbackComboBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Button suggestionExitButton;
    }
}