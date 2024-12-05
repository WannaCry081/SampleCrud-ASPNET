using System.ComponentModel.DataAnnotations;

namespace SampleCrud_ASPNET.Models.Dtos.Auth;

public class AuthLoginUserDto
{
    [Required(ErrorMessage = "Email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; init; } = null!;

    [Required(ErrorMessage = "Password field is required.")]
    [StringLength(255, MinimumLength = 8,
        ErrorMessage = "Password must be between {2} and {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; init; } = null!;
}
