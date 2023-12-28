using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace WebApi.Middlewares;

/// <summary>
/// Api'de oluşan bütün exception'ları yakalayan middleware.
/// </summary>
public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch ( Exception ex )
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Server Error"));
    }
}