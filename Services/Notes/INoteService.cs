using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Notes;

/// <summary>
///     Note service interface.
/// </summary>
public interface INoteService
{
    /// <summary>
    ///    List all notes.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>
    ///     Retrieves a list of notes objects.
    /// </returns>
    Task<ApiResponse<List<NoteDto>>> ListAsync(int userId);

    /// <summary>
    ///     Retrieve a note.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="noteId"></param>
    /// <returns>
    ///     Retrieves a note object.
    /// </returns>
    Task<ApiResponse<NoteDto>> RetrieveAsync(int userId, int noteId);

    /// <summary>
    ///     Create a note.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="createNote"></param>
    /// <returns>
    ///     The newly created note object.
    /// </returns>
    Task<ApiResponse<NoteDto>> CreateAsync(int userId, CreateNoteDto createNote);

    /// <summary>
    ///     Update a note.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="noteId"></param>
    /// <param name="updateNote"></param>
    /// <returns>
    ///     The updated note object.
    /// </returns>
    Task<ApiResponse<NoteDto>> UpdateAsync(int userId, int noteId, UpdateNoteDto updateNote);

    /// <summary>
    ///    Delete a note.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="noteId"></param>
    /// <returns>
    ///     No content.
    /// </returns>
    Task<ApiResponse<object?>> DestroyAsync(int userId, int noteId);
}
