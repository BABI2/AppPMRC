using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.Models
{
    public class Localite : BaseEntity
    {

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        // Une localisation est associée à une région
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
