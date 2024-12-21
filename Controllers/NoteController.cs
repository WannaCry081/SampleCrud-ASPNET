using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleCrud_ASPNET.Services.Notes;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Utils;
using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Dtos.Response;

namespace SampleCrud_ASPNET.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notes")]
public class NoteController(
    ILogger<NoteController> logger,
    INoteService noteService) : ControllerBase
{
    /// <summary>
    ///     Retrieve all notes of the authenticated user.
    /// </summary>
    /// <returns>
    ///     Returns an <see cref="IActionResult" /> containing:
    ///     - <see cref="OkObjectResult"/> if the notes are successfully retrieved.
    ///     - <see cref="UnauthorizedObjectResult"/> if the user is not authenticated.
    ///     - <see cref="ProblemDetails" /> if an unexpected error occurred.
    /// </returns>
    /// <response code="200">Returns the notes of the authenticated user.</response>
    /// <response code="401">Bad request.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(SuccessResponse<NoteDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RetrieveNotes()
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            var response = await noteService.ListAsync(userId);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during retrieving user notes.");
            return Problem("An internal server error occurred.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> RetrieveNote([FromRoute] int id)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            var response = await noteService.RetrieveAsync(userId, id);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during fetching user note.");
            return Problem("An internal server error occurred.");
        }
    }

    /// <summary>
    ///    Create a new note for the authenticated user.
    /// </summary>
    /// <param name="createNote"></param>
    /// <returns>
    ///     Returns an <see cref="IActionResult" /> containing:
    ///     - <see cref="StatusCodeResult"/> if the note is successfully created.
    ///     - <see cref="BadRequestObjectResult"/> if the request is invalid.
    ///     - <see cref="UnauthorizedObjectResult"/> if the user is not authenticated.
    ///     - <see cref="ProblemDetails" /> if an unexpected error occurred.
    /// </returns>
    /// <response code="201">Returns the created note.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created,
        Type = typeof(SuccessResponse<NoteDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto createNote)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerUtil.ValidateState<object>(ModelState));
            }

            var response = await noteService.CreateAsync(userId, createNote);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during creating user note.");
            return Problem("An internal server error occurred.");
        }
    }

    /// <summary>
    ///    Update an existing note of the authenticated user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateNote"></param>
    /// <returns>
    ///     Returns an <see cref="IActionResult" /> containing:
    ///     - <see cref="OkObjectResult"/> if the note is successfully updated. 
    ///     - <see cref="BadRequestObjectResult"/> if the request is invalid.
    ///     - <see cref="UnauthorizedObjectResult"/> if the user is not authenticated.
    ///     - <see cref="ProblemDetails" /> if an unexpected error occurred.
    /// </returns>
    /// <response code="200">Returns the updated note.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(SuccessResponse<NoteDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateNote(
        [FromRoute] int id, [FromBody] UpdateNoteDto updateNote)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerUtil.ValidateState<object>(ModelState));
            }

            var response = await noteService.UpdateAsync(userId, id, updateNote);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during updating user note.");
            return Problem("An internal server error occurred.");
        }
    }

    /// <summary>
    ///     Delete an existing note of the authenticated user.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    ///     Returns an <see cref="IActionResult" /> containing:
    ///     - <see cref="NoContentResult"/> if the note is successfully deleted.
    ///     - <see cref="UnauthorizedObjectResult"/> if the user is not authenticated.
    ///     - <see cref="ProblemDetails" /> if an unexpected error occurred.
    /// </returns>
    /// <response code="204">Returns no content.</response>
    /// <response code="401">Unauthorized access.</response>
    /// <response code="500">Internal server error.</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DestroyNote([FromRoute] int id)
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            var response = await noteService.DestroyAsync(userId, id);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during deleting user note.");
            return Problem("An internal server error occurred.");
        }
    }
}
