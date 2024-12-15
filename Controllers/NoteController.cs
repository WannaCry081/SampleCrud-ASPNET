using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SampleCrud_ASPNET.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notes")]
public class NoteController : ControllerBase
{

    [HttpGet]
    public Task<IActionResult> GetNotes()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public Task<IActionResult> GetNote()
    {
        throw new NotImplementedException();
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
