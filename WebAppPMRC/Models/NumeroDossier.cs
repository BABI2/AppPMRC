using System.ComponentModel.DataAnnotations;

namespace WebAppPMRC.Models
{
    public class NumeroDossier : BaseEntity
    {
        public int PersonId { get; set; }
        public string DossierNumero { get; set; } 

        public Person Person { get; set; }
    }
}
