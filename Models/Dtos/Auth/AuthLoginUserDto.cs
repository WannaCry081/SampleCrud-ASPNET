using System.ComponentModel.DataAnnotations;

namespace SampleCrud_ASPNET.Models.Dtos.Auth;

/// <summary>
///    DTO for logging in a user.
/// </summary>
public class AuthLoginUserDto
{
    /// <summary>
    ///    The email of the user.
    /// </summary>
    /// <example>johndoe@example.com</example>
    [Required(ErrorMessage = "Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; init; } = null!;

    /// <summary>
    ///     The password of the user.
    /// </summary>
    /// <example>MyStrongPass123</example>
    [Required(ErrorMessage = "Password field is required.")]
    [StringLength(255, MinimumLength = 8,
        ErrorMessage = "Password must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;
}
