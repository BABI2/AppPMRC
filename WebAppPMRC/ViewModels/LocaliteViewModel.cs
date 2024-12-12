using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.ViewModels
{
    public class LocaliteViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        public int RegionId { get; set; }

        public string NomRegion { get; set; } = string.Empty;
    }
}
