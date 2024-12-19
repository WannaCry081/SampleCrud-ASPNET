using SampleCrud_ASPNET.Models.Dtos.Users;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Users;

public interface IUserService
{
    Task<ApiResponse<UserDetailsDto>> GetUserDetailsAsync(int id);
    Task<User?> GetUserByIdAsync(int id);
}
