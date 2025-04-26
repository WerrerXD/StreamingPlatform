using System.Net;
using System.Text.Json;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Exceptions;

namespace Stream.Service.Presentation.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        using var scope = context.RequestServices.CreateScope();
        
        var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await loggingService.LogErrorAsync("An unhandled exception occurred.", ex);
            
            await ExceptionAsync(context, ex);
        }
    }

    private static Task ExceptionAsync(HttpContext context, Exception ex)
    {
        var message = "Unexpected error";

        var statusCode = ex switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            AlreadyExistsException => HttpStatusCode.Conflict,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };
        
        message = ex.Message;

        var result = JsonSerializer.Serialize(new { message });
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
}