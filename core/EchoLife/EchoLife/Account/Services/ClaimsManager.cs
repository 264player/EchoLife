using System.Security.Claims;
using EchoLife.Account.Models;
using EchoLife.Common.Exceptions;

namespace EchoLife.Account.Services;

public static class ClaimsManager
{
    public static string? GetUserId(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public static string GetAuthorizedUserId(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value
            ?? throw new ForbiddenException("Missing necessary identifiers.");
    }

    public static string? GetUsername(ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }

    public static List<string> GetRoles(ClaimsPrincipal principal)
    {
        return [.. principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value)];
    }

    public static void EnsureRole(ClaimsPrincipal principal, AccountRoles role)
    {
        var result = principal.Claims.Any(c =>
            c.Type == ClaimTypes.Role && c.Value == role.ToString()
        );
        if (!result)
        {
            throw new ForbiddenException(GetAuthorizedUserId(principal));
        }
    }
}
