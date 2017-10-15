using System;
using System.Windows.Forms;

namespace Alus
{
    public partial class FeedbackForm : Form
    {
        FeedbackClass feedback = new FeedbackClass();

        public FeedbackForm()
        {
            InitializeComponent();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (feedback.EmailIsValid(emailTextBox.Text) == true)
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
