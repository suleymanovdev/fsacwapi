using fsacwapi.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fsacwapi.Infrastructure.Services.JWT;

public class JsonWebTokenService
{
    public string GenerateJwtToken(Guid userId, Role userRole)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userId.ToString()),
            new Claim(ClaimTypes.Role, userRole.ToString())
        };

        var token = new JwtSecurityToken(
            Environment.GetEnvironmentVariable("JWT__Issuer"),
            Environment.GetEnvironmentVariable("JWT__Audience"),
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
