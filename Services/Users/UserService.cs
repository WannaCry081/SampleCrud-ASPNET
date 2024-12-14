using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Data;
using SampleCrud_ASPNET.Models.Dtos.Users;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Services.Users;

public class UserService(DataContext context, IMapper mapper) : IUserService
{
    public async Task<ApiResponse<UserDetailsDto>> GetUserDetailsAsync(int id)
    {
        var user = await GetUserByIdAsync(id);

        if (user is null)
        {
            return ApiResponse<UserDetailsDto>.ErrorResponse(
                Error.ErrorType.Unauthorized,
                Error.PERMISSION_DENIED
            );
        }

        var userDetails = mapper.Map<UserDetailsDto>(user);
        return ApiResponse<UserDetailsDto>.SuccessResponse(userDetails);
    }

    private async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
}
