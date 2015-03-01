using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace PasswordStrengthChecker.Tests
{
    [TestClass]
    public class PasswordStrengthCheckerTest
    {
        private readonly Mock<IUserRepository> mock = new Mock<IUserRepository>();

        [TestMethod]
        public void VerifyTest_If_Password_Length_Less_Than_7_Chars_ReturnFalse()
        {
            var password = "12Ab";
            var username = "user";
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_Password_Does_Not_Contain_Alphabetic_Character_ReturnFalse()
        {
            var password = "123456789";
            var username = "user";
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_Password_Does_Not_Contain_Numeric_Character_ReturnFalse()
        {
            var password = "abcdEFGH";
            var username = "user";
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_Password_Does_Not_Contain_Capital_Letter_ReturnFalse()
        {
            var password = "1234abcd";
            var username = "user";
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_Password_Length_Less_Than_10_Chars_For_Admin_ReturnFalse()
        {
            var password = "12Ab;[]";
            var username = "admin";
            var isAdmin = true;
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username, isAdmin);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_Password_Does_Not_Contain_Special_Character_For_Admin_ReturnFalse()
        {
            var password = "123456abcDEF";
            var username = "admin";
            var isAdmin = true;
            var expected = false;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username, isAdmin);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }

        [TestMethod]
        public void VerifyTest_If_CreateUser_Method_Is_Called_Only_Once_ReturnTrue()
        {
            var password = "123abcDEF";
            var username = "user";
            var expected = true;
            var checker = new PasswordStrengthChecker(mock.Object);

            var actual = checker.Verify(password, username);

            mock.Verify(repo => repo.CreateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(expected, actual.Item1, actual.Item2);
        }
    }
}