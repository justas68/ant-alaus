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

        /// <summary>
        /// Taken from http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx
        /// </summary>
        /// <returns></returns>
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" //https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
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
