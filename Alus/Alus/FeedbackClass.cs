using System.Text.RegularExpressions;

namespace Alus
{
    class FeedbackClass
    {
        private static Regex validEmailRegex = CreateValidEmailRegex();

        public FeedbackClass()
        {

        }

        private static Regex CreateValidEmailRegex()
        {
            // https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public bool EmailIsValid(string emailAddress)
        {
            return validEmailRegex.IsMatch(emailAddress); ;
        }
    }
}
