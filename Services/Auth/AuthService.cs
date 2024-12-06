using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Data;
using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Response;
using SampleCrud_ASPNET.Models.Utils;
using SampleCrud_ASPNET.Services.Utils;

namespace SampleCrud_ASPNET.Services.Auth;

public class AuthService(
    IMapper mapper,
    DataContext context,
    ILogger<AuthService> logger,
    JWTSettings jwt) : IAuthService
{
    public async Task<ApiResponse<AuthTokenDto>> RegisterUserAsync(AuthRegisterUserDto authRegisterUser)
    {
        var details = new Dictionary<string, string>();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var isUserExists = await context.Users.AnyAsync(
                u => u.UserName.Equals(authRegisterUser.UserName) ||
                    u.Email.Equals(authRegisterUser.Email));

            if (isUserExists)
            {
                details.Add("user", "Invalid user credentials.");
                return ApiResponse<AuthTokenDto>.ErrorResponse(
                    Error.ErrorType.BadRequest,
                    Error.BAD_REQUEST,
                    details
                );
            }

            var user = mapper.Map<User>(authRegisterUser);
            user.Password = PasswordUtil.HashPassword(user.Password);

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var tokens = await CreateTokensAsync(user);
            await transaction.CommitAsync();

            return ApiResponse<AuthTokenDto>.SuccessResponse(
                tokens, Success.USER_AUTHENTICATED);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "An unexpected error occurred in the register service.");
            return ApiResponse<AuthTokenDto>.ErrorResponse(
                Error.ErrorType.InternalServer,
                Error.CREATING_RESOURCE("User")
            );
        }
    }

    public async Task<ApiResponse<AuthTokenDto>> LoginUserAsync(AuthLoginUserDto authLoginUser)
    {
        var details = new Dictionary<string, string>();
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var user = await context.Users.Include(u => u.Tokens)
                .FirstOrDefaultAsync(u => u.Email.Equals(authLoginUser.Email));

            if (user is null || !PasswordUtil.VerifyPassword(
                user.Password, authLoginUser.Password))
            {
                details.Add("user", "Invalid user credentials.");
                return ApiResponse<AuthTokenDto>.ErrorResponse(
                    Error.ErrorType.Unauthorized,
                    Error.PERMISSION_DENIED,
                    details);
            }

            var tokens = await CreateTokensAsync(user);
            await transaction.CommitAsync();

            return ApiResponse<AuthTokenDto>.SuccessResponse(
                tokens, Success.USER_AUTHENTICATED);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "An unexpected error occurred in the login service.");
            return ApiResponse<AuthTokenDto>.ErrorResponse(
                Error.ErrorType.InternalServer,
                Error.CREATING_RESOURCE("Token")
            );
        }
    }
}
