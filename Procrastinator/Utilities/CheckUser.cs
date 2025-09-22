using System.Security.Claims;
using Procrastinator.Context;
using Procrastinator.Models;

namespace Procrastinator.Utilities
{
    public class CheckUser
    {
        public static Guid? GetUserIdFromClaim(ClaimsPrincipal userClaim)
        {
            var userIdString = userClaim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(userIdString, out var userId))
            {
                return userId;
            }
            return null;
        }

    }
}
