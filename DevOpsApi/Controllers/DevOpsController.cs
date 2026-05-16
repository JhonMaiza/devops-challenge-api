using DevOpsApi.Models;
using DevOpsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DevOpsController : ControllerBase
{
    private readonly JwtService _jwtService;

    public DevOpsController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }
    [HttpPost]
    public IActionResult Post([FromBody] DevOpsRequest request)
    {
        var jwt = _jwtService.GenerateToken(request.TimeToLifeSec);

        var response = new
        {
            message = $"Hello {request.To} your message will be sent",
            jwt = jwt,
            host = Environment.MachineName
        };

        return Ok(response);
    }

    [HttpGet]
    [HttpPut]
    [HttpDelete]
    public IActionResult Error()
    {
        return BadRequest("ERROR");
    }
}