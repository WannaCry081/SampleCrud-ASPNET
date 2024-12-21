using SampleCrud_ASPNET.Models.Dtos.Users;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;

namespace SampleCrud_ASPNET.Services.Users;

/// <summary>
///     User service interface.
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Get the authenticated user details.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    ///     The user details.
    /// </returns>
    Task<ApiResponse<UserDetailsDto>> GetUserDetailsAsync(int id);

    /// <summary>
    ///    Get the user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>
    ///     If the id is valid return the user object, otherwise return null.
    /// </returns>
    Task<User?> GetUserByIdAsync(int id);
}
