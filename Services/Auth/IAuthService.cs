using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Auth;

public interface IAuthService
{
    Task<ApiResponse<AuthTokenDto>> RegisterUserAsync(
        AuthRegisterUserDto authRegisterUser);
}
