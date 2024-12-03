namespace SampleCrud_ASPNET.Services.Utils;

public class PasswordUtil
{
    public static string HashPassword(string password)
    {
        string hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
        return hashPassword;
    }
}
