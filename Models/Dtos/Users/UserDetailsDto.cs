namespace SampleCrud_ASPNET.Models.Dtos.Users;

/// <summary>
///    DTO for user details.
/// </summary>
public class UserDetailsDto
{
    public string UserName { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string Lastname { get; init; } = null!;
    public string Email { get; init; } = null!;
}