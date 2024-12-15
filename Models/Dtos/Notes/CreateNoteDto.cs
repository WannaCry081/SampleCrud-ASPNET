namespace SampleCrud_ASPNET.Models.Dtos.Notes;

public class CreateNoteDto
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
}
