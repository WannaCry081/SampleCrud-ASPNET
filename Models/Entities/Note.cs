using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SampleCrud_ASPNET.Models.Entities;

[Index(nameof(Title), IsUnique = true)]
public class Note : BaseEntity
{
    [ForeignKey("User")]
    public int UserId { get; set; }

    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    // Navigation Property
    public User User { get; set; } = null!;
}
