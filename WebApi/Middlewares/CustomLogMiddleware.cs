namespace WebApi.Middlewares;

/// <summary>
/// Bir Controller Action'ına girerken çalışacak middleware.
/// </summary>
public class CustomLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomLogMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"{context.Request.Path} path adresindeki action başladı.");
        await _next(context);
    }
}