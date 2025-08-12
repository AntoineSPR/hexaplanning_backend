using System.Security.Claims;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Utilities
{
    public class CheckUser
    {
        public static string? GetUserIdFromClaim(ClaimsPrincipal userClaim)
        {
            string? userId = userClaim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }
    }
}
