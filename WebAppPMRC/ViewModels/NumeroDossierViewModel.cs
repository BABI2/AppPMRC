using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.ViewModels
{
    public class NumeroDossierViewModel
    {
        public int Id { get; set; }

        [Required]
        public string DossierNumero { get; set; } = string.Empty;

        [Required]
        public int PersonId { get; set; }

        public string NomPerson { get; set; } = string.Empty;

    }
}
