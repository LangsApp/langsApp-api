using LangApp.BLL.Exceptions;
using System.Text.Json;

namespace LangApp.API.Middlewares;

public class ExceptionHandleMiddleware
{
   private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConflictException ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status409Conflict,
                ex.Message);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(
                context,
                StatusCodes.Status500InternalServerError,
                ex.Message);
        }
    }
    private static async Task HandleExceptionAsync(
            HttpContext context,
            int statusCode,
            string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
