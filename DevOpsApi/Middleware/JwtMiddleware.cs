using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DevOpsApi.Services;

namespace DevOpsApi.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, JwtService jwtService)
    {
        if (!context.Request.Headers.ContainsKey("X-JWT-KWY"))
        {
            await _next(context);
            return;
        }

        if (!context.Request.Headers.TryGetValue("X-JWT-KWY", out var token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("JWT missing");
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtService.GetSecret()))
                },
                out _);
        }
        catch
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid JWT");
            return;
        }

        await _next(context);

    }
}