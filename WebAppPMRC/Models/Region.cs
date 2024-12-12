using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.Models
{
    public class Region : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        // Liste des localités associées à cette région
        public ICollection<Localite> Localites { get; set; }
    }
}