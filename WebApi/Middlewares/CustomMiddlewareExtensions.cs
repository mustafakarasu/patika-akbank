namespace WebApi.Middlewares;

/// <summary>
/// Program.cs dosyasında middleware'lara eklemek için middleware extension'ları.
/// </summary>
public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder CustomLogging(this IApplicationBuilder appBuilder, ILogger logger)
    {
        return appBuilder.UseMiddleware<CustomLogMiddleware>(logger);
    }

    public static IApplicationBuilder CustomException(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<CustomExceptionMiddleware>();
    }
}