using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Notes;

public interface INoteService
{
    Task<ApiResponse<List<NoteDto>>> ListAsync(int userId);
    Task<ApiResponse<NoteDto>> RetrieveAsync(int userId, int noteId);
    Task<ApiResponse<NoteDto>> CreateAsync(int userId, CreateNoteDto createNote);
    Task<ApiResponse<NoteDto>> UpdateAsync(int userId, int noteId, UpdateNoteDto updateNote);
    Task<ApiResponse<object?>> DeleteNoteAsync();
}
