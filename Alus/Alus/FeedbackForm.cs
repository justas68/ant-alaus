using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alus.Core.Models;

namespace Alus
{
    public class FeedbackValidationEventArgs : EventArgs
    {
        public FeedbackValidationEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }

    public partial class FeedbackForm : ChildForm
    {
        private event EventHandler<FeedbackValidationEventArgs> ValidationError;

        private readonly IFeedbackSender _feedbackSender;
        private readonly IEmailValidator _emailValidator;

        public FeedbackForm(IFeedbackSender feedbackSender, IEmailValidator emailValidator)
        {
            InitializeComponent();

            _feedbackSender = feedbackSender;
            _emailValidator = emailValidator;

            sendButton.Click += async (s, e) => await send_button_Click(s, e);

            foreach (var feedbackType in EnumUtil.GetValues<FeedbackType>())
            {
                // type widening: from enum to string
                feedbackComboBox.Items.Add(feedbackType.ToString());
            }

            // preselected general
            feedbackComboBox.SelectedIndex = 0;

            ValidationError += (obj, e) => MessageBox.Show(e.ErrorMessage);
        }

        private void FeedbackForm_errorMessage(object sender, string e)
        {
            throw new NotImplementedException();
        }

        private void Raise(string errorMessage)
        {
            ValidationError?.Invoke(this, new FeedbackValidationEventArgs(errorMessage));
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

            if (!_emailValidator.Validate(feedback.EMail))
            {
                Raise("Invalid email address");
                return;
            }

            if (feedback.Text.Length < 10)
            {
                Raise("The feedback should contain at least 10 symbols");
                return;
            }

            try
            {
                await _feedbackSender.SendAsync(feedback);
                MessageBox.Show("Feedback sent. Thank you");
            }
            catch (FeedbackSenderException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch
            {
                Raise("The feedback should contain at least 10 symbols");
            }
        }

        private void suggestionExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
