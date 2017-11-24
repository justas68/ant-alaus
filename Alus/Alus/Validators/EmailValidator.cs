using System.Text.RegularExpressions;

namespace Alus
{
    public class EmailValidator : IEmailValidator
    {
        private static Regex validEmailRegex = CreateValidEmailRegex();

        private static Regex CreateValidEmailRegex()
        {
            // https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public bool Validate(string emailAddress)
        {
            return validEmailRegex.IsMatch(emailAddress); ;
        }
    }
}
