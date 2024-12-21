using Microsoft.AspNetCore.Mvc;
using SampleCrud_ASPNET.Controllers.Utils;
using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Models.Dtos.Response;
using SampleCrud_ASPNET.Services.Auth;

namespace SampleCrud_ASPNET.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(
    ILogger<AuthController> logger,
    IAuthService authService) : ControllerBase
{
    /// <summary>
    ///     Register a new user.
    /// </summary>
    /// <param name="authRegisterUser"></param>
    /// <returns>
    ///     Returns an <see cref="IActionResult"/> containing:
    ///     - <see cref="StatusCodeResult"/>  with the access and refresh tokens.
    ///     - <see cref="BadRequestObjectResult"/> if the request is invalid.
    ///     - <see cref="ProblemDetails"/> if an internal server error occurred. 
    /// </returns>
    /// <response code="200">Returns the access and refresh tokens.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("register")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(SuccessResponse<AuthTokenDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            return StatusCode(StatusCodes.Status201Created, response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during registering user details.");
            return Problem("An internal server error occurred.");
        }
    }

    /// <summary>
    ///    Login a user.
    /// </summary>
    /// <param name="authLoginUser"></param>
    /// <returns>
    ///     Returns an <see cref="IActionResult"/> containing:
    ///     - <see cref="OkObjectResult"/>  with the access and refresh tokens.
    ///     - <see cref="BadRequestObjectResult"/> if the request is invalid.
    ///     - <see cref="UnauthorizedObjectResult"/> if the user credentials are invalid.
    ///     - <see cref="ProblemDetails"/> if an internal server error occurred.
    /// </returns>
    /// <response code="200">Returns the access and refresh tokens.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="500">Internal server error.</response>
    [HttpPost("login")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(SuccessResponse<AuthTokenDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,
        Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser([FromBody] AuthLoginUserDto authLoginUser)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerUtil.ValidateState<object>(ModelState));
            }

            var response = await authService.LoginUserAsync(authLoginUser);

            if (!response.Success)
            {
                return ControllerUtil.GetErrorActionResult(response);
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error occurred during logging in user credentials.");
            return Problem("An internal server error occurred.");
        }
    }
}
