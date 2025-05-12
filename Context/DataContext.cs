using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Procrastinator.Models;

namespace Procrastinator.Context
{
    public class DataContext : IdentityDbContext<UserApp>
    {
        /// <summary>
        /// Constructeur de base de la classe DataContext : permet de configurer le contexte de la BDD
        /// en se basant sur les options passées en paramètre (héritées de la classe IdentityDbContext qui fait partie de la librairie Identity)
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        /// <summary>
        /// Méthode appelée lors de la création du modèle de données : 
        /// permet de le configurer en ajoutant des données initiales
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = "63a2a3ac-442e-4e4c-ad91-1443122b5a6a",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "63a2a3ac-442e-4e4c-ad91-1443122b5a6a",
                },
                new Role()
                {
                    Id = "12ccaa16-0d50-491e-8157-ec1b133cf120",
                    Name = "Client",
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = "12ccaa16-0d50-491e-8157-ec1b133cf120",
                },

            };
            builder.Entity<Role>().HasData(roles);
        }

        // Accès aux tables : 
        public DbSet<UserApp> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Quest> Quests { get; set; }
    }
}
