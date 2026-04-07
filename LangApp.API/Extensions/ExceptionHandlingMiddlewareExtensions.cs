using LangApp.API.Middlewares;

namespace LangApp.API.Extensions;

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandleMiddleware>();
    }
}
