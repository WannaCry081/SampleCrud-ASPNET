namespace SampleCrud_ASPNET.Models.Utils;

public class SMTPSettings
{
    public string Server { get; init; } = null!;
    public int Port { get; init; }
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}
