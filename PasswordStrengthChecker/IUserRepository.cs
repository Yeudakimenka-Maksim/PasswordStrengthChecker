namespace PasswordStrengthChecker
{
    public interface IUserRepository
    {
        int CreateUser(string username, string password);
    }
}