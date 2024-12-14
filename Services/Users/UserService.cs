using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Data;
using SampleCrud_ASPNET.Models.Dtos.Users;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Services.Users;

public class UserService(DataContext context) : IUserService
{
    public Task<ApiResponse<UserDetailsDto>> GetUserDetailsAsync(int id)
    {
        throw new NotImplementedException();
    }

    private async Task<User?> GetUserByIdAsync(int id)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

}
