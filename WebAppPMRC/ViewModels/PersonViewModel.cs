using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Nom { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Prenom { get; set; } = string.Empty;

        [StringLength(50)]
        public string Surnom { get; set; } = string.Empty;

        [Required, StringLength(15), Phone]
        public string Contact { get; set; } = string.Empty;

        [Required]
        public double Longueur { get; set; }

        [Required]
        public double Largeur { get; set; }

        public double Superficie => Longueur * Largeur;

        [Required]
        public decimal PrixM2 { get; set; }

        [Required]
        public decimal MontantComp { get; set; }

        [StringLength(500)]
        public string Commentaire { get; set; } = string.Empty;

        public string? PhotoPath { get; set; }
        public IFormFile? Photo { get; set; }

        [Required]
        public int LocaliteId { get; set; }
        public IEnumerable<SelectListItem> Localites { get; set; } = new List<SelectListItem>();

        [Required]
        public int RegionId { get; set; }
        public IEnumerable<SelectListItem> Regions { get; set; } = new List<SelectListItem>();

        
        public string NumeroDossier { get; set; } = string.Empty;
    }
}
