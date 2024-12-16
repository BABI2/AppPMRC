using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebAppPMRC.Models;

namespace WebAppPMRC.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet pour gérer les tables
        public DbSet<Person> Persons { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Localite> Localites { get; set; }
        public DbSet<NumeroDossier> NumeroDossiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Ajouter les 8 régions du Niger
            modelBuilder.Entity<Region>().HasData(
                new Region { Id = 1, Nom = "Agadez" },
                new Region { Id = 2, Nom = "Diffa" },
                new Region { Id = 3, Nom = "Dosso" },
                new Region { Id = 4, Nom = "Maradi" },
                new Region { Id = 5, Nom = "Niamey" },
                new Region { Id = 6, Nom = "Tahoua" },
                new Region { Id = 7, Nom = "Tillabéri" },
                new Region { Id = 8, Nom = "Zinder" }
            );
        }


    }
}
