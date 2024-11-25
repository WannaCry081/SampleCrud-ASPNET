using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleCrud_ASPNET.Models.Entities;

[Index(nameof(Key), IsUnique = true)]
public class Token : BaseEntity
{
    [ForeignKey("User")]
    public int UserId { get; set; }

    public bool IsRevoked { get; set; } = false;
    public string Key { get; set; } = null!;
    public DateTime Expiration { get; set; }

    // Navigation Property 
    public User User { get; set; } = null!;
}
