using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alus.Core.Models;

namespace Alus
{
    public partial class FeedbackForm : Form
    {
        EmailValidator validator = new EmailValidator();

        public FeedbackForm()
        {
            InitializeComponent();

            sendButton.Click += async (s, e) => await send_button_Click(s, e);

            foreach (var feedbackType in EnumUtil.GetValues<FeedbackType>())
            {
                // type widening: from enum to string
                feedbackComboBox.Items.Add(feedbackType.ToString());
            }

            // preselected general
            feedbackComboBox.SelectedIndex = 0;
        }

        private async Task send_button_Click(object sender, EventArgs e)
        {
            var feedback = new Feedback()
            {
                EMail = emailTextBox.Text,
                Text = feedbackTextBox.Text,
                // type narrowing, from string to enum
                Type = (FeedbackType)Enum.Parse(typeof(FeedbackType), feedbackComboBox.Text)
            };

            if (!validator.Validate(feedback.EMail))
            {
                MessageBox.Show("Invalid email address");
                return;
            }

            if (feedback.Text.Length < 10)
            {
                MessageBox.Show("The feedback should contain at least 10 symbols");
                return;
            }


            await FeedbackFileSender.Instance.SendAsync(feedback);

            MessageBox.Show("Feedback sent. Thank you");
        }

        private void suggestionExitButton_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Hide();
        }
    }
}
