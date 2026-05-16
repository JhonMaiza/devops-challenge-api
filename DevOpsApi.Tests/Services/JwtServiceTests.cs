using DevOpsApi.Services;
using System.IdentityModel.Tokens.Jwt;

namespace DevOpsApi.Tests.Services;

public class JwtServiceTests
{
    [Fact]
    public void GenerateToken_Should_Return_Token()
    {
        // Arrange
        var service = new JwtService();

        // Act
        var token = service.GenerateToken(45);

        // Assert
        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateToken_Should_Have_Expiration()
    {
        // Arrange
        var service = new JwtService();

        // Act
        var token = service.GenerateToken(45);

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        // Assert
        Assert.NotNull(jwt.ValidTo);
    }

    [Fact]
    public void GetSecret_Should_Return_Value()
    {
        var service = new JwtService();

        var secret = service.GetSecret();

        Assert.False(string.IsNullOrEmpty(secret));
    }

}