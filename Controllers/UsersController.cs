using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleCrud_ASPNET.Services.Users;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UsersController(
    ILogger<UsersController> logger,
    IUserService userService
    ) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetUserDetails()
    {
        try
        {
            var userId = ControllerUtil.GetUserId(User);

            if (userId == -1)
            {
                return Unauthorized(new { message = Error.PERMISSION_DENIED });
            }

            var response = await userService.GetUserDetailsAsync(userId);

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during getting user details.");
            return Problem("An internal server error occurred.");
        }
    }
}
