using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAppPMRC.Models;

namespace WebAppPMRC.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            // Nom de la table
            builder.ToTable("Persons");

            // Clé primaire (héritée de BaseEntity)
            builder.HasKey(p => p.Id);

            // Configuration des colonnes
            builder.Property(p => p.Nom).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Prenom).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Surnom).HasMaxLength(50);
            builder.Property(p => p.Contact).IsRequired().HasMaxLength(15);

            // Gestion des propriétés numériques
            builder.Property(p => p.Longueur).IsRequired();
            builder.Property(p => p.Largeur).IsRequired();

            // Précision pour les champs de type decimal
            builder.Property(p => p.PrixM2).HasPrecision(16, 2).IsRequired();
            builder.Property(p => p.MontantComp).IsRequired();

            // Gestion des images (PhotoPath)
            builder.Property(p => p.PhotoPath).HasMaxLength(255);

            // Configuration des champs facultatifs
            builder.Property(p => p.Commentaire).HasMaxLength(500);

            // Ignorer les propriétés non mappées
            builder.Ignore(p => p.Superficie); // Propriété calculée (non stockée en base)

            // Configuration optionnelle : index pour les recherches rapides
            builder.HasIndex(p => p.Nom).HasDatabaseName("IX_Person_Nom");
            builder.HasIndex(p => p.Prenom).HasDatabaseName("IX_Person_Prenom");

            // Configuration de la relation avec Localite
            builder.HasOne(p => p.Localite)
                   .WithMany()
                   .HasForeignKey(p => p.LocaliteId)
                   .OnDelete(DeleteBehavior.Restrict); // Eviter la suppression en cascade de Localite
        }
    }
}
