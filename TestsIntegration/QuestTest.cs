using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Procrastinator.Models;

namespace TestsIntegration
{
    public class QuestTest: IClassFixture<Factory>
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonOptions;
        private readonly Factory factory;

        public QuestTest(Factory factory)
        {
            this.factory = factory;
            this.httpClient = factory.CreateClient();
            this.jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
            };
        }

        [Fact]
        public async Task ReadQuest_ShouldReturnQuestList_WhenAuthenticated()
        {
            // Arrange - Get test user and create JWT token
            using var scope = factory.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserApp>>();
            var testUser = await userManager.FindByEmailAsync("user@gmail.com");
            
            Assert.NotNull(testUser);

            var token = GenerateJwtToken(testUser);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await httpClient.GetAsync("/quest");
            
            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseArray = JsonSerializer.Deserialize<List<Quest>>(responseContent, jsonOptions);

            Assert.NotNull(responseArray);
            Assert.Single(responseArray); // Should have exactly 1 quest from seed data
            
            var quest = responseArray.First();
            Assert.Equal("Test Quest", quest.Title);
            Assert.Equal("This is a test quest", quest.Description);            
            Assert.Equal(testUser.Id, quest.UserId);
        }

        [Fact]
        public async Task ReadQuest_ShouldReturnUnauthorized_WhenNotAuthenticated()
        {
            // Act
            var response = await httpClient.GetAsync("/quest");
            
            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        private string GenerateJwtToken(UserApp user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException("JWT_KEY not found");
            var apiBackUrl = Environment.GetEnvironmentVariable("API_BACK_URL") ?? throw new InvalidOperationException("API_BACK_URL not found");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = apiBackUrl,
                Audience = apiBackUrl,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}