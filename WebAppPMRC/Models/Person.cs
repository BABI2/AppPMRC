using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppPMRC.Models
{
    public class Person : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; } = string.Empty;

        [StringLength(50)]
        public string Surnom { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Phone]
        public string Contact { get; set; } = string.Empty;

        [Required]
        public double Longueur { get; set; }

        [Required]
        public double Largeur { get; set; }

        [NotMapped]
        public double Superficie => Longueur * Largeur;

        [Required]
        [Precision(16, 2)]
        public decimal PrixM2 { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal MontantComp { get; set; }

        [StringLength(500)]
        public string Commentaire { get; set; } = string.Empty;

        public string PhotoPath { get; set; } = string.Empty;

        [Required]
        public int LocaliteId { get; set; }

        public Localite Localite { get; set; }
        public string NumeroDossier { get; set; } = string.Empty;
    }
}
