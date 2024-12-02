using Microsoft.AspNetCore.Mvc;

namespace SampleCrud_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{

}
