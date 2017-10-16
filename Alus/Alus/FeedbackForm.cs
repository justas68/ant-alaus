using System;
using System.Windows.Forms;

namespace Alus
{
    public partial class FeedbackForm : Form
    {
        EmailValidator validator = new EmailValidator();

        public FeedbackForm()
        {
            InitializeComponent();

            foreach (var feedbackType in EnumUtil.GetValues<FeedbackType>())
            {
                feedbackComboBox.Items.Add(feedbackType);
            }

            // preselected general
            feedbackComboBox.SelectedIndex = 0;
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            var email = emailTextBox.Text;
            if (validator.Validate(email))
            {
                var feedback = new Feedback()
                {
                    EMail = emailTextBox.Text,
                    Text = feedbackTextBox.Text,
                    Type = (FeedbackType)Enum.Parse(typeof(FeedbackType), feedbackComboBox.Text)
                };

                FeedbackFileSender.Instance.Send(feedback);

                MessageBox.Show("Feedback sent. Thank you");
            }
            else
            {
                MessageBox.Show("Invalid email address");
            }
        }

        private void suggestionExitButton_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Hide();
        }
    }
}
