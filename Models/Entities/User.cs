using Microsoft.EntityFrameworkCore;

namespace SampleCrud_ASPNET.Models.Entities;

[Index(nameof(Email), nameof(UserName), IsUnique = true)]
public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    // Navigation Property
    public ICollection<Note> Notes { get; set; } = [];
    public ICollection<Token> Tokens { get; set; } = [];
}
