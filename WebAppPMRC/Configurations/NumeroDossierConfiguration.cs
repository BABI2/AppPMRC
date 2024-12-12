using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppPMRC.Models;

namespace WebAppPMRC.Configurations
{
    public class NumeroDossierConfiguration : IEntityTypeConfiguration<NumeroDossier>
    {
        public void Configure(EntityTypeBuilder<NumeroDossier> builder)
        {
            // Nom de la table
            builder.ToTable("NumeroDossiers");

            // Clé primaire (héritée de BaseEntity)
            builder.HasKey(n => n.Id);

            // Configuration des colonnes
            builder.Property(n => n.DossierNumero).IsRequired().HasMaxLength(100);

            // Configuration de la relation avec Person
            builder.HasOne(n => n.Person)
                   .WithMany()
                   .HasForeignKey(n => n.PersonId)
                   .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade

            // Index sur le numéro de dossier pour optimisation des recherches
            builder.HasIndex(n => n.DossierNumero).HasDatabaseName("IX_NumeroDossier_Numero");
        }
    }
}
