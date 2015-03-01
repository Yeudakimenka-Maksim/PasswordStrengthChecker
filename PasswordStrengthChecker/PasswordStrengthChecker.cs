using System;
using System.Linq;

namespace PasswordStrengthChecker
{
    public class PasswordStrengthChecker
    {
        private readonly IUserRepository repository;

        public PasswordStrengthChecker(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Tuple<bool, string> Verify(string password, string username, bool isAdmin = false)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password should not be empty", "password");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username should not be empty", "username");

            if (!isAdmin)
            {
                if (password.Length < 7)
                    return new Tuple<bool, string>(false, "Password length is less than 7 characters");
            }
            else
            {
                if (password.Length < 10)
                    return new Tuple<bool, string>(false, "Password length for admin is less than 10 characters");

                if (password.IndexOfAny("!\"№;%:?*()_+=-~/\\<>,.[]{}".ToCharArray()) < 0)
                    return new Tuple<bool, string>(false, "Password does not contain special character");
            }

            if (!password.Any(char.IsLetter))
                return new Tuple<bool, string>(false, "Password does not contain alphabetic character");

            if (!password.Any(char.IsDigit))
                return new Tuple<bool, string>(false, "Password does not contain numeric character");

            if (!password.Any(char.IsUpper))
                return new Tuple<bool, string>(false, "Password does not contain uppercase character");

            repository.CreateUser(username, password);
            return new Tuple<bool, string>(true, "OK");
        }
    }
}