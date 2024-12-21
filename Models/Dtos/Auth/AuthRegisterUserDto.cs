using System.ComponentModel.DataAnnotations;

namespace SampleCrud_ASPNET.Models.Dtos.Auth;

/// <summary>
///    DTO for registering a new user.
/// </summary>
public class AuthRegisterUserDto
{
    /// <summary>
    ///    The username of the user.
    /// </summary>
    /// <example>Johnny</example>
    [Required(ErrorMessage = "UserName field is required.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "UserName must be between {2} and {1} characters.")]
    public string UserName { get; init; } = null!;

    /// <summary>
    ///     The first name of the user.
    /// </summary>
    /// <example>John</example>
    [Required(ErrorMessage = "FirstName field is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "FirstName must be between {2} and {1} characters.")]
    public string Firstname { get; init; } = null!;

    /// <summary>
    ///    The last name of the user.
    /// </summary>
    /// <example>Doe</example>
    [Required(ErrorMessage = "LastName field is required.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "LastName must be between {2} and {1} characters.")]
    public string LastName { get; init; } = null!;

    /// <summary>
    ///     The email of the user.
    /// </summary>
    /// <example>johndoe@example.com</example>
    [Required(ErrorMessage = "Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; init; } = null!;

    /// <summary>
    ///    The password of the user.
    /// </summary>
    /// <example>MyStrongPass123</example>
    [Required(ErrorMessage = "Password field is required.")]
    [StringLength(255, MinimumLength = 8,
        ErrorMessage = "Password must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;

    /// <summary>
    ///   The re-entered password of the user.
    /// </summary>
    /// <example>MyStrongPass123</example>
    [Required(ErrorMessage = "RePassword field is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string RePassword { get; init; } = null!;
}
