using System.Security.Claims;

namespace User.Service.Presentation.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new InvalidOperationException("User ID not found in the claims.");
        }

        return Guid.Parse(userIdClaim);
    }
    
    public static string GetAccessToken(this HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            throw new InvalidOperationException("Authorization header is missing.");
        }

        var accessToken = authHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
        
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new InvalidOperationException("Access token is invalid or empty.");
        }

        return accessToken;
    }
}