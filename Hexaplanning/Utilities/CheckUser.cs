using System.Security.Claims;
using Hexaplanning.Context;
using Hexaplanning.Models;

namespace Hexaplanning.Utilities
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
