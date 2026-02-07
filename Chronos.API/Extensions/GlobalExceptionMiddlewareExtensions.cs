using Chronos.API.Middleware;

namespace Chronos.API.Extensions;

public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}

