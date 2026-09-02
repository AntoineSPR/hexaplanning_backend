using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Hexaplanning.Context;
using Hexaplanning.Models;
using Hexaplanning.Utilities;

namespace Hexaplanning.Services
{
    public class AuthService
    {
        // A refresh token that gets replayed shortly after it was rotated away is usually a benign
        // race (e.g. a mobile tab reloaded by the OS while a previous refresh call was still in
        // flight), not theft - RefreshAsync uses this window to tell the two apart.
        private static readonly TimeSpan RefreshReuseGracePeriod = TimeSpan.FromSeconds(30);

        private readonly DataContext context;
        private readonly UserManager<UserApp> userManager;

        public AuthService(
            DataContext context,
            UserManager<UserApp> userManager
        )
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<UserResponseDTO?> Register(UserCreateDTO model)
        {
            // V�rifier si l'adresse e-mail est d�j� utilis�e
            bool isEmailAlreadyUsed = await IsEmailAlreadyUsedAsync(model.Email);
            if (isEmailAlreadyUsed)
            {
                throw new Exception("Email d�j� utilis�");
            }

            try
            {
                // Cr�er un nouvel utilisateur en utilisant les donn�es du mod�le et la base de donn�es contextuelle
                UserApp newUser = model.ToUserApp();

                // Tenter de cr�er un nouvel utilisateur avec le gestionnaire d'utilisateurs
                IdentityResult result = await userManager.CreateAsync(newUser, model.Password);


                // V�rifier si la cr�ation de l'utilisateur a �chou�
                if (!result.Succeeded)
                {
                    // Si la cr�ation a �chou�, ajouter les erreurs au mod�le d'�tat et renvoyer une exception
                    var errors = Enumerable.Empty<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Append(error.Description);
                        throw new Exception(error.Description);
                    }
                }

                // Tenter d'ajouter l'utilisateur aux r�les sp�cifi�s dans le mod�le
                IdentityResult roleResult = await userManager.AddToRolesAsync(
                    user: newUser,
                    roles: ["Client"]
                );

                return newUser.ToUserResponseDTO();
            }
            catch
            {
                throw new Exception("Une erreur s'est produite");
            }
        }

        public async Task<UserResponseDTO> Update(UserCreateDTO model, ClaimsPrincipal UserPrincipal)
        {
            try
            {
                var user = UserService.GetUserFromClaim(UserPrincipal, context);
                if (user is null)
                {
                    throw new Exception("Account not found");
                }

                user = model.ToSimpleUser(user);

                await context.SaveChangesAsync();

                return user.ToUserResponseDTO();
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> ChangePassword(ChangePasswordDTO passwordData, ClaimsPrincipal userPrincipal)
        {
            try
            {
                var user = UserService.GetUserFromClaim(userPrincipal, context);
                if (user is null)
                {
                    throw new Exception("Utilisateur non trouv�");
                }

                var result = await userManager.ChangePasswordAsync(user, passwordData.CurrentPassword, passwordData.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Erreur lors du changement de mot de passe : {errors}");
                }

                return true;
            }
            catch
            {
                throw;
            }
        }


        public async Task<object> Login(UserLoginDTO model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var result = await userManager.CheckPasswordAsync(user: user, password: model.Password);
                if (!result)
                {
                    throw new Exception("Login failed");
                }

                var userRoles = await userManager.GetRolesAsync(user);

                return new LoginResponseDTO
                {
                    Token = await GenerateAccessTokenAsync(user),
                    RefreshToken = await GenerateRefreshTokenAsync(user),
                    User = user.ToUserResponseDTO(userRoles.ToList()),
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GenerateRefreshTokenAsync(UserApp user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(Env.REFRESH_TOKEN_VALIDITY_DAYS),
            };

            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync();

            return refreshToken.Token;
        }

        public async Task<LoginResponseDTO> RefreshAsync(string refreshToken)
        {
            var storedToken = await context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == refreshToken);

            if (storedToken == null)
            {
                throw new UnauthorizedAccessException("Refresh token invalide");
            }

            if (storedToken.RevokedAt != null)
            {
                var isWithinGracePeriod = DateTime.UtcNow - storedToken.RevokedAt.Value < RefreshReuseGracePeriod;
                if (isWithinGracePeriod && storedToken.ReplacedByToken != null)
                {
                    var successor = await context.RefreshTokens
                        .Include(r => r.User)
                        .FirstOrDefaultAsync(r => r.Token == storedToken.ReplacedByToken);

                    if (successor != null && successor.IsActive)
                    {
                        // Benign race, not theft: this token was already rotated a moment ago (e.g. two
                        // requests refreshed around the same time). Resync onto the token that already
                        // won instead of nuking every device's session.
                        var successorRoles = await userManager.GetRolesAsync(successor.User);
                        return new LoginResponseDTO
                        {
                            Token = await GenerateAccessTokenAsync(successor.User),
                            RefreshToken = successor.Token,
                            User = successor.User.ToUserResponseDTO(successorRoles.ToList()),
                        };
                    }
                }

                // The token has already been rotated away (or explicitly revoked via logout) and is
                // being presented again outside any benign race window - this is a reuse/theft signal,
                // not a normal expiry. Revoke every currently-active token for this user to force a
                // fresh login everywhere.
                var activeTokens = await context.RefreshTokens
                    .Where(r => r.UserId == storedToken.UserId && r.RevokedAt == null)
                    .ToListAsync();
                foreach (var token in activeTokens)
                {
                    token.RevokedAt = DateTime.UtcNow;
                }
                await context.SaveChangesAsync();

                throw new UnauthorizedAccessException("Refresh token d�j� utilis�");
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                // Simply expired, not reused - no mass revocation, just reject this one.
                throw new UnauthorizedAccessException("Refresh token expir�");
            }

            storedToken.RevokedAt = DateTime.UtcNow;

            var userRoles = await userManager.GetRolesAsync(storedToken.User);
            var newRefreshToken = await GenerateRefreshTokenAsync(storedToken.User);
            storedToken.ReplacedByToken = newRefreshToken;

            var response = new LoginResponseDTO
            {
                Token = await GenerateAccessTokenAsync(storedToken.User),
                RefreshToken = newRefreshToken,
                User = storedToken.User.ToUserResponseDTO(userRoles.ToList()),
            };

            await context.SaveChangesAsync();

            return response;
        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (storedToken != null && storedToken.RevokedAt == null)
            {
                storedToken.RevokedAt = DateTime.UtcNow;
                await context.SaveChangesAsync();
            }
        }

        public async Task<string> GenerateAccessTokenAsync(UserApp user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Env.JWT_KEY)
                );
                var credentials = new SigningCredentials(
                    key: securityKey,
                    algorithm: SecurityAlgorithms.HmacSha256
                );

                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: user.Email ?? string.Empty),
                new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
            };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(type: ClaimTypes.Role, value: userRole));
                }

                var token = new JwtSecurityToken(
                    issuer: Env.API_BACK_URL,
                    audience: Env.API_BACK_URL,
                    claims: authClaims,
                    expires: DateTime.UtcNow.AddMinutes(Env.ACCESS_TOKEN_VALIDITY_MINUTES),
                    signingCredentials: credentials
                );

                context.Entry(user).State = EntityState.Modified;

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                throw;
            }
        }

        private async Task<bool> IsEmailAlreadyUsedAsync(string email)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            return existingUser != null;
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    throw new Exception("Utilisateur non trouv�");
                }

                var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Erreur lors de la r�initialisation : {errors}");
                }

                return true;
            }
            catch
            {
                throw;
            }
        }               
    }
}
