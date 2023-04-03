using System.ComponentModel.DataAnnotations;

namespace PharmacySupplyProject.Models
{
    public class MedicalRepresentative
    {
        public int Id { get; set; }
        [Key]
        public string Name { get; set; } = string.Empty;
        [StringLength(10)]
        public string ContactNumber { get; set; } = string.Empty;
    }
}
