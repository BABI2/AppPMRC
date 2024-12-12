using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.ViewModels
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; } = string.Empty;

        // Liste des localités associées à la région (si nécessaire)
        public List<LocaliteViewModel> Localites { get; set; }
    }
}
