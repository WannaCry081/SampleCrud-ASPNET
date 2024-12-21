namespace SampleCrud_ASPNET.Models.Dtos.Notes;

/// <summary>
///   DTO for creating a new note.
/// </summary>
public class CreateNoteDto
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
}
