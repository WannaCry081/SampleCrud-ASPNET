using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Data;
using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;
using SampleCrud_ASPNET.Services.Users;

namespace SampleCrud_ASPNET.Services.Notes;

public class NoteService(
    IMapper mapper,
    DataContext context,
    IUserService userService) : INoteService
{
    public async Task<ApiResponse<NoteDto>> RetrieveAsync(int userId, int noteId)
    {
        var note = await context.Notes
            .SingleOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note is null)
        {
            return ApiResponse<NoteDto>.ErrorResponse(
                Error.ErrorType.NotFound,
                Error.FETCHING_RESOURCE(nameof(Note))
            );
        }

        return ApiResponse<NoteDto>.SuccessResponse(
            mapper.Map<NoteDto>(note));
    }

    public async Task<ApiResponse<List<NoteDto>>> ListAsync(int userId)
    {
        var notes = await context.Notes
            .Where(n => n.UserId == userId)
            .ToListAsync();

        return ApiResponse<List<NoteDto>>.SuccessResponse(
            mapper.Map<List<NoteDto>>(notes));
    }

    public async Task<ApiResponse<NoteDto>> CreateAsync(int userId, CreateNoteDto createNote)
    {
        var user = await userService.GetUserByIdAsync(userId);

        if (user is null)
        {
            return ApiResponse<NoteDto>.ErrorResponse(
               Error.ErrorType.Unauthorized,
               Error.PERMISSION_DENIED
           );
        }

        var note = mapper.Map<Note>(createNote, opt => opt.Items["User"] = user);

        await context.Notes.AddAsync(note);
        await context.SaveChangesAsync();

        return ApiResponse<NoteDto>.SuccessResponse(
            mapper.Map<NoteDto>(note));
    }

    public async Task<ApiResponse<NoteDto>> UpdateAsync(int userId, int noteId, UpdateNoteDto updateNote)
    {
        var note = await context.Notes
            .SingleOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note is null)
        {
            return ApiResponse<NoteDto>.ErrorResponse(
                Error.ErrorType.NotFound,
                Error.FETCHING_RESOURCE(nameof(Note))
            );
        }

        mapper.Map(updateNote, note);
        await context.SaveChangesAsync();

        return ApiResponse<NoteDto>.SuccessResponse(
            mapper.Map<NoteDto>(note));
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
