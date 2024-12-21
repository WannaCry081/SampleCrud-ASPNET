namespace SampleCrud_ASPNET.Models.Dtos.Notes;

/// <summary>
///   DTO for updating a note.
/// </summary>
public class UpdateNoteDto
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
}
