namespace DevOpsApi.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string APIKEY = "2f5ae96c-b558-4c7b-a590-a501ae1c3f6c";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(
            "X-Parse-REST-API-Key", out var extractedKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        if (extractedKey != APIKEY)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}