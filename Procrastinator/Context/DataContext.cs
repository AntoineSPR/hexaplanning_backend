using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Procrastinator.Models;
using Procrastinator.Utilities;

namespace Procrastinator.Context
{
    // Déclaration de la classe Role qui hérite de IdentityRole<Guid> :
    public class Role : IdentityRole<Guid> { }

    public class DataContext : IdentityDbContext<UserApp, Role, Guid>
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

            builder
                .Entity<Quest>()
                .HasOne(q => q.HexAssignment)
                .WithOne(h => h.Quest)
                .HasForeignKey<HexAssignment>(h => h.QuestId);

            builder.Entity<RefreshToken>().HasIndex(r => r.Token).IsUnique();

            builder
                .Entity<QuestGroup>()
                .HasMany(g => g.Quests)
                .WithOne(q => q.QuestGroup)
                .HasForeignKey(q => q.QuestGroupId);

            builder
                .Entity<Theme>()
                .HasMany(t => t.Quests)
                .WithOne(q => q.Theme)
                .HasForeignKey(q => q.ThemeId);

            // Il ne peut y avoir qu'un seul hexAssignment correspondant à chaque jeu de coordonnées, par utilisateur :
            //builder.Entity<HexAssignment>()
            //    .HasIndex(h => new { h.Q, h.R, h.S, h.UserId })
            //    .IsUnique();

            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = Guid.Parse("63a2a3ac-442e-4e4c-ad91-1443122b5a6a"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
                new Role()
                {
                    Id = Guid.Parse("12ccaa16-0d50-491e-8157-ec1b133cf120"),
                    Name = "Client",
                    NormalizedName = "CLIENT",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                },
            };
            builder.Entity<Role>().HasData(roles);

            var statuses = new List<Status>()
            {
                new Status()
                {
                    Id = HardCode.STATUS_WAITING_ID,
                    Name = "À accomplir",
                },
                new Status()
                {
                    Id = HardCode.STATUS_IN_PROGRESS_ID,
                    Name = "En cours",
                },
                new Status()
                {
                    Id = HardCode.STATUS_ON_HOLD_ID,
                    Name = "En attente",
                },
                new Status()
                {
                    Id = HardCode.STATUS_COMPLETED_ID,
                    Name = "Terminée",
                },
            };
            builder.Entity<Status>().HasData(statuses);
        }

        // Accès aux tables :
        public DbSet<UserApp> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Quest> Quests { get; set; }

        public DbSet<HexAssignment> HexAssignments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<FavoriteColor> FavoriteColors { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<QuestGroup> QuestGroups { get; set; }
    }
}
