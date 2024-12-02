namespace SampleCrud_ASPNET.Models.Dtos.Auth;

public class AuthTokenDto
{
    public string Access { get; init; } = string.Empty;
    public string Refresh { get; init; } = string.Empty;
}
