using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Auth;

/// <summary>
///    Authentication service interface.
/// </summary>
public interface IAuthService
{
    /// <summary>
    ///     Register a new user.
    /// </summary>
    /// <param name="authRegisterUser"></param>
    /// <returns>
    ///     The authentication token.
    /// </returns>
    Task<ApiResponse<AuthTokenDto>> RegisterUserAsync(
        AuthRegisterUserDto authRegisterUser);

    /// <summary>
    ///    Login a user.
    /// </summary>
    /// <param name="authLoginUser"></param>
    /// <returns>
    ///     The authentication token.
    /// </returns>
    Task<ApiResponse<AuthTokenDto>> LoginUserAsync(
        AuthLoginUserDto authLoginUser);
}
