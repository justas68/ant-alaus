using System;
using System.Windows.Forms;

namespace Alus
{
    public partial class FeedbackForm : Form
    {
        EmailValidator feedback = new EmailValidator();

        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (feedback.Validate(emailTextBox.Text))
            {
                MessageBox.Show("Feedback sent. Thank you");
            }
            else
            {
                MessageBox.Show("Invalid email adress");
            }
        }

        private void suggestionExitButton_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Hide();
        }
    }
}
