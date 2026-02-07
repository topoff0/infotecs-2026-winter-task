using Chronos.Application.Common.Errors;
using Chronos.Application.Common.Results;
using System.Net;
using System.Text.Json;

namespace Chronos.API.Middleware;

public sealed class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogCritical(exception, "Unhandled exception occurred");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = Error.Failure(
            "internal_server_error",
            "An unexpected error occurred."
        );

        var response = ResultT<Error>.Failure(error);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response.Error));
    }
}

