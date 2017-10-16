using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Alus.Tests
{
    [TestClass]
    public class EmailValidatorTest
    {
        [TestMethod]
        public void CheckValidEmails()
        {
            var validator = new EmailValidator();
            Assert.IsTrue(validator.Validate("mif@mif.vu.lt"));
            Assert.IsTrue(validator.Validate("a@mif.vu.lt"));
        }

        [TestMethod]
        public void CheckInvalidEmails()
        {
            var validator = new EmailValidator();
            Assert.IsFalse(validator.Validate("name surname@mif.vu.lt"));
            Assert.IsFalse(validator.Validate("prefix@suffix"));
        }
    }
}
