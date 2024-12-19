using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleCrud_ASPNET.Services.Notes;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Utils;
using SampleCrud_ASPNET.Models.Dtos.Notes;

namespace SampleCrud_ASPNET.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notes")]
public class NoteController(
    ILogger<NoteController> logger,
    INoteService noteService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetNotes()
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
    public async Task<IActionResult> GetNote([FromRoute] int id)
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

    [HttpPost]
    public Task<IActionResult> CreateNote()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public Task<IActionResult> UpdateNote()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public Task<IActionResult> DeleteNote()
    {
        throw new NotImplementedException();
    }
}
