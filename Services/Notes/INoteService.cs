using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Notes;

public interface INoteService
{
    Task<ApiResponse<List<NoteDto>>> GetNotesAsync();
    Task<ApiResponse<NoteDto>> GetNoteAsync();
    Task<ApiResponse<NoteDto>> CreateAsync(int userId, CreateNoteDto createNote);
    Task<ApiResponse<NoteDto>> UpdateNoteAsync();
    Task<ApiResponse<object?>> DeleteNoteAsync();
}
