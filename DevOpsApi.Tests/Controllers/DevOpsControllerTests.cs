using DevOpsApi.Controllers;
using DevOpsApi.Models;
using DevOpsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsApi.Tests.Controllers;

public class DevOpsControllerTests
{
    [Fact]
    public void Post_Should_Return_Hello_Message()
    {
        // Arrange
        var jwtService = new JwtService();
        var controller = new DevOpsController(jwtService);

        var request = new DevOpsRequest
        {
            Message = "This is a test",
            To = "Juan Perez",
            From = "Rita Asturia",
            TimeToLifeSec = 45
        };

        // Act
        var result = controller.Post(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);

        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void Error_Should_Return_BadRequest()
    {
        // Arrange
        var jwtService = new JwtService();
        var controller = new DevOpsController(jwtService);

        // Act
        var result = controller.Error();

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("ERROR", badRequest.Value);
    }
}