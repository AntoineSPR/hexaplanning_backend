using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Procrastinator.Context;
using Procrastinator.Models;
using Procrastinator.Utilities;
using Procrastinator.Models.UserApp;
using Microsoft.AspNetCore.Http;

namespace Procrastinator.Services

{
    public class AuthService

        public async Task<LoginResponseDTO?> RefreshToken(string refreshToken)
        {
            var tokenEntity = context.RefreshTokens.Include(r => r.User).FirstOrDefault(r => r.Token == refreshToken);
            if (tokenEntity == null || !tokenEntity.IsActive)
                return null;

            // Revoke old token
            tokenEntity.Revoked = DateTime.UtcNow;
            tokenEntity.RevokedByIp = GetIpAddress();

            // Generate new refresh token
            var newRefreshToken = GenerateRefreshToken(GetIpAddress());
            tokenEntity.User.RefreshTokens.Add(newRefreshToken);
            context.Update(tokenEntity.User);
            await context.SaveChangesAsync();

            var userRoles = await userManager.GetRolesAsync(tokenEntity.User);

            return new LoginResponseDTO
            {
                Token = await GenerateAccessTokenAsync(tokenEntity.User),
                RefreshToken = newRefreshToken.Token,
                User = tokenEntity.User.ToUserResponseDTO(userRoles.ToList()),
            };
        }
    {
    private readonly DataContext context;
    private readonly UserManager<UserApp> userManager;
    private readonly IHttpContextAccessor httpContextAccessor;

        public AuthService(
            DataContext context,
            UserManager<UserApp> userManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            this.context = context;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserResponseDTO?> Register(UserCreateDTO model)
        {
            // Vérifier si l'adresse e-mail est déjà utilisée
            bool isEmailAlreadyUsed = await IsEmailAlreadyUsedAsync(model.Email);
            if (isEmailAlreadyUsed)
            {
                throw new Exception("Email déjà utilisé");
            }

            try
            {
                // Créer un nouvel utilisateur en utilisant les données du modèle et la base de données contextuelle
                UserApp newUser = model.ToUserApp();

                // Tenter de créer un nouvel utilisateur avec le gestionnaire d'utilisateurs
                IdentityResult result = await userManager.CreateAsync(newUser, model.Password);


                // Vérifier si la création de l'utilisateur a échoué
                if (!result.Succeeded)
                {
                    // Si la création a échoué, ajouter les erreurs au modèle d'état et renvoyer une exception
                    var errors = Enumerable.Empty<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Append(error.Description);
                        throw new Exception(error.Description);
                    }
                }

                // Tenter d'ajouter l'utilisateur aux rôles spécifiés dans le modèle
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
                    throw new Exception("Utilisateur non trouvé");
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

                // Generate refresh token
                var refreshToken = GenerateRefreshToken(GetIpAddress());
                user.RefreshTokens.Add(refreshToken);
                context.Update(user);
                await context.SaveChangesAsync();

                return new LoginResponseDTO
                {
                    Token = await GenerateAccessTokenAsync(user),
                    RefreshToken = refreshToken.Token,
                    User = user.ToUserResponseDTO(userRoles.ToList()),
                };
            }
            catch
            {
                throw;
            }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            var randomBytes = new byte[64];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string GetIpAddress()
        {
            return httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "127.0.0.1";
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
                    expires: DateTime.Now.AddDays(Env.TOKEN_VALIDITY_DAYS),
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
                    throw new Exception("Utilisateur non trouvé");
                }

                var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Erreur lors de la réinitialisation : {errors}");
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
