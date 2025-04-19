using System.Security.Claims;
using EchoLife.Common.Exceptions;

namespace EchoLife.Account.Services;

public static class ClaimsManager
{
    public static string? GetUserId(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public static string EnsureGetUserId(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            ?? throw new ForbiddenException("Missing necessary identifiers.");
    }

    public static string? GetUsername(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
}
