using Microsoft.AspNetCore.Mvc;

namespace SampleCrud_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/notes")]
public class NoteController : ControllerBase
{

}
