using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SampleCrud_ASPNET.Services.Users;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Utils;
using SampleCrud_ASPNET.Models.Dtos.Response;
using SampleCrud_ASPNET.Models.Dtos.Users;

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
    /// <summary>
    ///     Get user details
    /// </summary>
    /// <returns>
    ///     Returns an <see cref="IActionResult"/> containing:
    ///     - <see cref="OkObjectResult" /> if the user details are successfully retrieved.
    ///     - <see cref="UnauthorizedObjectResult" /> if the user is not authenticated.
    ///     - <see cref="ProblemDetails" /> if an unexpected error occurred.
    /// </returns>
    /// <response code="200">Returns the user details.</response>
    /// <response code="401">Bad request.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("me")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(SuccessResponse<UserDetailsDto>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
