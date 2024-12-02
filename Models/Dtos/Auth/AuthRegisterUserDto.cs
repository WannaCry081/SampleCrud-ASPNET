using System.ComponentModel.DataAnnotations;

namespace SampleCrud_ASPNET.Models.Dtos.Auth;

public class AuthRegisterUserDto
{

    [Required(ErrorMessage = "UserName field is required.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "UserName must be between {2} and {1} characters.")]
    public string UserName { get; init; } = null!;

    [Required(ErrorMessage = "FirstName field is required.")]
    [StringLength(50, MinimumLength = 2,
        ErrorMessage = "FirstName must be between {2} and {1} characters.")]
    public string Firstname { get; init; } = null!;

    [Required(ErrorMessage = "LastName field is required.")]
    [StringLength(50, MinimumLength = 1,
        ErrorMessage = "LastName must be between {2} and {1} characters.")]
    public string LastName { get; init; } = null!;

    [Required(ErrorMessage = "Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "Password field is required.")]
    [StringLength(255, MinimumLength = 8,
        ErrorMessage = "Password must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;

    [Required(ErrorMessage = "RePassword field is required.")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    [DataType(DataType.Password)]
    public string RePassword { get; init; } = null!;
}
