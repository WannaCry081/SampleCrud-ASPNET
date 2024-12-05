using Microsoft.AspNetCore.Mvc;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Services.Auth;

namespace SampleCrud_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] AuthRegisterUserDto authRegisterUser)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerUtil.ValidateState<object>(ModelState));
            }

            var response = await authService.RegisterUserAsync(authRegisterUser);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "");
            return Problem();
        }
    }
}
