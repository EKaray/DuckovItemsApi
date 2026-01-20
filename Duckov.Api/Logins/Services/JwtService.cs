using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Duckov.Api.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Duckov.Api.Logins.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _jwt;

    public JwtService(IOptions<JwtOptions> options)
    {
        _jwt = options.Value;
    }

    public string GenerateToken(string userName)
    {
        var claims = new[]
        {
          new Claim(ClaimTypes.Name, userName),
          new Claim(ClaimTypes.Role, "Admin"),
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        if (string.IsNullOrWhiteSpace(_jwt.Key))
        {
            throw new InvalidOperationException("JWT_SECRET_KEY not set");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = _jwt.Audience,
            Issuer = _jwt.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwt.ExpireMinutes),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}