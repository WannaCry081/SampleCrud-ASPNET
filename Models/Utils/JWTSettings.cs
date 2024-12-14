namespace SampleCrud_ASPNET.Models.Utils;

public class JWTSettings
{
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int AccessExpiry { get; init; }
    public int RefreshExpiry { get; init; }
    public int ResetPasswordExpiry { get; init; }
}
