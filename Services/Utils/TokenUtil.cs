using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using SampleCrud_ASPNET.Models.Entities;
using SampleCrud_ASPNET.Models.Utils;

namespace SampleCrud_ASPNET.Services.Utils;

public static class TokenUtil
{
    public enum TokenType
    {
        REFRESH,
        ACCESS,
        RESET
    }

    public static string GenerateToken(User user, JWTSettings jwt, TokenType type)
    {
        DateTime now = DateTime.Now;
        DateTime expires = type switch
        {
            TokenType.REFRESH => now.AddDays(jwt.RefreshExpiry),
            TokenType.ACCESS => now.AddMinutes(jwt.AccessExpiry),
            _ => now.AddMinutes(jwt.ResetPasswordExpiry)
        };

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Iss, jwt.Issuer),
            new(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new(JwtRegisteredClaimNames.Exp,
                new DateTimeOffset(expires).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

        if (type != TokenType.REFRESH)
        {
            claims.Add(new(ClaimTypes.Email, user.Email));
            claims.Add(
                type == TokenType.ACCESS
                    ? new(ClaimTypes.NameIdentifier, user.Id.ToString())
                    : new("purpose", "reset-password"));
        }

        var secret = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(jwt.Secret));
        var creds = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
