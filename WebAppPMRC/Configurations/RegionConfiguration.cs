using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppPMRC.Models;

namespace WebAppPMRC.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            // Nom de la table
            builder.ToTable("Regions");

            // Clé primaire
            builder.HasKey(r => r.Id);

            // Configuration des colonnes
            builder.Property(r => r.Nom).IsRequired().HasMaxLength(100);

            // Index pour la recherche rapide sur le nom de la région
            builder.HasIndex(r => r.Nom).HasDatabaseName("IX_Region_Nom");

            // Configuration des localités associées à cette région
            builder.HasMany(r => r.Localites)
                   .WithOne(l => l.Region)
                   .HasForeignKey(l => l.RegionId);
        }
    }
}
