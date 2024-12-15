namespace SampleCrud_ASPNET.Models.Dtos.Notes;

public class NoteDto
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
