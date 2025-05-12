using Microsoft.AspNetCore.Identity;
using Procrastinator.Context;
using Procrastinator.Models;
using System.Security.Claims;

namespace Procrastinator.Services
{
    public class UserService
    {
        private readonly DataContext context;
        private readonly UserManager<UserApp> userManager;

        public UserService(
                DataContext context,
                UserManager<UserApp> userManager
                )
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserResponseDTO?> GetUserByEmail(string email)
        {
            UserApp? user = context.Users.FirstOrDefault(x => x.Email == email);
            List<string> roles = new();
            if (user != null)
            {
                roles = (await userManager.GetRolesAsync(user)).ToList();
            }
            UserResponseDTO? userResponse = user?.ToUserResponseDTO(roles);
            return userResponse;
        }
        public static UserApp? GetUserFromClaim(
            ClaimsPrincipal userClaim,
            DataContext context
        )
        {
            string? userEmail = userClaim
                .Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)
                ?.Value;
            if (userEmail == null)
                return null;
            var user = context
                .Users
                .FirstOrDefault(x => x.Email == userEmail);

            if (user == null)
                return null;
            return user;
        }

        public static async Task<(UserApp? user, bool isNull)> CheckUserNullByEmail(
            string email,
            UserManager<UserApp> _userManager
        )
        {
            var user = await _userManager.FindByEmailAsync(email);

            return (user, user is null);
        }

        public static async Task<(UserApp? user, bool isNull)> CheckUserNullByUserId(
            string id,
            UserManager<UserApp> _userManager
        )
        {
            var user = await _userManager.FindByIdAsync(id);

            return (user, user is null);
        }
    }
}
