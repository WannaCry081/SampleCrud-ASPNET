using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Notes;

public class NoteService : INoteService
{
    public Task<ApiResponse<NoteDto>> CreateNoteAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<object?>> DeleteNoteAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<NoteDto>> GetNoteAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<List<NoteDto>>> GetNotesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<NoteDto>> UpdateNoteAsync()
    {
        throw new NotImplementedException();
    }
}
