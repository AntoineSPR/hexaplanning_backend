using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Procrastinator.Context;
using Procrastinator.Models;
using Testcontainers.PostgreSql;

namespace TestsIntegration;

public class Factory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgresContainer;

    public Factory()
    {
        _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("admin")
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Environment.SetEnvironmentVariable("API_BACK_URL", "https://localhost:7168");
        Environment.SetEnvironmentVariable("API_FRONT_URL", "https://localhost:4200");
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        Environment.SetEnvironmentVariable("JWT_KEY", "i7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPsi7RdBacZPs");
        Environment.SetEnvironmentVariable("TOKEN_VALIDITY_DAYS", "7");

        await Task.Delay(1000);
    }

    public new async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Ajouter le service provider pour injecter les services necessaires
            var tempServiceProvider = services.BuildServiceProvider();
            var configuration = tempServiceProvider.GetService<IConfiguration>();

            // Chercher le service DbContextOptions<DataContext> et le supprimer
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<DataContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Le remplacer par un qui pointe sur la base de test PostgreSQL uniquement
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(_postgresContainer.GetConnectionString());
                options.EnableSensitiveDataLogging();
            });

            var serviceProvider = services.BuildServiceProvider();

            // Créer la base de données et appliquer les migrations de base
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserApp>>();

            try
            {
                // Creer la base de données
                context.Database.EnsureCreated();

                // Ajouter les données de test
                SeedDataAsync(userManager, context).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization failed: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        });
    }

    private async Task SeedDataAsync(UserManager<UserApp> userManager, DataContext dataContext)
    {
        var user = new UserApp
        {
            FirstName = "Test",
            LastName = "User",
            Email = "user@gmail.com",
            UserName = "user@gmail.com",
            EmailConfirmed = true,
        };

        var userResult = await userManager.CreateAsync(user, "testuser123!");

        var quest = new Quest
        {
            Title = "Test Quest",
            Description = "This is a test quest",
            IsDone = false,
            UserId = user.Id,
            EstimatedTime = 30,
            Priority = QuestPriority.PRIMARY,
            IsAssigned = false
        };

        var questResult = await dataContext.Quests.AddAsync(quest);
        await dataContext.SaveChangesAsync();
    }
}