using User.Service.Domain.Interfaces;

namespace User.Service.Presentation.Middleware;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IBlacklistedTokenRepository _blacklistedTokenRepository;

    public TokenValidationMiddleware(RequestDelegate next, IBlacklistedTokenRepository blacklistedTokenRepository)
    {
        _next = next;
        _blacklistedTokenRepository = blacklistedTokenRepository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (!string.IsNullOrEmpty(accessToken))
        {
            var isBlacklisted = await _blacklistedTokenRepository.IsBlacklistedAsync(accessToken);

            if (isBlacklisted)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token is blacklisted.");
                return;
            }
        }

        await _next(context);
    }
}