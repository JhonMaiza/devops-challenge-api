using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DevOpsApi.Services;

public class JwtService
{
    private const string SECRET = "super_secret_key_devops_test_2026";

    public string GenerateToken(int timeToLiveSec)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(SECRET);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("transactionId", Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddSeconds(timeToLiveSec),
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(descriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GetSecret()
    {
        return SECRET;
    }
}