using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{
    public partial class Feedback : Form
    {
        static Regex ValidEmailRegex = CreateValidEmailRegex();

        public Feedback()
        {
            InitializeComponent();
        }

        private void send_button_Click(object sender, EventArgs e)
        {
            if (EmailIsValid(email_textBox.Text) == true)
            {
                MessageBox.Show("All good");
            }
        }

        private static Regex CreateValidEmailRegex()
        {
            // https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" 
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            return ValidEmailRegex.IsMatch(emailAddress);;
        }
    }
}
