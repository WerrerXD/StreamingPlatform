using System.Net;
using System.Text.Json;
using User.Service.Application.Exceptions;
using User.Service.Domain.Interfaces;

namespace User.Service.Presentation.Middleware;

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
        catch (Exception excp)
        {
            await loggingService.LogErrorAsync("An unhandled exception occurred.", excp);
            
            await ExceptionAsync(context, excp);
        }
    }

    private static Task ExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode statusCode;
        string message = "Unexpected error";
        var excpType = ex.GetType();

        if (excpType == typeof(BadRequestException))
        {
            statusCode = HttpStatusCode.BadRequest;
            message = ex.Message;
        }
        else if (excpType == typeof(NotFoundException))
        {
            statusCode = HttpStatusCode.NotFound;
            message = ex.Message;
        }
        else if (excpType == typeof(AlreadyExistsException))
        {
            statusCode = HttpStatusCode.Conflict;
            message = ex.Message;
        }
        else if (excpType == typeof(UnauthorizedException))
        {
            statusCode = HttpStatusCode.Unauthorized;
            message = ex.Message;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = ex.Message;
        }

        var result = JsonSerializer.Serialize(new { message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }
}
