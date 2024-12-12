using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppPMRC.Models;

namespace WebAppPMRC.Configurations
{
    public class LocaliteConfiguration : IEntityTypeConfiguration<Localite>
    {
        public void Configure(EntityTypeBuilder<Localite> builder)
        {
            // Nom de la table
            builder.ToTable("Localites");

            // Clé primaire
            builder.HasKey(l => l.Id);

            // Configuration des colonnes
            builder.Property(l => l.Nom).IsRequired().HasMaxLength(100);

            // Configuration de la relation avec Region
            builder.HasOne(l => l.Region)
                   .WithMany(r => r.Localites)
                   .HasForeignKey(l => l.RegionId)
                   .OnDelete(DeleteBehavior.Restrict); // Empêcher la suppression d'une région si des localités existent

            // Index pour les recherches sur le nom de la localité
            builder.HasIndex(l => l.Nom).HasDatabaseName("IX_Localite_Nom");
        }
    }
}
