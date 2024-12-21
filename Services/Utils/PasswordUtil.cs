namespace SampleCrud_ASPNET.Services.Utils;

public static class PasswordUtil
{
    public static string HashPassword(string password)
    {
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashPassword;
    }

    public static bool VerifyPassword(string hashPassword, string password)
    {

        bool isEqual = BCrypt.Net.BCrypt.Verify(password, hashPassword);
        return isEqual;
    }
}
